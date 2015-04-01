using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TXNewHouseWeb.Common;

namespace TXNewHouseWeb.Controllers
{
    public class ToolsController : BaseController
    {
        readonly TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

        /// <summary>
        /// 新房前台 -工具- 购房计算器 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            #region SEO处理
            SEO seo = new SEO();
            string typename = "";
            switch(id){
                case "1":
                    typename = "贷款计算器";
                    break;
                case "2":
                    typename = "购房能力评估计算器";
                    break;
                case "3":
                    typename = "公积金贷款计算器";
                    break;
                case "4":
                    typename = "提前还贷计算器";
                    break;
                case "5":
                    typename = "税费计算器";
                    break;
                default:
                    typename = "贷款计算器";
                    break;
            }
            ViewData["Seo"] = seo.SeoLoanComputational(new SEOModel(), ViewData["cityName"] as string, typename);
            List<string> typeList = new List<string> { "1", "2", "3", "4", "5" };
            ViewData["tooltype"] = typeList.Contains(id) ? id : "1";
            #endregion
            return View();
        }

        /// <summary>
        /// 购房手册导航页
        /// </summary>
        /// <returns></returns>
        public ActionResult GouFangShouCe()
        {
            #region SEO处理
            SEO seo = new SEO();
            ViewData["Seo"] = seo.SeoManuals(new SEOModel(), ViewData["cityName"] as string);
            #endregion
            return View();
        }
        /// <summary>
        /// 购房手册详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GouFangShouCeXQ(string id)
        {
            return View();
        }

        /// <summary>
        /// 新房前台工具-购房系统自查系统
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GouFangZiChaXT()
        {
            #region SEO处理
            SEO seo = new SEO();
            ViewData["Seo"] = seo.SeoEntitledToOwn(new SEOModel(), ViewData["cityName"] as string);
            #endregion
            return View();
        }
        /// <summary>
        /// 新房前台工具-购房系统自查系统上海
        /// </summary>
        /// <returns></returns>
        public ActionResult GouFangZiChaSH()
        {
            #region SEO处理
            SEO seo = new SEO();
            ViewData["Seo"] = seo.SeoEntitledToOwn(new SEOModel(), ViewData["cityName"] as string);
            #endregion
            return View();
        }

        #region 楼盘对比
        /// <summary>
        /// Author：崔利国
        /// Time：2013-11-28
        /// 楼盘对比
        /// </summary>
        /// <returns></returns>
        public ActionResult PremisesCompare(string id)
        {
            #region SEO处理
            SEO seo = new SEO();
            ViewData["Seo"] = seo.SeoPremisesPK(new SEOModel(), ViewData["cityName"] as string);
            #endregion
            if (!string.IsNullOrWhiteSpace(id))
            {
                string[] mc = System.Text.RegularExpressions.Regex.Split(id.TrimStart(',').TrimEnd(','), ",");
                List<string> idList = new List<string>();
                foreach (var item in mc)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        int temp;
                        if (int.TryParse(item, out temp))
                        {
                            idList.Add(item);
                        }
                    }
                    if (idList.Count == 5) { break; }
                }
                if (idList != null && idList.Count > 0)
                {
                    var list = _premisesbll.GetPremisesCompareByIdList(idList);
                    foreach (var model in list)
                    {
                        #region 楼盘项目特色
                        string strPremisesCharacteristic = string.Empty;

                        if (null != model && null != model.Characteristic)
                        {
                            if (!string.Empty.Equals(model.Characteristic.Trim()))
                            {
                                List<TXOrm.PremisesFeature> PremisesFeaturels = _premisesbll.GetPremisesFeatureList(model.Characteristic);

                                strPremisesCharacteristic = string.Join(",", PremisesFeaturels.Select(c => c.Name).ToArray());
                            }
                        }
                        model.Characteristic = strPremisesCharacteristic;
                        #endregion
                    }
                    return View(list);
                }
            }
            return View();
        }

        /// <summary>
        /// Author：崔利国
        /// Time：2013-11-28
        /// 根据楼盘名称获取楼盘信息（异步方法）
        /// </summary>
        /// <param name="premisesName"></param>
        /// <returns></returns>
        public ActionResult GetPremisesByName(string premisesName)
        {
            var data = _premisesbll.GetPremisesByName(premisesName);
            if (data == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            data.View_BuildingType = TXCommons.NewHouseWeb.HouseType.GetBuildingType(data.BuildingType);
            data.PropertyType = TXCommons.NewHouseWeb.BuildingType.GetPropertyType(data.PropertyType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 模糊匹配楼盘
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public ActionResult SearchPremisesByName(string q)
        {
            //string resultstring = ""; 
            //var str = TXCommons.GetConfig.SearchUrl + "Company/GetCompanyIndex.ashx?type=array&q=" + q.Trim();
            //WebRequest req = WebRequest.Create(str);
            //WebResponse res = req.GetResponse();
            //Stream ReceiveStream = res.GetResponseStream();
            //StreamReader sr = new StreamReader(ReceiveStream);
            //resultstring = sr.ReadToEnd().Replace("\\n", "\n");
            //return Content(resultstring);

            var list = _premisesbll.SearchPremisesByName(q).Select(p => "{result:'true',VID:'" + p.Id + "',Name:'" + p.Name + "'}").ToArray();
            var result = string.Join("\n", list);

            return Content(result);
        }

        #endregion

    }
}
