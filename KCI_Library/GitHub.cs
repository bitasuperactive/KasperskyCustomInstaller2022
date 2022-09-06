using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KCI_Library
{
    // TODO - Comprobar si hay una nueva versión disponible de la aplicación.
    public static class GitHub
    {
        /// <summary>
        /// Enlace web a este repositorio en GitHub.
        /// </summary>
        private static readonly string repositoryUrl = "https://github.com/bitasuperactive/KasperskyCustomInstaller2022";

        /// <summary>
        /// Abre el enlace web a este repositorio en GitHub.
        /// </summary>
        public static void BrowseToThisRepository()
        {
            ProcessStartInfo psi = new()
            {
                FileName = repositoryUrl,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
    }
}
