using KCI_Library.DataAccess;
using KCI_Library.Extensions;
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

        public void RunInstallation()
        {
            throw new NotImplementedException();
        }




        public async Task DownloadSources()
        {
            SourcesModel sources = await SqlConnector.CreateSourcesModel(Configuration.ProductToInstall);
            Uri setupUrl = Configuration.OfflineSetup ? sources.OfflineSetupUri : sources.OnlineSetupUri;
            string path = Path.Combine(Path.GetTempPath(), "kas_installer.exe");

            try
            {
                using HttpClient client = new();

                // Create a file stream to store the downloaded data.
                // This really can be any type of writeable stream.
                using FileStream file = new(path, FileMode.Create, FileAccess.Write);

                // Use the custom extension method below to download the data.
                // The passed progress-instance will receive the download status updates.
                await client.DownloadAsync(setupUrl.OriginalString, file);
            }
            catch (HttpRequestException)
            {
                // TODO - Implementar control de excepción: Sin conexión a internet.
                throw new NotImplementedException();
            }

            // TODO - Almacenar array de licencias en las configuración de la aplicación.
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
