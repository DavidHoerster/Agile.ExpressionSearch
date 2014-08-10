using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace AgileWays.ExpressionSearch2.Repository
{
    public class BaseballContext : IBaseballContext, IDisposable
    {
        private readonly MongoClient _client;
        private readonly MongoServer _svr;
        private readonly MongoDatabase _db;
        private readonly String _collName;

        public BaseballContext(String connectionString, String dbName, String table)
        {
            _client = new MongoClient(connectionString);
            _svr = _client.GetServer();
            _db = _svr.GetDatabase(dbName);
            _collName = table;
        }

        public IQueryable<T> SelectAll<T>()
        {
            var coll = _db.GetCollection<T>(_collName);
            return coll.AsQueryable();
        }

        public void Dispose()
        {
            _svr.Disconnect();
        }
    }
}