
namespace TXCommons.NewHouseWeb
{
    public class PremisesType
    {
        /// <summary>
        /// 环线位置
        /// </summary>
        /// <param name="ringTypeId"></param>
        /// <returns></returns>
        public static string GeRingType(int ringTypeId)
        {
                // 环线位置
                //1 一环
                //2二环
                //3 三环
                //4四环
                //5五环
                //6六环
            string str = "";
            if (ringTypeId == 1)
            {
                str = "一环";
            }
            else if (ringTypeId == 2)
            {
                str = "二环";
            }
            if (ringTypeId == 3)
            {
                str = "三环";
            }
            if (ringTypeId == 4)
            {
                str = "四环";
            }
            if (ringTypeId == 5)
            {
                str = "五环";
            }
            if (ringTypeId == 6)
            {
                str = "六环";
            }
            return str;
        }

        /// <summary>
        /// 销售状态
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetSalesStatus(int typeId)
        {
            //           销售状态
            //0 待售
            //1 在售
            //2 售罄
            string str = "";
            if (typeId == 0)
            {
                str = "待售";
            }
            else if (typeId == 1)
            {
                str = "在售";
            }
            if (typeId == 2)
            {
                str = "售罄";
            }
            return str;
        }

        /// <summary>
        /// 建筑类别
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetBuildingType(int typeId)
        {
            //            建筑类别
            //1板楼
            //2塔楼
            //3砖楼
            //4砖混
            //5平房
            //6钢混
            string str = "板楼";
            if (typeId == 1)
            {
                str = "板楼";
            }
            else if (typeId == 2)
            {
                str = "塔楼";
            }
            if (typeId == 3)
            {
                str = "砖楼";
            }
            if (typeId == 4)
            {
                str = "砖混";
            }
            if (typeId == 5)
            {
                str = "平房";
            }
            if (typeId == 6)
            {
                str = "钢混";
            }
            return str;
        }
    }
}
