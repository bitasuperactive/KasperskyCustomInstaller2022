namespace KCI_Library.Models
{
    /// <summary>
    /// Modelo de los requisitos necesarios para llevar a cabo una instalación automática.
    /// </summary>
    public class AutoInstallRequirementsModel
    {
        /// <summary>
        /// El usuario actual posee privilegios de administrador.
        /// </summary>
        public bool Admin { get; private set; }
        /// <summary>
        /// El producto tiene deshabilitada la protección por contraseña.
        /// </summary>
        public bool PasswordProtectionDisabled { get; private set; }
        /// <summary>
        /// El producto no se encuentra en ejecución.
        /// </summary>
        public bool KasClosed { get; private set; }
        /// <summary>
        /// La base de datos es accesible.
        /// </summary>
        public bool DatabaseAccesible { get; private set; }
        /// <summary>
        /// Los requisitos anteriores han sido cumplidos.
        /// </summary>
        public bool AllMet { get; private set; }

        public AutoInstallRequirementsModel()
        {

        }

        public AutoInstallRequirementsModel(bool admin, bool passwordProtectionDisabled, bool kasClosed, bool databaseAccesible)
        {
            Admin = admin;
            PasswordProtectionDisabled = passwordProtectionDisabled;
            KasClosed = kasClosed;
            DatabaseAccesible = databaseAccesible;
            AllMet = admin && passwordProtectionDisabled && kasClosed && databaseAccesible;
        }
    }
}
