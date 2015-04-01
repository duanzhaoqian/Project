using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TXSearch.Goodses
{
    /// <summary>
    /// GetRentalGoodses 的摘要说明
    /// </summary>
    public class GetRentalGoodses : IHttpHandler
    {
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "RentalGoodsesIndex/";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}