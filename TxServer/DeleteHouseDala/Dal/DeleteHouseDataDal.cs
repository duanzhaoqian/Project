using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;

namespace DeleteHouseDala.Dal
{
    public class DeleteHouseDataDal
    {

        /// <summary>
        /// 返回未操作房源
        /// </summary>
        /// <returns></returns>
        public DataTable GetWillDeleteData(int day, int type)
        {
            string delid = St.ConfigKey("DelId");
            string where = string.Empty;
            if (!string.IsNullOrEmpty(delid))
            {
                where = " and id=" + delid;
            }
            string sql = @"SELECT id FROM dbo.S_LongHouse(NOLOCK) WHERE dateadd(day," + day + ",updatetime)<getdate() ";
            sql += " AND [Type]=0 and IsDel=0  and [state]=0 and pType<>4 " + where;
            return BaseInfoDB.GetTable(sql);
        }
        /// <summary>
        /// 返回采集房源
        /// </summary>
        /// <returns></returns>
        public DataTable GetCollectionDeleteData(int DelCJLongHouseDay)
        {
            string delid = St.ConfigKey("DelId");
            string where = string.Empty;
            if (!string.IsNullOrEmpty(delid))
            {
                where = " and id=" + delid;
            }
            string sql = @"SELECT id,cityid,state,AuthenticationState FROM dbo.S_LongHouse(NOLOCK) WHERE 
fromurl IS NOT NULL AND fromurl <> '' AND DATEADD(day, " + DelCJLongHouseDay + ", updatetime) < GETDATE() AND IsDel=0 " + where;
            return BaseInfoDB.GetTable(sql);
        }
        /// <summary>
        /// 修改下架状态
        /// </summary>
        /// <param name="id"></param>
        public void UpdateIsDelete(string id)
        {
            string sql = @" update s_LongHouse set IsDel=1,updatetime=getdate() where Id=" + id;
            BaseInfoDB.SqlExecute(sql);
        }
        /// <summary>
        /// 返回删除房源
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeleteData(int day)
        {
            string delid = St.ConfigKey("DelId");
            string where = " dateadd(day," + day + ",updatetime)<getdate() and IsDel=1 and pType<>4 ";
            if (!string.IsNullOrEmpty(delid))
            {
                where = " id=" + delid;
            }
            string sql = @"SELECT Id,InnerCode,CityId FROM dbo.S_LongHouse WHERE " + where;
            return BaseInfoDB.GetTable(sql);
        }
        /// <summary>
        /// 删除房源
        /// </summary>
        /// <param name="id"></param>
        public void DeleteHouseData(string id)
        {
            string sql = @" delete from s_LongHouse  where Id=" + id +
                          @" delete from S_LongHouseOther where SLHId=" + id +
                          @"  delete from S_LongHouseBidPrice where ShId=" + id;
            BaseInfoDB.SqlExecute(sql);
            //SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
    }
}
