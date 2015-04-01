using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.User.Controllers
{
    using System.Web.Mvc;

    using KYJ.ZS.Models.Common;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/5/13 10:52:29
    /// 描述：
    /// </summary>
    public class UploadController : BaseController
    {
        [HttpGet]
        public ActionResult Upload(string guid, string picturetype, int maxnum, string pictureNo)
        {
            var info = new UpLoadGoodsImageViewModel()
            {
                _Guid = guid,
                FullPath = true,
                PictureType = picturetype,
                Maxnum = maxnum,
                PictureNo = pictureNo
            };
            return PartialView("Upload/_Upload", info);
        }
    }
}
