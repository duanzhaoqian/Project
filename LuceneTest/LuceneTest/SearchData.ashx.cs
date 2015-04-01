using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using LuceneOpt;
using TXOrm;

namespace LuceneTest
{
    /// <summary>
    /// SearchData 的摘要说明
    /// </summary>
    public class SearchData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            string key = context.Request["key"];
            if (string.IsNullOrEmpty(key))
            {
                key = "王先生";
            }
            if (!string.IsNullOrEmpty(key))
            {
                LuceneSearchData luceneSearchData = new LuceneSearchData();
                List<S_LongHouseBase> list = luceneSearchData.Seclect(key);
                if (list.Count > 0)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<S_LongHouseBase>));

                    XmlWriter xmlWriter = new XmlTextWriter(context.Response.OutputStream, Encoding.UTF8);
                    xmlSerializer.Serialize(xmlWriter, list);
                    xmlWriter.Close();
                }

            }

            //context.Response.Write(System.IO.Directory.GetCurrentDirectory() + "<br/>");
            //context.Response.Write(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + "<br/>");
            //context.Response.Write(AppDomain.CurrentDomain.BaseDirectory + "<br/>");
            //context.Response.Write(System.Environment.CurrentDirectory+"<br/>");
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