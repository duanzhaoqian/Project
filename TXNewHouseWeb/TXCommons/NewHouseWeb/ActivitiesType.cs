
namespace TXCommons.NewHouseWeb
{
    public class ActivitiesType
    {
        /// <summary>
        /// 1摇号2限时折扣3排号购房4阶梯团购5竞价6砍价7秒杀8一口价
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ActivitiesTypeByNo(int type){
            string str="";
            switch(type){
                case 1:
                    str = "摇号";
                    break;
                case 2:
                    str = "限时折扣";
                    break;
                case 3:
                    str = "排号购房";
                    break;
                case 4:
                    str = "阶梯团购";
                    break;
                case 5:
                    str = "竞价";
                    break;
                case 6:
                    str = "砍价";
                    break;
                case 7:
                    str = "秒杀";
                    break;
                case 8:
                    str = "一口价";
                    break;
            }
            return str;
        }
    }
}
