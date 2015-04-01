using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace TXCommons.Admins
{
    public class AdminComs
    {
        private static AdminComs _instance;

        public static AdminComs Instance
        {
            get { return _instance ?? (_instance = new AdminComs()); }
        }

        #region 通用方法

        /// <summary>
        /// 获取剩余时间
        /// </summary>
        /// <param name="endTime">结束时间</param>
        /// <param name="beginTime">当前（开始）时间</param>
        /// <returns></returns>
        public string GetRestTime(DateTime endTime, DateTime beginTime)
        {
            var timespan = endTime - beginTime;
            var timeBuilder = new StringBuilder();
            if (0 < timespan.Days)
            {
                timeBuilder.AppendFormat("{0}天", timespan.Days);
            }

            if (0 < timespan.Hours)
            {
                timeBuilder.AppendFormat("{0}小时", timespan.Hours);
            }

            if (0 < timespan.Minutes)
            {
                timeBuilder.AppendFormat("{0}分钟", timespan.Minutes);
            }

            if (string.IsNullOrEmpty(timeBuilder.ToString()))
            {
                timeBuilder.Append("0");
            }

            return timeBuilder.ToString();
        }

        /// <summary>
        /// 获取环线
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, string>> GetRingLine(int cityId)
        {
            var list = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(0, "环线") };
            switch (cityId)
            {
                case 253:
                    //北京市
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "二环以内"),
                        new KeyValuePair<int, string>(2, "二环至三环"),
                        new KeyValuePair<int, string>(3, "三环至四环"),
                        new KeyValuePair<int, string>(4, "四环至五环"),
                        new KeyValuePair<int, string>(5, "五环至六环"),
                        new KeyValuePair<int, string>(6, "六环以外")
                    }); break;
                case 239:
                    // 上海
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "内环以内"),
                        new KeyValuePair<int, string>(2, "中内环间"),
                        new KeyValuePair<int, string>(3, "中外环间"),
                        new KeyValuePair<int, string>(4, "外环以外")
                    }); break;
                case 155:
                    // 成都
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "一环以内"),
                        new KeyValuePair<int, string>(2, "一至二环"),
                        new KeyValuePair<int, string>(3, "二至三环"),
                        new KeyValuePair<int, string>(4, "三环以外")
                    }); break;
                case 205:
                    // 天津
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "内环以内"),
                        new KeyValuePair<int, string>(2, "内至中环"),
                        new KeyValuePair<int, string>(3, "中至外环"),
                        new KeyValuePair<int, string>(4, "外环以外")
                    }); break;
                case 224:
                    // 武汉
                    list.AddRange(new List<KeyValuePair<int, string>>
                    {
                        new KeyValuePair<int, string>(1, "内环以内"),
                        new KeyValuePair<int, string>(2, "内至二环"),
                        new KeyValuePair<int, string>(3, "二至中环"),
                        new KeyValuePair<int, string>(4, "中环以外")
                    }); break;
                default:
                    return null;
            }
            return list;
        }

        #endregion

        #region 通用方法（活动）

        /// <summary>
        /// 判断活动是否在今天开始
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public bool IsActivityStartInToday(DateTime start)
        {
            if (start.Year == DateTime.Now.Year
                && start.Month == DateTime.Now.Month
                && start.Day == DateTime.Now.Day)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断活动是否在今天的指定这个小时开始
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public bool IsActivityStartInHour(DateTime start)
        {
            if (start.Year == DateTime.Now.Year
                && start.Month == DateTime.Now.Month
                && start.Day == DateTime.Now.Day
                && start.Hour <= DateTime.Now.Hour)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}