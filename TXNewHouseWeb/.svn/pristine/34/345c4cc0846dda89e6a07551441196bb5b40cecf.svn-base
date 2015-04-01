
namespace TXCommons.NewHouseWeb
{
    public class HouseType
    {
        /// <summary>
        /// 建筑类别
        /// </summary>
        /// <param name="buildingTypeId"></param>
        /// <returns></returns>
        public static string GetBuildingType(int buildingTypeId)
        {
            //            建筑类别
            //1板楼
            //2塔楼
            //3砖楼
            //4砖混
            //5平房
            //6钢混
            string str = "";
            if (buildingTypeId == 1)
            {
                str = "板楼";
            }
            else if (buildingTypeId == 2)
            {
                str = "塔楼";
            }
            if (buildingTypeId == 3)
            {
                str = "砖楼";
            }
            if (buildingTypeId == 4)
            {
                str = "砖混";
            }
            if (buildingTypeId == 5)
            {
                str = "平房";
            }
            if (buildingTypeId == 6)
            {
                str = "钢混";
            }
            return str;
        }

        /// <summary>
        /// 物业类型
        /// </summary>
        /// <param name="typeId">类型编号</param>
        /// <returns></returns>
        public static string GetPropertyType(int typeId)
        {
            //            物业类型
            //1住宅
            //2写字楼
            //3别墅
            //4商业
            string str = "";
            if (typeId == 1)
            {
                str = "住宅";
            }
            else if (typeId == 2)
            {
                str = "写字楼";
            }
            if (typeId == 3)
            {
                str = "别墅";
            }
            if (typeId == 4)
            {
                str = "商业";
            }
            return str;
        }

        /// <summary>
        /// 装修情况
        /// </summary>
        /// <param name="typeId">类型编号</param>
        /// <returns></returns>
        public static string GetRenovation(int typeId)
        {
            //            装修程度
            //1毛坯2简装修、3中等装修、4精装修、5豪华装修
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
                str = "中等装修";
            }
            if (typeId == 4)
            {
                str = "精装修";
            }
            if (typeId == 5)
            {
                str = "豪华装修";
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
            //          销售状态
            //0待售
            //1开发商保留
            //2在售
            //3已认购
            //4已签约
            //5已备案
            //6已办产权
            //7被限制
            //8拆迁安置
            //9售罄
            string str = "";
            if (typeId == 0)
            {
                str = "待售";
            }
            else if (typeId == 1)
            {
                str = "开发商保留";
            }
            if (typeId == 2)
            {
                str = "在售";
            }
            if (typeId == 3)
            {
                str = "已认购";
            }
            if (typeId == 4)
            {
                str = "已签约";
            }
            if (typeId == 5)
            {
                str = "已备案";
            }
            if (typeId == 6)
            {
                str = "已办产权";
            }
            if (typeId == 7)
            {
                str = "被限制";
            }
            if (typeId == 8)
            {
                str = "拆迁安置";
            }
            if (typeId == 9)
            {
                str = "售罄";
            }
            return str;
        }

        /// <summary>
        /// 销售状态
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public static string GetSalesStatusClass(int typeId)
        {
            //<span class=\"bgcol_1 ml10\">&nbsp;</span>开发商保留<span class=\"bgcol_2 ml35\">&nbsp;</span>可售<span
            //                class=\"bgcol_3 ml35\">&nbsp;</span>已认购<span class=\"bgcol_4 ml35\">&nbsp;</span>已签约<span
            //                    class=\"bgcol_5 ml35\">&nbsp;</span>已备案<span class=\"bgcol_6 ml35\">&nbsp;</span>已办产权<span
            //                        class=\"bgcol_1 ml35\">&nbsp;</span>被限制<span class=\"bgcol_7 ml35\">&nbsp;</span>拆迁安置
            string str = "";
            if (typeId == 0)
            {
                str = "class=\"bgcol_9\"";
            }
            else if (typeId == 1)
            {
                str = "class=\"bgcol_11\"";
            }
            if (typeId == 2)
            {
                str = "class=\"bgcol_22\"";
            }
            if (typeId == 3)
            {
                str = " class=\"bgcol_33\"";
            }
            if (typeId == 4)
            {
                str = "class=\"bgcol_44\"";
            }
            if (typeId == 5)
            {
                str = "class=\"bgcol_55\"";
            }
            if (typeId == 6)
            {
                str = "class=\"bgcol_66\"";
            }
            if (typeId == 7)
            {
                str = "class=\"bgcol_88\"";
            }
            if (typeId == 8)
            {
                str = "class=\"bgcol_77\"";
            }
            if (typeId == 9)
            {
                str = "class=\"bgcol_10\"";
            }
            return str;
        }


        /// <summary>
        /// 采光情况
        /// </summary>
        /// <param name="typeId">类型编号</param>
        /// <returns></returns>
        public static string GetOrientation(int typeId)
        {
            var str = "";
            //1东、2南、3西、4北、5东南、6东北、7西南、8西北、9南北、10东西
            string[] arr = new string[] { "东", "南", "西", "北", "东南", "东北", "西南", "西北", "南北", "东西" };
            if (typeId > 0 && typeId <= arr.Length)
            {
                str = arr[typeId - 1];
            }

            return str;
        }
    }
}
