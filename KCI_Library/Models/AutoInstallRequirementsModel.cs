using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    /// <summary>
    /// Modelo de los requisitos necesarios para llevar a cabo una instalación silenciosa.
    /// </summary>
    public class AutoInstallRequirementsModel
    {
        /// <summary>
        /// Almacena si ha sido posible generar un modelo <c>SourcesModel</c> completo.
        /// </summary>
        public bool DatabaseAccesible { get; private set; }
        /// <summary>
        /// Almacena si el usuario posee privilegios de administrador.
        /// </summary>
        public bool Admin { get; private set; }
        /// <summary>
        /// Almacena si el producto de Kaspersky, instalado en el equipo, 
        /// tiene habilitada la protección por contraseña.
        /// </summary>
        public bool PasswordProtectionDisabled { get; private set; }
        /// <summary>
        /// Almacena si el producto de Kaspersky, instalado en el equipo, 
        /// no se encuentra en ejecución.
        /// </summary>
        public bool KasClosed { get; private set; }
        /// <summary>
        /// Almacena si los requisitos anteriores han sido cumplidos.
        /// </summary>
        public bool AllMet { get; private set; }

        public AutoInstallRequirementsModel()
        {

        }

        public AutoInstallRequirementsModel(bool databaseAccesible, bool admin, bool passwordProtectionDisabled, bool kasClosed)
        {
            DatabaseAccesible = databaseAccesible;
            Admin = admin;
            PasswordProtectionDisabled = passwordProtectionDisabled;
            KasClosed = kasClosed;

            if (databaseAccesible && admin && passwordProtectionDisabled && kasClosed)
                AllMet = true;
        }
    }
}
