using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.User
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
                string result = UploadPicture.SaveUserPortraitPicture(file, guid, UserPictureType.LOGO.ToString());
                context.Response.Write(!string.IsNullOrEmpty(result) ? result : "error");
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