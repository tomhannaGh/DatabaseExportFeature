using CsvHelper;
using DBExport.DatabaseObjects;
using DBExport.DatabaseWirter.DataWriter.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExportDatabaseWriter
{
    public class CsvDataWriter : IDataWriter
    {
        public void WriteData(ExportSource database, Stream stream)
        {
            using var sw = new StreamWriter(stream, leaveOpen: true);
            using var csv = new CsvWriter(sw, CultureInfo.InvariantCulture);

            var columns = database.Reader.GetColumnSchema();

            while (database.Reader.Read())
            {
                for (int i = 0; i < columns.Count; i++)
                    csv.WriteField(database.Reader[i].ToString(), true);
                csv.NextRecord();
            }

            csv.Flush();
        }
    }
}
