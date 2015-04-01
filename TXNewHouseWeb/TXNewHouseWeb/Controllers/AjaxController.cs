using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Text;
using TXCommons;
using TXModel.Ajax;
using TXBll.Ajax;
using TXBll.HouseData;
using TXOrm;


namespace TXNewHouseWeb.Controllers
{
    public class AjaxController : Controller
    {
        #region 取楼盘特色

        /// <summary>
        /// 取楼盘特色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFeature()
        {
            PremisesFeatureBll _featureBll = new PremisesFeatureBll();
            List<PremisesFeature> feature = _featureBll.GetList();
            string str = "{";
            string id = "\"value\":[\"\",";
            string name = "\"text\":[\"不限\",";
            for (int i = 0; i < feature.Count; i++)
            {
                id += "\"" + feature[i].Id + "\",";
                name += "\"" + feature[i].Name + "\",";
            }
            str += id.TrimEnd(',') + "]" + "," + name.TrimEnd(',') + "]" + "}";
            return Content(str);
        }

        #endregion

        #region 区域 商圈 地铁 站点

        /// <summary>
        ///  区域 商圈 地铁 站点
        /// </summary>
        /// <param name="type"></param>
        /// <param name="city"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBaseData(int type, string city, string pid)
        {
            string url = TXCommons.GetConfig.SearchUrl + (type == 0 ? "Area/GetAreaIndex.ashx" : "Traffic/GetTrafficIndex.ashx");
            string param = "CityPY=" + city + (pid == "0" ? "" : "&" + (type == 0 ? "DPY" : "PId") + "=" + pid);
            //string returnStr=TXCommons.WebClient.AsyncRequest.Send("POST", url,param );
            string returnStr = HttpWebRequest("POST", url, param);
            return Content(returnStr);
        }

        /// <summary>
        ///  HttpWebRequest 
        /// </summary>
        /// <param name="RequestMethod">GET | POST</param>
        /// <param name="url">URL</param>
        /// <param name="Param">参数</param>
        /// <returns></returns>
        public static string HttpWebRequest(string RequestMethod, string url, string Param)
        {
            string result;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                byte[] byteRequest = Encoding.ASCII.GetBytes(Param);
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                ServicePointManager.Expect100Continue = false;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.Timeout = 10000;
                httpWebRequest.Method = RequestMethod;
                using (Stream st = httpWebRequest.GetRequestStream())
                {
                    st.Write(byteRequest, 0, byteRequest.Length);
                }
                using (Stream responseStream = httpWebRequest.GetResponse().GetResponseStream())
                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            return result;
        }

        #endregion

        #region 跟据楼盘ID取楼栋

        /// <summary>
        /// 跟据楼盘ID取楼栋,厅室
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStaticData(string id, string type)
        {
            List<StaticData> data = null;
            StaticDataBll _staticData = new StaticDataBll();
            switch (type)
            {
                case "d":
                    data = _staticData.GetBuildingByPremisesId(Util.ToInt(id));
                    break;
                case "r":
                    data = _staticData.GetUnitByPremisesId(Util.ToInt(id));
                    break;
            }
            return Content(ToJSon.ObjectToJson<StaticData>("baseData", data));
        }

        #endregion

        #region 取最近浏览
        /// <summary>
        /// 取最近浏览
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RecentlyViewed()
        {
            PremisesBll _premisesBll = new PremisesBll();
            string cookie = CookieHelper.GetCookie("recentlyviewed");
            if (!string.IsNullOrEmpty(cookie))
            {
                //List<RecentlyViewed> premis = _premisesBll.GetPremisesbyIds(cookie);
                string pre = "[";
                var str = cookie.Split(',');
                for (var i = 0; i < str.Length; i++)
                {
                    var o = str[i].Split('$');
                    pre += "{\"Id\":\"" + o[0] + "\",\"Name\":\"" + o[1] + "\",\"DName\":\"" + o[2] + "\",\"ReferencePrice\":\"" + o[3] + "\",\"Url\":\"" + o[4] + "\"}";
                    if (i < str.Length - 1)
                        pre += ",";
                }
                //if (premis != null)
                //{
                //pre = ToJSon.ObjectToJson<RecentlyViewed>("premis", premis);
                //}
                pre += "]";
                return pre;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 存最近浏览
        /// <summary>
        /// 存最近浏览
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void SetRecentlyViewed(string id)
        {
            string cookie = CookieHelper.GetCookie("recentlyviewed");
            PremisesBll _premisesBll = new PremisesBll();
            TXOrm.Premis premis = _premisesBll.GetPremises_ById(Util.ToInt(id));
            string url = Request.UrlReferrer.ToString();
            string pre = premis.Id + "$" + premis.Name + "$" + premis.DName + "$" + premis.ReferencePrice + "$" + url;
            if (cookie == null || cookie == "")
            {
                CookieHelper.SetCookie("recentlyviewed", pre);
            }
            else
            {
                string premisesIds = pre + ",";
                var val = cookie.Split(',');
                var tem = 0;
                for (int i = 0; i < val.Length; i++)
                {
                    if (val[i].Split('$')[0].ToString() != id)
                    {
                        premisesIds += val[i] + ",";
                        tem++;
                    }
                    if (tem > 3)
                    {
                        break;
                    }
                }
                //premisesIds += pre;
                CookieHelper.SetCookie("recentlyviewed", premisesIds.TrimEnd(','));
            }
        }
        #endregion

        #region 获取活动房源上次出价

        /// <summary>
        /// 获取活动房源上次出价
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public string GetLastBidPrice(int activityId, int houseId)
        {
            decimal price = 0;
            PremisesBll bll = new PremisesBll();
            var bidPrics = bll.GetLastBidPrice(activityId, houseId);
            if (bidPrics != null)
            {
                price = bidPrics.BidUserPrice;
            }

            return ((double)price).ToString();
        }
        #endregion
    }
}
