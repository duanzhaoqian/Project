using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using KYJ.ZS.DAL.PictureFolders;
using KYJ.ZS.Models.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KYJ.ZS.Tests.maq
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月22日
    /// PictureFolderTest 
    /// </summary>
    [TestClass]
    public class PictureFolderTest
    {

        PictureFolderDal dal = new PictureFolderDal();
        public PictureFolderTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        // [TestMethod]

        /// <summary>
        /// insert方法测试
        /// </summary>
        public void Insert()
        {
            //
            // TODO: 在此处添加测试逻辑
            //
            PictureFolder pictureFolder = new PictureFolder();
            pictureFolder.FolderGuid = Guid.NewGuid().ToString();
            pictureFolder.FolderName = "美图";
            pictureFolder.FolderSort = 1;
            long i = dal.Insert(pictureFolder);
            Assert.AreEqual(i, 1);
        }
        /// <summary>
        /// update方法测试
        /// </summary>
        //[TestMethod]
        public void UpDate()
        {
            PictureFolder pictureFolder = new PictureFolder();
            pictureFolder.FolderGuid = Guid.NewGuid().ToString();
            pictureFolder.FolderName = "美图";
            pictureFolder.FolderSort = 1;
            pictureFolder.FolderId=1;
            long i = dal.UpDate(pictureFolder);
            Assert.AreEqual(i, 1);
        }
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetModel()
        {
            PictureFolder pictureFolder = dal.GetModel(2);
            Assert.AreEqual(pictureFolder.FolderGuid, "246eddd0-4824-4c67-ba62-b0360b51f749");
        }
    }
}
