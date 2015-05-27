using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpReceive
{
    class Program
    {
        static void Main(string[] args)
        {

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8889);
            UdpClient udpClient = new UdpClient(ipEndPoint);
            while (true)
            {
                byte[] bytes = udpClient.Receive(ref ipEndPoint);
                string result = Encoding.UTF8.GetString(bytes);
                Console.WriteLine(result);
            }
            Console.ReadKey();
        }
    }
}
