using System.Runtime.Serialization;

namespace DBExport.Options
{
    public class DatabaseExportOptions
    {
        public DatabaseOptions DatabaseOptions { get; set; } = new();
        public OptionOfExport OptionOfExport { get; set; } = new();
    }
}