using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.Options
{
    public interface IDatabaseExportOptionsValidator
    {
        void Validate(DatabaseExportOptions options);
    }
}
