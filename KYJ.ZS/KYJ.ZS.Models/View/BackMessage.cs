using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.View
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年5月7日13:58:03
    /// 描述：用于在ajax请求中向客户端返回数据的实体
    /// </summary>
    public enum BackAction
    {
        /// <summary>
        /// 默认的
        /// </summary>
        Default=0,
        /// <summary>
        /// 重定向
        /// </summary>
        Redirect=1
    }

    public class BackMessge
    {
        public object Tag { get; set; }
        public BackMessge()
        {
            success = false;
            Action = BackAction.Default;
        }
        private bool success;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success
        {
            get { return success; }
            set { success = value; }
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        /// <summary>
        /// 重定向的地址
        /// </summary>
        public string StrUrl
        {
            get { return strUrl; }
            set { strUrl = value; }
        }
        /// <summary>
        /// 动作
        /// </summary>
        public BackAction Action
        {
            get { return action; }
            set { action = value; }
        }
        private BackAction action;
        private string strUrl;
        private string message;
    }
}
