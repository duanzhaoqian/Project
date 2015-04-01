using System;
using System.Web;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons.PictureModular;
using ServiceStack;
using System.Drawing;
using KeyManager = KYJ.ZS.Commons.Common.KeyManager;

namespace KYJ.ZS.Image.User
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
            string guid;
            string picturetype;
            int maxnum;
            if (para.IndexOf("^", System.StringComparison.Ordinal) > -1)
            {
                guid = para.Split(new[] { '^' })[0];
                picturetype = para.Split(new[] { '^' })[1];
                maxnum = int.Parse(para.Split(new[] { '^' })[2]);
            }
            else
            {
                guid = para.Split(new[] { '_' })[0];
                picturetype = para.Split(new[] { '_' })[1];
                maxnum = int.Parse(para.Split(new[] { '_' })[2]);
            }

            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid))
            {
                
                string key = KeyManager.GetUserPictureKey(guid, picturetype.ToUpper());
                int num = RedisTool.GetSortedSetCount<UserPictureInfo>(key, FunctionTypeEnum.UserHeadImage, 0);
                if (num < maxnum)
                {
                    string result = UploadPicture.SaveUserPicture(file, false, guid, picturetype);
                    context.Response.Write(!string.IsNullOrEmpty(result) ? result : "error");
                }
                else if (picturetype == UserPictureType.PASSPORT.ToString() ||
                         picturetype == UserPictureType.MERCHANTLOGO.ToString())
                {
                    
                    string result = UploadPicture.SaveUserPicture(file, true, guid, picturetype);
                    context.Response.Write(!string.IsNullOrEmpty(result) ? result : "error");
                }
                else
                {
                    context.Response.Write("exceedmaxnum");
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