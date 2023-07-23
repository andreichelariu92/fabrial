using fabrial.DomainLayer;
using fabrial.InfrastructureLayer;
using System.Data.SqlClient;

namespace fabrial.ApplicationLayer
{

    public class CreateSqlCommandUsecase
    {
        private readonly SqlConnectionManager sqlConnectionManager;

        public CreateSqlCommandUsecase(SqlConnectionManager sqlConnectionManager)
        {
            this.sqlConnectionManager = sqlConnectionManager;
        }

        public class Result
        {
            public IEnumerable<string>? Rows { get; set; }
            public string? ErrorDetail { get; set; }
        }

        public Result Execute(string sqlCommand)
        {
            var connection = sqlConnectionManager.CreateSqlConnection();
            var command = new SqlCommand(sqlCommand, connection);
            var output = new Result();

            try
            {
                var reader = command.ExecuteReader();

                Func<bool> rowsAvailableCallback = () =>
                {
                    return reader.Read();
                };
                Func<int, string> readColumnCallback = (int columnIdx) =>
                {
                    return reader.GetString(columnIdx);
                };
                var resultReader = new SqlResultReader(rowsAvailableCallback, readColumnCallback, reader.FieldCount);
                output.Rows = resultReader.ReadSqlResult(SqlResultReader.DefaultDelimiter);
                output.ErrorDetail = null;
            }
            catch (Exception ex)
            {
                output.ErrorDetail = ex.Message;
                output.Rows = null;
            }
            finally
            {
                sqlConnectionManager.DestroySqlConnection(connection);
            }

            return output;
        }
    }
}
