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
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

namespace KCI_Library
{
    public class DefaultInstallation
    {
        private KasperskyModel kaspersky;
        private ConfigurationModel configuration;
        private IProgress<ProgressReportModel> progress;
        private CancellationToken cancellation;
        private SourcesModel sources;
        private string dataPath = Path.Combine(Path.GetTempPath(), "kci_data.txt");

        public DefaultInstallation(KasperskyModel kaspersky, ConfigurationModel configuration, IProgress<ProgressReportModel> progress, CancellationToken cancellation)
        {
            this.kaspersky = kaspersky;
            this.configuration = configuration;
            this.progress = progress;
            this.cancellation = cancellation;
        }

        public async Task RunInstallation()
        {
            await ExportClientConfiguration();
            await UninstallClient();
        }

        public async Task FinishInstallation()
        {
            GetDataFromFile(out string filePath, out string[]? licenses);
        }

        /// <summary>
        /// Throws:
        /// HttpRequestException
        /// OperationCancelledException
        /// </summary>
        /// <returns></returns>
        public async Task DownloadSources()
        {
            sources = await SqlConnector.CreateSourcesModel(configuration.ProductToInstall);
            Uri setupUri = configuration.OfflineSetup ? sources.OfflineSetupUri : sources.OnlineSetupUri;
            string filePath = Path.Combine(Path.GetTempPath(), "kas_installer.exe");

            using HttpClient client = new();
            using HttpResponseMessage response = await client.GetAsync(setupUri, HttpCompletionOption.ResponseHeadersRead, cancellation);

            response.EnsureSuccessStatusCode();

            long? totalBytes = response.Content.Headers.ContentLength;

            using Stream stream = await response.Content.ReadAsStreamAsync(cancellation);
            using Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

            byte[] buffer = new byte[8192];
            int bytesRead = 0;
            long totalBytesRead = 0L;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                cancellation.ThrowIfCancellationRequested();

                await fileStream.WriteAsync(buffer, 0, bytesRead);

                totalBytesRead += bytesRead;

                int percentage = (int)(100 * (double)totalBytesRead / totalBytes.GetValueOrDefault());
                progress.Report(new(percentage, "Descargando instalador"));
            }

            SaveDataToFile(filePath, sources.Licenses);
        }

        protected async Task ExportClientConfiguration()  // *** Guardar configPath
        {
            string configPath = Path.Combine(Path.GetTempPath(), "kas_config.cfg");
            string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", $"export {configPath}", cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_config_export_log.txt"), log);
        }

        protected virtual async Task UninstallClient()
        {
            string log = await ProcessExecutor.WindowHidden("msiexec.exe", $"/i {kaspersky.Guid}", cancellation);  //  /norestart
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_uninstall_log.txt"), log);

            if (Dependencies.AnyProductInstalled(out RegistryKey? kaslabKey))
            {
                kaslabKey!.Close();
                throw new InvalidOperationException($"La desinstalación de {kaspersky.FullName} ha sido interrumpida.");
            }

            // *** restartRequired = true
        }

        protected void RegistryCleanUp()
        {
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

            void DeleteSubKeyTree(string name)
            {
                using RegistryKey? key32 = LocalMachine32View.OpenSubKey(name);
                using RegistryKey? key64 = LocalMachine32View.OpenSubKey(name);

                if (key32 != null)
                    LocalMachine32View.DeleteSubKeyTree(name);
                if (key64 != null)
                    LocalMachine64View.DeleteSubKeyTree(name);
            }
        }

        protected virtual void Restart()
        {
            throw new NotImplementedException();
        }

        protected virtual async Task InstallClient(string filePath)
        {
            await Process.Start(filePath).WaitForExitAsync(cancellation);

            kaspersky = Dependencies.CreateKasperskyModel();

            if (!kaspersky.Installed)
            {
                throw new InvalidOperationException($"La instalación de Kaspersky ha sido interrumpida.");  // TODO - Mantener el objeto configuration.
            }
        }

        protected async Task UpdateClientDatabase()
        {
            string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", "Update", cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_update_log.txt"), log);
        }

        protected async Task ActivateClient(string[] licenses)
        {
            foreach (string license in licenses)
            {
                string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", $"License /add {license}", cancellation);

                if (ActivationSuccess(log))
                    return;
            }

            throw new ArgumentException("No ha sido posible activar la aplicación.");

            bool ActivationSuccess(string log)
            {
                return !log.Contains("failed"); // AddKeyOrActivationCode failed, result = 0x80010102

                // using RegistryKey LocalMachine32View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                // using RegistryKey wmiHlpSubKey = LocalMachine32View.OpenSubKey(@"SOFTWARE\KasperskyLab\WmiHlp");
                // return !wmiHlpSubKey.GetValueNames().Contains("IsReportedExpired");
            }
        }

        protected async Task ImportClientConfiguration(string configPath)
        {
            string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", $"import {configPath}", cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_config_import_log.txt"), log);
        }

        protected async Task UninstallKsde()
        {
            if (!kaspersky.Ksde.Installed)
                return;

            Process[] processes = Process.GetProcessesByName("ksde").Concat(Process.GetProcessesByName("ksdeui")).ToArray();
            foreach (Process p in processes)
            {
                p.Kill();
            }

            string log = await ProcessExecutor.WindowHidden("msiexec.exe", $"/x {kaspersky.Ksde.Guid} /quiet", cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "ksde_uninstall_log.txt"), log);
        }

        protected void SaveDataToFile(string filePath, string[]? licenses)
        {
            using StreamWriter writer = new(dataPath, true);

            writer.WriteLine(filePath);

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

        protected void GetDataFromFile(out string filePath, out string[] licenses)
        {
            if (!File.Exists(dataPath))
                throw new FileNotFoundException($"El archivo {dataPath} no existe.");

            using StreamReader reader = new(dataPath);

            filePath = reader.ReadLine()!;

            int licenses_length = int.Parse(reader.ReadLine()!);
            if (licenses_length == 0)
            {
                licenses = Array.Empty<string>();
                return;
            }

            licenses = new string[licenses_length];
            for (int i = 0; i < licenses_length; i++)
                licenses[i] = reader.ReadLine()!;
        }
    }
}
