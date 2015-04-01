using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.Messages;

namespace KYJ.ZS.BLL.Messages
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-28
    /// Desc:站内通知
    /// </summary>  
    public class MessageBll
    {
        MessagesDal dal = new MessagesDal();

        #region 查询站内通知 +List<Message> GetMessageList(int id,int pageIndex, int pageSize, out int totalRecord, out int TotalPage)
        /// <summary>
        /// 获取所有的站内通知
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="totalRecord">总条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns></returns>
        public List<Message> GetMessageList(int id,int pageIndex, int pageSize, out int totalRecord, out int TotalPage)
        {
            return dal.GetMessageList(id,pageIndex, pageSize, out totalRecord, out TotalPage);
        }
        #endregion

        #region 将站内信设为已读 +bool IsRead(int id)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:将站内信设为已读
        /// </summary>
        /// <param name="id">站内信id</param>
        /// <returns></returns>
        public bool IsRead(int id)
        {

            int isRead = dal.IsRead(id);
            if (isRead > 0)
            {
                return true;
            }
            return false;
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
            return dal.NotRead();
        } 
        #endregion

        #region 获取商户未读的短消息 +int GetShortMessage(int merId)
        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月7日14:19:37
        /// 描述：获取商户未读的短消息
        /// </summary>
        /// <param name="merId"></param>
        /// <returns></returns>
        public int GetShortMessage(int merId)
        {
            return dal.GetShortMessage(merId);
        } 
        #endregion
    }
}
