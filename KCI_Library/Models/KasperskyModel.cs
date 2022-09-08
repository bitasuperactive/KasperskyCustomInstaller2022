using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library.Models
{
    public class KasperskyModel
    {
        public bool Installed { get; private set; }
        public DatabaseId Id { get; private set; }
        public string? FullName { get; private set; }
        public Guid? Guid { get; private set; }
        public DirectoryInfo? Root { get; private set; }
        public string? Avp { get; private set; }
        public bool LicenseExpired { get; private set; }

        public KasperskyModel()
        {

        }

        public KasperskyModel(bool installed, DatabaseId id, string fullName, Guid guid, DirectoryInfo root, string avp, bool licenseExpired)
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
