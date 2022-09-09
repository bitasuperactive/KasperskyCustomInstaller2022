using KCI_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    // (?) Clase objeto e inicializarla en la forma.
    public static class GlobalConfig
    {
        public static SourcesModel KavSources { get; private set; } = SqlConnector.CreateSourcesModel(DatabaseId.kav);
        public static SourcesModel KisSources { get; private set; } = SqlConnector.CreateSourcesModel(DatabaseId.kis);
        public static SourcesModel KtsSources { get; private set; } = SqlConnector.CreateSourcesModel(DatabaseId.kts);
        public static Dictionary<DatabaseId, string> AvailableLicenses { get; private set; } = SqlConnector.GetAvailableLicenses();
        public static KasperskyModel Kaspersky { get; private set; } = Dependencies.CreateKasperskyModel();
        public static AutoInstallRequirementsModel AutoInstallRequirements { get; private set; } = Dependencies.CreateAutoInstallRequirementsModel();

        public static void RefreshSources()
        {
            KavSources = SqlConnector.CreateSourcesModel(DatabaseId.kav);
            KisSources = SqlConnector.CreateSourcesModel(DatabaseId.kis);
            KtsSources = SqlConnector.CreateSourcesModel(DatabaseId.kts);
            AvailableLicenses = SqlConnector.GetAvailableLicenses();
        }

        public static void RefreshKaspersky()
        {
            Kaspersky = Dependencies.CreateKasperskyModel();
        }

        public static void RefreshAutoInstallRequirements()
        {
            AutoInstallRequirements = Dependencies.CreateAutoInstallRequirementsModel();
        }
    }
}
