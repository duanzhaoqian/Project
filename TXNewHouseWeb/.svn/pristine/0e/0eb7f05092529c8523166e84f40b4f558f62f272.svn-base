using System;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Collections.Generic;
using TXModel.Web;
using TXOrm;
namespace TXDal.HouseData
{
    /// <summary>
    /// 楼盘DAL类-前台
    /// </summary>
    public partial class PremisesDal
    {
        #region  新房前台用到的数据访问层方法

        #region  通过楼盘ID获取楼盘基本信息
        /// <summary>
        /// 通过楼盘ID获取楼盘基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TXOrm.Premis GetPremisesbyId(int Id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT  *
                      from Premises  (NOLOCK)  where 1=1  and IsDel=0  and Id={0}  
                        ", new object[] { Id });
                    #region 执行操作
                    ObjectResult<TXOrm.Premis> query = houseDb.ExecuteStoreQuery<TXOrm.Premis>(sql);
                    TXOrm.Premis ls = query.ToList().FirstOrDefault();
                    #endregion
                    return ls;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TXOrm.Premis> GetPremisesbyName(string Name)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT  *
                      from Premises  (NOLOCK)  where 1=1  and IsDel=0  and Name like '%{0}%'  
                        ", new object[] { Name });
                    #region 执行操作
                    ObjectResult<TXOrm.Premis> query = houseDb.ExecuteStoreQuery<TXOrm.Premis>(sql);
                    List<TXOrm.Premis> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region  通过楼盘ID和楼栋ID获取楼栋基本信息
        /// <summary>
        /// 通过楼盘ID和楼栋ID获取楼栋基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TXOrm.Building GetBuildingInfobyId(int premisesid, int id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Empty;
                    if (id > 0)
                    {
                        sql = string.Format(@"
                         SELECT  *
                        
                      from Building  (NOLOCK)  where 1=1 and Id={0}  and  PremisesId={1} and IsDel<>1 order by  Id  asc
                        ", new object[] { id,
                                          premisesid});
                    }
                    else
                    {

                        sql = string.Format(@"
                          SELECT  *
                      from Building  (NOLOCK)  where 1=1    and  PremisesId={0} and IsDel<>1   order by  Id  asc
                        ", new object[] {  
                                          premisesid});
                    }

                    #region 执行操作
                    ObjectResult<TXOrm.Building> query = houseDb.ExecuteStoreQuery<TXOrm.Building>(sql);
                    TXOrm.Building ls = query.ToList().FirstOrDefault();
                    #endregion
                    return ls;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 获取楼栋基本信息
        /// </summary>
        /// <param name="premisesid"></param>
        /// <param name="PNum"></param>
        /// <returns></returns>
        public TXOrm.Building GetBuildingInfobyPNum(int premisesid, int pnum)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Empty;
                    sql = string.Format(@"
                         SELECT  *
                        
                      from Building  (NOLOCK)  where 1=1 and PNum={0}  and  PremisesId={1}  order by  Id  desc
                        ", new object[] { pnum,
                                          premisesid});
                    #region 执行操作
                    ObjectResult<TXOrm.Building> query = houseDb.ExecuteStoreQuery<TXOrm.Building>(sql);
                    TXOrm.Building ls = query.ToList().FirstOrDefault();
                    #endregion
                    return ls;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region   获取房源列表(新房前台楼盘房源页面使用)
        /// <summary>
        /// 获取房源列表(新房前台楼盘房源页面使用)
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="buildingid"></param>
        /// <param name="unitId"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public List<House> GetHouseList(int premisesId, int buildingid, int unitId, int floor)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                  SELECT  distinct h.*
                                FROM  [House] h,
                                dbo.Building b,dbo.Unit u,
                                dbo.Premises p
                                where 1=1 and h.IsDel=0   and  h.PremisesId=p.id
                                and  h.BuildingId=b.id
                                and h.UnitId=u.Num  and u.PremisesId=h.PremisesId and u.BuildingId=h.BuildingId 
                                and h.IsRelease=1 
                        ");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(sql);
                    if (premisesId > 0)
                    {
                        sb.Append(string.Format("   and h.PremisesId={0}  ", premisesId));
                    }
                    if (buildingid > 0)
                    {
                        sb.Append(string.Format("   and h.BuildingId={0}   ", buildingid));
                    }
                    if (unitId > 0)
                    {
                        sb.Append(string.Format("   and h.UnitId={0}   ", unitId));
                    }
                    if (floor > 0)
                    {
                        sb.Append(string.Format("   and h.Floor={0}   ", floor));
                    }
                    #region 执行操作
                    ObjectResult<House> query = houseDb.ExecuteStoreQuery<House>(sb.ToString());
                    List<House> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premisesId }), ex);
                throw;
            }
        }


        public List<PremisesActivitiesHouse> GetHouseListNEW(int premisesId, int buildingid, int unitId, int floor)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                       SELECT  
                                  H.*
                                  ,A.Type
                           ,(CASE WHEN A.Type = '1' THEN '摇号'      
                                                                WHEN A.Type = '2' THEN '限时折扣'      
                                                                WHEN A.Type = '3' THEN '排号购房'  
                                                                WHEN A.Type = '4' THEN '阶梯团购'  
                                                                WHEN A.Type = '5' THEN '竞价'  
                                                                WHEN A.Type = '6' THEN '砍价'  
                                                                WHEN A.Type = '7' THEN '秒杀'  
                                                                WHEN A.Type = '8' THEN '一口价'  
                                                                ELSE '其他' END ) as    Typedesc  
                        FROM    dbo.House AS H
		                        left JOIN  dbo.ActivitiesHouse AS AH ON AH.HouseId = H.Id
		                        left JOIN dbo.Activities AS A ON AH.ActivitiesId = A.Id
                                left  JOIN dbo.Unit AS U ON H.UnitId = U.Id
                                left JOIN dbo.Building AS B ON AH.BuildingId = B.Id
                                left JOIN dbo.Premises AS P ON AH.PremisesId = P.Id
                        WHERE   AH.IsDel = 0
                                AND H.IsDel = 0
                                AND P.IsDel = 0
                                AND B.IsDel = 0 
                        ");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(sql);
                    if (premisesId > 0)
                    {
                        sb.Append(string.Format("   and H.PremisesId={0}  ", premisesId));
                    }
                    if (buildingid > 0)
                    {
                        sb.Append(string.Format("   and H.BuildingId={0}   ", buildingid));
                    }
                    if (unitId > 0)
                    {
                        sb.Append(string.Format("   and H.UnitId={0}   ", unitId));
                    }
                    if (floor > 0)
                    {
                        sb.Append(string.Format("   and H.Floor={0}   ", floor));
                    }
                    #region 执行操作
                    ObjectResult<PremisesActivitiesHouse> query = houseDb.ExecuteStoreQuery<PremisesActivitiesHouse>(sb.ToString());
                    List<PremisesActivitiesHouse> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premisesId }), ex);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public List<House> GetHouseListbypremisesId(int premisesId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                SELECT h.*
                                FROM  [House] h
                                where 1=1    
                        ");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(sql);
                    if (premisesId > 0)
                    {
                        sb.Append(string.Format("   and h.PremisesId={0}  ", premisesId));
                    }
                    #region 执行操作
                    ObjectResult<House> query = houseDb.ExecuteStoreQuery<House>(sb.ToString());
                    List<House> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premisesId }), ex);
                throw;
            }
        }
        #endregion

        #region 通过楼盘ID获取参加营销活动的房源列表
        public List<PremisesActivitiesHouse> newGetActivitiesHouseById(int Id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT distinct   
                                      ah.[DeveloperId]
                                      ,ah.[PremisesId]
                                      ,ah.[BuildingId]
                                      ,ah.[HouseId]
                                      ,ah.[Discount]
                                      ,ah.[IsDel]
                                      ,p.Name  as  PremisesName-----楼盘名称
	                                    ,b.Name  as BuildingName-----楼栋名称
                                       ,b.NameType  as BuildingNameType-----楼栋名称
                                      ,u.Name  as UnitName-----单元名称
                                      ,a.BidPrice----起价
                                        ,(CASE WHEN a.Type = '1' THEN '摇号'      
                                        WHEN a.Type = '2' THEN '限时折扣'      
                                        WHEN a.Type = '3' THEN '排号购房'  
                                        WHEN a.Type = '4' THEN '阶梯团购'  
                                        WHEN a.Type = '5' THEN '竞价'  
                                        WHEN a.Type = '6' THEN '砍价'  
                                        WHEN a.Type = '7' THEN '秒杀'  
                                        WHEN a.Type = '8' THEN '一口价'  
                                        ELSE '其他' END ) as    Typedesc  
                                        ,a.Type
                                      ,h.[BuildingArea]----------建筑面积
                                      ,h.[Area]-------------使用面积
                                      ,h.[Room]-----------室
                                      ,h.[Hall]------------厅
                                      ,h.[Toilet]-------------卫
                                      ,h.[Kitchen]----------厨房
                                      ,h.Floor 
                                      ,h.TotalPrice---------市场价（总价）
                                      ,b.TheFloor-------地上楼层
                                      ,h.Name as HouseName -------房号
                            from House h ,  [ActivitiesHouse] ah        
                            , dbo.Activities a   
                            ,  dbo.Premises p   
                            , Building b   
                           , dbo.Unit u      
                            where 1=1
								and ah.[HouseId]=h.Id
								and a.Id=ActivitiesId
								and  p.Id=ah.[PremisesId]
								and ah.[BuildingId]=b.Id
								and  h.UnitId=u.Num  
								and u.PremisesId=h.PremisesId and u.BuildingId=h.BuildingId 
                                and ah.IsDel = 0 
                                and h.IsDel = 0 
                                and p.IsDel = 0 
                                and b.IsDel = 0 
                                and p.Id={0}  and  a.BidPrice is not null 
                                and a.EndTime>getdate() and a.ActivitieState<2  and h.SalesStatus=2
                                 ", new object[] { Id });
                    #region 执行操作
                    ObjectResult<PremisesActivitiesHouse> query = houseDb.ExecuteStoreQuery<PremisesActivitiesHouse>(sql);
                    List<PremisesActivitiesHouse> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { Id }), ex);
                throw;
            }
        }
        /// <summary>
        /// 通过楼盘ID获取参加营销活动的房源列表(活动进行中的在售房源)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<PremisesActivitiesHouse> GetActivitiesHouseById(int Id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT distinct top 6   
                                      ah.[DeveloperId]
                                      ,ah.[PremisesId]
                                      ,ah.[BuildingId]
                                      ,ah.[HouseId]
                                      ,ah.[Discount]
                                      ,ah.[IsDel]
                                      ,p.Name  as  PremisesName-----楼盘名称
	                                    ,b.Name  as BuildingName-----楼栋名称
                                       ,b.NameType  as BuildingNameType-----楼栋名称
                                      ,u.Name  as UnitName-----单元名称
                                      ,a.BidPrice----起价
                                        ,(CASE WHEN a.Type = '1' THEN '摇号'      
                                        WHEN a.Type = '2' THEN '限时折扣'      
                                        WHEN a.Type = '3' THEN '排号购房'  
                                        WHEN a.Type = '4' THEN '阶梯团购'  
                                        WHEN a.Type = '5' THEN '竞价'  
                                        WHEN a.Type = '6' THEN '砍价'  
                                        WHEN a.Type = '7' THEN '秒杀'  
                                        WHEN a.Type = '8' THEN '一口价'  
                                        ELSE '其他' END ) as    Typedesc  
                                        ,a.Type
                                      ,h.[BuildingArea]----------建筑面积
                                      ,h.[Area]-------------使用面积
                                      ,h.[Room]-----------室
                                      ,h.[Hall]------------厅
                                      ,h.[Toilet]-------------卫
                                      ,h.[Kitchen]----------厨房
                                      ,h.Floor 
                                      ,h.TotalPrice---------市场价（总价）
                                      ,b.TheFloor-------地上楼层
                                      ,h.Name as HouseName -------房号
                            from House h ,  [ActivitiesHouse] ah        
                            , dbo.Activities a   
                            ,  dbo.Premises p   
                            , Building b   
                           , dbo.Unit u      
                            where 1=1
								and ah.[HouseId]=h.Id
								and a.Id=ActivitiesId
								and  p.Id=ah.[PremisesId]
								and ah.[BuildingId]=b.Id
								and  h.UnitId=u.Num  
								and u.PremisesId=h.PremisesId and u.BuildingId=h.BuildingId 
                                and ah.IsDel = 0 
                                and h.IsDel = 0 
                                and p.IsDel = 0 
                                and b.IsDel = 0 
                                and p.Id={0}  and  a.BidPrice is not null 
                                and a.EndTime>getdate() and a.ActivitieState=1  and h.SalesStatus=2
                                 ", new object[] { Id });
                    #region 执行操作
                    ObjectResult<PremisesActivitiesHouse> query = houseDb.ExecuteStoreQuery<PremisesActivitiesHouse>(sql);
                    List<PremisesActivitiesHouse> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { Id }), ex);
                throw;
            }
        }
        #endregion

        #region 通过楼盘ID获取参加营销活动的房源列表（新方法）
        /// <summary>
        /// 通过楼盘ID获取参加营销活动的房源列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<PremisesActivitiesHouse> GetActivitiesHouseByIdnew(int Id)
        {
            //            二〇一四年二月二十六日星期三
            //楼盘主页-房源列表，优先显示参加营销活动的房源，如没有参加营销活动的房源则显示未参加营销活动的房源
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT distinct top 6 
                                            isnull(a.Id,0) as ActivitiesId,
                                            (CASE
                                              WHEN a.Type = '1' THEN
                                               '摇号'
                                              WHEN a.Type = '2' THEN
                                               '限时折扣'
                                              WHEN a.Type = '3' THEN
                                               '排号购房'
                                              WHEN a.Type = '4' THEN
                                               '阶梯团购'
                                              WHEN a.Type = '5' THEN
                                               '竞价'
                                              WHEN a.Type = '6' THEN
                                               '砍价'
                                              WHEN a.Type = '7' THEN
                                               '秒杀'
                                              WHEN a.Type = '8' THEN
                                               '一口价'
                                              ELSE
                                               ' '
                                            END) as Typedesc,
                                            h.Id as HouseId,
                                            h. DeveloperId,
                                            h. PremisesId,
                                            h. BuildingId,
                                             isnull(ah. Discount,0) as Discount,
			                                 isnull(ah. IsDel,0) as IsDel,
                                            b.Name as BuildingName,
                                            b.NameType as BuildingNameType,
                                            u.Name as UnitName,
                                             isnull(a. BidPrice,0) as BidPrice,
                                             isnull(a. Type,0) as Type,
                                            h. BuildingArea,
                                            h. Area,
                                            h. Room,
                                            h. Hall,
                                            h. Toilet,
                                            h. Kitchen,
                                            h.Floor,
                                            h.TotalPrice,
                                            b.TheFloor,
                                            h.Name as HouseName
                              from House h
                              left join ActivitiesHouse ah
                                on h.Id = ah. HouseId
                              left join dbo.Activities a
                                on ah.ActivitiesId = a.Id
                              left join Building b
                                on h. BuildingId = b.Id
                              left join dbo.Unit u
                                on (h.UnitId = u.Num and u.PremisesId = h.PremisesId and
                                   u.BuildingId = h.BuildingId)
                             where 1 = 1
                               and h.IsDel = 0 and h.IsRelease=1  
                               and b.IsDel = 0
                               and h. PremisesId = {0}
                             order by ActivitiesId desc

                                 ", new object[] { Id });
                    #region 执行操作
                    ObjectResult<PremisesActivitiesHouse> query = houseDb.ExecuteStoreQuery<PremisesActivitiesHouse>(sql);
                    List<PremisesActivitiesHouse> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { Id }), ex);
                throw;
            }
        }
        #endregion

        #region  获取可能感兴趣的楼盘列表
        /// <summary>
        /// 获取可能感兴趣的楼盘列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.Premis> GetInterestPremisesList(TXOrm.Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                SELECT top 6  *
                      from Premises  (NOLOCK) 
                    where 1=1 
                    and  SalesStatus=1  and DId={0} and   ReferencePrice={1}  and Id<>{2}  and  CityId={3}  and IsDel=0
                        ", new object[] { premises.DId,
                                        premises.ReferencePrice,
                                         premises.Id,
                                          premises.CityId
                         });
                    #region 执行操作
                    ObjectResult<TXOrm.Premis> query = houseDb.ExecuteStoreQuery<TXOrm.Premis>(sql);
                    List<TXOrm.Premis> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region   同区域热门楼盘列表
        /// <summary>
        /// 同区域热门楼盘列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<Premis> GetHotPremisesList(Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                SELECT top 5   *
                      from Premises  (NOLOCK) 
                    where 1=1 
                       and DId={0} and CityId={1}
                       and Id<>{2}
                        ", new object[] { 
                                         premises.DId,
                                         premises.CityId,
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<Premis> query = houseDb.ExecuteStoreQuery<Premis>(sql);
                    List<Premis> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region   同区域最新楼盘
        /// <summary>
        /// 同区域最新楼盘
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<Premis> GetLastPremisesList(Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                  SELECT top 5   *
                      from Premises  (NOLOCK) 
                    where 1=1 
                       and DId={0} and CityId={1}
                       and Id<>{2}  order by  CreateTime  desc 
                        ", new object[] {
                                            premises.DId,
                                         premises.CityId,
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<Premis> query = houseDb.ExecuteStoreQuery<Premis>(sql);
                    List<Premis> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region   同区域最热楼盘排行(详情信息页面)
        /// <summary>
        /// 同区域最热楼盘排行
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<Premis> GetHotPremisesListbyDId(Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                               SELECT top 10   *
                                
                      from Premises  (NOLOCK) 
                    where 1=1 
                       and DId={0} and CityId={1}
                       and Id<>{2}
                        ", new object[] { 
                                         premises.DId,
                                         premises.CityId,
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<Premis> query = houseDb.ExecuteStoreQuery<Premis>(sql);
                    List<Premis> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region   同价位最热楼盘排行（详情信息页面）
        /// <summary>
        /// 同价位最热楼盘排行
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<Premis> GetHotPremisesbyReferencePrice(Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                 SELECT top 10   *
                      from Premises  (NOLOCK) 
                    where 1=1 
                       and ReferencePrice={0}  
                       and Id<>{1}
                        ", new object[] { 
                                         premises.ReferencePrice,
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<Premis> query = houseDb.ExecuteStoreQuery<Premis>(sql);
                    List<Premis> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region  获取楼盘下的楼栋列表
        /// <summary>
        /// 获取楼盘下的楼栋列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.Building> GetBuildingListbyPremisesId(TXOrm.Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT *
                      from   Building  (NOLOCK) 
                    where 1=1 
                      and PremisesId={0}  and IsDel<>1   order  by [CreateTime] asc 
                        ", new object[] { 
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<TXOrm.Building> query = houseDb.ExecuteStoreQuery<TXOrm.Building>(sql);
                    List<TXOrm.Building> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region  获取楼栋下的单元信息列表
        /// <summary>
        /// 获取楼栋下的单元信息列表
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<TXOrm.Unit> GetUnitListbyBuildingId(int buildingId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                     SELECT     *
                      from   Unit  (NOLOCK) 
                    where 1=1 
                      and BuildingId={0}
                        ", new object[] { 
                                         buildingId
                         });
                    #region 执行操作
                    ObjectResult<TXOrm.Unit> query = houseDb.ExecuteStoreQuery<TXOrm.Unit>(sql);
                    List<TXOrm.Unit> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { buildingId }), ex);
                throw;
            }
        }
        #endregion

        #region  获取楼栋下的单元信息列表(新房前台楼盘房源页面使用)
        /// <summary>
        /// 获取楼栋下的单元信息列表(新房前台楼盘房源页面使用)
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<TXOrm.Unit> GetUnitListbyBuildingId(int premisesid, int buildingId, int unitId, string strwhere)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sqltemp = string.Format(@"
                      SELECT     *
                      from   Unit  (NOLOCK) 
                        ");
                    StringBuilder sql = new StringBuilder();
                    sql.Append(sqltemp);
                    sql.Append("   where 1=1   ");
                    if (premisesid > 0)
                    {
                        sql.Append(string.Format("   and PremisesId={0}  ", premisesid));
                    }
                    if (buildingId > 0)
                    {
                        sql.Append(string.Format("   and BuildingId={0}   ", buildingId));
                    }
                    if (unitId > 0)
                    {
                        sql.Append(string.Format("   and Num={0}   ", unitId));
                    }
                    sql.Append(string.Format("   and  Num  in {0}   ", strwhere));
                    #region 执行操作
                    ObjectResult<TXOrm.Unit> query = houseDb.ExecuteStoreQuery<TXOrm.Unit>(sql.ToString());
                    List<TXOrm.Unit> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { buildingId }), ex);
                throw;
            }
        }
        #endregion

        #region  获取楼栋下的某个单元下的房源统计
        /// <summary>
        /// 获取楼栋下的某个单元下的房源统计
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="buildingId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public int GetHousecountbyunit(int premisesId, int buildingId, int unitId, int floor)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                    SELECT  distinct h.*
                                                FROM  [House] h,
                                                dbo.Building b,dbo.Unit u,
                                                dbo.Premises p
                                                where 1=1 and h.IsDel=0  and  h.IsRelease=1  and  h.PremisesId=p.id
                                                and  h.BuildingId=b.id
                                                and h.UnitId=u.Num and u.PremisesId={0} and u.BuildingId={1}
                                                and h.PremisesId={2}  and h.BuildingId={3} 
                        ", new object[] { 
                                            premisesId,
                                            buildingId, 
                                            premisesId, 
                                            buildingId, 
                                            unitId
                         });
                    StringBuilder sb = new StringBuilder();
                    sb.Append(sql);
                    if (unitId == -10000) { }
                    else
                    {
                        sb.Append(string.Format("  and  h.UnitId={0} ", unitId));
                    }
                    if (floor == -10000) { }
                    else
                    {
                        sb.Append(string.Format("  and  h.Floor={0}  ", floor));
                    }
                    #region 执行操作
                    ObjectResult<TXOrm.House> query = houseDb.ExecuteStoreQuery<TXOrm.House>(sb.ToString());
                    List<TXOrm.House> ls = query.ToList();
                    #endregion
                    return ls.Count;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { buildingId }), ex);
                throw;
            }
        }
        #endregion

        #region  获取楼栋下的某个单元下的房源统计
        /// <summary>
        /// 获取楼栋下的某个单元下的房源统计
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="buildingId"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<TXModel.Web.HouseFloorCount> GetHouseFloorList(int premisesId, int buildingId, int unitId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                 ----  SELECT count(Floor) as Floorcount ,Floor
                  -----    FROM  House  (NOLOCK) 
                  -----  where [PremisesId]={0} and [BuildingId]={1} and  [UnitId]={2}   group by  [Floor]
                        SELECT count(h.Floor) as Floorcount ,h.Floor
                        FROM  House h (NOLOCK) ,
                        dbo.Building b,dbo.Unit u,
                        dbo.Premises p
                        where 1=1 and h.IsDel=0  and h.IsRelease=1   and  h.PremisesId=p.id
                        and  h.BuildingId=b.id
                        and h.UnitId=u.Num and u.PremisesId=h.PremisesId
                        and u.BuildingId=h.BuildingId
                        and h.PremisesId={0} and h.BuildingId={1} and  h.UnitId={2}  
                        group by  [Floor]
                        ", new object[] { 
                                            premisesId, 
                                            buildingId, 
                                            unitId
                         });
                    #region 执行操作
                    ObjectResult<TXModel.Web.HouseFloorCount> query = houseDb.ExecuteStoreQuery<TXModel.Web.HouseFloorCount>(sql);
                    List<TXModel.Web.HouseFloorCount> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { buildingId }), ex);
                throw;
            }
        }

        public List<TXModel.Web.HouseFloor> GetHouseFloorInfoList(int premisesId, int buildingId, int floor, string strwhereFloorInfo)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sqltemp = string.Format(@"
                  select  distinct Floor   ,[BuildingId]  ,UnitId
                    from House as h  where [PremisesId]={0} and h.IsRelease=1 and h.isDel=0    
                        ", new object[] { 
                                            premisesId, 
                                            buildingId
                                         
                         });
                    StringBuilder sql = new StringBuilder();
                    sql.Append(sqltemp);
                    if (buildingId > 0)
                    {
                        sql.Append(string.Format("   and BuildingId={0}    ", buildingId));
                    }
                    //sql.Append(string.Format("   and Floor={0}    ", floor));
                    if (floor == 0)
                    {
                        sql.Append(string.Format("   and  Floor  in {0}   ", strwhereFloorInfo));

                    }
                    else
                    {
                        if (floor != -1000)
                        {
                            sql.Append(string.Format("   and Floor={0}    ", floor));
                        }
                    }

                    #region 执行操作
                    ObjectResult<TXModel.Web.HouseFloor> query = houseDb.ExecuteStoreQuery<TXModel.Web.HouseFloor>(sql.ToString());
                    List<TXModel.Web.HouseFloor> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { buildingId }), ex);
                throw;
            }
        }
        #endregion

        #region  获取楼盘的区域参考均价
        /// <summary>
        /// 获取楼盘的区域参考均价
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public decimal GetAvgReferencePrice(TXOrm.Premis premises)
        {
            decimal avgreferenceprice = 0;
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                        SELECT  
                            Avg(ReferencePrice)  as  ReferencePrice
                      from   Premises  (NOLOCK) 
                    where 1=1 
                      and DId={0} and CityId={1}
                        ", new object[] { 
                                         premises.DId,
                                         premises.CityId
                         });
                    var temp = houseDb.ExecuteStoreQuery<decimal>(sql);
                    avgreferenceprice = temp.FirstOrDefault<decimal>();
                    return avgreferenceprice;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
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
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                        SELECT  
                             distinct Renovation
                      from   Building    (NOLOCK) 
                    where 1=1 
                      and  PremisesId={0}  and IsDel=0 and State=2 
                        ", new object[] { 
                                         premises.Id
                         });
                    ObjectResult<int> query = houseDb.ExecuteStoreQuery<int>(sql);
                    List<int> ls = query.ToList();
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
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
            try
            {
                using (var entity = new TXOrm.kyj_NewHouseDBEntities())
                {
                    StringBuilder sbSql = new StringBuilder();
                    string sql = string.Format(@"
                    select Id, ----楼盘Id
                           Name, ----楼盘名称
                           InnerCode, ----GUID
                           ReferencePrice, ----参考均价
                           DName, ----区域名称
                           Ring, ----环线位置（1一环，2二环，3三环，4四环，5五环，6六环）
                           BName, ----商圈名称
                           PremisesAddress, ---项目地址
                           Developer, ----开发商
                           PropertyRight, ----产权
                           BuildingArea, ----建筑面积
                           Area, ----占地面积
                           UserCount, ----总户数
                           BuildingType, ----建筑类别（1板楼，2塔楼，3砖楼，4砖混，5平房，6钢混）
                           Characteristic, ----项目特色
                           PropertyType, -------物业类型（1住宅，2写字楼，3别墅，4商业）
                           AreaRatio, ----容积率
                           PropertyPrice, ----物业费
                           ParkingLot, ----车位信息
                           GreeningRate, ----绿化率
                           SupportingIntroduction, ----配套介绍
                           School, ----学校
                           Shopping, ----购物
                           Hospital, ----医院
                           Life, ----生活
                           Entertainment, ----娱乐
                           Catering, ----餐饮
                           Bus, ----公交
                           Subway ----地铁
                    from Premises P left join PremisesOther PO on P.Id = PO.PremisesId");
                    sbSql.Append(sql);
                    sbSql.Append(" where");
                    for (int i = 0; i < IdList.Count; i++)
                    {
                        sbSql.Append(" P.Id = {" + i + "}");
                        sbSql.Append(" or");
                    }
                    string sqlResult = string.Format(sbSql.ToString().TrimEnd('o', 'r'), IdList.ToArray());
                    ObjectResult<TXModel.Web.PremisesCompare> query = entity.ExecuteStoreQuery<TXModel.Web.PremisesCompare>(sqlResult);
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("崔利国", string.Format("({0})", new object[] { IdList }), ex);
                throw;
            }
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
            try
            {
                using (var entity = new TXOrm.kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                    select Id, ----楼盘Id
                           Name, ----楼盘名称
                           InnerCode, ----GUID
                           ReferencePrice, ----参考均价
                           DName, ----区域名称
                           Ring, ----环线位置（1一环，2二环，3三环，4四环，5五环，6六环）
                           BName, ----商圈名称
                           PremisesAddress, ---项目地址
                           Developer, ----开发商
                           PropertyRight, ----产权
                           BuildingArea, ----建筑面积
                           Area, ----占地面积
                           UserCount, ----总户数
                           BuildingType, ----建筑类别（1板楼，2塔楼，3砖楼，4砖混，5平房，6钢混）
                           Characteristic, ----项目特色
                           PropertyType, -------物业类型（1住宅，2写字楼，3别墅，4商业）
                           AreaRatio, ----容积率
                           PropertyPrice, ----物业费
                           ParkingLot, ----车位信息
                           GreeningRate, ----绿化率
                           SupportingIntroduction, ----配套介绍
                           School, ----学校
                           Shopping, ----购物
                           Hospital, ----医院
                           Life, ----生活
                           Entertainment, ----娱乐
                           Catering, ----餐饮
                           Bus, ----公交
                           Subway ----地铁
                    from Premises P left join PremisesOther PO on P.Id = PO.PremisesId where P.Name = '{0}'", new object[] { premisesName });
                    ObjectResult<TXModel.Web.PremisesCompare> query = entity.ExecuteStoreQuery<TXModel.Web.PremisesCompare>(sql);
                    return query.ToList().FirstOrDefault();
                }
                //using (var entity = new TXOrm.kyj_NewHouseDBEntities())
                //{
                //    var query = from premis in entity.Premises
                //                join premisesOther in entity.PremisesOthers on premis.Id equals premisesOther.PremisesId into leftJoin
                //                from preOther in leftJoin.DefaultIfEmpty()
                //                where premis.Name == premisesName
                //                select new { premis };
                //    var result = query.FirstOrDefault();
                //    TXModel.Web.PremisesCompare premisesCompare = new TXModel.Web.PremisesCompare();
                //    if (result != null)
                //    {
                //        premisesCompare.Id = result.premis.Id;
                //        premisesCompare.Name = result.premis.Name;
                //        premisesCompare.InnerCode = result.premis.InnerCode;
                //        premisesCompare.ReferencePrice = result.premis.ReferencePrice;
                //        premisesCompare.DName = result.premis.DName;
                //        premisesCompare.Ring = result.premis.Ring;
                //        premisesCompare.BName = result.premis.BName;
                //        premisesCompare.PremisesAddress = result.premis.PremisesAddress;
                //        premisesCompare.Developer = result.premis.Developer;
                //        premisesCompare.PropertyRight = result.premis.PropertyRight;
                //        premisesCompare.BuildingArea = result.premis.BuildingArea;
                //        premisesCompare.Area = result.premis.Area;
                //        premisesCompare.UserCount = result.premis.UserCount;
                //        premisesCompare.BuildingType = result.premis.BuildingType;
                //        premisesCompare.Characteristic = result.premis.Characteristic;
                //        premisesCompare.PropertyType = result.premis.PropertyType;
                //        premisesCompare.AreaRatio = result.premis.AreaRatio;
                //        premisesCompare.PropertyPrice = result.premis.PropertyPrice;
                //        premisesCompare.ParkingLot = result.premis.ParkingLot;
                //        premisesCompare.GreeningRate = result.premis.GreeningRate;
                //        premisesCompare.SupportingIntroduction = result.premis.SupportingIntroduction;
                //    }
                //    if (result != null)
                //    {
                //        premisesCompare.School = result.preOther.School;
                //        premisesCompare.Shopping = result.preOther.Shopping;
                //        premisesCompare.Hospital = result.preOther.Hospital;
                //        premisesCompare.Life = result.preOther.Life;
                //        premisesCompare.Entertainment = result.preOther.Entertainment;
                //        premisesCompare.Catering = result.preOther.Catering;
                //        premisesCompare.Bus = result.preOther.Bus;
                //        premisesCompare.Subway = result.preOther.Subway;
                //    }
                //    return premisesCompare;
                //}
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("崔利国", string.Format("({0})", new object[] { premisesName }), ex);
                throw;
            }
        }
        #endregion

        #region 获取房间信息
        /// <summary>
        /// 根据房源ID获取房源信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PremisesHouse GetPremisesHouseById(int id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
             select top 1 ah.ActivitiesId as ActivityId,a.[Type] as ActivityType,a.ActivitieState as ActivityState,a.BeginTime as ActBeginTime, a.EndTime as ActEndTime, b.PermitPresale, b.TheFloor, p.Name as PremisesName,b.Name+b.NameType as BuildingName,u.Name as UnitName, h.Id as HouseId, h.DeveloperId,h.PremisesId,h.BuildingId,h.UnitId,h.[Floor],h.Name as HouseName,h.SinglePrice,h.TotalPrice,h.DownPayment,h.SalesStatus,h.BuildingArea,h.Area,h.Room,h.Hall,h.Toilet,h.Kitchen,h.Orientation,h.UpdateTime,h.PriceType from house h left join activitiesHouse ah on h.id=ah.houseid left join Unit u on h.UnitId=u.Num left join activities a on ah.activitiesId=a.id left join building b on h.buildingid=b.id left join Premises p on h.PremisesId=p.id where h.id={0} order by ah.CreateTime desc", new object[] { id });
                    #region 执行操作
                    ObjectResult<PremisesHouse> query = houseDb.ExecuteStoreQuery<PremisesHouse>(sql);
                    PremisesHouse ls = query.ToList().FirstOrDefault();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("崔利国A", string.Format("({0})", new object[] { id }), ex);
                throw;
            }
        }

        /// <summary>
        /// 根据活动ID获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.Activity GetHouseActivityById(int id)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT *
                              FROM [kyj_NewHouseDB].[dbo].[Activities] where [Id]={0}
                        ", new object[] { id });
                    #region 执行操作
                    ObjectResult<TXOrm.Activity> query = houseDb.ExecuteStoreQuery<TXOrm.Activity>(sql);
                    TXOrm.Activity _activity = query.ToList().FirstOrDefault();
                    #endregion
                    return _activity;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("崔利国A", string.Format("({0})", new object[] { id }), ex);
                throw;
            }
        }

        /// <summary>
        /// 根据活动ID和房源ID获取出价信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<TXOrm.BidPrice> GetBidPriceList(int activityId, int houseId)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                                SELECT *
                      from BidPrice  (NOLOCK) 
                    where 1=1 
                       and ActivitiesId={0} and HouseId={1}
                        ", new object[] { 
                                         activityId,
                                         houseId,
                         });
                    #region 执行操作
                    ObjectResult<BidPrice> query = houseDb.ExecuteStoreQuery<BidPrice>(sql);
                    List<BidPrice> bp = query.ToList();
                    #endregion
                    return bp;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("崔利国A", string.Format("({0})", new object[] { activityId, houseId }), ex);
                throw;
            }
        }

        /// <summary>
        /// 获取活动房源上次出价
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public TXOrm.BidPrice GetLastBidPrice(int activityId, int houseId)
        {
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    var model = houseDb.BidPrices.Where(b => b.ActivitiesId == activityId && b.HouseId == houseId && b.IsDel == false).OrderByDescending(b => b.Id).FirstOrDefault();
                    return model;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //        /// <summary>
        //        /// 根据活动ID获取参与活动的用户信息
        //        /// </summary>
        //        /// <param name="activityId"></param>
        //        /// <param name="houseId"></param>
        //        /// <returns></returns>
        //        public List<TXOrm.ActivitiesUser> GetActivityUserList(int activityId)
        //        {
        //            try
        //            {
        //                using (var houseDb = new kyj_NewHouseDBEntities())
        //                {
        //                    string sql = string.Format(@"
        //                                SELECT *
        //                      from ActivitiesUser  (NOLOCK) 
        //                    where 1=1 
        //                       and ActivitiesId={0}
        //                        ", new object[] { 
        //                                         activityId,
        //                         });
        //                    #region 执行操作
        //                    ObjectResult<ActivitiesUser> query = houseDb.ExecuteStoreQuery<ActivitiesUser>(sql);
        //                    List<ActivitiesUser> au = query.ToList();
        //                    #endregion
        //                    return au;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Log4netService.RecordLog.RecordException("崔利国A", string.Format("({0})", new object[] { activityId }), ex);
        //                throw;
        //            }
        //        }

        #endregion

        #region  获取某个楼盘下户型信息(新房前台楼盘主页用)

        #region  获取某个楼盘下面的所有房源的户型基本信息
        /// <summary>
        /// 获取某个楼盘下面的所有房源的户型基本信息
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXModel.Web.PremisesHouseHuX> GetPremisesHouseHuXList(TXOrm.Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                              SELECT 
                                distinct
                                     RId as   PictureData
                                      ,[BuildingArea]
                                      ,[Room]
                                      ,[Hall]
                                      ,[Toilet]
                                      ,[Kitchen]
                      from  House  (NOLOCK) 
                    where 1=1  and IsDel<>1 and IsRelease=1
                      and PremisesId={0}  
                        ", new object[] { 
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<TXModel.Web.PremisesHouseHuX> query = houseDb.ExecuteStoreQuery<TXModel.Web.PremisesHouseHuX>(sql);
                    List<TXModel.Web.PremisesHouseHuX> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }

        public List<TXModel.Web.PremisesHouseHuX> GetPremisesHouseHuXList(TXOrm.Premis premises, int buildingId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                              SELECT 
                                distinct  RId as   PictureData,
                                      [BuildingArea]
                                      ,[Room]
                                      ,[Hall]
                                      ,[Toilet]
                                      ,[Kitchen]
                      from  House  (NOLOCK) 
                    where 1=1 
                      and PremisesId={0}   and BuildingId={1} and IsDel<>1 and IsRelease=1
                        ", new object[] { 
                                         premises.Id,
                                         buildingId
                         });
                    #region 执行操作
                    ObjectResult<TXModel.Web.PremisesHouseHuX> query = houseDb.ExecuteStoreQuery<TXModel.Web.PremisesHouseHuX>(sql);
                    List<TXModel.Web.PremisesHouseHuX> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }

        public List<TXModel.Web.BuildingHouseHuX> GetPremisesHouseHuXListNew(TXOrm.Premis premises, int buildingId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                              SELECT [RId],[BuildingArea],[Room],[Hall],[Toilet],[Kitchen],[id] as HouseId from
                              (
                                SELECT ROW_NUMBER() OVER(partition by [BuildingArea],[Room],[Hall],[Toilet],[Kitchen] Order by RId DESC) as num,
                                       [RId],[BuildingArea],[Room],[Hall],[Toilet],[Kitchen],[id]
                                from  House  (NOLOCK) 
                                where PremisesId={0}   and BuildingId={1} and IsDel<>1 and IsRelease=1
                               ) as a where a.num=1
                        ", new object[] { 
                                         premises.Id,
                                         buildingId
                         });
                    #region 执行操作
                    ObjectResult<TXModel.Web.BuildingHouseHuX> query = houseDb.ExecuteStoreQuery<TXModel.Web.BuildingHouseHuX>(sql);
                    List<TXModel.Web.BuildingHouseHuX> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
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
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT   distinct
                                [Room]
                      from  House  (NOLOCK) 
                    where 1=1 
                      and PremisesId={0}  
                        ", new object[] { 
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<int> query = houseDb.ExecuteStoreQuery<int>(sql);
                    List<int> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        public List<int> GetHouseRoomInfoList(TXOrm.Premis premises, int BuildingId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT   distinct
                                [Room]
                      from  House  (NOLOCK) 
                    where 1=1 
                      and PremisesId={0}   and BuildingId={1} 
                        ", new object[] { 
                                         premises.Id
                                         ,BuildingId
                         });
                    #region 执行操作
                    ObjectResult<int> query = houseDb.ExecuteStoreQuery<int>(sql);
                    List<int> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
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
            String empty = String.Empty;
            int houseCount = 0;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                      SELECT 
                                distinct
                                      [Rid]
                                      ,[BuildingArea]
                                      ,[Room]
                                      ,[Hall]
                                      ,[Toilet]
                                      ,[Kitchen]
                      from  House  (NOLOCK) 
                    where 1=1 
                      and PremisesId={0}  and Room={1}
                        ", new object[] { 
                                         premises.Id,
                                         room
                         });
                    #region 执行操作
                    ObjectResult<TXModel.Web.PremisesHouseHuX> query = houseDb.ExecuteStoreQuery<TXModel.Web.PremisesHouseHuX>(sql);
                    List<TXModel.Web.PremisesHouseHuX> ls = query.ToList();
                    houseCount = ls.Count;
                    #endregion
                    return houseCount;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #endregion

        #region  获取某个楼盘的预售许可证列表
        /// <summary>
        /// 获取某个楼盘的预售许可证列表
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        public List<TXOrm.PermitPresale> GetPermitPresaleList(TXOrm.Premis premises)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT  *
                      from   PermitPresale  (NOLOCK) 
                    where 1=1 
                      and  PremisesId={0}   order  by  CreateTime desc 
                        ", new object[] { 
                                         premises.Id
                         });
                    #region 执行操作
                    ObjectResult<TXOrm.PermitPresale> query = houseDb.ExecuteStoreQuery<TXOrm.PermitPresale>(sql);
                    List<TXOrm.PermitPresale> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                throw;
            }
        }
        #endregion

        #region  获取某个楼盘的楼盘特色信息
        /// <summary>
        /// 获取某个楼盘的楼盘特色信息
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TXOrm.PremisesFeature> GetPremisesFeatureList(string where)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                         SELECT  *
                      from   PremisesFeature  (NOLOCK) 
                    where 1=1 
                      and  Id in ({0}) --- order  by  CreateTime desc 
                        ", new object[] { 
                                         where
                         });
                    #region 执行操作
                    ObjectResult<TXOrm.PremisesFeature> query = houseDb.ExecuteStoreQuery<TXOrm.PremisesFeature>(sql);
                    List<TXOrm.PremisesFeature> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { where }), ex);
                throw;
            }
        }
        #endregion

        #region  获取某个楼盘下的单个楼栋下的所有房源的平均价格
        /// <summary>
        /// 获取某个楼盘下的单个楼栋下的所有房源的平均价格
        /// </summary>
        /// <param name="premises"></param>
        /// <param name="BuildingId"></param>
        /// <returns></returns>
        public decimal GetHouseSinglePrice(TXOrm.Premis premises, int buildingId)
        {
            decimal avgSinglePrice = 0;
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                       select AVG([SinglePrice]) 
                      from   House  (NOLOCK) 
                    where 1=1 
                     and  [PremisesId]={0}  and  BuildingId={1}
                        ", new object[] { 
                                         premises.Id,
                                        buildingId
                         });
                    var temp = houseDb.ExecuteStoreQuery<decimal>(sql);
                    avgSinglePrice = temp.FirstOrDefault<decimal>();
                    return avgSinglePrice;
                }
            }
            catch (Exception ex)
            {
                avgSinglePrice = 0;
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premises }), ex);
                return avgSinglePrice;
            }
        }
        #endregion

        #region  通过楼盘ID获取楼盘基本信息
        /// <summary>
        /// 通过楼盘ID获取楼盘基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<RecentlyViewed> GetPremisesbyIds(string Id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT  * from Premises  (NOLOCK)  where 1=1  and IsDel=0  and Id in ({0})  
                        ", new object[] { Id });
                    #region 执行操作
                    ObjectResult<RecentlyViewed> query = houseDb.ExecuteStoreQuery<RecentlyViewed>(sql);
                    List<RecentlyViewed> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 根据活动ID和房源ID获取出价信息

        /// <summary>
        /// 根据活动ID和房源ID获取出价信息
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<TXOrm.BidPrice> GetBidPriceList(int activityId, int houseId, int pageindex, int pagesize, out int totalcount, int type, int userId)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    if (type == 1 || type == 3)
                    {
                        var query = from bid in newhouseDb.BidPrices
                                    where bid.IsDel == false && bid.ActivitiesId == activityId && bid.HouseId == houseId
                                    select bid;
                        totalcount = query.Count();
                        List<TXOrm.BidPrice> list = query.OrderByDescending(it => it.Status).ThenByDescending(it => it.CreateTime).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
                        return list;
                    }
                    else
                    {
                        if (userId != 0)
                        {
                            var query = from bid in newhouseDb.BidPrices
                                        where bid.IsDel == false && bid.ActivitiesId == activityId && bid.HouseId == houseId && bid.BidUserId == userId
                                        select bid;
                            totalcount = query.Count();
                            List<TXOrm.BidPrice> list = query.OrderByDescending(it => it.CreateTime).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
                            return list;
                        }
                        else
                        {
                            List<BidPrice> list = new List<BidPrice>();
                            totalcount = 0;
                            return list;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region  通过楼盘ID获取楼盘扩展表基本信息
        /// <summary>
        /// 通过楼盘ID获取楼盘扩展表基本信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TXOrm.PremisesOther GetPremisesOthersbyId(int Id)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                            SELECT  *
                      from   PremisesOther  (NOLOCK)  where 1=1     and  PremisesId={0}  
                        ", new object[] { Id });
                    #region 执行操作
                    ObjectResult<TXOrm.PremisesOther> query = houseDb.ExecuteStoreQuery<TXOrm.PremisesOther>(sql);
                    TXOrm.PremisesOther ls = query.ToList().FirstOrDefault();
                    #endregion
                    return ls;
                }
            }
            catch (Exception)
            {
                throw;
            }
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
            try
            {
                using (var entity = new TXOrm.kyj_NewHouseDBEntities())
                {
                    return entity.Premises.Where(p => p.Name.Contains(name)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 获取楼盘沙盘标注
        public List<SandTableLabel> GetSandTableLabelList(int premisesId)
        {
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"
                               SELECT s.[Id]
                            --      ,s.[DeveloperId]
                                  ,s.[PremisesId]
                                  ,s.[SandBox]
                                  ,s.[Number]
                                  ,s.[CoordX]
                                  ,s.[CoordY]+28 as  CoordY
                                  ,s.[CreateTime]
                                  ,isnull(b.State,-100)  as [DeveloperId]
                              FROM [dbo].[SandTableLabel] s join dbo.Building b on ( s.[PremisesId]=b.[PremisesId]
                            and s.Id=b.[PNum])
                            where 1=1
                           --- and  b.State is not null   
                        ");
                    StringBuilder sb = new StringBuilder();
                    sb.Append(sql);
                    if (premisesId > 0)
                    {
                        sb.Append(string.Format("   and s.PremisesId={0}  ", premisesId));
                    }
                    #region 执行操作
                    ObjectResult<SandTableLabel> query = houseDb.ExecuteStoreQuery<SandTableLabel>(sb.ToString());
                    List<SandTableLabel> ls = query.ToList();
                    #endregion
                    return ls;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("曾清华", string.Format("({0})", new object[] { premisesId }), ex);
                throw;
            }
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
            String empty = String.Empty;
            try
            {
                using (var houseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"SELECT * FROM dbo.Premises WHERE POWER(convert(float,Lng)-{0},2)+POWER(CONVERT(float,lat)-{1},2)<=POWER({2}/111.111,2)", new object[] { Lng, Lat, km });
                    #region 执行操作
                    ObjectResult<TXOrm.Premis> query = houseDb.ExecuteStoreQuery<TXOrm.Premis>(sql);
                    List<TXOrm.Premis> fj = query.ToList();
                    #endregion
                    return fj;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("sunlin", string.Format("({0})", new object[] { Lng }), ex);
                throw;
            }
        }

        #endregion

        #endregion

    }

}