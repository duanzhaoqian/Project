using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.DB;

namespace KYJ.ZS.DAL.Messages
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-28
    /// Desc:站内通知
    /// </summary>   
    public class MessagesDal
    {
        #region 查询站内通知 +List<Message> GetMessageList(int id,int pageIndex, int pageSize, out int totalRecord, out int TotalPage)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:获取所有的站内通知
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns></returns>
        public List<Message> GetMessageList(int id,int pageIndex, int pageSize, out int totalRecord, out int TotalPage)
        {
            try
            {
                string tableList = "M_Messages";
                string colList = "message_id,  message_title, message_content,message_isread, message_createtime, message_updatetime";//显示列
                string where_ = "message_isdel='false' and message_receiveid=" + id;

                DataTable dt = KYJ_ZushouRDB.GetPages(pageIndex, where_, "  message_isread asc, message_createtime DESC ", colList, tableList, pageSize, true, out totalRecord, out TotalPage);
                if (Auxiliary.CheckDt(dt))
                {
                    List<Message> list = new List<Message>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Message mdlMessage = new Message();
                        mdlMessage.Id = Auxiliary.ToInt32(dr["message_id"]);
                        mdlMessage.Title = dr["message_title"].ToString();
                        mdlMessage.Content = dr["message_content"].ToString();
                        mdlMessage.IsRead = (bool)dr["message_isread"];
                        mdlMessage.CreateTime = Convert.ToDateTime(dr["message_createtime"]);
                        mdlMessage.UpdateTime = Convert.ToDateTime(dr["message_updatetime"]);
                        list.Add(mdlMessage);
                    }
                    return list;
                }
                return null;
            }
            catch (Exception e) {
                string param = string.Empty;
                param = string.Format("{0},{1},{2}",id,pageIndex,pageSize);
                Log4net.RecordLog.RecordException("cheny", param, e);
                totalRecord = TotalPage = 0;
                return null;
            }
        }
        #endregion

        #region 将站内信设为已读 +int IsRead(int id)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:将站内信设为已读
        /// </summary>
        /// <param name="id">站内信id</param>
        /// <returns></returns>
        public int IsRead(int id)
        {
            try
            {
                #region SQL
                var sql = @"UPDATE  M_Messages
                        SET     message_isread ='true'
                        WHERE   message_id = @message_id";
                #endregion
                #region 参数
                SqlParam param = new SqlParam();
                param.AddParam("@message_id", id);

                #endregion
                #region 执行

                return KYJ_ZushouWDB.SqlExecute(sql, param);

                #endregion
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", id.ToString(), e);
                return 0;
            }
        } 
        #endregion

        #region 获取未读的站内信 +int NotRead()
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:获取未读的站内信
        /// </summary>      
        /// <returns></returns>
        public int NotRead()
        {
            try
            {
                #region SQL
                var sql = @"SELECT COUNT(message_id) FROM  M_Messages
                        WHERE    message_isread = 'false' and message_isdel='false'";
                #endregion
                #region 执行

                return Convert.ToInt32(KYJ_ZushouWDB.GetFirst(sql));

                #endregion
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", null, e);
                return 0;
            }
        } 
        #endregion

        #region 获取商户未读站内信 +int GetShortMessage(int merId)
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月7日14:17:22
        /// 描述：获取商户未读站内信
        /// </summary>
        /// <param name="merId"></param>
        /// <returns></returns>
        public int GetShortMessage(int merId)
        {
            try
            {
                string strSql = "SELECT COUNT(1) FROM dbo.M_Messages WHERE message_isread=0 and message_receiveid=@id ";
                SqlParam sqlParam = new SqlParam();
                sqlParam.AddParam("@id", merId);
                string result = KYJ_ZushouRDB.GetFirst(strSql, sqlParam);
                if (!string.IsNullOrEmpty(result))
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Log4net.RecordLog.RecordException("cheny", merId.ToString(), e);
                return 0;
            }
        } 
        #endregion
    }
}
