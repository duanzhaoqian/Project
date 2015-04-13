using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using ServiceStack.AOP;

namespace ServiceStack.Log
{
    public class LogMailExceptionAttribute : AOPFilterAttribute
    {
        public LogMailExceptionAttribute(string name, string content)
        {
            Name = name;
            Content = content;
        }
        public string Name { get; set; }
        public string Content { get; set; }
        ILog log = LoggerFactory.GetLog("maillog");
        public override void Execute(IProcess processer)
        {
            try
            {
                ExecuteBefore(processer);
            }
            catch (Exception exception)
            {
                log.LogError("", "LogMailExceptionAttribute", exception);
            }
            try
            {
                processer.Process();

            }
            catch (Exception exception)
            {
                LoggerFactory.LogAllException(Content, Name, exception.InnerException);
            }
            try
            {
                ExecuteAfter(processer);

            }
            catch (Exception exception)
            {
                log.LogError("", "LogMailExceptionAttribute", exception);
            }
        }

        public override void ExecuteBefore(IProcess process)
        {

        }

        public override void ExecuteAfter(IProcess process)
        {
        }
    }
}
