using KCI_Library.Models;
using Microsoft.Win32;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;

namespace KCI_Library.DataAccess
{
    public static class Dependencies
    {
        /// <summary>
        /// Crea un modelo <c>KasperskyModel</c>.
        /// </summary>
        /// <returns><see cref="KasperskyModel"/></returns>
        public static KasperskyModel CreateKasperskyModel()
        {
            bool anyProductInstalled = AnyProductInstalled(out RegistryKey? kasLabKey);

            if (!anyProductInstalled)
                return new KasperskyModel();

            // Ni <kasLabKey>, ni ninguna de sus subclaves serán nulas aquí si existe algún producto instalado.
            string avpKeyName = kasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
            RegistryKey? environmentKey = kasLabKey.OpenSubKey($@"{avpKeyName}\environment");
            RegistryKey? wmiHlpKey = kasLabKey.OpenSubKey("WmiHlp");
            string productNameValue = environmentKey.GetValue("ProductName").ToString();
            Guid productCodeValue = new(environmentKey.GetValue("ProductCode").ToString());
            DirectoryInfo productRootValue = new(environmentKey.GetValue("ProductRoot").ToString());
            bool isReportedExpired = wmiHlpKey.GetValueNames().Any(val => val.Equals("IsReportedExpired"));

            kasLabKey.Close();
            environmentKey.Close();
            wmiHlpKey.Close();

            DatabaseId productId = DatabaseId.none;
            switch (productNameValue)
            {
                case "Kaspersky Antivirus":
                    productId = DatabaseId.kav;
                    break;
                case "Kaspersky Internet Security":
                    productId = DatabaseId.kis;
                    break;
                case "Kaspersky Total Security":
                    productId = DatabaseId.kts;
                    break;
            }

            return new KasperskyModel(
                anyProductInstalled,
                productId,
                productNameValue,
                productCodeValue,
                productRootValue,
                avpKeyName,
                isReportedExpired);
        }

        /// <summary>
        /// Crea un modelo <c>AutoInstallRequirementsModel</c>.
        /// </summary>
        /// <returns><see cref="AutoInstallRequirementsModel"/></returns>
        public static async Task<AutoInstallRequirementsModel> CreateAutoInstallRequirementsModel(IProgress<ProgressReportModel> progress, CancellationToken cancellation, bool waitForPwrdProtection)
        {
            ProgressReportModel progressReport = new();

            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            progress.Report(progressReport.Set("Privilegios del usuario obtenidos", 1));

            bool passwordProtectionDisabled = false;
            if (AnyProductInstalled(out RegistryKey? kasLabKey))
            {
                // Ni <kasLabKey>, ni ninguna de sus subclaves serán nulas aquí si existe algún producto instalado.
                string avpKeyName = kasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
                RegistryKey passwordProtectionSettingsKey =
                    kasLabKey.OpenSubKey($@"{avpKeyName}\Settings\PasswordProtectionSettings");

                passwordProtectionDisabled = await PasswordProtectionDisabled();

                kasLabKey.Close();
                passwordProtectionSettingsKey.Close();


                // TODO - (!) La detección de PasswordProtect tarda hasta 30s en actualizar.
                async Task<bool> PasswordProtectionDisabled()
                {
                    if (!waitForPwrdProtection)
                        return string.IsNullOrEmpty(passwordProtectionSettingsKey.GetValue("OPEP").ToString());

                    for (int i = 1; i <= 30; i++)
                    {
                        if (string.IsNullOrEmpty(passwordProtectionSettingsKey.GetValue("OPEP").ToString()))
                            return true;

                        progress.Report(progressReport.Set("Esperando por PasswordProtectionEnabled", progressReport.LastProgressValue + i * 4));
                        await Task.Delay(1000);
                        cancellation.ThrowIfCancellationRequested();
                    }

                    return false;
                }
            }

            bool kasClosed = Process.GetProcessesByName("avp").Length == 0;
            progress.Report(progressReport.Set("Instancias abiertas de AVP contadas", 91));

            cancellation.ThrowIfCancellationRequested();
            bool databaseAccesible = await SqlConnector.DatabaseAccesible();
            cancellation.ThrowIfCancellationRequested();
            progress.Report(progressReport.Set("Base de datos accedida", 100));

             return new AutoInstallRequirementsModel(
                 admin, 
                 passwordProtectionDisabled, 
                 kasClosed, 
                 databaseAccesible);
        }

        /// <summary>
        /// Comprueba si existe algún producto doméstico de Kaspersky Lab 
        /// instalado en el equipo local.
        /// </summary>
        /// <param name="kasLabKey">Clave de registro principal del producto.</param>
        /// <returns><c>Verdadero</c> si existe agún producto, <c>Falso</c> en su defecto.</returns>
        private static bool AnyProductInstalled(out RegistryKey? kasLabKey)
        {
            kasLabKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\KasperskyLab");
            return kasLabKey is null ? false : kasLabKey.GetSubKeyNames().Any(subkey => subkey.Contains("AVP"));
        }
    }
}
