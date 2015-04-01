

namespace TXModel.AdminPVM
{
    public class PVE_NH_House_Detail
    {

        public string PremisesName { set; get; }

        public string BuildingName { set; get; }

        public string HouseName { set; get; }

        public string UnitName { set; get; }

        public int Floor { set; get; }

        public string FloorName { set; get; }

        public int Room { set; get; }

        public int Hall { set; get; }

        public int Toilet { set; get; }

        public int Kitchen { set; get; }

        public string HouseTypeStructure { set; get; }

        public int BuildingType { set; get; }

        public string BuildingTypeName { set; get; }

        public int Orientation { set; get; }

        public string OrientationName { set; get; }

        public int PriceType { set; get; }

        public string PriceTypeName { set; get; }

        public decimal TotalPrice { set; get; }

        public string TotalPriceString { set; get; }

        public decimal SinglePrice { set; get; }

        public string SinglePriceString { set; get; }

        public decimal DownPayment { set; get; }

        public string DownPaymentString { set; get; }

        public int SalesStatus { set; get; }

        public string SalesStatusName { set; get; }

        public double BuildingArea { set; get; }

        public string BuildingAreaString { set; get; }

        public double Area { set; get; }

        public string AreaString { set; get; }

        public int RId { set; get; }

        public double CoordX { set; get; }

        public double CoordY { set; get; }
    }
}