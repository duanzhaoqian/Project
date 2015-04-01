using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace TXModel.AdminPVM
{
    public class PVM_NH_Bargain
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }           
        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string HouseNum { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 楼号
        /// </summary>
        public string FloorNum { get; set; }
        /// <summary>
        /// 楼栋类型
        /// </summary>
        public string NameType { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 所属开发商
        /// </summary>
        public string DeveloperName { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 保底价
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// 一口价
        /// </summary>
        public decimal APrice { get; set; }

        /// <summary>
        /// 加价幅度
        /// </summary>
        public decimal AddPrice { get; set; }
        /// <summary>
        /// 最大幅度
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        public string BeginTimeString { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        public string EndTimeString { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public string SurplusTimeString { get; set; }

        /// <summary>
        /// 活动类型(1摇号 2限时折扣 3排号购房 4阶梯团购 5竞价 6砍价 7秒杀 8一口价)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivitieState { get; set; }
        public string ActivitieStateString { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 楼盘Id 
        /// </summary>
        public int PremisesId { get; set; }

        /// <summary>
        /// 房源Id
        /// </summary>
        public int HouseId { get; set; }
    }

    public class PVM_NH_BargainActive
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivitiesId { get; set; }
        /// <summary>
        /// 活动房源关系id
        /// </summary>
        public int ActivitiesHouseId { get; set; }
        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 房源id
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string HouseNum { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 楼号
        /// </summary>
        public string FloorNum { get; set; }
        /// <summary>
        /// 楼盘
        /// </summary>
        public string PremisesName { get; set; }
        /// <summary>
        /// 楼栋
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 楼栋类型
        /// </summary>
        public string NameType { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 所属开发商
        /// </summary>
        public string DeveloperName { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 保底价
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// 一口价
        /// </summary>
        public decimal APrice { get; set; }
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
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        public string BeginTimeString { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        public string EndTimeString { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public string SurplusTimeString { get; set; }

        /// <summary>
        /// 活动类型(1摇号 2限时折扣 3排号购房 4阶梯团购 5竞价 6砍价 7秒杀 8一口价)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivitieState { get; set; }
        public string ActivitieStateString { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #region 级联选择辅助
        /// <summary>
        /// 楼盘id
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// 楼盘绑定数据
        /// </summary>
        public List<SelectListItem> Premises { get; set; }
        /// <summary>
        /// 楼栋id
        /// </summary>
        public int BuildingId { get; set; }
        /// <summary>
        /// 楼栋绑定数据
        /// </summary>
        public List<SelectListItem> Buildings { get; set; }
        /// <summary>
        /// 单元
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 单元绑定数据
        /// </summary>
        public List<SelectListItem> Units { get; set; }
        /// <summary>
        /// 楼层绑定数据
        /// </summary>
        public List<SelectListItem> Floors { get; set; }
        /// <summary>
        /// 销售状态绑定数据
        /// </summary>
        public List<SelectListItem> SalesStatuss { get; set; }

        #endregion
    }
}
