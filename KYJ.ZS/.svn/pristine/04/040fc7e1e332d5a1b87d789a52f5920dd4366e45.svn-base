using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.User
{
    /// <summary>
    /// UploadAdvanced 的摘要说明
    /// </summary>
    public class UploadAdvanced : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["Filedata"];
            string guid = context.Request.QueryString["para"];

            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid))
            {
                string result = UploadPicture.SaveUserPicture(file, true, guid, UserPictureType.LOGO.ToString());
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