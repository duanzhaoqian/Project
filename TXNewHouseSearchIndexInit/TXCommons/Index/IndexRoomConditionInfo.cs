
namespace TXCommons.Index
{
    public class IndexRoomConditionInfo
    {
        /// <summary>
        /// 城市ID 下标0
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 楼盘ID 下标1
        /// </summary>
        public string PremisesID { get; set; }
        /// <summary>
        /// 楼栋ID 下标2
        /// </summary>
        public string BuildingId { get; set; }
        /// <summary>
        /// 室 下标3
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// 起始面积 下标4
        /// </summary>
        public string BeginArea { get; set; }
        /// <summary>
        /// 终止面积 下标5
        /// </summary>
        public string EndArea { get; set; }
        /// <summary>
        /// 户型图ID 下标6
        /// </summary> 
        public string RID { get; set; }

        private string _PageIndex = "1";
        /// <summary>
        /// 第几页  (下标 7)
        /// </summary>
        public string PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        private string _PageSize = "10";
        /// <summary>
        /// 每页显示大小 (下标 8)
        /// </summary>
        public string PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
    }
}
