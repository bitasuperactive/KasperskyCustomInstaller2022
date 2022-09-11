namespace KCI_Library.Models
{
    public class ConfigurationModel : IComparable<ConfigurationModel>
    {
        /// <summary>
        /// Mantener la configuración de la aplicación del producto instalado.
        /// </summary>
        public bool KeepKasperskyConfig { get; private set; } = true;
        /// <summary>
        /// Utilizar el instalador completo (offline) del producto seleccionado a instalar.
        /// </summary>
        // TODO - Obligatorio si la instalación es automática.
        public bool OfflineSetup { get; set; }
        /// <summary>
        /// No utilizar las licencias disponibles en la base de datos.
        /// </summary>
        // Obligatorio si la base de datos no está disponible.
        public bool DoNotUseDatabaseLicenses { get; private set; } = true;
        /// <summary>
        /// Instalar Kaspersky Secure Connection, viene por defecto con los productos domésticos de Kaspersky Lab.
        /// </summary>
        public bool KasperskySecureConnection { get; private set; }

        public ConfigurationModel()
        {

        }

        public ConfigurationModel(bool mantainKasConfig, bool useOfflineSetup, bool doNotUseDatabaseLicenses, bool installKasSecureConnection)
        {
            KeepKasperskyConfig = mantainKasConfig;
            OfflineSetup = useOfflineSetup;
            DoNotUseDatabaseLicenses = doNotUseDatabaseLicenses;
            KasperskySecureConnection = installKasSecureConnection;
        }

        /// <summary>
        /// Compara un modelo con otro.
        /// </summary>
        /// <param name="other"><see cref="ConfigurationModel"/> a comparar.</param>
        /// <returns><c>1</c> en caso de ser iguales, <c>0</c> en caso de ser diferentes.</returns>
        public int CompareTo(ConfigurationModel? other)
        {
            if (this is null && other is null)
                return 1;
            else if (other is null)
                return 0;

            if (this.KeepKasperskyConfig.Equals(other.KeepKasperskyConfig) &&
                this.OfflineSetup.Equals(other.OfflineSetup) &&
                this.DoNotUseDatabaseLicenses.Equals(other.DoNotUseDatabaseLicenses) &&
                this.KasperskySecureConnection.Equals(other.KasperskySecureConnection))
                return 1;
            else
                return 0;
        }
    }
}
