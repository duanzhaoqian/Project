using TXDal.Dev;
using TXModel.Commons;
using TXOrm;

namespace TXBll.Dev
{
    /// <summary>
    /// 站内信
    /// </summary>
    public class DeveloperMessageBll
    {
        private readonly DeveloperMessageDal _developerMessageDal = new DeveloperMessageDal();

        #region 站内信相关

        /// <summary>
        /// 得到站内信
        /// Author:台永海
        /// </summary>
        /// <param name="paging">分页类</param>
        /// <param name="userId">用户ID</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public Paging<DeveloperMessage> GetPageMessage(Paging<DeveloperMessage> paging, int userId, string key)
        {
            return _developerMessageDal.GetPageMessage(paging, userId, key);
        }

        /// <summary>
        /// 修改消息已读或删除
        /// Author:台永海
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="messageId">消息Id</param>
        /// <param name="flag">标示：true：改为已读；false：改为删除</param>
        /// <returns></returns>
        public int UpdateMessageReadOrDel(int userId, int messageId, bool flag)
        {
            return _developerMessageDal.UpdateMessageReadOrDel(userId, messageId, flag);
        }

        /// <summary>
        /// 获取未读站内信数量
        /// author: maboxun
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int GetUnReadMsgCount(int userId)
        {
            return _developerMessageDal.GetUnReadMsgCount(userId);
        }

        #endregion

        /// <summary>
        /// 添加
        /// author: liyuzhao
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int Add(DeveloperMessage message)
        {
            return _developerMessageDal.Add(message);
        }
    }
}