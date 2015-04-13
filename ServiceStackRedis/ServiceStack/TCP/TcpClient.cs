using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServiceStack.TCP
{
    public class TcpClient
    {

        public TcpClient(string host, int port)
        {
            mSAEA.SetBuffer(new byte[1024 * 2], 0, 1024 * 2);
            mHost = host;
            mPort = port;
            DB = 0;
        }

        private string mHost;

        private int mPort;

        private bool mConnected = false;

        private System.Net.Sockets.Socket mSocket;

        private Exception mLastError;

        private System.Net.Sockets.SocketAsyncEventArgs mSAEA = new System.Net.Sockets.SocketAsyncEventArgs();

        public void DisConnect()
        {
            mConnected = false;
            try
            {
                if (mSocket != null)
                {
                    mSocket.Close();

                }
            }
            catch
            {
            }
            mSocket = null;
        }

        public Exception LastError
        {
            get
            {
                return mLastError;
            }
        }

        public System.Net.Sockets.Socket Socket
        {
            get
            {
                return mSocket;
            }

        }

        public bool Connected
        {
            get
            {
                return mConnected;
            }
        }

        public bool Connect()
        {
            if (mConnected)
                return true;
            IPAddress[] ips = Dns.GetHostAddresses(mHost);
            if (ips.Length == 0)
            {
                //Tool.WriteTextToFile("get host's IPAddress error");
                throw new Exception("get host's IPAddress error");
            }
            var address = ips[0];

            try
            {
                mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mSocket.ReceiveTimeout = 5000;
                mSocket.SendTimeout = 5000;
                mSocket.Connect(address, mPort);
                mConnected = true;
                return true;
            }
            catch (Exception e_)
            {
                DisConnect();
                mLastError = e_;
                return false;
            }


        }

        public TcpReceiveArgs Receive()
        {
            try
            {
                if (Socket == null)
                {
                    throw new Exception("Socket equals null client disconnect!");
                }
                int count = Socket.Receive(mSAEA.Buffer, System.Net.Sockets.SocketFlags.None);
                if (count == 0)
                {
                    throw new Exception(mHost + "count is  0 client disconnect!msaea:" + mSAEA.Buffer + "|buferend");
                }
                return new TcpReceiveArgs() { Client = this, Count = count, Data = mSAEA.Buffer, Offset = 0 };
            }
            catch (Exception e_)
            {
                DisConnect();
                mLastError = e_;
                throw e_;
            }
        }

        public bool Send(string value)
        {
            return Send(value, Encoding.UTF8);
        }

        public bool Send(string value, Encoding coding)
        {
            return Send(coding.GetBytes(value));
        }

        public bool Send(byte[] data)
        {
            return Send(data, 0, data.Length);
        }

        public bool Send(byte[] data, int offset, int count)
        {
            if (Connect())
            {
                try
                {

                    while (count > 0)
                    {
                        int sends = mSocket.Send(data, offset, count, SocketFlags.None);
                        count -= sends;
                        offset += sends;
                    }
                    return true;
                }
                catch (Exception e_)
                {
                    DisConnect();
                    mLastError = e_;
                    return false;
                }
            }
            return false;

        }

        public bool Send(ArraySegment<byte> data)
        {
            return Send(data.Array, data.Offset, data.Count);

        }

        public int DB
        {
            get;
            set;
        }

        internal static Result Send(Command cmd, TcpClient client)
        {
            byte[] sdata = BufferPool.Single.Pop();
            int count = 0;
            try
            {
                count = cmd.toData(sdata);

                if (!client.Send(sdata, 0, count))
                {
                    throw new Exception("client disconnect!");
                }
            }
            finally
            {
                BufferPool.Single.Push(sdata);
            }
            Result result = new Result();

            try
            {
                while (true)
                {
                    TcpReceiveArgs res = client.Receive();

                    if (result.Import(res.Data, res.Offset, res.Count))
                    {
                        break;
                    }
                }
            }
            catch (Exception e_)
            {
                string data = System.Text.Encoding.Default.GetString(sdata);
                data = data.Substring(0, data.IndexOf("\r\n\0"));
                Tool.WriteTextToFile("Error啦:" + e_.StackTrace + "|message:" + e_.Message + "|data:" + data.ToString() + "|");
                result.Dispose();
                Exception err = e_;
                //throw e_;
            }
            return result;
        }

    }
}
