using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    /// <summary>
    /// Modelo de la información local del producto doméstico de Kaspersky Lab.
    /// </summary>
    public class KasperskyModel
    {
        /// <summary>
        /// Almacena si existe algún producto instalado en el equipo.
        /// </summary>
        public bool Installed { get; private set; }
        /// <summary>
        /// Almacena el id correspondiente en la base de datos al nombre
        /// del producto.
        /// </summary>
        public DatabaseId Id { get; private set; }
        /// <summary>
        /// Nombre completo del producto.
        /// </summary>
        public string? FullName { get; private set; }
        /// <summary>
        /// Identificador global único del producto.
        /// </summary>
        public Guid? Guid { get; private set; }
        /// <summary>
        /// Directorio de instalación del producto.
        /// </summary>
        public DirectoryInfo? Root { get; private set; }
        /// <summary>
        /// Versión de AVP del producto.
        /// </summary>
        public string? Avp { get; private set; }
        /// <summary>
        /// Almacena si la licencia del producto ha expirado.
        /// </summary>
        public bool LicenseExpired { get; private set; }

        public KasperskyModel()
        {

        }

        public KasperskyModel(bool installed, DatabaseId id, string fullName, Guid guid, DirectoryInfo root, string avp, bool licenseExpired)
        {
            Installed = installed;
            Id = id;
            FullName = fullName;
            Guid = guid;
            Root = root;
            Avp = avp;
            LicenseExpired = licenseExpired;
        }
    }
}
