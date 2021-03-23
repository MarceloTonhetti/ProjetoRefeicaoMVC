using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class ConnectionDB : IDisposable
    {
        private SqlConnection _conn;
        private static ConnectionDB _instance;

		public ConnectionDB()
		{
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexaoSQLServer"].ConnectionString);
            _conn.Open();
		}

        public bool ExecQuery(string query)
        {
            var cmd = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = _conn
            };

            if (cmd.ExecuteNonQuery() >= 1)
                return true;

            return false;
        }

        public SqlDataReader ExecQueryReturn(string query)
        {
            var cmd = new SqlCommand(query, _conn);
            return cmd.ExecuteReader();
        }

		public void Dispose()
		{
            if (_conn.State == ConnectionState.Open)
                _conn.Close();

        }
	}
}
