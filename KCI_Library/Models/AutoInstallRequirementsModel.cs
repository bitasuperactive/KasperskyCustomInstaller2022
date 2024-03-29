﻿namespace KCI_Library.Models
{
    /// <summary>
    /// Modelo de los requisitos necesarios para llevar a cabo una instalación automática.
    /// </summary>
    public class AutoInstallRequirementsModel
    {
        /// <summary>
        /// La base de datos es accesible.
        /// </summary>
        public bool DatabaseAccesible { get; private set; }
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
        /// Todos los requisitos han sido cumplidos.
        /// </summary>
        public bool AllMet
        {
            get
            {
                return DatabaseAccesible && Admin && PasswordProtectionDisabled && KasClosed;
            }
        }

        public AutoInstallRequirementsModel()
        {
            DatabaseAccesible = false;
            Admin = false;
            PasswordProtectionDisabled = false;
            KasClosed = false;
        }

        public AutoInstallRequirementsModel(bool databaseAccesible, bool admin, bool passwordProtectionDisabled, bool kasClosed)
        {
            DatabaseAccesible = databaseAccesible;
            Admin = admin;
            PasswordProtectionDisabled = passwordProtectionDisabled;
            KasClosed = kasClosed;
        }
    }
}
