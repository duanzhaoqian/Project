using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using Data;

namespace TestSend
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("开始订阅");
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8899);
            socket.Connect(endPoint);
            SubData subData = new SubData();
            subData.Type = "Sub";
            subData.SubStrings = new string[] { "a", "b", "c" };
            socket.Send(Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(subData)));

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    byte[] bytes = new byte[128 * 1024];
                    int i = socket.Receive(bytes);
                    string str = Encoding.UTF8.GetString(bytes, 0, i).Trim();
                    //Console.WriteLine(str);
                }
            });
            thread.IsBackground = true;
            thread.Start();

            //Console.WriteLine("Begin Replease");
            
            
            //for (int i = 0; i < 100000; i++)
            //{
            //    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    EndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8999);
            //    socket.Connect(endPoint);
            //    RelData relData = new RelData() { Key = "a", Value = "6465465" };
            //    socket.Send(Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(relData)));

            //}
            //Console.WriteLine("Replease OK");

            Console.WriteLine("订阅成功");
            Console.ReadKey();
        }
    }
}
