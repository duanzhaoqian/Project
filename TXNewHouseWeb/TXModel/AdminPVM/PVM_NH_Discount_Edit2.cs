
namespace TXModel.AdminPVM
{
    public class PVM_NH_Discount_Edit2
    {
        public int HouseId { set; get; }

        public string HouseName { set; get; }

        public int Floor { set; get; }

        public string FloorName { set; get; }

        public string BuildingName { set; get; }

        public string BuildingNameType { set; get; }

        #region 户型

        public int Room { set; get; }

        public int Hall { set; get; }

        public int Toilet { set; get; }

        public int Kitchen { set; get; }

        public string RoomTypeString { set; get; }

        #endregion

        public double BuildingArea { set; get; }

        public string BuildingAreaString { set; get; }

        public decimal TotalPrice { set; get; }

        public string TotalPriceString { set; get; }

        public int Discount { set; get; }

        public string DiscountString { set; get; }

        public decimal AfterDiscountPrice { set; get; }

        public string AfterDiscountPriceString { set; get; }
    }
}