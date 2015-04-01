using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MonitorClient.AppUtility
{
    public class Encrypt
    {
        private static string _mykey = "ising_99";//DateTime.Now.ToString("yyyyMMdd");//DateTime.Now.ToString("yyyyMMddHHmmssffff");
        public static string MyKey
        {
            get { return _mykey; }
            set
            {
                _mykey = value;
            }
        }
        public Encrypt()
        { }
        //public static Encrypt()
        //{
        //    MyKey = "ising_99";
        //}

        public Encrypt(string skey)
        {
            if (!string.IsNullOrEmpty(skey))
            {
                MyKey = skey;
            }
        }

        ///MD5加密
        ///
        //const string sKey = "lxw88888";
        /// <summary>
        /// 加密//public static string MD5Encrypt(string pToEncrypt, string sKey)
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string pToEncrypt, ref bool IsSuc)
        {
            try
            {
                string sKey = MyKey;// "is988888";
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                IsSuc = true;
                return ret.ToString();
            }
            catch (Exception ex)
            {
                IsSuc = false;
                return pToEncrypt;
            }
        }

        /// <summary>
        /// 解密//public static string MD5Decrypt(string pToDecrypt, string sKey)
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        public static string MD5Decrypt(string pToDecrypt, ref bool IsSuc)
        {
            try
            {
                string sKey = MyKey;// "is988888";

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                IsSuc = true;
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                IsSuc = false;
                return pToDecrypt;
            }
        }

        /// <summary>
        /// MD5(32位加密)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            try
            {
                MD5 md5 = MD5.Create();//实例化一个md5对像
                // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
                // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                for (int i = 0; i < s.Length; i++)
                {
                    // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

                    pwd = pwd + s[i].ToString("X").PadLeft(2, '0');

                }
                return pwd;
            }
            catch
            {
                return str;
            }
        }
        //public string getMd5(string md5)
        //{
        //    System.Security.Cryptography.MD5CryptoServiceProvider md = new System.Security.Cryptography.MD5CryptoServiceProvider();
        //    byte[] value, hash;
        //    value = System.Text.Encoding.UTF8.GetBytes(md5);
        //    hash = md.ComputeHash(value);
        //    md.Clear();
        //    string temp = "";
        //    for (int i = 0, len = hash.Length; i < len; i++)
        //    {
        //        temp += hash[i].ToString("x").PadLeft(2, '0');
        //    }
        //    return temp;
        //}



    }
}
