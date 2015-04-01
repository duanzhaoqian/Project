
namespace TXModel.NHouseActivies.Discount
{
    public class PremiseModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int DeveloperId { get; set; }
        public int CityId { get; set; }
    }
    public class BuildingModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
    }
    public class UnitModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
    }
    
}
