using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDBHelper.Base;
using MongoDBHelper.Query;

namespace MongoDBHelper.Wrapper
{
    class HouseDB_QueryWrapper<T>:IQuery<T>
    {
        private IQuery<T> query;

        public HouseDB_QueryWrapper(string connectionString, string dataBaseName, string collectionName)
        {
            query = new HouseDB_Query<T>(connectionString, dataBaseName, collectionName);
        }

        public T FindOne(int houseid)
        {
            return query.FindOne(houseid);
        }

        public List<T> FindAll(params int[] houseIds)
        {
            return query.FindAll(houseIds);
        }
    }
}
