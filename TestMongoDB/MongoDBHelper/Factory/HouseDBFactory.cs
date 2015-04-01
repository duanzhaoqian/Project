using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDBHelper.NoQuery;
using MongoDBHelper.Query;
using MongoDBHelper.Wrapper;

namespace MongoDBHelper.Factory
{
    public class HouseDBFactory<T>
    {
        public static INoQuery<T> GetNoQuery(string connectionString, string dataBaseName, string collectionName)
        {
            return new HouseDB_NoQueryWrapper<T>(connectionString, dataBaseName, collectionName);
        }

        public static IQuery<T> GetQuery(string connectionString, string dataBaseName, string collectionName)
        {
            return new HouseDB_QueryWrapper<T>(connectionString, dataBaseName, collectionName);
        } 
    }
}
