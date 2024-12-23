using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseWirter.DataWriter.Abstractions
{
    public interface IDataWriterFactory
    {
        public IDataWriter GetDataWriter();
    }
}
