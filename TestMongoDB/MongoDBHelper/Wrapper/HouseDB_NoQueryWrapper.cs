using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using MongoDB.Driver;
using MongoDBHelper.NoQuery;

namespace MongoDBHelper.Wrapper
{
    class HouseDB_NoQueryWrapper<T> : INoQuery<T>
    {
        private INoQuery<T> noQuery;
        public HouseDB_NoQueryWrapper(string connectionString, string dataBaseName, string collectionName)
        {
            noQuery = new HouseDB_NoQuery<T>(connectionString, dataBaseName, collectionName);
        }

        public HouseDB_NoQueryWrapper(INoQuery<T> t)
        {
            noQuery = t;
        }
        public object Update<T>(T t)
        {
            PropertyInfo propertyInfo = t.GetType().GetProperty("_id");
            int _id;
            if (propertyInfo == null || !int.TryParse(propertyInfo.GetValue(t, null).ToString(), out _id) || _id == 0)
            {
                throw new ArgumentException("Require _id Property And _id value can't 0");
            }
            WriteConcernResult writeConcernResult = noQuery.Update(t) as WriteConcernResult;
            if (writeConcernResult == null || !writeConcernResult.Ok)
            {
                throw new Exception("Update Failed，_id：" + _id);
            }
            return writeConcernResult;
        }


        public object Update<T>(List<T> list)
        {
            List<WriteConcernResult> listResults=new List<WriteConcernResult>();
            foreach (T t in list)
            {
                listResults.Add((WriteConcernResult)Update(t));
            }
            return listResults;
        }
    }
}
