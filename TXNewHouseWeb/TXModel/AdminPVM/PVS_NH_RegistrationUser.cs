
namespace TXModel.AdminPVM
{
    public class PVS_NH_RegistrationUser
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 活动类型(1摇号 2限时折扣 3排号购房 4阶梯团购 5竞价 6砍价 7秒杀 8一口价)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 楼盘id
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremisesName { get; set; }
        /// <summary>
        /// 房源id
        /// </summary>
        public int HouseId { get; set; }

    }
}
