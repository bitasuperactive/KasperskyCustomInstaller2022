using KCI_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class AutomaticInstallation : DefaultInstallation
    {
        // TODO - Todos los requisitos han de haberse cumplido previamente.
        public AutomaticInstallation(KasperskyModel kaspersky, ConfigurationModel configuration, Progress<float> progress) : base(kaspersky, configuration, progress)
        {

        }

        protected override void UninstallClient()
        {
            throw new NotImplementedException();
        }

        // TODO - Avisar de reinicio automático.
        protected override void Restart()
        {
            throw new NotImplementedException();
        }

        protected override void InstallClient()
        {
            throw new NotImplementedException();
        }
    }
}
