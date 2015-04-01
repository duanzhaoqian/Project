using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 修改 竞价活动
    /// </summary>
    public class PVE_NH_Biding
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivitiesId { get; set; }
        /// <summary>
        /// 房源id
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string HouseName { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 单元名
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 楼栋id
        /// </summary>
        public int BuildingId { get; set; }
        /// <summary>
        /// 楼号
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 楼号类型
        /// </summary>
        public string BuildingType { get; set; }
        /// <summary>
        /// 楼盘id
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremisesName { get; set; }
        /// <summary>
        /// 开发商id
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// 开发商名字
        /// </summary>
        public string DeveloperName { get; set; }


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

        /// <summary>
        /// 起拍价
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// 加价幅度
        /// </summary>
        public decimal AddPrice { get; set; }
        /// <summary>
        /// 最大幅度
        /// </summary>
        public decimal MaxPrice { get; set; }
         /// <summary>
        /// 保证金
        /// </summary>
        public decimal Bond { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }
        /// <summary>
        /// 活动房源关系id
        /// </summary>
        public int ActivitiesHouseId { get; set; }
    }
}
