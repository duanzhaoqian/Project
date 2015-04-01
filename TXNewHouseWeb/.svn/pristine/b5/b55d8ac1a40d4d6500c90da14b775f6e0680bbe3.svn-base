using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Commons;

namespace TXNewSearch.Premises
{
    /// <summary>
    /// Ranking 的摘要说明
    /// </summary>
    public class Ranking : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string condition = context.Request.Params["condition"];
            context.Response.ContentType = "text/xml";
            string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
            try
            {
                var opt = new LuceneOpt();
                MQHelper.SetDictionary();

                System.Data.DataSet ds = (System.Data.DataSet)opt.SearchRanking(context.Server.UrlDecode(condition));


                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    result += "<Results/>";
                }
                else
                {
                    result += "\n" + ds.GetXml();
                }

            }
            catch (Exception ex)
            {
                result += "<Results/>";
            }
            context.Response.Write(result);
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