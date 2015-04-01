using System.Web.Mvc;
using KYJ.ZS.Models.Common;
using KYJ.ZS.Models.View;
using KYJ.ZS.Models.Adverts;
using KYJ.ZS.BLL.Adverts;

namespace KYJ.ZS.Manager.Controllers
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

        public ActionResult UploadAdvertisement(string guid)
        {
            var info = new UpLoadGoodsImageViewModel()
            {
                _Guid = guid,
                FullPath = true
            };
            return PartialView("Upload/_UploadAdvertisement", info);
        }
        public ActionResult AdvertSize(int? locationId)
        {
            AdvertSize advertSize = new AdvertManageBll().GetAdvertSize(locationId);
            return Json(new { width=advertSize.AdvertWidth,height=advertSize.AdvertHeight});
        }
    }
}
