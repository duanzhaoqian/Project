
namespace TXModel.Web
{
    public class PremisesHouseHuX
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

        public int PictureData { get; set; }
        public string  desc { get; set; }
    }
}
