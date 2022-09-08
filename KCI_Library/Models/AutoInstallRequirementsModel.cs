using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    public class AutoInstallRequirementsModel
    {
        public bool DatabaseAccesible { get; private set; }
        public bool Admin { get; private set; }
        public bool PasswordProtectionDisabled { get; private set; }
        public bool KasClosed { get; private set; }
        public bool AllMet { get; private set; }

        public AutoInstallRequirementsModel()
        {

        }

        public AutoInstallRequirementsModel(bool databaseAccesible, bool admin, bool passwordProtectionDisabled, bool kasClosed)
        {
            DatabaseAccesible = databaseAccesible;
            Admin = admin;
            PasswordProtectionDisabled = passwordProtectionDisabled;
            KasClosed = kasClosed;

            if (databaseAccesible && admin && passwordProtectionDisabled && kasClosed)
                AllMet = true;
        }
    }
}
