using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ServiceStack.AOP
{
    class Processer : IProcess
    {
        public Processer(Type type, object instance, IMethodCallMessage methodCallMessage)
        {
            Type = type;
            Instance = instance;
            MethodCallMessage = methodCallMessage;
        }
        public Type Type { get; private set; }
        public object Instance { get; private set; }
        public IMethodCallMessage MethodCallMessage { get; private set; }
        private IMethodReturnMessage _methodReturnMessage;
        public IMethodReturnMessage MethodReturnMessage
        {
            get
            {
                return _methodReturnMessage ?? new ReturnMessage(null, MethodCallMessage); 
            }
            private set { _methodReturnMessage = value; }
        }
        public void Process()
        {
            var obj = MethodCallMessage.MethodBase.Invoke(Instance, MethodCallMessage.Args);
            MethodReturnMessage = new ReturnMessage(obj, MethodCallMessage.Args, MethodCallMessage.ArgCount, MethodCallMessage.LogicalCallContext, MethodCallMessage);
        }

        public void AttachReturnMessage(IMethodReturnMessage message)
        {
            MethodReturnMessage = message;
        }
    }
}
