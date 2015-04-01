using System;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 新房房源实体
    /// </summary>
    public class PVM_NH_House
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// guid
        /// </summary>
        public string InnerCode { get; set; }
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
        /// 建筑类型
        /// </summary>
        public int BuildingType { get; set; }
        /// <summary>
        /// 采光情况
        /// </summary>
        public int Orientation { get; set; }
        /// <summary>
        /// 价格类型
        /// </summary>
        public int PriceType { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal SinglePrice { get; set; }
        /// <summary>
        /// 首付款
        /// </summary>
        public decimal DownPayment { get; set; }
        /// <summary>
        /// 销售状态
        /// </summary>
        public int SalesStatus { get; set; }
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
        /// 阳台
        /// </summary>
        public int Balcony { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 销售状态（界面显示）
        /// </summary>
        public string SalesStatusStr { get; set; }
        /// <summary>
        /// 价格类型（界面显示）
        /// </summary>
        public string PriceTypeStr { get; set; }
        /// <summary>
        /// 楼栋名
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 楼盘名
        /// </summary>
        public string PremisesName { get; set; }

        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public int CityId { get; set; }
    }
    /// <summary>
    /// 返回house id和name
    /// </summary>
    public class HouseIdAndName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
