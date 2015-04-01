
namespace TXModel.Web
{
    public class BuildingHouseHuX
    {
        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildingArea { get; set; }

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
        /// 楼栋id
        /// </summary>
        public int BuildingId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public int Rid { get; set; }

        /// <summary>
        /// 房源id
        /// </summary>
        public int HouseId { get; set; }
    }
}
