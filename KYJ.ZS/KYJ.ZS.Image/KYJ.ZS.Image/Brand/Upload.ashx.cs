using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Brand
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];

            
            string guid = context.Request.QueryString["para"];

            if (!string.IsNullOrEmpty(guid))
            {
                ///////////////////////////////////////
                var result = UploadPicture.SaveBrandPicture(file, guid);
                context.Response.Write(!string.IsNullOrEmpty(result) ? result : "error");
            }
            else
            {
                context.Response.Write("error");
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