using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Appender;

namespace RemotingReceive
{
    public interface IReceiveLog : RemotingAppender.IRemoteLoggingSink
    {
    }
}
