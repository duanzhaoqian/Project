
namespace TXCommons.Index
{
    public class IndexRankingConditionInfo
    {
        /// <summary>
        /// 省份ID (下标 0)
        /// </summary>
        public string ProvinceID { get; set; }
        /// <summary>
        /// 城市ID (下标 1)
        /// </summary>
        public string CityID { get; set; }
        /// <summary>
        /// 区域ID (下标 2)
        /// </summary>
        public string DistrictID { get; set; }
        /// <summary>
        /// 商圈ID (下标 3)
        /// </summary>
        public string BusinessID { get; set; }
        /// <summary>
        /// 楼盘ID (下标 4)
        /// </summary>
        public string PremisesID { get; set; }
        /// <summary>
        /// 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
        /// </summary>
        public string Ranking { get; set; }

        private string _PageIndex = "1";
        /// <summary>
        /// 第几页  (下标 6)
        /// </summary>
        public string PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }

        private string _PageSize = "10";
        /// <summary>
        /// 每页显示大小 (下标 7)
        /// </summary>
        public string PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
 

    }
}
