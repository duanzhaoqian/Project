
namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-17
    /// Desc：运费计算
    /// </summary>
    public class FreightCost
    {

        public int Id { get; set; }

        /// <summary>
        /// 运费模板ID
        /// </summary>
        public int TempId { get; set; }

        /// <summary>
        /// 运费类型：0 未知，1 快递，2 EMS， 3 平邮，4 物流
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 运费使用城市（格式 北京ID,上海ID,深圳ID）
        /// </summary>
        public string CityIds { get; set; }

        /// <summary>
        /// 运费首件
        /// </summary>
        public decimal FirstIme { get; set; }

        /// <summary>
        /// 首件价格
        /// </summary>
        public decimal FirstPrice { get; set; }

        /// <summary>
        /// 运费续件
        /// </summary>
        public decimal ContinuetIme { get; set; }

        /// <summary>
        /// 续件价格
        /// </summary>
        public decimal ContinuePrice { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
