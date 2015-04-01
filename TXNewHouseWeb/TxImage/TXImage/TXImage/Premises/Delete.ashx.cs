using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TXCommons.PictureModular;
using ServiceStack;

namespace TXImage.Premises
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string guid = context.Request["guid"];
            string id = context.Request["id"];
            string picturetype = context.Request.QueryString["picturetype"];
            int minnum = int.Parse(context.Request.QueryString["minnum"]);
            int cityId = int.Parse(context.Request.QueryString["cityId"]);

            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(picturetype))
            {
                string key = KeyManager.GetPremisesPictureKey(guid, picturetype.ToUpper());
                int num = Redis.GetSortedSetCount<PremisesPictureInfo>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                if (num > minnum)
                {
                    bool result = UploadPicture.DeleteRentHousePicture(guid, int.Parse(id), picturetype, cityId);
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
                    context.Response.Write(Jsonp(context, "result:\"需至少上传" + minnum + "张照片\""));
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