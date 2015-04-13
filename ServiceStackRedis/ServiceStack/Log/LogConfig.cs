using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ServiceStack.Log
{
    [Obsolete("不再读取Config文件，此类作废", true)]
    internal static class LogConfig
    {
        private static XDocument _xDocument = null;

        public static XDocument XDocument
        {
            get
            {
                if (_xDocument == null)
                {
                    XDocument = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AopLog.config"));
                }
                return _xDocument;
            }
            set { _xDocument = value; }
        }

        public static XElement GetElement(XElement element, string attributeName, string attrebuteValue)
        {
            IEnumerable<XElement> elements = element.Elements();
            foreach (XElement ele in elements)
            {
                XAttribute xAttribute = ele.Attribute(attributeName);
                if (xAttribute != null)
                {
                    if (xAttribute.Value == attrebuteValue)
                    {
                        return ele;
                    }
                }
                XElement xElement = GetElement(ele, attributeName, attrebuteValue);
                if (xElement != null)
                {
                    return xElement;
                }
            }
            return null;
        }
    }
}
