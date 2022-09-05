using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KCI_Library
{
    public static class GitHubAccess
    {
        // TODO - Actualizar enlace al repositorio en GitHub.
        /// <summary>
        /// Enlace web a este repositorio en GitHub.
        /// </summary>
        private static string GitHubUrl { get; } = "https://github.com/bitasuperactive/KasperskyCustomInstaller2022";

        /// <summary>
        /// Abre el enlace web a este repositorio en GitHub.
        /// </summary>
        public static void BrowseToThisGitHubRepository()
        {
            ProcessStartInfo psi = new()
            {
                FileName = GitHubUrl,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
    }
}
