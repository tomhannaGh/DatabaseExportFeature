using DBExport.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseObjects
{
    public class DatabaseBuilderFactory
    {
        public static IExportSourceBuilder? ServerConnection (string  connectionString, ServerType serverTypes)
        {
            return serverTypes switch
            {
                ServerType.SqlServer => new SqlServerDatabaseBuilder(connectionString),
                ServerType.MySql => new MySqlDatabaseBuilder(connectionString),
                _ => null
            };

        }
    }
}
