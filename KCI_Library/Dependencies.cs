using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        public static Dictionary<KasInfoType, string> KasInfo { get; private set; } =
            new Dictionary<KasInfoType, string>();
        public static Dictionary<AutoInstallRequirementType, bool> AutoInstallRequirements { get; private set; } =
            new Dictionary<AutoInstallRequirementType, bool>();

        private static RegistryKey? KasLabKey { get; } =
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
            Root
        }

        /// <summary>
        /// 
        /// </summary>
        public enum AutoInstallRequirementType
        {
            Admin,
            MyDatabase,
            PasswordProtection,
            Active
        }

        /// <summary>
        /// Comprueba si existe alguno de los productos domésticos de Kaspersky Lab en el equipo local 
        /// mediante una búsqueda de la clave de registro "SOFTWARE\KasperskyLab\*AVP*".
        /// </summary>
        /// <param name="kasLabKey">Verdadero si existe alguno, falso en su defecto.</returns>
        private static bool AnyDomesticProductInstalled()
        {
            return (KasLabKey is null) ? false : KasLabKey.GetSubKeyNames().Any(subkey => subkey.Contains("AVP"));
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

            // <kasLabKey> nunca será nulo aquí, si lo fuera el método retornaría con anterioridad.
            string avpKeyName = KasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
            RegistryKey environmentKey = KasLabKey.OpenSubKey($@"{avpKeyName}\environment");
            string productNameValue = environmentKey.GetValue("ProductName").ToString();
            string productCodeValue = environmentKey.GetValue("ProductCode").ToString();
            string productRootValue = environmentKey.GetValue("ProductRoot").ToString();

            environmentKey.Close();

            Dictionary<KasInfoType, string> keyValuePairs = new()
            {
                { KasInfoType.Avp, avpKeyName },
                { KasInfoType.Name, productNameValue },
                { KasInfoType.Guid, productCodeValue },
                { KasInfoType.Root, productRootValue }
            };

            return keyValuePairs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kasLabKey"></param>
        /// <returns></returns>
        private static Dictionary<AutoInstallRequirementType, bool> CheckAutoInstallRequirements()
        {
            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            // TODO - Comprobar si la bbdd se encuentra disponible.
            bool dbIsAccesible = false;

            // TODO - (!!!) Detección de PasswordProtect no funciona en KTS (demás versiones sin probar).
            bool passwordProtection = false;
            if (KasIsInstalled)
            {
                string avpKeyName = KasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
                RegistryKey? flushedMsiCriticalSettingsKey = KasLabKey.OpenSubKey($@"{avpKeyName}\Settings");

                passwordProtection = flushedMsiCriticalSettingsKey.GetValue("EnablePswrdProtect").ToString().Equals('1');

                flushedMsiCriticalSettingsKey.Close();
            }

            bool kasIsActive = Process.GetProcessesByName("avp").Length > 0;

            Dictionary<AutoInstallRequirementType, bool> keyValuePairs = new()
            {
                { AutoInstallRequirementType.Admin, admin },
                { AutoInstallRequirementType.MyDatabase, dbIsAccesible},
                { AutoInstallRequirementType.PasswordProtection, passwordProtection},
                { AutoInstallRequirementType.Active, kasIsActive }
            };

            return keyValuePairs;
        }
    }
}
