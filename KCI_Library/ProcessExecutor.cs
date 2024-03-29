﻿using System.Diagnostics;
using System.Net;

namespace KCI_Library
{
    public static class ProcessExecutor
    {
        /// <summary>
        /// Ejecuta el archivo especificado como administrador.
        /// </summary>
        /// <param name="filePath">Ruta completa al archivo deseado a ejecutar.</param>
        /// <param name="args">Argumentos de ejecución.</param>
        /// <returns><c>Verdadero</c> si la ejecución ha sido exitosa, <c>Falso</c> en su defecto.</returns>
        public static bool AsAdmin(string filePath, string args = "")
        {
            ProcessStartInfo psi = new()
            {
                FileName = filePath,
                Arguments = args,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                return Process.Start(psi) is not null;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // Acceso denegado.
                return false;
            }
        }

        /// <summary>
        /// Ejecuta el archivo especificado y oculta su interfaz gráfica de usuario.
        /// </summary>
        /// <param name="filePath">Ruta completa al archivo deseado a ejecutar.</param>
        /// <param name="args">Argumentos de ejecución.</param>
        /// <param name="waitForExit">Esperar a la salida de la aplicación.</param>
        /// <returns><c>Verdadero</c> si la ejecución ha sido exitosa, <c>Falso</c> en su defecto.</returns>
        public static async Task<string> WindowHidden(string filePath, string args, CancellationToken cancellation)
        {
            ProcessStartInfo psi = new()
            {
                FileName = filePath,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                //WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
            };

            Process process = new();
            process.StartInfo = psi;
            process.Start();

            await process.WaitForExitAsync(cancellation);

            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// Abre la url especificada en el navegador predeterminado del sistema.
        /// </summary>
        /// <param name="url">Dirección url a abrir.</param>
        /// <returns><c>Verdadero</c> si la ejecución ha sido exitosa, <c>Falso</c> en su defecto.</returns>
        public static bool BrowseToUrl(string url)
        {
            ProcessStartInfo info = new()
            {
                FileName = url,
                UseShellExecute = true
            };

            return Process.Start(info) is not null;
        }
    }
}
