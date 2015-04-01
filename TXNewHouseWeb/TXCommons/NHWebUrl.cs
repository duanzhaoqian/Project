using System;
using System.Text.RegularExpressions;

namespace TXCommons
{
    public class NHWebUrl
    {
        /// <summary>
        /// 前台楼栋详情页
        /// </summary>
        /// <param name="pid">楼盘Id</param>
        /// <param name="bid">楼栋Id</param>
        /// <returns></returns>
        public static string BuildingDetailUrl(string pid, string bid)
        {
            string url = String.Empty;
            if (!String.IsNullOrWhiteSpace(pid) && !String.IsNullOrWhiteSpace(bid))
                url = String.Format(GetConfig.GetSiteRoot + "/lp-fy-p{0}-b{1}", pid, bid);
            return url;
        }

        /// <summary>
        /// 楼盘首页
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public static string PremisesIndexUrl(string premisesId)
        {
            var key = Regex.Match(premisesId, @"\d+").ToString();//关键字
            string url = "";
            if (key != "")
                url = GetConfig.GetSiteRoot + "/lp-" + premisesId + ".html";
            return url;
        }

        /// <summary>
        /// 楼盘详情页
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public static string PremisesDetailUrl(string premisesId)
        {
            var key = Regex.Match(premisesId, @"\d+").ToString();//关键字
            string url = "";
            if (key != "")
                url = GetConfig.GetSiteRoot + "/lp-xq-" + premisesId + ".html";
            return url;
        }

        /// <summary>
        /// 楼盘房源页
        /// </summary>
        /// <param name="premisesId">楼盘ID</param>
        /// <param name="bId">楼栋Id</param>
        /// <param name="uId">单元Id</param>
        /// <param name="fId">楼层</param>
        /// <returns></returns>
        public static string PremisesHouseUrl(string premisesId, string bId, string uId, string fId)
        {
            var _pid = Regex.Match(premisesId, @"\d+").ToString();//楼盘Id
            var _bid = Regex.Match(bId, @"\d+").ToString();//楼栋Id
            var _uid = Regex.Match(uId, @"\d+").ToString();//单元Id
            var _fid = Regex.Match(fId, @"\d+").ToString();//楼层
            string url = "";
            if (_pid != "")
                url += GetConfig.GetSiteRoot + "/lp-fy-p" + premisesId;
            if (_bid != "")
                url += "-b" + bId;
            if (_uid != "")
                url += "-u" + uId;
            if (_fid != "")
                url += "-f" + fId;
            return url;
        }

        /// <summary>
        /// 楼盘房间信息页
        /// </summary>
        /// <param name="id">房源ID</param>
        /// <returns></returns>
        public static string PremisesHouseInfoUrl(string houseId)
        {
            var key = Regex.Match(houseId, @"\d+").ToString();//关键字
            string url = "";
            if (key != "")
                url = GetConfig.GetSiteRoot + "/lp-fy-house-" + houseId + ".html";
            return url;
        }

        /// <summary>
        /// 摇号结果列表页
        /// </summary>
        /// <returns></returns>
        public static string ActivityResultUrl()
        {
            return GetConfig.GetSiteRoot + "/yhhd-yhjg";
        }

        /// <summary>
        /// 摇号活动列表页
        /// </summary>
        /// <returns></returns>
        public static string ActivityUrl()
        {
            return GetConfig.GetSiteRoot + "/yhhd";
        }

        /// <summary>
        /// 工具页
        /// </summary>
        /// <returns></returns>
        public static string ToolsUrl()
        {
            return GetConfig.GetSiteRoot + "/gj-dk.html";
        }

        /// <summary>
        /// 摇号直播页
        /// </summary>
        /// <param name="id">活动ID</param>
        /// <returns></returns>
        public static string ActivityLiveUrl(string activityId)
        {
            var key = Regex.Match(activityId, @"\d+").ToString();//关键字
            string url = "";
            if (key != "")
                url = GetConfig.GetSiteRoot + "/yhhd-live_" + activityId + ".html";
            return url;
        }

        /// <summary>
        /// 图片详情页
        /// </summary>
        /// <param name="type">xgt效果图|hxt户型图|ght规划图|wzt位置图|ldt楼栋平面图|sjt实景图|jtt交通图</param>      
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="picId">图片Id</param>
        /// <param name="where">图片类型</param>
        /// <param name="houseID">房源ID</param>
        /// <returns></returns>
        public static string ImageDetailsUrl(string type, string premisesId, string picId, string where, string houseID)
        {
            var _pid = Regex.Match(premisesId, @"\d+").ToString();//楼盘Id
            var _picid = Regex.Match(picId, @"\d+").ToString();//图片Id
            string url = "";
            if (_pid != "" && _picid != "")
                url += GetConfig.GetSiteRoot + "/lp-pic-" + type + "-" + premisesId + "-" + picId;
            if (where != "")
                url += where;
            if (!string.IsNullOrEmpty(houseID))
                url += "-h" + houseID;
            return url;
        }

        /// <summary>
        /// 楼盘相册页
        /// </summary>
        /// <param name="type">1.户型图2.规划图3.位置图4.楼栋平面图5.效果图6.实景图7.交通图</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="t">小类（r1,d1,m1）</param>
        /// <returns></returns>
        public static string AlbumUrl(string type, string premisesId, string t)
        {
            var _pid = Regex.Match(premisesId, @"\d+").ToString();//楼盘Id
            string url = "";
            if (_pid != "")
            {
                url += GetConfig.GetSiteRoot + "/lp-xc";
                if (type != "")
                    url += "-" + type;
                url += "-" + premisesId;
                if (t != "")
                    url += "-" + t;
            }
            return url;
        }

        /// <summary>
        /// 楼盘户型图页
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="loudong">楼栋</param>
        /// <param name="mianji">面积</param>
        /// <param name="tingshi">厅室</param>
        /// <param name="show">显示1.图片,2.列表</param>
        /// <returns></returns>
        public static string SizeChartUrl(string premisesId, int mianji, int tingshi, int loudong, int show)
        {
            var _pid = Regex.Match(premisesId, @"\d+").ToString();//楼盘Id
            string url = "";
            if (_pid != "")
            {
                url += GetConfig.GetSiteRoot + "/lp-hxt-" + premisesId;
                if (mianji > 0)
                    url += "-m" + mianji;
                if (tingshi > 0)
                    url += "-r" + tingshi;
                if (loudong > 0)
                    url += "-d" + loudong;
                url += "-l" + show;
            }
            return url;
        }

        /// <summary>
        /// 楼盘交通配套页
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public static string TrafficFacilitiesUrl(string premisesId)
        {
            var key = Regex.Match(premisesId, @"\d+").ToString();//关键字
            string url = "";
            if (key != "")
                url = GetConfig.GetSiteRoot + "/lp-jtpt-" + premisesId;
            return url;
        }

        /// <summary>
        /// 新房首页
        /// </summary>
        /// <returns></returns>
        public static string NewHouseUrl()
        {
            return GetConfig.GetSiteRoot + "/quyu";
        }

        /// <summary>
        /// 快有家首页
        /// </summary>
        /// <returns></returns>
        public static string KYJIndexUrl()
        {
            return GetConfig.GetQTBaseUrl;//"http://www.kuaiyoujia.com";
        }
    }
}
