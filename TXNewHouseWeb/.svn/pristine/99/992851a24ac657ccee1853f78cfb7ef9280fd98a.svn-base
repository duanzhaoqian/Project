using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TXImage.User
{
    /// <summary>
    /// SaveCommon 的摘要说明
    /// </summary>
    public class SaveCommon : IHttpHandler
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string para = context.Request.Params["para"];

            string guid = string.Empty;

            string filename = string.Empty;

            if (para.IndexOf("^") > -1)
            {
                guid = para.Split(new char[] { '^' })[0];
                filename = para.Split(new char[] { '^' })[1];
            }
            else
            {
                guid = para.Split(new char[] { '_' })[0];
                filename = para.Split(new char[] { '_' })[1];
            }


            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(filename))
            {
                string result = TXCommons.PictureModular.UploadPicture.SaveUserPortraitRdies(guid, filename, TXCommons.PictureModular.UserPictureType.LOGO.ToString());

                if (!string.IsNullOrEmpty(result))
                {
                    context.Response.Write(Jsonp(context, result));
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
            return context.Request["callbackparam"].ToString() + "([{" + s + "}])";
        }
    }
}