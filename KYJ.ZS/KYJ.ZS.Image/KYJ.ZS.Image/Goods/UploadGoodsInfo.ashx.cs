using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Goods
{
    /// <summary>
    /// UploadGoodsInfo 的摘要说明
    /// </summary>
    public class UploadGoodsInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];
            var para = context.Request.Params["para"];

            string guid = para.Split(new[] { '^' })[0];

            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid))
            {
                var result = UpdataImageRAR.SavePostedImage(file, guid, "INFO");
                var arr = result.Split(',').ToList();
                var data = new PicJsonData
                {
                    id = arr[1],
                    original = arr[2],
                    name = arr[2],
                    type = arr[3],
                    url = arr[0],
                    size = arr[4],
                    state = "SUCCESS"
                };
                context.Response.Write(ToJSon.ObjectToJson(data));
            }
            else
            {
                context.Response.Write(Jsonp(context, "result:\"Eroor\""));
            }
        }
        

        public string Jsonp(HttpContext context, string s)
        {
            return context.Request["callbackparam"].ToString() + "([{" + s + "}])";
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