using System;
using System.Data;

namespace TXPublicData.Dal
{
    public class SearchServiceDal
    {
        string ConnectionString = "";
        public SearchServiceDal(string Connstring)
        {
            ConnectionString = Connstring;
        }
     

      

        #region 最新需求

    

        #region 公共数据索引服务
        /// <summary>
        /// 返回小区数据
        /// </summary>
        /// <param name="villageid"></param>
        /// <returns></returns>
        public DataSet GetVillage(string villageid)
        {
            string sql = @"  select v.Id,v.CityId,v.DId,v.BId,v.Name,v.Address,v.Lat,v.Lng,d.Name as DName,b.Name as BName,v.Guid  " +
                         @"  from Village as v " +
                         @"  join District as d on d.Id=v.DId join Business as b on b.Id=v.BId where v.IsDel=0 ";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 返回小区地铁关系数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetVillageSubWay(string villageid)
        {
            string sql = @" select s.tid,s.vid,t.pid from traffic as t join subway as s  on  s.tid=t.tid " +
                          "  where s.IsDel=0 and t.IsDel=0 ";
            if (!string.IsNullOrEmpty(villageid))
            {
                sql += " and s.vid=" + villageid;
            }
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 返回区域相关数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetArea()
        {
            string sql = @" select * from  ( select b.id as BId,b.Name  as BName,b.DId as DId ,d.Name as DName ,d.CityId as CityId, a.Name as CityName,d.sort as sort,b.adprice as adprice " +
                          " from  Business as b join District as d on b.Did=d.Id join Area as a " +
                          " on d.CityId=a.Id " +
                          " union " +
                         " select 0 as BId,'' as BName,d.Id as DId ,d.Name as DName ,d.CityId as CityId, a.Name as CityName,d.sort as sort,d.adprice as adprice " +
                          " from   District as d  join Area as a " +
                         "  on d.CityId=a.Id ) as aa order by aa.sort ";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 返回地铁线路数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetTraffic()
        {
            string sql = @" Select t.TId,t.PId,t.Name as TName,t.CityId,a.Name as CityName  " +
                       "   from Traffic as t join Area as a on t.Cityid =a.Id order by t.Sort ";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 返回经纪公司信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetCompany()
        {
            string sql = @" Select  * from U_Agentcorporation where IsDel=0 ";


            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 返回广告数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetAdvert()
        {
            //string sql = @" Select  a.*,al.AreaId as aid,al.AreaName as aname from Advert a join AdvertLog al on a.houseid=al.houseid   where a.IsDel=0 and a.BuyTime='" + DateTime.Now.Date + "' " +
            //            " order by a.CreateTime desc ";
            string sql = @" Select  a.* from Advert a   where a.IsDel=0 and a.BuyTime='" + DateTime.Now.Date + "' " +
                         " order by a.CreateTime desc ";

            
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetAreaData(string id, string type)
        {
            string sql="select a.id,a.name from district d join area a on d.cityid=a.id where d.id="+id;
            if(type=="2")
            {
                sql="select a.id,a.name from business b join area a on b.cityid=a.id  where b.id="+id;
            }
             return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
       
        #endregion

        #endregion

    }
}
