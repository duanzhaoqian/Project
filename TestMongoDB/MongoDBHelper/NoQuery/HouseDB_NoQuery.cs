using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Wrappers;
using MongoDBHelper.Base;

namespace MongoDBHelper.NoQuery
{
    class HouseDB_NoQuery<T> : DBBase<T>, INoQuery<T>
    {
        public HouseDB_NoQuery(string connectionString, string dataBaseName, string collectionName)
            : base(connectionString, dataBaseName, collectionName)
        {

        }
        public object Update<T>(T t)
        {
            return mongoCollection.Update(new QueryDocument("_id", Convert.ToInt32(t.GetType().GetProperty("_id").GetValue(t, null))), new UpdateDocument("$set", t.ToBsonDocument()));
            //return mongoCollection.Save(t, new MongoInsertOptions() { CheckElementNames = true, Flags = InsertFlags.None });
        }

        /// <summary>
        /// 批量插入方法，_id必须不能重复
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>List<Wr></returns>
        public object Update<T>(List<T> list)
        {
            MongoInsertOptions mongoInsertOptions = new MongoInsertOptions() { Flags = InsertFlags.ContinueOnError };
            return mongoCollection.InsertBatch(list, mongoInsertOptions).ToList();


            //List<WriteConcernResult> listResults = new List<WriteConcernResult>();
            //foreach (T t in list)
            //{
            //    listResults.Add((WriteConcernResult)Update(t));
            //}
            //return listResults;
        }
    }
}
