using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXOrm;
using TXModel.User;
using TXModel.Commons;
using System.Data.SqlClient;
namespace TXDal.User
{
    /// <summary>
    /// 我的新房
    /// author:汪敏
    /// </summary>
    public partial class CT_BidDal
    {
        /// <summary>
        /// 获取我参与的竞购
        /// author:汪敏
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">活动类型</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<CT_Bid> GetMyBidList(int userId, int type, Paging<TXModel.User.CT_Bid> paging)
        {
            List<CT_Bid> result = null;
            int resultCount = 0;
            #region 拼接分页SQL
            string sql = string.Empty;
            if (type == 0)
            {
                sql = @"SELECT * from(select ROW_NUMBER() OVER(ORDER BY o.CreateTime DESC) AS Row,
                                    house.id as HouseId,--房源id
                                    house.room as Room,--室
                                    house.[Name] as [Name],--房号
                                    house.[floor] as [Floor],--楼层
                                    house.hall as Hall,--厅
                                    house.TotalPrice as Marketprice,--市场价
                                    house.buildingArea as BuildingArea,--建筑面积
                                    house.salesStatus as HouseState,--房源状态
                                    premises.[Name] as PremisesName,--楼盘
                                    premises.Innercode as InnerCode,--楼盘效果图
                                    premises.CityId as CityId,--城市Id
                                    premises.PremisesAddress as ActivieAddress,--项目地址
                                    building.[Name] as BuildingName,--楼栋
                                    building.NameType as BuildingNameType,--楼栋类型
                                    ISNULL(building.TheFloor,0) as TheFloor,--总楼层数
                                    unit.[Name] as Unit,--单元
                                    activities.Id as ActivitieId,--活动Id
                                    activities.[Name] as ActivitieName,--活动名称
                                    activities.bidPrice as BidPrice,--活动价格
                                    activities.[Type] as  ActivieType,--活动类型
                                    activities.BeginTime as BeginTime,--活动开始时间
                                    activities.EndTime as EndTime,--活动结束时间
                                    activities.ActivitieState as ActivieStatus,--活动状态
                                    activities.Bond as Bond,--活动保证金
                                    o.Id as OrderId,--订单Id
                                    o.BondStatus as BondState,--保证金状态
                                    o.CreateTime as BidTime, --参与活动时间
                                    o.Status as BidPriceState,--出价状态(0未成交,1已成交,2已中标)
                                    (select count(1) from dbo.BidPrice as b with(nolock) where  b.ActivitiesId=activities.id and b.isdel=0) AS BidCount, --出价次数
                                    (select count(T.row) from(select count(1) as row from dbo.BidPrice as b with(nolock) where  b.ActivitiesId=activities.id and b.isdel=0  group by b.BidUserId ) as T) AS UserCount--参与人数
                                    from (select id,premisesid,buildingid,unitid,room,[name],[floor],hall,TotalPrice,buildingArea,salesStatus,innerCode from dbo.House with(nolock)
                                    where id in(
                                    select HouseId from  [Order] with(nolock) where BidUserId=@userId and isdel=0) and isdel=0) as house 
                                     join Premises as premises on premises.id=house.premisesid and premises.isdel=0  
                                     join Building as building on building.id=house.buildingid and building.isdel=0
                                     join Unit as unit on unit.num=house.unitid and unit.BuildingId=house.BuildingId and unit.isdel=0
                                    inner join [Order] as  o on o.HouseId=house.id and o.BidUserId=@userId and o.isdel=0
                                     join Activities as activities  on activities.id=o.ActivitiesId and activities.isdel=0) as temp where Row BETWEEN @pageBegin AND @pageEnd";
            }
            else
            {
                sql = @"SELECT * from (select  ROW_NUMBER() OVER(ORDER BY o.CreateTime DESC) AS Row,
                                    house.id as HouseId,--房源id
                                    house.room as Room,--室
                                    house.[Name] as [Name],--房号
                                    house.[floor] as [Floor],--楼层
                                    house.hall as Hall,--厅
                                    house.TotalPrice as Marketprice,--市场价
                                    house.buildingArea as BuildingArea,--建筑面积
                                    house.salesStatus as HouseState,--房源状态
                                    premises.[Name] as PremisesName,--楼盘
                                    premises.Innercode as InnerCode,--楼盘效果图
                                    premises.CityId as CityId,--城市Id
                                    premises.PremisesAddress as ActivieAddress,--项目地址
                                    building.[Name] as BuildingName,--楼栋
                                    building.NameType as BuildingNameType,--楼栋类型
                                    ISNULL(building.TheFloor,0) as TheFloor,--总楼层数
                                    unit.[Name] as Unit,--单元
                                    activities.Id as ActivitieId,--活动Id
                                    activities.[Name] as ActivitieName,--活动名称
                                    activities.bidPrice as BidPrice,--活动价格
                                    activities.[Type] as  ActivieType,--活动类型
                                    activities.BeginTime as BeginTime,--活动开始时间
                                    activities.EndTime as EndTime,--活动结束时间
                                    activities.ActivitieState as ActivieStatus,--活动状态
                                    activities.Bond as Bond,--活动保证金
                                    o.Id as OrderId,--订单Id
                                    o.BondStatus as BondState,--保证金状态
                                    o.CreateTime as BidTime, --参与活动时间
                                    o.Status as BidPriceState,--出价状态(0未成交,1已成交,2已中标)
                                    (select count(1) from dbo.BidPrice as b with(nolock) where  b.ActivitiesId=activities.id and b.isdel=0) AS BidCount, --出价次数
                                    (select count(T.row) from(select count(1) as row from dbo.BidPrice as b with(nolock) where  b.ActivitiesId=activities.id and b.isdel=0  group by b.BidUserId ) as T) AS UserCount--参与人数
                                    from (select id,premisesid,buildingid,unitid,room,[name],[floor],hall,TotalPrice,buildingArea,salesStatus,innerCode from dbo.House with(nolock)
                                    where  id in(
                                    select HouseId from  [Order] with(nolock) where BidUserId=@userId and isdel=0) and isdel=0) as house  
                                     join Premises as premises on premises.id=house.premisesid and premises.isdel=0 
                                     join Building as building on building.id=house.buildingid and building.isdel=0
                                     join Unit as unit on unit.num=house.unitid and unit.BuildingId=house.BuildingId and unit.isdel=0
                                    inner join [Order] as  o on o.HouseId=house.id and o.BidUserId=@userId and o.isdel=0
                                     join Activities as activities  on activities.id=o.ActivitiesId where activities.isdel=0 and activities.Type=@type) as temp where temp.ActivieType=@type and Row BETWEEN @pageBegin AND @pageEnd";
            }
            #endregion

            #region 拼接总记录SQL
            //总记录SQL
            string sqlCount = String.Empty;
            if (type == 0)
            {
                sqlCount = @"SELECT COUNT(1) FROM  [Order] WITH(NOLOCK),House WITH(NOLOCK) WHERE [Order].HouseId=House.Id and House.IsDel=0 and  [Order].IsDel = 0  AND [Order].BidUserId = @userId ";
            }
            else
            {
                sqlCount = @"SELECT COUNT(1) FROM [Order] WITH(NOLOCK),Activities WITH(NOLOCK),House WITH(NOLOCK)  WHERE [Order].HouseId=House.Id and House.IsDel=0 and  [Order].ActivitiesId=Activities.Id and Activities.IsDel=0 And [Order].IsDel = 0 And  Activities.Type=@type AND [Order].BidUserId = @userId ";
            }
            #endregion

            #region 设置分页参数
            //分页参数
            SqlParameter[] para = new SqlParameter[] { 
                            new SqlParameter("@pageBegin",((paging.PageIndex - 1) * paging.PageSize) + 1),
                            new SqlParameter("@pageEnd",paging.PageIndex * paging.PageSize),
                            new SqlParameter("@type",type),
                            new SqlParameter("@userId",userId)
                    };
            #endregion

            #region 设置总记录参数
            //总记录参数
            SqlParameter[] paraCount = new SqlParameter[] { 
                         new SqlParameter("@userId",userId),
                         new SqlParameter("@type",type)
            };
            #endregion

            try
            {
                using (var newHouseEntity = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    //查询分页
                    var query = newHouseEntity.ExecuteStoreQuery<CT_Bid>(sql, para);
                    result = query.ToList();
                    #endregion

                    #region 查询总记录数据
                    var queryCount = newHouseEntity.ExecuteStoreQuery<int>(sqlCount, paraCount);
                    resultCount = queryCount.FirstOrDefault<int>();

                    #endregion

                    //List<CT_Bid> list = new List<CT_Bid>();
                    //var query = newHouseEntity.ExecuteStoreQuery<CT_Bid>(sql).ToList();
                    //if (query != null && query.Count() > 0)
                    //{
                    //    ///活动房源基本信息
                    //    foreach (var item in query)
                    //    {
                    //        CT_Bid Bidhouse = new CT_Bid();
                    //        Bidhouse.HouseId = item.HouseId;
                    //        Bidhouse.ActivitieId = item.ActivitieId;
                    //        Bidhouse.CityId = Convert.ToInt32(item.CityId);
                    //        Bidhouse.Hall = item.Hall;
                    //        Bidhouse.Floor = item.Floor;
                    //        Bidhouse.TheFloor = Convert.ToInt32(item.TheFloor);
                    //        Bidhouse.Room = item.Room;
                    //        Bidhouse.Name = item.Name;
                    //        Bidhouse.ActivieStatus = Convert.ToInt32(item.ActivieStatus);
                    //        Bidhouse.ActivitieName = item.ActivitieName;
                    //        Bidhouse.ActivieType = Convert.ToInt32(item.ActivieType);
                    //        Bidhouse.BeginTime = Convert.ToDateTime(item.BeginTime);
                    //        Bidhouse.BidPriceState = Convert.ToInt32(item.BidPriceState);
                    //        Bidhouse.BidTime = Convert.ToDateTime(item.BidTime);
                    //        Bidhouse.Bond = item.Bond;
                    //        Bidhouse.BondState = item.BondState;
                    //        Bidhouse.BuildingArea = item.BuildingArea;
                    //        Bidhouse.BuildingName = item.BuildingName;
                    //        Bidhouse.EndTime = Convert.ToDateTime(item.EndTime);
                    //        Bidhouse.OrderId = item.OrderId;
                    //        Bidhouse.HouseState = item.HouseState;
                    //        Bidhouse.InnerCode = item.InnerCode;
                    //        Bidhouse.Marketprice = item.Marketprice;
                    //        Bidhouse.PremisesName = item.PremisesName;
                    //        Bidhouse.Unit = item.Unit;
                    //        Bidhouse.BidPrice = Convert.ToDecimal(item.BidPrice);
                    //        Bidhouse.ActivieAddress = item.ActivieAddress;
                    //        //欠缺字段
                    //        Bidhouse.Houseploidy = 0;
                    //        Bidhouse.Discount = 0;
                    //        Bidhouse.BidCount = 0;
                    //        Bidhouse.HouseName = "";
                    //        Bidhouse.Clicks = 0;
                    //        Bidhouse.UserCount = 0;
                    //        list.Add(Bidhouse);
                    //    }
                    //    if (type > 0)
                    //    {
                    //        list = list.Where(p => p.ActivieType == type).ToList();
                    //    }
                    //    paging.TotalCount = list.Count();
                    //    paging.ResultData = list.OrderByDescending(q => q.BidTime).Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                    //}
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", string.Format("UserId:{0},type:{1},paging:{2}", userId, type, paging), e);
                //throw;
            }
            paging.ResultData = result;
            paging.TotalCount = resultCount;
            return paging;
        }

        /// <summary>
        /// 获取我参与的网络摇号
        /// author:汪敏
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<CT_Yaohao> GetMyYaohaoList(int UserId, int pageindex, int pagesize, out int totalcount)
        {
            string sql = string.Empty;
            sql = String.Format(@"select 
                                    yaohao.ActivitiesId as ActivitieId,--活动id
                                    yaohao.ActivitieLocation as ActivitieLocation,--摇号地点
                                    yaohao.NotarialOffice as NotarialOffice,--公证处
                                    yaohao.ChooseHouseTime as ChooseHouseTime,--选房时间
                                    activities.SignupBeginTime as  SignupBeginTime,--报名开始时间
                                    activities.SignupEndTime as SignupEndTime,--报名结束时间
                                    activities.UserCount as UserCount,--参与人数
                                    activities.BeginTime as BeginTime,--活动开始时间
                                    activities.ActivitieState as ActivieStatus,--活动状态
                                    activities.Bond as Bond--保证金
                                    from
                                    (select ActivitiesId,Periods,ActivitieLocation,NotarialOffice,ChooseHouseTime,Introduction from ActivitiesYaoHaoInfos with(nolock)
                                    where ActivitiesId in (select ActivitiesId from BidPrice with(nolock) where BidUserId={0})) as yaohao
                                    left join Activities as activities on activities.Id=yaohao.ActivitiesId and activities.isdel=0", UserId);

            try
            {
                using (var newHouseEntity = new kyj_NewHouseDBEntities())
                {
                    List<CT_Yaohao> list = new List<CT_Yaohao>();
                    var query = newHouseEntity.ExecuteStoreQuery<CT_Yaohao>(sql).ToList();
                    if (query != null && query.Count() > 0)
                    {
                        ///摇号基本信息
                        foreach (var item in query)
                        {
                            CT_Yaohao yaohao = new CT_Yaohao();
                            yaohao.ActivieStatus = Convert.ToInt32(item.ActivieStatus);
                            yaohao.ActivitieId = item.ActivitieId;
                            yaohao.ActivitieLocation = item.ActivitieLocation;
                            yaohao.NotarialOffice = item.NotarialOffice;
                            yaohao.BeginTime = Convert.ToDateTime(item.BeginTime);
                            yaohao.ChooseHouseTime = item.ChooseHouseTime;
                            yaohao.SignupBeginTime = Convert.ToDateTime(item.SignupBeginTime);
                            yaohao.SignupEndTime = Convert.ToDateTime(item.SignupEndTime);
                            yaohao.UserCount = Convert.ToInt32(item.UserCount);
                            yaohao.Bond = Convert.ToDecimal(item.Bond);
                            //欠缺字段(备选套数)
                            yaohao.PremisesName = "";
                            list.Add(yaohao);
                        }
                        totalcount = list.Count;
                        return list.OrderByDescending(it => it.BeginTime).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", string.Format("UserId:{0},pageindex:{1},pagesize:{2}", UserId, pageindex, pagesize), e);
                //throw;
            }
            totalcount = 0;
            return null;
        }

        /// <summary>
        /// 获取我对某个房源的出价记录
        /// author:汪敏
        /// </summary>
        /// <param name="houseid">房源Id</param>
        /// <param name="userid">用户Id</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<TXOrm.BidPrice> GetMyBidRecord(int houseid, int userid, Paging<TXOrm.BidPrice> paging)
        {
            List<TXOrm.BidPrice> result = null;
            int resultCount = 0;
            #region 拼接分页SQL
            string sql = string.Empty;
            sql = @"SELECT * from(select ROW_NUMBER() OVER(ORDER BY id ASC) AS Row,* from BidPrice WITH(NOLOCK)  where HouseId=@houseid and BidUserId=@userId and isdel=0) as temp where Row BETWEEN @pageBegin AND @pageEnd";
            #endregion

            #region 拼接总记录SQL
            //总记录SQL
            string sqlCount = String.Empty;
            sqlCount = @"SELECT COUNT(1) FROM BidPrice  WITH(NOLOCK) WHERE IsDel = 0 AND HouseId=@houseid AND BidUserId = @userId ";
            #endregion

            #region 设置分页参数
            //分页参数
            SqlParameter[] para = new SqlParameter[] { 
                            new SqlParameter("@pageBegin",((paging.PageIndex - 1) * paging.PageSize) + 1),
                            new SqlParameter("@pageEnd",paging.PageIndex * paging.PageSize),
                            new SqlParameter("@houseid",houseid),
                            new SqlParameter("@userId",userid)
                    };
            #endregion

            #region 设置总记录参数
            //总记录参数
            SqlParameter[] paraCount = new SqlParameter[] { 
                        new SqlParameter("@houseid",houseid),
                            new SqlParameter("@userId",userid)
            };
            #endregion
            try
            {
                using (var newHouseEntity = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    //查询分页
                    var query = newHouseEntity.ExecuteStoreQuery<TXOrm.BidPrice>(sql, para);
                    result = query.ToList();
                    #endregion

                    #region 查询总记录数据
                    var queryCount = newHouseEntity.ExecuteStoreQuery<int>(sqlCount, paraCount);
                    resultCount = queryCount.FirstOrDefault<int>();
                    #endregion
                    //该房源所有出价金额
                    var temp = (from a in newHouseEntity.BidPrices where a.HouseId == houseid select a).Distinct().OrderByDescending(it => it.BidUserPrice).Select(n => n.BidUserPrice).ToList();
                    if (result != null && result.Count() > 0)
                    {
                        foreach (var item in result)
                        {
                            //排名
                            item.Id = temp.IndexOf(item.BidUserPrice) + 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", string.Format("houseid:{0},userid:{1},pageindex:{2},pagesize:{3}", houseid, userid, paging.PageIndex, paging.PageSize), e);
                //throw;
            }
            paging.ResultData = result.OrderByDescending(p => p.CreateTime).ToList();
            paging.TotalCount = resultCount;
            return paging;
            //List<TXOrm.BidPrice> list = new List<BidPrice>();
            //try
            //{
            //    using (var newHouseEntity = new kyj_NewHouseDBEntities())
            //    {
            //        //该房源所有出价金额
            //        var temp = (from a in newHouseEntity.BidPrices where a.HouseId == houseid select a).Distinct().OrderByDescending(it => it.BidUserPrice).Select(n => n.BidUserPrice).ToList();
            //        //用户出价记录
            //        var query = from bid in newHouseEntity.BidPrices
            //                    where bid.HouseId == houseid && bid.BidUserId == userid && bid.IsDel == false
            //                    select new
            //                    {
            //                        BidUserPrice = bid.BidUserPrice,
            //                        CreateTime = bid.CreateTime,
            //                        Status = bid.Status,
            //                    };
            //        if (query != null && query.Count() > 0)
            //        {
            //            foreach (var item in query)
            //            {
            //                TXOrm.BidPrice bid = new BidPrice();
            //                bid.BidUserPrice = item.BidUserPrice;
            //                bid.CreateTime = item.CreateTime;
            //                bid.Status = item.Status;
            //                //排名
            //                bid.Id = temp.IndexOf(item.BidUserPrice) + 1;
            //                list.Add(bid);
            //            }
            //            paging.TotalCount = list.Count();
            //            paging.ResultData = list.OrderByDescending(q => q.CreateTime).Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Log4netService.RecordLog.RecordException("汪敏", string.Format("houseid:{0},userid:{1},pageindex:{2},pagesize:{3}", houseid, userid, paging.PageIndex, paging.PageSize), e);
            //    //throw;
            //}
            //return paging;
        }

        /// <summary>
        /// 获取用户收藏的楼盘
        /// author:汪敏
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public Paging<CT_FavoritePrem> GetMyPremisesFavorite(string pids, Paging<CT_FavoritePrem> paging)
        {
            List<CT_FavoritePrem> result = new List<CT_FavoritePrem>();
            int resultCount = 0;
            string sql = string.Empty;
            string ids = string.Empty;
            #region 拼接分页sql
            sql = @"create table #temp1
                    (
                    Id int,
                    CreateTime datetime
                    );";
            string[] arr = pids.Split(',');
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var item = arr[i].Split('|');
                sql += @" insert into #temp1(
                            Id ,
                            CreateTime
                            )values (" + item[0] + ",'" + item[1] + "'); ";

                ids += item[0] + ",";
            }

            sql += @"select * from (select ROW_NUMBER() OVER(ORDER BY temp0.CreateTime DESC) AS Row,
                            premis.Id as PremisId,
                            premis.InnerCode as InnerCode,
                            premis.[Name] as [Name],
                            premis.Ring as Ring,
                            premis.BName as BName,
                            premis.salesAddress as salesAddress,
                            premis.SalesStatus as SalesStatus,
                            premis.Developer as Developer,
                            premis.PropertyType as PropertyType,
                            premis.ReferencePrice as ReferencePrice,
                            premis.TelePhone as TelePhone,
                            premis.Lng as Lng,
                            premis.Lat as Lat,
                            premis.CityId as CityId,
                            premis.Characteristic as Features
                            from Premises as premis with(nolock)
                            
                            inner join #temp1 as temp0 on temp0.Id = premis.Id
                    
    where premis.IsDel = 0  AND premis.Id in (" + ids.Trim(',') + ")) as temp where Row BETWEEN @pageBegin AND @pageEnd ";




            #endregion

            #region 拼接总记录SQL
            //总记录SQL
            string sqlCount = String.Empty;
            sqlCount = @"SELECT COUNT(1) FROM  Premises WITH(NOLOCK) WHERE IsDel = 0  AND Premises.Id in (" + pids.Trim(',') + ") ";

            #endregion

            #region 设置参数
            //分页参数
            SqlParameter[] para = new SqlParameter[] { 
                            new SqlParameter("@pageBegin",((paging.PageIndex - 1) * paging.PageSize) + 1),
                            new SqlParameter("@pageEnd",paging.PageIndex * paging.PageSize)
                    };
            #endregion
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    //查询分页
                    var query = db.ExecuteStoreQuery<CT_FavoritePrem>(sql, para);
                    result = query.ToList();
                    #endregion

                    #region 查询总记录数据
                    var queryCount = db.ExecuteStoreQuery<int>(sqlCount);
                    resultCount = queryCount.FirstOrDefault<int>();
                    #endregion

                    foreach (var item in result)
                    {
                        List<RoomCount> rclist = db.ExecuteStoreQuery<RoomCount>("select temp.Room as Room,count(*) as [Count] from (select Room,BuildingArea from dbo.House with(nolock) where PremisesId=" + item.PremisId + " group by Room,BuildingArea) as temp group by temp.Room").ToList();
                        // 户型图
                        if (rclist.Count > 0)
                        {
                            foreach (var s in rclist.ToList())
                            {
                                item.Rooms += "<a href=\"" + TXCommons.GetConfig.BaseUrl + "xinfang/lp-hxt-" + item.PremisId + "-r" + s.Room + "-l1\" class=\"linkD mr5\" target=\"_blank\">" + s.Room + "居(" + s.Count + ")</a>";
                            }
                        }
                        else
                        {
                            item.Rooms = "";
                        }
                        //特色
                        if (item.Features != null)
                        {
                            string[] features = item.Features.Split(',');
                            if (features.Length > 0)
                            {
                                StringBuilder str = new StringBuilder();
                                for (int m = 0; m < features.Length; m++)
                                {
                                    switch (features[m])
                                    {
                                        case "8":
                                            str.Append("<span class=\"bg_85adff\">学区房</span>");
                                            break;
                                        case "9":
                                            str.Append("<span class=\"bg_ea8985\">特价房</span>");
                                            break;
                                        case "10":
                                            str.Append("<span class=\"bg_a6c45d\">养老房</span>");
                                            break;
                                    }
                                }
                                item.Features = str.ToString();
                            }
                        }
                        item.PropertyType = ConverPropertyType(item.PropertyType);
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", string.Format("pids:{0},pagine:{1}", pids, paging), e);
                //throw;
            }
            paging.ResultData = result;
            paging.TotalCount = resultCount;
            return paging;
        }

        /// <summary>
        /// 获取楼盘预览房源信息
        /// author:汪敏
        /// </summary>
        /// <param name="PremisId">楼盘Id</param>
        /// <returns></returns>
        public List<CT_Bid> GetPreviewHouseByPremisId(int PremisId)
        {
            List<CT_Bid> list = new List<CT_Bid>();
            try
            {
                using (var newHouseEntity = new kyj_NewHouseDBEntities())
                {
                    var query = (from house in newHouseEntity.Houses
                                 join p in newHouseEntity.Premises
                                 on house.PremisesId equals p.Id
                                 into hp
                                 from h in hp.DefaultIfEmpty()
                                 join b in newHouseEntity.Buildings
                                 on house.BuildingId equals b.Id
                                 into hb
                                 from m in hb.DefaultIfEmpty()
                                 join u in newHouseEntity.Units
                                 on house.UnitId equals u.Num
                                 into hu
                                 from gg in hu.DefaultIfEmpty()
                                 where house.PremisesId == PremisId && house.IsDel == false && gg.BuildingId == house.BuildingId
                                 select new
                                 {
                                     Id = house.Id,
                                     Hall = house.Hall,
                                     Floor = house.Floor,
                                     TheFloor = m.TheFloor,
                                     Room = house.Room,
                                     Name = house.Name,
                                     BuildingArea = house.BuildingArea,
                                     TotalPrice = house.TotalPrice,
                                     InnerCode = house.InnerCode,
                                     PremisName = h.Name,
                                     BuildingName = m.Name + m.NameType,
                                     UnitName = gg.Name
                                 });
                    if (query.Count() > 0)
                    {
                        if (query.Count() >= 2)
                        {
                            query = query.Take(2);
                        }
                        foreach (var item in query.ToList())
                        {
                            CT_Bid house = new CT_Bid();
                            house.HouseId = item.Id;
                            house.InnerCode = item.InnerCode;
                            house.Hall = item.Hall;
                            house.Floor = item.Floor;
                            house.TheFloor = item.TheFloor;
                            house.Room = item.Room;
                            house.BuildingArea = item.BuildingArea;
                            house.Name = item.Name;
                            house.BidPrice = item.TotalPrice;
                            house.PremisesName = item.PremisName;
                            house.BuildingName = item.BuildingName;
                            house.Unit = item.UnitName;
                            list.Add(house);
                        }
                        return list.Take(2).ToList();
                    }

                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", string.Format("PremisId:{0}", PremisId), e);
                //throw;
            }
            return list;
        }

        /// <summary>
        /// 取消收藏
        /// author:汪敏
        /// </summary>
        /// <param name="pid">楼盘id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        /// 
        public int Delete(int pid, int uid)
        {
            int result = 0;
            try
            {
                using (var newHouseEntity = new kyj_NewHouseDBEntities())
                {
                    var linq = (from a in newHouseEntity.PremisesFavorites where a.PremisesId == pid && a.UserId == uid select a).First();
                    newHouseEntity.PremisesFavorites.DeleteObject(linq);
                    newHouseEntity.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", string.Format("pid:{0},uid:{1}", pid, uid), e);
                //throw;
            }
            return result;

        }

        /// <summary>
        /// 物业类型转换
        /// author:汪敏
        /// </summary>
        /// <param name="pt">物业类型</param>
        /// <returns></returns>
        public string ConverPropertyType(string pt)
        {
            StringBuilder result = new StringBuilder();
            string[] temp = pt.Split(',');
            if (temp.Length > 1)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    switch (temp[i])
                    {
                        case "1":
                            result.Append("住宅,");
                            break;
                        case "2":
                            result.Append("写字楼,");
                            break;
                        case "3":
                            result.Append("别墅,");
                            break;
                        case "4":
                            result.Append("商业,");
                            break;
                    }
                }
            }
            else
            {
                switch (pt)
                {
                    case "1":
                        result.Append("住宅");
                        break;
                    case "2":
                        result.Append("写字楼");
                        break;
                    case "3":
                        result.Append("别墅");
                        break;
                    case "4":
                        result.Append("商业");
                        break;
                }
            }
            return result.ToString().TrimEnd(',');
        }
    }
    public class RoomCount
    {
        public int Room { get; set; }
        public int Count { get; set; }
    }
}
