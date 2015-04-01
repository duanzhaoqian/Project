using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    /// <summary>
    /// 服务对象
    /// </summary>
    public class Server
    {
        private Socket _subSocket;
        private Socket _relSocket;
        /// <summary>
        /// 用于订阅套接字
        /// </summary>
        public Socket SubSocket
        {
            get
            {
                if (_subSocket == null)
                {
                    _subSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _subSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8899));
                }
                return _subSocket;
            }
        }
        /// <summary>
        /// 用于发布套接字
        /// </summary>
        public Socket RelSocket
        {
            get
            {
                if (_relSocket == null)
                {
                    _relSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _relSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8999));
                }
                return _relSocket;
            }
        }
    }
}
