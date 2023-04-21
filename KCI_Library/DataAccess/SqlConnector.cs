using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Text;

namespace KCI_Library.DataAccess
{
    public static class SqlConnector
    {
        /// <summary>
        /// Cadena de conexión a la base de datos.
        /// </summary>
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["kci"].ConnectionString;

        /// <summary>
        /// Comprueba si la base de datos es accesible.
        /// </summary>
        /// <returns><c>Verdadero</c> si es accesible, <c>Falso</c> en su defecto.</returns>
        public static bool DatabaseAccesible()
        {
            try
            {
                using MySqlConnection connection = new(ConnectionString);
                connection.Open();
                
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene de la base de datos, el id de los productos que tengan
        /// disponibles licencias junto con la fecha de su última actualización.
        /// </summary>
        /// <returns>
        /// <see cref="Dictionary{TKey, TValue}"/> donde <c>TKey</c> es el <see cref="DatabaseId"/> del producto 
        /// y <c>TValue</c> es la fecha de la última actualización.
        /// </returns>
        public static async Task<Dictionary<DatabaseId, string>> GetAvailableLicenses()
        {
            try
            {
                using MySqlConnection connection = new(ConnectionString);

                DynamicParameters p = new();
                p.Add("id", dbType: DbType.String, direction: ParameterDirection.Output);
                p.Add("LastUpdated", dbType: DbType.String, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("kci.sources_availableLicenses", p, commandType: CommandType.StoredProcedure);

                // Se deben separar los valores de la Query porque puede devolver varias filas agrupadas.
                string[] ids = p.Get<string>("id").Split(',');
                string[] timeStamps = p.Get<string>("LastUpdated").Split(',');

                Dictionary<DatabaseId, string> keyValuePairs = new();

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
            catch (MySqlException)
            {
                return new Dictionary<DatabaseId, string>();
            }
        }

        /// <summary>
        /// Crea un modelo <c>SourcesModel</c>.
        /// </summary>
        /// <param name="id">El <see cref="DatabaseId"/> del producto a obtener.</param>
        /// <returns><see cref="SourcesModel"/></returns>
        public static async Task<SourcesModel> CreateSourcesModel(DatabaseId id)
        {
            try
            {
                using MySqlConnection connection = new(ConnectionString);

                DynamicParameters p = new();
                p.Add("id", id.ToString());
                p.Add("OnlineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
                p.Add("OfflineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
                p.Add("LastUpdated", dbType: DbType.DateTime2, direction: ParameterDirection.Output);
                p.Add("Licenses", dbType: DbType.String, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("kci.sources_select", p, commandType: CommandType.StoredProcedure);

                Uri onlineSetupUri = new(p.Get<string>("OnlineSetupUrl"));
                Uri offlineSetupUri = new(p.Get<string>("OfflineSetupUrl"));
                // Divide la cadena omitiendo la hora.
                string lastUpdated = Encoding.Default.GetString(p.Get<byte[]>("LastUpdated")).Split(' ').First();

                // Divide la cadena obteniendo un array de licencias, omitiendo la información adicional no deseada.
                string getLicenses = p.Get<string>("Licenses");
                string[] licenses = getLicenses is null ? Array.Empty<string>() : getLicenses.Split(',');
                for (int i = 0; i < licenses.Length; i++)
                {
                    int pFrom = licenses[i].IndexOf("\":\"") + "\":\"".Length;
                    int pTo = licenses[i].LastIndexOf('"');

                    licenses[i] = licenses[i][pFrom..pTo];
                }

                return new SourcesModel(
                    onlineSetupUri,
                    offlineSetupUri,
                    lastUpdated,
                    licenses);
            }
            catch (MySqlException)
            {
                // Establece el enlace predeterminado para la descarga del instalador del producto elegido.
                switch (id)
                {
                    case DatabaseId.kav:
                        return new SourcesModel(
                            new Uri("https://products.s.kaspersky-labs.com/spanish/homeuser/kav2018/for_reg_es/startup.exe"));
                    case DatabaseId.kis:
                        return new SourcesModel(
                            new Uri("https://products.s.kaspersky-labs.com/spanish/homeuser/kis2018/for_reg_es/startup.exe"));
                    case DatabaseId.kts:
                        return new SourcesModel(
                            new Uri("https://products.s.kaspersky-labs.com/spanish/homeuser/kts2018/for_reg_es/startup.exe"));
                }

                throw;
            }
        }
    }
}
