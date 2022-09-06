using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class KasperskyModel
    {
        public bool Installed { get; set; }
        public DatabaseIds Id { get; set; }
        public string? FullName { get; set; }
        public Guid? Guid { get; set; }
        public DirectoryInfo? Root { get; set; }
        public string? Avp { get; set; }
        public bool LicenseExpired { get; set; }

        public KasperskyModel()
        {

        }

        public KasperskyModel(bool installed, DatabaseIds id, string fullName, Guid guid, DirectoryInfo root, string avp, bool licenseExpired)
        {
            Installed = installed;
            Id = id;
            FullName = fullName;
            Guid = guid;
            Root = root;
            Avp = avp;
            LicenseExpired = licenseExpired;
        }
    }
}
