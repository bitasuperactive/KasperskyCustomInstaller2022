using KCI_Library.Models;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;

namespace KCI_Library.DataAccess
{
    public static class Dependencies
    {
        /// <summary>
        /// Comprueba si existe algún producto doméstico de Kaspersky Lab 
        /// instalado en el equipo local.
        /// </summary>
        /// <param name="kasLabKey">Clave de registro principal del producto.</param>
        /// <returns><c>Verdadero</c> si existe agún producto, <c>Falso</c> en su defecto.</returns>
        public static bool AnyProductInstalled(out RegistryKey? kasLabKey)
        {
            kasLabKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\KasperskyLab");
            return kasLabKey is null ? false : kasLabKey.GetSubKeyNames().Any(subkey => subkey.Contains("AVP"));
        }

        /// <summary>
        /// Crea un modelo <c>KasperskyModel</c>.
        /// </summary>
        /// <returns><see cref="KasperskyModel"/></returns>
        public static KasperskyModel CreateKasperskyModel()
        {
            if (!AnyProductInstalled(out RegistryKey? kasLabKey))
                return new KasperskyModel();

            // * Ni la clave KasperskyLab, ni ninguna de sus subclaves serán nulas aquí si existe algún producto instalado.
            string avpKeyName = kasLabKey!.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
            using RegistryKey environmentKey = kasLabKey.OpenSubKey($@"{avpKeyName}\environment")!;
            using RegistryKey wmiHlpKey = kasLabKey.OpenSubKey("WmiHlp")!;
            string productName = environmentKey.GetValue("ProductName")!.ToString()!;
            string productCode = environmentKey.GetValue("ProductCode")!.ToString()!;
            DirectoryInfo productRoot = new(environmentKey.GetValue("ProductRoot")!.ToString()!);
            bool isReportedExpired = wmiHlpKey.GetValueNames().Any(value => value.Equals("IsReportedExpired"));

            // Excepto las claves de KSDE que es un producto opcinal.
            string ksdeKeyName = kasLabKey!.GetSubKeyNames().First(subkey => subkey.Contains("KSDE"));
            using RegistryKey? ksdeKey = kasLabKey!.OpenSubKey($@"{ksdeKeyName}\environment");
            bool ksdeInstalled = ksdeKey is not null;
            KsdeModel ksdeModel = (ksdeInstalled) ? new(ksdeKey!.GetValue("ProductCode")!.ToString()!) : new();

            kasLabKey.Close();

            /*
            switch (productName)
            {
                case "Kaspersky Antivirus":
                    productId = DatabaseId.KAV;
                    break;
                case "Kaspersky Internet Security":
                    productId = DatabaseId.KIS;
                    break;
                case "Kaspersky Total Security":
                    productId = DatabaseId.KTS;
                    break;
            }
            */
            // Extraer el nombre abreviado del producto.
            // TODO - (!!!) Lanza ArgumentException si la abreviación no coincide con ningún enumerador.
            ProductId productId = ProductId.none;
            string productNameAbreviated = new string(productName.Split(' ').Select(c => c[0]).ToArray());
            try
            {
                productId = (ProductId)Enum.Parse(typeof(ProductId), productNameAbreviated);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }

            return new KasperskyModel(
                productId,
                productName,
                productCode,
                productRoot,
                avpKeyName,
                isReportedExpired,
                ksdeModel);
        }

        /// <summary>
        /// Crea un modelo <c>AutoInstallRequirementsModel</c>.
        /// </summary>
        /// <returns><see cref="AutoInstallRequirementsModel"/></returns>
        // TODO - (!!!) Testear cancelacion mediante Wait().
        public static async Task<AutoInstallRequirementsModel> CreateAutoInstallRequirementsModel(IProgress<ProgressReportModel> progress, CancellationToken cancellation, bool waitForRegistryToUpdate = false)
        {
            progress.Report(new(0, "Conectando a la base de datos"));
            bool databaseAccesible = await SqlConnector.DatabaseAccesible(cancellation);

            //if (!databaseAccesible)
            //    return new AutoInstallRequirementsModel();

            cancellation.ThrowIfCancellationRequested();
            progress.Report(new(33, "Verificando privilegios del usuario"));
            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            ProgressReportModel progressReport = new(35, "Comprobando protección por contraseña de AVP");
            progress.Report(progressReport);
            bool passwordProtectionDisabled = false;
            if (AnyProductInstalled(out RegistryKey? kasLabKey))
            {
                // Ni la clave de KasperskyLab, ni ninguna de sus subclaves serán nulas si existe algún producto instalado.
                string avpKeyName = kasLabKey!.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
                RegistryKey passwordProtectionSettingsKey = kasLabKey.OpenSubKey($@"{avpKeyName}\Settings\PasswordProtectionSettings")!;

                passwordProtectionDisabled = await PasswordProtectionDisabled();

                kasLabKey.Close();
                passwordProtectionSettingsKey.Close();


                // TODO - (!) La detección de PasswordProtect tarda hasta 30s en actualizar.
                async Task<bool> PasswordProtectionDisabled()
                {
                    if (!waitForRegistryToUpdate)
                        return string.IsNullOrEmpty(passwordProtectionSettingsKey.GetValue("OPEP")!.ToString());

                    for (int i = 1; i <= 60; i++)
                    {
                        if (string.IsNullOrEmpty(passwordProtectionSettingsKey.GetValue("OPEP")!.ToString()))
                            return true;

                        await Task.Delay(500);
                        cancellation.ThrowIfCancellationRequested();
                        progress.Report(new(progressReport.Percentage + i, progressReport.Description));
                    }

                    return false;
                }
            }

            progress.Report(new(96, "Comprobando instancias abiertas de AVP"));
            bool kasClosed = Process.GetProcessesByName("avp").Length == 0;

            progress.Report(new(100, "Requisitos obtenidos con éxito"));
            return new AutoInstallRequirementsModel(
                admin,
                passwordProtectionDisabled,
                kasClosed,
                databaseAccesible);
        }
    }
}
