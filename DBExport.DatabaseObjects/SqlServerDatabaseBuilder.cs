using Microsoft.Data.SqlClient;

namespace DBExport.DatabaseObjects
{
    internal class SqlServerDatabaseBuilder: IExportSourceBuilder
    {
        private readonly string connectionstring;
        public SqlServerDatabaseBuilder(string connectionString)
        {
            this.connectionstring = connectionString ?? 
                throw new ArgumentNullException(nameof(connectionstring));
        }

        public ExportSource Build(string selectQuery)
        {
            try
            {
                var conn = new SqlConnection(connectionstring);
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;
                var reader = cmd.ExecuteReader();
                var database = new ExportSource()
                {
                    Connection = conn,
                    Reader = reader
                };
                return database;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}