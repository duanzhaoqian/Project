using System;

namespace TXModel.Web
{
    public class HouseAndBuilding
    {
        /// <summary>
        /// 房源ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 楼栋名称
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
        /// 房号
        /// </summary>
        public string HouseName { get; set; }

        /// <summary>
        /// 楼层
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
        public double Area { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal SinglePrice { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        public int DeveloperId { get; set; }

        public int BuildingId { get; set; }

        public int PremisesId { get; set; }

        public int UnitId { get; set; }

        public int IsRelease { get; set; }

        public int CityID { get; set; }


    }
}
