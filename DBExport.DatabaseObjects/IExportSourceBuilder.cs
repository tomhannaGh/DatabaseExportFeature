using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseObjects
{
    public interface IExportSourceBuilder
    {
        ExportSource Build(string selectQuery);
    }
}
