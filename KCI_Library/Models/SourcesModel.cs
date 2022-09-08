using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class SourcesModel
    {
        public Uri OnlineSetupUri { get; private set; }
        public Uri? OfflineSetupUri { get; private set; }
        public string? LastUpdated { get; private set; }
        public string[]? Licenses { get; private set; }

        public SourcesModel(Uri onlineSetupUrl)
        {
            OnlineSetupUri = onlineSetupUrl;
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
