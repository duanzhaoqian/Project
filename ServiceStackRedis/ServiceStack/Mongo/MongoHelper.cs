using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace ServiceStack.Mongo
{
    public sealed class MongoHelper : IMongo
    {
        // Fields
        private MongoDatabase database;
        private MongoServer server;

        // Methods
        public MongoHelper(string serverconnstr)
        {
            this.ServerConnstr = serverconnstr;
            Uri uri = new Uri(serverconnstr);
            this.DatabaseName = uri.LocalPath.Remove(0, 1);
            this.server = MongoServer.Create(this.ServerConnstr);
            this.database = this.server.GetDatabase(this.DatabaseName);
        }

        public MongoHelper(string serverconnstr, string databasename)
        {
            this.ServerConnstr = serverconnstr;
            this.DatabaseName = databasename;
            this.server = MongoServer.Create(this.ServerConnstr);
            this.database = this.server.GetDatabase(databasename);
        }

        public void CreateIndex(string collectionName, params string[] keyNames)
        {
            new SafeModeResult();
            if (keyNames != null)
            {
                using (this.server.RequestStart(this.database))
                {
                    MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                    if (!collection.IndexExists(keyNames))
                    {
                        collection.EnsureIndex(keyNames);
                    }
                }
            }
        }

        public SafeModeResult Delete(string collectionName, string _id)
        {
            ObjectId id;
            if (!ObjectId.TryParse(_id, out id))
            {
                return null;
            }
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection<BsonDocument>(collectionName).Remove(Query.EQ("_id", id));
            }
        }

        public SafeModeResult DeleteAll(string collectionName)
        {
            return this.DeleteAll(collectionName, null);
        }

        public SafeModeResult DeleteAll(string collectionName, IMongoQuery query)
        {
            using (this.server.RequestStart(this.database))
            {
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    return collection.RemoveAll();
                }
                return collection.Remove(query);
            }
        }

        public bool ExitDocument(string collectionName, IMongoQuery query)
        {
            if (this.GetObjectId(collectionName, query) == ObjectId.Empty)
            {
                return false;
            }
            return true;
        }

        public List<T> GetAll<T>(string collectionName)
        {
            List<T> list = new List<T>();
            using (this.server.RequestStart(this.database))
            {
                foreach (T local in this.database.GetCollection<BsonDocument>(collectionName).FindAllAs<T>())
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public List<T> GetAll<T>(string collectionName, IMongoQuery query)
        {
            List<T> list = new List<T>();
            using (this.server.RequestStart(this.database))
            {
                MongoCursor<T> cursor;
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    cursor = collection.FindAllAs<T>();
                }
                else
                {
                    cursor = collection.FindAs<T>(query);
                }
                foreach (T local in cursor)
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public List<T> GetAll<T>(string collectionName, IMongoQuery query, params string[] fields)
        {
            List<T> list = new List<T>();
            using (this.server.RequestStart(this.database))
            {
                MongoCursor<T> cursor;
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    cursor = collection.FindAllAs<T>();
                }
                else
                {
                    cursor = collection.FindAs<T>(query);
                }
                if (fields != null)
                {
                    cursor.SetFields(fields);
                }
                foreach (T local in cursor)
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo)
        {
            return this.GetAll<T>(collectionName, query, pagerInfo, SortBy.Null, null);
        }

        public List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy)
        {
            return this.GetAll<T>(collectionName, query, pagerInfo, sortBy, null);
        }

        public List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, params string[] fields)
        {
            return this.GetAll<T>(collectionName, query, pagerInfo, SortBy.Null, fields);
        }

        public List<T> GetAll<T>(string collectionName, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields)
        {
            List<T> list = new List<T>();
            using (this.server.RequestStart(this.database))
            {
                MongoCursor<T> cursor;
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
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

        public List<T> GetAll<T>(string collectionName, int count, IMongoQuery query, IMongoSortBy sortBy, params string[] fields)
        {
            PagerInfo pagerInfo = new PagerInfo
            {
                Page = 1,
                PageSize = count
            };
            return this.GetAll<T>(collectionName, query, pagerInfo, sortBy, fields);
        }

        public List<T> GetAll<T>(string collectionName, int top, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy)
        {
            return this.GetAll<T>(collectionName, top, query, pagerInfo, sortBy, null);
        }

        public List<T> GetAll<T>(string collectionName, int top, IMongoQuery query, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields)
        {
            List<T> list = new List<T>();
            using (this.server.RequestStart(this.database))
            {
                MongoCursor<T> cursor;
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    cursor = collection.FindAllAs<T>().SetLimit(top);
                }
                else
                {
                    cursor = collection.FindAs<T>(query).SetLimit(top);
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

        public long GetCount(string collectionName, IMongoQuery query)
        {
            using (this.server.RequestStart(this.database))
            {
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    return collection.Count();
                }
                return collection.Count(query);
            }
        }

        public List<string> GetDistinctAll(string collectionName, string distinctkey, QueryDocument query)
        {
            IEnumerable<BsonValue> enumerable = this.GetDistinctAllIEnumerable(collectionName, distinctkey, query);
            List<string> list = new List<string>();
            foreach (BsonValue value2 in enumerable)
            {
                list.Add(value2.ToString());
            }
            return list;
        }

        public IEnumerable<BsonValue> GetDistinctAllIEnumerable(string collectionName, string distinctkey, QueryDocument query)
        {
            using (this.server.RequestStart(this.database))
            {
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (null == query)
                {
                    return collection.Distinct(distinctkey);
                }
                return collection.Distinct(distinctkey, query);
            }
        }

        public void GetGroup(string collectionName, IMongoQuery query, BsonJavaScript keyFunction, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            throw new NotImplementedException();
        }

        public void GetGroup(string collectionName, IMongoQuery query, string key, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            throw new NotImplementedException();
        }

        public void GetGroup(string collectionName, string key, IMongoQuery query, BsonDocument initial, BsonJavaScript reduce, BsonJavaScript finalize)
        {
            throw new NotImplementedException();
        }

        public MapReduceResult GetMapReduce(string collectionName, BsonJavaScript map, BsonJavaScript reduce)
        {
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection(collectionName).MapReduce(map, reduce);
            }
        }

        public MapReduceResult GetMapReduce(string collectionName, BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions options)
        {
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection(collectionName).MapReduce(map, reduce, options);
            }
        }

        public MapReduceResult GetMapReduce(string collectionName, IMongoQuery query, BsonJavaScript map, BsonJavaScript reduce)
        {
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection(collectionName).MapReduce(query, map, reduce);
            }
        }

        public MapReduceResult GetMapReduce(string collectionName, IMongoQuery query, BsonJavaScript map, BsonJavaScript reduce, IMongoMapReduceOptions options)
        {
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection(collectionName).MapReduce(query, map, reduce, options);
            }
        }

        public ObjectId GetObjectId(string collectionName, IMongoQuery query)
        {
            BsonDocument one = this.GetOne<BsonDocument>(collectionName, query);
            if (null != one)
            {
                //return one.get_Item("_id").get_AsObjectId();
                return one.GetElement("_id").Value.AsObjectId;
            }
            return ObjectId.Empty;
        }

        public T GetOne<T>(string collectionName, IMongoQuery query)
        {
            T local = default(T);
            using (this.server.RequestStart(this.database))
            {
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (query == null)
                {
                    return collection.FindOneAs<T>();
                }
                return collection.FindOneAs<T>(query);
            }
        }

        public T GetOne<T>(string collectionName, string _id)
        {
            ObjectId id;
            T local = default(T);
            if (!ObjectId.TryParse(_id, out id))
            {
                return default(T);
            }
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection<BsonDocument>(collectionName).FindOneAs<T>(Query.EQ("_id", id));
            }
        }

        public T GetOne<T>(string collectionName, IMongoQuery query, IMongoSortBy sortBy, params string[] fields)
        {
            PagerInfo pagerInfo = new PagerInfo
            {
                Page = 1,
                PageSize = 1
            };
            return this.GetAll<T>(collectionName, query, pagerInfo, sortBy, fields).FirstOrDefault<T>();
        }

        public IEnumerable<SafeModeResult> InsertAll<T>(string collectionName, IEnumerable<T> entitys)
        {
            if (entitys == null)
            {
                return null;
            }
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection<BsonDocument>(collectionName).InsertBatch<T>(entitys);
            }
        }

        public void InsertOne<T>(string collectionName, T entity)
        {
            new SafeModeResult();
            if (entity != null)
            {
                using (this.server.RequestStart(this.database))
                {
                    this.database.GetCollection<BsonDocument>(collectionName).Insert<T>(entity);
                }
            }
        }

        public bool InsertOneSafe<T>(string collectionName, T entity)
        {
            bool flag;
            try
            {
                new SafeModeResult();
                if (entity == null)
                {
                    flag = false;
                }
                else
                {
                    using (this.server.RequestStart(this.database))
                    {
                        this.database.GetCollection<BsonDocument>(collectionName).Insert<T>(entity, SafeMode.True);
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public SafeModeResult UpdateAll<T>(string collectionName, IMongoQuery query, IMongoUpdate update)
        {
            if ((query == null) || (update == null))
            {
                return null;
            }
            using (this.server.RequestStart(this.database))
            {
                WriteConcernResult writeConcern=this.database.GetCollection<BsonDocument>(collectionName).Update(query, update,UpdateFlags.Upsert,WriteConcern.Acknowledged);
                return writeConcern;
            }
        }

        public SafeModeResult UpdateOne<T>(string collectionName, T entity)
        {
            using (this.server.RequestStart(this.database))
            {
                return this.database.GetCollection<BsonDocument>(collectionName).Save<T>(entity);
            }
        }

        // Properties
        public string DatabaseName { get; set; }

        public string ServerConnstr { get; set; }

        public T GetOne<T>(string collectionName, IMongoQuery iMongoQuery, FieldsBuilder fieldsBuilder)
        {
            List<T> list = new List<T>();
            using (this.server.RequestStart(this.database))
            {
                MongoCursor<T> cursor;
                MongoCollection<BsonDocument> collection = this.database.GetCollection<BsonDocument>(collectionName);
                if (iMongoQuery == null)
                {
                    cursor = collection.FindAllAs<T>();
                }
                else
                {
                    cursor = collection.FindAs<T>(iMongoQuery);
                }

                if (fieldsBuilder != null)
                {
                    cursor.SetFields(fieldsBuilder);
                }
                foreach (T local in cursor)
                {
                    list.Add(local);
                }
            }
            return list.First();
        }
    }


}
