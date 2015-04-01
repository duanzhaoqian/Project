using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TXImage.Document
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
            string para = context.Request.Params["para"];
            string guid = string.Empty;
            string documenttype = string.Empty;
            if (para.IndexOf("^") > -1)
            {
                guid = para.Split(new char[] { '^' })[0];
                documenttype = para.Split(new char[] { '^' })[1];
            }
            else
            {
                guid = para.Split(new char[] { '_' })[0];
                documenttype = para.Split(new char[] { '_' })[1];
            }

            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid))
            {
                string result = TXCommons.PictureModular.UploadPicture.SaveDocument(file, guid, documenttype);

                if (!string.IsNullOrEmpty(result))
                {
                    context.Response.Write(result);
                }
                else
                {
                    context.Response.Write("error");
                }
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