using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace Mongo
{
    internal interface IMongoDbPool
    {
        // Methods
        void Connect();
        void DisposeConnect();
        MongoCollection GetCollection(MongoCollectionSettings set);
        MongoCollection<T> GetCollection<T>(string collname);
        MongoCollection<T> GetCollection<T>(string collname, WriteConcern WriteConcern);
        MongoCollection GetCollection(Type type, string collname);
        MongoCollection GetCollection(Type type, string collname, WriteConcern WriteConcern);
        MongoDatabase GetCurDataBase();
        MongoDatabase GetDataBase(string dbname);
        GetLastErrorResult GetLastEerror();

        // Properties
        string CollectionName { get; set; }
        string DbName { get; set; }
        string Host { get; set; }
        int Port { get; set; }
    }

 

 

}
