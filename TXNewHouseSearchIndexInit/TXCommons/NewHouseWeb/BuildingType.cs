using System.Linq;

namespace TXCommons.NewHouseWeb
{
    public class BuildingType
    {
        /// <summary>
        /// 物业类型
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetPropertyType(string typeId)
        {
            //           物业类型
            //1住宅
            //2写字楼
            //3别墅
            //4商业
            string str = "";
            if (typeId.Contains('1'))
            {
                str = str + ",住宅";
            }
            if (typeId.Contains('2'))
            {
                str = str + ",写字楼";
            }
            if (typeId.Contains('3'))
            {
                str = str + ",别墅";
            }
            if (typeId.Contains('4'))
            {
                str = str + ",商业";
            }
            return str.TrimStart(',');
        }
        /// <summary>
        /// 装修程度
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetRenovation(int typeId)
        {
            //           装修程度
            //1毛坯、
            //2简装修、
            //3精装修、
            //4菜单式装修、
            //5公共部分精装修、
            //6全装修
            string str = "";
            if (typeId == 1)
            {
                str = "毛坯";
            }
            else if (typeId == 2)
            {
                str = "简装修";
            }
            if (typeId == 3)
            {
                str = "精装修";
            }
            if (typeId == 4)
            {
                str = "菜单式装修";
            }
            if (typeId == 5)
            {
                str = "公共部分精装修";
            }
            if (typeId == 6)
            {
                str = "全装修";
            }
            return str;
        }


        /// <summary>
        /// 楼栋所处楼盘位置
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetBuildingPosition(int typeId)
        {
//           楼栋所处楼盘位置
//1临街、2中部、3东部、4西部、5南部、6北部
            string str = "";
            if (typeId == 1)
            {
                str = "临街";
            }
            else if (typeId == 2)
            {
                str = "中部";
            }
            if (typeId == 3)
            {
                str = "东部";
            }
            if (typeId == 4)
            {
                str = "西部";
            }
            if (typeId == 5)
            {
                str = "南部";
            }
            if (typeId == 6)
            {
                str = "北部";
            }
            return str;
        }

        /// <summary>
        /// 梯户配比
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetLadder(int typeId)
        {
            // 梯户配比
            //1 1梯2户
            string str = "1梯2户";
            if (typeId == 1)
            {
                str = "1梯1户";
            }
            if (typeId == 2)
            {
                str = "1梯2户";
            }
            if (typeId == 3)
            {
                str = "1梯3户";
            }
            if (typeId == 4)
            {
                str = "1梯4户";
            }
            if (typeId == 5)
            {
                str = "1梯5户";
            }
            if (typeId == 6)
            {
                str = "1梯6户";
            }
            return str;
        }
    }
}
