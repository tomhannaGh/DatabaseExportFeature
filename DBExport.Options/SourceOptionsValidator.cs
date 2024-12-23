using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.Options
{
    public class SourceOptionsValidator : IDatabaseExportOptionsValidator
    {
        public void Validate(DatabaseExportOptions options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));
            if (!(string.IsNullOrWhiteSpace(options.DatabaseOptions.SelectQuery) ^ string.IsNullOrWhiteSpace(options.DatabaseOptions.TableName)))
            {
                throw new ArgumentException("Only SelectQuery or TableName can be set");
            }
        }
        public static readonly SourceOptionsValidator Instance = new();
    }
}
