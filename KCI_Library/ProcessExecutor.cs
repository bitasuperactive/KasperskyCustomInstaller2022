using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public static class ProcessExecutor
    {
        /// <summary>
        /// Ejecuta el archivo especificado como administrador.
        /// </summary>
        /// <param name="fileName">Ruta completa al archivo deseado a ejecutar.</param>
        /// <returns>Verdadero si la ejecución ha sido satisfactoria, falso en su defecto.</returns>
        public static bool RunAsAdmin(string fileName)
        {
            ProcessStartInfo psi = new()
            {
                FileName = fileName,
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
        /// Ejecuta el archivo especificado y oculta su interfaz gráfica al usuario.
        /// </summary>
        /// <param name="fileName">Ruta completa al archivo deseado a ejecutar.</param>
        /// <param name="args">Argumentos de ejecución.</param>
        /// <param name="waitForExit">Esperar a la salida de la aplicación.</param>
        /// <returns>Verdadero si la ejecución ha sido satisfactoria, falso en su defecto.</returns>
        public static bool RunHidden(string fileName, string args = "", bool waitForExit = false)
        {
            ProcessStartInfo psi = new()
            {
                FileName = fileName,
                Arguments = args,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                //UseShellExecute = true,
            };
            Process process = new()
            {
                StartInfo = psi
            };

            process.Start();

            if (waitForExit)
                process.WaitForExit();

            return process is not null;
        }
    }
}
