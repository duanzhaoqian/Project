using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using IService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PicTest
{
    [TestClass]
    public class TempProcessPicTest
    {
        [TestMethod]
        public void TestSavePic()
        {
            IProcessPic processPic = Factory.ObjectFactory.GetTempProcessPic();
            FileStream fileStream = new FileStream(@"C:\Users\duanzq\Pictures\img103.png", FileMode.Open, FileAccess.Read);
            byte[] bytes=new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            string str = processPic.ProcessPic("aaaa", "bb", bytes, "1234.jpg");
            int i = 0;
        }
        [TestMethod]
        public void TestStream()
        {
            IProcessPic processPic = Factory.ObjectFactory.GetTempProcessPic();
            FileStream fileStream = new FileStream(@"D:\img\28\14\82\281482a3-fe99-41a4-8420-33f3328570ab\nptpr4..jpg", FileMode.Open, FileAccess.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
           // string str = processPic.ProcessPic("aaaa", "bb", bytes, "1234.jpg");
            FileStream fileStream1 = new FileStream(@"D:\img\28\14\82\281482a3-fe99-41a4-8420-33f3328570ab\aa.jpg",FileMode.OpenOrCreate,FileAccess.Write);
            BinaryWriter bw =new BinaryWriter(fileStream1);
            bw.Write(bytes, 0, bytes.Length);
            bw.Flush();
            fileStream1.Close();
        }
        [TestMethod]
        public void TestImageFormat()
        {
           // Console.WriteLine(ImageFormat.Jpeg.ToString());
            Bitmap bitmap = new Bitmap(@"D:\img\28\14\82\281482a3-fe99-41a4-8420-33f3328570ab\nptpr4..jpg");
            string str=new ImageFormatConverter().ConvertToString(bitmap.RawFormat);
            Console.WriteLine(bitmap.RawFormat);
        }
    }
}
