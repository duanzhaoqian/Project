using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MongoDal;
using MongoProcess;
using MongoService;
using Newtonsoft.Json;

namespace MongoImpl
{
    [JavascriptCallbackBehavior(UrlParameterName = "callback")]
    public class MongoHouseService : IMongoHouseService
    {
        MongoProcess.MongoDBProcess mongoDbProcess=new MongoDBProcess();
        private WebOperationContext wctx;
        public MongoHouseService()
        {
            wctx = WebOperationContext.Current;
            if (wctx != null)
            {
                string accept = wctx.IncomingRequest.Accept;
                if (!string.IsNullOrEmpty(accept) && accept.Contains("text/html"))
                {
                    wctx.OutgoingResponse.ContentType = "application/xml";
                }
            }
        }
        public Message Get(string id)
        {
            Dictionary<string, object> dictionary = mongoDbProcess.GetDataById(id);

            if (wctx != null)
            {
                string accept = wctx.IncomingRequest.Accept;
                if (!string.IsNullOrEmpty(accept) && accept.Contains("xml"))
                {
                    XElement xElement = new XElement("House");
                    DictionaryToXElement(dictionary, xElement);
                    Message xmlmessage = wctx.CreateXmlResponse(xElement);
                    return xmlmessage;
                }
            }
            // string strJson = JsonConvert.SerializeObject(dictionary);
            AssemblyName assemblyName = new AssemblyName("DynimacHouseModel");
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,
                AssemblyBuilderAccess.RunAndCollect);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName + ".dll");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("HouseModel", TypeAttributes.Public);
            foreach (KeyValuePair<string, object> keyValuePair in dictionary)
            {
                Type valueType = keyValuePair.Value == null ? typeof(object) :
                keyValuePair.Value.GetType();
                FieldBuilder _p1FieldBuilder = typeBuilder.DefineField("_" + keyValuePair.Key, valueType, FieldAttributes.Private);
                PropertyBuilder p1 = typeBuilder.DefineProperty(keyValuePair.Key, PropertyAttributes.HasDefault, valueType, null);
                MethodAttributes getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName |
                                                   MethodAttributes.HideBySig;
                MethodBuilder p1get = typeBuilder.DefineMethod("get_" + keyValuePair.Key, getSetAttributes, valueType, Type.EmptyTypes);

                ILGenerator ilP1get = p1get.GetILGenerator();
                ilP1get.Emit(OpCodes.Ldarg_0);
                ilP1get.Emit(OpCodes.Ldfld, _p1FieldBuilder);
                ilP1get.Emit(OpCodes.Ret);

                MethodBuilder p1set = typeBuilder.DefineMethod("set_" + keyValuePair.Key, getSetAttributes, null, new[] { valueType });
                ILGenerator ilP1Set = p1set.GetILGenerator();
                ilP1Set.Emit(OpCodes.Ldarg_0);
                ilP1Set.Emit(OpCodes.Ldarg_1);
                ilP1Set.Emit(OpCodes.Stfld, _p1FieldBuilder);
                ilP1Set.Emit(OpCodes.Ret);

                p1.SetSetMethod(p1set);
                p1.SetGetMethod(p1get);
            }

            Type type = typeBuilder.CreateType();

            var obj = Assembly.GetAssembly(type).CreateInstance(type.FullName);

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (PropertyInfo info in propertyInfos)
            {
                info.SetValue(obj, dictionary[info.Name], null);
            }

            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(type);
            wctx.OutgoingResponse.Headers.Add(HttpResponseHeader.ContentType, "application/json;charset=utf-8");
            Message jsonmessage = wctx.CreateJsonResponse(obj, dataContractJsonSerializer);
            return jsonmessage;
        }

        private static void DictionaryToXElement(Dictionary<string, object> dictionary, XElement xElement)
        {
            foreach (KeyValuePair<string, object> keyValuePair in dictionary)
            {
                object obj = keyValuePair.Value;
                if (keyValuePair.Value != null)
                {
                    if (keyValuePair.Value.GetType() == typeof(List<Dictionary<string, object>>))
                    {
                        List<Dictionary<string, object>> list = keyValuePair.Value as List<Dictionary<string, object>>;
                        List<XElement> listeElements = new List<XElement>();
                        foreach (Dictionary<string, object> dic in list)
                        {
                            XElement xxxElement =
                                new XElement(keyValuePair.Key);
                            DictionaryToXElement(dic, xxxElement);
                            listeElements.Add(xxxElement);
                        }
                        obj = listeElements;
                    }
                }
                xElement.Add(new XElement(keyValuePair.Key, obj));
            }
        }


        public bool Delete(string id)
        {
            return mongoDbProcess.Delete(id);
        }

        public bool UpSert(string id)
        {
            return mongoDbProcess.Upsert(id);
        }
    }
}
