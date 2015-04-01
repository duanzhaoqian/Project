using System;
using System.Collections.Generic;

namespace TXModel
{
    public partial class CT_Activity
    {

        /// <summary>
        /// 活动Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 活动房源数量
        /// </summary>
        public int HouseCount { get; set; }
        /// <summary>
        /// 活动创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 活动保证金
        /// </summary>
        public decimal Bond { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivitieState { get; set; }
        /// <summary>
        /// 活动折扣
        /// </summary>
        public int MinDiscount { get; set; }

        /// <summary>
        /// 活动折扣集合
        /// </summary>
        public IEnumerable<int> ListDiscount { get; set; }
    }

    public partial class CT_ActivitiesHouse
    {

        /// <summary>
        /// 房号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 楼盘
        /// </summary>
        public string Premis { get; set; }

        /// <summary>
        /// 楼盘
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// 楼盘
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 楼盘
        /// </summary>
        public int Floor { get; set; }

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
        /// 建筑面积
        /// </summary>
        public double BuildingArea { get; set; }

        /// <summary>
        /// 市场价 总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal SinglePrice { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public int Discount { get; set; }
    }
}
