using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 新房房源搜索
    /// </summary>
    public class PVS_NH_House
    {
        /// <summary>
        /// 开发商id
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// 楼盘id
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// 楼盘Name
        /// </summary>
        public string PremisesName { get; set; }
        /// <summary>
        /// 楼栋id
        /// </summary>
        public int BuildingId { get; set; }
        /// <summary>
        /// 楼栋Name
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 单元id
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }
        /// <summary>
        /// 城市id 房源所在城市 即楼盘所在
        /// </summary>
        public int CityId { get; set; }

        #region 级联选择辅助

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
        private List<SelectListItem> _SalesStatus = new List<SelectListItem>() { new SelectListItem() { Text = "销售状态", Value = "-1" } };
        /// <summary>
        /// 销售状态绑定数据
        /// </summary>
        public List<SelectListItem> SalesStatuss
        {
            get { return _SalesStatus; }
            set { _SalesStatus = value; }
        }
        #endregion
    }
}
