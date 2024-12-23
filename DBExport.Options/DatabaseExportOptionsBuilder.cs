using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBExport.Options
{
    /*
    * Usage: dbexport <connectionstring> <select query> [-f:filename] [-server:<SqlServer>] [-format:<csv|tsql>] [-compress] [-adt]
    */
    public class DatabaseExportOptionsBuilder
    {

        private readonly string[] args;
        private readonly IEnumerable<IDatabaseExportOptionsValidator> validators;
        public Func<DateTime> CurrentDateTime {  get; set; } = ()=> DateTime.Now;
        public string FileDataTimeFormat { get; set; } = "yyyyddMM-HHmmss";
        
        public DatabaseExportOptionsBuilder(string[] args, IEnumerable<IDatabaseExportOptionsValidator> validators)
        {
            this.args = args;
            this.validators = validators ?? [];
        }
        public DatabaseExportOptions Build()
        {
            var option = Parse(args);
            return option;
        }

        private DatabaseExportOptions Parse(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                throw new ArgumentException("Missing required parameters");
            }

            var options = new DatabaseExportOptions();
            int i = 0;
            options.DatabaseOptions.ConnectString = args[i++];
            options.DatabaseOptions.SelectQuery = args[i++];

            for (; i < args.Length; i++)
            {
                if (args[i].StartsWith("-f:"))
                    options.OptionOfExport.FileName = args[i][3..];
                else if (args[i].StartsWith("-server:"))
                {
                    if ("SqlServer".Equals(args[i][8..], StringComparison.OrdinalIgnoreCase))
                        options.DatabaseOptions.ServerType = ServerType.SqlServer;
                    else if ("MySql".Equals(args[i][8..], StringComparison.OrdinalIgnoreCase))
                        options.DatabaseOptions.ServerType = ServerType.MySql;
                }
                else if (args[i].StartsWith("-format:"))
                {
                    if ("csv".Equals(args[i][8..], StringComparison.OrdinalIgnoreCase))
                        options.OptionOfExport.ExportFormats = ExportFormats.Csv;
                    else if ("tsql".Equals(args[i][8..], StringComparison.OrdinalIgnoreCase))
                        options.OptionOfExport.ExportFormats = ExportFormats.TSql;
                }
                else if (args[i].StartsWith("-compress"))
                    options.OptionOfExport.ZipCompressed = true;
                else if (args[i].StartsWith("-adt"))
                    options.OptionOfExport.AppendExportTimeToFileName = true;
                else
                    throw new ArgumentException($"Unknown option: {args[i]}");
            }

            if (options.OptionOfExport.AppendExportTimeToFileName)
                options.OptionOfExport.FileName += $"-{CurrentDateTime().ToString(FileDataTimeFormat)}";
            if (options.OptionOfExport.ExportFormats == ExportFormats.TSql)
                options.OptionOfExport.FileName += ".sql";
            else if (options.OptionOfExport.ExportFormats == ExportFormats.Csv)
                options.OptionOfExport.FileName += ".csv";
            if (options.OptionOfExport.ZipCompressed)
                options.OptionOfExport.FileName += ".zip"; 

            return Validate(options);

        }

        protected DatabaseExportOptions Validate(DatabaseExportOptions options)
        {
            foreach (var arg in validators)
            {
                arg.Validate(options);
            }
            return options;
        }
    }
}
