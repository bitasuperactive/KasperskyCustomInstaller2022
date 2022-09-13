using KCI_Library.DataAccess;
using KCI_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class DefaultInstallation
    {
        private KasperskyModel Kaspersky { get; set; }
        private ConfigurationModel Configuration { get; set; }

        public DefaultInstallation(KasperskyModel kaspersky, ConfigurationModel configuration)
        {
            Kaspersky = kaspersky;
            Configuration = configuration;
        }

        // TODO - Todos los requisitos han de haberse cumplido previamente para la instalación automática.
        public static void RunInstallation()
        {
            throw new NotImplementedException();
        }

        // TODO - Offline setup obligatorio para la instalación automática.
        protected void DownloadSources()
        {
            // Crear SourcesModel de forma asincrónica

            // Descargar instalador del cliente al directorio temporal
            // Utilizar HttpClient
            // Pasar evento del progreso de la descarga a la UI

            // Almacenar array de licencias con persistencia tras el reinicio

            throw new NotImplementedException();
        }

        protected void ExportClientConfiguration()
        {
            throw new NotImplementedException();
        }

        protected virtual void UninstallClient()
        {
            throw new NotImplementedException();
        }

        protected void RegistryCleanUp()
        {
            throw new NotImplementedException();
        }

        // TODO - Avisar de reinicio automática en la instalación automática.
        protected virtual void Restart()
        {
            throw new NotImplementedException();
        }

        protected virtual void InstallClient()
        {
            throw new NotImplementedException();
        }

        protected void UpdateClientDatabase()
        {
            throw new NotImplementedException();
        }

        protected void ActivateClient()
        {
            throw new NotImplementedException();
        }

        protected void ImportClientConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
