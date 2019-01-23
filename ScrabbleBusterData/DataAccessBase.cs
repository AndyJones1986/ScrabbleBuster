using System;
using System.Collections.Generic;
using System.Linq;
using ScrabbleBusterData.Tables.Structure;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LiteDB;

namespace ScrabbleBusterData
{
    public abstract class DataAccessBase<T> : IDisposable
    {
        private const string dbName = "scrableBusterDB";
        private List<T> _data { get; set; }
        private LiteCollection<T> _collection { get; set; }
        private LiteDatabase _database { get; set; }

        public string CollectionName { get { return typeof(T).Name; } }

        public DataAccessBase()
        {

        }

        abstract public void RefreshStorage();

        public DataAccessBase(string instanceName = "core")
        {
            this._database = new LiteDatabase(string.Format("{0}_{1}.db", instanceName, dbName));
            this._collection = _database.GetCollection<T>(this.CollectionName);
        }

        public void Dispose()
        {
            this._database.Dispose();
            this._database = null;
        }

        public IEnumerable<T> Select(Func<T, bool> predicate)
        {
            return _collection.FindAll().Where(predicate);
        }

        public IEnumerable<T> Select()
        {
            return _collection.FindAll();
        }

        public void Insert(T record)
        {
            _collection.Insert(record);
        }

        public void Insert(IEnumerable<T> records)
        {
            _collection.Insert(records);
        }

        public void Update(T record)
        {
            _collection.Update(record);
        }

        public int Delete()
        {
            return _collection.Delete(item => true);
        }

        public int Delete(Query query)
        {
            return _collection.Delete(query);

        }
    }
}
