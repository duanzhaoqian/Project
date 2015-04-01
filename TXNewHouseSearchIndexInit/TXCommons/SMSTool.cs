using System;
using System.Text;
using System.Net;
using System.IO;
namespace TXCommons
{
    public class SMSInfo
    {
        public int option { get; set; }
        public string Mobile { get; set; }
        public string Content { get; set; }
    }
    public class SMSOptionType
    {
        public static int RESEND_SMS = 0;
        public static int SEND_SMS = 1;
        public static int GET_BALANCE_0 = 2;
        public static int GET_BALANCE_1 = 3;
    }

    public class SMSTool
    {
        string[] values = null;
        /// <summary>
        /// 解密短信参数,短信平台调用
        /// 2010-12-15 邓亚军
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool parsePara(string content)
        {
            try
            {
                if (content == null || content.Trim().Equals(""))
                    return false;
                //解密
                content = DESEncrypt.Decrypt(content);
                values = content.Split(new char[] { '^' });

                //参数长度检查
                if (values.Length != 3)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 返回手机对象数据,短信平台调用
        /// 2010-12-15 邓亚军
        /// </summary>
        /// <returns></returns>
        public SMSInfo GetSMSInfo()
        {

            SMSInfo smsInfo = new SMSInfo();
            string option = values[0];//操作类型
            string mobile = values[1];//手机号
            string content = values[2];//内容

            try
            {
                smsInfo.option = int.Parse(option);
                smsInfo.Mobile = mobile;
                smsInfo.Content = content;
                return smsInfo;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 封装手机参数,客户端调用
        /// 2010-12-15 邓亚军
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="contente"></param>
        /// <returns></returns>
        public string PackMobileParas(int option,
                                    string mobile,
                                    string contente)
        {
            string paras = option + "^" + mobile + "^" + contente;
            paras = DESEncrypt.Encrypt(paras);
            return paras;
        }

        /// <summary>
        /// 发送短信(采用默认短信接口),客户端调用
        /// 2010-12-15 邓亚军
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string sendSms(int option, string mobile, string content)
        {
            return sendSms(option, GetConfig.SMSWebUrl, mobile, content);
        }
        /// <summary>
        /// 发送短信（制定短信接口）,客户端调用
        /// 2010-12-15 邓亚军
        /// </summary>
        /// <param name="smsWebUrl">短信接口地址</param>
        /// <param name="mobile">手机</param>
        /// <param name="content">短信内容</param>
        /// <returns></returns>
        public string sendSms(int option, string smsWebUrl, string mobile, string content)
        {
            try
            {
                SMSTool smsTool = new SMSTool();
                string cons = smsTool.PackMobileParas(option, mobile, content);
                return PostData(smsWebUrl, "contents=" + cons);
            }
            catch
            {
                return "0";
            }
        }
        /// <summary>
        /// post数据
        /// 2010-12-15 邓亚军
        /// </summary>
        /// <param name="purl"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private string PostData(string purl, string str)
        {
            Stream stream = null;
            HttpWebResponse rep = null;
            StreamReader readStream = null;
            Stream receiveStream = null;
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("utf-8").GetBytes(str);
                // 准备请求 
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(purl);

                //设置超时
                req.Timeout = 30000;
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                stream = req.GetRequestStream();
                // 发送数据 
                stream.Write(data, 0, data.Length);

                //接收数据
                rep = (HttpWebResponse)req.GetResponse();
                receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }
                return sb.ToString();

            }
            catch
            {
                return "0";
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (rep != null)
                    rep.Close();
                if (readStream != null)
                    readStream.Close();
                if (receiveStream != null)
                    receiveStream.Close();

            }
        }

    }
}
