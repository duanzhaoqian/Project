using System.Net.Mail;

namespace TXCommons
{
    /// <summary> EnhancedMailMessage is a class that provides more features for email sending in .NET </summary>
    public class MailComm : MailMessage
    {
        private string fromName;
        private string toName;
        private string smtpUserName;
        private string smtpUserPassword;
        private string mailSmtp = TXCommons.GetConfig.MailSmtp;
        public MailComm()
        {
            fromName = string.Empty;
            toName = string.Empty;
            smtpUserName = string.Empty;
            smtpUserPassword = string.Empty;
        }

        /// <summary>
        /// The display name that will appear in the recipient mail client
        /// </summary>
        public string FromName
        {
            set
            {
                fromName = value;
            }
            get
            {
                return fromName;
            }
        }

        /// <summary>
        /// The display name that will appear in the recipient mail client
        /// </summary>
        public string ToName
        {
            set
            {
                toName = value;
            }
            get
            {
                return toName;
            }
        }

        /// <summary>
        /// SMTP用户名
        /// </summary>
        public string SMTPUserName
        {
            set
            {
                smtpUserName = value;
            }
            get
            {
                return smtpUserName;
            }
        }

        /// <summary>
        /// SMTP密码
        /// </summary>
        public string SMTPUserPassword
        {
            set
            {
                smtpUserPassword = value;
            }
            get
            {
                return smtpUserPassword;
            }
        }

        private void SendMail(SmtpClient client, MailAddress from, string password, MailAddress to, MailMessage message)
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from.Address, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                message.Dispose();
            }
        }
        #region
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="strSubject">标题</param>
        /// <param name="strBody">内容</param>
        /// <returns></returns>
        public bool SendEmailToSelf(string strSubject, string strBody)
        {
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(FromName);
            System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(toName);
            MailMessage message = new MailMessage(from, to);
            message.Subject = strSubject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Body = strBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            try
            {
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(mailSmtp);//这个不能修改
                SendMail(client, from, SMTPUserPassword, to, message);
            }
            catch (SmtpException err)
            {
                if (err.StatusCode == SmtpStatusCode.GeneralFailure)
                {
                    try
                    {
                        SmtpClient client = new SmtpClient(from.Host);
                        SendMail(client, from, SMTPUserPassword, to, message);
                    }
                    catch (SmtpException err1)
                    {
                        string errInfo1 = err1.ToString();
                    }
                }
                else
                {
                    string errInfo = err.ToString();
                }
                return false;
            }
            return true;
        }
        #endregion
    }
}