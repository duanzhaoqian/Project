using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MonitorClient.AppUtility
{
    public class SessionMag
    {
        // SessionString = dt.Rows[0]["ID"].ToString().Trim() + "<@#$%>" + dt.Rows[0]["USERID"].ToString().Trim() + "<@#$%>" + dt.Rows[0]["ROLES"].ToString().Trim() + "<@#$%>";/
        //string ip = MonitorClient.AppUtility.SessionMag.ToStringBySession(MonitorClient.AppUtility.CommonList.mySESSION, 2);
        const string strf = @"\<\@\#\$\%\>";
        /// <summary>
        /// USERID+设备类型（1：Intel CE 2：武汉精伦 3：网吧 4：爱聊）+外网IP+内网IP+登录时间+:<@#$%>
        /// SessionString = dt.Rows[0]["USERID"].ToString().Trim() + "<@#$%>" + DEVTYPE + "<@#$%>" + WANIP + "<@#$%>" + LANIP + "<@#$%>"+MACADDR;
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="INo"></param>
        /// <returns></returns>
        public static string ToStringBySession(string strSource, int INo)
        {
            try
            {
                bool iss = false;
                strSource = Encrypt.MD5Decrypt(strSource, ref iss);
                string[] resultString = Regex.Split(strSource, strf, RegexOptions.IgnoreCase);
                if (INo >= resultString.Length)
                    return string.Empty;
                return resultString[INo].ToString().Trim();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
