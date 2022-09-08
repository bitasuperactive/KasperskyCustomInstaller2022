using KCI_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public static class GlobalConfig
    {
        public static SqlConnector Connection { get; private set; } = new SqlConnector("kci");
        public static Dictionary<DatabaseId, string> AvailableLicenses { get; set; } = Connection.GetAvailableLicenses();
        public static KasperskyModel Kaspersky { get; set; } = Dependencies.CreateKasperskyModel();
        public static AutoInstallRequirementsModel AutoInstallRequirements { get; set; } = Dependencies.CreateAutoInstallRequirementsModel(Connection.Opened);
        public static SourcesModel? Sources { get; set; }
    }
}
