using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExport.DatabaseObjects
{
    public class ExportSource : IDisposable
    {
        private bool disposedValue;
        public required DbConnection Connection { get; set; }
        public required DbDataReader Reader { get; set; }
        public virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!Reader.IsClosed)
                        Reader.Close();
                    if (Connection.State == System.Data.ConnectionState.Open)
                        Connection.Close();
                }
                disposing = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
