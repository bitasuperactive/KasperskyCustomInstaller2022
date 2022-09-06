using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCI_Library
{
    public class SqlConnector
    {
        public bool DatabaseAccesible { get; set; }
        private static MySqlConnection Connection = new();

        public SqlConnector(string name)
        {
            // TODO - Manejar adecuadamente la autenticación hacia al servidor en App.config.
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;
            DatabaseAccesible = OpenConnection();
        }

        public bool OpenConnection()
        {
            // TODO - Manejar evento "Connection.State.Changed".
            //Connection.StateChange += delegate {  };

            try
            {
                Connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                // TODO - Comprobar si la excepción ha sido lanzada porque no hay conexión a internet.
                // Acceso denegado.
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DatabaseIds> GetAvailableLicenses()
        {
            DynamicParameters p = new();
            p.Add("id", dbType: DbType.String, direction: ParameterDirection.Output);

            Connection.Execute("kci.sources_licensenotnull", p, commandType: CommandType.StoredProcedure);

            // Se deben separar los valores de la Query porque puede devolver varias filas agrupadas.
            string[] strArr = p.Get<string>("id").Split(',');

            List<DatabaseIds> keyValuePairs = new();

            foreach (string str in strArr)
                switch (str)
                {
                    case "kav":
                        keyValuePairs.Add(DatabaseIds.kav);
                        break;
                    case "kis":
                        keyValuePairs.Add(DatabaseIds.kis);
                        break;
                    case "kts":
                        keyValuePairs.Add(DatabaseIds.kts);
                        break;
                }

            return keyValuePairs;
        }

        // TODO - Controlar excepciones.
        // TODO - (?) Usar enum.
        // TODO - Obtener tan solo el string de cada licencia, sin más carácteres.
        public SourcesModel CreateSourcesModel(DatabaseIds id)
        {
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
            string[] licenses = p.Get<string>("Licenses").Split("\":\"");

            return new SourcesModel(
                onlineSetupUri, 
                offlineSetupUri,
                lastUpdated,
                licenses);
        }

        public void CloseConnection()
        {
            Connection.Dispose();
            Connection.Close();
        }
    }
}
