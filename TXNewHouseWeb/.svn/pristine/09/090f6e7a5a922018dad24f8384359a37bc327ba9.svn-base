using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TXCommons.PictureModular;
using ServiceStack;
using System.IO;
using System.Configuration;

namespace TXImage
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";

                HttpPostedFile file = context.Request.Files["Filedata"];

                string para = context.Request.Params["para"];
                string guid = string.Empty;
                string picturetype = string.Empty;
                int maxnum = 0;
                int cityId = 0;
                if (para.IndexOf("^") > -1)
                {
                    guid = para.Split(new char[] { '^' })[0];
                    picturetype = para.Split(new char[] { '^' })[1];
                    maxnum = int.Parse(para.Split(new char[] { '^' })[2]);
                    cityId = int.Parse(para.Split(new char[] { '^' })[3]);
                }
                else
                {
                    guid = para.Split(new char[] { '_' })[0];
                    picturetype = para.Split(new char[] { '_' })[1];
                    maxnum = int.Parse(para.Split(new char[] { '_' })[2]);
                    cityId = int.Parse(para.Split(new char[] { '_' })[3]);
                }
                if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(picturetype))
                {
                    string result = string.Empty;
                    string key = KeyManager.GetPremisesPictureKey(guid, picturetype.ToUpper());
                    //int num = Redis.GetSortedSetCount<PremisesPictureInfo>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                    //if (num < maxnum)
                    //{
                    result = UploadPicture.SavePremisesPicture(file, guid, picturetype, cityId);

                    if (!string.IsNullOrEmpty(result))
                    {
                        context.Response.Write(result);
                    }
                    else
                    {
                        context.Response.Write("error");
                    }
                    //}
                    //else
                    //{
                    //    context.Response.Write("exceedmaxnum");
                    //}
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            catch(Exception ex)
            {

                Log("图片上传错误", ex.Message, ConfigurationManager.AppSettings["LogPath"].ToString());
            }
           
        }
        public static void Log(string flag, string msg, string Logpath)
        {
            try
            {
                string strPath = Logpath + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + ".txt";
                FileStream fs = new FileStream(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(flag + ":  " + msg + " :" + DateTime.Now.ToString() + "\n");
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch { }
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