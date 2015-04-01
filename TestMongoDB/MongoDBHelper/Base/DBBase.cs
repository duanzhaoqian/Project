using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace MongoDBHelper.Base
{
    internal abstract class DBBase<T>
    {
        protected MongoClient mongoClient;
        protected MongoDatabase mongoDatabase;
        protected MongoCollection<T> mongoCollection;

        public DBBase(string connectionString, string dataBaseName, string collectionName)
        {
            mongoClient = new MongoClient(connectionString);
            mongoDatabase = mongoClient.GetServer().GetDatabase(dataBaseName);
            mongoCollection = mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}
