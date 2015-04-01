
namespace TXModel.Web
{
    /// <summary>
    /// 新房前台-楼盘轮播实体
    /// </summary>
    public class PremisesCarousel
    {
        public int Id { get; set; }

        /// <summary>
        /// 楼盘Id
        /// </summary>
        public int PremisesId { get; set; }

        /// <summary>
        /// 轮播小图URL
        /// </summary>
        public string SmallImgSrc { get; set; }

        /// <summary>
        /// 轮播大图URL
        /// </summary>
        public string  ImgSrc { get; set; }

        /// <summary>
        /// 图片描述
        /// </summary>
        public string ImgDesc { get; set; }
    }
}
