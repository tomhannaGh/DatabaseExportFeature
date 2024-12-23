using DBExport.DatabaseObjects;
using DBExport.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseWirter.DataWriter.Abstractions
{
    public interface IDataWriter
    {
        void WriteData(ExportSource exportSource, Stream stream);
    }
}
