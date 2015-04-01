using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TXCommons.PictureModular;
using ServiceStack;

namespace TXImage.User
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
            string picturetype = string.Empty;
            int maxnum = 0;
            int cityId = 0;
            if (para.IndexOf("^") > -1)
            {
                guid = para.Split(new char[] { '^' })[0];
                picturetype = para.Split(new char[] { '^' })[1];
                maxnum = int.Parse(para.Split(new char[] { '^' })[2]);
                cityId = int.Parse(para.Split(new char[] { '^' })[3]);
            }
            else
            {
                guid = para.Split(new char[] { '_' })[0];
                picturetype = para.Split(new char[] { '_' })[1];
                maxnum = int.Parse(para.Split(new char[] { '_' })[2]);
                cityId = int.Parse(para.Split(new char[] { '_' })[3]);
            }
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(picturetype))
            {
                string result = string.Empty;
                string key = KeyManager.GetUserPictureKey(guid, picturetype.ToUpper());
                //int num = Redis.GetSortedSetCount<UserPictureInfo>(key, FunctionTypeEnum.NewHouseUserInfo, cityId);
                //if (num < maxnum)
                //{
                    result = UploadPicture.SaveUserPicture(file, guid, picturetype, cityId);
                  
                    if (!string.IsNullOrEmpty(result))
                    {
                        context.Response.Write(result);
                    }
                    else
                    {
                        context.Response.Write("error");
                    }
                //}
                //else
                //{
                //    context.Response.Write("exceedmaxnum");
                //}
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