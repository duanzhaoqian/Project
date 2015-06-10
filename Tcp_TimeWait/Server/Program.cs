using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 8888));
            socket.Listen(10);

            Thread thread1 = new Thread(() =>
            {
                while (true)
                {
                    Socket socket1 = null;
                    //try
                    //{
                    socket1 = socket.Accept();
                    Thread thread = new Thread(() =>
                    {
                        Socket socket2 = socket1;
                        //try
                        //{
                        byte[] bytes = new byte[10];
                        //int n = 0;
                        while (true)
                        {
                            try
                            {
                                int n = socket2.Receive(bytes, 0, bytes.Length, SocketFlags.None);
                                string str = Encoding.UTF8.GetString(bytes, 0, n);
                                if (!string.IsNullOrEmpty(str))
                                {
                                    Console.WriteLine(str);
                                }

                                socket2.Send(Encoding.UTF8.GetBytes("Server " + str));
                            }
                            catch (Exception exception)
                            {
                                socket2.Close();
                                Console.WriteLine("Receive");
                                //  Console.WriteLine(exception);
                                break;
                            }

                        }
                        //while (true)
                        //{
                        //    Thread.Sleep(10000);
                        //}
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine("socket2");
                        //}
                        //finally
                        //{
                        //    socket2.Close();
                        //}
                    });
                    thread.IsBackground = true;
                    thread.Start();

                    //}
                    //catch (Exception exception)
                    //{
                    //    Console.WriteLine("socket1");
                    //}
                    //finally
                    //{
                    //    if (socket1 != null) socket1.Close();
                    //}
                }

            });
            thread1.IsBackground = true;
            thread1.Start();
            Console.ReadKey();
        }
    }
}
