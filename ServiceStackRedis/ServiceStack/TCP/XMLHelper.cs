using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Collections;
using System.IO;

namespace ServiceStack.TCP
{
    /// <summary>
    /// XML操作类【增、删、改】
    /// </summary>
    public class XmlHelper
    {
        #region 公共变量
        XmlDocument xmldoc;
        XmlNode xmlnode;
        XmlElement xmlelem;
        #endregion

        #region 创建Xml文档
        /// <summary>
        /// 创建一个带有根节点的Xml文件
        /// </summary>
        /// <param name="FileName">Xml文件名称</param>
        /// <param name="rootName">根节点名称</param>
        /// <param name="Encode">编码方式:gb2312，UTF-8等常见的</param>
        /// <param name="DirPath">保存的目录路径</param>
        /// <returns></returns>
        public bool CreateXmlDocument(string FileName, string rootName, string Encode)
        {
            try
            {
                xmldoc = new XmlDocument();
                XmlDeclaration xmldecl;
                xmldecl = xmldoc.CreateXmlDeclaration("1.0", Encode, null);
                xmldoc.AppendChild(xmldecl);
                xmlelem = xmldoc.CreateElement("", rootName, "");
                xmldoc.AppendChild(xmlelem);
                xmldoc.Save(FileName);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region 常用操作方法(增删改)

        /// <summary>
        /// 插入一个节点和它的若干子节点
        /// </summary>
        /// <param name="XmlFile">Xml文件路径</param>
        /// <param name="NewNodeName">插入的节点名称</param>
        /// <param name="HasAttributes">此节点是否具有属性，True为有，False为无</param>
        /// <param name="fatherNode">此插入节点的父节点</param>
        /// <param name="htAtt">此节点的属性，Key为属性名，Value为属性值</param>
        /// <param name="htSubNode">子节点的属性，Key为Name,Value为InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool InsertNode(string XmlFile, string NewNodeName, bool HasAttributes, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);
                XmlNode root = xmldoc.SelectSingleNode(fatherNode);
                xmlelem = xmldoc.CreateElement(NewNodeName);

                if (htAtt != null && HasAttributes)//若此节点有属性，则先添加属性
                {
                    SetAttributes(xmlelem, htAtt);

                    SetNodes(xmlelem.Name, xmldoc, xmlelem, htSubNode);//添加完此节点属性后，再添加它的子节点和它们的InnerText

                }
                else
                {
                    SetNodes(xmlelem.Name, xmldoc, xmlelem, htSubNode);//若此节点无属性，那么直接添加它的子节点
                }

                root.AppendChild(xmlelem);
                xmldoc.Save(XmlFile);

                return true;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }

        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="XmlFile">Xml文件路径</param>
        /// <param name="fatherNode">需要更新节点的上级节点</param>
        /// <param name="htAtt">需要更新的属性表，Key代表需要更新的属性，Value代表更新后的值</param>
        /// <param name="htSubNode">需要更新的子节点的属性表，Key代表需要更新的子节点名字Name,Value代表更新后的值InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool UpdateNode(string XmlFile, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);
                XmlNodeList root = xmldoc.SelectSingleNode(fatherNode).ChildNodes;
                UpdateNodes(root, htAtt, htSubNode);
                xmldoc.Save(XmlFile);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 删除指定节点下的子节点
        /// </summary>
        /// <param name="XmlFile">Xml文件路径</param>
        /// <param name="fatherNode">制定节点</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public bool DeleteNodes(string XmlFile, string fatherNode)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);
                xmlnode = xmldoc.SelectSingleNode(fatherNode);
                xmlnode.RemoveAll();
                xmldoc.Save(XmlFile);
                return true;
            }
            catch (XmlException xe)
            {
                throw new XmlException(xe.Message);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="xe">节点所处的Element</param>
        /// <param name="htAttribute">节点属性，Key代表属性名称，Value代表属性值</param>
        private void SetAttributes(XmlElement xe, Hashtable htAttribute)
        {
            foreach (DictionaryEntry de in htAttribute)
            {
                xe.SetAttribute(de.Key.ToString(), de.Value.ToString());
            }
        }

        /// <summary>
        /// 增加子节点到根节点下
        /// </summary>
        /// <param name="rootNode">上级节点名称</param>
        /// <param name="XmlDoc">Xml文档</param>
        /// <param name="rootXe">父根节点所属的Element</param>
        /// <param name="SubNodes">子节点属性，Key为Name值，Value为InnerText值</param>
        private void SetNodes(string rootNode, XmlDocument XmlDoc, XmlElement rootXe, Hashtable SubNodes)
        {
            foreach (DictionaryEntry de in SubNodes)
            {
                xmlnode = XmlDoc.SelectSingleNode(rootNode);
                XmlElement subNode = XmlDoc.CreateElement(de.Key.ToString());
                subNode.InnerText = de.Value.ToString();
                rootXe.AppendChild(subNode);
            }
        }

        /// <summary>
        /// 更新节点属性和子节点InnerText值
        /// </summary>
        /// <param name="root">根节点名字</param>
        /// <param name="htAtt">需要更改的属性名称和值</param>
        /// <param name="htSubNode">需要更改InnerText的子节点名字和值</param>
        private void UpdateNodes(XmlNodeList root, Hashtable htAtt, Hashtable htSubNode)
        {
            foreach (XmlNode xn in root)
            {
                xmlelem = (XmlElement)xn;
                if (xmlelem.HasAttributes)//如果节点如属性，则先更改它的属性
                {
                    foreach (DictionaryEntry de in htAtt)//遍历属性哈希表
                    {
                        if (xmlelem.HasAttribute(de.Key.ToString()))//如果节点有需要更改的属性
                        {
                            xmlelem.SetAttribute(de.Key.ToString(), de.Value.ToString());//则把哈希表中相应的值Value赋给此属性Key
                        }
                    }
                }
                if (xmlelem.HasChildNodes)//如果有子节点，则修改其子节点的InnerText
                {
                    XmlNodeList xnl = xmlelem.ChildNodes;
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe = (XmlElement)xn1;
                        foreach (DictionaryEntry de in htSubNode)
                        {
                            if (xe.Name == de.Key.ToString())//htSubNode中的key存储了需要更改的节点名称，
                            {
                                xe.InnerText = de.Value.ToString();//htSubNode中的Value存储了Key节点更新后的数据
                            }
                        }
                    }
                }

            }
        }

        #endregion
    }

    /// <summary>
    /// 读取XML操作类
    /// Author: 黄继华
    /// </summary>
    public class XmlReaderClass
    {
        #region 获取XmlDocument对象

        #region 根据XML文件内容获取XmlDocument对象
        /// <summary>
        /// 根据XML文件内容获取XmlDocument对象
        /// </summary>
        /// <param name="xmlFileContent">xmlFileContent</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument GetXmlDocByXmlContent(string xmlFileContent)
        {
            if (string.IsNullOrEmpty(xmlFileContent))
            {
                return null;
            }

            var xDoc = new XmlDocument();
            try
            {
                xDoc.LoadXml(xmlFileContent);
            }
            catch
            {
                xDoc = null;
            }

            return xDoc;
        }
        #endregion

        #region 根据XML文件路径获取XmlDocument对象


        /// <summary>
        /// 根据XML文件路径获取XmlDocument对象
        /// </summary>
        /// <param name="xmlFilePath">文件路径</param>
        /// <returns>XmlDocument</returns>
        public static XmlDocument GetXmlDocByFilePath(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
            {
                return null;
            }

            var xDoc = new XmlDocument();
            try
            {
                xDoc.Load(xmlFilePath);
            }
            catch
            {
                throw new Exception(string.Format("请确认该XML文件格式正确，路径为：{0}", xmlFilePath));
            }

            return xDoc;
        }

        #endregion

        #endregion

        //====//

        #region 获取XML节点（或节点列表）

        #region 获取父节点下指定节点名称的第一个子节点
        /// <summary>
        /// 获取父节点下指定节点名称的第一个子节点
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns>XmlNode</returns>
        public static XmlNode GetFirstChildNodeByName(XmlNode parentXmlNode, string childNodeName)
        {
            var childXmlNodes = GetChildNodesByName(parentXmlNode, childNodeName);
            if (childXmlNodes != null && childXmlNodes.Count > 0)
            {
                return childXmlNodes[0];
            }

            return null;
        }

        #endregion

        #region 获取父节点下指定节点名称的子节点列表
        /// <summary>
        /// 获取父节点下指定节点名称的子节点列表
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns>XmlNodeList</returns>
        public static XmlNodeList GetChildNodesByName(XmlNode parentXmlNode, string nodeName)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(nodeName))
            {
                return null;
            }

            return GetChildNodesByXPathExpr(parentXmlNode, string.Format(".//{0}", nodeName));
        }
        #endregion

        #region 获取父节点下满足xpathExpr表达式的XML子节点列表
        /// <summary>
        /// 获取父节点下满足xpathExpr表达式的XML子节点列表
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <param name="xpathExpr"></param>
        /// <returns>XmlNodeList</returns>   
        public static XmlNodeList GetChildNodesByXPathExpr(XmlNode parentXmlNode, string xpathExpr)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(xpathExpr))
            {
                return null;
            }

            return parentXmlNode.SelectNodes(xpathExpr);
        }
        #endregion

        #region 获取父节点下的第一个子节点
        /// <summary>
        /// 获取父节点下的第一个子节点
        /// </summary>
        /// <param name="parentXmlNode">parentXmlNode</param>
        /// <returns>XmlNode</returns>
        public static XmlNode GetFirstChildNode(XmlNode parentXmlNode)
        {
            var childXmlNodes = GetChildNodes(parentXmlNode);
            if (childXmlNodes != null && childXmlNodes.Count > 0)
            {
                return childXmlNodes[0];
            }

            return null;
        }
        #endregion

        #region 获取父节点的子节点列表
        /// <summary>
        /// 获取父节点的子节点列表
        /// </summary>
        /// <param name="parentXmlNode">父节点</param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodes(XmlNode parentXmlNode)
        {
            return parentXmlNode == null ? null : parentXmlNode.ChildNodes;
        }
        #endregion

        #endregion

        //====//

        #region 读取节点属性值

        #region 读取某个XML节点的属性值（根据属性名）
        /// <summary>
        /// 读取某个XML节点的属性值（根据属性名）
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string ReadAttrValue(XmlNode xmlNode, string attrName)
        {
            var xmlElement = xmlNode as XmlElement;

            return xmlElement == null ? null : xmlElement.GetAttribute(attrName);
        }
        #endregion

        #region 读取父节点下指定节点名和属性名的第一个子节点的属性值
        /// <summary>
        /// 读取父节点下指定节点名和属性名的第一个子节点的属性值
        /// </summary>
        /// <param name="parentXmlNode">XML父节点</param>
        /// <param name="childNodeName">节点名称</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string ReadFirstAttrValue(XmlNode parentXmlNode, string childNodeName, string attrName)
        {
            var attrVals = ReadAttrValues(parentXmlNode, childNodeName, attrName);
            return (attrVals == null || attrVals.Length == 0) ? null : attrVals[0];
        }
        #endregion

        #region 读取父节点下指定节点名和属性名的所有子节点的该属性值的数组
        /// <summary>
        /// 读取父节点下指定节点名和属性名的所有子节点的该属性值的数组
        /// </summary>
        /// <param name="parentXmlNode">XML文档</param>
        /// <param name="childNodeName">节点名称</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string[] ReadAttrValues(XmlNode parentXmlNode, string childNodeName, string attrName)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(childNodeName) || string.IsNullOrEmpty(attrName))
            {
                return null;
            }

            var xpathExpr = string.Format("//{0}[@{1}]", childNodeName, attrName);
            var nodes = GetChildNodesByXPathExpr(parentXmlNode, xpathExpr);
            if (nodes != null && nodes.Count > 0)
            {
                var nodeCount = nodes.Count;
                var attrVals = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    attrVals[i] = ((XmlElement)nodes[i]).GetAttribute(attrName);
                }

                return attrVals;
            }

            return null;
        }
        #endregion

        #endregion

        //====//

        #region 读取父节点下的子节点的文本内容

        #region 读取父节点下指定节点名的第一个子节点的文本
        /// <summary>
        /// 读取父节点下指定节点名的第一个子节点的文本
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static string ReadFirstChildNodeTextByName(XmlNode parentXmlNode, string childNodeName)
        {
            var childNodeTexts = ReadChildNodeTextsByName(parentXmlNode, childNodeName);
            if (childNodeTexts != null && childNodeTexts.Length > 0)
            {
                return childNodeTexts[0];
            }

            return null;
        }
        #endregion

        #region 读取父节点下指定节点名的所有子节点的文本数组
        /// <summary>
        /// 读取父节点下指定节点名的所有子节点的文本数组
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static string[] ReadChildNodeTextsByName(XmlNode parentXmlNode, string childNodeName)
        {
            if (parentXmlNode == null || string.IsNullOrEmpty(childNodeName))
            {
                return null;
            }

            var xpathExpr = string.Format(".//{0}", childNodeName);
            var childNodes = GetChildNodesByXPathExpr(parentXmlNode, xpathExpr);
            if (childNodes != null && childNodes.Count > 0)
            {
                var nodeCount = childNodes.Count;
                var nodeTexts = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    nodeTexts[i] = childNodes[i].InnerText;
                }

                return nodeTexts;
            }

            return null;
        }
        #endregion

        #region 读取父节点下的第一个子节点的文本
        /// <summary>
        /// 读取父节点下的第一个子节点的文本
        /// </summary>
        /// <param name="parentXmlNode"></param>
        /// <returns></returns>
        public static string ReadFirstChildNodeText(XmlNode parentXmlNode)
        {
            var childNodeTexts = ReadChildNodeTexts(parentXmlNode);
            if (childNodeTexts != null && childNodeTexts.Length > 0)
            {
                return childNodeTexts[0];
            }

            return null;
        }
        #endregion

        #region 读取父节点下的所有子节点的文本数组
        /// <summary>
        /// 读取父节点下的所有子节点的文本数组
        /// </summary>
        /// <param name="parentXmlNode">parentXmlNode</param>
        /// <returns>string[]</returns>
        public static string[] ReadChildNodeTexts(XmlNode parentXmlNode)
        {
            if (parentXmlNode == null)
            {
                return null;
            }

            var childNodes = GetChildNodes(parentXmlNode);
            if (childNodes != null && childNodes.Count > 0)
            {
                var nodeCount = childNodes.Count;
                var nodeTexts = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    nodeTexts[i] = childNodes[i].InnerText;
                }

                return nodeTexts;
            }

            return null;
        }
        #endregion

        #region 读取XML节点文本
        /// <summary>
        /// 读取XML节点文本
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        public static string ReadNodeText(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                return null;
            }

            return xmlNode.InnerText;
        }
        #endregion

        #endregion

        //==== 2014-08-25//
        #region 根据xmlpath文件获取父节点下的XMLNodeList
        /// <summary>
        ///  根据xmlpath文件路径获取返回XMLNodeList
        /// </summary>
        /// <param name="xmlpath">xml路径,如:/data/test.xml</param>
        /// <param name="xPath">XPath Expression 如：/taglist/tag 表示taglist根节点下所有的tag子节点</param>
        /// <returns>XMLNodeList</returns>
        public static XmlNodeList GetXmlNodeList(string xmlpath, string xPath, bool isMapPath = true)
        {
            XmlDocument xmldoc = XmlReaderClass.GetXmlDocByFilePath(isMapPath ? HttpContext.Current.Server.MapPath(xmlpath) : xmlpath);
            XmlNodeList nodelist = xmldoc.SelectNodes(xPath);

            return nodelist;
        }
        #endregion

        #region 根据标签属性值条件来获取该标签的InnerText
        /// <summary>
        /// 根据标签属性值条件来获取该标签的InnerText
        /// </summary>
        /// <param name="xmlPath">xml路径,如:/data/test.xml</param>
        /// <param name="xPath">XPath Expression 如：/taglist/tag[@id=1] 表示taglist根节点下的tag子节点中的id属性=1</param>
        /// <returns>inneText</returns>
        public static string GetSingleInnerText(string xmlPath, string xPath)
        {
            string result = string.Empty;
            try
            {
                XmlDocument xmldoc = XmlReaderClass.GetXmlDocByFilePath(HttpContext.Current.Server.MapPath(xmlPath));
                XmlNode xn = xmldoc.SelectSingleNode(xPath);
                result = xn.InnerText;
            }
            catch (Exception)
            {
                //throw;
            }

            return result;
        }
        #endregion
    }
}
