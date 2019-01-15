using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace ScrabbleBusterData
{
    public class Common : IDisposable
    {
        public static LiteDatabase Database { get; set; }
        public Common(string databaseName)
        {
            Database = new LiteDatabase(databaseName);
        }

        public void Dispose()
        {
            Database.Dispose();
            Database = null;
        }
    }
}
