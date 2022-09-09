using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KCI_Library
{
    // TODO - Manejar adecuadamente la autenticación hacia al servidor en App.config.
    public class SqlConnector
    {
        //public static bool DatabaseIsAccesible { get; private set; }

        /// <summary>
        /// Cadena de conexión a la base de datos.
        /// </summary>
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["kci"].ConnectionString;

        // TODO - (!!!) Este método no tiene sentido si ya obtenemos los modelos sources.
        /// <summary>
        /// Obtiene de la base de datos, el id de los productos que tengan
        /// disponibles licencias junto con la fecha de su última actualización.
        /// </summary>
        /// <returns>
        /// Diccionario donde <c>TKey</c> es el <c>DatabaseId</c> del producto 
        /// y <c>TValue</c> es la fecha de la última actualización.
        /// </returns>
        public static Dictionary<DatabaseId, string> GetAvailableLicenses()
        {
            try
            {
                using (MySqlConnection connection = new(ConnectionString))
                {
                    DynamicParameters p = new();
                    p.Add("id", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("LastUpdated", dbType: DbType.String, direction: ParameterDirection.Output);

                    connection.Execute("kci.sources_availableLicenses", p, commandType: CommandType.StoredProcedure);

                    // Se deben separar los valores de la Query porque puede devolver varias filas agrupadas.
                    string[] ids = p.Get<string>("id").Split(',');
                    string[] timeStamps = p.Get<string>("LastUpdated").Split(',');

                    Dictionary<DatabaseId, string> keyValuePairs = new Dictionary<DatabaseId, string>();

                    for (int i = 0; i < ids.Length; i++)
                        switch (ids[i])
                        {
                            case "kav":
                                keyValuePairs.Add(DatabaseId.kav, timeStamps[i]);
                                break;
                            case "kis":
                                keyValuePairs.Add(DatabaseId.kis, timeStamps[i]);
                                break;
                            case "kts":
                                keyValuePairs.Add(DatabaseId.kts, timeStamps[i]);
                                break;
                        }

                    return keyValuePairs;
                }
            }
            catch (MySqlException)
            {
                return new Dictionary<DatabaseId, string>();
            }
        }

        /// <summary>
        /// Crea un modelo <c>SourcesModel</c>.
        /// </summary>
        /// <param name="id">El <c>DatabaseId</c> del producto a obtener.</param>
        /// <returns></returns>
        public static SourcesModel CreateSourcesModel(DatabaseId id)
        {
            try
            {
                using (MySqlConnection connection = new(ConnectionString))
                {
                    DynamicParameters p = new();
                    p.Add("id", id.ToString());
                    p.Add("OnlineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("OfflineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
                    p.Add("LastUpdated", dbType: DbType.DateTime2, direction: ParameterDirection.Output);
                    p.Add("Licenses", dbType: DbType.String, direction: ParameterDirection.Output);

                    connection.Execute("kci.sources_select", p, commandType: CommandType.StoredProcedure);

                    Uri onlineSetupUri = new Uri(p.Get<string>("OnlineSetupUrl"));
                    Uri offlineSetupUri = new Uri(p.Get<string>("OfflineSetupUrl"));
                    // Separa la cadena de caracteres omitiendo la hora.
                    string lastUpdated = Encoding.Default.GetString(p.Get<byte[]>("LastUpdated")).Split(' ').First();

                    // Separa la cadena de caracteres obteniendo un array de licencias, omitiendo la información adicional no deseada.
                    string getLicenses = p.Get<string>("Licenses");
                    string[] licenses = getLicenses is null ? Array.Empty<string>() : getLicenses.Split(',');
                    for (int i = 0; i < licenses.Length; i++)
                    {
                        int pFrom = licenses[i].IndexOf("\":\"") + "\":\"".Length;
                        int pTo = licenses[i].LastIndexOf('"');

                        licenses[i] = licenses[i].Substring(pFrom, pTo - pFrom);
                    }

                    return new SourcesModel(
                        onlineSetupUri,
                        offlineSetupUri,
                        lastUpdated,
                        licenses);
                }
            }
            catch (MySqlException)
            {
                // TODO - Manejar enlaces predeterminados para OnlineSetupUri.
                switch (id)
                {
                    case DatabaseId.kav:
                        return new SourcesModel(
                            new Uri("https://pdc2.fra5.pdc.kaspersky.com/DownloadManagers/a6/c9/a6c95eaf-96c1-4c0f-b40b-2e502a369ec9/kav21.3.10.391abes_25639.exe"));
                    case DatabaseId.kis:
                        return new SourcesModel(
                            new Uri("https://pdc5.pa2.pdc.kaspersky.com/DownloadManagers/d7/b3/d7b36992-6015-4c46-bc86-48356085af73/kis21.3.10.391abes_25641.exe"));
                    case DatabaseId.kts:
                        return new SourcesModel(
                            new Uri("https://pdc6.pa2.pdc.kaspersky.com/DownloadManagers/20/57/2057ff46-4ca1-43ab-96ac-d22c89bfc6cf/kts21.3.10.391abes_25644.exe"));
                }

                throw;
            }
        }
    }
}
