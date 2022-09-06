using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class SourcesModel
    {
        public Uri? OnlineSetupUri { get; set; }
        public Uri? OfflineSetupUri { get; set; }
        public string? LastUpdated { get; set; }
        public string[]? Licenses { get; set; }

        public SourcesModel()
        {

        }

        public SourcesModel(Uri onlineSetupUrl, Uri offlineSetupUrl, string lastUpdated, string[]? licenses = null)
        {
            OnlineSetupUri = onlineSetupUrl;
            OfflineSetupUri = offlineSetupUrl;
            Licenses = licenses;
            LastUpdated = lastUpdated;
        }
    }
}
