using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpClient.Connect("192.168.3.68", 8888);
            byte[] bytes = Encoding.UTF8.GetBytes("C#");
            //Thread thread=new Thread(() =>
            //{
            //    while (true)
            //    {
            //        int n = 0;
            //        byte[] bytesbuff = new byte[10];
            //        StringBuilder stringBuilder = new StringBuilder();
            //        while (n > -1)
            //        {
            //            n = tcpClient.Receive(bytesbuff, 0, bytesbuff.Length, SocketFlags.None);
            //            stringBuilder.Append(Encoding.UTF8.GetString(bytesbuff, 0, n));
            //        }
            //        Console.WriteLine(stringBuilder);
            //    }
            //});
            //thread.IsBackground = true;
            //thread.Start();
            tcpClient.Send(bytes, 0, bytes.Length, SocketFlags.None);
            //  tcpClient.Close();
            // Console.ReadKey();
            //tcpClient.BeginSend(bytes, 0, bytes.Length, 0, (c) => tcpClient.EndSend(c), null);

            //  tcpClient.Client.BeginSend(Encoding.UTF8.GetBytes("C#"),)

            //  tcpClient.LingerState.Enabled = false;
            // byte[] bytes = new byte[10];
            // IList<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
            // tcpClient.Client.Send(Encoding.UTF8.GetBytes("C#"));
            //     int n = 0;
            //while (n > -1)
            //{
            //    n = tcpClient.Client.Receive(list);
            //}

            //tcpClient.Shutdown(SocketShutdown.Both);
            //tcpClient.Dispose();
            //  tcpClient.Close();

            Thread.Sleep(10 * 1000);
            tcpClient.Close();
            Console.WriteLine("Close");
            Thread.Sleep(100 * 1000);

            //Console.ReadKey();
        }
    }
}
