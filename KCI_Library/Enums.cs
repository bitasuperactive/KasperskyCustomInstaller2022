using static System.Net.WebRequestMethods;

namespace KCI_Library
{
    /// <summary>
    /// Productos reconocidos por la aplicación.
    /// <br/>
    /// <c>KAV</c> : Kaspersky Antivirus
    /// <br/>
    /// <c>KIS</c> : Kaspersky Internet Security
    /// <br/>
    /// <c>KTS</c> : Kaspersky Total Security
    /// </summary>
    // TODO - ¿Añadir valor "no identificado"?
    public enum ProductId
    {
        none,
        KAV,
        KIS,
        KTS
    }
}
