using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    public class InstallationModel
    {
        public static readonly string InstallerFileName = "kas_installer.exe";
        public static readonly string ConfigFileName = "kas_config.cfg";
        public static readonly string LicensesFileName = "kas_licenses.txt";
        public ConfigurationModel Configuration { get; private set; }
        public IProgress<ProgressReportModel> Progress { get; private set; }
        public CancellationToken Cancellation { get; private set; }

        public InstallationModel(ConfigurationModel configuration, IProgress<ProgressReportModel> progress, CancellationToken cancellation)
        {
            Configuration = configuration;
            Progress = progress;
            Cancellation = cancellation;
        }
    }
}
