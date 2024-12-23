using DBExport.DatabaseWirter.DataWriter.Abstractions;
using DBExportDatabaseWriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseWirter
{
    public class CsvDataWriterFactory : IDataWriterFactory
    {
        public CsvDataWriterFactory() { }
        public IDataWriter GetDataWriter()
        {
            return new CsvDataWriter();
        }
    }
}
