using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.BLL.Messages;
using System.Web.Mvc;
using KYJ.ZS.Models.DB;
using Webdiyer.WebControls.Mvc;

namespace KYJ.ZS.Merchant.Controllers
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-28
    /// Desc:站内通知
    /// </summary>  
    public class MessageController : BaseController
    {
        MessageBll bll = new MessageBll();

        /// <summary>
        /// 分页尺寸
        /// </summary>
        const int PAGE_SIZE = 10;   //add by cy 20140526

        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:获取所有的站内通知
        /// </summary>
        /// <returns></returns>
        public ActionResult SiteNotice(int? pageIndex)
        {
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            List<Message> MessageList = new List<Message>();
            int index = pageIndex ?? 1;
            int totalCount = 0;
            int totalPage = 0;
            MessageList = bll.GetMessageList(id,index, PAGE_SIZE, out  totalCount, out totalPage);
            PagedList<Message> mPage = null;
            if (MessageList != null)
            {
                mPage = new PagedList<Message>(MessageList, index, PAGE_SIZE, totalCount);
            }
            return View(mPage);
        }

        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:将站内信设为已读
        /// </summary>
        /// <param name="id">站内信id</param>
        /// <returns></returns>
        public ActionResult IsRead(int id)
        {
            bool IsRead = bll.IsRead(id);
            return Json(IsRead);
        }
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-28
        /// Desc:获取未读的站内信
        /// </summary>
        /// <param name="id">站内信id</param>
        /// <returns></returns>
        public ActionResult NotRead()
        {
            int NotReadCount = bll.NotRead();
            return Json(NotReadCount);
        }
    }
}
