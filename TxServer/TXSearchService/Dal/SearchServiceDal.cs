using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TXSearchService.Dal
{
    public class SearchServiceDal
    {
        string ConnectionString = "";
        public SearchServiceDal(string Connstring)
        {
            ConnectionString = Connstring;
        }
     

        //#region 以前服务
        //#region   处理房源出价待处理状态
        ///// <summary>
        ///// 返回出价中房源
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetHouseStatus(string cityid)
        //{
        //    string sql = "  select Id from S_LongHouse(nolock) where BidStartTime<=getdate() and BidEndTime>getdate() and PType!=1 and BidStatus=0 and IsDel=0 " + " and CityId in (" + cityid + ") ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 修改房源出价状态
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <param name="bidStatus"></param>
        //public void UpdateHouseStatus(string houseid, int bidStatus)
        //{
        //    string str = "";
        //    if (bidStatus == 3 || bidStatus == 4)
        //        str = ",[state]=0";
        //    string sql = " update S_LongHouse set  BidStatus=" + bidStatus + str + "  where id=" + houseid;

        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 返回出价已结束数据
        ///// </summary>
        ///// <param name="ptype"></param>
        ///// <returns></returns>
        //public DataSet GetHouseStatus(int ptype, string cityid)
        //{
        //    string sql = " select Id,VName,Room,CityName,DName from S_LongHouse(nolock) where  BidEndTime<=getdate() and PType=" + ptype + " and (BidStatus=0 or BidStatus=1) and IsDel=0  "
        //                + " and CityId in (" + cityid + ") ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}
      
       

        ///// <summary>
        ///// 返回置顶到期房源
        ///// </summary>
        ///// <param name="cityid"></param>
        ///// <returns></returns>
        //public DataSet GetHouseIsRecommend(string cityid)
        //{
        //    string sql = " select Id,VName,Room,CityName,DName from S_LongHouse(nolock) where  RecTime<=getdate() and IsRecommend=1 and IsDel=0 "
        //                 + " and CityId in (" + cityid + ") ";
        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}
       
       
       

        // /// <summary>
        //  /// 自动退还保证金
        ///// </summary>
        ///// <param name="cityid"></param>
        ///// <returns></returns>
        //  public DataSet GetHouseIsBack(string cityid)
        // {
        //     string sql = " select a.id,a.AllPrice,a.DayNum,s.uid as UserId from  ARent a join s_LongHouse s on a.houseid=s.id where  a.ETime<getdate() and a.IsPay=1 and AucState=2 and  a.IsBack=0 and a.IsDel=0 ";
                       
        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //  }
        ///// <summary>
        //  /// 自动退还保证金
        ///// </summary>
        ///// <param name="houseid"></param>
        //  public void UpdateIsBack(string id)
        //  {
        //      string sql = " update ARent set IsBack=1,BackTime=getdate() where id=" + id;

        //      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //  }


      


        //   /// <summary>
        //  /// 打开房租支付开口
        //  /// </summary>
        //  /// <param name="cityid"></param>
        //  /// <returns></returns>
        //  public DataSet GetHouseIsOpenHouse(string cityid)
        //  {
        //      string sql = "  select ac.houseid from ARentContract ac join ARentPayment ap on ac.Id=ap.ARCId  "+
        //                   "  where ac.state=4 and ac.IsOpen=0 and ap.IsOpen=0 and ap.AucState=0 and ap.State=0 and ap.ZTime<=dateadd(day,15,getdate())  "+
        //                   "  group by ac.houseid ";

        //      return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //  }

        //  /// <summary>
        //  /// 打开房租支付开口
        //  /// </summary>
        //  /// <param name="cityid"></param>
        //  /// <returns></returns>
        //  public DataSet GetHouseIsOpen(string houseid)
        //  {
        //      string sql = " select ac.id as cid,ap.id as pid,ap.ZTime as time from ARentContract ac join ARentPayment ap on ac.Id=ap.ARCId  "+
        //                   "  where ac.state=4 and ac.IsOpen=0 and ap.IsOpen=0 and ap.AucState=0 and ap.State=0 and ap.ZTime<=dateadd(day,15,getdate()) and ac.houseid= "+houseid;

        //      return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //  }
        //  /// <summary>
        //  /// 打开房租支付开口
        //  /// </summary>
        //  /// <param name="houseid"></param>
        //  public void UpdateIsOpenC(string id,string time)
        //  {
        //      string sql = " update ARentContract set  IsOpen=1, ZTime='" + time + "' where  id=" + id;

        //      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //  }

        //  /// <summary>
        //  /// 打开房租支付开口
        //  /// </summary>
        //  /// <param name="houseid"></param>
        //  public void UpdateIsOpenP(string id)
        //  {
        //      string sql = " update ARentPayment set  IsOpen=1 where Type=0 and AucState=0 and id=" + id;

        //      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //  }
        ///// <summary>
        //  /// 到期返回保证金
        ///// </summary>
        ///// <param name="cityid"></param>
        ///// <returns></returns>
        //  public DataSet GetHouseIsLock(string cityid)
        //  {
        //      string sql = " select ar.id from ARentContract ac join ARent ar on  ac.HouseId=ar.HouseId "+
        //                  "  where ac.state=4 and ar.IsLock=1 and ar.Createtime> dateadd(day,180,getdate()) ";

        //      return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //  }
        //  /// <summary>
        //  /// 到期返回保证金
        //  /// </summary>
        //  /// <param name="cityid"></param>
        //  /// <returns></returns>
        //  public void UpdateIsLock(string id)
        //  {
        //      string sql = " update ARent set IsLock=0 where id=" + id;

        //      SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //  }

       
        ///// <summary>
        ///// 修改游戏出价开始
        ///// </summary>
        //public int UpdateGameStateBegin()
        //{
        //    string sql = " update game set state=0 where BeginTime<=getdate() and EndTime>getdate() and state!=0" ;

        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 修改游戏出价结束
        ///// </summary>
        //public int UpdateGameStateEnd()
        //{
        //    string sql = " update game set state=2 where   EndTime<=getdate() and state!=2";

        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
       

        ///// <summary>
        ///// 返回房源出价总数量
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <returns></returns>
        //public int GetBidCount(string houseid)
        //{
        //    string sql = " select BidUserNum from S_LongHouse where Id=" + houseid + " and IsDel=0  ";

        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql));
        //}

        ///// <summary>
        ///// 返回房源出价有效数量
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <returns></returns>
        //public int GetBiddingCount(string houseid)
        //{
        //    string sql = " select count(id) from S_LongHouseBidPrice where ShId=" + houseid + " and IsDel=0 and [order]<10000  ";

        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql));
        //}
        ///// <summary>
        ///// 返回中标最少参与人数
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <returns></returns>
        //public int GetMinSincerityCount(string houseid)
        //{
        //    string sql = " select MinSincerityCount from S_LongHouseOther where SLHId=" + houseid;

        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql));
        //}
       
        ///// <summary>
        ///// 返回总公司
        ///// </summary>
        ///// <param name="acid"></param>
        ///// <returns></returns>
        //public int GetCAgent(int acid)
        //{
        //    string sql = " select CPId from U_AgentComPany where  id=" + acid;
        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql));
        //}
        ///// <summary>
        /////  修改砍价中标
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <param name="Status"></param>
        ///// <returns></returns>
        //public int UpdateBid(string Id)
        //{
        //    string sql = " Update   S_LongHouseBidPrice set Status=2 where Id=" + Id;

        //    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql));
        //}
        ///// <summary>
        ///// 返回已中标砍价数据
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <returns></returns>
        //public DataSet GetBid(string houseid)
        //{
        //    string sql = " select  top 1 * from S_LongHouseBidPrice where  IsDel=0  and ShId=" + houseid + " order by [Order]  ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}

        //public DataSet GetUpBid(string houseid)
        //{
        //    string sql = " select  top 1 * from S_LongHouseBidPrice where  IsDel=0  and ShId=" + houseid + " order by BidPrice desc,CreateTime  ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 添加站内信
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="title"></param>
        ///// <param name="content"></param>
        //public string InsertUserMessage(string userid, string title, string content)
        //{
        //    string message = "发送成功:title:" + title + "_content:" + content;
        //    try
        //    {
        //        string sql = " INSERT INTO UserMessage " +
        //                   " ([SendUserId] " +
        //                     " ,[ReceiveUserId] " +
        //                    " ,[Platform] " +
        //                       " ,[Title] " +
        //                     " ,[Content] " +
        //                     " ,[IsRead] " +
        //                     "  ,[CreateTime] " +
        //                     " ,[IsDel] " +
        //                    " ,[MessageType] " +
        //                    " ,[MessageSource] " +
        //                      " ,[SourceCount] " +
        //                      " ) " +
        //             "  VALUES " +
        //                     "  (0 " +
        //                    "   , " + userid +
        //                     "  ,'FC' " +
        //                     "  ,'" + title + "' " +
        //                     "   ,'" + content + "' " +
        //                    "   ,0 " +
        //                     "  ,getdate() " +
        //                    "   ,0 " +
        //                     "  ,2 " +
        //                   "   ,1 " +
        //                    "   ,0 " +
        //                   "   ) ";
        //        SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }
        //    return message;
        //}

        //#endregion


        //#region   小区，区域，地铁相关操作

      
       
        //#endregion


        //#region   诚意金,保证金
        ///// <summary>
        ///// 返回经纪人退还保证金
        ///// </summary>
        //public DataSet GetReturnGuarantee(string houseid)
        //{
        //    string sql = @" select * from U_Guarantee where HouseId=" + houseid + " and IsReturn=0 and IsDel=0 ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}

        ///// <summary>
        ///// 更新退还保证金
        ///// </summary>
        //public void UpdateReturnGuarantee(int id)
        //{
        //    string sql = " update U_Guarantee set IsReturn=1 where Id=" + id;
        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}

        ///// <summary>
        ///// 更新经纪人退还保证金
        ///// </summary>
        //public void UpdateReturnGuarantee(int id, int userid, decimal price)
        //{
        //    string sql = " begin tran " +
        //                 " set xact_abort on " +
        //                  " update A_Account set Price=Price+" + price + ",UpdateTime=getdate() " +
        //                  " where UserID=" + userid +

        //                  " update U_Guarantee set IsReturn=1 where Id=" + id +

        //                   " Insert A_ConsumeLog (A_Id,Price,[Type],PayType,[Desc],CreateTime) " +
        //                   " Values (" + userid + "," + price + ",6,0,'退还经纪人保证金',getdate())" +
        //                 " commit tran ";

        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 返回个人诚意金数据
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetReturnSincerity(string houseid)
        //{
        //    string sql = @" select * from U_Sincerity where HouseId=" + houseid + " and IsReturn=0 and UserType=0 and IsDel=0";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 更新个人退还诚意金
        ///// </summary>
        ///// <param name="id"></param>
        //public void UpdateReturnSincerity(int id)
        //{
        //    string sql = " update U_Sincerity set IsReturn=1 where Id=" + id;
        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 返回砍价房个人退还诚意金数据
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetBidReturnSincerity(string houseid)
        //{
        //    string sql = @" select * from U_BidSincerity where HouseId=" + houseid + " and IsReturn=0 and UserType=0 and IsDel=0";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}

        ///// <summary>
        ///// 更新砍价房个人退还诚意金
        ///// </summary>
        ///// <param name="id"></param>
        //public void UpdateBidReturnSincerity(int id)
        //{
        //    string sql = " update U_BidSincerity set IsReturn=1 where Id=" + id;
        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        //#endregion

        //#region 经纪人服务到期
        ///// <summary>
        ///// 返回经纪人服务到期数据
        ///// </summary>
        //public DataSet GetAgentService()
        //{
        //    string sql = @" select * from AgentService as a where dateadd(day,7,a.EndTime)<getdate() and [State]=1 and IsDel=0 ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 经纪人服务跟新
        ///// </summary>
        //public void UpdateAgentService(int id)
        //{
        //    string sql = @" update AgentService Set [State]=2 where Id=" + id;

        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}

        ///// <summary>
        ///// 返回替换经纪人本月服务数据
        ///// </summary>
        //public DataSet GetWillAgentService()
        //{
        //    string sql = @" select * from AgentService as a where a.BeginTime<=getdate() and  a.EndTime>getdate() and [State]=5 and IsDel=0 ";

        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 经纪人服务替换
        ///// </summary>
        //public void UpdateWillAgentService(int id, int userid)
        //{
        //    string sql = @" update AgentService Set [State]=2  where A_Id=" + userid + " and State=1 and IsDel=0 " +
        //                 " update AgentService Set [State]=1 where Id =" + id;

        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}

        //#endregion

        //#region 平台赠送金币
        ///// <summary>
        ///// 平台赠送经纪人金币
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetAgentGoldCoin()
        //{
        //    string sql = "select Id from U_Agent where VcationalCetification=1 and IsDel=0 ";
        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}

        ///// <summary>
        ///// 返回平台赠送个人用户金币
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetUserGoldCoin()
        //{
        //    string sql = "select Id from [User] where IsDel=0 and [Platform]='FC' ";
        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        //}

        //#endregion

        //#region 账户操作
        ///// <summary>
        ///// 修改经纪人账户
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="price"></param>
        //public void UpdateA_Account(int userid, decimal price)
        //{
        //    string sql = " update A_Account set Price=Price+" + price + ",UpdateTime=getdate() " +
        //                " where UserID=" + userid;



        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 添加经纪人日志
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="price"></param>
        //public void AddA_ConsumeLog(int userid, decimal price, int type, string message)
        //{
        //    string sql = " Insert A_ConsumeLog (A_Id,Price,[Type],PayType,[Desc],CreateTime) " +
        //               " Values (" + userid + "," + price + "," + type + ",0,'" + message + "',getdate())";


        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 修改经纪人账户事务操作
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="price"></param>
        ///// <param name="type"></param>
        ///// <param name="message"></param>
        //public string UpdateA_AccountTransaction(int userid, decimal price, int type, string message)
        //{
        //    try
        //    {
        //        string sql = " begin tran " +
        //                    " set xact_abort on " +
        //                    " update A_Account set Price=Price+" + price + ",UpdateTime=getdate() " +
        //                    " where UserID=" + userid +
        //                    " Insert A_ConsumeLog (A_Id,Price,[Type],PayType,[Desc],CreateTime) " +
        //                   " Values (" + userid + "," + price + "," + type + ",0,'" + message + "',getdate()) " +
        //                    " commit tran ";

        //        SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //        return "1";
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}


        ///// <summary>
        ///// 修改个人账户记录
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="price"></param>
        //public void UpdateU_Account(int userid, decimal price)
        //{
        //    string sql = " update U_Account set Price=Price+" + price + ",UpdateTime=getdate() " +
        //                 " where UserID=" + userid;



        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}



        ///// <summary>
        ///// 添加个人操作日志
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="price"></param>
        //public void AddU_AccountLog(int userid, decimal price, int LogType, string message)
        //{
        //    string sql = " Insert U_AccountLog (UserId,UserType,LogType,Type,Price,[Desc],CreateTime) " +
        //              " Values (" + userid + ",0," + LogType + ",1," + price + ",'" + message + "',getdate())";


        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 修改个人人账户事务操作
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="price"></param>
        ///// <param name="LogType"></param>
        ///// <param name="message"></param>
        //public string UpdateU_AccountTransaction(int userid, double price, int LogType, string message)
        //{
        //    try
        //    {
        //        string sql = " begin tran " +
        //                  " set xact_abort on " +
        //                  " update U_Account set Price=Price+" + price + ",UpdateTime=getdate()"+
        //                   " where UserID=" + userid +
        //                    " Insert U_AccountLog (UserId,UserType,LogType,Type,Price,[Desc],CreateTime) " +
        //                " Values (" + userid + ",0," + LogType + ",1," + price + ",'" + message + "',getdate()) " +
        //                 " commit tran ";
        //        SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //        return "1";
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}
        //#endregion
       
        //#region 预约刷新
        ///// <summary>
        ///// 返回需要刷新数据
        ///// </summary>
        ///// <param name="ConnectionString"></param>
        ///// <returns></returns>
        //public DataSet GetTxRefurbishTimingData(DateTime date, string cityid)
        //{

        //    //"cityid ="+cityid[0]+" or cityid="+cityid[1]+""
        //    //  select RefurbishEndCount from U_UserOther where UserType=1 and UserID= 
        //    string sql = " select r.houseid,r.userid, "+
        //                 " isnull(( select top 1 ro.RefurbishEndCount  from U_UserOther(nolock) ro where ro.UserType=1 and ro.UserID=r.UserID ),0) as rcount, " +
        //                 " isnull((select top 1 us.ACount from U_Service us where us.Id= (select top 1 a.S_Id From AgentService(nolock) a  where a.A_Id=r.UserID  and a.State=1 and a.IsDel=0)),10) as acount" +
        //                " from  Refurbish(nolock) r join u_Agent(nolock) a on r.UserID=a.Id " +
        //                 "  where r.EndTime>='" + date.ToShortDateString() + "' and r.[Hour]=" + date.Hour + " and r.[Minute]=" + date.Minute +
        //                 " and ( select s.CityId from s_LongHouse(nolock) s where s.Id=r.houseid ) in (" + cityid + ") ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 返回经纪人服务允许操作数
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        //public int GetServiceCount(int userid)
        //{
        //    int count = 10;
        //    string sql = "select ACount from U_Service where Id= (select top 1 S_Id From AgentService a  where a.A_Id=" + userid + " and State=1 and IsDel=0) ";

        //    object o = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql);
        //    if (o != null)
        //    {
        //        if (o.ToString() != "")
        //        {
        //            count = int.Parse(o.ToString());
        //        }
        //    }
        //    return count;
        //}
        ///// <summary>
        /////  返回经纪人已操作数
        ///// </summary>
        ///// <param name="agentid"></param>
        ///// <returns></returns>
        //public int GetRefurbishCount(int agentid)
        //{
        //    string sql = "  select RefurbishEndCount from U_UserOther where UserType=1 and UserID= " + agentid;
        //    object o = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql);
        //    return Convert.ToInt32(o);
        //}
        ///// <summary>
        ///// 经纪人刷新成功
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="houseid"></param>
        ///// <param name="date"></param>
        //public void RefurbishR(int userid, int houseid, DateTime date)
        //{
        //    string sql = " Update U_UserOther set RefurbishEndCount=RefurbishEndCount+1 where UserType=1 and UserID=" + userid +
        //                 "  and (select ptype from s_LongHouse where id=" + houseid + ")=1 " +

        //             " Update S_LongHouseOther set RefurbishEndCount=RefurbishEndCount+1 where SLHId=" + houseid +
        //             " Update S_LongHouse set UpdateTime='" + date.ToString() + "' where  Id =" + houseid +
        //            " INSERT INTO RefurbishLog " +
        //            "  ([UserID] " +
        //            "  ,[HouseID] " +
        //           "   ,[RTime] " +
        //          "  ,[RType] " +
        //          "   ,[IsR] " +
        //          "   ,[CreateTime]) " +
        //          "  VALUES " +
        //           "   ( " + userid +
        //              "  , " + houseid +
        //            "  , '" + date.ToString() +
        //              "'  ,1 " +
        //            "  ,1 " +
        //            "   ,getdate())";
        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 经纪人刷新失败
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="houseid"></param>
        ///// <param name="date"></param>
        //public void RefurbishF(int userid, int houseid, DateTime date)
        //{
        //    string sql = " INSERT INTO RefurbishLog " +
        //            "  ([UserID] " +
        //            "  ,[HouseID] " +
        //           "   ,[RTime] " +
        //          "  ,[RType] " +
        //          "   ,[IsR] " +
        //          "   ,[CreateTime]) " +
        //          "  VALUES " +
        //           "   ( " + userid +
        //              "  , " + houseid +
        //            "  , '" + date.ToString() +
        //              "'  ,1 " +
        //            "  ,0 " +
        //            "   ,getdate())";
        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        //#endregion

        //#region 用户操作数据归零
        ///// <summary>
        ///// 用户操作数据归零
        ///// </summary>
        //public int UpdateRefurbishEndCountDay()
        //{
        //    string sql = "Update U_UserOther set RefurbishEndCount=0";
        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 房源刷新操作数归零
        ///// </summary>
        ///// <returns></returns>
        //public int UpdateS_LongHouseOtherRefurbishEndCount()
        //{
        //    string sql = "Update S_LongHouseOther set RefurbishEndCount=0";
        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 经纪人发布标签房数归零
        ///// </summary>
        //public int UpdateRefurbishEndCountMonth()
        //{
        //    string sql = " Update U_UserOther set BargainingCount=0 where UserType=1 ";
        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 经纪人发布报价房数归零
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <returns></returns>
        //public int UpdateRefurbishEndCountMonth(string userid)
        //{
        //    string sql = " Update U_UserOther set BCount=0 where UserType=1 and UserID =" + userid;
        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        ///// <summary>
        ///// 返回本月未到期经纪人
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetEndCountMonth()
        //{
        //    string sql = " select * from AgentService as a where a.EndTime>getdate() and  IsDel=0 ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}

        //#endregion

        //#region 着急房源改为不着急
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="day"></param>
        ///// <returns></returns>
        //public DataSet GetIsWorryData(int type, int day, string cityid)
        //{
        //    string sql = " select Id,sl.WorryTime from s_LongHouse(nolock) s join S_LongHouseOther(nolock) sl " +
        //                 "  on s.Id=sl.SLHId " +
        //                "  where s.IsWorry=2 and Type=" + type + " and s.IsDel=0 and dateadd(day," + day + ",sl.WorryTime) <getdate() "
        //                 + " and CityId in (" + cityid + ") ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public int UpdateIsWorryData(string id)
        //{
        //    string sql = " update  s_LongHouse   set IsWorry=0 where Id =" + id;
        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        //#endregion

        //#region 赠送vip经纪人标签房
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="day"></param>
        ///// <returns></returns>
        //public DataSet GetBargainingCountData()
        //{
        //    string sql = " select ag.A_Id, us.BargainingCount from AgentService ag join U_Service us on  ag.S_Id=us.Id " +
        //                 "  where ag.State=1 and ag.IsDel=0 and us.IsDel=0 ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public int UpdateBargainingCountData(string id, int BargainingCount)
        //{
        //    string sql = " update  U_Agent   set BargainingCount=" + BargainingCount + " where Id =" + id;
        //    return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}

        ///// <summary>
        ///// 返回秒杀开始
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetBidBegin()
        //{
        //    string sql = " select Id from s_LongHouse(nolock) " +
        //                 "  where IsDel=0 and PType=4 and BidStatus=0 and BidStartTime<=getdate() and BidEndTime>getdate()  ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}

        ///// <summary>
        ///// 返回秒杀结束
        ///// </summary>
        ///// <returns></returns>
        //public DataSet GetBidEnd()
        //{
        //    string sql = "  select Id from s_LongHouse(nolock)  " +
        //                 "  where IsDel=0 and PType=4 and (BidStatus=1 or BidStatus=0)  and BidEndTime<=getdate()  ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 修改秒杀状态
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="status"></param>
        //public void UpdateBidBe(string id, int status)
        //{
        //    string sql = " update  s_LongHouse   set BidStatus=" + status + " where  Id =" + id;
        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}
        //#endregion
        //#region 投资回报
        ///// <summary>
        ///// 返回投资数据
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <returns></returns>
        //public DataSet GetInvestment(string houseid)
        //{
        //    string sql = "  select   *  from Investment where  IsDel=0  and Status =1  and HouseId=" + houseid;


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        ///// <summary>
        ///// 修改投资状态
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="status"></param>
        //public void UpdateInvestment(string id, int status)
        //{
        //    string sql = "update Investment set Status =" + status + "  where id=" + id;



        //    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        //}


        ///// <summary>
        ///// 返回用户数据
        ///// </summary>
        ///// <param name="houseid"></param>
        ///// <returns></returns>
        //public DataSet GetLocalUser(string userid)
        //{
        //    string sql = "    select Mobile from LocalUser where UserId=" + userid + " and IsDel=0 ";


        //    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        //}
        //#endregion

        //#endregion

        #region 最新需求

        #region 房源消息监听服务
        #region 房源索引
        /// <summary>
        /// 返回房源数据
        /// </summary>
        /// <param name="houseid"></param>
        public DataSet GetDataSetForLongHouse(string houseid)
        {

            string sql = @"select 
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
                       
                        where a.IsDel=0 and a.state=1 and PType!=4   and a.AuthenticationState=1 ";
            if (!string.IsNullOrEmpty(houseid))
            {
                sql += " and a.Id=" + houseid;
            }
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
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
        /// <summary>
        /// 根据用户返回房源数据
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="usertype"></param>
        /// <returns></returns>
        public DataSet GetDataSetForLongHouse(string userid, string usertype, string cityid)
        {

            string sql = @"select 
                        a.*
                        from s_longhouse(nolock) a
                       
                        where a.IsDel=0 and (a.state=1 or a.BidStatus=2) and PType!=4   and a.AuthenticationState=1 and UId=" + userid + " and UserType=" + usertype + " and CityId " + cityid ;

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
        /// 返回个人用户性别
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataRow GetUser(string userid)
        {
            string sql = " select Sex,Mobile,EMail,QQ,Wechat,IsAllowShow from LocalUser where UserId=" + userid;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0].Rows[0];
        }

        /// <summary>
        /// 返回是否优秀经纪人
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataRow GetAgent(string userid)
        {
            string sql = " select IsExcellent,Mobile,EMail,QQ,Wechat,IsAllowShow,AC_Id from U_Agent where Id=" + userid;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql).Tables[0].Rows[0];
        }
        #endregion

        #region 广告索引
        /// <summary>
        /// 判断是否在推广中
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns></returns>
        public DataSet pdAdvert(string houseid)
        {
            string sql = @" Select  a.*,al.AreaId as aid,al.AreaName as aname from Advert a join AdvertLog al on a.OrderNumber=al.OrderNumber   where a.IsDel=0 and a.BuyTime='" + DateTime.Now.Date + "' and a.houseid=" + houseid;
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        #endregion

        #endregion

        #region 房源索引修改服务
        #region 修改到期下架
        /// <summary>
        /// 返回报价房过期数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetHouseBao(string cityid)
        {
            string sql = " select Id,VName,Room,CityName,DName from S_LongHouse(nolock) where  BidEndTime<=getdate() and State=1 and IsRecommend=0 and IsPlat=0  and PType=1 and BidStatus!=2 and IsDel=0 "
                          + " and CityId " + cityid;
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 返回判断是否正在推广
        /// </summary>
        /// <param name="houseid"></param>
        /// <returns></returns>
        public DataSet GetHouseAdvert(string houseid)
        {
            string sql = " select top 1 buytime from advert where Houseid=" + houseid + " order by buytime desc ";
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 修改房源上下架状态
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="state"></param>
        public void UpdateHouseState(string houseid, int state)
        {
            //S_LongHouseBase
            string sql = " update S_LongHouseBase set State=" + state + " where id=" + houseid;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }

        public void UpdateHouseStateUserType(string houseid, int state)
        {
            //S_LongHouseBase
            string sql = " update S_LongHouseBase set State=" + state + " where UserType=1 and id=" + houseid;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        #endregion


        #region 房源保证金房源到期
        /// <summary>
        /// 房源失去省心租服务
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public DataSet GetHouseIsPlat(string cityid)
        {
            string sql = " select a.id,a.AllPrice,a.DayNum,s.uid as UserId,a.houseid from  ARent a join s_LongHouse s on a.houseid=s.id where  a.ETime<getdate() and a.IsPay=1 and AucState=2 and  a.IsBack=0 and a.IsDel=0 "+
                            "  and s.CityId " + cityid;
            //string sql = " select s.id , ar.id as rid from  ARent ar join s_Longhouse s on ar.houseid=s.id  " +
            //             "  where ar.IsBack=0 And ar.ETime<getdate() and ar.IsDel=0 " +
            //               " and s.CityId in (" + cityid + ") ";
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 房源失去省心租服务
        /// </summary>
        /// <param name="houseid"></param>
        public void UpdateIsPlat(string houseid)
        {
            string sql = " update S_LongHouse set IsGuarantee=0 where id=" + houseid;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 自动退还保证金
        /// </summary>
        /// <param name="houseid"></param>
        public void UpdateIsBack(string id)
        {
            string sql = " update ARent set IsBack=1,BackTime=getdate() where id=" + id;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 修改个人人账户事务操作
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="price"></param>
        /// <param name="LogType"></param>
        /// <param name="message"></param>
        public string UpdateU_AccountTransaction(int userid, double price, int LogType, string message)
        {
            try
            {
                string sql = " begin tran " +
                          " set xact_abort on " +
                          " update U_Account set Price=Price+" + price + ",UpdateTime=getdate()" +
                           " where UserID=" + userid +
                            " Insert U_AccountLog (UserId,UserType,LogType,Type,Price,[Desc],CreateTime) " +
                        " Values (" + userid + ",0," + LogType + ",1," + price + ",'" + message + "',getdate()) " +
                         " commit tran ";
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
                return "1";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region 房源推广服务
        /// <summary>
        /// 修改推广状态
        /// </summary>
        /// <returns></returns>
        public DataSet GetHouseAdvertIsRecommend(string cityid)
        {
            string sql = " select id,UserType,Mobile from s_LongHouse where IsRecommend=1 and RecTime<getdate() " + " and CityId " + cityid  ;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        public DataSet GetHouseAdvertIsRecommendUserType(string cityid)
        {
            string sql = " select id,UserType,Mobile from s_LongHouse where UserType=1 and state=0 and IsRecommend=1 and RecTime>getdate() " + "and CityId " + cityid;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        public int UpdateIsRecommend(string houseid, int isr)
        {
            //S_LongHouseBase
            string sql = " update S_LongHouseBase set IsRecommend=" + isr + " where  id=" + houseid;

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 返回推广中房源Id
        /// </summary>
        /// <returns></returns>
        public DataSet GetHouseIsAdvert(string cityid)
        {
            string sql = @" Select  sh.id as id from Advert a join s_LongHouse sh on a.houseid=sh.id  " +
                         "  where a.IsDel=0 and a.BuyTime='" + DateTime.Now.Date + "' " +
                           " and sh.CityId  " + cityid;


            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        public int GetAdvertCount(string houseid)
        {
            string sql = "select top 1 Id from Advert where   BuyTime>=GETDATE() and HouseId=" + houseid;
            object o = SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql);
            if (o != null)
                return 1;
            else
                return 0;
        }
        #endregion 

        #region 最新服务
        public DataSet GetHouseNew1(string cityid)
        {
            string sql = " select hs.HouseId,max(hs.Createtime) as createtime from housestate as hs " +
                       " inner join s_longhouse as slh on slh.id=hs.HouseId  " +
                        " where slh.isreal=0 and slh.isdel=0 and getdate()>dateadd(day,3,hs.Createtime)  " +
                         " and slh.CityId " + cityid  +
                      "  group by hs.HouseId,slh.isreal,slh.isdel ";
                      

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        public int UpdateNew1(string houseid)
        {
            //S_LongHouseBase
            string sql = " update S_LongHouseBase set isreal=0 where  id=" + houseid;

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        #endregion
        #endregion

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
        ///  返回城市数据 
        /// </summary>
        /// <returns></returns>
        public DataSet GetCity()
        {
            string sql = "select * from area where pid>1 order by sort";
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 返回广告配置数据
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public DataSet GetAdvertConfigure(string areaid,string areatype)
        {
            string sql = " select * from AdvertConfigure where  AreaId=" + areaid + " and AreaType="+areatype;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);

        }

        public DataSet GetAdvertConfigure()
        {
            string sql = " select * from AdvertConfigure ";

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
