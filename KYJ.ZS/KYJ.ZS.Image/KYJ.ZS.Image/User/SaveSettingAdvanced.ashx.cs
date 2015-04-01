using System;
using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.User
{
    /// <summary>
    /// SaveSettingAdvanced 的摘要说明
    /// </summary>
    public class SaveSettingAdvanced : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string guid = context.Request.QueryString["guid"];
            string x = context.Request.QueryString["x"];
            string y = context.Request.QueryString["y"];
            string w = context.Request.QueryString["w"];
            string h = context.Request.QueryString["h"];
            string ow = context.Request.QueryString["ow"];
            string oh = context.Request.QueryString["oh"];
            string filename = context.Request.QueryString["filename"];


            try
            {
                int newX = Convert.ToInt32(Convert.ToDouble(x));
                int newY = Convert.ToInt32(Convert.ToDouble(y));
                int newW = Convert.ToInt32(Convert.ToDouble(w));
                int newH = Convert.ToInt32(Convert.ToDouble(h));
                int newOh = Convert.ToInt32(Convert.ToDouble(oh));
                int newOw = Convert.ToInt32(Convert.ToDouble(ow));

                var result = UploadPicture.SaveUserCutPicture(guid, filename, newW, newH, newX, newY, newOw, newOh);
                if (!string.IsNullOrEmpty(result))
                {
                    context.Response.Write(Jsonp(context, result));
                }
                else
                {
                    context.Response.Write(Jsonp(context, "result:\"false\""));
                }
            }
            catch
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