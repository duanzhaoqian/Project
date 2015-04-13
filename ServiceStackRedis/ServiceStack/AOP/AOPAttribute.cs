using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace ServiceStack.AOP
{
    class AOPAttribute : ProxyAttribute
    {
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            MarshalByRefObject marshalByRefObject = base.CreateInstance(serverType);
            AOPProxy aopProxy=new AOPProxy(serverType,marshalByRefObject);
            return (MarshalByRefObject)aopProxy.GetTransparentProxy();
        }
    }
}
