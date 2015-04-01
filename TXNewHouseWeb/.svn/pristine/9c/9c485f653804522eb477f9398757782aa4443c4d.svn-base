using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TXBll.HouseData;
using TXBll.WebSite;

namespace TXManagerWeb.Common
{
    public class Auxiliary
    {
        private static readonly Auxiliary Inst = new Auxiliary(); // instance

        public static Auxiliary Instance
        {
            get { return Inst; }
        }

        private Auxiliary()
        {
        }

        #region CSS/Script （样式地址或者脚本地址）

        public string ManagerUrl
        {
            get { return ConfigurationManager.AppSettings["baseUrl"]; }
        }

        public string NhManagerUrl
        {
            get { return ConfigurationManager.AppSettings["baseUrl.nh"]; }
        }

        /// <summary>
        /// 引用公用的js、css 、images 的url
        /// </summary>
        public string NhBasePublicUrl(string vpath)
        {
            return TXCommons.GetConfig.GetFileUrl(vpath, "static.baseUrl.nh");
        }

        /// <summary>
        /// 引用非公用的js、css 、images 的url  例如：http://static.cjkjb.com/static_fangchan/manage4.0/manage/
        /// </summary>
        public string NhWebThemeUrl(string vpath)
        {
            return TXCommons.GetConfig.GetFileUrl(vpath, "static.baseUrl.nh");
        }

        /// <summary>
        /// 开发商后台
        /// </summary>
        public string NhDevelopersUrl
        {
            get { return ConfigurationManager.AppSettings["static.baseUrl.developers"]; }
        }

        #endregion

        #region 权限计算

        public bool ValidNavigation(string num, string strOrg)
        {
            if (string.IsNullOrWhiteSpace(strOrg))
                return false;
            var str = strOrg.Split(',');
            var valid = false;
            foreach (var item in str)
            {
                if (num.Equals(item))
                    valid = true;
            }
            return valid;
        }

        public bool ValidNavigation(int num, string strOrg)
        {
            if (string.IsNullOrWhiteSpace(strOrg))
                return false;
            var str = strOrg.Split(',');
            var valid = false;
            foreach (var item in str)
            {
                if (num == Convert.ToInt32(item))
                {
                    valid = true;
                }
            }
            return valid;
        }

        #endregion

        #region 获取版本号

        /// <summary>
        /// Author:wangzhikun
        /// 获取版本号
        /// </summary>
        public string GetVersion
        {
            get { return ConfigurationManager.AppSettings["Version"]; }
        }

        #endregion

        #region 客服热线电话

        /// <summary>
        /// 客服热线电话1
        /// </summary>
        public string NhServiceHotLine1
        {
            get { return ConfigurationManager.AppSettings["service_hotline_1"]; }
        }

        #endregion

        #region 获取选项卡信息

        /// <summary>
        /// 获取指定id的模块信息
        /// </summary>
        /// <param name="carId">选项卡ID</param>
        /// <param name="modelId">模块ID</param>
        /// <returns>AdminPageInfo</returns>
        public AdminPageInfoModel Model_GetModelsInfo(int carId, int modelId)
        {
            TXModel.Models.Model_AdminPageInfo model = new ModelBll().Model_GetModelsInfo(modelId);
            var result = new AdminPageInfoModel
            {
                ItemId = model.ItemId,
                ItemName = model.ItemName,
                FatherItemId = model.FatherItemId,
                FatherItemName = model.FatherItemName,
                CardId = carId,
                CardName = Instance.GetCardInfoById(carId)
            };
            result.PageTitle = string.Format("{0}-{1}-{2}", result.CardName, result.FatherItemName, result.ItemName);
            return result;
        }

        #endregion

        #region 站点模块信息

        /// <summary>
        /// 根据选项卡编号获取名称
        /// </summary>
        /// <param name="id">选项卡编号</param>
        /// <returns></returns>
        public string GetCardInfoById(int id)
        {
            switch (id)
            {
                case 1:
                    return "公共管理";
                case 2:
                    return "房产客服管理";
                case 3:
                    return "财务管理";
                case 4:
                    return "经纪人管理";
                case 5:
                    return "经纪公司管理";
                case 6:
                    return "新房管理";
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region 联动下拉框

        /// <summary>
        /// 获取省列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetProvinces()
        {
            var list = new GeographyBll().Z_GetProvinces().ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据省编号获取城市列表
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public List<SelectListItem> GetCitiesByProvinceId(int provinceId)
        {
            var list = new GeographyBll().Z_GetCities(provinceId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据城市编号获取区域列表
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetDistrictsByCityId(int cityId)
        {
            var list = new GeographyBll().Z_GetDistricts(cityId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据区域编号获取商圈列表
        /// </summary>
        /// <param name="districtId">区域编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetBusinessesByDistrictId(int districtId)
        {
            var list = new GeographyBll().Z_GetBussineses(districtId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据城市编号获取地铁线路列表
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetSubWayLinesByCityId(int cityId)
        {
            var list = new GeographyBll().GetSubWayLinesByCityID(cityId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 获取楼盘列表
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPremises()
        {
            var list = new PremisesBll().GetPremisesForSelectItems().ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据省、市、开发商编号获取楼盘列表
        /// </summary>
        /// <param name="provinceId">省编号</param>
        /// <param name="cityId">市编号</param>
        /// <param name="developerId">开发商编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetPremisesByProvinceOrCityOrDeveloperId(int provinceId = 0, int cityId = 0, int developerId = 0)
        {
            var list = new PremisesBll().GetPremisesByProvinceOrCityOrDeveloperIdForSelectItems(provinceId, cityId, developerId).ConvertAll(it => new SelectListItem
            {
                Text = it.GeographyName,
                Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
            });
            return list;
        }

        /// <summary>
        /// 根据楼盘编号获取预售许可证列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetPermitPresaleByPremisesId(int premisesId)
        {
            var list = new PermitPresaleBll().GetPermitPresaleByPremisesId(premisesId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据楼盘编号获取楼栋平面图列表
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetBuildingPlansByPremisesId(int premisesId)
        {
            var list = new PermitPresaleBll().GetPermitPresaleByPremisesId(premisesId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据楼盘编号获取楼栋沙盘编号列表
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetPNumsByPremisesId(int premisesId)
        {
            var list = new PermitPresaleBll().GetPermitPresaleByPremisesId(premisesId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.GeographyName,
                            Value = it.GeographyCode.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据楼盘编号获取楼栋列表
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetBuildingsByPremisesId(int premisesId = 0)
        {
            var list = new BuildingBll().GetBuildingListByPremisesId(premisesId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.Name,
                            Value = it.Id.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据楼栋编号获取单元列表
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetUnitsByBuildingId(int buildingId)
        {
            var list = new PremisesBll().GetUnitListbyBuildingId(buildingId).ToList()
                .ConvertAll(
                    it =>
                        new SelectListItem
                        {
                            Text = it.Name,
                            Value = it.Num.ToString(CultureInfo.InvariantCulture)
                        });
            return list;
        }

        /// <summary>
        /// 根据楼栋编号获取楼层信息
        /// </summary>
        /// <param name="buildingId">楼栋编号</param>
        /// <returns></returns>
        public List<SelectListItem> GetFloorsByBuildingId(int buildingId)
        {
            var building = new BuildingBll().GetEntity_ById(buildingId);
            if (null == building)
            {
                return null;
            }

            var list = new List<SelectListItem>();
            for (int i = building.TheFloor; 0 < i; i--)
            {
                list.Add(new SelectListItem {Text = string.Format("{0}F", i), Value = Convert.ToString(i)});
            }

            for (int i = 1; i <= building.Underloor; i++)
            {
                list.Add(new SelectListItem {Text = string.Format("B{0}", i), Value = Convert.ToString((0 - i))});
            }

            return list;
        }

        /// <summary>
        /// 获取项目特色列表
        /// </summary>
        /// <returns></returns>
        public List<object> GetPremisesFeatures()
        {
            var list = new PremisesFeatureBll().GetList();

            var newList = new List<object>();
            if (null != list && 0 < list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    newList.Add(list[i]);
                }
            }
            return newList;
        }

        #endregion

        #region 通用方法（活动）

        /// <summary>
        /// 判断活动是否在今天开始
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public bool IsActivityStartInToday(DateTime start)
        {
            if (start.Year == DateTime.Now.Year
                && start.Month == DateTime.Now.Month
                && start.Day == DateTime.Now.Day)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断活动是否在今天的指定这个小时开始
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public bool IsActivityStartInHour(DateTime start)
        {
            if (start.Year == DateTime.Now.Year
                && start.Month == DateTime.Now.Month
                && start.Day == DateTime.Now.Day
                && start.Hour <= DateTime.Now.Hour)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}