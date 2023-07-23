using fabrial.Config;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace fabrial.InfrastructureLayer
{
    public class SqlConnectionManager
    {
        private readonly IOptionsMonitor<SqlServerConfig> optionsMonitor;
        private SqlConnectionManager(IOptionsMonitor<SqlServerConfig> options)
        {
            optionsMonitor = options;
        }

        public SqlConnection CreateSqlConnection()
        {
            var connection = new SqlConnection(optionsMonitor.CurrentValue.ConnectionString);
            connection.Open();
            return connection;
        }

        public void DestroySqlConnection(SqlConnection connection)
        {
            connection.Close();
        }

    }
}
