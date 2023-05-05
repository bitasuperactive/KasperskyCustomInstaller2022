namespace KCI_Library.Models
{
    /// <summary>
    /// Modelo de la información local del producto doméstico de Kaspersky Lab.
    /// </summary>
    public class KasperskyModel
    {
        /// <summary>
        /// Almacena si existe algún producto instalado en el equipo.
        /// </summary>
        public bool Installed { get; private set; }
        /// <summary>
        /// Almacena el id correspondiente en la base de datos al nombre
        /// del producto.
        /// </summary>
        public ProductId Id { get => GetValueOrThrow(_id); private set => _id = value; }
        /// <summary>
        /// Nombre completo del producto.
        /// </summary>
        public string? FullName { get => GetValueOrThrow(_fullName); private set => _fullName = value; }
        /// <summary>
        /// Identificador global único del producto.
        /// </summary>
        public string? Guid { get => GetValueOrThrow(_guid); private set => _guid = value; }
        /// <summary>
        /// Directorio de instalación del producto.
        /// </summary>
        public DirectoryInfo? Root { get => GetValueOrThrow(_root); private set => _root = value; }
        /// <summary>
        /// Versión de AVP del producto.
        /// </summary>
        public string? Avp { get => GetValueOrThrow(_avp); private set => _avp = value; }
        /// <summary>
        /// Almacena si la licencia del producto ha expirado.
        /// </summary>
        public bool LicenseExpired { get => GetValueOrThrow(_licenseExpired); private set => _licenseExpired = value; }
        /// <summary>
        /// Almacena el modelo <c>KsdeModel</c>.
        /// </summary>
        public KsdeModel Ksde { get; private set; }

        private ProductId _id;
        private string? _fullName;
        private string? _guid;
        private DirectoryInfo? _root;
        private string? _avp;
        private bool _licenseExpired;

        public KasperskyModel()
        {
            Installed = false;
        }

        public KasperskyModel(ProductId id, string fullName, string guid, DirectoryInfo root, string avp, bool licenseExpired, KsdeModel ksde)
        {
            Installed = true;
            Id = id;
            FullName = fullName;
            Guid = guid;
            Root = root;
            Avp = avp;
            LicenseExpired = licenseExpired;
            Ksde = ksde;
        }

        private T GetValueOrThrow<T>(T value)
        {
            if (!this.Installed)
                throw new InvalidOperationException("No existe ningún producto Kaspersky Lab instalado en el equipo.");

            return value!;
        }
    }
}
