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

namespace KCI_Library
{
    public class SqlConnector
    {
        public bool Opened { get; set; }

        private static MySqlConnection Connection = new();

        // TODO - Manejar adecuadamente la autenticación hacia al servidor en App.config.
        public SqlConnector(string name)
        {
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            Opened = OpenConnection();
        }

        // TODO - Manejar escepción cuando la conexión no esté abierta.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<DatabaseId, string> GetAvailableLicenses()
        {
            if (!Opened)
                return new() { { DatabaseId.none, string.Empty } };

            DynamicParameters p = new();
            p.Add("id", dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add("LastUpdated", dbType: DbType.String, direction: ParameterDirection.Output);

            Connection.Execute("kci.sources_availableLicenses", p, commandType: CommandType.StoredProcedure);

            // Se deben separar los valores de la Query porque puede devolver varias filas agrupadas.
            string[] ids = p.Get<string>("id").Split(',');
            string[] timeStamps = p.Get<string>("LastUpdated").Split(',');

            Dictionary<DatabaseId, string> keyValuePairs = new Dictionary<DatabaseId, string>();

            for (int i = 0; i < ids.Length; i++)
                switch (ids[i])
                {
                    // TODO - Parsear DatabaseId a string.
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

        // TODO - Controlar excepciones.
        // TODO - (?) Usar enum.
        // TODO - Obtener tan solo el string de cada licencia, sin más carácteres.
        public SourcesModel? CreateSourcesModel(DatabaseId id)
        {
            if (!Opened)
                return null;

            DynamicParameters p = new();
            p.Add("id", id.ToString());
            p.Add("OnlineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add("OfflineSetupUrl", dbType: DbType.String, direction: ParameterDirection.Output);
            p.Add("LastUpdated", dbType: DbType.DateTime2, direction: ParameterDirection.Output);
            p.Add("Licenses", dbType: DbType.String, direction: ParameterDirection.Output);

            Connection.Execute("kci.sources_select", p, commandType: CommandType.StoredProcedure);

            Uri onlineSetupUri = new Uri(p.Get<string>("OnlineSetupUrl"));
            Uri offlineSetupUri = new Uri(p.Get<string>("OfflineSetupUrl"));
            // Separa la cadena de caracteres omitiendo la hora.
            string lastUpdated = Encoding.Default.GetString(p.Get<byte[]>("LastUpdated")).Split(' ').First();

            // Separa la cadena de caracteres obteniendo un array de licencias, omitiendo la información adicional no deseada.
            string[] licenses = p.Get<string>("Licenses").Split(',');
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

        // TODO - Manejar evento "Connection.State.Changed".
        private bool OpenConnection()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                // Acceso denegado.
                return false;
            }
        }

        private void CloseConnection()
        {
            Connection.Dispose();
            Connection.Close();
        }
    }
}
