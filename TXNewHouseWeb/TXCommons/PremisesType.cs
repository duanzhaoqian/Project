using System;
using System.Collections.Generic;
using System.Linq;

namespace TXCommons
{
    public class PremisesType
    {
        #region Search

        /// <summary>
        /// 获取物业类型: 1住宅 2写字楼 3别墅 4商业
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetTypeName(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "住宅";
                    break;
                case "2":
                    _str = "写字楼";
                    break;
                case "3":
                    _str = "别墅";
                    break;
                case "4":
                    _str = "商业";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 获取物业类型
        /// </summary>
        /// <param name="typeIds"></param>
        /// <returns></returns>
        public static string[] GetPropertyTypeByIds(string typeIds)
        {
            List<string> arr = new List<string>();
            if (!string.IsNullOrEmpty(typeIds))
            {
                var arrPropertyType = typeIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (arrPropertyType != null)
                {
                    arr = arrPropertyType.ToList().Select(a => TXCommons.PremisesType.GetTypeName(a)).ToList();
                }
            }
            return arr.ToArray();
        }

        /// <summary>
        /// 获取销售状态：0 待售1 在售2 售罄
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSalesStatus(string type)
        {
            string _str = "";
            switch (type)
            {
                case "0":
                    _str = "zt-i-3";
                    break;
                case "1":
                    _str = "zt-i-1";
                    break;
                case "2":
                    _str = "zt-i-2";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 获取环线位置：1 二环以内 2二至三环 3三至四环 4四至五环 5五至六环 6六环以外
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetRing(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "二环以内";
                    break;
                case "2":
                    _str = "二至三环";
                    break;
                case "3":
                    _str = "三至四环";
                    break;
                case "4":
                    _str = "四至五环";
                    break;
                case "5":
                    _str = "五至六环";
                    break;
                case "6":
                    _str = "六环以外";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 取搜索条件价格
        /// </summary>
        /// '', '0,9999', '10000,15000', '15000,20000', '20000,30000', '30000,40000', '40000,0'
        /// <returns></returns>
        public static string GetSearchPrice(string type)
        {
            string _str = "";
            switch (type)
            {
                case "-p1":
                    _str = "p0_9999";
                    break;
                case "-p2":
                    _str = "p10000_15000";
                    break;
                case "-p3":
                    _str = "p15000_20000";
                    break;
                case "-p4":
                    _str = "p20000_30000";
                    break;
                case "-p5":
                    _str = "p30000_40000";
                    break;
                case "-p6":
                    _str = "p40000_0";
                    break;

            }
            return _str;
        }

        /// <summary>
        /// 取搜索条件面积
        /// </summary>
        /// '', '0,50', '50,70', '70,90', '90,110', '110,130', '130,150', '150,200', '200,300', '300,0'
        /// <returns></returns>
        public static string GetSearchArea(string type)
        {
            string _str = "";
            switch (type)
            {
                case "-m1":
                    _str = "m0_50";
                    break;
                case "-m2":
                    _str = "m50_70";
                    break;
                case "-m3":
                    _str = "m70_90";
                    break;
                case "-m4":
                    _str = "m90_110";
                    break;
                case "-m5":
                    _str = "m110_130";
                    break;
                case "-m6":
                    _str = "m130_150";
                    break;
                case "-m7":
                    _str = "m150_200";
                    break;
                case "-m8":
                    _str = "m200_300";
                    break;
                case "-m9":
                    _str = "m300_0";
                    break;

            }
            return _str;
        }


        /// <summary>
        /// 获取户型
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetHouseTypes(string type)
        {
            string _str = "";
            switch (type)
            {
                case "":
                    _str = "";
                    break;
                case "0":
                    _str = "";
                    break;
                case "1":
                    _str = "一居";
                    break;
                case "2":
                    _str = "二居";
                    break;
                case "3":
                    _str = "三居";
                    break;
                case "4":
                    _str = "四居";
                    break;
                case "5":
                    _str = "五居";
                    break;
                default:
                    _str = "五居以上";
                    break;
            }
            return _str;
        }

        #endregion

        #region Seo

        /// <summary>
        /// 销售状态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSeoSalesStatus(string type)
        {
            string _str = "";
            switch (type)
            {//
                case "0":
                    _str = "待售楼盘";
                    break;
                case "1":
                    _str = "在售楼盘";
                    break;
                case "2":
                    _str = "售罄楼盘";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 开盘时间
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSeoOpeningTime(string type)
        {
            string _str = "";
            switch (type)
            {//
                case "1":
                    _str = "本月开盘";
                    break;
                case "2":
                    _str = "下月开盘";
                    break;
                case "3":
                    _str = "三月内开盘";
                    break;
                case "4":
                    _str = "六月内开盘";
                    break;
                case "5":
                    _str = "前三个月已开";
                    break;
                case "6":
                    _str = "前六个月已开";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 装修状况
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSeoRenovation(string type)
        {
            string _str = "";
            switch (type)
            {//
                case "1":
                    _str = "毛坯";
                    break;
                case "2":
                    _str = "简装修";
                    break;
                case "3":
                    _str = "中等装修";
                    break;
                case "4":
                    _str = "精装修";
                    break;
                case "5":
                    _str = "豪华装修";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 取建筑类别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSeoBuildingType(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "板楼";
                    break;
                case "2":
                    _str = "塔楼";
                    break;
                case "3":
                    _str = "砖楼";
                    break;
                case "4":
                    _str = "砖混";
                    break;
                case "5":
                    _str = "平房";
                    break;
                case "6":
                    _str = "钢混";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 获取物业类型: 1住宅 2写字楼 3别墅 4商业
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetSeoPropertyType(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "住宅";
                    break;
                case "2":
                    _str = "写字楼";
                    break;
                case "3":
                    _str = "别墅";
                    break;
                case "4":
                    _str = "商业";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 获取环线位置：1 二环以内 2二至三环 3三至四环 4四至五环 5五至六环 6六环以外
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSeoRing(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "二环以内";
                    break;
                case "2":
                    _str = "二至三环";
                    break;
                case "3":
                    _str = "三至四环";
                    break;
                case "4":
                    _str = "四至五环";
                    break;
                case "5":
                    _str = "五至六环";
                    break;
                case "6":
                    _str = "六环以外";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 取搜索条件价格
        /// </summary>
        /// '', '0,9999', '10000,15000', '15000,20000', '20000,30000', '30000,40000', '40000,0'
        /// <returns></returns>
        public static string GetSeoPrice(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "0-9999";
                    break;
                case "2":
                    _str = "10000-15000";
                    break;
                case "3":
                    _str = "15000-20000";
                    break;
                case "4":
                    _str = "20000-30000";
                    break;
                case "5":
                    _str = "30000-40000";
                    break;
                case "6":
                    _str = "40000-0";
                    break;

            }
            return _str;
        }

        /// <summary>
        /// 取搜索条件面积
        /// </summary>
        /// '', '0,50', '50,70', '70,90', '90,110', '110,130', '130,150', '150,200', '200,300', '300,0'
        /// <returns></returns>
        public static string GetSeoArea(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "0-50";
                    break;
                case "2":
                    _str = "50-70";
                    break;
                case "3":
                    _str = "70-90";
                    break;
                case "4":
                    _str = "90-110";
                    break;
                case "5":
                    _str = "110-130";
                    break;
                case "6":
                    _str = "130-150";
                    break;
                case "7":
                    _str = "150-200";
                    break;
                case "8":
                    _str = "200-300";
                    break;
                case "9":
                    _str = "300-0";
                    break;

            }
            return _str;
        }


        /// <summary>
        /// 获取户型
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetSeoHouseTypes(string type)
        {
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "一居";
                    break;
                case "2":
                    _str = "二居";
                    break;
                case "3":
                    _str = "三居";
                    break;
                case "4":
                    _str = "四居";
                    break;
                case "5":
                    _str = "五居";
                    break;
                case "6":
                    _str = "五居以上";
                    break;
            }
            return _str;
        }

        /// <summary>
        /// 获取楼盘特色
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetSeoCharacteristic(string type)
        {
            //
            string _str = "";
            switch (type)
            {
                case "1":
                    _str = "打折优惠楼盘";
                    break;
                case "2":
                    _str = "地铁沿线";
                    break;
                case "3":
                    _str = "现房";
                    break;
                case "4":
                    _str = "小户型";
                    break;
                case "5":
                    _str = "低总价";
                    break;
                case "6":
                    _str = "教育地产";
                    break;
                case "7":
                    _str = "公园地产";
                    break;
                case "8":
                    _str = "宜居生态地产";
                    break;
                case "9":
                    _str = "综合体";
                    break;
            }
            return _str;
        }
        #endregion

    }
}
