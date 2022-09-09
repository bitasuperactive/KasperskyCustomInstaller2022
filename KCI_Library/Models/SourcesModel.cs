using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    /// <summary>
    /// Modelo de la información proporcionada por la base de datos.
    /// </summary>
    public class SourcesModel
    {
        /// <summary>
        /// Enlace al instalador parcial (online) del producto seleccionado.
        /// </summary>
        public Uri OnlineSetupUri { get; private set; }
        /// <summary>
        /// Enlace al instalador completo (offline) del producto seleccionado.
        /// </summary>
        public Uri? OfflineSetupUri { get; private set; }
        /// <summary>
        /// Fecha de la última actualización de la base de datos.
        /// </summary>
        public string? LastUpdated { get; private set; }
        /// <summary>
        /// Licencias, si las hubiera, para activar el producto seleccionado.
        /// </summary>
        public string[]? Licenses { get; private set; }

        public SourcesModel(Uri onlineSetupUri)
        {
            OnlineSetupUri = onlineSetupUri;
        }

        public SourcesModel(Uri onlineSetupUrl, Uri offlineSetupUrl, string lastUpdated, string[]? licenses = null)
        {
            OnlineSetupUri = onlineSetupUrl;
            OfflineSetupUri = offlineSetupUrl;
            Licenses = licenses;
            LastUpdated = lastUpdated;
        }
    }
}
