using System;
using System.Xml.Serialization;
using System.IO;

namespace TXCommons.Xml
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
                Log4netService.RecordLog.RecordException("反序列化", xml, e); return null;
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
                Log4netService.RecordLog.RecordException("序列化XML文件", obj.ToString(), e); return null;
            }
            stream.Position = 0;
            var sr = new StreamReader(stream);
            string str = sr.ReadToEnd();
            return str;
        } 
    }
}
