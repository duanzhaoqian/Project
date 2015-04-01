using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;

namespace AOPTest
{
    class MyRealProxy : RealProxy
    {
        //对放对象的类型
        private readonly Type _instanceType;
        //存放对象实例的透明代理
        private readonly object _instance;
        public MyRealProxy(Type type, object obj)
            : base(type)
        {
            _instanceType = type;
            _instance = obj;
        }
        /// <summary>
        /// 每次方法执行
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            IMethodCallMessage callMessage = msg as IMethodCallMessage;
            //构造方法
            IConstructionCallMessage constructionCallMessage = msg as IConstructionCallMessage;
            if (constructionCallMessage != null)
            {
                var proxy = RemotingServices.GetRealProxy(_instance);
                proxy.InitializeServerObject(constructionCallMessage);
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(constructionCallMessage,
                    (MarshalByRefObject)base.GetTransparentProxy());
            }

            MethodInfo methodInfo = _instance.GetType().GetMethod(callMessage.MethodName);
            //Attribute[] attributes = Attribute.GetCustomAttributes(methodInfo);
            Console.WriteLine("Method  1");
            object returnobj = new object();
            try
            {
                returnobj = methodInfo.Invoke(_instance, callMessage.Args);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.InnerException.Message+Environment.NewLine+exception.InnerException.StackTrace);

            }
            Console.WriteLine("method 2");
            return new ReturnMessage(returnobj, callMessage.Args, callMessage.ArgCount, callMessage.LogicalCallContext, callMessage);
        }
    }
}
