using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class AutoInstallRequirementsModel
    {
        public bool Admin { get; set; }
        public bool DatabaseAccesible { get; set; }
        public bool PasswordProtectionDisabled { get; set; }
        public bool KasClosed { get; set; }

        public AutoInstallRequirementsModel()
        {

        }

        public AutoInstallRequirementsModel(bool admin, bool databaseAccesible, bool passwordProtectionDisabled, bool kasClosed)
        {
            Admin = admin;
            DatabaseAccesible = databaseAccesible;
            PasswordProtectionDisabled = passwordProtectionDisabled;
            KasClosed = kasClosed;
        }
    }
}
