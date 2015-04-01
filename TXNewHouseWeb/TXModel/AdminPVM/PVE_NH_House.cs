﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVE_NH_House
    {
        /// <summary>
        /// 开发商id
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// 楼盘id
        /// </summary>
        public int PremisesId { get; set; }
        private List<SelectListItem> _Premises = new List<SelectListItem>() { new SelectListItem() { Text = "选择楼盘", Value = "0" } };
        /// <summary>
        /// 楼盘绑定数据
        /// </summary>
        public List<SelectListItem> Premises
        {
            get { return _Premises; }
            set { _Premises = value; }
        }
        /// <summary>
        /// 楼栋id
        /// </summary>
        public int BuildingId { get; set; }
        private List<SelectListItem> _Buildings = new List<SelectListItem>() { new SelectListItem() { Text = "选择楼栋", Value = "0" } };
        /// <summary>
        /// 楼栋绑定数据
        /// </summary>
        public List<SelectListItem> Buildings
        {
            get { return _Buildings; }
            set { _Buildings = value; }
        }
        /// <summary>
        /// 单元id
        /// </summary>
        public int UnitId { get; set; }
        private List<SelectListItem> _Units = new List<SelectListItem>() { new SelectListItem() { Text = "选择单元", Value = "0" } };
        /// <summary>
        /// 单元绑定数据
        /// </summary>
        public List<SelectListItem> Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        /// <summary>
        /// 所在楼层
        /// </summary>
        public int Floor { get; set; }
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
        /// <summary>
        /// 建筑形式1 开间2 错层3 跃层4 复式5 平层
        /// </summary>
        public List<SelectListItem> BuildingType { get; set; }
        /// <summary>
        /// 采光情况1东、2南、3西、4北、5东南、6东北、7西南、8西北、9南北、10东西
        /// </summary>
        public List<SelectListItem> Orientation { get; set; }
        /// <summary>
        /// 户型id
        /// </summary>
        public int RId { get; set; }
        private List<SelectListItem> _RIds = new List<SelectListItem>() { new SelectListItem() { Text = "选择户型图", Value = "-1" } };
        /// <summary>
        /// 户型绑定数据
        /// </summary>
        public List<SelectListItem> RIds
        {
            get { return _RIds; }
            set { _RIds = value; }
        }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class PVME_NH_House
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
        /// 户型id
        /// </summary>
        public int RId { get; set; }
        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }
        /// <summary>
        /// 建筑类型
        /// </summary>
        public int BuildingType { get; set; }
        /// <summary>
        /// 采光情况
        /// </summary>
        public int Orientation { get; set; }
        public PVE_NH_House PveHouse { get; set; }
        public PVM_NH_House PvmHouse { get; set; }
    }

}