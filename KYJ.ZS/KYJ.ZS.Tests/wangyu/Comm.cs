using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KYJ.ZS.Tests.wangyu
{
    using System.IO;
    using System.IO.Compression;

    using KYJ.ZS.Commons;

    [TestClass]
    public class Comm
    {
        [TestMethod]
        public void GZipCompressDecompress()
        {
            string str = @" ";

            var s1 = str.Compress();
            var s2 = s1.Decompress();
        }
        [TestMethod]
        public void ToDouble()
        {
            var str = "18.123456789";

            var s1 = str.ToDouble();

            var s2 = "".ToDouble(0);
        }

        [TestMethod]
        public void ToJoson()
        {
            var json = "http://img.zushou.com/8c/f1/4f/8cf14f6d-2ff7-4c8d-96da-0918ee3a5e3b/6q6olv.jpg";
            var dd = json.ToJson();
        }
    }
}
