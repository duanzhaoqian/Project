using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.Log
{
    class FileAndMailLog : LogBase, ILog
    {
        private FileLog fileLog = null;
        private MailLog mailLog = null;
        public FileAndMailLog(string name)
            : base(name)
        {
            fileLog=new FileLog(name);
            mailLog=new MailLog(name);
        }

        public void Log(string content, string name)
        {
            fileLog.Log(content,name);
            mailLog.Log(content,name);
        }

        public void LogError(string content, string name, Exception exception)
        {
           
            fileLog.LogError(content,name,exception);
            mailLog.LogError(content,name,exception);
        }
    }
}
