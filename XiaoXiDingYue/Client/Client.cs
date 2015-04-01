using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;
using Data;
using DicManager;


namespace Client
{
    /// <summary>
    /// 客户端类
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 连接套接字
        /// </summary>
        public Socket ClientSocket { get; set; }
        /// <summary>
        /// 发送发布的消息
        /// </summary>
        /// <param name="data"></param>
        public void SendMsg(RelData data)
        {
            try
            {
                //序列化为Json发布
                byte[] sendBytes = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(data));
                ClientSocket.Send(sendBytes);
            }
            catch
            {
                //异常抛到Repleaser对象的RelMsg()方法中
                throw new ExceptionExtison.ExceptionExtison() { ExClient = this };
            }
        }
    }
}
