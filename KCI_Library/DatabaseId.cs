using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    /// <summary>
    /// Enumerador de los ids disponibles en la base de datos.
    /// <br/>
    /// <c>kav</c> : Kaspersky Antivirus
    /// <br/>
    /// <c>kis</c> : Kaspersky Internet Security
    /// <br/>
    /// <c>kts</c> : Kaspersky Total Security
    /// </summary>
    public enum DatabaseId
    {
        none,
        kav,
        kis,
        kts
    }
}
