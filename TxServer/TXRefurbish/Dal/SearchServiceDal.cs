using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TXRefurbish.Dal
{
    public class SearchServiceDal
    {
        string ConnectionString = "";
        public SearchServiceDal(string Connstring)
        {
            ConnectionString = Connstring;
        }
        #region   返回房源相关索引
        /// <summary>
        /// 返回房源数据
        /// </summary>
        /// <param name="houseid"></param>
        public DataSet  GetDataSetForLongHouse(string houseid)
        {
           
             string  sql = @"select 
                        a.Id,
                        a.CityId,
                        a.Type as SearchType,
                        a.PropertyType,
                        a.CityName,
                        a.DId,
                        a.DName,
                        a.BId,
                        a.BName,
                       
                        a.RentType,
                        a.QuotedPrice,
                        a.Area,
                        a.Room,
                        a.Mobile,
                        a.SupportingFacilities as Facilities, 
                        a.Renovation,
                        a.BidStatus,
                        a.IsAgency,
                        a.PType,
                        a.UserType,
                        a.Address,
                        a.VId,
                        a.VName,
                        a.UName,
                        a.PType,
                        a.InnerCode,
                        a.BidCount,
                        a.BidStartTime,
                        a.BidEndTime,
                        (year(getdate())-a.BuildingYear) as HouseAge,
                        a.Title,
                        a.IsRecommend,
                        a.Orientation,
                        a.TheFloar,
                        a.AllFloar,
                        a.UpdateTime,
                      
                        
                        a.MaxPrice,
                        a.MinPrice,
                        a.BidCount,
                        a.PayType,
                        a.IsGuarantee,
                        a.IsSVip,
                        a.Hall,
                        a.Orientation,
                        a.BidUserNum,
                        a.BuildingArea,
                        a.IsTxfree,
                        a.IsFullYears,
                        a.IsUnique,
                        a.BuildingStructure,
                        a.PTypeState,
                        a.CompanyName,
                        a.CreateTime,
                        a.IsBargaining,
                        a.HouseType,
                        a.IsRecommend
                        from s_longhouse a
                       
                        where a.IsDel=0 and (a.state=1 or a.BidStatus=2)   and a.AuthenticationState=1 ";
                if (!string.IsNullOrEmpty(houseid))
                {
                    sql += " and a.Id=" + houseid;
                }
                return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns></returns>
        public DataSet GetDataSetForBid(string houseid)
        {
            string sql = @"select  BidPrice as BidPrice, BidUserMobile as BidUserMobile,BidUserName   " +
                       " from S_LongHouseBidPrice where  IsDel=0 and (Status=1 or Status=2) and ShId=" + houseid;
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        #endregion



        #region 预约刷新
        /// <summary>
        /// 返回需要刷新数据
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public DataSet GetTxRefurbishTimingData(DateTime date)
        {
            string sql = "  select houseid,userid  from  Refurbish r where EndTime>='" + date.ToShortDateString() + "' and [Hour]=" + date.Hour + " and [Minute]=" + date.Minute;
                       

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        }
        /// <summary>
        /// 返回经纪人服务允许操作数
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetServiceCount(int userid)
        {
            int count = 10;
            string sql = "select ACount from U_Service where Id= (select top 1 S_Id From AgentService a  where a.A_Id="+userid+" and State=1 and IsDel=0) ";
                      
            object o= SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql);
            if (o != null)
            {
                if (o.ToString() != "")
                {
                    count = int.Parse(o.ToString());
                }
            }
            return count;
        }
        /// <summary>
        ///  返回经纪人已操作数
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        public int GetRefurbishCount(int agentid)
        {
            string sql = "  select RefurbishEndCount from U_UserOther where UserType=1 and UserID= " + agentid;
            object o = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql);
            return Convert.ToInt32(o);
        }
        /// <summary>
        /// 经纪人刷新成功
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="houseid"></param>
        /// <param name="date"></param>
        public void RefurbishR(int userid, int houseid, DateTime date)
        {
            string sql = " Update U_UserOther set RefurbishEndCount=RefurbishEndCount+1 where UserType=1 and UserID=" + userid +
                         "  and (select ptype from s_LongHouse where id=" + houseid + ")=1 " +

                     " Update S_LongHouseOther set RefurbishEndCount=RefurbishEndCount+1,RefurbishCount=RefurbishCount-1 where SLHId=" + houseid +
                     " Update S_LongHouse set UpdateTime=getdate() where  Id =" + houseid +
                    " INSERT INTO RefurbishLog " +
                    "  ([UserID] " +
                    "  ,[HouseID] " +
                   "   ,[RTime] " +
                  "  ,[RType] " +
                  "   ,[IsR] " +
                  "   ,[CreateTime]) " +
                  "  VALUES " +
                   "   ( " + userid +
                      "  , " + houseid +
                    "  , '" + date.ToString() +
                      "'  ,1 " +
                    "  ,1 " +
                    "   ,getdate())";
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 经纪人刷新失败
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="houseid"></param>
        /// <param name="date"></param>
        public void RefurbishF(int userid, int houseid, DateTime date)
        {
            string sql = " INSERT INTO RefurbishLog " +
                    "  ([UserID] " +
                    "  ,[HouseID] " +
                   "   ,[RTime] " +
                  "  ,[RType] " +
                  "   ,[IsR] " +
                  "   ,[CreateTime]) " +
                  "  VALUES " +
                   "   ( " + userid +
                      "  , " + houseid +
                    "  , '" + date.ToString() +
                      "'  ,1 " +
                    "  ,0 " +
                    "   ,getdate())";
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        #endregion 

      

        
    }
}
