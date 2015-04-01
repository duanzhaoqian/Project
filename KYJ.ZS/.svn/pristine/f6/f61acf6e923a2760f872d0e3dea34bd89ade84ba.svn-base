using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.DB;
using KYJ.ZS.User.Controllers.ActionFilters;
using KYJ.ZS.Models.Common;
using KYJ.ZS.Commons;

namespace KYJ.ZS.User.Controllers
{
    public class DeliveryAddressController : BaseController
    {

        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:根据User_Id查找收货地址
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns>收货地址集合</returns>
        public ActionResult List()
        {
            int UserId = _ServiceContext.CurrentUser.UserID;
            var bll = new KYJ.ZS.BLL.DeliveryAddresses.DeliveryAddressBll();
            List<DeliveryAddress> list = new List<DeliveryAddress>();
            list = bll.DeliveryAddresses(UserId);
            return View(list);
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-24
        /// Desc:用户后台删除收货地址
        /// </summary>
        /// <param name="deliveryAddressId">收货地址ID</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public ActionResult DelAddress(int deliveryAddressId)
        {
            int UserId = _ServiceContext.CurrentUser.UserID;
            var bll = new KYJ.ZS.BLL.DeliveryAddresses.DeliveryAddressBll();
            int result = bll.DelDeliveryAddress(deliveryAddressId, UserId);
            return Json(result);
        }
        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Insert()
        {
            var bll = new KYJ.ZS.BLL.Geographies.GeographyBll();
            ViewData["Province"] = bll.GetProvinces();
            return View();
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-24
        /// Desc:用户后台添加收货地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddDeliveryAddress()
        {
            DeliveryAddress address = new DeliveryAddress();
            InitializeDeliveryAddressesEntity(address);
            address.CreateTime = System.DateTime.Now;
            if (TryValidateModel(address))
            {
                var bll = new KYJ.ZS.BLL.DeliveryAddresses.DeliveryAddressBll();
                int result = bll.AddDeliveryAddress(address);
                if (result > 0)
                    return Json(new { result = true, message = "添加成功!" });
                else
                    return Json(new { result = false, message = "添加失败,请重试" });
            }
            else
            {
                return Json(new { result = false, message = "添加失败,请重试" });
            }
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:用户后台修改收货地址
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateDeliveryAddress()
        {
            DeliveryAddress address = new DeliveryAddress();
            address.Id = string.IsNullOrEmpty(Request["hdn_AddressId"]) ? 0 : Auxiliary.ToInt32(Request["hdn_AddressId"]); ;
            InitializeDeliveryAddressesEntity(address);
            if (TryValidateModel(address))
            {

                var bll = new KYJ.ZS.BLL.DeliveryAddresses.DeliveryAddressBll();
                int result = bll.UpdateDeliveryAddress(address);
                if (result > 0)
                    return Json(new { result = true, message = "修改成功!" });
                else
                    return Json(new { result = false, message = "修改失败,请重试" });
            }
            else
            {
                return Json(new { result = false, message = "修改失败,请重试" });
            }
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-12
        /// 初始化收货地址数据
        /// </summary>
        /// <param name="entity"></param>
        public void InitializeDeliveryAddressesEntity(DeliveryAddress entity)
        {
            entity.UserId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            entity.ProvinceId = string.IsNullOrEmpty(Request["LiveProvinceId"]) ? 0 : Auxiliary.ToInt32(Request["LiveProvinceId"]);
            entity.ProvinceName = string.IsNullOrEmpty(Request["LiveProvinceName"]) ? string.Empty : Request["LiveProvinceName"].ToString();
            entity.CityId = string.IsNullOrEmpty(Request["LiveCityId"]) ? 0 : Auxiliary.ToInt32(Request["LiveCityId"]);
            entity.CityName = string.IsNullOrEmpty(Request["LiveCityName"]) ? string.Empty : Request["LiveCityName"].ToString();
            entity.DistrictId = string.IsNullOrEmpty(Request["LiveDistrictId"]) ? 0 : Auxiliary.ToInt32(Request["LiveDistrictId"]);
            entity.DistrictName = string.IsNullOrEmpty(Request["LiveDistrictName"]) ? string.Empty : Request["LiveDistrictName"].ToString();
            entity.Address = string.IsNullOrEmpty(Request["txt_Address"]) ? string.Empty : Request["txt_Address"].ToString();
            entity.Code = string.IsNullOrEmpty(Request["txt_Code"]) ? string.Empty : Request["txt_Code"].ToString();
            entity.RealName = string.IsNullOrEmpty(Request["txt_RealName"]) ? string.Empty : Request["txt_RealName"].ToString();
            entity.Tel = string.IsNullOrEmpty(Request["txt_Tel"]) ? string.Empty : Request["txt_Tel"].ToString();
            entity.ResTel = string.IsNullOrEmpty(Request["txt_ResTel"]) ? string.Empty : Request["txt_ResTel"].ToString();
            entity.IsDefault = string.IsNullOrEmpty(Request["checkbox"]) ? false : true;
            entity.UpdateTime = System.DateTime.Now;

        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:根据收货地址ID查找收货地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            int UserId = _ServiceContext.CurrentUser.UserID;//当前登录用户ID
            var bll = new KYJ.ZS.BLL.DeliveryAddresses.DeliveryAddressBll();
            DeliveryAddress address = bll.FindIdDeliveryAddresses(id, UserId);
            if (address != null) {
                return View(address);
            }
            return View("Error");
        }
    }
}
