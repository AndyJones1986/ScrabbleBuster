using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace ScrabbleBusterData
{
    public abstract class DataAccessBase<ITable> : IDisposable
    {
        private const string dbName = "scrableBusterDB";
        private List<ITable> _data { get; set; }
        private LiteCollection<ITable> _collection { get; set; }
        private LiteDatabase _database { get; set; }

        public string CollectionName { get { return typeof(ITable).Name; } }

        public DataAccessBase(string instanceName = "core")
        {
            this._database = new LiteDatabase(string.Format("{0}_{1}", instanceName, dbName));
            this._collection = _database.GetCollection<ITable>(this.CollectionName);
        }

        public void Dispose()
        {
            this._database.Dispose();
            this._database = null;
        }

        public IEnumerable<ITable> Select(Func<ITable, bool> predicate)
        {
            return _collection.FindAll().Where(predicate);
        }

        public IEnumerable<ITable> Select()
        {
            return _collection.FindAll();
        }

        public void Insert(ITable record)
        {
            _collection.Insert(record);
        }

        public void Update(ITable record)
        {
            _collection.Update(record);
        }

        public void Delete()
        {
            _collection.Delete(item => true);
        }

        public void Delete(System.Linq.Expressions.Expression<Func<ITable, bool>> predicate)
        {
            _collection.Delete(predicate);
        }
    }
}
