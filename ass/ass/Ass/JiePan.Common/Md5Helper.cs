#region head
//  ┏┓　　　┏┓
// ┏┛┻━━━┛┻┓
// ┃　　　　　　　┃ 　
// ┃　　　━　　　┃
// ┃　┳┛　┗┳　┃
// ┃　　　　　　　┃
// ┃　　　┻　　　┃
// ┃　　　　　　　┃
// ┗━┓　　　┏━┛
//     ┃　　　┃   神兽保佑　　　　　　　　　
//     ┃　　　┃   代码无BUG！
//     ┃　　　┗━━━┓
//     ┃　　　　　　　┣┓
//     ┃　　　　　　　┏┛
//     ┗┓┓┏━┳┓┏┛
//       ┃┫┫　┃┫┫
//       ┗┻┛　┗┻┛
// 
// 
// Author:		lianjiepan
// Blog:		blog.jiepansoft.com
// Description:	md5助手类
// Copyirght:	Copyright (C) 2014 - CCINN All rights reserved
// Solution:	JiePan.CMS
// Project:		JiePan.Common
// File:		Md5Helper.cs
// CreateDate: 	2014-06-11 22:00
// ModifyDate:	2014-06-11 22:00
#endregion
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JiePan.Common
{
    public class Md5Helper
    {
        /// <summary>
        /// 获取字符穿的md5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetStringMd5Hash(string str)
        {
            StringBuilder sb = new StringBuilder();
            MD5 md5 = MD5.Create();
            //将要转换的字符串转换成字节数组
            byte[] bts = Encoding.Default.GetBytes(str);
            //将字节数组进行md5算法的处理
            bts = md5.ComputeHash(bts);
            //将转换后的每个字节转换成字符串并连接到StringBuilder里面
            for (int i = 0; i < bts.Length; i++)
            {
                sb.Append(bts[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取文件的md5值
        /// </summary>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public static string GetFileMd5Hash(string pathName)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            FileStream oFileStream = null;
            MD5CryptoServiceProvider oMD5Hasher = new MD5CryptoServiceProvider();
            try
            {
                oFileStream = new FileStream(pathName.Replace("\"", ""), FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
                arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream); //计算指定Stream 对象的哈希值
                oFileStream.Close();
                strHashData = BitConverter.ToString(arrbytHashValue);
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return strResult;
        }
        /// <summary>
        /// 获取流的md5
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static String GetStreamMD5Hash(Stream stream)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            arrbytHashValue = oMD5Hasher.ComputeHash(stream); //计算指定Stream 对象的哈希值
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = System.BitConverter.ToString(arrbytHashValue);
            //替换-
            strHashData = strHashData.Replace("-", "");
            strResult = strHashData;
            return strResult;
        }
        /// <summary>
        /// 获取字符串两次MD5后的结果
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String GetDoubleStringMd5Hash(string str)
        {
            return GetStringMd5Hash(GetStringMd5Hash(str));
        }
    }
}