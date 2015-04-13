using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ServiceStack.Mongo
{
    public interface IMongo
    {
        // Methods
        void CreateIndex(string collectionName, params string[] keyNames);
        SafeModeResult Delete(string collectionName, string _id);
        SafeModeResult DeleteAll(string collectionName);
        SafeModeResult DeleteAll(string collectionName, IMongoQuery query);
        bool ExitDocument(string collectionName, IMongoQuery query);
        List<T> GetAll<T>(string collectionName);
        List<T> GetAll<T>(string collectionName, IMongoQuery query);
        List<T> GetAll<T>(string collectionName, IMongoQuery query, params string[] fields);
        List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo);
        List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy);
        List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, params string[] fields);
        List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields);
        List<T> GetAll<T>(string collectionName, int count, IMongoQuery query, IMongoSortBy sortBy, params string[] fields);
        List<T> GetAll<T>(string collectionName, int top, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy);
        List<T> GetAll<T>(string collectionName, int top, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields);
        long GetCount(string collectionName, IMongoQuery query);
        List<string> GetDistinctAll(string collectionName, string distinctkey, QueryDocument query);
        IEnumerable<BsonValue> GetDistinctAllIEnumerable(string collectionName, string distinctkey, QueryDocument query);
        void GetGroup(string collectionName, IMongoQuery query, BsonJavaScript keyFunction, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize);
        void GetGroup(string collectionName, IMongoQuery query, string key, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize);
        void GetGroup(string collectionName, string key, IMongoQuery query, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize);
        MapReduceResult GetMapReduce(string collectionName, BsonJavaScript map, BsonJavaScript reduce);
        MapReduceResult GetMapReduce(string collectionName, BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions options);
        MapReduceResult GetMapReduce(string collectionName, IMongoQuery query, BsonJavaScript map, BsonJavaScript reduce);
        MapReduceResult GetMapReduce(string collectionName, IMongoQuery query, BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions options);
        ObjectId GetObjectId(string collectionName, IMongoQuery query);
        T GetOne<T>(string collectionName, IMongoQuery query);
        T GetOne<T>(string collectionName, string _id);
        T GetOne<T>(string collectionName, IMongoQuery query, IMongoSortBy sortBy, params string[] fields);
        IEnumerable<SafeModeResult> InsertAll<T>(string collectionName, IEnumerable<T> entitys);
        void InsertOne<T>(string collectionName, T entity);
        bool InsertOneSafe<T>(string collectionName, T entity);
        SafeModeResult UpdateAll<T>(string collectionName, IMongoQuery query, IMongoUpdate update);
        SafeModeResult UpdateOne<T>(string collectionName, T entity);

        // Properties
        string DatabaseName { get; set; }
        string ServerConnstr { get; set; }
    }


}
