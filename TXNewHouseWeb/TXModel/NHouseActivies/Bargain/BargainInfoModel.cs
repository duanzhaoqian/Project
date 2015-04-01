using System;

namespace TXModel.NHouseActivies.Bargain
{
    public class BargainInfoModel
    {

        public int Floor { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AddPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Bond { get; set; }
        public int ActivitieState { get; set; }
    }
}
