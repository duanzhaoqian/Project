using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using KYJ.ZS.DAL.TenancyTemplates;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KYJ.ZS.Tests.maq
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月22日
    /// TenancyTemplateTest 的摘要说明
    /// </summary>
    [TestClass]
    public class TenancyTemplateTest
    {
        TenancyTemplateDal dal = new TenancyTemplateDal();
        public TenancyTemplateTest()
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
        /// <summary>
        /// insert方法测试
        /// </summary>
        [TestMethod]
        public void Insert()
        {
            //
            // TODO: 在此处添加测试逻辑
            //
            TenancyTemplate t = new TenancyTemplate();
            t.MerchantId = 1; 
            t.TtempCreatetime = DateTime.Now;
            t.TtempIsdel = false;
            t.TtempMonths = "1,2,3";
            t.TtempTitle = "模板二";
            t.TtempUpdatetime = DateTime.Now;

            long i = dal.Insert(t);
            Assert.AreEqual(i, 1);
        }

        /// <summary>
        /// update方法测试
        /// </summary>
        [TestMethod]
        public void Update()
        {
            TenancyTemplate t = new TenancyTemplate();
            t.TtempId = 3;
            t.MerchantId = 1;
            t.TtempCreatetime = DateTime.Now;
            t.TtempIsdel = false;
            t.TtempMonths = "1,2,3,4,5,6";
            t.TtempTitle = "模板二";
            t.TtempUpdatetime = DateTime.Now;
            long i = dal.UpDate(t,7);
            Assert.AreEqual(i, 1);
        }
        /// <summary>
        /// getmodel方法测试
        /// </summary>
       [TestMethod]
        public void GetModel()
        {
            TenancyTemplate t = new TenancyTemplate();
            t.TtempId = 3;
            t.MerchantId = 1;
            t.TtempCreatetime = DateTime.Now;
            t.TtempIsdel = false;
            t.TtempMonths = "1,2,3,4,5,6";
            t.TtempTitle = "模板二";
            t.TtempUpdatetime = DateTime.Now;
            TenancyTemplate tenancy = dal.GetModel(3,7);

            Assert.AreEqual(t.MerchantId, tenancy.MerchantId);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetList()
        {
            PagePms pms= new PagePms();
            pms.PageIndex = 1;
            pms.PageSize = 3;
            pms.StrWhere = "";
            PageData < TenancyTemplate > list= dal.GetList(pms);
            Assert.AreEqual(list.PageCount,2);
        }
    }
}
