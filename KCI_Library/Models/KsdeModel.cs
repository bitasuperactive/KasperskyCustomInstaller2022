namespace KCI_Library.Models
{
    public class KsdeModel
    {
        /// <summary>
        /// Almacena si existe el producto opcional KSDE instalado.
        /// </summary>
        public bool Installed { get; private set; }
        /// <summary>
        /// Identificador global único del producto opcional KSDE.
        /// </summary>
        public string? Guid { get => GetValueOrThrow(_guid); private set => _guid = value; }

        private string? _guid;

        public KsdeModel()
        { 
            Installed = false;
        }

        public KsdeModel(string guid)
        {
            Installed = true;
            Guid = guid;
        }

        private T GetValueOrThrow<T>(T value)
        {
            if (!this.Installed)
                throw new InvalidOperationException("El producto KSDE no se encuentra instalado en el equipo.");

            return value!;
        }
    }
}
