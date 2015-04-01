using System;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace TXCommons
{
    public class PubConstant
    {
        ///// <summary>
        ///// 获得CSS/JS路径【即将删除禁止使用】请GetConfig下的属性及方法
        ///// </summary>
        ///// <param name="fileName">文件名称</param>
        ///// <returns></returns>
        //public static string GetFileUrl(string fileName)
        //{
        //    StringBuilder str = new StringBuilder();
        //    // js/css文件根路径
        //    str.Append(GetConfigString("static.fileurl"));
        //    str.Append(fileName);
        //    str.Append("?v=");
        //    // js/css文件版本号
        //    str.Append(GetCurrentVersion());
        //    return str.ToString();
        //}
        ///// <summary>
        ///// 域名【即将删除禁止使用】请GetConfig下的属性及方法
        ///// </summary>
        //public static string GetBaseUrl()
        //{
        //    return GetConfigString("baseUrl");
        //}

        ///// <summary>
        ///// Domain【即将删除禁止使用】请GetConfig下的属性及方法
        ///// </summary>
        //public static string GetDomain()
        //{
        //    return GetConfigString("domain");
        //}

        ///// <summary>
        ///// 获得IMG路径【即将删除禁止使用】请GetConfig下的属性及方法
        ///// </summary>
        ///// <returns></returns>
        //public static string GetImgUrl()
        //{
        //    return GetConfigString("static.imgurl");
        //}
        ///// <summary>
        ///// 获取当前JS/CSS版本【即将删除禁止使用】请GetConfig下的属性及方法
        ///// </summary>
        ///// <returns></returns>
        //public static string GetCurrentVersion()
        //{
        //    return GetConfigString("static.version");
        //}

        /// <summary>
        /// 获取配置文件中配置的信息
        /// author:baochen 2013年8月15日13:55:33
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }


        #region 类型转换

        public static Int32 ToInt32(object str)
        {
            return ToInt32(str, 0);
        }
        public static Int32 ToInt32(object str, Int32 defValue)
        {
            Int32 outValue;

            if (str != null && !String.IsNullOrEmpty(str.ToString()))
            {
                if (Int32.TryParse(str.ToString(), out outValue))
                {
                    return outValue;
                }
            }
            return defValue;
        }

        #endregion

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="length">验证码的长度</param>
        /// <returns>验证码</returns>
        public static string CreateValidateNumber(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            StringBuilder validateNumberStr = new StringBuilder();
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr.Append(validateNums[i].ToString());
            }
            return validateNumberStr.ToString();
        }
        ///// <summary>
        ///// 获取支付宝合作身份者ID
        ///// </summary>
        ///// <returns></returns>
        //public static string GetAlipartner()
        //{
        //    return GetConfigString("Alipartner");
        //}
        //public static string GetAlikey()
        //{
        //    return GetConfigString("Alikey");
        //}
        /// <summary>
        /// JsonToObj
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonTObj<T>(string json)
        {
            System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            return ser.Deserialize<T>(json);
        }

        //public static string MQPremisesPicQueueName
        //{
        //    get
        //    {
        //        return ConfigurationManager.AppSettings["MQPremisesPicQueueName"].ToString();
        //    }
        //}

        //public static string PremisesImgUploadBaseUrl
        //{
        //    get
        //    {
        //        return ConfigurationManager.AppSettings["premisesimgupload.baseUrl"].ToString();
        //    }
        //}

        public static bool CheckCityId(int cityId)
        {
            int[] a = { 253, 239, 344, 355, 205, 307, 243, 155, 296, 263, 347, 346, 343 };
            if (a.Contains(cityId)) return true;
            return false;
        }
    }
}
