using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBExport.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DBExport.Options.Tests
{
    [TestClass()]
    public class DatabaseExportOptionsBuilderTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            string[] args = ["Server=.;Database=Test", "SELECT * FROM table", "-f:filename", "-server:SqlServer", "-format:csv", "-compress", "-adt"];
            var db = new DatabaseExportOptionsBuilder(args, [])
            {
                CurrentDateTime = () => new DateTime(2024, 05, 01, 23, 59, 59)
            };
            var options = db.Build();
            Assert.IsNotNull(options);
            Assert.AreEqual("Server=.;Database=Test", options.DatabaseOptions.ConnectString);
            Assert.AreEqual("SELECT * FROM table", options.DatabaseOptions.SelectQuery);
            Assert.AreEqual(ServerType.SqlServer, options.DatabaseOptions.ServerType);
            Assert.AreEqual(ExportFormats.Csv, options.OptionOfExport.ExportFormats);
            Assert.IsTrue(options.OptionOfExport.ZipCompressed);
            Assert.IsTrue(options.OptionOfExport.AppendExportTimeToFileName);
            Assert.AreEqual("filename-20240105-235959.csv.zip", options.OptionOfExport.FileName);
        }
    }
}