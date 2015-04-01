using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HouseDataInit
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
        public DataSet  GetDataSetForLongHouse(string cityid)
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
                        a.UId,
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
                        a.IsRecommend,
                        a.HouseScore,
                        a.IsReal,
                        a.IsReturn,
                        a.IsWorry,
                        a.IsMorePic,
                        a.QuotedMaxPrice,
                        a.QuotedMinPrice,
                        a.IsSheng,
                        a.IsPlat,
                        a.IsDK,
                        a.IsDaiK,
                        a.BuildingType,
                        a.BuildingYear,
                        a.OfficeType
                        from s_longhouse a
                        where a.IsDel=0 and a.state=1  and PType!=4   and a.AuthenticationState=1 " + " and CityId " + cityid ;
               
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
                       " from S_LongHouseBidPrice where  IsDel=0  and ShId=" + houseid;
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
        /// 返回个人用户性别
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataRow GetUser(string userid)
        {
            string sql = " select * from LocalUser where UserId=" + userid;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0].Rows[0];
        }

        /// <summary>
        /// 返回是否优秀经纪人
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataRow GetAgent(string userid)
        {
            string sql = " select * from U_Agent where Id=" + userid;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0].Rows[0];
        }
        /// <summary>
        /// 返回总公司
        /// </summary>
        /// <param name="acid"></param>
        /// <returns></returns>
        public int GetCAgent(int acid)
        {
            string sql = " select CPId from U_AgentComPany where  id=" + acid;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql));
        }
        /// <summary>
        /// 返回活动房源
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns></returns>
        public DataSet GetActiveDataSetForLongHouse(string houseid)
        {

            string sql = @"select 
                        a.*
                        from s_longhouse a
                       
                        where a.IsDel=0 and a.state=1 and PType=4 ";
            if (!string.IsNullOrEmpty(houseid))
            {
                sql += " and a.Id=" + houseid;
            }
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        #endregion
    }
}
