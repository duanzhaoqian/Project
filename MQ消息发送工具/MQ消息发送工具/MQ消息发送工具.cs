using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MQ消息发送工具
{
    public partial class MQ消息发送工具 : Form
    {
        public MQ消息发送工具()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (File.Exists("MQLibrary.xml"))
                {
                    XDocument xDocument = XDocument.Load("MQLibrary.xml");
                    List<string> listIPAddress = xDocument.Element("MQLibrary").Element("MQIPAddress").Elements("IPAddress").Select(c => c.Value.Trim()).ToList();
                    List<string> listMQName = xDocument.Element("MQLibrary").Element("MQName").Elements("Name").Select(c => c.Value.Trim()).ToList();

                    List<string> listMsg1 = xDocument.Element("MQLibrary").Element("MQMessage").Element("cmbMsg1").Elements("text").Select(c => c.Value.Trim()).ToList();
                    List<string> listMsg2 = xDocument.Element("MQLibrary").Element("MQMessage").Element("cmbMsg2").Elements("text").Select(c => c.Value.Trim()).ToList();
                    List<string> listMsg3 = xDocument.Element("MQLibrary").Element("MQMessage").Element("cmbMsg3").Elements("text").Select(c => c.Value.Trim()).ToList();
                    List<string> listMsg4 = xDocument.Element("MQLibrary").Element("MQMessage").Element("cmbMsg4").Elements("text").Select(c => c.Value.Trim()).ToList();

                    listIPAddress.Where(c => !string.IsNullOrEmpty(c)).ToList().ForEach(c => this.cmbMQAddress.Items.Add(c));
                    listMQName.Where(c => !string.IsNullOrEmpty(c)).ToList().ForEach(c => this.cmbMQName.Items.Add(c));
                    listMsg1.Where(c => !string.IsNullOrEmpty(c)).ToList().ForEach(c => this.cmbMsg1.Items.Add(c));
                    listMsg2.Where(c => !string.IsNullOrEmpty(c)).ToList().ForEach(c => this.cmbMsg2.Items.Add(c));
                    listMsg3.Where(c => !string.IsNullOrEmpty(c)).ToList().ForEach(c => this.cmbMsg3.Items.Add(c));
                    listMsg4.Where(c => !string.IsNullOrEmpty(c)).ToList().ForEach(c => this.cmbMsg4.Items.Add(c));
                }
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private void btnSendArrayList_Click(object sender, EventArgs e)
        {
            MQSenderHelp senderHelp = new MQSenderHelp();
            senderHelp.MQAddress = cmbMQAddress.Text;
            senderHelp.MQName = cmbMQName.Text;
            ArrayList arrayList = new ArrayList();
            arrayList.Add(cmbMsg1.Text);
            arrayList.Add(cmbMsg2.Text);
            arrayList.Add(cmbMsg3.Text);
            arrayList.Add(cmbMsg4.Text);
            senderHelp.SendObjectMsg(arrayList);
            SaveLibrary();
        }

        private void SaveLibrary()
        {
            if (!File.Exists("MQLibrary.xml"))
            {
                XDocument xDocumentNew = new XDocument();
                XElement xElement = new XElement("MQLibrary");
                XElement xElement1 = new XElement("MQIPAddress");
                XElement xElement2 = new XElement("MQName");
                XElement xElement3 = new XElement("MQMessage");
                XElement xElementcmb1 = new XElement("cmbMsg1");
                XElement xElementcmb2 = new XElement("cmbMsg2");
                XElement xElementcmb3 = new XElement("cmbMsg3");
                XElement xElementcmb4 = new XElement("cmbMsg4");
                xDocumentNew.Add(xElement);
                xElement.Add(xElement1);
                xElement.Add(xElement2);
                xElement.Add(xElement3);
                xElement3.Add(xElementcmb1);
                xElement3.Add(xElementcmb2);
                xElement3.Add(xElementcmb3);
                xElement3.Add(xElementcmb4);
                xDocumentNew.Save("MQLibrary.xml");
            }
            XDocument xDocument = XDocument.Load("MQLibrary.xml");
            List<string> listIPAddress =
                xDocument.Element("MQLibrary").Element("MQIPAddress").Elements("IPAddress").Select(c => c.Value.Trim()).ToList();
            List<string> listMQName =
                xDocument.Element("MQLibrary").Element("MQName").Elements("Name").Select(c => c.Value.Trim()).ToList();

            List<string> listMsg1 =
                xDocument.Element("MQLibrary")
                    .Element("MQMessage")
                    .Element("cmbMsg1")
                    .Elements("text")
                    .Select(c => c.Value.Trim())
                    .ToList();
            List<string> listMsg2 =
                xDocument.Element("MQLibrary")
                    .Element("MQMessage")
                    .Element("cmbMsg2")
                    .Elements("text")
                    .Select(c => c.Value.Trim())
                    .ToList();
            List<string> listMsg3 =
                xDocument.Element("MQLibrary")
                    .Element("MQMessage")
                    .Element("cmbMsg3")
                    .Elements("text")
                    .Select(c => c.Value.Trim())
                    .ToList();
            List<string> listMsg4 =
                xDocument.Element("MQLibrary")
                    .Element("MQMessage")
                    .Element("cmbMsg4")
                    .Elements("text")
                    .Select(c => c.Value.Trim())
                    .ToList();
            if (!string.IsNullOrEmpty(this.cmbMQAddress.Text.Trim()))
            {
                if (!listIPAddress.Contains(this.cmbMQAddress.Text.Trim()))
                {
                    XElement element = new XElement("IPAddress");
                    element.SetValue(this.cmbMQAddress.Text.Trim());
                    xDocument.Element("MQLibrary").Element("MQIPAddress").Add(element);
                }
            }
            if (!string.IsNullOrEmpty(this.cmbMQName.Text.Trim()))
            {
                if (!listMQName.Contains(this.cmbMQName.Text.Trim()))
                {
                    XElement element = new XElement("Name");
                    element.SetValue(this.cmbMQName.Text.Trim());
                    xDocument.Element("MQLibrary").Element("MQName").Add(element);
                }
            }
            if (!string.IsNullOrEmpty(this.cmbMsg1.Text.Trim()))
            {
                if (!listMsg1.Contains(this.cmbMsg1.Text.Trim()))
                {
                    XElement element = new XElement("text");
                    element.SetValue(this.cmbMsg1.Text.Trim());
                    xDocument.Element("MQLibrary").Element("MQMessage")
                    .Element("cmbMsg1").Add(element);
                }
            }
            if (!string.IsNullOrEmpty(this.cmbMsg2.Text.Trim()))
            {
                if (!listMsg2.Contains(this.cmbMsg2.Text.Trim()))
                {
                    XElement element = new XElement("text");
                    element.SetValue(this.cmbMsg2.Text.Trim());
                    xDocument.Element("MQLibrary").Element("MQMessage")
                    .Element("cmbMsg2").Add(element);
                }
            }
            if (!string.IsNullOrEmpty(this.cmbMsg3.Text.Trim()))
            {
                if (!listMsg3.Contains(this.cmbMsg3.Text.Trim()))
                {
                    XElement element = new XElement("text");
                    element.SetValue(this.cmbMsg3.Text.Trim());
                    xDocument.Element("MQLibrary").Element("MQMessage")
                    .Element("cmbMsg3").Add(element);
                }
            }
            if (!string.IsNullOrEmpty(this.cmbMsg4.Text.Trim()))
            {
                if (!listMsg4.Contains(this.cmbMsg4.Text.Trim()))
                {
                    XElement element = new XElement("text");
                    element.SetValue(this.cmbMsg4.Text.Trim());
                    xDocument.Element("MQLibrary").Element("MQMessage")
                    .Element("cmbMsg4").Add(element);

                }
            }
            xDocument.Save("MQLibrary.xml");
        }

        private void btnSendSingle_Click(object sender, EventArgs e)
        {
            MQSenderHelp senderHelp = new MQSenderHelp();
            senderHelp.MQAddress = cmbMQAddress.Text;
            senderHelp.MQName = cmbMQName.Text;
            senderHelp.SendTextMsg(cmbMsg1.Text);
            SaveLibrary();

        }
    }
}
