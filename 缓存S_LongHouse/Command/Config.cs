using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Command
{
    public class Config
    {
        /// <summary>
        /// S_LongHouse_Cache连接字符串
        /// </summary>
        public static string S_LongHouse_Cache_ConnectionString = ConfigurationManager.ConnectionStrings["S_LongHouse_Cache_ConnectionString"].ConnectionString;
        /// <summary>
        /// SourceLongHouse连接字符串
        /// </summary>
        public static string SourceLongHouse_ConnectionString = ConfigurationManager.ConnectionStrings["SourceLongHouse_ConnectionString"].ConnectionString;
        /// <summary>
        /// SqlDataAdapter超时时间
        /// </summary>
        public static string SqlDataAdapterTimeOut = ConfigurationManager.AppSettings["SqlDataAdapterTimeOut"];
        /// <summary>
        /// SqlBulkCopy超时时间
        /// </summary>
        public static string SqlBulkCopyTimeOut = ConfigurationManager.AppSettings["SqlBulkCopyTimeOut"];
        /// <summary>
        /// SqlBulkCopy批次执行
        /// </summary>
        public static string SqlBulkCopyBatchSize = ConfigurationManager.AppSettings["SqlBulkCopyBatchSize"];
        /// <summary>
        /// S_LongHouse表名
        /// </summary>
        public static string S_LongHouse_TableName = ConfigurationManager.AppSettings["S_LongHouse"];
        /// <summary>
        /// S_LongHouseCache表名
        /// </summary>
        public static string S_LongHouseCache_TableName = ConfigurationManager.AppSettings["S_LongHouseCache"];
        /// <summary>
        /// MQIP地址
        /// </summary>
        public static string MQIPAddress = ConfigurationManager.AppSettings["MQIPAddress"];
        /// <summary>
        /// MQ连接超时时间
        /// </summary>
        public static string MQConnectionTimeOut = ConfigurationManager.AppSettings["MQConnectionTimeOut"];
        /// <summary>
        /// MQ重试次数
        /// </summary>
        public static string MQRetryCount = ConfigurationManager.AppSettings["MQRetryCount"];
        /// <summary>
        /// MQ名称
        /// </summary>
        public static string MQName = ConfigurationManager.AppSettings["MQName"];

    }
}
