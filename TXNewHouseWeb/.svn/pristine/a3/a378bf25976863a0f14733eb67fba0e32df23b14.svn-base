using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVE_NH_Premises2
    {
        // 楼盘编号
        public int Id { set; get; }

        // 开发商编号
        public int DeveloperId { set; get; }

        // 楼盘名称
        public string Name { set; get; }

        // 参考均价
        public decimal ReferencePrice { set; get; }

        // 咨询电话
        public string TelePhone { set; get; }

        // 楼盘状态
        public int SalesStatus { set; get; }
        
        // 楼盘状态(下拉列表)
        private List<SelectListItem> _salesStatusList = new List<SelectListItem> { new SelectListItem { Text = "请选择", Value = "0" } };
        public List<SelectListItem> SalesStatusList
        {
            get { return _salesStatusList; }
            set { _salesStatusList = value; }
        }


        #region 省、市、区、商圈

        // 省Id
        public int ProvinceId { set; get; }

        // 省名称
        public string ProvinceName { set; get; }

        // 省（下拉列表）
        private List<SelectListItem> _provinceList = new List<SelectListItem> { new SelectListItem { Text = "请选择省", Value = "0" } };
        public List<SelectListItem> ProvinceList
        {
            get { return _provinceList; }
            set { _provinceList = value; }
        }

        // 市Id
        public int CityId { set; get; }

        // 市名称
        public string CityName { set; get; }

        // 市（下拉列表）
        private List<SelectListItem> _cities = new List<SelectListItem> { new SelectListItem { Text = "请选择城市", Value = "0" } };
        public List<SelectListItem> Cities
        {
            get { return _cities; }
            set { _cities = value; }
        }

        // 区域Id
        public int DId { set; get; }

        // 区域名称
        public string DName { set; get; }

        // 区域（下拉列表）
        private List<SelectListItem> _districts = new List<SelectListItem> { new SelectListItem { Text = "请选择区域", Value = "0" } };
        public List<SelectListItem> Districts
        {
            get { return _districts; }
            set { _districts = value; }
        }

        // 商圈Id
        public int BId { set; get; }

        // 商圈名称
        public string BName { set; get; }

        // 商圈（下拉列表）
        private List<SelectListItem> _businesses = new List<SelectListItem> { new SelectListItem { Text = "请选择", Value = "0" } };
        public List<SelectListItem> Businesses
        {
            get { return _businesses; }
            set { _businesses = value; }
        }

        #endregion

        // 环线位置
        public int Ring { set; get; }

        // 环线位置(下拉列表)
        private List<SelectListItem> _ringList = new List<SelectListItem> { new SelectListItem { Text = "请选择", Value = "0" } };
        public List<SelectListItem> RingList
        {
            get { return _ringList; }
            set { _ringList = value; }
        }

        // 项目地址
        public string PremisesAddress { set; get; }

        // 售楼地址
        public string SalesAddress { set; get; }

        // 开发商
        public string Developer { set; get; }

        // 代理商
        public string Agent { set; get; }

        // 预售许可证（列表）
        public List<PVEL_NH_PermitPresale> PermitPresales { set; get; }

        // 预售许可证（列表JSON）
        public string PermitPresales_Json { set; get; }



        // 产权
        public string PropertyRight { set; get; }

        // 建筑面积
        public double BuildingArea { set; get; }

        // 占地面积
        public double Area { set; get; }

        // 总户数
        public int UserCount { set; get; }

        // 项目特色
        public string Characteristic { set; get; }

        // 项目特色(选项表)
        public List<object> PremisesFeatureList { set; get; }

        // 项目特色(JSON)
        public string Characteristic_Json { set; get; }

        // 建筑类别
        public int BuildingType { set; get; }

        // 建筑类别(下拉列表)
        private List<SelectListItem> _buildingTypeList = new List<SelectListItem> { new SelectListItem { Text = "请选择", Value = "0" } };
        public List<SelectListItem> BuildingTypeList
        {
            get { return _buildingTypeList; }
            set { _buildingTypeList = value; }
        }



        // 物业类型
        public string PropertyType { set; get; }

        // 容积率
        public double AreaRatio { set; get; }

        // 得房率
        public double RoomRate { set; get; }

        // 物业费
        public double PropertyPrice { set; get; }

        // 车位信息
        public string ParkingLot { set; get; }

        // 物业公司
        public string PropertyCompany { set; get; }

        // 绿化率
        public double GreeningRate { set; get; }



        // 经度
        public string Lng { set; get; }

        // 纬度
        public string Lat { set; get; }

        // 地铁线编号（逗号分隔）
        public string TId { set; get; }

        // 交通介绍
        public string TrafficIntroduction { set; get; }

        // 配套介绍
        public string SupportingIntroduction { set; get; }



        // 楼盘介绍
        public string PremisesIntroduction { set; get; }



        // 图片
        public string InnerCode { set; get; }

        // 是否修改沙盘图
        public int IsUpdateSandbox { set; get; }

        // 是否接入400
        public int IsShow400 { set; get; }

        // 是否在前台显示
        //public int IsShow { set; get; }
    }

    /// <summary>
    /// 预售许可证(列表实体)
    /// </summary>
    public class PVEL_NH_PermitPresale
    {
        // 预售许可证编号
        public int Id { set; get; }

        // 预售许可证名称
        public string Name { set; get; }

        // 是否删除(0:可用 1:删除)
        public int IsDel { set; get; }
    }
}