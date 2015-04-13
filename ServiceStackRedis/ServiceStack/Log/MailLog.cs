using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ServiceStack.Log
{
    [Obsolete("不再使用，以MailLogger代替",true)]
    class MailLog:LogBase,ILog
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
        public MailLog(string name) : base(name)
        {
            string mailConfigName =
                LogConfig.GetElement(LogConfig.XDocument.Element("Log"), "name", name).Attribute("config").Value;
            XElement xElement = LogConfig.GetElement(LogConfig.XDocument.Element("Log"), "name", mailConfigName);
            SmtpHost = xElement.Element("SmtpHost").Value;
            //SmtpPort = Convert.ToInt32(xElement.Element("SmtpPort").Value);
            SmtpUserName = xElement.Element("SmtpUserName").Value;
            SmtpPwd = xElement.Element("SmtpPwd").Value;
            SmtpEnableSsl = Convert.ToBoolean(xElement.Element("SmtpEnableSsl").Value);
            MailFrom = xElement.Element("MailFrom").Value;
            MailDisplayName = xElement.Element("MailDisplayName").Value;
            MailChaoSong = xElement.Element("MailChaoSong").Value;
            MailTo = xElement.Element("MailTo").Value;
            MailSubject = xElement.Element("MailSubject").Value;
        }

        public void Log(string content, string name)
        {
            MailHelp mailHelp=new MailHelp(SmtpHost,SmtpPort,SmtpUserName,SmtpPwd,SmtpEnableSsl);
            mailHelp.SendMail(MailFrom, MailDisplayName, MailChaoSong, MailTo, MailSubject, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}===={3}", content, name, DateTime.Now, Environment.NewLine));
        }

        public void LogError(string content, string name, Exception exception)
        {
            MailHelp mailHelp = new MailHelp(SmtpHost, SmtpPort, SmtpUserName, SmtpPwd, SmtpEnableSsl);
            mailHelp.SendMail(MailFrom, MailDisplayName, MailChaoSong, MailTo, MailSubject, string.Format("===={3}Content：{0} {3}Name：{1} {3}Date：{2}{3}Exception：Message：{4}{3}StackTrace：{5}{3}===={3}", content, name, DateTime.Now, Environment.NewLine, exception.Message, exception.StackTrace));
        }
    }
}
