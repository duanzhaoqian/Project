using System;
using System.Drawing;
using System.Web;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Advertisement
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
            var guid = para.Split(new[] { '^' })[0];
            int bwi = Convert.ToInt32(para.Split(new[] { '^' })[1]);
            int bhei = Convert.ToInt32(para.Split(new[] { '^' })[2]); 
            if (file != null)
            {
                var bit = new Bitmap(file.InputStream);
                if (bwi == 0 && bhei == 0)
                {
                    context.Response.Write("sizeerror,请选择广告位置后再上传图片"); 
                }
                else if (bit.Width == bwi && bit.Height == bhei)
                {
                    var result = UploadPicture.SaveAdvertisementPicture(file, guid);
                    context.Response.Write(!string.IsNullOrEmpty(result) ? result : "error");
                }
                else
                {
                    context.Response.Write("sizeerror,请上传尺寸为" + bwi + "*" + bhei + "大小的图片");
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