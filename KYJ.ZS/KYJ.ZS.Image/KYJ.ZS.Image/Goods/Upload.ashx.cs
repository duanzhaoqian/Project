using System;
using System.Drawing;
using System.Web;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Image.Goods
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
            if (para.IndexOf("^", StringComparison.Ordinal) > -1)
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
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(picturetype))
            {
                string key = KeyManager.GetGoodsPictureKey(guid, picturetype);
                int num = RedisTool.GetSortedSetCount<GoodsInfo>(key, FunctionType.ZuShouGoodsPicture, 0);
                if (GoodsType.FREE.ToString().Equals(picturetype))
                {
                    var result = UploadPicture.SaveGoodsPicture(file, guid, picturetype);
                    context.Response.Write(!string.IsNullOrEmpty(result) ? result : "error");
                }
                if (num < maxnum)
                {
                    var result = UploadPicture.SaveGoodsPicture(file, guid, picturetype);
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