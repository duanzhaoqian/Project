using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web.Script.Serialization;
using ServiceStack.AOP;

namespace ServiceStack.Log
{
    public class LogFileAttribute : AOPFilterAttribute
    {
        private List<ILog> logs = new List<ILog>();
        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="name">使用都名称</param>
        /// <param name="content">自定义标识</param>
        /// <param name="types">记录类（FileLogger或者MailLogger）例如：new Type[]{typeof(FileLogger)}</param>
        /// <param name="args">对应type[]中类型的构造方法的参数，要和type[]参数对应。例如：FileLogger的构造方法无参,传入null</param>
        public LogFileAttribute(string name, string content, Type[] types,params object[] args )
        {
            Name = name;
            Content = content;
            for (int i = 0; i < types.Count(); i++)
            {
                ILog log = types[i].Assembly.CreateInstance(types[i].FullName, true, BindingFlags.Default, null, args == null ? null : args[i] as object[], null, null) as ILog;
                if (log != null)
                {
                    logs.Add(log);
                }
            }
        }
        public string Name { get; set; }
        public string Content { get; set; }
       // ILog Log = LoggerFactory.GetLog("filelog"); 
        Stopwatch stopwatch = new Stopwatch();
        public override void Execute(IProcess processer)
        {
            try
            {
                ExecuteBefore(processer);
            }
            catch (Exception exception)
            {
                logs.ForEach(c => c.LogError("", "LogFileAttribute", exception));
            }
            try
            {

                processer.Process();

            }
            catch (Exception exception)
            {
                StringBuilder strArgs = new StringBuilder();
                int i = processer.MethodCallMessage.ArgCount;
                if (i > 0)
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    for (int j = 0; j < i; j++)
                    {
                        Delegate dDelegate = processer.MethodCallMessage.GetArg(j) as Delegate;
                        if (dDelegate == null)
                        {
                            strArgs.AppendFormat("参数名：{0}，参数值：{1}  ", processer.MethodCallMessage.GetArgName(j),
   javaScriptSerializer.Serialize(processer.MethodCallMessage.GetArg(j)));
                        }
                        else
                        {
                            strArgs.AppendFormat("参数名：{0}，参数值：{1}  ", processer.MethodCallMessage.GetArgName(j), dDelegate.Method);
                        }
                    }
                }
                logs.ForEach(c => c.LogError(Content + (!string.IsNullOrEmpty(strArgs.ToString()) ? Environment.NewLine + "传入参数：" + strArgs.ToString() : ""), Name, exception.InnerException));
                //LoggerFactory.LogAllException(Content, Name + (!string.IsNullOrEmpty(strArgs.ToString()) ? Environment.NewLine + "传入参数：" + strArgs.ToString() : ""), exception.InnerException);
            }
            try
            {
                ExecuteAfter(processer);

            }
            catch (Exception exception)
            {
                logs.ForEach(c => c.LogError("", "LogFileAttribute", exception));
            }
        }

        public override void ExecuteBefore(IProcess process)
        {
            StringBuilder strArgs = new StringBuilder();
            int i = process.MethodCallMessage.ArgCount;
            if (i > 0)
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                for (int j = 0; j < i; j++)
                {
                    Delegate dDelegate = process.MethodCallMessage.GetArg(j) as Delegate;
                    if (dDelegate == null)
                    {
                        strArgs.AppendFormat("参数名：{0}，参数值：{1}  ", process.MethodCallMessage.GetArgName(j),
   javaScriptSerializer.Serialize(process.MethodCallMessage.GetArg(j)));
                    }
                    else
                    {
                        strArgs.AppendFormat("参数名：{0}，参数值：{1}  ", process.MethodCallMessage.GetArgName(j), dDelegate.Method);
                    }

                }
            }
            logs.ForEach(c => c.Log(string.Format("{0}方法开始执行{2}Content：{1}{2}传入参数：{3}", process.MethodCallMessage.MethodName, Content, Environment.NewLine, strArgs), Name));
            stopwatch.Restart();
        }

        public override void ExecuteAfter(IProcess process)
        {
            stopwatch.Stop();
            logs.ForEach(c => c.Log(string.Format("{0}方法执行完成，消耗时间：{3}{2}Content：{1}", process.MethodCallMessage.MethodName, Content, Environment.NewLine, stopwatch.Elapsed), Name));
        }
    }
}
