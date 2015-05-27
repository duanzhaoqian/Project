using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using log4net.Appender;
using log4net.Core;

namespace TestRemotingReceive
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClientChannel tcpClientChannel = new TcpClientChannel();
            ChannelServices.RegisterChannel(tcpClientChannel);
            RemotingAppender.IRemoteLoggingSink iRemoteLoggingSink =
                (RemotingAppender.IRemoteLoggingSink)
                    Activator.GetObject(typeof(RemotingAppender.IRemoteLoggingSink), "tcp://localhost:8888/RemotingReceiveLog");
            iRemoteLoggingSink.LogEvents(new LoggingEvent[]{new LoggingEvent(new LoggingEventData(){Message = "hello"})});
        }
    }
}
