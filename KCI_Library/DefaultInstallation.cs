using KCI_Library.DataAccess;
using KCI_Library.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ubiety.Dns.Core;

namespace KCI_Library
{
    public class DefaultInstallation
    {
        private InstallationModel installation;
        private string applicationPath;  // System.Reflection.Assembly.GetEntryAssembly().Location;
        private KasperskyModel kaspersky;
        private SourcesModel sources;
        private event EventHandler rebootEvent;

        public DefaultInstallation(InstallationModel installation, string applicationPath, KasperskyModel kaspersky, EventHandler rebootEvent)
        {
            this.installation = installation;
            this.applicationPath = applicationPath;
            this.kaspersky = kaspersky;
            this.rebootEvent = rebootEvent;
        }

        public async Task RunInstallation()
        {
            await DownloadSources();

            if (installation.Configuration.KeepKasperskyConfig)
                await ExportClientConfiguration();
            if (kaspersky.Installed)
                await UninstallClient();
            RegistryCleanUp();
            RebootInvoke();
        }

        /// <summary>
        /// Throws:
        /// HttpRequestException
        /// OperationCancelledException
        /// </summary>
        /// <returns></returns>
        public async Task DownloadSources()
        {
            installation.Progress.Report(new(0, "Descargando recursos"));

            sources = await SqlConnector.CreateSourcesModel(installation.Configuration.ProductToInstall, installation.Configuration.DoNotUseDatabaseLicenses);
            Uri setupUri = installation.Configuration.OfflineSetup ? sources.OfflineSetupUri! : sources.OnlineSetupUri;
            string filePath = Path.Combine(Path.GetTempPath(), InstallationModel.InstallerFileName);

            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(setupUri, HttpCompletionOption.ResponseHeadersRead, installation.Cancellation);

            response.EnsureSuccessStatusCode();

            long? totalBytes = response.Content.Headers.ContentLength;

            using Stream stream = await response.Content.ReadAsStreamAsync(installation.Cancellation);
            using Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            byte[] buffer = new byte[8192];
            int bytesRead = 0;
            long totalBytesRead = 0L;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                installation.Cancellation.ThrowIfCancellationRequested();

                await fileStream.WriteAsync(buffer, 0, bytesRead);

                totalBytesRead += bytesRead;

                int percentage = (int)(100 * (double)totalBytesRead / totalBytes.GetValueOrDefault());
                installation.Progress.Report(new(percentage, "Descargando instalador"));
            }

            //if (!installation.Configuration.DoNotUseDatabaseLicenses)
                StoreLicenses(sources.Licenses);
        }

        protected async Task ExportClientConfiguration()
        {
            installation.Progress.Report(new(0, "Exportando configuración"));

            string configPath = Path.Combine(Path.GetTempPath(), InstallationModel.ConfigFileName);
            string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", $"export {configPath}", installation.Cancellation);

            if (string.IsNullOrEmpty(log) || !File.Exists(configPath))
                throw new Exception("No ha sido posible exportar la configuración.");
        }

        protected virtual async Task UninstallClient()
        {
            installation.Progress.Report(new(0, "Desintalando antivirus"));

            string log = await ProcessExecutor.WindowHidden("msiexec.exe", $"/i {kaspersky.Guid} /norestart REMOVE=ALL", installation.Cancellation);  //  /norestart
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_uninstall_log.txt"), log); // null

            if (Dependencies.AnyProductInstalled(out RegistryKey? kaslabKey))
            {
                kaslabKey!.Close();
                throw new InvalidOperationException($"La desinstalación de {kaspersky.FullName} ha sido interrumpida.");
            }
        }

        protected void RegistryCleanUp() // **********************************************************************
        {
            installation.Progress.Report(new(0, "Limpiando registro"));

            using RegistryKey LocalMachine32View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            using RegistryKey LocalMachine64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            try
            {
                DeleteSubKeyTree(@"SOFTWARE\KasperskyLab");
                DeleteSubKeyTree(@"SOFTWARE\Microsoft\SystemCertificates\SPC\Certificates");
                DeleteSubKeyTree(@"SOFTWARE\Microsoft\Cryptography\RNG");
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (SecurityException)
            {
                //throw;
            }

            void DeleteSubKeyTree(string name)
            {
                using RegistryKey? key32 = LocalMachine32View.OpenSubKey(name, true);
                using RegistryKey? key64 = LocalMachine64View.OpenSubKey(name, true);

                if (key32 != null)
                {
                    key32!.DeleteSubKeyTree("");
                }
                if (key64 != null)
                {
                    key64!.DeleteSubKeyTree("");
                }
            }
        }

        protected virtual void RebootInvoke()
        {
            installation.Progress.Report(new(0, "Reiniciando"));

            using (RegistryKey runOnceKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\RunOnce", true)!)
            {
                runOnceKey.SetValue("KasperskyCustomInstaller", applicationPath, RegistryValueKind.String);
            }

            rebootEvent.Invoke(this, EventArgs.Empty);
        }

        protected void StoreLicenses(string[]? licenses)
        {
            using StreamWriter writer = new(Path.Combine(Path.GetTempPath(), InstallationModel.LicensesFileName));

            if (licenses is null)
            {
                writer.WriteLine(0);
                return;
            }

            writer.Write(licenses.Length);
            foreach (string str in licenses)
            {
                writer.WriteLine(str);
            }
        }
    }
}
