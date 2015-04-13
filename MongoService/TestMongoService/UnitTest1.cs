using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMongoService
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WebClient webClient = new WebClient();
           // webClient.Headers.Add("Accept", "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
            string str = webClient.DownloadString("http://mongoservice.yorhome.com/MongoHouseService.svc/upsert/19425");
            //XDocument xDocument = XDocument.Load("http://mongoservice.kyjob.com/MongoHouseService.svc/get/1");
            //XElement xElement = XElement.Load("http://mongoservice.kyjob.com/MongoHouseService.svc/get/1");
            //XElement xxElement = xDocument.Element("House");

         //   int i = 0;
        }
        [TestMethod]
        public void TestMethod2()
        {
            WebClient webClient = new WebClient();
            // webClient.Headers.Add("Accept", "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8");
            string str = webClient.DownloadString("http://mongoservice.yorhome.com/MongoHouseService.svc/get/19425");

       //     int i = 0;
        }
    }
}
