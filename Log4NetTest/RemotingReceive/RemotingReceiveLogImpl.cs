using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace RemotingReceive
{
    public class RemotingReceiveLogImpl : MarshalByRefObject, IReceiveLog
    {
        public void LogEvents(log4net.Core.LoggingEvent[] events)
        {
            foreach (LoggingEvent loggingEvent in events)
            {
                string str = loggingEvent.GetLoggingEventData().Message;
                Console.WriteLine(str);
            }
        }
    }
}
