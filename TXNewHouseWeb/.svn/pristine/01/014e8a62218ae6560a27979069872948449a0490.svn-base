using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TXImage.User
{
    /// <summary>
    /// UploadCommon 的摘要说明
    /// </summary>
    public class UploadCommon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string guid = context.Request.QueryString["para"];

            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid))
            {
                string result = TXCommons.PictureModular.UploadPicture.SaveUserPortraitPicture(file, guid, TXCommons.PictureModular.UserPictureType.LOGO.ToString());
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