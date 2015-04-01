using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace KYJ.ZS.Log4net
{
    /// <summary>
    /// Author: zhuzh
    /// Date  :2014-05-29
    /// Desc  :系统记录日志类
    /// </summary>
    public class RecordLog
    {
        private static string Ip
        {
            get
            {
                try { return System.Web.HttpContext.Current.Request.UserHostAddress; }
                catch { return string.Empty; }
            }
        }
        private static string rawUrl
        {
            get
            {
                try { return System.Web.HttpContext.Current.Request.RawUrl + "</br>当前请求的Url:" + System.Web.HttpContext.Current.Request.Url.ToString(); }
                catch { return string.Empty; }
            }
        }

        #region 搜索日志
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword">关键词</param>
        public static void SearchLogs(string value)
        {
            LogImplement log = LogFactory.GetLogger("searchlogs");
            log.Info(value);
        }

        #endregion

        #region 支付日志
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="keyword">关键词</param>
        /// <param name="adminId"></param>
        /// <param name="adminName"></param>
        public static void PayPalLogs(string type, string keyword, string adminId, string adminName)
        {
            LogImplement log = LogFactory.GetLogger("paypallogs");
            log.Info(type + ":" + keyword + "," + adminId + "," + adminName);
        }

        #endregion

        #region 服务日志
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="adminId"></param>
        /// <param name="adminName"></param>
        public static void ServiceLogs(string realname, string msg)
        {
            LogImplement log = LogFactory.GetLogger("Servicelogs");
            log.Info("处理人" + ":" + realname + "<br/>" + msg + "</br>");
        }

        #endregion

        #region 无异常情况下 记录log
        /// <summary>
        ///  无异常情况下 记录log
        /// </summary>
        /// <param name="realname">真实姓名</param>
        /// <param name="message">消息说明</param>
        public static void RecordException(string realname, string message)
        {
            LogImplement log = LogFactory.GetLogger("logerror");
            log.Error("处理人:" + realname + "</br>未出异常情况：" + message + "</br>请求客户端IP:" + Ip + "</br>请求原始Url:" + rawUrl);
        }
        #endregion

        #region 记录异常2
        /// <summary>
        ///  记录异常2
        /// </summary>
        /// <param name="realName">处理人姓名 (各自的写的代码异常处理调用该方法)</param>
        /// <param name="param"> 传入参数[多个参数 按照传入顺序一次拼接格式如下(a,b,c,d) ] 无参数传null</param>
        /// ///  <param name="e">Exception</param>
        public static void RecordException(string realName, string param, Exception e)
        {
            LogImplement log = LogFactory.GetLogger("logerror");
            string str = String.IsNullOrEmpty(param) ? "" : "传入参数:" + param + "<br/>";
            log.Error("处理人" + ":" + realName + "<br/>" + str + e.Message + e.StackTrace + "</br>请求客户端IP:" + Ip + "</br>请求原始Url:" + rawUrl);
        }
        #endregion

        #region 记录异常 多参数传参数实体
        /// <summary>
        ///  记录异常2
        /// </summary>
        /// <param name="realName">处理人姓名 (各自的写的代码异常处理调用该方法)</param>
        /// <param name="param">实体</param>
        /// ///  <param name="e">Exception</param>
        public static void RecordException<T>(string realName, T mode, Exception e)
        {
            LogImplement log = LogFactory.GetLogger("logerror");
            StringBuilder sb = new StringBuilder();
            object value = null;
            if (mode != null)
            {
                foreach (PropertyInfo pi in typeof(T).GetProperties())
                {
                    value = pi.GetValue(mode, null);
                    sb.Append(pi.Name + ":" + (value == null ? "" : value.ToString()));
                }
            }
            log.Error("处理人" + ":" + realName + "<br/>" + sb.ToString() + e.Message + e.StackTrace + "</br>请求客户端IP:" + Ip + "</br>请求原始Url:" + rawUrl);
        }
        #endregion


    }
}
