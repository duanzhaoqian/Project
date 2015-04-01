using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using KYJ.ZS.Commons.Common;
using ServiceStack;

namespace KYJ.ZS.Commons
{
    public class Tool
    {
        #region 构造

        private static Tool _instance = new Tool();

        private Tool()
        {
        }

        public static Tool Instance { get { return _instance; } }

        #endregion

        #region Tool

        /// <summary>
        /// 获取随机名称
        /// </summary>
        /// <param name="num">位数</param>
        /// <returns>名称</returns>
        public string GetRandomFileName(int num)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < num; i++)
            {
                int index = random.Next(0, chars.Length);
                sb.Append(chars[index].ToString());
                //result += chars[index].ToString();
            }
            //return result;
            return sb.ToString();
        }
        /// <summary>
        /// 获取一个文件里文本内容
        /// </summary>
        /// <param name="FileFullPath">文件完整路径</param>
        /// <returns></returns>
        public string GetFileContent(string FileFullPath, Encoding en)
        {
            string Content = "";
            Encoding enCode = Encoding.UTF8;
            if (en != null)
            {
                enCode = en;
            }
            if (System.IO.File.Exists(FileFullPath))
            {
                StreamReader sr = new StreamReader(FileFullPath, enCode);
                Content = sr.ReadToEnd();
                sr.Close();
            }
            return Content;
        }

        /// <summary>
        /// 窦海超
        /// 2014年5月6日 15:44:33
        /// 生成日志
        /// </summary>
        /// <param name="flag">文字类型</param>
        /// <param name="msg">内容</param>
        /// <param name="Logpath">日志绝对目录</param>
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
        #endregion

        #region 订单编号

        /// <summary>
        /// Author:zhuzh
        /// Time:2014-04-23
        /// Desc:获取指定订单类型以当天时分秒为单位的订单号
        /// </summary>
        /// <param name="type">1 购物订单（租），2 购物订单（售），3 充值订单，4 提现订单</param>
        /// <returns></returns>
        public string GetOrderNumber(int type)
        {
            //获取订单（Redis）自增数，处理1秒内并发情况
            double number = KYJ.ZS.Commons.Common.RedisTool.GetNextSequence(FunctionType.ZuShouCar, 0, KeyManager.GetOrderNumberKey("ORDERMANTISSA"), DateTime.Now.AddSeconds(10));
            var result = string.Format("{0:1600}{1:0000000000}{2:00}", type, DateTime.Now.ToString("yyMMddHHmmss"), Auxiliary.ToInt32(number));
            return result;
        }

        #endregion
    }
}
