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

namespace ServiceStack.AOP
{
     class AOPProxy : RealProxy
    {
        private Type _instanceType;
        private object _instance;

        public AOPProxy(Type instanceType, object instance):base(instanceType)
        {
            _instanceType = instanceType;
            _instance = instance;
        }
        public override IMessage Invoke(IMessage msg)
        {
            IConstructionCallMessage constructionCallMessage = msg as IConstructionCallMessage;
            IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
            if (constructionCallMessage != null)
            {
                var proxy = RemotingServices.GetRealProxy(_instance);
                proxy.InitializeServerObject(constructionCallMessage);
                return EnterpriseServicesHelper.CreateConstructionReturnMessage(constructionCallMessage,
                    (MarshalByRefObject)base.GetTransparentProxy());
            }
            Processer processer = new Processer(_instanceType, _instance, methodCallMessage);
            if (methodCallMessage != null)
            {
                #region Get Attributes

                var allFilters = new List<IAOPFilter>();
                //获取类方法上的Attributes
                var methodFilters = GetFilterAttributes(methodCallMessage.MethodName, methodCallMessage.MethodSignature).ToList();

                //获取类上的
                var clsAttrs = _instance.GetType().GetCustomAttributes(typeof(IAOPFilter), false).Cast<IAOPFilter>();
                allFilters.InsertRange(0, clsAttrs);
                if (_instanceType != null && _instance.GetType() != _instanceType)
                {
                    //获取接口方法上的Attributes
                    var interMethodAttributes = methodCallMessage.MethodBase.GetCustomAttributes(typeof(IAOPFilter), false)
                        .Cast<IAOPFilter>();
                    //获取接口上的
                    var interfaceAttributes = _instanceType.GetCustomAttributes(typeof(IAOPFilter), false).Cast<IAOPFilter>();


                    allFilters.AddRange(interMethodAttributes.Reverse());
                    allFilters.InsertRange(0, interfaceAttributes);
                }
                allFilters.AddRange(methodFilters);

                #endregion

                new AOPFilterContainer(allFilters).Execute(processer);

            }
            return processer.MethodReturnMessage;
        }

        /// <summary>
        /// 获取Filter，顺序：接口的定义上》实例的类定义上》接口的方法上》实例的方法上
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        IEnumerable<IAOPFilter> GetFilterAttributes(string methodName, object paramTypes)
        {
            var instanceType = _instance.GetType();
            var memInfo = instanceType.GetMethod(methodName,BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.IgnoreCase|BindingFlags.Instance,null, (Type[])paramTypes,null);
            var attrs = memInfo.GetCustomAttributes(typeof(IAOPFilter), false);
            return attrs.Cast<IAOPFilter>();
        }
    }
}
