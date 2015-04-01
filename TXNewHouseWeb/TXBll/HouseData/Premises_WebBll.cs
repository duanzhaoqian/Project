using System.Collections.Generic;
using TXModel.Web;
using TXCommons.Index;
using System.Data;
using System.Reflection;
using System;
namespace TXBll.HouseData
{
    /// <summary>
    /// 楼盘BLL类-前台
    /// </summary>
    public partial class PremisesBll
    {
        #region  新房前台用到的数据访问层方法

        #region 通过楼盘ID获取楼盘基本信息
        /// <summary>
        /// 通过楼盘ID获取楼盘基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TXOrm.Premis GetPremisesbyId(int Id)
        {
            return _premisesDal.GetPremisesbyId(Id);
        }
        public List<TXOrm.Premis> GetPremisesbyName(string Name)
        {
            return _premisesDal.GetPremisesbyName(Name);
        }
          #endregion

        #region 通过楼盘ID和楼栋ID获取楼栋基本信息
        /// <summary>
        /// 通过楼盘ID和楼栋ID获取楼栋基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TXOrm.Building GetBuildingInfobyId(int premisesid, int id)
        {
            return _premisesDal.GetBuildingInfobyId( premisesid,  id);
        }

        public TXOrm.Building GetBuildingInfobyPNum(int premisesid, int pnum)
        {
            return _premisesDal.GetBuildingInfobyPNum(premisesid, pnum);
        }
        #endregion

        #region 获取房源列表
        /// <summary>
        /// 获取房源列表
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="buildingid"></param>
        /// <param name="unitId"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public List<TXOrm.House> GetHouseList(int premisesId, int buildingid, int unitId, int floor)
        {
            return _premisesDal.GetHouseList( premisesId,  buildingid,  unitId,  floor);
        }
        public List<PremisesActivitiesHouse> GetHouseListNEW(int premisesId, int buildingid, int unitId, int floor)
        {
            return _premisesDal.GetHouseListNEW(premisesId, buildingid, unitId, floor);
        }
        public List<TXOrm.House> GetHouseListbypremisesId(int premisesId)
        {
            return _premisesDal.GetHouseListbypremisesId(premisesId);
        }
        #endregion

        #region 通过楼盘ID获取参加营销活动的房源列表
        /// <summary>
        /// 通过楼盘ID获取参加营销活动的房源列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<PremisesActivitiesHouse> GetActivitiesHouseById(int Id)
        {
            return _premisesDal.GetActivitiesHouseById(Id);
        }
        public List<PremisesActivitiesHouse> newGetActivitiesHouseById(int Id)
        {
            return _premisesDal.newGetActivitiesHouseById(Id);
        }
        public List<PremisesActivitiesHouse> GetActivitiesHouseByIdnew(int Id)
        {
            return _premisesDal.GetActivitiesHouseByIdnew(Id);
        }
        #endregion

        #region 获取可能感兴趣的楼盘列表
        /// <summary>
        /// 获取可能感兴趣的楼盘列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.Premis> GetInterestPremisesList(TXOrm.Premis premises)
        {
            return _premisesDal.GetInterestPremisesList(premises);
        }
        #endregion

        #region 同区域热门楼盘列表
        /// <summary>
        /// 同区域热门楼盘列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<IndexRanking> GetHotPremisesList(IndexRankingConditionInfo conditionInfo)
        {
            try
            {
                string temp = TXCommons.Index.IndexConditionInfo.GetSearchRankingCondiction(conditionInfo);
                var url = TXCommons.MsgQueue.MQHelp.GetLuceneRankingUrlByCityId(conditionInfo.CityID);
                string premises_xml_url = url + "?condition=" + temp;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds.Tables.Count < 2)
                {
                    return null;
                }
                else
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        List<IndexRanking> listform = new List<IndexRanking>();
                        TXCommons.Index.IndexRanking sf = new TXCommons.Index.IndexRanking();
                        //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            sf = new IndexRanking();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //获取属性值需要指定属性名
                                PropertyInfo ProInfo = sf.GetType().GetProperty(dt.Columns[k].ColumnName);
                                ProInfo.SetValue(sf, Convert.ToString(dr[k]), null);
                            }
                            listform.Add(sf);
                        }

                        return listform;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
              
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", conditionInfo), e);
                List<IndexRanking> listform = new List<IndexRanking>();
                return listform;
            }
            //return _premisesDal.GetHotPremisesList(premises);
        }
        #endregion

        #region 同区域最新楼盘
        /// <summary>
        /// 同区域最新楼盘
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<IndexRanking> GetLastPremisesList(TXOrm.Premis premises,string ranking, string conditions, string getdatapagename, int cityId = 253)
        {
            try
            {
                IndexRankingConditionInfo conditionInfo = new IndexRankingConditionInfo();
                //info.ProvinceID = u1[0];
                //info.CityID = u1[1];
                //info.DistrictID = u1[2];
                //info.BusinessID = u1[3];
                //info.PremisesID = u1[4];
                //info.Ranking = u1[5];
                //conditionInfo.ProvinceID = premises.ProvinceId.ToString();
                conditionInfo.CityID = premises.CityId.ToString();
                //conditionInfo.DistrictID = premises.DId.ToString();
                //conditionInfo.BusinessID = premises.BId.ToString();
                //conditionInfo.PremisesID = premises.Id.ToString();
                /////   /// 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
                //conditionInfo.Ranking = ranking;
                string temp = TXCommons.Index.IndexConditionInfo.GetSearchRankingCondiction(conditionInfo);
                var url = TXCommons.MsgQueue.MQHelp.GetLuceneRankingUrlByCityId(conditionInfo.CityID);
                string premises_xml_url = url + "?condition=" + temp;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds.Tables.Count < 2)
                {
                    return null;
                }
                else
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        List<IndexRanking> listform = new List<IndexRanking>();
                        TXCommons.Index.IndexRanking sf = new TXCommons.Index.IndexRanking();
                        //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            sf = new IndexRanking();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //获取属性值需要指定属性名
                                PropertyInfo ProInfo = sf.GetType().GetProperty(dt.Columns[k].ColumnName);
                                ProInfo.SetValue(sf, Convert.ToString(dr[k]), null);
                            }
                            listform.Add(sf);
                        }

                        return listform;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("崔利国A", string.Format("({0},{1})", conditions, getdatapagename), e);
                throw;
            }
            //return _premisesDal.GetLastPremisesList(premises);
        
        }
        #endregion

        #region 同区域最热楼盘排行(详情信息页面)
        /// <summary>
        /// 同区域最热楼盘排行(详情信息页面)
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.Premis> GetHotPremisesListbyDId(TXOrm.Premis premises)
        {
            return _premisesDal.GetHotPremisesListbyDId(premises);

        }
        #endregion

        #region 同价位最热楼盘排行（详情信息页面）
        /// <summary>
        /// 同价位最热楼盘排行（详情信息页面）
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.Premis> GetHotPremisesbyReferencePrice(TXOrm.Premis premises)
        {
            return _premisesDal.GetHotPremisesbyReferencePrice(premises);

        }
        #endregion

        #region 获取楼盘下的楼栋列表
        /// <summary>
        /// 获取楼盘下的楼栋列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.Building> GetBuildingListbyPremisesId(TXOrm.Premis premises)
        {
            return _premisesDal.GetBuildingListbyPremisesId(premises);
        }
        #endregion

        #region 获取楼栋下的单元信息列表
       /// <summary>
        /// 获取楼栋下的单元信息列表
       /// </summary>
       /// <param name="buildingId"></param>
       /// <returns></returns>
        public List<TXOrm.Unit> GetUnitListbyBuildingId(int buildingId)
        {
            return _premisesDal.GetUnitListbyBuildingId(buildingId);
        }
        #endregion

        #region 获取楼栋下的单元信息列表(新房前台楼盘房源页面使用)
        /// <summary>
        /// 获取楼栋下的单元信息列表(新房前台楼盘房源页面使用)
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<TXOrm.Unit> GetUnitListbyBuildingId(int premisesid, int buildingId, int unitId, string strwhere)
        {
            return _premisesDal.GetUnitListbyBuildingId(premisesid, buildingId, unitId, strwhere);
        }
        #endregion

        #region 获取楼栋下的某个单元下的房源统计
     /// <summary>
        /// 获取楼栋下的某个单元下的房源统计
     /// </summary>
     /// <param name="premisesId"></param>
     /// <param name="buildingId"></param>
     /// <param name="unitId"></param>
     /// <returns></returns>
        public List<TXModel.Web.HouseFloorCount> GetHouseFloorList(int premisesId, int buildingId, int unitId)
        {
            return _premisesDal.GetHouseFloorList(premisesId, buildingId, unitId);
        }

        public List<TXModel.Web.HouseFloor> GetHouseFloorInfoList(int premisesId, int buildingId, int floor, string strwhereFloorInfo)
        {
            return _premisesDal.GetHouseFloorInfoList(premisesId, buildingId,floor,  strwhereFloorInfo);
        }

        public int GetHousecountbyunit(int premisesId, int buildingId, int unitId, int floor)
        {
            return _premisesDal.GetHousecountbyunit(premisesId, buildingId, unitId, floor);
        }
        #endregion

        #region 获取楼盘的区域参考均价
        /// <summary>
        /// 获取楼盘的区域参考均价
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public decimal GetAvgReferencePrice(TXOrm.Premis premises)
        {
            return _premisesDal.GetAvgReferencePrice(premises);
        }
        #endregion

        #region  获取楼盘的下所有在售楼栋装修情况
        /// <summary>
        /// 获取楼盘的下所有在售楼栋装修情况
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<int> GetRenovationInfobyPremisesId(TXOrm.Premis premises)
        {
            return _premisesDal.GetRenovationInfobyPremisesId(premises);
        }
        #endregion

        #region 楼盘对比
        /// <summary>
        /// Author：崔利国
        /// Time：2013-11-28
        /// 根据楼盘Id（一个或多个）获取楼盘集合信息
        /// </summary>
        /// <param name="IdList"></param>
        /// <returns></returns>
        public List<TXModel.Web.PremisesCompare> GetPremisesCompareByIdList(List<string> IdList)
        {
            return _premisesDal.GetPremisesCompareByIdList(IdList);
        }

        /// <summary>
        /// Author：崔利国
        /// Time：2013-11-28
        /// 根据楼盘名称获取楼盘信息
        /// </summary>
        /// <param name="premisesName"></param>
        /// <returns></returns>
        public TXModel.Web.PremisesCompare GetPremisesByName(string premisesName)
        {
            return _premisesDal.GetPremisesByName(premisesName);
        }
        #endregion

        #region 获取房间信息
        public PremisesHouse GetPremisesHouseById(int id)
        {
            return _premisesDal.GetPremisesHouseById(id);
        }
        #endregion

        #region 根据活动ID获取活动信息
        /// <summary>
        /// 根据活动ID获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.Activity GetHouseActivityById(int id)
        {
            return _premisesDal.GetHouseActivityById(id);
        }
        #endregion 

        #region  获取某个楼盘下户型信息(新房前台楼盘主页用)

        #region 获取某个楼盘下面的所有房源的户型基本信息
       /// <summary>
        /// 获取某个楼盘下面的所有房源的户型基本信息
       /// </summary>
       /// <param name="premises"></param>
       /// <returns></returns>
        public List<TXModel.Web.PremisesHouseHuX> GetPremisesHouseHuXList(TXOrm.Premis premises)
        {
            return _premisesDal.GetPremisesHouseHuXList(premises);
        }

        public List<TXModel.Web.PremisesHouseHuX> GetPremisesHouseHuXList(TXOrm.Premis premises, int buildingId)
        {
            return _premisesDal.GetPremisesHouseHuXList(premises, buildingId);
        }

        public List<TXModel.Web.BuildingHouseHuX> GetPremisesHouseHuXListNew(TXOrm.Premis premises, int buildingId)
        {
            return _premisesDal.GetPremisesHouseHuXListNew(premises, buildingId);
        }

        #endregion 

        #region  获取某个楼盘一共有多少种基本户型
        /// <summary>
        /// 获取某个楼盘一共有多少种基本户型
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<int> GetHouseRoomInfoList(TXOrm.Premis premises)
        {
            return _premisesDal.GetHouseRoomInfoList(premises);
        }
        public List<int> GetHouseRoomInfoList(TXOrm.Premis premises, int BuildingId)
        {
            return _premisesDal.GetHouseRoomInfoList(premises,  BuildingId);
        }
        #endregion

        #region  获取某个楼盘某种户型的房源一共多少套
        /// <summary>
        /// 获取某个楼盘某种户型的房源一共多少套
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public int GetHouseRoomCount(TXOrm.Premis premises, int room)
        {
            return _premisesDal.GetHouseRoomCount(premises, room);
        }
        #endregion

        #endregion

        #region 获取某个楼盘的预售许可证列表
        /// <summary>
        /// 获取某个楼盘的预售许可证列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.PermitPresale> GetPermitPresaleList(TXOrm.Premis premises)
        {
            return _premisesDal.GetPermitPresaleList(premises);
        }
        #endregion

        #region 获取某个楼盘的楼盘特色信息
        /// <summary>
        /// 获取某个楼盘的楼盘特色信息
      /// </summary>
      /// <param name="where"></param>
      /// <returns></returns>
        public List<TXOrm.PremisesFeature> GetPremisesFeatureList(string where)
        {
            return _premisesDal.GetPremisesFeatureList(where);
        }
        #endregion

        #region 获取某个楼盘下的单个楼栋下的所有房源的平均价格
        /// <summary>
        /// 获取某个楼盘下的单个楼栋下的所有房源的平均价格
       /// </summary>
       /// <param name="premises"></param>
       /// <param name="buildingId"></param>
       /// <returns></returns>
        public decimal GetHouseSinglePrice(TXOrm.Premis premises, int buildingId)
        {
            return _premisesDal.GetHouseSinglePrice(premises, buildingId);
        }
        #endregion

        #region 根据活动ID和房源ID获取出价信息
        /// <summary>
        /// 根据活动ID和房源ID获取出价信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<TXOrm.BidPrice> GetBidPriceList(int activityId, int houseId)
        {
            return _premisesDal.GetBidPriceList(activityId, houseId);
        }

        /// <summary>
        /// 获取活动房源上次出价
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public TXOrm.BidPrice GetLastBidPrice(int activityId, int houseId)
        {
            return _premisesDal.GetLastBidPrice(activityId, houseId);
        }

        #endregion

        //#region 根据活动ID和房源ID获取出价信息
        ///// <summary>
        ///// 根据活动ID获取参与活动的用户信息
        ///// </summary>
        ///// <param name="activityId"></param>
        ///// <param name="houseId"></param>
        ///// <returns></returns>
        //public List<TXOrm.ActivitiesUser> GetActivityUserList(int activityId)
        //{
        //    return _premisesDal.GetActivityUserList(activityId);
        //}
        //#endregion

        #region 通过楼盘ID集合获取楼盘基本信息
        /// <summary>
        /// 通过楼盘ID获取楼盘基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<RecentlyViewed> GetPremisesbyIds(string Id)
        {
            return _premisesDal.GetPremisesbyIds(Id);
        }
        #endregion

        #region 根据活动ID和房源ID获取出价信息
        /// <summary>
        /// 根据活动ID和房源ID获取出价信息(分页)
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<TXOrm.BidPrice> GetBidPriceList(int activityId, int houseId, int pageindex, int pagesize, out int totalcount,int type,int userId)
        {
            return _premisesDal.GetBidPriceList(activityId, houseId, pageindex, pagesize, out totalcount,type,userId);
        }
        #endregion

        #region 通过楼盘ID获取楼盘扩展表基本信息
        /// <summary>
        /// 通过楼盘ID获取楼盘扩展表基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TXOrm.PremisesOther GetPremisesOthersbyId(int Id)
        {
            return _premisesDal.GetPremisesOthersbyId(Id);
        }
        #endregion

        #region 关键字匹配楼盘

        /// <summary>
        /// 关键字匹配楼盘
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<TXOrm.Premis> SearchPremisesByName(string name)
        {
            return dal.SearchPremisesByName(name);
        }

        #endregion

        #region 获取楼盘沙盘标注
        /// <summary>
        /// 
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<TXOrm.SandTableLabel> GetSandTableLabelList(int premisesId)
        {
            return _premisesDal.GetSandTableLabelList(premisesId);
        }
        #endregion

        #region 附近楼盘
        /// <summary>
        /// 附近楼盘 
        /// </summary>
        /// <param name="Lng">经度</param>
        /// <param name="Lat">纬度</param>
        /// <param name="km">公里</param>
        /// <returns></returns>
        public List<TXOrm.Premis> GetNearbyPremises(string Lng, string Lat, string km)
        {
            return _premisesDal.GetNearbyPremises(Lng,Lat,km);
        }

        #endregion

        #endregion
    }
}