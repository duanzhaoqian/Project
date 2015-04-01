using System.Web.Mvc;
using KYJ.ZS.Models.Common;

namespace KYJ.ZS.Merchant.Controllers
{
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
