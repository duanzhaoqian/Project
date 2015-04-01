using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/5/7 10:51:57
    /// 描述：序列化、反序列化
    /// </summary>
    public static class SerializeHelper
    {
        #region xml

        public static string ToSimpleXml(this object o)
        {
            if (o == null)
                return null;

            var otype = o.GetType();

            string root = otype.IsGenericType ? otype.GetGenericArguments().First().Name : o.GetType().Name;

            if (root.Contains("AnonymousType"))
                root = null;

            return o.ToSimpleXml(root);
        }

        public static string ToSimpleXml(this object o, string root)
        {
            if (o == null)
                return null;

            var name = root;
            if (name.IsNullOrEmpty())
                name = "object";
            StringBuilder
                sbXml = new StringBuilder();

            sbXml.Append("<{0} type=\"{1}\">".FormatString(name, o.GetType().IsGenericType ? "Dynamic" : o.GetType().Name));

            var props = o.GetType().GetProperties();
            foreach (var p in props)
            {
                if (!p.CanRead)
                    continue;

                object value = p.GetValue(o, null);
                string v = value == null ? "" : System.Web.HttpContext.Current.Server.HtmlEncode(value.ToString());

                var ptypeName = p.PropertyType.IsGenericType ? p.PropertyType.GetGenericArguments().First().Name : (p.PropertyType.Name.Contains("AnonymousType") ? "Dynamic" : p.PropertyType.Name);

                sbXml.Append("<{0} type=\"{2}\">{1}</{0}>".FormatString(p.Name, v, ptypeName));
            }

            sbXml.Append("</{0}>".FormatString(name));
            var xml = sbXml.ToString();

            return xml;
        }

        #region

        #endregion
        /// <summary>
        /// 序列化对象,类型默认来自obj.GetType(),默认utf-8编码
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToXml(this object obj)
        {
            return ToXml(obj, new UTF8Encoding(false));
        }

        /// <summary>
        /// 序列化对象，类型默认来自obj.GetType()
        /// </summary>
        /// <param name="t">对象</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToXml(this object obj, Encoding encoding)
        {
            return ToXml(obj, obj.GetType(), encoding);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="type">指定对象类型名称</param>
        /// <returns></returns>
        public static string ToXml<T>(this object obj, string type)
        {
            return ToXml(obj, Type.GetType(type));
        }

        /// <summary>
        /// 序列化对象，默认utf-8编码
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">指定对象类型</param>
        /// <returns></returns>
        public static string ToXml(this object obj, Type type)
        {
            return ToXml(obj, type, new UTF8Encoding(false));
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">指定对象类型</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string ToXml(this object obj, Type type, Encoding encoding)
        {
            if (obj == null)
                return "";

            XmlSerializer
                xs = new XmlSerializer(type);
            XmlWriterSettings
                settings = new XmlWriterSettings
                {
                    Indent = true,
                    Encoding = encoding
                };
            string xml = "";
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlWriter
                    writer = XmlWriter.Create(ms, settings))
                {
                    //去除根节点的xmlns属性
                    XmlSerializerNamespaces
                        xmlns = new XmlSerializerNamespaces();
                    xmlns.Add(String.Empty, String.Empty);

                    /*
                     * 
                     * 序列化对象时使用StreamBuilder对象将始终输出UTF-16编码的格式(即使指定编码)
                     * 
                     * XmlWriter只支持在Stream类的输出中设置Encoding。
                     * 只有Stream类的输出，Encoding才可以用来进行编码，
                     * 对于StringBuilder来说，Xml的输出直接就变成了字符串了，没有任何编码过程，因此Encoding失效。
                     * 
                     */
                    xs.Serialize(writer, obj, xmlns);
                    xml = encoding.GetString(ms.ToArray());

                    writer.Close();
                }
                ms.Close();
                //ms.Dispose();
            }

            if (string.IsNullOrEmpty(xml))
                return xml;

            //return xml.Replace( "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n", "" );
            string s = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n", "").Trim();

            return s;
        }

        /// <summary>
        /// 将xml反序列化为对象,类型默认来自T.GetType()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeXmlToObject<T>(this string xml)
        {
            if (xml.IsNullOrEmpty())
                return default(T);

            return FromXml<T>(xml);
        }

        /// <summary>
        /// 反序列化对象,类型默认来自T.GetType()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T FromXml<T>(string xml)
        {
            return FromXml<T>(xml, typeof(T));
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="type">指定对象类型名称</param>
        /// <returns></returns>
        public static T FromXml<T>(string xml, string type)
        {
            return FromXml<T>(xml, Type.GetType(type));
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="type">指定对象类型</param>
        /// <returns></returns>
        public static T FromXml<T>(string xml, Type type)
        {
            object
                result = default(T);
            XmlSerializer
                xmlSerializer = new XmlSerializer(type);

            using (TextReader textReader = new StringReader(xml))
            {
                result = xmlSerializer.Deserialize(textReader);
                textReader.Close();
            }

            return (T)result;
        }

        public static object FromXml(string xml, string type)
        {
            Type t = Type.GetType(type);

            XmlSerializer
                xmlSerializer = new XmlSerializer(t);

            object result;

            using (TextReader textReader = new StringReader(xml))
            {
                result = xmlSerializer.Deserialize(textReader);
                textReader.Close();
            }

            return result;
        }

        #endregion
        #region Json
        /// <summary>
        /// 转为Json字段串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return "";
            DataContractJsonSerializer
                serializer = new DataContractJsonSerializer(obj.GetType());
            StringBuilder
                builder = new StringBuilder();

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                builder.Append(Encoding.UTF8.GetString(stream.ToArray()));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Json字符串转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
            T
                obj = default(T);
            if (json.IsNullOrEmpty())
                return obj;

            DataContractJsonSerializer
                serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                obj = (T)serializer.ReadObject(stream);
                stream.Close();
            }

            return obj;
        }
        #endregion
    }
}
