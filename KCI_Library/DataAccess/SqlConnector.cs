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
        // TODO - (!!!) Testear token de cancelacion.
        public static async Task<bool> DatabaseAccesible(CancellationToken cancellation)
        {
            try
            {
                using MySqlConnection connection = new(ConnectionString);
                await connection.OpenAsync(cancellation);

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
        /// <see cref="Dictionary{TKey, TValue}"/> donde <c>TKey</c> es el <see cref="ProductId"/> del producto 
        /// y <c>TValue</c> es la fecha de la última actualización.
        /// </returns>
        public static async Task<Dictionary<ProductId, string>> GetAvailableLicenses()
        {
            using MySqlConnection connection = new(ConnectionString);

            DynamicParameters p = new();
            p.Add("id", dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add("LastUpdated", dbType: DbType.String, direction: ParameterDirection.Output);

            try
            {
                await connection.ExecuteAsync("kci.sources_availableLicenses", p, commandType: CommandType.StoredProcedure);
            }
            catch (MySqlException)
            {
                return new();
            }

            // Se deben separar los valores de la Query porque puede devolver varias filas agrupadas.
            string[] ids = p.Get<string>("id").Split(',');
            string[] timeStamps = p.Get<string>("LastUpdated").Split(',');

            Dictionary<ProductId, string> keyValuePairs = new();
            for (int i = 0; i < ids.Length; i++)
            {
                ProductId id = (ProductId)Enum.Parse(typeof(ProductId), ids[i]);
                keyValuePairs.Add(id, timeStamps[i]);
            }

            return keyValuePairs;
        }

        /// <summary>
        /// Crea un modelo <c>SourcesModel</c>.
        /// </summary>
        /// <param name="id">El <see cref="ProductId"/> del producto a obtener.</param>
        /// <returns><see cref="SourcesModel"/></returns>
        public static async Task<SourcesModel> CreateSourcesModel(ProductId id, bool doNotUseDataBaseLicenses)
        {
            using MySqlConnection connection = new(ConnectionString);

            DynamicParameters p = new();
            p.Add("id", id.ToString());
            p.Add("OnlineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add("OfflineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add("LastUpdated", dbType: DbType.DateTime2, direction: ParameterDirection.Output);
            if (!doNotUseDataBaseLicenses)
                p.Add("Licenses", dbType: DbType.String, direction: ParameterDirection.Output);

            try
            {
                await connection.ExecuteAsync("kci.sources_select", p, commandType: CommandType.StoredProcedure);
            }
            catch (MySqlException)
            {
                // Establece el enlace predeterminado para la descarga online del producto elegido.
                switch (id)
                {
                    case ProductId.KAV:
                        return new SourcesModel(
                            new Uri("https://products.s.kaspersky-labs.com/spanish/homeuser/kav2018/for_reg_es/startup.exe"));
                    case ProductId.KIS:
                        return new SourcesModel(
                            new Uri("https://products.s.kaspersky-labs.com/spanish/homeuser/kis2018/for_reg_es/startup.exe"));
                    case ProductId.KTS:
                        return new SourcesModel(
                            new Uri("https://pdc5.pa2.pdc.kaspersky.com/DownloadManagers/2a/26/2a26001a-dac7-4096-8f99-275c4d5abab5/kts21.3.10.391abes_25655.exe")); // https://products.s.kaspersky-labs.com/spanish/homeuser/kts2018/for_reg_es/startup.exe
                }

                throw;
            }

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
    }
}
