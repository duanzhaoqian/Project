
namespace TXCommons.Index
{
    public class IndexRoom
    {
        /// <summary>
        /// 楼盘ID
        /// </summary>
        public string PremisesID { get; set; }
        /// <summary>
        /// 房源ID
        /// </summary>
        public string HouseID { get; set; }
        /// <summary>
        /// 单元ID
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 楼栋ID
        /// </summary>
        public string BuildingId { get; set; }
        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// 楼栋单位
        /// </summary>
        public string BuildingNameType { get; set; }
        /// <summary>
        /// 户型图ID
        /// </summary>
        public string RID { get; set; }
        /// <summary>
        /// 室
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// 客厅
        /// </summary>
        public string Hall { get; set; }
        /// <summary>
        /// 卫
        /// </summary>
        public string Toilet { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string BuildingArea { get; set; }
        /// <summary>
        /// 使用面积
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 户型图URL
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 户型图片描述
        /// </summary>
        public string PicDesc { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public string Hits { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图片详情地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string CreateTime { get; set; }
     
    }
}
