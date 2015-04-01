using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Webdiyer.WebControls.Mvc;
using TXBll.NHouseSearch;
using TXCommons.Index;
using TXCommons;
using System.Linq;
using TXNewHouseWeb.Common;
using TXCommons.SEO;
using ServiceStack;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using TXModel.AD;

namespace TXNewHouseWeb.Controllers
{
    public class SearchController : BaseController
    {
        #region 搜索列表页

        /// <summary>
        /// 搜索列表页
        /// author：sunlin
        /// </summary>
        /// <returns></returns>
        public ActionResult PremisesList(string Id)
        {

            #region SEO处理
            //string city = Request.Url.Host.Split('.')[0];
            //if (city == "nhwww" || city == "www")
            //    city = "beijing";
            string city = ViewData["cityPinyin"] as string;
            Id = Id.Replace("cs", city); //城市+url
            SEO seo = new SEO();
            IndexPremisesConditionInfo ipci = SeoCommon.GetSearchCondiction(Id);
            ViewData["Seo"] = seo.SeoSearchList(new SEOModel(), ViewData["cityName"] as string, ViewData["cityPinyin"] as string, ipci);
            #endregion

            #region 搜索数据处理

            bool iscurrentpage = Regex.IsMatch(Id, @"-i\d{1,}");//是否是页格式
            var url = Request.RawUrl.Substring(1);//url
            var key = Regex.Match(url, @"-w_\w+").ToString();//关键字
            if (!iscurrentpage) if (key != "") { url = url.Replace(key, "") + "-i1" + key; } else { url = url + "-i1"; }//把key加到url最后
            string current = Regex.Match(url, @"-i\d{1,}").Value;//当前页
            var _p = Regex.Match(Id, @"-p\d{1,}").ToString();
            if (!string.IsNullOrEmpty(_p))
                Id = Id.Replace(_p, "-" + PremisesType.GetSearchPrice(_p));//重新拼写价格参数
            //var _m = Regex.Match(Id, @"-m\d{1,}").ToString();
            //if (!string.IsNullOrEmpty(_m))
            //    Id = Id.Replace(_m, "-" + PremisesType.GetSearchArea(_m));//重新拼写面积参数
            string cityId = Util.ToStringTrim(CityId);// ViewData["cityId"] as string;//取城市
            if (string.IsNullOrWhiteSpace(cityId))
            {
                cityId = "253";
            }
            PagedList<IndexPremises> searchList = null;
            #endregion

            try
            {
                //Id:条件，地址，城市
                searchList = SearchBll.NHouseListResult(Id.Replace("я", "·").Replace("ю", "•"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<IndexHouse> indexHouses = new List<IndexHouse>();
                for (int i = 0; i < searchList.Count; i++)
                {
                    if (searchList[i].Houses != "")
                    {
                        var indexHouse = searchList[i].Houses;
                        indexHouses = SetHits(ToJSon.JsonToObject<List<IndexHouse>>(indexHouse));
                        searchList[i].Houses = ser.Serialize(indexHouses);
                    }
                }

            }
            catch (Exception) { }
            ViewData["adv"] = AdvXmlRead(cityId);
            //点击量
            //ClickCount(searchList);
            //searchList.CurrentPageIndex = Int16.Parse(Regex.Match(current, @"\d{1,}").Value);
            ViewData["conPage"] = url;
            ViewData["pxclass"] = Regex.Match(Regex.Match(Id, @"-y\d+").ToString(), @"\d").ToString();
            //ViewData["UserId"] = GetUserInfo == null ? 0 : GetUserInfo.Id;
            ViewData["UserId"] = GetUserInfo == null ? 0 : GetUserInfo.Id;
            return View(searchList);
        }


        /// <summary>
        /// 设置点击量
        /// </summary>
        /// <param name="sizeChartList"></param>
        private List<IndexHouse> SetHits(List<IndexHouse> indexHouse)
        {
            int b = indexHouse.Count % 20;//10
            int a = indexHouse.Count / 20;//0
            if (indexHouse != null && indexHouse.Count > 0)
            {
                List<string> count = null;
                //大于20条
                if (indexHouse.Count > 20)
                {
                    #region 活动房源赋值
                    for (int i = 0; i < a + 1; i++)//循环几次
                    {
                        var keys = "";
                        //循环Keys
                        for (int j = i * 20; j < ((indexHouse.Count > ((i * 20) + 20)) ? ((i * 20) + 20) : (i * 20) + b); j++)
                        {
                            keys += "h_" + indexHouse[j].HouseID + ",";
                        }
                        //取点击量集合
                        count = TXCommons.Redis.GetValues<List<string>>(FunctionTypeEnum.NewHouseViewCount, Util.ToInt(ViewData["cityId"]), keys.TrimEnd(','));
                        if (count != null)
                        {
                            int m = 0;
                            for (int k = i * 20; k < ((indexHouse.Count > ((i * 20) + 20)) ? ((i * 20) + 20) : (i * 20) + b); k++)
                            {
                                m++;
                                if (count[m].ToString() != "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1#2B#B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee#2B##2B#999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9#2B#fB8/Ij76N3/lwe79j/6fAAAA//#2B#qiwZXBwAAAA==")
                                {
                                    indexHouse[k].BrowseCount = count[m] == "" ? 0 : Util.ToInt(count[m]);
                                }
                                else
                                {
                                    TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseViewCount, Util.ToInt(ViewData["cityId"]), "h_" + indexHouse[k].HouseID).ToString();
                                    indexHouse[k].BrowseCount = 1;
                                }
                            }
                        }
                        else
                        {
                            //循环Keys
                            for (int j = i * 20; j < ((indexHouse.Count > ((i * 20) + 20)) ? ((i * 20) + 20) : (i * 20) + b); j++)
                            {
                                for (int n = 0; n < indexHouse.Count; n++)
                                {
                                    indexHouse[n].BrowseCount = 0;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {//小于等于20条
                    #region 取接口点击量
                    var keys = "";
                    for (int i = 0; i < indexHouse.Count; i++)
                    {
                        for (int n = 0; n < indexHouse.Count; n++)//拼key
                        {
                            keys += "h_" + indexHouse[n].HouseID + ",";
                        }
                    }
                    //取点击量集合
                    count = TXCommons.Redis.GetValues<List<string>>(FunctionTypeEnum.NewHouseViewCount, Util.ToInt(ViewData["cityId"]), keys.TrimEnd(','));
                    #endregion
                    if (count != null)//点击量集合存在
                    {
                        #region 活动房源赋值
                        for (int n = 0; n < indexHouse.Count; n++)//循环活动房源
                        {
                            if (count[n + 1].ToString() != "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1#2B#B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee#2B##2B#999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9#2B#fB8/Ij76N3/lwe79j/6fAAAA//#2B#qiwZXBwAAAA==")
                            {
                                indexHouse[n].BrowseCount = count[n + 1] == "" ? 0 : Util.ToInt(count[n + 1]);
                            }
                            else
                            {
                                TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseViewCount, Util.ToInt(ViewData["cityId"]), "h_" + indexHouse[n].HouseID).ToString();
                                indexHouse[n].BrowseCount = 1;
                            }
                        }
                        #endregion
                    }
                    else//点击量不存在
                    {
                        #region 活动房源赋值

                        for (int n = 0; n < indexHouse.Count; n++)
                        {
                            TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseViewCount, Util.ToInt(ViewData["cityId"]), "h_" + indexHouse[n].HouseID).ToString();
                            indexHouse[n].BrowseCount = 1;
                        }

                        #endregion
                    }
                }

            }
            return indexHouse;
        }


        public List<Advs> AdvXmlRead(string cityId)
        {

            List<Advs> advList = new List<Advs>();
            //将XML文件的内容读入XmlDocument对象XmlDoc中
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(Server.MapPath("~/AdvConfig.xml"));
            //获取<data>节点的所有子节点
            XmlNodeList nodeList = XmlDoc.SelectSingleNode("/Advs").ChildNodes;
            foreach (XmlNode xnode in nodeList)//遍历所有子节点 
            {
                var a = xnode.Attributes["id"].InnerText;
                if (a == cityId)
                {
                    foreach (XmlNode xn in xnode.ChildNodes)
                    {
                        Advs adv = new Advs();
                        adv.Pos = xn.Attributes["pos"].InnerText;
                        adv.Type = xn.Attributes["type"].InnerText;
                        adv.Linkurl = xn.Attributes["linkurl"].InnerText;
                        adv.Disurl = xn.Attributes["disurl"].InnerText;
                        adv.Alt = xn.Attributes["alt"].InnerText;
                        advList.Add(adv);
                    }
                }
            }
            return advList;
        }

        #endregion

        #region 同区热门楼盘列表

        /// <summary>
        /// 同区热门楼盘列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="d"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ActionResult HotPremisesList(string conditions, string d, int cityId)
        {
            SearchBll _searchBll = new SearchBll();
            // 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
            //List<IndexRanking> list = _searchBll.PremisesList("1", conditions, "", CityId);
            List<IndexRecList> list = _searchBll.PremisesList(0, FunctionTypeEnum.NewHousePropertyRank, CityId);
            ////===========
            ////获取点击次数
            //if (list != null)
            //{
            //    list.ForEach(p =>
            //    {
            //        p.ClickRate = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", p.PremisesID), FunctionTypeEnum.NewHouseViewCount, Util.ToInt(p.CityID)).ToString();
            //        //p.PremisesName = Util.getStrLenB(p.PremisesName, 0, 24);
            //    });
            //    list = list.OrderByDescending(p => Util.ToInt(p.ClickRate)).ToList();
            //}
            //===========
            return PartialView("_HotPremisesList", list);
        }

        /// <summary>
        /// 同区最新楼盘列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="d"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ActionResult NewPremisesList(string conditions, string d, int cityId)
        {
            SearchBll _searchBll = new SearchBll();
            List<IndexRecList> list = _searchBll.PremisesList(1, FunctionTypeEnum.NewHousePropertyRank, CityId);
            // 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
            //List<IndexRanking> list = _searchBll.PremisesList("4", conditions, "", CityId);
            ////===========
            ////获取点击次数
            //if (list != null)
            //{
            //    list.ForEach(p =>
            //    {
            //        p.ClickRate = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", p.PremisesID), FunctionTypeEnum.NewHouseViewCount, Util.ToInt(p.CityID)).ToString();
            //        //p.PremisesName = Util.getStrLenB(p.PremisesName, 0, 24);
            //    });
            //}
            //===========
            return PartialView("_NewPremisesList", list);
        }


        #endregion

        /// <summary>
        /// 添加用户收藏
        /// </summary>
        /// <param name="premisesid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string AddUserFavorite(string premisesid, string userid)
        {
            string info = string.Empty;
            NHWebFavorite favorite = new NHWebFavorite();

            int iuserid = 0;
            int.TryParse(userid, out iuserid);
            if (iuserid > 0)
            {

                int ipremisesid = 0;
                int.TryParse(premisesid, out ipremisesid);
                try
                {
                    int countfavorite = favorite.IsCountFavorite(iuserid, ipremisesid, "400");
                    if (countfavorite > 0)
                    {
                        info = "不能重复收藏！";
                    }
                    else
                    {
                        //新房楼盘  400
                        if (favorite.AddUserFavourite(iuserid, "400", premisesid, "新房前台楼盘收藏", "", ""))
                        {
                            info = "收藏成功！";
                        }
                        else
                        {
                            info = "收藏失败，请稍后再试！";
                        }
                    }
                }
                catch (Exception ex)
                {
                    info = "收藏失败，请稍后再试！";
                }
            }
            else
            {

                info = "请先登录！";
            }


            return info;
        }
    }
}
