using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.Web
{
    /// <summary>
    /// 楼盘房间信息Model类
    /// </summary>
    public class PremisesHouse
    {
        /// <summary>
        /// 房源Id
        /// </summary>
        public int HouseId { get; set; }

        /// <summary>
        /// 开发商Id
        /// </summary>
        public int DeveloperId { get; set; }

        /// <summary>
        /// 楼盘Id
        /// </summary>
        public int PremisesId { get; set; }

        /// <summary>
        /// 楼栋Id
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 楼栋的下拉列表
        /// </summary>
        public List<SelectListItem> Buildings { get; set; }

        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremisesName { get; set; }

        /// <summary>
        ///楼栋名称 
        /// </summary>
        public string BuildingName { get; set; }


        /// <summary>
        /// 单元ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// 单元名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 单元的下拉列表
        /// </summary>
        public List<SelectListItem> Units { get; set; }

        /// <summary>
        /// 所在楼层
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// 楼层的下拉列表
        /// </summary>
        public List<SelectListItem> Floors { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// 房号的列表
        /// </summary>
        public List<SelectListItem> Houses { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int? ActivityId { get; set; }

        /// <summary>
        /// 活动类型
        /// </summary>
        public int? ActivityType { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int? ActivityState { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime? ActBeginTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? ActEndTime { get; set; }

        /// <summary>
        /// 参考单价
        /// </summary>
        public decimal SinglePrice { get; set; }

        /// <summary>
        /// 参考总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 首期付款
        /// </summary>
        public decimal DownPayment { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }

        /// <summary>
        /// 楼栋的预售证
        /// </summary>
        public string PermitPresale { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildingArea { get; set; }

        /// <summary>
        /// 使用面积
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// 室
        /// </summary>
        public int Room { get; set; }
        /// <summary>
        /// 厅
        /// </summary>
        public int Hall { get; set; }
        /// <summary>
        /// 卫
        /// </summary>
        public int Toilet { get; set; }
        /// <summary>
        /// 厨
        /// </summary>
        public int Kitchen { get; set; }

        /// <summary>
        /// 地上楼层
        /// </summary>
        public int TheFloor { get; set; }

        /// <summary>
        /// 采光情况
        /// </summary>
        public int Orientation { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 价格类型
        /// </summary>
        public int PriceType { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }
    }
}
