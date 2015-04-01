
namespace TXCommons
{
    public class Email
    {
        /// <summary>
        /// 返回邮箱操作类
        /// </summary>
        /// <returns></returns>
        public static EmailInfo GetEmailInfo()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["MailUrl"].ToString();
            string pwd = System.Configuration.ConfigurationManager.AppSettings["MailPwd"].ToString();

            EmailInfo emailInfo = new EmailInfo();
            //  emailInfo.EmailUrl = url;
            emailInfo.SMTPUserName = url;
            emailInfo.SMTPUserPassword = pwd;
            emailInfo.FromName = url;
            return emailInfo;
        }

        /// <summary>
        /// 返回激活链接页面URL
        /// </summary>
        /// <param name="keys">链接参数名</param>
        /// <param name="values">链接参数值</param>
        /// <returns></returns>
        public static string GetEmailUrl(string[] keys, string[] values, string url)
        {
            string path = url;
            path = path + "?" + keys[0] + "=" + values[0];
            for (int i = 1; i < keys.Length; i++)
            {
                path = path + "&" + keys[i] + "=" + values[i];
            }
            return path;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="pEUrl">收件人</param>
        /// <param name="pEUrl">发送内容</param>
        /// <param name="pEmailInfo">邮件信息</param>
        /// <returns></returns>
        public static bool SendEmailReturn(string pEmail, string pEUrl, EmailInfo pEmailInfo)
        {

            MailComm mailCom = new MailComm();
            mailCom.SMTPUserName = pEmailInfo.SMTPUserName;
            mailCom.SMTPUserPassword = pEmailInfo.SMTPUserPassword;
            mailCom.FromName = pEmailInfo.FromName;
            mailCom.ToName = pEmail;
            //发送标题，内容
            return mailCom.SendEmailToSelf("来自快有家的密码找回邮件", pEUrl);

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="pEmail">收件人</param>
        /// <param name="pContent">发送内容</param>
        /// <param name="pEmailInfo">邮件信息</param>
        /// <param name="message">邮件标题</param>
        /// <returns></returns>
        public static bool SendEmailReturn(string pEmail, string pContent, EmailInfo pEmailInfo, string message)
        {

            MailComm mailCom = new MailComm();
            mailCom.SMTPUserName = pEmailInfo.SMTPUserName;
            mailCom.SMTPUserPassword = pEmailInfo.SMTPUserPassword;
            mailCom.FromName = pEmailInfo.FromName;
            mailCom.ToName = pEmail;
            //发送标题，内容
            return mailCom.SendEmailToSelf(message, pContent);

        }


    }

    /// <summary>
    /// 邮件功能信息 
    /// </summary>
    public class EmailInfo
    {
        string _EmailUrl = "";
        string _SMTPUserName = "";
        string _SMTPUserPassword = "";
        string _FromName = "";

        /// <summary>
        /// 邮箱登陆验证地址
        /// </summary>
        public string EmailUrl
        {
            get
            {
                return _EmailUrl;
            }
            set
            {
                _EmailUrl = value;
            }
        }
        /// <summary>
        /// 服务器邮箱用户名
        /// </summary>
        public string SMTPUserName
        {
            get
            {
                return _SMTPUserName;
            }
            set
            {
                _SMTPUserName = value;
            }
        }
        /// <summary>
        /// 服务器邮箱密码
        /// </summary>
        public string SMTPUserPassword
        {
            get
            {
                return _SMTPUserPassword;
            }
            set
            {
                _SMTPUserPassword = value;
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string FromName
        {
            get
            {
                return _FromName;
            }
            set
            {
                _FromName = value;
            }
        }
    }
}
