using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Wrappers;
using MongoDBHelper.Base;

namespace MongoDBHelper.Query
{
    class HouseDB_Query<T> : DBBase<T>, IQuery<T>
    {
        public HouseDB_Query(string connectionString, string dataBaseName, string collectionName)
            : base(connectionString, dataBaseName, collectionName)
        {
        }

        public T FindOne(int houseid)
        {
            return this.mongoCollection.FindOneAs<T>(MongoDB.Driver.Builders.Query.EQ("_id",houseid));
        }

        public List<T> FindAll(params int[] houseIds)
        {
            List<IMongoQuery> listqQueries=new List<IMongoQuery>();
            foreach (int houseId in houseIds)
            {
                listqQueries.Add(new QueryDocument("_id",houseId));
            }
            MongoCursor<T> mongoCursor = mongoCollection.FindAs<T>(MongoDB.Driver.Builders.Query.Or(listqQueries));
            return mongoCursor.ToList();
        }
    }
}
