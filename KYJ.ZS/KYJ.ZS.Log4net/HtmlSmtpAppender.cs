using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;
using System.Net.Mail;
using System.Net;

namespace KYJ.ZS.Log4net
{
    /// <summary>
    /// Author: zhuzh
    /// Date  : 2014-05-29   
    /// Desc  : 重写SmtpAppender，发送HTML电子邮件时记录事件发生
    /// </summary>
    public class HtmlSmtpAppender : SmtpAppender
    {
        /// <summary>Sends an email message</summary>
        protected override void SendEmail(string body)
        {
            CreateSmtpClient().Send(CreateMailMessage(body));
        }

        /// <summary>Creats new SMTP client based on the properties set in the configuration file</summary>
        private SmtpClient CreateSmtpClient()
        {
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = SmtpHost;
            smtpClient.Port = Port;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (string.IsNullOrEmpty(Username))
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            else
                smtpClient.Credentials = new NetworkCredential(Username, Password);

            return smtpClient;
        }

        /// <summary>Creats new mail message based on the properties set in the configuration file</summary>
        private MailMessage CreateMailMessage(string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(From);
            mailMessage.To.Add(To);
            mailMessage.Subject = Subject;
            mailMessage.Body = body;
            mailMessage.Priority = Priority;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }
    }
}
