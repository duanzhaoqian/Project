using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Caching;
using TXCommons;
using TXCommons.bd.cjkjb.webservice;
using TXCommons.user.cjkjb.webservice;
using ServiceStack;
using TXCommons.Xml;

namespace TXNewHouseWeb.Controllers
{
    public class LoginInfo
    {
        public bool IsLogin { get; set; }
        public UserInfo UserInfo { get; set; }
    }

    public class BaseController : Controller
    {
        private static BaseDataWebService bdwService = new BaseDataWebService();
        public OperaUserService _ous = new OperaUserService();
        public int CityId
        {
            get
            {
                string url = Request.Url.ToString().ToLower();
                string cityPinyin = url.Replace("http://", "").Split('.')[0];
                int _id = 253;
                switch (cityPinyin)
                {
                    case "beijing": _id = 253; break;
                    case "shanghai": _id = 239; break;
                    case "guangzhou": _id = 344; break;
                    case "shenzhen": _id = 355; break;
                    case "tianjin": _id = 205; break;
                    case "hangzhou": _id = 307; break;
                    case "nanjing": _id = 243; break;
                    case "chengdu": _id = 155; break;
                    case "dalian": _id = 296; break;
                    case "qingdao": _id = 263; break;
                    case "anyang": _id = 88; break;
                    case "beihai": _id = 328; break;
                    case "baotou": _id = 143; break;
                    case "baoding": _id = 378; break;
                    case "bengbu": _id = 189; break;
                    case "binzhou": _id = 254; break;
                    case "chongqing": _id = 403; break;
                    case "changchun": _id = 232; break;
                    case "changsha": _id = 174; break;
                    case "changzhou": _id = 240; break;
                    case "dongguan": _id = 342; break;
                    case "datong": _id = 368; break;
                    case "dezhou": _id = 255; break;
                    case "changshu": _id = 413; break;
                    case "fuzhou": _id = 318; break;
                    case "foshan": _id = 343; break;
                    case "guiyang": _id = 107; break;
                    case "guilin": _id = 332; break;
                    case "haerbin": _id = 119; break;
                    //case "hangzhou": _id = 307; break;
                    case "huhehaote": _id = 146; break;
                    case "hefei": _id = 195; break;
                    case "haikou": _id = 114; break;
                    case "huizhou": _id = 346; break;
                    case "huzhou": _id = 308; break;
                    case "jinan": _id = 257; break;
                    case "jiaxing": _id = 309; break;
                    case "jinhua": _id = 310; break;
                    case "jiangmen": _id = 347; break;
                    case "jilinshi": _id = 233; break;
                    case "jining": _id = 258; break;
                    case "hengyang": _id = 177; break;
                    case "kunming": _id = 78; break;
                    case "kunshan": _id = 414; break;
                    case "lanzhou": _id = 395; break;
                    case "luoyang": _id = 92; break;
                    case "liuzhou": _id = 336; break;
                    case "lijiang": _id = 79; break;
                    case "mianyang": _id = 165; break;
                    case "linyi": _id = 261; break;
                    //case "nanjing": _id = 261; break;
                    case "ningbo": _id = 312; break;
                    case "nanchang": _id = 135; break;
                    case "nanning": _id = 337; break;
                    case "nantong": _id = 244; break;
                    case "qinhuangdao": _id = 384; break;
                    case "quanzhou": _id = 323; break;
                    case "qingyuan": _id = 351; break;
                    case "shenyang": _id = 304; break;
                    //case "shanghai": _id = 239; break;
                    //case "shenzhen": _id = 355; break;
                    case "shijiazhuang": _id = 385; break;
                    case "suzhou": _id = 245; break;
                    case "shantou": _id = 352; break;
                    case "sanya": _id = 115; break;
                    case "shaoxing": _id = 314; break;
                    case "taiyuan": _id = 374; break;
                    case "tangshan": _id = 386; break;
                    case "taian": _id = 265; break;
                    case "taizhou": _id = 247; break;
                    case "wuhan": _id = 224; break;
                    case "wuxi": _id = 248; break;
                    case "wulumuqi": _id = 284; break;
                    case "weihai": _id = 266; break;
                    case "wenzhou": _id = 316; break;
                    case "weifang": _id = 267; break;
                    case "wuhu": _id = 203; break;
                    case "xian": _id = 212; break;
                    case "xiamen": _id = 325; break;
                    case "xining": _id = 410; break;
                    case "xvzhou": _id = 249; break;
                    case "xianyang": _id = 213; break;
                    case "xiangtan": _id = 181; break;
                    case "xiangyang": _id = 416; break;
                    case "xinxiang": _id = 99; break;
                    case "yinchuan": _id = 365; break;
                    case "yantai": _id = 268; break;
                    case "yangzhou": _id = 251; break;
                    case "yichang": _id = 229; break;
                    case "yangjiang": _id = 356; break;
                    case "zhengzhou": _id = 102; break;
                    case "zhuhai": _id = 361; break;
                    case "zibo": _id = 270; break;
                    case "zhenjiang": _id = 252; break;
                    case "zhongshan": _id = 360; break;
                    case "zhaoqing": _id = 359; break;
                    case "zhangjiakou": _id = 388; break;
                    default: _id = 253; break;
                }
                return _id;
            }
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //城市切换
            ChangeCity();
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            //if (this.IsLogin)
            //{
            //    ViewData["user"] = this.GetUserInfo;
            //    ViewData["NoReadCount"] = _ous.GetSystemMessageCount("binPath", "classPath", this.GetUserInfo.Id, false, "FC");
            //}
            return base.View(viewName, masterName, model);
        }

        /// <summary>
        /// 切换城市
        /// </summary>
        private void ChangeCity()
        {
            string url = Request.Url.ToString();
            string cityPinyin = url.Replace("http://", "").Split('.')[0].ToLower();
            StringBuilder cookieName = new StringBuilder();
            cookieName.Append("search_cityDataCookie_");
            cookieName.Append(cityPinyin);
            List<string> cityData = new List<string>();
            HttpCookie cityDataCookies = this.Request.Cookies[cookieName.ToString()];

            //发布时需要按发布的域名来修改代码
            if ((cityPinyin == "nhwww" || cityPinyin == "www") && cityDataCookies == null)
            {
                ViewData["cityId"] = "253";
                ViewData["cityName"] = "北京";
                ViewData["cityPinyin"] = "beijing";
                cityData.Add("253");
                cityData.Add("北京");
                cityData.Add("beijing");
                //存入Cookie
                this.SetCityDataCookie(cookieName, cityData);
            }
            else
            {
                if (cityDataCookies == null)
                {
                    bool temp = false;
                    var area = bdwService.GetCityById(CityId);
                    if (area != null)
                    {
                        temp = true;
                        cityData.Add(CityId.ToString());
                        cityData.Add(area.Name == null ? "" : area.Name.TrimEnd('市'));
                        cityData.Add(cityPinyin);
                    }
                    //foreach (var item in this.GetAllCityData())
                    //{
                    //    if (item.Spelling.Contains(cityPinyin))
                    //    {
                    //        temp = true;
                    //        cityData.Add(item.Id.ToString());
                    //        if (item.Name.Contains("市"))
                    //        {
                    //            item.Name = item.Name.TrimEnd('市');
                    //        }
                    //        cityData.Add(item.Name);
                    //        cityData.Add(cityPinyin);
                    //        break;
                    //    }
                    //}
                    if (temp == false)
                    {
                        cityData.Add("253");
                        cityData.Add("北京");
                        cityData.Add("beijing");
                    }
                    ViewData["cityId"] = cityData[0];
                    ViewData["cityName"] = cityData[1];
                    ViewData["cityPinyin"] = cityData[2];
                    //存入Cookie
                    this.SetCityDataCookie(cookieName, cityData);
                }
                else
                {
                    HttpCookie cookies = this.Request.Cookies[cookieName.ToString()];
                    ViewData["cityId"] = "253";
                    ViewData["cityName"] = "北京";
                    ViewData["cityPinyin"] = "beijing";
                    ViewData["cityId"] = cookies == null ? "253" : TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Decrypt(cookies.Values["search_cityId"]);
                    ViewData["cityName"] = cookies == null ? "北京" : TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Decrypt(cookies.Values["search_cityName"]);
                    ViewData["cityPinyin"] = cookies == null ? "beijing" : TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Decrypt(cookies.Values["search_cityPinyin"]);
                }
            }
            //切换城市设置，只限：北京和上海
            string cpy = ViewData["cityPinyin"] as string;
            //ViewData["cpy"] = string.IsNullOrWhiteSpace(cpy) ? "beijing" : cpy == "beijing" ? "shanghai" : "beijing";
            ViewData["cpy"] = string.IsNullOrWhiteSpace(cpy) ? "beijing" : cpy;
            //ViewData["selectText"] = string.IsNullOrWhiteSpace(cpy) ? "切换至北京" : cpy == "beijing" ? "切换至上海" : "切换至北京";
        }

        /// <summary>
        /// 设置城市信息Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cityData"></param>
        private void SetCityDataCookie(StringBuilder cookieName, List<string> cityData)
        {
            //存入cookie
            HttpCookie cityDataCookies = new HttpCookie(cookieName.ToString());
            cityDataCookies.Domain = GetConfig.Domain;
            cityDataCookies.Values.Add("search_cityId", TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Encrypt(cityData[0]));
            cityDataCookies.Values.Add("search_cityName", TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Encrypt(cityData[1]));
            cityDataCookies.Values.Add("search_cityPinyin", TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Encrypt(cityData[2]));
            cityDataCookies.Expires = DateTime.Now.AddDays(7);
            this.Response.Cookies.Add(cityDataCookies);
        }

        /// <summary>
        /// 得到所有城市信息（城市Id、城市名称、城市全拼等）
        /// </summary>
        /// <returns></returns>
        private dynamic GetAllCityData()
        {
            Cache allCityCache = System.Web.HttpContext.Current.Cache;
            string key = "AllCityData";
            dynamic data = null;
            if (allCityCache[key] != null)
            {
                data = allCityCache[key];
            }
            else
            {
                BaseDataWebService bdwService = new BaseDataWebService();
                data = bdwService.GetOpenCityList();
                allCityCache.Add(key, data, null, DateTime.Now.AddDays(7), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            return data;
        }

        #region 登陆相关

        /// <summary>
        /// 是否登陆
        /// author：sunlin
        /// </summary>
        public bool IsLogin
        {
            get
            {
                HttpCookie privateAuthCookie = Request.Cookies[TXCommons.GetConfig.PrivateCookie];

                if (privateAuthCookie != null)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 登陆用户信息
        /// author：sunlin
        /// </summary>
        public UserInfo GetUserInfo
        {
            get
            {
                if (IsLogin)
                {
                    #region  逻辑处理

                    string userId = "", guid = "";

                    var userXml = "";

                    if (Request.Cookies[TXCommons.GetConfig.PrivateCookie] != null)
                    {
                        string data = Request.Cookies[TXCommons.GetConfig.PrivateCookie].Value;

                        string[] arr = TXCommons.ForKuaiYouJiaCookie.BaseRC4.RC4Decrypt(data).Split(';');

                        userId = arr[0];

                        guid = arr[1];

                        #region 获取用户信息
                        int cityId = Util.ToInt(ViewData["cityId"]);
                        try
                        {
                            userXml = Redis.GetAllItemsFromList<string>(guid, FunctionTypeEnum.UserInfo, cityId);
                            if (string.IsNullOrEmpty(userXml))
                            {
                                userXml = _ous.GetUserById("binPath", "classPath", int.Parse(userId), "FC");

                                Redis.AddItemToList<string>(userXml, guid, DateTime.Now.AddHours(1), FunctionTypeEnum.UserInfo, cityId);
                            }
                            // 反序列化
                            return ComObjOrXml.Deserialize(typeof(UserInfo), userXml) as UserInfo;
                        }
                        catch (Exception ex)
                        {
                            //记录日志
                            //Log4netService.RecordLog.RecordException("", string.Format("(userId:{0},guid:{1}", userId, guid), ex);
                        }

                        #endregion

                        return TXCommons.Xml.ComObjOrXml.Deserialize(typeof(UserInfo), userXml) as UserInfo;
                    }
                    else
                    {
                        return null;
                    }

                    #endregion
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

    }


}