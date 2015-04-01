using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Advertisement
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var para = context.Request.Params["para"];

            string guid = para.Split(new[] { '^' })[0]; 
            string id = para.Split(new[] { '^' })[1];

            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(id))
            {
                bool result = UploadPicture.DeleteAdvertisementPicture(guid, int.Parse(id));
                if (result)
                {
                    context.Response.Write(Jsonp(context, "result:\"true\""));
                }
                else
                {
                    context.Response.Write(Jsonp(context, "result:\"false\""));
                }
            }
            else
            {
                context.Response.Write(Jsonp(context, "result:\"false\""));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string Jsonp(HttpContext context, string s)
        {
            return context.Request["callbackparam"] + "([{" + s + "}])";
        }
    }
}