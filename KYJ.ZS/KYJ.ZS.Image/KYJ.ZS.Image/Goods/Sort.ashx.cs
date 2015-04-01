using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Goods
{
    /// <summary>
    /// Sort 的摘要说明
    /// </summary>
    public class Sort : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            string guid = context.Request.QueryString["guid"];
            string picturetype = context.Request.QueryString["picturetype"];
            string newSortIds = context.Request.QueryString["newSortIds"];

            if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(newSortIds))
            {
                var newSortId = newSortIds.Split('-');
                var type = picturetype.Split('-');
                if (newSortId.Length == type.Length)
                {
                    for (int i = 0; i < type.Length; i++)
                    {
                        GetPicture.NewSortGoodsPicture(guid, type[i], newSortId[i]);
                    }
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