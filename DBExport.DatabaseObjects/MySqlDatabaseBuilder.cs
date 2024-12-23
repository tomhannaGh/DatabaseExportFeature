namespace DBExport.DatabaseObjects
{
    internal class MySqlDatabaseBuilder:IExportSourceBuilder
    {
        private readonly string connectionString;
        public MySqlDatabaseBuilder(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ExportSource? Build(string selectQuery)
        {
            return null;
        }
    }
}