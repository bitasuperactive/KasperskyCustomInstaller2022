using KCI_Library.DataAccess;
using KCI_Library.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class DefaultInstallation2
    {
        private InstallationModel installation;
        private KasperskyModel kaspersky;

        public DefaultInstallation2(InstallationModel installation)
        {
            this.installation = installation;
        }

        public async Task RunInstallation()
        {
            await InstallClient();
            await UpdateClientDatabase();

            if (!installation.Configuration.DoNotUseDatabaseLicenses)
                await ActivateClient(RecoverLicenses());
            if (installation.Configuration.KeepKasperskyConfig)
                await ImportClientConfiguration();
            if (!installation.Configuration.KasperskySecureConnection)
                await UninstallKsde();
        }

        protected virtual async Task InstallClient()
        {
            installation.Progress.Report(new(0, "Instalando cliente"));

            await Process.Start(Path.Combine(Path.GetTempPath(), InstallationModel.InstallerFileName)).WaitForExitAsync(installation.Cancellation);
            await Process.GetProcessesByName("startup")[0].WaitForExitAsync(installation.Cancellation);

            kaspersky = Dependencies.CreateKasperskyModel();

            if (!kaspersky.Installed)
            {
                throw new InvalidOperationException($"La instalación de Kaspersky ha sido interrumpida.");  // TODO - Mantener el objeto configuration.
            }
        }

        protected async Task UpdateClientDatabase()
        {
            installation.Progress.Report(new(0, "Actualizando cliente"));

            string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", "Update", installation.Cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_update_log.txt"), log);
        }

        // *** ¿Cómo activar licencia de prueba?
        protected async Task ActivateClient(string[] licenses)
        {
            installation.Progress.Report(new(0, "Activando cliente"));

            foreach (string license in licenses)
            {
                string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com", $"License /add {license}", installation.Cancellation);

                if (ActivationSuccess())
                {
                    File.AppendAllText(Path.Combine(Path.GetTempPath(), "kci_activacion_log.txt"), $"Activación exitosa: {license}");
                    return;
                }
                else
                {
                    // Guardar código de error.
                    // Licencia no válida: 0x80010102
                    // Número máximo de activaciones excedido: 0xA045001C
                    string errorCode = Regex.Match(log, @"0x[\da-fA-F]+").Value;
                    File.AppendAllText(Path.Combine(Path.GetTempPath(), "kci_activacion_log.txt"), $"Error de activación: {errorCode}");
                }
            }

            throw new ArgumentException($"No ha sido posible activar {kaspersky.FullName}.");

            bool ActivationSuccess()
            {
                using RegistryKey LocalMachine32View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                using RegistryKey wmiHlpSubKey = LocalMachine32View.OpenSubKey(@"SOFTWARE\KasperskyLab\WmiHlp")!;
                return !wmiHlpSubKey.GetValueNames().Contains("IsReportedExpired");
            }
        }

        protected async Task ImportClientConfiguration()
        {
            installation.Progress.Report(new(0, "Importando configuración"));

            string log = await ProcessExecutor.WindowHidden($@"{kaspersky.Root}\avp.com",
                $"import {Path.Combine(Path.GetTempPath(), InstallationModel.ConfigFileName)}", installation.Cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "kas_config_import_log.txt"), log);
        }

        protected async Task UninstallKsde()
        {
            installation.Progress.Report(new(0, "Desinstalando Kaspersky Secure Connection"));

            if (!kaspersky.Ksde.Installed)
                return;

            string log = await ProcessExecutor.WindowHidden("msiexec.exe", $"/x {kaspersky.Ksde.Guid} /passive", installation.Cancellation);
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "ksde_uninstall_log.txt"), log); // empty
        }

        protected string[] RecoverLicenses()
        {
            string licensesPath = Path.Combine(Path.GetTempPath(), InstallationModel.LicensesFileName);

            if (!File.Exists(licensesPath))
                throw new FileNotFoundException($"El archivo {licensesPath} no existe.");

            using StreamReader reader = new(licensesPath);

            int licenses_length = int.Parse(reader.ReadLine()!);
            if (licenses_length == 0)
            {
                return Array.Empty<string>();
            }

            string[] licenses = new string[licenses_length];
            for (int i = 0; i < licenses_length; i++)
            {
                licenses[i] = reader.ReadLine()!;
            }
            return licenses;
        }
    }
}
