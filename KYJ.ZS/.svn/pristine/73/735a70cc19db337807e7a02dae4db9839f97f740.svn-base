using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Goods
{
    /// <summary>
    /// Delete 的摘要说明
    /// </summary>
    public class Delete : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var para = context.Request.Params["para"];

            string guid = para.Split(new[] {'^'})[0]; //context.Request["guid"];
            string id = para.Split(new[] {'^'})[1]; //context.Request["id"];
            string picturetype = para.Split(new[] {'^'})[2];//context.Request.QueryString["picturetype"];

            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(picturetype))
            {
                bool result = UploadPicture.DeleteGoodsPicture(guid, int.Parse(id), picturetype);
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