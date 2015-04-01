using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVS_NH_Biding
    {
        
        /// <summary>
        /// 楼盘id
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// 楼栋id
        /// </summary>
        public int BuildingId { get; set; }
        /// <summary>
        /// 单元id
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivitieState { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 开发商或者代理商名称
        /// </summary>
        public string Name { get; set; }
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
        private List<SelectListItem> _Premises = new List<SelectListItem>() { new SelectListItem() { Text = "选择楼盘", Value = "0" } };
        /// <summary>
        /// 楼盘绑定数据
        /// </summary>
        public List<SelectListItem> Premises
        {
            get { return _Premises; }
            set { _Premises = value; }
        }

        private List<SelectListItem> _Buildings = new List<SelectListItem>() { new SelectListItem() { Text = "选择楼栋", Value = "0" } };
        /// <summary>
        /// 楼栋绑定数据
        /// </summary>
        public List<SelectListItem> Buildings
        {
            get { return _Buildings; }
            set { _Buildings = value; }
        }

        private List<SelectListItem> _Units = new List<SelectListItem>() { new SelectListItem() { Text = "选择单元", Value = "0" } };
        /// <summary>
        /// 单元绑定数据
        /// </summary>
        public List<SelectListItem> Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        private List<SelectListItem> _Floors = new List<SelectListItem>() { new SelectListItem() { Text = "选择楼层", Value = "0" } };
        /// <summary>
        /// 楼层绑定数据
        /// </summary>
        public List<SelectListItem> Floors
        {
            get { return _Floors; }
            set { _Floors = value; }
        }
        private List<SelectListItem> _ActivitieState = new List<SelectListItem>() { new SelectListItem() { Text = "活动状态", Value = "-1" } };
        /// <summary>
        /// 销售状态绑定数据
        /// </summary>
        public List<SelectListItem> ActivitieStates
        {
            get { return _ActivitieState; }
            set { _ActivitieState = value; }
        }
        #endregion
    }
}
