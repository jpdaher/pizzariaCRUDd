using MySql.Data.MySqlClient;
using System;
using System.Data;
using DotNetEnv;
using System.Windows.Forms;
using System.IO;

namespace Products_DRUD
{
    public class Connection
    {
        private static Connection _instance; 
        private static readonly object _lock = new object(); 
        private string connectionString;
        private MySqlConnection _connection; 

        public Connection()
        {
            DotNetEnv.Env.TraversePath().Load("db.env");
            string server = Environment.GetEnvironmentVariable("DB_SERVER");
            string user = Environment.GetEnvironmentVariable("DB_USER");
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            string database = Environment.GetEnvironmentVariable("DB_DATABASE");
            string connectionString = $"server={server};user={user};password={password};database={database};sslmode=none;";
;            _connection = new MySqlConnection(connectionString);
        }

        public static Connection GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Connection();
                    }
                }
            }
            return _instance;
        }

        public MySqlConnection GetConnection()
        {
            if (_connection.State == ConnectionState.Closed || _connection.State == ConnectionState.Broken)
            {
                _connection.Open();
            }
            return _connection;
        }

        public DataTable ExecuteQuery(string query)
        {
            using (MySqlCommand command = new MySqlCommand(query, GetConnection()))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public void ExecuteNonQuery(string query, params MySqlParameter[] parameters)
        {
            using (MySqlCommand command = new MySqlCommand(query, GetConnection()))
            {
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        
    }
}
