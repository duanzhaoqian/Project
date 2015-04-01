using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Factory;
using IService;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace TXImageService
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //获取参数
            string picType = context.Request["picturetype"];
            string guid = context.Request["guid"];
            int maxNum;
            int.TryParse(context.Request["maxNum"].ToString(), out maxNum);
            HttpPostedFile file= context.Request.Files[0];
            byte[] bytes =new byte[file.InputStream.Length];
            Stream stream = file.InputStream;
            string fileName = file.FileName;
            //string fileExtension = Path.GetExtension(fileName);
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            stream.Dispose();
            IProcessPic processPic = ObjectFactory.GetTempProcessPic();
            object entity = processPic.ProcessPic(guid, picType, bytes, fileName);//返回图片实体的信息包含path
            context.Response.Write(ConvertObjectToJson(entity));
        }
        /// <summary>
        /// 序列化成Json
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string ConvertObjectToJson(object o)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
            setting.Converters.Add(new IsoDateTimeConverter()
            {
                DateTimeFormat = "yyyy'-'MM'-'dd HH:mm:ss"

            });
            string Json = JsonConvert.SerializeObject(o, Formatting.None, setting);
            return Json;
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