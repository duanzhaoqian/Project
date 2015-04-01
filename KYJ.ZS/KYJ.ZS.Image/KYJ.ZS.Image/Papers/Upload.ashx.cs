using System.Web;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Papers
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
            var para = context.Request.Params["para"];
            string guid;
            string picturetype;
            int maxnum;
            if (para.IndexOf("^") > -1)
            {
                guid = para.Split(new[] { '^' })[0];
                picturetype = para.Split(new[] { '^' })[1];
                maxnum = int.Parse(para.Split(new[] { '^' })[2]);
            }
            else
            {
                guid = para.Split(new char[] { '_' })[0];
                picturetype = para.Split(new char[] { '_' })[1];
                maxnum = int.Parse(para.Split(new char[] { '_' })[2]);
            }
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(picturetype))
            {
                string result = string.Empty;
                string key = KeyManager.GetUserPapersPictureKey(guid, picturetype.ToUpper());
                int num = RedisTool.GetSortedSetCount<UserPapersPictureInfo>(key, FunctionType.ZuShouPapersPicture, 0);
                if (num < maxnum)
                {
                    result = UploadPicture.SaveUserPapersPicture(file, guid, picturetype);
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