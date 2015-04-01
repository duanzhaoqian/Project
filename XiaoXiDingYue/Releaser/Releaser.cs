using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Data;
using DicManager;
using TimeTest;

namespace Releaser
{
    /// <summary>
    /// 消息发布者
    /// </summary>
    public class Releaser
    {
        /// <summary>
        /// 连接套接字
        /// </summary>
        public Socket RelSocket { get; set; }
        /// <summary>
        /// 发布消息方法
        /// </summary>
        /// <param name="data"></param>
        public void RelMsg(RelData data)
        {
            //开始记时
            Time.Stopwatch.Start();

            if (SubInfo.SubInfoDictionary.ContainsKey(data.Key))
            {
                if (SubInfo.SubInfoDictionary[data.Key] != null)
                {
                    try
                    {
                        for (int j = 0; j < 500000; j++)
                        {
                            SubInfo.SubInfoDictionary[data.Key].Invoke(data);
                        }
                    }
                    catch (ExceptionExtison.ExceptionExtison exceptionExtison)
                    {
                        //捕捉从Client抛出的对象，从字典中移出
                        Client.Client client = exceptionExtison.ExClient as Client.Client;
                        SubInfo.SubInfoDictionary[data.Key] -= client.SendMsg;
                        client.ClientSocket.Close();
                    }

                    Test();
                }
            }
            //发送完消息关闭连接
            RelSocket.Close();
        }
        private static int i = 1;
        /// <summary>
        /// 测试发送
        /// </summary>
        private void Test()
        {

            Time.Stopwatch.Stop();
            Console.WriteLine(Time.Stopwatch.Elapsed);
            Console.WriteLine(i);

            i++;

        }
    }
}
