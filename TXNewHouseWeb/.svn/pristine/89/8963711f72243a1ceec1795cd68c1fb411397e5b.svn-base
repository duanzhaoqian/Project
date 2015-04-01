using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TXModel.Commons;
using TXOrm;

namespace TXDal.Dev
{
    public class DeveloperMessageDal : BaseDal_Admin
    {

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
            List<DeveloperMessage> result = null;
            int resultCount = 0;
            try
            {
                #region 拼接分页SQL
                //分页SQL
                string sql = String.Empty;
                sql = @"SELECT * FROM (
                    SELECT ROW_NUMBER() OVER(ORDER BY CreateTime DESC) AS Row, * FROM DeveloperMessage WITH(NOLOCK) 
                    WHERE IsDel = 0 AND ReceiveUserId = @W_ReceiveUserId AND [Content] LIKE '%'+@W_Keyword+'%'
                    ) AS tempTable
                    WHERE Row BETWEEN @W_PageBegin AND @W_PageEnd";
                #endregion
                #region 拼接总记录SQL
                //总记录SQL
                string sqlCount = String.Empty;
                sqlCount = @"SELECT COUNT(1) FROM DeveloperMessage WITH(NOLOCK) WHERE IsDel = 0 AND ReceiveUserId = @W_ReceiveUserId AND [Content] LIKE '%'+@W_Keyword+'%'";
                #endregion

                #region 设置分页参数
                //分页参数
                SqlParameter[] para = new SqlParameter[] { 
                        new SqlParameter("@W_PageBegin",((paging.PageIndex - 1) * paging.PageSize) + 1),
                        new SqlParameter("@W_PageEnd",paging.PageIndex * paging.PageSize),
                        new SqlParameter("@W_ReceiveUserId",userId),
                        new SqlParameter("@W_Keyword",key)
                    };
                #endregion
                #region 设置总记录参数
                //总记录参数
                SqlParameter[] paraCount = new SqlParameter[] { 
                        new SqlParameter("@W_ReceiveUserId",userId),
                        new SqlParameter("@W_Keyword",key)
                    };
                #endregion

                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    #region 查询分页数据
                    //查询分页
                    var query = houseDB.ExecuteStoreQuery<DeveloperMessage>(sql, para);
                    result = query.ToList();
                    #endregion
                    #region 查询总记录数据
                    //查询总记录
                    var queryCount = houseDB.ExecuteStoreQuery<int>(sqlCount, paraCount);
                    resultCount = queryCount.FirstOrDefault<int>();
                    #endregion
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("userId:{0},key:{1}", userId, key), e);
                //throw;
            }
            paging.ResultData = result;
            paging.TotalCount = resultCount;
            return paging;
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
            int result = 0;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    DeveloperMessage entity = houseDB.DeveloperMessages.FirstOrDefault(e => e.ReceiveUserId == userId && e.Id == messageId);
                    if (null != entity)
                    {
                        if (flag)
                            entity.IsRead = true;
                        else
                            entity.IsDel = true;
                        result = houseDB.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("userId:{0},messageId:{1},flag:{2}", userId, messageId, flag), e);
                //throw;
            }
            return result;
        }


        /// <summary>
        /// 获取未读站内信数量
        /// author: maboxun
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public int GetUnReadMsgCount(int userId)
        {
            int count = 0;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    count = houseDB.DeveloperMessages.Where(m => m.ReceiveUserId == userId && m.IsDel == false && m.IsRead == false).Count();
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("马波讯", userId.ToString(), e);
            }
            return count;
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
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    var sql = @"
INSERT  INTO DeveloperMessage ( SendUserId, ReceiveUserId, Title, Content, IsRead, CreateTime, IsDel )
VALUES  ( @SendUserId, @ReceiveUserId, @Title, @Content, 0, GETDATE(), 0 )
SELECT  @@IDENTITY AS Result";

                    var parms = new object[]
                    {
                        new SqlParameter("SendUserId", message.SendUserId),
                        new SqlParameter("ReceiveUserId", message.ReceiveUserId),
                        new SqlParameter("Title", message.Title),
                        new SqlParameter("Content", message.Content)
                    };

                    var list = db.ExecuteStoreQuery<ESqlResult_ScalarString>(sql, parms.ToArray()).ToList();

                    if (0 < list.Count)
                    {
                        return Convert.ToInt32(list[0].Result);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", message, ex);
                return 0;
            }
        }
    }
}