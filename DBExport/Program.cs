using CsvHelper;
using DBExport.DatabaseObjects;
using DBExport.DatabaseWirter;
using DBExport.DatabaseWirter.DataWriter.Abstractions;
using DBExport.Options;
using DBExportDatabaseWriter;
using System.IO.Compression;
using System.Net.Sockets;

namespace DBExport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseExportOptions? options = null;
            try
            {
                var optionsBuiler = new DatabaseExportOptionsBuilder(args, [SourceOptionsValidator.Instance]);
                options = optionsBuiler.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (options == null) return;

            var databaseBuilder = DatabaseBuilderFactory.ServerConnection(options.DatabaseOptions.ConnectString, options.DatabaseOptions.ServerType);

            if (databaseBuilder == null) return;

            try
            {
                using var database = databaseBuilder.Build(options.DatabaseOptions.SelectQuery);
                if (options.OptionOfExport.ZipCompressed)
                {
                    ExportToZipFile(options.OptionOfExport, database);
                }
                else
                {
                    using var stream = new FileStream(options.OptionOfExport.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    Export(options.OptionOfExport, database, stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Export(OptionOfExport options, ExportSource source, Stream stream)
        {
            var writerFactory = options.ExportFormats switch
            {
                ExportFormats.Csv => new CsvDataWriterFactory(),
                ExportFormats.TSql => throw new Exception($"{ExportFormats.TSql} is being updated"),
                _=> null
            };
            if (writerFactory == null) return;
            var dataWriter = writerFactory.GetDataWriter();
            dataWriter.WriteData(source, stream);
        }
        private static void ExportToZipFile(OptionOfExport options, ExportSource source)
        {
            var fileName = Path.GetFileName(options.FileName);
            if (Path.GetFileName(options.FileName).EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                fileName = Path.GetFileNameWithoutExtension(fileName);
            using (var stream = new FileStream(options.FileName, FileMode.Create))
            {
                using (var fileZip = new ZipArchive(stream, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry entry = fileZip.CreateEntry(fileName);
                    var streamZip = entry.Open();
                    Export(options, source, streamZip);
                    streamZip.Flush();
                }
            }
        }
    }
}
