using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TXBll.FinancialData;
using TXManagerWeb.Common;
using TXModel.AdminPVM;

namespace TXManagerWeb.Controllers
{
    [LoginChecked]
    public class NhGuaranteeController : BaseController
    {
        /// <summary>
        /// 新房保证金
        /// </summary>
        /// <returns></returns>
        [ActionFilters.AdminPageInfo(3, (int)AdminNavi.Financial.Deposit.List)]
        public ActionResult Index(PVS_NH_Bond search = null)
        {
            if (null == search)
            {
                search = new PVS_NH_Bond();
                search.Provinces = new List<SelectListItem>();
                search.Cities = new List<SelectListItem>();
            }

            search.UserTypes = GetUserTypes();
            search.UserType = (search.UserType == 0 ? 3 : search.UserType);

            if (3 != search.UserType)
            {
                return Redirect(string.Format("{0}guarantee/search.html?ram={1}&provinceid={2}&cityid={3}&begintime={4}&endtime={5}&usertype={6}&keyword={7}",
                    Auxiliary.Instance.ManagerUrl,
                    new Random().Next(1, 999999999),
                    search.ProvinceId,
                    search.CityId,
                    search.BeginTime,
                    search.EndTime,
                    search.UserType,
                    search.KeyWord));
            }

            var list = Auxiliary.Instance.GetProvinces();
            // 加载省份
            if (0 < list.Count)
            {
                search.Provinces = new List<SelectListItem>();
                search.Provinces.Add(new SelectListItem { Text = "选择省份", Value = "0" });
                search.Provinces.AddRange(list);
            }

            // 加载城市
            if (search.ProvinceId > 0)
            {
                search.Cities = new List<SelectListItem>();
                list = Auxiliary.Instance.GetCitiesByProvinceId(search.ProvinceId);
                search.Cities.AddRange(list);
            }
            else
            {
                search.Cities = new List<SelectListItem>();
                search.Cities.Add(new SelectListItem { Text = "选择城市", Value = "0" });
            }

            return View(search);
        }

        /// <summary>
        /// 根据查询条件查询提现列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ActionResult SearchResult(PVS_NH_Bond search, int pageindex, int pagesize)
        {
            pageindex = pageindex + 1;
            List<PVM_NH_Bond> list = null;
            switch (search.UserType)
            {
                case 3:
                    list = new BondBll().GetPageList_BySearch_Bond(search, pageindex, pagesize);
                    break;
            }
            return PartialView("PageTables/Financial/_Bond", list);
        }

        /// <summary>
        /// 根据查询条件查询提现总记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public JsonResult SearchCount(PVS_NH_Bond search)
        {
            int count = new BondBll().GetTotalCount_BySearch_Bond(search);

            return Json(new {result = count});
        }

        /// <summary>
        /// 获取用户类型
        /// </summary>
        /// <returns></returns>
        private List<SelectListItem> GetUserTypes()
        {
            var userTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "个人", Value = "1"},
                new SelectListItem {Text = "经纪人", Value = "2"},
                new SelectListItem {Text = "开发商", Value = "3"}
            };

            return userTypes;
        }

    }
}