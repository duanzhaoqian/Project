using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;

namespace RemotingReceive
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            //// Log an info level message
            //if (log.IsInfoEnabled) log.Info("Application [RemotingServer] Start");

            //// Configure remoting. This loads the TCP channel as specified in the .config file.
            //RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            //// Publish the remote logging server. This is done using the log4net plugin.
            ////log4net.LogManager.GetRepository().PluginMap.Add(new log4net.Plugin.RemoteLoggingServerPlugin("RemotingReceiveLog"));

            //// Wait for the user to exit
            //Console.WriteLine("Press 0 and ENTER to Exit");
            //String keyState = "";
            //while (String.Compare(keyState, "0", true) != 0)
            //{
            //    keyState = Console.ReadLine();
            //}

            //// Log an info level message
            //if (log.IsInfoEnabled) log.Info("Application [RemotingServer] End");

            TcpServerChannel tcpServerChannel = new TcpServerChannel(8888);
            ChannelServices.RegisterChannel(tcpServerChannel,false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemotingReceiveLogImpl), "RemotingReceiveLog", WellKnownObjectMode.SingleCall);
       
            Console.WriteLine("Ok");
            Console.ReadKey();
        }
    }
}
