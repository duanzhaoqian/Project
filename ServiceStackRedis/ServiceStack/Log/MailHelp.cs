using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ServiceStack.Log
{
    class MailHelp
    {
        private SmtpClient smtpClient = new SmtpClient();

        public MailHelp(string smtpHost, int port, string userName, string pwd, bool enableSsl)
        {
            smtpClient.Host = smtpHost;
            if (port!=0)
            {
                smtpClient.Port = port;
            }
            smtpClient.Credentials = new NetworkCredential(userName, pwd);
            if (enableSsl)
            {
                smtpClient.EnableSsl = true;
            }
        }

        public void SendMail(string mailFrom, string displayName, string mailchaosong, string mailTo, string mailSubject, string content)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(mailTo);
            mailMessage.From = new MailAddress(mailFrom, displayName, Encoding.UTF8);
            if (!string.IsNullOrEmpty(mailchaosong))
            {
                mailMessage.CC.Add(mailchaosong);
            }
            mailMessage.Subject = mailSubject;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = content;
            mailMessage.BodyEncoding = Encoding.UTF8;
            smtpClient.Send(mailMessage);
        }
    }
}
