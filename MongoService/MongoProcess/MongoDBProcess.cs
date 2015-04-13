using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Mongo;
using MongoDal;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.Log;

namespace MongoProcess
{

    public class MongoDBProcess : LogClassBase
    {
        private HouseDal mongoHouseDal = new HouseDal(ConfigurationManager.AppSettings["HouseDbSqlConnection"]);
        private MongoHelper mongoHelper = new MongoHelper(ConfigurationManager.AppSettings["MongoConnection"]);
        private VillageDal villageDal = new VillageDal();
        public HouseDal HouseDal
        {
            get { return mongoHouseDal; }
        }

        public MongoHelper MongoHelper
        {
            get { return mongoHelper; }
        }

        public VillageDal VillageDal
        {
            get { return villageDal; }
        }

        [LogFile("MongoDBProcess", "MongoDBProcess", new[] { typeof(FileLogger) }, null)]
        public void Process(List<int> listids)
        {
            DataTable dataTable = mongoHouseDal.GetHouseModels(listids);
            var list = DataTableToDictionaryList(dataTable);
            foreach (Dictionary<string, Object> dictionary in list)
            {
                string id = dictionary["Id"].ToString();
                string villageGuid = villageDal.GetGuidById(dictionary["VId"]);
                dictionary.Add("VillageGuid", villageGuid);
                mongoHelper.UpdateAll<BsonDocument>("HouseCollection", Query.EQ("_id", id), new UpdateDocument("$set", dictionary.ToBsonDocument()));
            }
        }

        private static List<Dictionary<string, object>> DataTableToDictionaryList(DataTable dataTable)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(DataRowToDictionary(row));
            }
            return list;
        }

        [LogFile("MongoDBProcess", "MongoDBProcess", new[] { typeof(FileLogger) }, null)]
        public void Process(List<int> listids, Action<string> showLog)
        {
            DataTable dataTable = mongoHouseDal.GetHouseModels(listids);
            var list = DataTableToDictionaryList(dataTable);
            foreach (Dictionary<string, Object> dictionary in list)
            {
                string id = dictionary["Id"].ToString();
                string villageGuid = villageDal.GetGuidById(dictionary["VId"]);
                dictionary.Add("VillageGuid", villageGuid);
                mongoHelper.UpdateAll<BsonDocument>("HouseCollection", Query.EQ("_id", id), new UpdateDocument("$set", dictionary.ToBsonDocument()));
                showLog("MongoDB添加成功：房源ID：" + id);
            }
        }
        [LogFile("MongoDBProcess", "MongoDBProcess", new[] { typeof(FileLogger) }, null)]
        public void Process(int startId, int endId, Action<string> showLog)
        {
            DataTable dataTable = mongoHouseDal.GetHouseModels(startId, endId);
            var list = DataTableToDictionaryList(dataTable);
            foreach (Dictionary<string, Object> dictionary in list)
            {
                string id = dictionary["Id"].ToString();
                string villageGuid = villageDal.GetGuidById(dictionary["VId"]);
                dictionary.Add("VillageGuid", villageGuid);
                mongoHelper.UpdateAll<BsonDocument>("HouseCollection", Query.EQ("_id", id), new UpdateDocument("$set", dictionary.ToBsonDocument()));
                showLog("MongoDB添加成功：房源ID：" + id);
            }
        }
        private static Dictionary<string, object> DataRowToDictionary(DataRow dataRow)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                dictionary.Add(column.ColumnName, dataRow[column.ColumnName] == DBNull.Value ? null : dataRow[column.ColumnName]);
            }
            return dictionary;
        }
        [LogFile("MongoDBProcess", "Upsert", new[] { typeof(FileLogger) }, null)]
        public bool Upsert(string id)
        {
            bool result = false;

            DataTable dataTable = mongoHouseDal.GetDataTableByWhere(" where  S_LongHouseBase.Id = " + id);
            if (dataTable.Rows.Count > 0)
            {
                Dictionary<string, object> dictionary = DataRowToDictionary(dataTable.Rows[0]);
                string villageGuid = villageDal.GetGuidById(dictionary["VId"]);
                dictionary.Add("VillageGuid", villageGuid);
                WriteConcernResult writeConcernResult = mongoHelper.UpdateAll<BsonDocument>("HouseCollection", Query.EQ("_id", id),
                          new UpdateDocument("$set", dictionary.ToBsonDocument()));
                result = CheckWriteConcernResult(writeConcernResult);
            }
            return result;
        }
        [LogFile("MongoDBProcess", "Delete", new[] { typeof(FileLogger) }, null)]
        public bool Delete(string id)
        {
            WriteConcernResult writeConcernResult = mongoHelper.DeleteAll("HouseCollection", Query.EQ("_id", id));
            return CheckWriteConcernResult(writeConcernResult);
        }

        private static bool CheckWriteConcernResult(WriteConcernResult writeConcernResult)
        {
            bool result = false;
            if (writeConcernResult.Ok && string.IsNullOrEmpty(writeConcernResult.LastErrorMessage))
            {
                result = true;
            }
            return result;
        }
        [LogFile("MongoDBProcess", "GetDataById", new[] { typeof(FileLogger) }, null)]
        public Dictionary<string, object> GetDataById(string id)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            BsonDocument bsonDocument = mongoHelper.GetOne<BsonDocument>("HouseCollection", Query.EQ("_id", id));
            if (bsonDocument != null)
            {
                BsonDocumentToDictionary(bsonDocument, dictionary);
            }
            return dictionary;
        }

        //private static void BsonDocumentToDictionary(BsonDocument bsonDocument, Dictionary<string, object> dictionary)
        //{
        //    foreach (BsonElement element in bsonDocument)
        //    {
        //        object obj = element.Value;
        //        if (element.Value.BsonType == BsonType.Array)
        //        {
        //            BsonArray bsonArray = element.Value.AsBsonArray;
        //            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        //            foreach (BsonValue value in bsonArray.Values)
        //            {
        //                Dictionary<string, object> dic = new Dictionary<string, object>();
        //                foreach (BsonElement ele in value.AsBsonDocument)
        //                {
        //                    dic.Add(ele.Name, ele.Value);
        //                }
        //                list.Add(dic);
        //            }
        //            obj = list;
        //        }
        //        if (element.Value.BsonType == BsonType.DateTime)
        //        {
        //            obj = element.Value.AsDateTime;
        //        }
        //        dictionary.Add(element.Name, obj);
        //    }
        //}
        private static void BsonDocumentToDictionary(BsonDocument bsonDocument, Dictionary<string, object> dictionary)
        {
            foreach (BsonElement element in bsonDocument)
            {
                object obj = element.Value;
                if (element.Value.BsonType == BsonType.Array)
                {
                    BsonArray bsonArray = element.Value.AsBsonArray;
                    List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                    foreach (BsonValue value in bsonArray.Values)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        BsonDocumentToDictionary(value.AsBsonDocument, dic);
                        list.Add(dic);
                    }
                    obj = list;
                }
                if (element.Value.BsonType == BsonType.DateTime)
                {
                    obj = element.Value.AsDateTime.UtcToLocalTime();
                }
                if (element.Value.IsBoolean)
                {
                    obj = element.Value.AsBoolean;
                } if (element.Value.IsBoolean)
                {
                    obj = element.Value.AsBoolean;
                } if (element.Value.IsBsonNull)
                {
                    obj = null;
                } if (element.Value.IsInt32)
                {
                    obj = element.Value.AsInt32;
                } if (element.Value.IsString)
                {
                    obj = element.Value.AsString;
                }
                if (element.Value.IsDouble)
                {
                    obj = element.Value.AsDouble;
                }
                dictionary.Add(element.Name, obj);
            }
        }
    }
}
