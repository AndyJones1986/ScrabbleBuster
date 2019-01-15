using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace ScrabbleBusterData
{
    public class Data : IDisposable
    {
        private const string dbName = "scrableBusterDB";
        public LiteDatabase Database { get; set; }
        public Data(string instanceName = "core")
        {
            Database = new LiteDatabase(string.Format("{0}_{1}", instanceName, dbName));
        }

        public void Dispose()
        {
            Database.Dispose();
            Database = null;
        }
    }
}
