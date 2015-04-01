using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVS_NH_Premises
    {
        /// <summary>
        /// 楼盘ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public int DistrictID { get; set; }
        /// <summary>
        /// 商圈ID
        /// </summary>
        public int BusinessID { get; set; }
        /// <summary>
        /// 开发商名称
        /// </summary>
        public string DeveloperName { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremisesName { get; set; }
        /// <summary>
        /// 销售状态 (楼盘状态  0待售  1在售  2售罄)
        /// </summary>
        public int SalesStatus { get; set; }
        /// <summary>
        /// 环线位置(1 一环 2二环 3 三环 4四环 5五环 6六环)
        /// </summary>
        public int Ring { get; set; }
        /// <summary>
        /// 建筑类别
        /// </summary>
        public int BuildingType { get; set; }

        #region 级联选择辅助

        private List<SelectListItem> _Provinces = new List<SelectListItem>() { new SelectListItem() { Text = "选择省份", Value = "0" } };
        /// <summary>
        /// 省份绑定数据
        /// </summary>
        public List<SelectListItem> Provinces
        {
            get { return _Provinces; }
            set { _Provinces = value; }
        }

        private List<SelectListItem> _Cities = new List<SelectListItem>() { new SelectListItem() { Text = "选择城市", Value = "0" } };
        /// <summary>
        /// 城市绑定数据
        /// </summary>
        public List<SelectListItem> Cities
        {
            get { return _Cities; }
            set { _Cities = value; }
        }

        private List<SelectListItem> _Districts = new List<SelectListItem>() { new SelectListItem() { Text = "选择区域", Value = "0" } };
        /// <summary>
        /// 区域绑定数据
        /// </summary>
        public List<SelectListItem> Districts
        {
            get { return _Districts; }
            set { _Districts = value; }
        }

        private List<SelectListItem> _Businesses = new List<SelectListItem>() { new SelectListItem() { Text = "选择商圈", Value = "0" } };
        /// <summary>
        /// 商圈绑定数据
        /// </summary>
        public List<SelectListItem> Businesses
        {
            get { return _Businesses; }
            set { _Businesses = value; }
        }


        #endregion

        /// <summary>
        /// 销售状态下拉列表
        /// </summary>
        public List<SelectListItem> SalesState { set; get; }
        /// <summary>
        /// 环线位置下拉列表
        /// </summary>
        public List<SelectListItem> RingList { set; get; }
        /// <summary>
        /// 建筑类别下拉列表
        /// </summary>
        public List<SelectListItem> BuildingTypes { set; get; }
    }
}
