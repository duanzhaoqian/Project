using System;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 
    /// </summary>
    public class PVM_NH_SecKill
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public int Id { set; get; }

        public int HouseId { set; get; }

        public string HouseName { set; get; }

        public int Floor { set; get; }

        public string FloorString { set; get; }

        public int UnitNum { set; get; }

        public string UnitName { set; get; }

        public int BuildingId { set; get; }

        public string BuildingName { set; get; }

        public string BuildingNameType { set; get; }

        // public string DeveloperName { set; get; }

        public string Developer { set; get; }

        public string Agent { set; get; }

        public decimal TotalPrice { set; get; }

        public string TotalPriceString { set; get; }

        public decimal BidPrice { set; get; }

        public string BidPriceString { set; get; }

        public DateTime BeginTime { set; get; }

        public DateTime EndTime { set; get; }

        public string ActivityTime { set; get; }

        public string RestTimeString { set; get; }

        public string BelongCity { set; get; }

        public int ActivitieState { set; get; }

        public string ActivitieStateString { set; get; }

    }
}