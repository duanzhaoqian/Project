using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.User
{
    /// <summary>
    /// SaveCommon 的摘要说明
    /// </summary>
    public class SaveCommon : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string para = context.Request.Params["para"];

            string guid;

            string filename;

            if (para.IndexOf("^", System.StringComparison.Ordinal) > -1)
            {
                guid = para.Split(new[] { '^' })[0];
                filename = para.Split(new[] { '^' })[1];
            }
            else
            {
                guid = para.Split(new[] { '_' })[0];
                filename = para.Split(new[] { '_' })[1];
            }


            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(filename))
            {
                string result = UploadPicture.SaveUserPortraitRdies(guid, filename, UserPictureType.LOGO.ToString());

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
            return context.Request["callbackparam"] + "([{" + s + "}])";
        }
    }
}