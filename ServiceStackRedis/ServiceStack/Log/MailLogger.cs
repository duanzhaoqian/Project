using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.Log
{
    public class MailLogger:ILog
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPwd { get; set; }
        public bool SmtpEnableSsl { get; set; }
        public string MailFrom { get; set; }
        public string MailDisplayName { get; set; }
        public string MailChaoSong { get; set; }
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        /// <summary>
        /// 发送邮件类
        /// </summary>
        /// <param name="mailSubject">邮件标题</param>
        /// <param name="mailTo">收件人地址。多个收件人以 , 隔开</param>
        /// <param name="mailChaoSong">抄送地址。多个抄送地址以 , 隔开</param>
        public MailLogger(string mailSubject,string mailTo,string mailChaoSong)
        {
            SmtpHost = "smtp.exmail.qq.com";
            //SmtpPort = Convert.ToInt32(xElement.Element("SmtpPort").Value);
            SmtpUserName = "service@kuaiyoujia.com";
            SmtpPwd = "CJKJB2038";
            SmtpEnableSsl = false;
            MailFrom = "service@kuaiyoujia.com";
            MailDisplayName = "快有家架构部";
            MailChaoSong = mailChaoSong;
            MailTo = mailTo;
            MailSubject = mailSubject;
        }

        public void Log(string content, string name)
        {
           // throw new NotImplementedException();
        }

        public void LogError(string content, string name, Exception exception)
        {
            MailHelp mailHelp = new MailHelp(SmtpHost, SmtpPort, SmtpUserName, SmtpPwd, SmtpEnableSsl);
            mailHelp.SendMail(MailFrom, MailDisplayName, MailChaoSong, MailTo, MailSubject, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}Exception：Message：{4}{3}StackTrace：{5}{3}===={3}", content, name, DateTime.Now, Environment.NewLine, exception.Message, exception.StackTrace));
        }
    }
}
