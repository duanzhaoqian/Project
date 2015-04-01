using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace KYJ.ZS.Commons.Xml
{
    public class ComObjOrXml
    {
        /// <summary> 
        /// 反序列化 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="xml">XML字符串</param> 
        /// <returns></returns> 
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (var sr = new StringReader(xml))
                {
                    var xmldes = new XmlSerializer(type);

                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                //Log4netService.RecordLog.RecordException("反序列化", xml, e); return null;
                return null;
            }
        }

        /// <summary> 
        /// 序列化XML文件 
        /// </summary> 
        /// <param name="type">类型</param> 
        /// <param name="obj">对象</param> 
        /// <returns></returns> 
        public static string Serializer(Type type, object obj)
        {
            var stream = new MemoryStream();
            //创建序列化对象 
            var xml = new XmlSerializer(type);
            try
            {
                //序列化对象 
                xml.Serialize(stream, obj);
            }
            catch (InvalidOperationException e)
            {
                
            }
            stream.Position = 0;
            var sr = new StreamReader(stream);
            string str = sr.ReadToEnd();
            return str;
        }

        public static string SerializeWrite<T>(T instance)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));


            MemoryStream st = new System.IO.MemoryStream();

            serializer.WriteObject(st, instance);
            byte[] array = st.ToArray();
            st.Close();
            string _serializeString = Encoding.UTF8.GetString(array, 0, array.Length);
            return _serializeString;

        }

        public static object SerializeRead<T>(string url)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));

            XmlReader xml = XmlReader.Create(url);
            var o = serializer.ReadObject(xml);
            xml.Close();
            xml = null;

            return o;


        }

        //json 序列化  

        public static string ToJsJson(object item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        //反序列化  

        public static T FromJsonTo<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                T jsonObject = (T)ser.ReadObject(ms);
                return jsonObject;
            }
        }

        public static string ToJson<T>(object customer)
        {

            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();

            ds.WriteObject(ms, customer);

            string strReturn = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return strReturn;
        }

        public static object FromJson<T>(string strJson)
        {
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(strJson));

            return ds.ReadObject(ms);
        }
    }
}
