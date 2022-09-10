using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    public class ConfigurationModel : IComparable<ConfigurationModel>
    {
        /// <summary>
        /// Mantener la configuración de la aplicación de Kaspersky instalada en el equipo.
        /// </summary>
        public bool KeepKasConfig { get; private set; } = true;

        // TODO - Obligatorio si la instalación es automática.
        /// <summary>
        /// Utilizar el instalador completo (offline) del producto.
        /// </summary>
        public bool UseOfflineSetup { get; set; }

        // Obligatorio si la base de datos no está disponible.
        /// <summary>
        /// No utilizar las licencias disponibles en la base de datos.
        /// </summary>
        public bool DoNotUseDatabaseLicenses { get; private set; } = true;

        /// <summary>
        /// Instalar Kaspersky Secure Connection, viene por defecto con los productos domésticos de Kaspersky Lab.
        /// </summary>
        public bool InstallKasSecureConnection { get; private set; }

        public ConfigurationModel()
        {

        }

        public ConfigurationModel(bool mantainKasConfig, bool useOfflineSetup, bool doNotUseDatabaseLicenses, bool installKasSecureConnection)
        {
            KeepKasConfig = mantainKasConfig;
            UseOfflineSetup = useOfflineSetup;
            DoNotUseDatabaseLicenses = doNotUseDatabaseLicenses;
            InstallKasSecureConnection = installKasSecureConnection;
        }

        /// <summary>
        /// Compara el modelo actual con otro.
        /// </summary>
        /// <param name="other">Modelo respecto al cual comparar.</param>
        /// <returns><c>1</c> en caso de ser iguales, <c>0</c> en caso de ser diferentes.</returns>
        public int CompareTo(ConfigurationModel? other)
        {
            if (other is null && this is null)
                return 1;
            else if (other is null)
                return 0;

            if (other.KeepKasConfig.Equals(this.KeepKasConfig) &&
                other.UseOfflineSetup.Equals(this.UseOfflineSetup) &&
                other.DoNotUseDatabaseLicenses.Equals(this.DoNotUseDatabaseLicenses) &&
                other.InstallKasSecureConnection.Equals(this.InstallKasSecureConnection))
                return 1;
            else
                return 0;
        }
    }
}
