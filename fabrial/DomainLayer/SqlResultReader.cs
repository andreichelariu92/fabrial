using System.Text;

namespace fabrial.DomainLayer
{
    public class SqlResultReader
    {
        private readonly Func<bool> rowsAvailableCallback;
        private readonly Func<int, string> readColumnCallback;
        private readonly int columnCount;

        public static readonly string DefaultDelimiter = ", ";

        public SqlResultReader(Func<bool> rowsAvailableCallback, Func<int, string> readColumnCallback, int columnCount)
        {
            this.rowsAvailableCallback = rowsAvailableCallback;
            this.readColumnCallback = readColumnCallback;
            this.columnCount = columnCount;
        }

        public bool IsValid()
        {
            return columnCount > 0;
        }

        public IEnumerable<string> ReadSqlResult(string delimiter)
        {
            if (!IsValid())
            {
                return Enumerable.Empty<string>();
            }

            var output = new List<string>();
            while (rowsAvailableCallback())
            {
                var columnString = new StringBuilder();
                for (int i = 0; i < columnCount; i++)
                {
                    columnString.Append(readColumnCallback(i));
                    columnString.Append(delimiter);
                }
                output.Add(columnString.ToString());
            }

            return output;
        }
    }
}
