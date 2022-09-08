using Dapper;
using KCI_Library.Models;
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
        /// <summary>
        /// Recupera toda la información necesaria sobre el producto doméstico de Kaspersky Lab 
        /// instalado en el equipo local, si lo hubiera.
        /// </summary>
        /// <returns>Diccionario que almacena el tipo de información como clave y su valor obtenido, 
        /// si existe algún producto compatible, en cuyo defecto devuelve un diccionario vacío.</returns>
        public static KasperskyModel CreateKasperskyModel()
        {
            bool anyProductInstalled = AnyProductInstalled(out RegistryKey? kasLabKey);

            if (!anyProductInstalled)
                return new KasperskyModel();

            // Ni <KasLabKey>, ni ninguna de sus subclaves, serán nulas aquí
            // si existe algún producto doméstico de KasperskyLab instalado en el equipo.
            string avpKeyName = kasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
            RegistryKey environmentKey = kasLabKey.OpenSubKey($@"{avpKeyName}\environment");
            RegistryKey wmiHlpKey = kasLabKey.OpenSubKey("WmiHlp");
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
        /// 
        /// </summary>
        /// <returns></returns>
        public static AutoInstallRequirementsModel CreateAutoInstallRequirementsModel(bool databaseAccesible)
        {
            bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            // TODO - (!!!) Detección de PasswordProtect no funciona adecuadamente en KTS (resto de versiones sin probar).
            bool passwordProtectionDisabled = false;
            if (AnyProductInstalled(out RegistryKey? kasLabKey))
            {
                string avpKeyName = kasLabKey.GetSubKeyNames().First(subkey => subkey.Contains("AVP"));
                RegistryKey? passwordProtectionSettingsKey = 
                    kasLabKey.OpenSubKey($@"{avpKeyName}\Settings\PasswordProtectionSettings");

                //passwordProtectionDisabled = settingsKey.GetValue("EnablePswrdProtect").ToString().Equals('1');
                passwordProtectionDisabled = passwordProtectionSettingsKey.GetValue("OPEP") is null;

                kasLabKey.Close();
                passwordProtectionSettingsKey.Close();
            }

            bool kasClosed = Process.GetProcessesByName("avp").Length == 0;

            return new AutoInstallRequirementsModel(
                databaseAccesible,
                admin,
                passwordProtectionDisabled,
                kasClosed);
        }

        private static bool AnyProductInstalled(out RegistryKey? kasLabKey)
        {
            kasLabKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\KasperskyLab");
            return kasLabKey is null ? false : kasLabKey.GetSubKeyNames().Any(subkey => subkey.Contains("AVP"));
        }
    }
}
