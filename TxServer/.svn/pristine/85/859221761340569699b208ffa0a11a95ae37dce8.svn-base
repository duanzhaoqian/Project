using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MessageUpdate.Dal
{
    public class SearchServiceDal
    {
        string ConnectionString = "";
        public SearchServiceDal(string Connstring)
        {
            ConnectionString = Connstring;
        }
        

        #region   定时服务需求

        #region 自动退还保证金
        /// <summary>
        /// 自动退还保证金
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public DataSet GetHouseIsBack(string cityid)
        {
            string sql = " select a.id,a.AllPrice,a.DayNum,s.uid as UserId,a.houseid from  ARent a join s_LongHouse s on a.houseid=s.id where  a.ETime<getdate() and a.IsPay=1 and AucState=2 and  a.IsBack=0 and a.IsDel=0 ";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
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

        #region 打开房租支付开口
        /// <summary>
        /// 打开房租支付开口
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public DataSet GetHouseIsOpenHouse(string cityid)
        {
            string sql = "  select ac.houseid from ARentContract ac join ARentPayment ap on ac.Id=ap.ARCId  " +
                         "  where ac.state=4 and ac.IsOpen=0 and ap.IsOpen=0 and ap.AucState=0 and ap.State=0 and ap.ZTime<=dateadd(day,15,getdate())  " +
                         "  group by ac.houseid ";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 打开房租支付开口
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public DataSet GetHouseIsOpen(string houseid)
        {
            string sql = " select ac.id as cid,ap.id as pid,ap.ZTime as time from ARentContract ac join ARentPayment ap on ac.Id=ap.ARCId  " +
                         "  where ac.state=4 and ac.IsOpen=0 and ap.IsOpen=0 and ap.AucState=0 and ap.State=0 and ap.ZTime<=dateadd(day,15,getdate()) and ac.houseid= " + houseid;

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 打开房租支付开口
        /// </summary>
        /// <param name="houseid"></param>
        public void UpdateIsOpenC(string id, string time)
        {
            string sql = " update ARentContract set  IsOpen=1, ZTime='" + time + "' where  id=" + id;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }

        /// <summary>
        /// 打开房租支付开口
        /// </summary>
        /// <param name="houseid"></param>
        public void UpdateIsOpenP(string id)
        {
            string sql = " update ARentPayment set  IsOpen=1 where Type=0 and AucState=0 and id=" + id;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        #endregion

        #region 到期返回保证金
        /// <summary>
        /// 到期返回保证金
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public DataSet GetHouseIsLock(string cityid)
        {
            string sql = " select ar.id from ARentContract ac join ARent ar on  ac.HouseId=ar.HouseId " +
                        "  where ac.state=4 and ar.IsLock=1 and ar.Createtime> dateadd(day,180,getdate()) ";

            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 到期返回保证金
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public void UpdateIsLock(string id)
        {
            string sql = " update ARent set IsLock=0 where id=" + id;

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        #endregion

        #region 游戏状态修改
        /// <summary>
        /// 修改游戏出价开始
        /// </summary>
        public int UpdateGameStateBegin()
        {
            string sql = " update game set state=0 where BeginTime<=getdate() and EndTime>getdate() and state!=0";

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        /// <summary>
        /// 修改游戏出价结束
        /// </summary>
        public int UpdateGameStateEnd()
        {
            string sql = " update game set state=2 where   EndTime<=getdate() and state!=2";

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql);
        }
        #endregion

        #endregion
    }
}
