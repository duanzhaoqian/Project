using System;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 竞价活动 实体
    /// </summary>
    public class PVM_NH_Biding
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
        /// <summary>
        /// 开发商名字
        /// </summary>
        public string Developer { set; get; }
        /// <summary>
        /// 代理商名字
        /// </summary>
        public string Agent { set; get; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
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
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public int ProvinceId { get; set; }
        /// <summary>
        /// 省份名
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivitieState { get; set; }
        /// <summary>
        /// 活动状态显示
        /// </summary>
        public string ActivitieStateStr { get; set; }
        /// <summary>
        /// 剩余时间
        /// </summary>
        public string SurplusTimeString { get; set; }

        public string BeginTimeStr { get; set; }
        public string EndTimeStr { get; set; }
        /// <summary>
        /// 时间标志 0 未开始 1 进行中 2 已结束
        /// </summary>
        public int TimeFlag { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal Bond { get; set; }
        /// <summary>
        /// 活动房源关系id
        /// </summary>
        public int ActivitiesHouseId { get; set; }
    }
}
