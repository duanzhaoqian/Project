using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace TXImage.User
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
                int new_x = 0, new_y = 0, new_w = 0, new_h = 0, new_oh = 0, new_ow = 0;

                new_x = Convert.ToInt32(Convert.ToDouble(x));
                new_y = Convert.ToInt32(Convert.ToDouble(y));
                new_w = Convert.ToInt32(Convert.ToDouble(w));
                new_h = Convert.ToInt32(Convert.ToDouble(h));
                new_oh = Convert.ToInt32(Convert.ToDouble(oh));
                new_ow = Convert.ToInt32(Convert.ToDouble(ow));

                TXCommons.PictureModular.UploadPicture.SaveUserCutPicture(guid, filename, new_w, new_h, new_x, new_y,
                    new_ow, new_oh);
                context.Response.Write(Jsonp(context, "result:\"true\""));
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
            return context.Request["callbackparam"].ToString() + "([{" + s + "}])";
        }
    }
}