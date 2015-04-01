using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using Data;
using DicManager;

namespace Module
{
    /// <summary>
    /// 执行流程对象，初始化Server，处理消息
    /// </summary>
    public class Module
    {
        Server.Server server = new Server.Server();
        private static double subNum = 1;
        /// <summary>
        /// 启动服务
        /// </summary>
        public void ServerStart()
        {
            server.SubSocket.Listen(10000);
            server.RelSocket.Listen(1);
        }
        /// <summary>
        /// 处理接收的数据
        /// </summary>
        public void ProcessRequest()
        {

            ProcessSub();
            ProcessRel();
        }
        /// <summary>
        /// 处理发布方法
        /// </summary>
        private void ProcessRel()
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Socket socket = server.RelSocket.Accept();

                    Thread th = new Thread(() =>
                    {

                        Releaser.Releaser releaser = new Releaser.Releaser();
                        releaser.RelSocket = socket;
                        byte[] bytes = new byte[128 * 1024];
                        int i = socket.Receive(bytes);
                        string strRel = Encoding.UTF8.GetString(bytes, 0, i).Trim();

                        Console.WriteLine("发布消息：" + strRel + Environment.NewLine);
                        RelData relData = new JavaScriptSerializer().Deserialize<RelData>(strRel);

                        releaser.RelMsg(relData);


                    });
                    th.IsBackground = true;
                    th.Start();
                }

            });
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 处理订阅方法
        /// </summary>
        private void ProcessSub()
        {
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Socket socket = server.SubSocket.Accept();
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    Thread th = new Thread(() =>
                    {

                        Client.Client client = new Client.Client();
                        client.ClientSocket = socket;
                        byte[] bytes = new byte[128 * 1024];
                        int i = socket.Receive(bytes);
                        string strSub = Encoding.UTF8.GetString(bytes, 0, i).Trim();

                        Console.WriteLine("订阅消息：" + strSub + Environment.NewLine + subNum);
                        subNum++;
                        SubData subData = javaScriptSerializer.Deserialize<SubData>(strSub);
                        foreach (var str in subData.SubStrings)
                        {
                            if (SubInfo.SubInfoDictionary.ContainsKey(str))
                            {
                                SubInfo.SubInfoDictionary[str] += client.SendMsg;
                            }
                            else
                            {
                                SubInfo.SubInfoDictionary.Add(str, client.SendMsg);
                            }
                        }

                    });
                    th.IsBackground = true;
                    th.Start();
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
