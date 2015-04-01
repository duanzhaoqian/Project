using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace AOPTest
{
    /// <summary>
    /// 动态代理特性，利用ContextBoundObject
    /// </summary>
    class MyAOPProxyAttribute:ProxyAttribute
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            //创建对象实例
            MarshalByRefObject obj = base.CreateInstance(serverType);
            //创建真实代理
            MyRealProxy myRealProxy=new MyRealProxy(serverType,obj);
            //获取真实代理的透明代理（即对象实例）返回
            return (MarshalByRefObject)myRealProxy.GetTransparentProxy();
        }
    }
}
