using Dapper;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

// TODO - Crear clase para actualizar las bbdd de kas y obtener el estado de la licencia.
//bool LicenseExpired = kasLabKey.OpenSubKey("WmiHlp").GetValueNames().Any(
//subkey => subkey.Contains("IsReportedExpired"));

namespace KCI_Library
{
    public class Dependencies
    {
        // TODO - (!!!) Ver cómo almacenar valores genéricos en un diccionario.
        public KasperskyModel KasInfo { get; private set; }
        public AutoInstallRequirementsModel AutoInstallRequirements { get; private set; }
        public List<DatabaseIds> AvailableLicenses { get; private set; }

        private readonly RegistryKey? KasLabKey =
            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\KasperskyLab");

        public Dependencies()
        {
            KasInfo = CreateKasperskyModel();
            AutoInstallRequirements = CreateAutoInstallRequirementsModel();
            AvailableLicenses = new SqlConnector("kci").GetAvailableLicenses(); //<<<<<<<<<<<<<<<<<<<<
        }

        /// <summary>
        /// Comprueba si existe alguno de los productos domésticos de Kaspersky Lab en el equipo local 
        /// mediante una búsqueda de la clave de registro "SOFTWARE\KasperskyLab\*AVP*".
        /// </summary>
        /// <param name="kasLabKey">Verdadero si existe alguno, falso en su defecto.</returns>
        private bool AnyProductInstalled()
        {
            return KasLabKey is null ? false : KasLabKey.GetSubKeyNames().Any(subkey => subkey.Contains("AVP"));
        }

        /// <summary>
        /// Recupera toda la información necesaria sobre el producto doméstico de Kaspersky Lab 
        /// instalado en el equipo local, si lo hubiera.
        /// </summary>
        /// <returns>Diccionario que almacena el tipo de información como clave y su valor obtenido, 
        /// si existe algún producto compatible, en cuyo defecto devuelve un diccionario vacío.</returns>
        private KasperskyModel CreateKasperskyModel()
        {
            bool anyProductInstalled = AnyProductInstalled();

            if (!anyProductInstalled)
                return new KasperskyModel();

            // Ni <KasLabKey>, ni ninguna de sus subclaves, serán nulas aquí
            // si existe algún producto doméstico de KasperskyLab instalado en el equipo.
            string avpKeyName = KasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
            RegistryKey environmentKey = KasLabKey.OpenSubKey($@"{avpKeyName}\environment");
            RegistryKey wmiHlpKey = KasLabKey.OpenSubKey("WmiHlp");
            string productNameValue = environmentKey.GetValue("ProductName").ToString();
            Guid productCodeValue = new(environmentKey.GetValue("ProductCode").ToString());
            DirectoryInfo productRootValue = new(environmentKey.GetValue("ProductRoot").ToString());
            bool isReportedExpired = wmiHlpKey.GetValueNames().Any(val => val.Equals("IsReportedExpired"));

            environmentKey.Close();
            wmiHlpKey.Close();

            DatabaseIds productId = DatabaseIds.none;
            switch (productNameValue)
            {
                case "Kaspersky Antivirus":
                    productId = DatabaseIds.kav;
                    break;
                case "Kaspersky Internet Security":
                    productId = DatabaseIds.kis;
                    break;
                case "Kaspersky Total Security":
                    productId = DatabaseIds.kts;
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
        /// 
        /// </summary>
        /// <returns></returns>
        private AutoInstallRequirementsModel CreateAutoInstallRequirementsModel()
        {
            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            bool databaseAccesible = SqlConnector.GetConnectionState() is ConnectionState.Open;

            // TODO - (!!!) Detección de PasswordProtect no funciona adecuadamente en KTS (resto de versiones sin probar).
            bool passwordProtectionDisabled = false;
            if (KasInfo.Installed)
            {
                string avpKeyName = KasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
                RegistryKey? passwordProtectionSettingsKey = 
                    KasLabKey.OpenSubKey($@"{avpKeyName}\Settings\PasswordProtectionSettings");

                //passwordProtectionDisabled = settingsKey.GetValue("EnablePswrdProtect").ToString().Equals('1');
                passwordProtectionDisabled = passwordProtectionSettingsKey.GetValue("OPEP") is null;

                passwordProtectionSettingsKey.Close();
            }

            bool kasClosed = Process.GetProcessesByName("avp").Length == 0;

            return new AutoInstallRequirementsModel(
                admin,
                databaseAccesible,
                passwordProtectionDisabled,
                kasClosed);
        }
    }
}
