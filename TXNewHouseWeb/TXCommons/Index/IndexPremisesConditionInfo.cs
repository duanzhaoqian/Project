
namespace TXCommons.Index
{
    public class IndexPremisesConditionInfo
    {
        /// <summary>
        /// 省份ID (下标 0)
        /// </summary>
        public string ProvinceID { get; set; }
        /// <summary>
        /// 省份名称 (下标 1)
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市ID (下标 2)
        /// </summary>
        public string CityID { get; set; }
        /// <summary>
        /// 城市成名 (下标 3)
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区域ID (下标 4)
        /// </summary>
        public string DistrictID { get; set; }
        /// <summary>
        /// 区域名称 (下标 5)
        /// </summary>
        public string DistrictName { get; set; }
        /// <summary>
        /// 商圈ID (下标 6)
        /// </summary>
        public string BusinessID { get; set; }
        /// <summary>
        /// 商圈名称 (下标 7)
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 地铁线 (下标 8)
        /// </summary>
        public string TrafficLine { get; set; }
        /// <summary>
        /// 地铁站 (下标 9)
        /// </summary>
        public string TrafficStation { get; set; }
        /// <summary>
        /// 物业类型 (下标 10) 1 住宅 2 写字楼 3 别墅 4 商业
        /// </summary>
        public string PropertyType { get; set; }
        /// <summary>
        /// 面积 9 开始 (下标 11)
        /// </summary>
        public string AreaBegin { get; set; }
        /// <summary>
        /// 面积 9 结束(下标 12)
        /// </summary>
        public string AreaEnd { get; set; }
        /// <summary>
        /// 房间面积
        /// </summary>
        public string HouseArea { get; set; }
        /// <summary>
        /// 起始价格 (下标 13)
        /// </summary>
        public string PriceBegin { get; set; }
        /// <summary>
        /// 结束价格 (下标 14)
        /// </summary>
        public string PriceEnd { get; set; }
        /// <summary>
        /// 居室 (下标 15) 户型 5居以上（6）
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// 环线 (下标 16)
        /// </summary>
        public string Ring { get; set; }
        /// <summary>
        /// 建筑类别 (下标 17)
        /// </summary>
        public string BuildingType { get; set; }
        /// <summary>
        /// 项目特色 (下标 18)
        /// </summary>
        public string Characteristic { get; set; }
        /// <summary>
        /// 开盘时间 (下标 19) 1 本月开盘 2 下月开盘 3 三月内开盘 4 六月内开盘  5 前三个月已开 6 前六个月已开
        /// </summary>
        public string OpeningTime { get; set; }
        /// <summary>
        /// 装修程度 (下标 20) 1 毛坯 2 简装修 3 中等装修 4 精装修 5 豪华装修
        /// </summary>
        public string Renovation { get; set; }
        /// <summary>
        /// 售价排序 (下标 21) 1 升序 2 降序
        /// </summary>
        public string PriceSort { get; set; }
        /// <summary>
        /// 开盘时间排序 (下标 22) 3 升序 4 降序
        /// </summary>
        public string OpeningSort { get; set; }
        /// <summary>
        /// 入住时间排序 (下标 23) 5 升序 6 降序
        /// </summary>
        public string CheckInSort { get; set; }
        /// <summary>
        /// 销售状态 (下标 24) 0 待售 1 在售 2 售罄
        /// </summary>
        public string SalesStatus { get; set; }
        /// <summary>
        /// 模糊搜索 （下标 25）
        /// </summary>
        public string KeyWord { get; set; }

        private string _PageIndex = "1";
        /// <summary>
        /// 第几页  (下标 26)
        /// </summary>
        public string PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }

        private string _PageSize = "10";
        /// <summary>
        /// 每页显示大小 (下标 27)
        /// </summary>
        public string PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        /// <summary>
        /// 楼盘ID （下标 28）
        /// </summary>
        public string PremisesID { get; set; }
        /// <summary>
        /// 特色数量，排序用（下标 29）
        /// </summary>
        public string CharacteristicCount { get; set; }
        /// <summary>
        /// 特色数量升降序
        /// </summary>
        public string CharacteristicSort { get; set; }


        /// <summary>
        ///区域搜索类型 quyu:区域 sub:地铁  4
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// type=quyu:区域Id type=sub:地铁   5
        /// </summary>
        public string DistrictPY { get; set; }
        /// <summary>
        /// type=quyu:商圈Id type=sub:站点   6
        /// </summary>
        public string BusinessPY { get; set; }
       
    }
}
