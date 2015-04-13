using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace Mongo
{
    public sealed class MongoDbOpe
    {
        // Fields
        private static MongoDatabase database1 = server1.GetDatabase(DatabaseName);
        private static MongoServer server1 = new MongoClient(ServerConnstr).GetServer();// new MongoClient(ServerConnstr).GetServer();

        // Methods
        public static void CreateIndex(string collectionName, params string[] keyNames)
        {
            CreateIndex(ServerConnstr, DatabaseName, collectionName, keyNames);
        }

        public static void CreateIndex(string connectionString, string databaseName, string collectionName, params string[] keyNames)
        {
            if (keyNames != null)
            {
                MongoServer server = new MongoClient(connectionString).GetServer();
                MongoDatabase database = server.GetDatabase(databaseName);
                using (server.RequestStart(database))
                {
                    MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                    if (!collection.IndexExists(keyNames))
                    {
                        collection.EnsureIndex(keyNames);
                    }
                }
            }
        }

        public static WriteConcernResult Delete(string collectionName, string _id)
        {
            return Delete(ServerConnstr, DatabaseName, collectionName, _id);
        }

        public static WriteConcernResult Delete(string connectionString, string databaseName, string collectionName, string _id)
        {
            ObjectId id;
            if (!ObjectId.TryParse(_id, out id))
            {
                return null;
            }
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                return database.GetCollection<BsonDocument>(collectionName).Remove(Query.EQ("_id", id));
            }
        }

        public static WriteConcernResult DeleteAll(string collectionName)
        {
            return DeleteAll(ServerConnstr, DatabaseName, collectionName, null);
        }

        public static WriteConcernResult DeleteAll(string collectionName, IMongoQuery query)
        {
            return DeleteAll(ServerConnstr, DatabaseName, collectionName, query);
        }

        public static WriteConcernResult DeleteAll(string connectionString, string databaseName, string collectionName, IMongoQuery query)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    return collection.RemoveAll();
                }
                return collection.Remove(query);
            }
        }

        public static List<T> GetAll<T>(string collectionName)
        {
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName);
        }

        public static List<T> GetAll<T>(string collectionName, int count)
        {
            return GetAll<T>(collectionName, count, null, null, new string[0]);
        }

        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo)
        {
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName, query, pagerInfo, null, new string[0]);
        }

        public static List<T> GetAll<T>(string collectionName, int count, IMongoQuery query)
        {
            return GetAll<T>(collectionName, count, query, null, new string[0]);
        }

        public static List<T> GetAll<T>(string collectionName, int count, IMongoSortBy sortBy)
        {
            return GetAll<T>(collectionName, count, null, sortBy, new string[0]);
        }

        public static List<T> GetAll<T>(string connectionString, string databaseName, string collectionName)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            List<T> list = new List<T>();
            using (server.RequestStart(database))
            {
                foreach (T local in database.GetCollection<BsonDocument>(collectionName).FindAllAs<T>())
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy)
        {
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName, query, pagerInfo, sortBy, new string[0]);
        }

        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, params string[] fields)
        {
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName, query, pagerInfo, null, fields);
        }

        public static List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields)
        {
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName, query, pagerInfo, sortBy, fields);
        }

        public static List<T> GetAll<T>(string collectionName, int count, IMongoQuery query, IMongoSortBy sortBy, params string[] fields)
        {
            PagerInfo pagerInfo = new PagerInfo
            {
                Page = 1,
                PageSize = count
            };
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName, query, pagerInfo, sortBy, fields);
        }

        public static List<T> GetAll<T>(string connectionString, string databaseName, string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            List<T> list = new List<T>();
            using (server.RequestStart(database))
            {
                MongoCursor<T> cursor;
                MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    cursor = collection.FindAllAs<T>();
                }
                else
                {
                    cursor = collection.FindAs<T>(query);
                }
                if (sortBy != null)
                {
                    cursor.SetSortOrder(sortBy);
                }
                if (fields != null)
                {
                    cursor.SetFields(fields);
                }
                foreach (T local in cursor.SetSkip((pagerInfo.Page - 1) * pagerInfo.PageSize).SetLimit(pagerInfo.PageSize))
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public static long GetCount(string collectionName, IMongoQuery query)
        {
            return GetCount(ServerConnstr, DatabaseName, collectionName, query);
        }

        private static long GetCount(string connectionString, string databaseName, string collectionName, IMongoQuery query)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    return 0L;
                }
                return collection.Count(query);
            }
        }

        public static List<string> GetDistinctAll(string collectionName, string distinctkey)
        {
            IEnumerable<BsonValue> enumerable = GetDistinctAll(ServerConnstr, DatabaseName, collectionName, distinctkey);
            List<string> list = new List<string>();
            foreach (BsonValue value2 in enumerable)
            {
                list.Add(value2.ToString());
            }
            return list;
        }

        private static IEnumerable<BsonValue> GetDistinctAll(string connectionString, string databaseName, string collectionName, string distinctkey)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                return database.GetCollection<BsonDocument>(collectionName).Distinct(distinctkey);
            }
        }

        public static void GetGroup(string collectionName, IMongoQuery query, BsonJavaScript keyFunction, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).Group(query, keyFunction, initial, reduce, finalize);
            }
        }

        public static void GetGroup(string collectionName, IMongoQuery query, string key, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).Group(query, key, initial, reduce, finalize);
            }
        }

        public static void GetGroup(string collectionName, string key, IMongoQuery query, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).Group(query, key, initial, reduce, finalize);
            }
        }

        public static void GetMapReduce(string collectionName, BsonJavaScript map, BsonJavaScript reduce)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).MapReduce(map, reduce);
            }
        }

        public static void GetMapReduce(string collectionName, BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions options)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).MapReduce(map, reduce, options);
            }
        }

        public static void GetMapReduce(string collectionName, IMongoQuery query, BsonJavaScript map, BsonJavaScript reduce)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).MapReduce(query, map, reduce);
            }
        }

        public static void GetMapReduce(string collectionName, IMongoQuery query, BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions options)
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
                database.GetCollection<BsonDocument>(collectionName).MapReduce(query, map, reduce, options);
            }
        }

        public static T GetOne<T>(string collectionName, IMongoQuery query)
        {
            return GetOne<T>(ServerConnstr, DatabaseName, collectionName, query);
        }

        public static T GetOne<T>(string collectionName, string _id)
        {
            return GetOne<T>(ServerConnstr, DatabaseName, collectionName, _id);
        }

        public static T GetOne<T>(string collectionName, IMongoQuery query, IMongoSortBy sortBy, params string[] fields)
        {
            PagerInfo pagerInfo = new PagerInfo
            {
                Page = 1,
                PageSize = 1
            };
            return GetAll<T>(ServerConnstr, DatabaseName, collectionName, query, pagerInfo, sortBy, fields).FirstOrDefault<T>();
        }

        public static T GetOne<T>(string connectionString, string databaseName, string collectionName, IMongoQuery query)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                MongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    return collection.FindOneAs<T>();
                }
                return collection.FindOneAs<T>(query);
            }
        }

        public static T GetOne<T>(string connectionString, string databaseName, string collectionName, string _id)
        {
            ObjectId id;
            if (!ObjectId.TryParse(_id, out id))
            {
                return default(T);
            }
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                return database.GetCollection<BsonDocument>(collectionName).FindOneAs<T>(Query.EQ("_id", id));
            }
        }

        public static void GetReduce()
        {
            MongoServer server = new MongoClient(ServerConnstr).GetServer();
            MongoDatabase database = server.GetDatabase(DatabaseName);
            using (server.RequestStart(database))
            {
            }
        }

        public static IEnumerable<WriteConcernResult> InsertAll<T>(string collectionName, IEnumerable<T> entitys)
        {
            return InsertAll<T>(ServerConnstr, DatabaseName, collectionName, entitys);
        }

        public static IEnumerable<WriteConcernResult> InsertAll<T>(string connectionString, string databaseName, string collectionName, IEnumerable<T> entitys)
        {
            if (entitys == null)
            {
                return null;
            }
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                return database.GetCollection<BsonDocument>(collectionName).InsertBatch<T>(entitys);
            }
        }

        public static WriteConcernResult InsertOne<T>(string collectionName, T entity)
        {
            return InsertOne<T>(ServerConnstr, DatabaseName, collectionName, entity);
        }

        public static WriteConcernResult InsertOne<T>(string connectionString, string databaseName, string collectionName, T entity)
        {
            WriteConcernResult result = new WriteConcernResult();
            if (entity == null)
            {
                return null;
            }
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                return database.GetCollection<BsonDocument>(collectionName).Insert<T>(entity);
            }
        }

        public static WriteConcernResult UpdateAll<T>(string collectionName, IMongoQuery query, IMongoUpdate update)
        {
            return UpdateAll<T>(ServerConnstr, DatabaseName, collectionName, query, update);
        }

        public static WriteConcernResult UpdateAll<T>(string connectionString, string databaseName, string collectionName, IMongoQuery query, IMongoUpdate update)
        {
            WriteConcernResult result;
            if ((query == null) || (update == null))
            {
                return null;
            }
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                result = database.GetCollection<BsonDocument>(collectionName).Update(query, update,UpdateFlags.None);
                new UpdateBuilder();
            }
            return result;
        }

        public static WriteConcernResult UpdateOne<T>(string collectionName, T entity)
        {
            return UpdateOne<T>(ServerConnstr, DatabaseName, collectionName, entity);
        }

        private static WriteConcernResult UpdateOne<T>(string connectionString, string databaseName, string collectionName, T entity)
        {
            MongoServer server = new MongoClient(connectionString).GetServer();
            MongoDatabase database = server.GetDatabase(databaseName);
            using (server.RequestStart(database))
            {
                return database.GetCollection<BsonDocument>(collectionName).Save<T>(entity);
            }
        }

        // Properties
        private static string k_DatabaseName;
        public static string DatabaseName
        {
            get
            {
                return k_DatabaseName;
            }
            set
            {
                k_DatabaseName = value;
            }
        }

        private static string k_ServerConnstr;
        public static string ServerConnstr
        {
            get
            {
                return k_ServerConnstr;
            }
            set
            {
                k_ServerConnstr = value;
            }
        }
    }


}
