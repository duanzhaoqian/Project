using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using FilterIPAddress.Bll;

namespace FilterIPAddress
{
    internal class FilterIPAddressModule : IHttpModule
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
    
        }
        FilterIPAddress_Bll filterIpAddressBll = new FilterIPAddress_Bll();
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication contextApplication = sender as HttpApplication;
            if (contextApplication != null)
            {
                string ipAddress = contextApplication.Request.UserHostAddress.Trim();
              
                if (!filterIpAddressBll.CheckIPAddress(ipAddress))
                {
                    contextApplication.Response.Redirect(ConfigurationManager.AppSettings["FilterIPAddressToURL"]);
                    contextApplication.Response.End();
                }
            }
        }
    }
}
