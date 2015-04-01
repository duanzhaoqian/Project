using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services.Description;
using Model;

namespace WebApplication1
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            OverSeaPremisesSearchModel overSeaPremisesSearchModel = new OverSeaPremisesSearchModel();

            string url = "http://search.newhouse.com/Premises/OverSeaPremisesHandler.ashx";
            string condition = new JavaScriptSerializer().Serialize(overSeaPremisesSearchModel);
            string httpUrl = url + "?condition=" + condition;
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                string result = webClient.UploadString(url, "post", "condition=" + condition);
                using ( StringReader stream = new StringReader(result))
                {
                    DataSet dataSet = new DataSet();

                    dataSet.ReadXml(stream);
                    dataSet.DataSetName = "Hello";
                    if (dataSet.Tables.Count == 1)
                    {
                        dataSet.Tables[0].TableName = "test";
                    }
                    context.Response.Write(dataSet.GetXml());
                }
              
            }
          
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