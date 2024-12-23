using DBExport.DatabaseObjects;
using DBExport.DatabaseWirter.DataWriter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseWirter
{
    public class SqlDataWriter : IDataWriter
    {
        public void WriteData(ExportSource exportSource, Stream stream)
        {
            Console.WriteLine("Feature is comming soon");
        }
    }
}
