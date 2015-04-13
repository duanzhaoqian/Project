using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ServiceStack;
using ServiceStack.TCP;

namespace MongoDal
{
    public class VillageDal
    {
        public string GetGuidById(object p)
        {
            VillageRedisModel villageRedisModel = new VillageRedisModel();
            if (p != null&&p!="0")
            {
                string key = "mongovillage" + p;
                try
                {
                    villageRedisModel = RedisHelperv2.GetValue<VillageRedisModel>(key, FunctionTypeEnum.BaseData, 0);
                }
                catch
                {

                    villageRedisModel = null;
                }
                if (villageRedisModel == null)
                {
                    villageRedisModel = new VillageRedisModel();
                    string sql = "SELECT id,Guid FROM dbo.village WHERE id = " + p;
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["WebDbSqlConnection"]))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                        {
                            sqlConnection.Open();
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.HasRows)
                                {
                                    sqlDataReader.Read();
                                    
                                    villageRedisModel.Id =sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Id"))?null: sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id")).ToString();
                                    villageRedisModel.Guid = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("Guid")) ? null : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Guid"));
                                }
                            }
                        }
                    }
                    try
                    {
                        RedisHelperv2.SetValue(key, villageRedisModel, FunctionTypeEnum.BaseData, 0, 1440);
                    }
                    catch
                    {

                    }
                }
            }
            return villageRedisModel.Guid;
        }
    }

    public class VillageRedisModel
    {
        public string Id { get; set; }
        public string Guid { get; set; }
    }
}
