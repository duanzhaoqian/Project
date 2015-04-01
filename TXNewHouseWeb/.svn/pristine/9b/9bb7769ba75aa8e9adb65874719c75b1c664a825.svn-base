using System;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace TXManagerWeb.Common
{
    public class AuthenticationService
    {
        private const int TICKET_VERSION_NUMBER = 1;

        public static void SignIn(string username, object userData, bool createPersistent)
        {
            HttpContext.Current.Response.Cookies.Clear();
            var currentDateTime = DateTime.Now;
            var formsTicket = new FormsAuthenticationTicket(TICKET_VERSION_NUMBER, username,
                currentDateTime, currentDateTime.AddMonths(1),
                createPersistent,
                userData == null ? string.Empty : userData.ToString());
            string encryptedTicket = FormsAuthentication.Encrypt(formsTicket);
            var formsTicketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //formsTicketCookie.Domain = "manager_kuaiyoujia";
            HttpContext.Current.Response.Cookies.Add(formsTicketCookie);
        }

        public static void SignIn(string userdata, bool createPersistent)
        {
            if (string.IsNullOrWhiteSpace(userdata))
                throw new ArgumentException("用户名不能为空字符串或者null", "username");
            var cookie = new HttpCookie("manager_kuaiyoujia");
            cookie.Domain = ConfigurationManager.AppSettings["domain"];
            cookie.Path = "/";
            cookie.Value = userdata;
            cookie.Expires = DateTime.Now.AddHours(12);
            HttpContext.Current.Response.SetCookie(cookie);
        }

        public static void SignOut()
        {
            var cookie = new HttpCookie("manager_kuaiyoujia");
            cookie.Domain = ConfigurationManager.AppSettings["domain"];
            cookie.Expires = DateTime.Now.AddDays(-30);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}