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
        public AutomaticInstallation(KasperskyModel kaspersky, ConfigurationModel configuration) : base(kaspersky, configuration)
        {

        }

        protected override void UninstallClient()
        {
            throw new NotImplementedException();
        }

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
