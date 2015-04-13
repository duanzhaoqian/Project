using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ServiceStack.AOP
{
    class ProcesserWrapper:IProcess
    {
        private readonly IProcess _processer;

        private readonly IEnumerator<IAOPFilter> _filters;

        public ProcesserWrapper(IProcess process, IEnumerator<IAOPFilter> filters)
        {
            _processer = process;
            _filters = filters;
        }

        public Type Type { get { return _processer.Type; } }
        public object Instance { get { return _processer.Instance; } }
        public IMethodCallMessage MethodCallMessage { get { return _processer.MethodCallMessage; } }
        public IMethodReturnMessage MethodReturnMessage { get { return _processer.MethodReturnMessage; }}
        public void Process()
        {
            if (_filters.MoveNext())
            {
                _filters.Current.Execute(this);
            }
            else
            {
                _processer.Process();
            }
        }

        public void AttachReturnMessage(IMethodReturnMessage message)
        {
           _processer.AttachReturnMessage(message);
        }
    }
}
