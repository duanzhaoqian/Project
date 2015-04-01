using System;
using System.Collections.Generic;
using System.Linq;
using TXOrm;
using TXModel.Web;
using System.Data.SqlClient;
using System.Collections;

namespace TXDal.Activitys
{
    public class ActivitysDal
    {
        #region 摇号活动列表
        /// <summary>
        /// 摇号活动列表
        /// author：sunlin
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="totalcount"></param>
        /// <returns></returns>
        public List<ActivityList> GetActivityList(int pageindex, int pagesize, out int totalcount)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = from act in newhouseDb.Activities
                                join house in newhouseDb.ActivitiesHouses
                                on act.Id equals house.ActivitiesId
                                join yh in newhouseDb.ActivitiesYaoHaoInfos
                                on act.Id equals yh.ActivitiesId
                                where act.IsDel == false && act.ActivitieState == 2 && act.Type == 1
                                select new
                                {
                                    act.Id,
                                    act.DeveloperId,
                                    act.Name,
                                    yh.ActivitieLocation,
                                    act.Type,
                                    act.UserCount,
                                    act.HouseCount,
                                    act.BidPrice,
                                    act.AddPrice,
                                    act.MaxPrice,
                                    //yh.SignupBeginTime,
                                    //yh.SignupEndTime,
                                    act.Bond,
                                    act.ActivitieState,
                                    act.UpdateTime,
                                    act.CreateTime,
                                    act.IsDel,
                                    act.BeginTime,
                                    yh.Periods,
                                    house.PremisesId,
                                    yh.NotarialOffice,
                                    yh.Introduction
                                };
                    totalcount = query.Count();
                    List<ActivityList> list = query.OrderByDescending(it => it.CreateTime).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList().ConvertAll(
                     it => new ActivityList
                     {
                         ActivitiesId = it.Id,
                         Name = it.Name,
                         ActivitieLocation = it.ActivitieLocation,
                         Type = it.Type,
                         UserCount = it.UserCount,
                         HouseCount = it.HouseCount,
                         BidPrice = it.BidPrice,
                         AddPrice = it.AddPrice,
                         MaxPrice = it.MaxPrice,
                         //SignupBeginTime = it.SignupBeginTime,
                         //SignupEndTime = it.SignupBeginTime,
                         Bond = it.Bond,
                         ActivitieState = it.ActivitieState,
                         UpdateTime = it.UpdateTime,
                         CreateTime = it.CreateTime,
                         IsDel = it.IsDel,
                         PremisesId = it.PremisesId,
                         Periods = it.Periods,
                         NotarialOffice = it.NotarialOffice,
                         Introduction = it.Introduction,
                         BeginTime = it.BeginTime
                     });
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 取本月活动

        /// <summary>
        /// 取本月活动
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActivityList GetActivity()
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = from act in newhouseDb.Activities
                                join house in newhouseDb.ActivitiesHouses
                                on act.Id equals house.ActivitiesId
                                join yh in newhouseDb.ActivitiesYaoHaoInfos
                                on act.Id equals yh.ActivitiesId
                                where act.IsDel == false && act.ActivitieState == 1 && act.Type == 1
                                select new
                                {
                                    act.Id,
                                    act.DeveloperId,
                                    act.Name,
                                    yh.ActivitieLocation,
                                    act.Type,
                                    act.UserCount,
                                    act.HouseCount,
                                    act.BidPrice,
                                    act.AddPrice,
                                    act.MaxPrice,
                                    //yh.SignupBeginTime,
                                    //yh.SignupEndTime,
                                    act.Bond,
                                    act.ActivitieState,
                                    act.UpdateTime,
                                    act.CreateTime,
                                    act.IsDel,
                                    yh.Periods,
                                    house.PremisesId,
                                    yh.NotarialOffice,
                                    yh.Introduction,
                                    act.BeginTime
                                };

                    var it = query.FirstOrDefault();
                    ActivityList list = null;
                    if (it != null)
                    {
                        list = new ActivityList
                        {
                            ActivitiesId = it.Id,
                            Name = it.Name,
                            ActivitieLocation = it.ActivitieLocation,
                            Type = it.Type,
                            UserCount = it.UserCount,
                            HouseCount = it.HouseCount,
                            BidPrice = it.BidPrice,
                            AddPrice = it.AddPrice,
                            MaxPrice = it.MaxPrice,
                            //SignupBeginTime = it.SignupBeginTime,
                            //SignupEndTime = it.SignupBeginTime,
                            Bond = it.Bond,
                            ActivitieState = it.ActivitieState,
                            UpdateTime = it.UpdateTime,
                            CreateTime = it.CreateTime,
                            IsDel = it.IsDel,
                            PremisesId = it.PremisesId,
                            Periods = it.Periods,
                            NotarialOffice = it.NotarialOffice,
                            Introduction = it.Introduction,
                            BeginTime = it.BeginTime
                        };
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /// <summary>
        /// 跟据活动Id取摇号活动详情
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActivityList GetActivityById(int id)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = from act in newhouseDb.Activities
                                join house in newhouseDb.ActivitiesHouses
                                on act.Id equals house.ActivitiesId
                                join yh in newhouseDb.ActivitiesYaoHaoInfos
                                on act.Id equals yh.ActivitiesId
                                where act.IsDel == false && act.Type == 1 && act.Id == id
                                select new
                                {
                                    act.Id,
                                    act.DeveloperId,
                                    act.Name,
                                    yh.ActivitieLocation,
                                    act.Type,
                                    act.UserCount,
                                    act.HouseCount,
                                    act.BidPrice,
                                    act.AddPrice,
                                    act.MaxPrice,
                                    //yh.SignupBeginTime,
                                    //yh.SignupEndTime,
                                    act.Bond,
                                    act.ActivitieState,
                                    act.UpdateTime,
                                    act.CreateTime,
                                    act.BeginTime,
                                    act.IsDel,
                                    yh.Periods,
                                    house.PremisesId,
                                    yh.NotarialOffice,
                                    yh.Introduction,
                                    yh.ChooseHouseTime,
                                    yh.Notice
                                };

                    var it = query.FirstOrDefault();
                    ActivityList list = null;
                    if (it != null)
                    {
                        list = new ActivityList
                        {
                            ActivitiesId = it.Id,
                            Name = it.Name,
                            ActivitieLocation = it.ActivitieLocation,
                            Type = it.Type,
                            UserCount = it.UserCount,
                            HouseCount = it.HouseCount,
                            BidPrice = it.BidPrice,
                            AddPrice = it.AddPrice,
                            MaxPrice = it.MaxPrice,
                            //SignupBeginTime = it.SignupBeginTime,
                            //SignupEndTime = it.SignupBeginTime,
                            Bond = it.Bond,
                            ActivitieState = it.ActivitieState,
                            UpdateTime = it.UpdateTime,
                            CreateTime = it.CreateTime,
                            IsDel = it.IsDel,
                            PremisesId = it.PremisesId,
                            Periods = it.Periods,
                            NotarialOffice = it.NotarialOffice,
                            Introduction = it.Introduction,
                            BeginTime = it.BeginTime,
                            ChooseHouseTime = it.ChooseHouseTime,
                            Notice = it.Notice
                        };
                    }
                    return list;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 活动订单相关

        /// <summary>
        /// 获取当天订单数量
        /// author: maboxun
        /// </summary>
        /// <returns></returns>
        public int GetDayOrderCount()
        {
            using (var newhouseDb = new kyj_NewHouseDBEntities())
            {
                try
                {
                    DateTime date = DateTime.Now.Date;
                    return newhouseDb.Orders.Where(o => o.CreateTime >= date).Count() + 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 生成充值编号
        /// type 1:摇号支付，2：砍价、竞价保证金支付，3：团购，4：排名购房，5：秒杀、一口价、打折
        /// author: maboxun
        /// </summary>
        /// <param name="MaxId"></param>
        /// <returns></returns>
        public string GetRechargeNo(string MaxId, int type)
        {
            return "16" + (type < 9 ? ("0" + type) : type.ToString()) + DateTime.Now.ToString("yyMMdd") + MaxId.PadLeft(6, '0');
        }

        /// <summary>
        /// 提交前台活动订单
        /// author:maboxun
        /// </summary>
        /// <param name="order">活动订单</param>
        /// <returns></returns>
        public int AddActivityOrder(TXOrm.Order order)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    TXOrm.Activity _activity = new TXDal.HouseData.PremisesDal().GetHouseActivityById(order.ActivitiesId);

                    //生成出价单号
                    int type = 0;
                    switch (_activity.Type)
                    {
                        //1:摇号支付，2：砍价、竞价保证金支付，3：团购，4：排名购房，5：秒杀、一口价、打折
                        case 1:
                            type = 1;
                            break;
                        case 2:
                            type = 5;
                            break;
                        case 3:
                            type = 0;
                            break;
                        case 4:
                            type = 3;
                            break;
                        case 5:
                            type = 2;
                            break;
                        case 6:
                            type = 2;
                            break;
                        case 7:
                            type = 5;
                            break;
                        case 8:
                            type = 5;
                            break;
                    }

                    order.OrderNo = GetRechargeNo(GetDayOrderCount().ToString(), type);

                    string sql = @"declare @BidCount int;
                                   select @BidCount=count(0)+1 from [BidPrice] where [ActivitiesId]=@ActivitiesId and [BidUserId]=@BidUserId and [IsDel]=0;
                            INSERT INTO [Order](
                                   [ActivitiesId],[HouseId], [BidUserId], [BidUserName],[BidRealName],[IDCard],[BidUserMobile],[BidUserQQ],
                                   [BidUserMail],[InnerCode],[BidUserPrice],[BidUserNumber],[BidCount],[OrderNo],[Desc],[BondStatus],
                                   [Status],[CreateTime], [UpdateTime], [IsDel]) 
                        values(@ActivitiesId, @HouseId, @BidUserId, @BidUserName, @BidRealName, @IDCard, @BidUserMobile, @BidUserQQ,
                               @BidUserMail, @InnerCode, @BidUserPrice, @BidUserNumber, @BidCount,@OrderNo,@Desc,@BondStatus,
                               @Status, getdate(), getdate(), @IsDel);";

                    var sqlparms = new object[]
                    {
                        new SqlParameter("@ActivitiesId",order.ActivitiesId),
                        new SqlParameter("@HouseId",order.HouseId),
                        new SqlParameter("@BidUserId",order.BidUserId),
                        new SqlParameter("@BidUserName",order.BidUserName),
                        new SqlParameter("@BidRealName",order.BidRealName),
                        new SqlParameter("@IDCard",order.IDCard),
                        new SqlParameter("@BidUserMobile",order.BidUserMobile),
                        new SqlParameter("@BidUserQQ",order.BidUserQQ),
                        new SqlParameter("@BidUserMail",order.BidUserMail),
                        new SqlParameter("@InnerCode",order.InnerCode),
                        new SqlParameter("@BidUserPrice",order.BidUserPrice),
                        new SqlParameter("@BidUserNumber",order.BidUserNumber),
                        new SqlParameter("@OrderNo",order.OrderNo),
                        new SqlParameter("@Desc",order.Desc),
                        new SqlParameter("@BondStatus",order.BondStatus),
                        new SqlParameter("@Status",order.Status),
                        new SqlParameter("@IsDel",order.IsDel)
                    };

                    int result = newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);
                    return result;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", order, e);
                throw;
            }
        }

        /// <summary>
        /// 根据id获得活动订单
        /// author: maboxun
        /// </summary>
        /// <param name="orderID">订单id</param>
        /// <returns></returns>
        public TXOrm.Order GetActivityOrderByID(int orderID)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = newhouseDb.Orders.Where(o => o.Id == orderID);
                    return query.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", orderID, e);
                throw;
            }
        }

        /// <summary>
        /// 获得用户参加某活动的订单
        /// author: maboxun
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="activitiesID"></param>
        /// <returns></returns>
        public List<TXOrm.Order> GetActivityOrderByUserID(int userID, int activitiesID)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    //查找匹配用户ID、活动ID、未删除 的活动订单信息
                    var query = newhouseDb.Orders.Where(o => o.ActivitiesId == activitiesID && o.BidUserId == userID && o.IsDel == false);
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 修改订单状态
        /// author: maboxun
        /// 订单支付成功，修改订单状态，添加出价和用户参与信息到相关表
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateActivityOrderSuc(int orderID)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var order = newhouseDb.Orders.Where(o => o.Id == orderID).FirstOrDefault();
                    if (order == null)
                    {
                        return 0;
                    }
                    else
                    {
                        //修改订单状态
                        string sql = @"update [Order] set BondStatus=1,[UpdateTime]=getdate() where [Id]=@Id;";

                        //获取活动结束时间、活动保证金
                        sql += @"declare @OperPrice int;declare @EndTime datetime;declare @ActivityType nvarchar(10);
                                 select @OperPrice=Bond,@EndTime=dateadd(d,7,EndTime),@ActivityType=(case [Type] when 5 then '竞价' when 6 then '砍价' else '' end)
                                 from [Activities] where [Id]=@ActivityID;";

                        //添加金额定时服务记录
                        sql += @"insert into [AmountTiming]([ActivityID], [OperId], [UserId], [Type],[AmountRecord], [OperPrice], [EndTime]) 
                                 values(@ActivityID,@Id,@BidUserId,1,('新房-房源'+cast(@HouseId as varchar(10))+@ActivityType+'保证金返还'),@OperPrice,@EndTime);";

                        var sqlparms = new object[]
                        {
                            new SqlParameter("@Id",orderID),
                            new SqlParameter("@ActivityID",order.ActivitiesId),
                            new SqlParameter("@BidUserId",order.BidUserId),
                            new SqlParameter("@HouseId", order.HouseId),
                        };

                        return newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", orderID, e);
                return 0;
            }
        }

        /// <summary>
        /// 第三方支付完成 要写入账户流水
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int AddUserAccountInfo(int orderID)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var order = newhouseDb.Orders.Where(o => o.Id == orderID).FirstOrDefault();
                    if (order == null)
                    {
                        return 0;
                    }
                    var activity = newhouseDb.Activities.Where(a => a.Id == order.ActivitiesId).FirstOrDefault();
                    if (activity == null)
                    {
                        return 0;
                    }

                    //添加账户流水记录
                    string msg = "我要充值时写入的金额";
                    TXCommons.NHUserAccount userAccount = new TXCommons.NHUserAccount();
                    userAccount.AddUserAccountInfo(order.BidUserId, order.BidUserPrice, msg, 1, 2);

                    msg = string.Format("新房-支付房源{0}{1}保证金", order.HouseId, TXCommons.NewHouseWeb.ActivitiesType.ActivitiesTypeByNo(activity.Type));
                    userAccount.AddUserAccountInfo(order.BidUserId, order.BidUserPrice, msg, 0, 6);

                    return 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", orderID, e);
                return 0;
            }
        }




        /// <summary>
        /// 修改秒杀活动订单状态
        /// author: maboxun
        /// 秒杀活动的订单支付成功，修改订单状态、添加出价、修改房源状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateMSOrderSuc(int orderID)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var order = newhouseDb.Orders.Where(o => o.Id == orderID).FirstOrDefault();
                    if (order == null)
                    {
                        return 0;
                    }
                    else
                    {
                        //修改订单状态
                        string sql = @"update [Order] set BondStatus=1,Status=1,[UpdateTime]=getdate() where [Id]=@Id;";

                        //获取出价次数
                        sql += @"declare @BidCount int;
                                 select @BidCount=count(0)+1 from [BidPrice] where [ActivitiesId]=@ActivitiesId and [BidUserId]=@BidUserId and [IsDel]=0;";

                        //获取活动结束时间、活动保证金
                        sql += @"declare @OperPrice int;declare @EndTime datetime;declare @ActivityType nvarchar(10);
                                 select @OperPrice=Bond,@EndTime=dateadd(d,7,convert(varchar(10),getdate(),120)),@ActivityType=(case [Type] when 7 then '秒杀' when 8 then '一口价' else '' end) 
                                 from [Activities] where [Id]=@ActivitiesId;";

                        //新增一条出价记录
                        sql += @"INSERT INTO [BidPrice](
                                      [ActivitiesId],[HouseId],[BidUserId],[BidUserName],[BidRealName],[IDCard],[BidUserMobile],[BidUserPrice],
                                      [BidUserNumber],[BidUserQQ],[BidUserMail],[InnerCode],[BidCount],[Status],[CreateTime],[UpdateTime],[IsDel]) 
                                 VALUES (@ActivitiesId,@HouseId,@BidUserId,@BidUserName,@BidRealName,@IDCard,@BidUserMobile,@BidUserPrice,
                                       0,@BidUserQQ,@BidUserMail,@InnerCode,@BidCount,@Status,getdate(),getdate(), @IsDel);";
                        //添加金额定时服务记录
                        sql += @"insert into [AmountTiming]([ActivityID], [OperId], [UserId], [Type], [AmountRecord],[OperPrice], [EndTime]) 
                                 values(@ActivitiesId,@Id,@BidUserId,1,('新房-房源'+cast(@HouseId as varchar(10))+@ActivityType+'保证金返还'),@OperPrice,@EndTime);";

                        //修改房源状态为：已认购
                        sql += @"update [House] set [SalesStatus]=3,[UpdateTime]=getdate() where [Id]=@HouseId;";

                        //修改活动状态为已结束
                        sql += @"update Activities set ActivitieState=2 where Id=@ActivitiesId;";

                        //修改活动定时服务 订单号和状态
                        sql += @"update [ActivitiesTiming] set [OperId]=@Id, [Status]=2 where [HouseId]=@HouseId and [ActivityID]=@ActivitiesId;";


                        var sqlparms = new object[]
                        {
                            new SqlParameter("@Id",orderID),
                            new SqlParameter("@ActivitiesId", order.ActivitiesId),
                        new SqlParameter("@HouseId", order.HouseId),
                        new SqlParameter("@BidUserId", order.BidUserId),
                        new SqlParameter("@BidUserName", order.BidUserName),
                        new SqlParameter("@IDCard", order.IDCard),
                        new SqlParameter("@BidUserPrice", order.BidUserPrice),
                        new SqlParameter("@BidUserQQ", order.BidUserQQ),
                        new SqlParameter("@BidUserMobile", order.BidUserMobile),
                        new SqlParameter("@BidUserMail", order.BidUserMail),
                        new SqlParameter("@InnerCode", ""),
                        new SqlParameter("@Status", 1),
                        new SqlParameter("@BidRealName", order.BidRealName),
                        new SqlParameter("@IsDel", false)
                        };

                        newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);

                        //发送楼盘消息
                        var premise = newhouseDb.Premises.Join(newhouseDb.Houses.Where(h => h.Id == order.HouseId), p => p.Id, h => h.PremisesId, (p, h) => p).FirstOrDefault();
                        if (premise != null)
                        {
                            TXCommons.MsgQueue.SendMessage.SendPremisesIndexMessage("update", premise.Id, premise.CityId);//楼盘消息
                        }

                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", orderID, e);
                return 0;
            }
        }


        /// <summary>
        /// 修改订单中相关用户信息
        /// author: maboxun
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderUserInfo(TXOrm.Order order)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"update [Order] set [BidUserName]=@BidUserName, [BidRealName]=@BidRealName, [IDCard]=@IDCard, [BidUserMobile]=@BidUserMobile, 
                                   [BidUserQQ]=@BidUserQQ, [BidUserMail]=@BidUserMail, [BidUserPrice]=@BidUserPrice,[Status]=@Status, [UpdateTime]=getdate() where [Id]=@Id;";

                    var sqlparms = new object[]
                    {
                        new SqlParameter("@Id",order.Id),
                        new SqlParameter("@BidUserName", order.BidUserName),
                        new SqlParameter("@BidRealName", order.BidRealName),
                        new SqlParameter("@IDCard", order.IDCard),
                        new SqlParameter("@BidUserMobile", order.BidUserMobile),
                        new SqlParameter("@BidUserQQ", order.BidUserQQ),
                        new SqlParameter("@BidUserMail", order.BidUserMail),
                        new SqlParameter("@BidUserPrice", order.BidUserPrice),
                        new SqlParameter("@Status", order.Status)
                    };

                    return newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", order, e);
                return 0;
            }
        }

        #endregion

        #region 秒杀相关

        /// <summary>
        /// 添加 唯一活动房源（添加成功失败状态视为秒杀状态）
        /// author: maboxun
        /// </summary>
        /// <returns></returns>
        public int AddUniqueActivitiesHouse(TXOrm.UniqueActivitiesHouse uaHouse)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    //查询是否有当前房源的数据(如果有直接返回0，不再插入，否则插入新数据)
                    int count = newhouseDb.UniqueActivitiesHouses.Where(u => u.HouseId == uaHouse.HouseId).Count();
                    if (count > 0)
                        return 0;


                    string sql = @"insert into [UniqueActivitiesHouse]([HouseId], [CreateTime]) values(@HouseId,getdate())";

                    var sqlparms = new object[]
                    {
                        new SqlParameter("@HouseId",uaHouse.HouseId)
                    };

                    newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", uaHouse, e);
                return 0;
            }
        }

        /// <summary>
        /// 添加 活动定时服务
        /// author: maboxun
        /// </summary>
        /// <param name="activitiesTiming">活动定时服务</param>
        /// <param name="msOperatingTime">秒杀操作处理时间</param>
        /// <returns></returns>
        public int AddActivitiesTiming(TXOrm.ActivitiesTiming activitiesTiming, int msOperatingTime)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"insert into [ActivitiesTiming]([ActivityID],[HouseId], [OperId], [Type], [Status], [EndTime])
                                   values(@ActivityID,@HouseId, @OperId, @Type, @Status,dateadd(mi,@msOperatingTime,getdate()));";

                    var sqlparms = new object[]
                    {
                        new SqlParameter("@ActivityID",activitiesTiming.ActivityID),
                        new SqlParameter("@HouseId",activitiesTiming.HouseId),
                        new SqlParameter("@OperId",activitiesTiming.OperId),
                        new SqlParameter("@Type",activitiesTiming.Type),
                        new SqlParameter("@Status",activitiesTiming.Status),
                        new SqlParameter("@msOperatingTime",msOperatingTime)
                    };

                    newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", activitiesTiming, e);
                return 0;
            }
        }

        /// <summary>
        /// 获取该活动某个订单是否有定时服务
        /// author: maboxun
        /// </summary>
        /// <param name="activitiesTiming"></param>
        /// <returns></returns>
        public bool HasActivitiesTimingByOrder(TXOrm.ActivitiesTiming activitiesTiming)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    //查找匹配用户ID、活动ID、未删除 的活动订单信息
                    var query = newhouseDb.ActivitiesTimings.Where(a => a.ActivityID == activitiesTiming.ActivityID && a.OperId == activitiesTiming.OperId);
                    return query.Count() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据房源id 修改 房源定时服务
        /// author: maboxun
        /// </summary>
        /// <returns></returns>
        public int UpdateHouseTimingByHouseID(TXOrm.ActivitiesTiming activitiesTiming)
        {
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    string sql = @"update [ActivitiesTiming] set [OperId]=@OperId, [Status]=@Status, [EndTime]=@EndTime where [HouseId]=@HouseId;";
                    var sqlparms = new object[]
                    {
                        new SqlParameter("@HouseId",activitiesTiming.HouseId),
                        new SqlParameter("@OperId",activitiesTiming.OperId),
                        new SqlParameter("@Status",activitiesTiming.Status),
                        new SqlParameter("@EndTime",activitiesTiming.EndTime)
                    };

                    newhouseDb.ExecuteStoreCommand(sql.ToString(), sqlparms);
                    return 1;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", activitiesTiming, e);
                return 0;
            }
        }



        #endregion


        #region 获取活动出价次数、参与人数

        /// <summary>
        /// 获取活动出价次数、参与人数
        /// author: maboxun
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList GetActivityBidNum(int id)
        {
            ArrayList arr = new ArrayList();
            try
            {
                using (var newhouseDb = new kyj_NewHouseDBEntities())
                {
                    var query = new
                    {
                        bidCount = newhouseDb.BidPrices.Where(b => b.ActivitiesId == id && b.IsDel == false).Count(),
                        bidPerson = newhouseDb.BidPrices.Where(b => b.ActivitiesId == id && b.IsDel == false).Select(b => b.BidUserId).Distinct().Count()
                    };
                    arr.Add(query.bidCount);
                    arr.Add(query.bidPerson);
                }
                return arr;
            }
            catch (Exception)
            {
                return arr;
            }
        }
        #endregion
    }
}
