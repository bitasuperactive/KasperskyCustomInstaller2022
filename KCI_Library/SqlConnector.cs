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
    public static class SqlConnector
    {
        private static readonly MySqlConnection Connection = new();

        public static bool OpenConnection(string name)
        {
            // TODO - Manejar adecuadamente la autenticación hacia al servidor en App.config.
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings[name].ConnectionString;

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

        public static ConnectionState GetConnectionState() => Connection.State;

        /*public static void CloseConnection()
        {
            if (Connection.State is not ConnectionState.Closed)
            {
                Connection.Dispose();
                Connection.Close();
            }
        }*/

        public static MySqlConnection GetConnection()
        {
            return (Connection.State is ConnectionState.Broken or ConnectionState.Closed) ? null : Connection;
        }
    }
}
