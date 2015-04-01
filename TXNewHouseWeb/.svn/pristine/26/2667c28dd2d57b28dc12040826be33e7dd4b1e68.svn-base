using System;
using System.Data;
using System.Configuration;

namespace TXCommons
{
    public class FilterHelper
    {
        /// <summary>
        /// 过滤敏感字
        /// </summary>
        /// <param name="str"></param>
        /// <returns>含有敏感字返回false,否则true,完全匹配</returns>
        public static bool FilterBadAll(string str, ref string badkey)
        {
            string[] keywords = null;
            object obj = System.Web.HttpContext.Current.Cache.Get("keywords_bad_all");
            if (obj != null)
            {
                keywords = (string[])obj;
            }
            else
            {
                string xmlurl = ConfigurationManager.AppSettings["KeywordsPath"].ToString(); //ConfigHelper.GetConfigString("KeywordsPath");
                DataSet ds = new DataSet();
                ds.ReadXml(xmlurl);
                DataTable dt = ds.Tables["keyword"];
                keywords = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    keywords[i] = dt.Rows[i]["Keyword_Text"].ToString();
                }
                System.Web.HttpContext.Current.Cache.Insert("keywords_bad_all", keywords, null, DateTime.Now.AddHours(24), System.TimeSpan.Zero);
            }

            foreach (string key in keywords)
            {
                if (str.Trim() == key)
                {
                    badkey = key;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 过滤敏感字
        /// </summary>
        /// <param name="str"></param>
        /// <returns>含有敏感字返回false,否则true</returns>
        public static bool FilterBad(string str, ref string badkey)
        {
            string[] keywords = null;
            object obj = System.Web.HttpContext.Current.Cache.Get("keywords_bad_all");
            if (obj != null)
            {
                keywords = (string[])obj;
            }
            else
            {
                string xmlurl = ConfigurationManager.AppSettings["KeywordsPath"].ToString(); //ConfigHelper.GetConfigString("KeywordsPath");
                DataSet ds = new DataSet();
                ds.ReadXml(xmlurl);
                DataTable dt = ds.Tables["keyword"];
                keywords = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    keywords[i] = dt.Rows[i]["Keyword_Text"].ToString();
                }
                System.Web.HttpContext.Current.Cache.Insert("keywords_bad_all", keywords, null, DateTime.Now.AddHours(24), System.TimeSpan.Zero);
            }

            foreach (string key in keywords)
            {
                badkey = key;
                if (str.IndexOf(key) >= 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///  接受Request字符串
        /// </summary>
        /// <param name="keystr"></param>
        /// <returns></returns>
        public static string GetRequestStr(string keystr)
        {
            return GetRequestStr(keystr, false, true);
        }
        /// <summary>
        ///  接受Request字符串(JSON 不过滤双引号)
        /// </summary>
        /// <param name="keystr"></param>
        /// <returns></returns>
        public static string GetRequestStrForJson(string keystr)
        {
            return GetRequestStrForJson(keystr, false, true);
        }
        /// <summary>
        /// 接受Request字符串并截取相应长度
        /// </summary>
        /// <param name="keystr">Request字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string GetRequestStr(string keystr, int length)
        {
            string returnstr = GetRequestStr(keystr);
            if (returnstr.Length > length)
                returnstr = returnstr.Substring(0, length);
            return returnstr;
        }
        public static long GetRequestStrToLong(string keystr)
        {
            long Value = 0;
            long.TryParse(GetRequestStr(keystr), out Value);
            return Value;
        }
        public static int GetRequestStrToInt(string keystr)
        {
            int Value = 0;
            int.TryParse(GetRequestStr(keystr), out Value);
            return Value;
        }
        public static decimal GetRequestStrToDecimal(string keystr)
        {
            decimal Value = 0;
            decimal.TryParse(GetRequestStr(keystr), out Value);
            return Value;
        }
        public static DateTime GetRequestStrToDateTime(string keystr)
        {
            DateTime date = DateTime.Now;
            DateTime.TryParse(GetRequestStr(keystr), out date);
            return date;
        }
        /// <summary>
        /// 接受Request字符串
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="isall"></param>
        /// <param name="iskeyword"></param>
        /// <returns></returns>
        public static string GetRequestStr(string keystr, bool isall, bool iskeyword)
        {
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            if (Request.Form[keystr] == null && Request.QueryString[keystr] == null)
            {
                return "";
            }
            string returnstr = "";
            if (Request.Form[keystr] != null)
            {
                if (Request.Form[keystr].ToString().Trim().Length > 0)
                {
                    returnstr = ReplaceChars(Request.Form[keystr].ToString().Trim(), isall, iskeyword);
                }
            }
            else if (Request.QueryString[keystr] != null)
            {
                if (Request.QueryString[keystr].ToString().Trim().Length > 0)
                {
                    returnstr = ReplaceChars(Request.QueryString[keystr].ToString().Trim(), isall, iskeyword);
                }
            }
            return returnstr;
        }

        /// <summary>
        /// 接受Request字符串(JSON 不过滤双引号)
        /// </summary>
        /// <param name="keystr"></param>
        /// <param name="isall"></param>
        /// <param name="iskeyword"></param>
        /// <returns></returns>
        public static string GetRequestStrForJson(string keystr, bool isall, bool iskeyword)
        {
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            if (Request.Form[keystr] == null && Request.QueryString[keystr] == null)
            {
                return "";
            }
            string returnstr = "";
            if (Request.Form[keystr] != null)
            {
                if (Request.Form[keystr].ToString().Trim().Length > 0)
                {
                    returnstr = ReplaceCharsForJson(Request.Form[keystr].ToString().Trim(), isall, iskeyword);
                }
            }
            else if (Request.QueryString[keystr] != null)
            {
                if (Request.QueryString[keystr].ToString().Trim().Length > 0)
                {
                    returnstr = ReplaceCharsForJson(Request.QueryString[keystr].ToString().Trim(), isall, iskeyword);
                }
            }
            return returnstr;
        }

        ///// <summary>
        ///// 检查是否含有关键字
        ///// 页面调用的时候，把所有要判断的字符串加到一起，统一调用一次本方法
        ///// </summary>
        ///// <param name="str">要检查的内容</param>
        ///// <param name="pKey">含有的关键字</param>
        ///// <returns></returns>
        //public static bool ContainKeywords(string str, ref string pKey)
        //{
        //    return ContainKeywords(str, false, ref pKey);
        //}

        ///// <summary>
        ///// 检查是否含有关键字
        ///// 页面调用的时候，把所有要判断的字符串加到一起，统一调用一次本方法
        ///// </summary>
        ///// <param name="str">要检查的内容</param>
        ///// <param name="isExactMatch">是否完全匹配</param>
        ///// <param name="pKey">含有的关键字</param>
        ///// <returns></returns>
        //public static bool ContainKeywords(string str, bool isExactMatch, ref string pKey)
        //{
        //    bool hasKeywords = false;
        //    string filterlKeywordsType = getConfigData("FilterlKeywordsType");
        //    if (!string.IsNullOrEmpty(filterlKeywordsType) && filterlKeywordsType == "1")
        //    {
        //        hasKeywords = KeywordsHelper.CheckKeywords(str, ref pKey);
        //    }
        //    else
        //    {
        //        hasKeywords = FilterKeywordsByXml(str, ref pKey);

        //    }
        //    if (hasKeywords && isExactMatch)
        //    {
        //        hasKeywords = str == pKey;
        //    }
        //    return hasKeywords;
        //}

        /// <summary>
        /// 通过xml文件过滤敏感词
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pKey"></param>
        /// <returns></returns>
        private static string FilterKeywordsByXml(string str, bool isexactmatch, ref string pKey)
        {
            string[] keywords = null;

            #region 获取keywords列表
            object obj = System.Web.HttpContext.Current.Cache.Get("keywords_bad_all");
            if (obj != null)
            {
                keywords = (string[])obj;
            }
            else
            {
                string xmlurl = ConfigurationManager.AppSettings["KeywordsPath"].ToString(); //ConfigHelper.GetConfigString("KeywordsPath");
                DataSet ds = new DataSet();
                ds.ReadXml(xmlurl);
                DataTable dt = ds.Tables["Keyword"];
                keywords = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    keywords[i] = dt.Rows[i]["Keyword_Text"].ToString();
                }
                System.Web.HttpContext.Current.Cache.Insert("keywords_bad_all", keywords, null, DateTime.Now.AddHours(24), System.TimeSpan.Zero);
            }
            #endregion

            foreach (string key in keywords)
            {
                if (isexactmatch)
                {
                    if (str == key)
                    {
                        str = "****";
                        pKey = key;
                    }
                }
                else
                {
                    if (str.Contains(key))
                    {
                        str = str.Replace(key, "****");
                        pKey += key + ",";
                    }
                }
            }
            return str;
        }


        /// <summary>
        /// 检查是否包含特殊字符
        /// </summary>
        /// <param name="oraString"></param>
        /// <returns></returns>4
        public static string ReplaceChars(string oraString)
        {
            return ReplaceChars(oraString, false, true);
        }

        /// <summary>
        /// 检查是否包含特殊字符
        /// </summary>
        /// <param name="oraString"></param>
        /// <param name="isAll">是否是完全匹配</param>
        /// <param name="iskeyword">是否使用关键字过滤</param>
        /// <returns></returns>4
        public static string ReplaceChars(string oraString, bool isAll, bool iskeyword)
        {
            if (oraString == null || oraString == "" || oraString.Trim() == "") return "";
            string[] sqlbadchar = { "script", "exec", "select", "update", "delete", "declare", "'", "#-$", "input", "\"", "<", ">" };
            string[] sqlnewchar = { "ｓｃｒｉｐｔ", "ｅｘｅｃ", "ｓｅｌｅｃｔ", "ｕｐｄａｔｅ", "ｄｅｌｅｔｅ", "ｄｅｃｌａｒｅ", "‘", "#－＄", "ｉｎｐｕｔ", "“", "＜", "＞" };
            for (int i = 0; i < sqlbadchar.Length; i++)
            {
                oraString = oraString.Replace(sqlbadchar[i], sqlnewchar[i]);
            }

            #region 关键字过虑
            //bool isNewKeyWord = false;
            //isNewKeyWord = bool.Equals(ConfigHelper.GetConfigString("IsStartNewKeywords"), "1");
            //if (!iskeyword)
            //{
            //    string isnorth = "N";
            //    if (ConfigHelper.GetConfigString("isnorth") != null)
            //    {
            //        isnorth = ConfigHelper.GetConfigString("isnorth");
            //    }
            //bool flag = true;
            //string badKey = string.Empty;
            //oraString = FilterKeywordsByXml(oraString, isAll, ref badKey);
            //if (isAll)
            //{
            //    flag = FilterBadAll(oraString, ref badKey);
            //}
            //else
            //{
            //    flag = FilterBad(oraString, ref badKey);
            //}
            //if (flag == false)
            //{
            //    oraString = oraString.Replace(badKey, string.Empty);
            //    //oraString = "";
            //}
            //}

            #endregion

            return oraString;
        }

        /// <summary>
        /// 检查是否包含特殊字符
        /// </summary>
        /// <param name="oraString"></param>
        /// <param name="isAll">是否是完全匹配</param>
        /// <param name="iskeyword">是否使用关键字过滤</param>
        /// <returns></returns>4
        public static string ReplaceScriptChars(string oraString)
        {
            if (oraString == null || oraString == "" || oraString.Trim() == "") return "";
            string[] sqlbadchar = { "script","javascript" };
            string[] sqlnewchar = { "ｓｃｒｉｐｔ","ｊａｖａｓｃｒｉｐｔ" };
            for (int i = 0; i < sqlbadchar.Length; i++)
            {
                oraString = oraString.Replace(sqlbadchar[i], sqlnewchar[i]);
            }
            return oraString;
        }

        /// <summary>
        /// 检查是否包含特殊字符(JSON 不过滤双引号)
        /// </summary>
        /// <param name="oraString"></param>
        /// <param name="isAll">是否是完全匹配</param>
        /// <param name="iskeyword">是否使用关键字过滤</param>
        /// <returns></returns>4
        public static string ReplaceCharsForJson(string oraString, bool isAll, bool iskeyword)
        {
            if (oraString == null || oraString == "" || oraString.Trim() == "") return "";
            string[] sqlbadchar = { "script", "exec", "select", "update", "delete", "declare", "'", "#-$", "input" };
            string[] sqlnewchar = { "ｓｃｒｉｐｔ", "ｅｘｅｃ", "ｓｅｌｅｃｔ", "ｕｐｄａｔｅ", "ｄｅｌｅｔｅ", "ｄｅｃｌａｒｅ", "‘", "#－＄", "ｉｎｐｕｔ" };
            for (int i = 0; i < sqlbadchar.Length; i++)
            {
                oraString = oraString.Replace(sqlbadchar[i], sqlnewchar[i]);
            }

            #region 关键字过虑
            //bool isNewKeyWord = false;
            //isNewKeyWord = bool.Equals(ConfigHelper.GetConfigString("IsStartNewKeywords"), "1");
            //if (!iskeyword)
            //{
            //    string isnorth = "N";
            //    if (ConfigHelper.GetConfigString("isnorth") != null)
            //    {
            //        isnorth = ConfigHelper.GetConfigString("isnorth");
            //    }
            //bool flag = true;
            //string badKey = string.Empty;
            //oraString = FilterKeywordsByXml(oraString, isAll, ref badKey);
            //if (isAll)
            //{
            //    flag = FilterBadAll(oraString, ref badKey);
            //}
            //else
            //{
            //    flag = FilterBad(oraString, ref badKey);
            //}
            //if (flag == false)
            //{
            //    oraString = oraString.Replace(badKey, string.Empty);
            //    //oraString = "";
            //}
            //}

            #endregion

            return oraString;
        }

        /// <summary>
        /// 过滤特殊符号
        /// </summary>
        /// <param name="oraString"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChar(string oraString)
        {
            if (oraString == null || oraString == "" || oraString.Trim() == "") return "";
            string[] spchar = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "，", "。", "、", "；", "：", "？", "…", "‘", "“", "《", "｛", "〖", "【", "】", "〗", "｝", "》", "”", "’", "＃", "～", "?", "※", "＋", "－", "×", "÷", "№", "＄", "￥", "§", "‰", "—", "§", "№", "☆", "★", "○", "●", "◎", "◇", "◆", "□", "℃", "‰", "€", "■", "△", "▲", "※", "→", "←", "↑", "↓", "〓", "¤", "°", "＃", "＆", "＠", "＼", "︿", "＿", "￣", "―", "♂", "♀", "￠", "￡", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", "！", "％", "…", "…", "＊" };
            foreach (string s in spchar)
            {
                oraString = oraString.Replace(s, "");
            }
            return oraString;
        }

        /// <summary>
        /// 修正xml格式
        /// </summary>
        /// <param name="old_value"></param>
        /// <returns></returns>
        public static string regular_xml_value(string old_value)
        {
            string new_value = old_value;
            new_value = new_value.Replace("<", "&lt;");
            new_value = new_value.Replace(">", "&gt;");
            new_value = new_value.Replace("&", "&amp;");
            return new_value;
        }

    }
}
