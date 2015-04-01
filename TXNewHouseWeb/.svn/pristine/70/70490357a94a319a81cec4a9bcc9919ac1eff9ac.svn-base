using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;
using TXModel.Commons;
namespace TXDal.NHouseActivies.Yaohao
{
    public partial class Yaohao_DeveloperDal
    {
        #region 摇号活动列表
        /// <summary>
        /// 摇号活动列表
        /// author:汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="paging">分页类</param>
        /// <returns></returns>
        public Paging<TXModel.NHouseActivies.YaoHao.CT_YaoHao> GetYaohaoList(int developerId, Paging<TXModel.NHouseActivies.YaoHao.CT_YaoHao> paging)//摇号活动列表
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    ////查询楼盘名称
                    //var Premises = from premises in newhouseDb.Premises
                    //               select new
                    //               {
                    //                   Id = premises.Id,
                    //                   PremisesName = premises.Name
                    //               };
                    //查询楼栋名称
                    var Building = from build in newhouseDb.Buildings
                                   where build.DeveloperId == developerId
                                   select new
                                   {
                                       Id = build.Id,
                                       BuildingName = build.Name,
                                       NameType = build.NameType
                                   };
                    var query = from activitie in newhouseDb.ActivitiesYaoHaoApplies
                                join premises in newhouseDb.Premises on activitie.PremisesId equals premises.Id
                                where activitie.IsDel == false && activitie.DeveloperId == developerId
                                select new
                                {
                                    ActivitiesId = activitie.ActivitiesId,
                                    Name = activitie.Name,
                                    //PremisesName = Premises.Where(p => p.Id == activitie.PremisesId).FirstOrDefault().PremisesName,
                                    PremisesName = premises.Name,
                                    CreateTime = activitie.CreateTime,
                                    ActivitieState = activitie.State,
                                    ActivitieExtend = activitie.BuildingIds,
                                    IsAllPremises = activitie.IsAllPremises
                                };
                    List<TXModel.NHouseActivies.YaoHao.CT_YaoHao> list = new List<TXModel.NHouseActivies.YaoHao.CT_YaoHao>();
                    foreach (var item in query)
                    {
                        TXModel.NHouseActivies.YaoHao.CT_YaoHao ct = new TXModel.NHouseActivies.YaoHao.CT_YaoHao();
                        ct.ActivitiesId = item.ActivitiesId;
                        ct.Name = item.Name;
                        ct.PremisesName = item.PremisesName;
                        ct.CreateTime = item.CreateTime;
                        ct.ActivitieState = item.ActivitieState;
                        if (item.IsAllPremises == false)
                        {
                            if (item.ActivitieExtend.Contains(","))
                            {
                                string[] temp = item.ActivitieExtend.Split(',');
                                int c = 0;
                                for (int i = 0; i < temp.Length; i++)
                                {
                                    c = Convert.ToInt32(temp[i]);
                                    ct.ActivitieExtend += Building.Where(p => p.Id == c).FirstOrDefault().BuildingName + Building.Where(p => p.Id == c).FirstOrDefault().NameType + ",";
                                }
                                ct.ActivitieExtend = ct.ActivitieExtend.TrimEnd(',');
                            }
                            else
                            {
                                int c = Convert.ToInt32(item.ActivitieExtend);
                                ct.ActivitieExtend = Building.Where(p => p.Id == c).FirstOrDefault().BuildingName + Building.Where(p => p.Id == c).FirstOrDefault().NameType;
                            }
                        }
                        else
                        {
                            ct.ActivitieExtend = "全部";
                        }
                        list.Add(ct);
                    }
                    paging.TotalCount = query.Count();
                    paging.ResultData = list.OrderByDescending(q => q.CreateTime).Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize).ToList();
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("developerId:{0},PageIndex:{1},PageSize:{2},ResultData:{3},TotalCount:{4}", developerId, paging.PageIndex, paging.PageSize, paging.ResultData, paging.TotalCount), e);
                //throw;
            }
            return paging;
        }
        #endregion

        #region 申请摇号活动
        /// <summary>
        /// 申请摇号活动
        /// author:汪敏
        /// </summary>
        /// <returns>申请失败返回:0</returns>
        public int Add(TXOrm.ActivitiesYaoHaoApply activity)
        {
            int result = 0;
            try
            {
                using (var kyjEnt = new kyj_NewHouseDBEntities())
                {
                    var act = new Activity()
                    {
                        ActivitieState = 3,
                        AddPrice = 0,
                        BeginTime = DateTime.Now,
                        BidPrice = 0,
                        Bond = 0,
                        CreateTime = DateTime.Now,
                        DeveloperId = activity.DeveloperId,
                        EndTime = DateTime.Now,
                        HouseCount = 0,
                        IsDel = false,
                        MaxPrice = 0,
                        Name = activity.Name,
                        SignupBeginTime = DateTime.Now,
                        SignupEndTime = DateTime.Now,
                        Type = 1,
                        UpdateTime = DateTime.Now,
                        UserCount = 0,
                    };
                    kyjEnt.Activities.AddObject(act);
                    kyjEnt.SaveChanges();

                    activity.ActivitiesId = act.Id;
                    kyjEnt.ActivitiesYaoHaoApplies.AddObject(activity);
                    kyjEnt.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("ActivitiesId:{0},BuildingIds:{1},DeveloperId:{2},IsAllPremises:{3},Name:{4},PremisesId:{5}", activity.ActivitiesId, activity.BuildingIds, activity.DeveloperId, activity.IsAllPremises, activity.Name, activity.PremisesId), e);
                // throw;
            }
            return result;
        }
        #endregion

        #region (楼盘)楼栋下是否有房源参与了摇号
        /// <summary>
        /// (楼盘)楼栋下是否有房源参与了摇号
        /// author:汪敏
        /// </summary>
        /// <param name="developerId">开发商Id</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="buildingId">楼栋Id</param>
        /// <returns></returns>
        public int CheckYaohao(int developerId, int premisesId, int buildingId)
        {
            int result = 0;
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {

                    if (premisesId == 0)
                    {
                        var query = (from a in newhouseDb.ActivitiesHouses
                                     join b in newhouseDb.Activities
                                     on a.ActivitiesId equals b.Id
                                     where a.DeveloperId == developerId && a.BuildingId == buildingId && b.Type == 1 && a.IsDel == false && b.IsDel == false
                                     select a);
                        if (query.Count() > 0)
                        {
                            result = 1;
                        }
                    }
                    else
                    {
                        var query = (from a in newhouseDb.ActivitiesHouses
                                     join b in newhouseDb.Activities
                                     on a.ActivitiesId equals b.Id
                                     where a.DeveloperId == developerId && a.PremisesId == premisesId && b.Type == 1 && a.IsDel == false && b.IsDel == false
                                     select a);
                        var query2 = (from a in newhouseDb.Buildings where a.PremisesId == premisesId && a.IsDel == false select a);
                        if (query.Count() == query2.Count())
                        {
                            result = 1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("developerId:{0},premisesId:{1},buildingId:{2}", developerId, premisesId, buildingId), e);
                // throw;
            }
            return result;
        }
        #endregion

        #region 摇号活动用户列表
        /// <summary>
        /// 摇号活动用户列表
        /// author:汪敏
        /// </summary>
        /// <param name="activityId">活动Id</param>
        /// <returns></returns>
        public List<TXOrm.BidPrice> GetYaohaoUserList(int activityId)//摇号活动用户列表
        {
            List<TXOrm.BidPrice> list = new List<BidPrice>();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = (from user in newhouseDb.BidPrices
                                 where user.IsDel == false && user.ActivitiesId == activityId
                                 select new
                                 {
                                     Name = user.BidUserName,
                                     IDCard = user.IDCard,
                                     //InnerCode = user.InnerCode,
                                     Mobile = user.BidUserMobile,
                                     Num = user.BidUserNumber,
                                     //Sex = user.Sex,
                                     CreateTime = user.CreateTime
                                 });
                    if (query.Count() > 0)
                    {
                        list = query.OrderByDescending(it => it.CreateTime).ToList().ConvertAll(
                    it => new TXOrm.BidPrice
                    {
                        BidUserName = it.Name,
                        IDCard = it.IDCard,
                        //InnerCode = it.InnerCode,
                        BidUserMobile = it.Mobile,
                        BidUserNumber = it.Num,
                        // Sex = it.Sex,
                        CreateTime = it.CreateTime
                    });
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("汪敏", String.Format("activityId:{0}", activityId), e);
                //throw;
            }
            return list;
        }
        #endregion
    }
}