using System;

namespace TXModel.NHouseActivies.Discount
{
    public class CreateActivityModel
    {
        public int DeveloperId { get; set; }
        public int PremisesId { get; set; }
        public int BuildingId { get; set; }
        public int HouseId { get; set; }
        public int Discount { get; set; }
        public string Name { get; set; }
        public double Bond { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
