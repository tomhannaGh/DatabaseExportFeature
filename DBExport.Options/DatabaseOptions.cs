namespace DBExport.Options
{
    public class DatabaseOptions
    {
        public string ConnectString { get; set; } = string.Empty;
        public string SelectQuery {  get; set; } = string.Empty;
        public string TableName {  get; set; } = string.Empty;
        public ServerType ServerType { get; set; } = ServerType.SqlServer;
    }
}