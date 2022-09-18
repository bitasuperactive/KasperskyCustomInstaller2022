using KCI_Library;

namespace KCI_UI
{
    // TODO - (*) General
    // Evitar utilizar loadingForm como el formulario principal de la aplicaci�n.
    // Revisar funcionamiento asincr�nico.
    // Manejar adecuadamente la autenticaci�n hacia al servidor en App.config.
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new LoadingForm());
        }
    }
}