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
    public static class Dependencies
    {
        public static bool KasIsInstalled { get; private set; } = false;
        public static Dictionary<KasInfoType, string> KasInfo { get; private set; } = new();
        public static Dictionary<AutoInstallRequirementType, bool> AutoInstallRequirements { get; private set; } = new();
        public static List<DatabaseTableIds> AvailableLicenses { get; private set; } = new();

        private static readonly RegistryKey? KasLabKey =
            RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\KasperskyLab");

        //private static RegistryKey LocalMachine32View { get; } = 
        //RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
        //private static RegistryKey LocalMachine64View { get; } = 
        //RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

        public static void ObtainDependencies()
        {
            KasIsInstalled = AnyDomesticProductInstalled();
            KasInfo = ObtainKasInfo();
            AutoInstallRequirements = CheckAutoInstallRequirements();
            AvailableLicenses = GetAvailableLicenses();
        }

        /// <summary>
        /// Enumera los tipos de información necesaria a obtener del producto doméstico 
        /// de Kaspersky Lab, instalado en el equipo local.
        /// </summary>
        public enum KasInfoType
        {
            Avp,
            Name,
            Guid,
            Root,
            LicenseExpired
        }

        // TODO - (?) Innecesario.
        public enum DatabaseTableIds
        {
            kav,
            kis,
            kts
        }

        public enum DatabaseDataType
        {
            OnlineSetupUrl,
            OfflineSetupUrl,
            Licenses,
            LastUpdated
        }

        /// <summary>
        /// Enumera los tipos de requisito necesarios a para realizar 
        /// una instalación automática.
        /// </summary>
        public enum AutoInstallRequirementType
        {
            Admin,
            MyDatabase,
            PasswordProtection,
            Closed
        }

        /// <summary>
        /// Comprueba si existe alguno de los productos domésticos de Kaspersky Lab en el equipo local 
        /// mediante una búsqueda de la clave de registro "SOFTWARE\KasperskyLab\*AVP*".
        /// </summary>
        /// <param name="kasLabKey">Verdadero si existe alguno, falso en su defecto.</returns>
        private static bool AnyDomesticProductInstalled()
        {
            return KasLabKey is null ? false : KasLabKey.GetSubKeyNames().Any(subkey => subkey.Contains("AVP"));
        }

        /// <summary>
        /// Recupera toda la información necesaria sobre el producto doméstico de Kaspersky Lab 
        /// instalado en el equipo local, si lo hubiera.
        /// </summary>
        /// <returns>Diccionario que almacena el tipo de información como clave y su valor obtenido, 
        /// si existe algún producto compatible, en cuyo defecto devuelve un diccionario vacío.</returns>
        private static Dictionary<KasInfoType, string> ObtainKasInfo()
        {
            if (!KasIsInstalled)
                return new Dictionary<KasInfoType, string>();

            // <KasLabKey> nunca será nulo aquí, si lo fuera el método retornaría con anterioridad.
            string avpKeyName = KasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
            RegistryKey environmentKey = KasLabKey.OpenSubKey($@"{avpKeyName}\environment");
            RegistryKey wmiHlpKey = KasLabKey.OpenSubKey("WmiHlp");
            string productNameValue = environmentKey.GetValue("ProductName").ToString();
            string productCodeValue = environmentKey.GetValue("ProductCode").ToString();
            string productRootValue = environmentKey.GetValue("ProductRoot").ToString();
            bool isReportedExpired = wmiHlpKey.GetValueNames().Any(val => val.Equals("IsReportedExpired"));

            environmentKey.Close();
            wmiHlpKey.Close();

            Dictionary<KasInfoType, string> keyValuePairs = new()
            {
                { KasInfoType.Avp, avpKeyName },
                { KasInfoType.Name, productNameValue },
                { KasInfoType.Guid, productCodeValue },
                { KasInfoType.Root, productRootValue },
                { KasInfoType.LicenseExpired, isReportedExpired.ToString() }
            };

            return keyValuePairs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static Dictionary<AutoInstallRequirementType, bool> CheckAutoInstallRequirements()
        {
            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            bool dbIsAccesible = SqlConnector.GetConnectionState() is ConnectionState.Open;

            // TODO - (!!!) Detección de PasswordProtect no funciona adecuadamente en KTS (resto de versiones sin probar).
            bool passwordProtection = false;
            if (KasIsInstalled)
            {
                string avpKeyName = KasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
                //RegistryKey? settingsKey = KasLabKey.OpenSubKey($@"{avpKeyName}\Settings");
                RegistryKey? passwordProtectionSettingsKey = 
                    KasLabKey.OpenSubKey($@"{avpKeyName}\Settings\PasswordProtectionSettings");

                //passwordProtection = settingsKey.GetValue("EnablePswrdProtect").ToString().Equals('1');
                passwordProtection = passwordProtectionSettingsKey.GetValue("OPEP") is not null;

                passwordProtectionSettingsKey.Close();
            }

            bool kasIsClosed = Process.GetProcessesByName("avp").Length == 0;

            Dictionary<AutoInstallRequirementType, bool> keyValuePairs = new()
            {
                { AutoInstallRequirementType.Admin, admin },
                { AutoInstallRequirementType.MyDatabase, dbIsAccesible},
                { AutoInstallRequirementType.PasswordProtection, passwordProtection},
                { AutoInstallRequirementType.Closed, kasIsClosed }
            };

            return keyValuePairs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static List<DatabaseTableIds> GetAvailableLicenses()
        {
            MySqlConnection connection = SqlConnector.GetConnection();
            DynamicParameters p = new();
            p.Add("id", dbType: DbType.String, direction: ParameterDirection.Output);

            connection.Execute("kci.sources_licensenotnull", p, commandType: CommandType.StoredProcedure);

            // Se deben separar los valores de la Query porque puede devolver varias filas agrupadas.
            string[] strArr = p.Get<string>("id").Split(',');

            List<DatabaseTableIds> keyValuePairs = new();

            foreach (string str in strArr)
                switch (str)
                {
                    case "kav":
                        keyValuePairs.Add(DatabaseTableIds.kav);
                        break;
                    case "kis":
                        keyValuePairs.Add(DatabaseTableIds.kis);
                        break;
                    case "kts":
                        keyValuePairs.Add(DatabaseTableIds.kts);
                        break;
                }

            return keyValuePairs;
        }

        // TODO - Controlar excepciones.
        // TODO - (?) Usar enum.
        // TODO - Ver qué licencias están disponibles en la base de datos.
        public static Dictionary<DatabaseDataType, string> GetDatabaseData(DatabaseTableIds id)
        {
            MySqlConnection connection = SqlConnector.GetConnection();

            DynamicParameters p = new();
            p.Add("id", id.ToString());
            p.Add(DatabaseDataType.OnlineSetupUrl.ToString(), dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add(DatabaseDataType.OfflineSetupUrl.ToString(), dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add(DatabaseDataType.Licenses.ToString(), dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add(DatabaseDataType.LastUpdated.ToString(), dbType: DbType.DateTime2, direction: ParameterDirection.Output);

            connection.Execute("kci.sources_select", p, commandType: CommandType.StoredProcedure);

            return new Dictionary<DatabaseDataType, string>
            {
                { DatabaseDataType.OnlineSetupUrl, p.Get<string>(DatabaseDataType.OnlineSetupUrl.ToString()) },
                { DatabaseDataType.OfflineSetupUrl, p.Get<string>(DatabaseDataType.OfflineSetupUrl.ToString()) },
                { DatabaseDataType.Licenses, p.Get<string>(DatabaseDataType.Licenses.ToString()) },
                { DatabaseDataType.LastUpdated, Encoding.Default.GetString(p.Get<byte[]>(DatabaseDataType.LastUpdated.ToString())) }
            };
        }
    }
}
