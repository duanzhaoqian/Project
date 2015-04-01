using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KYJ.ZS.Tests.zhuzh
{
    /// <summary>
    /// PayTest 的摘要说明
    /// </summary>
    [TestClass]
    public class PayTest
    {
        public PayTest()
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
        /// 测试订单支付回调接口
        /// </summary>
        [TestMethod]
        public void TestPaymentUpdateOrderStatus()
        {

            KYJ.ZS.BLL.Payments.PaymentBll _payBll = new BLL.Payments.PaymentBll();

            var ordernum = "160114061016074001";
            var uid = 397;

            var result = _payBll.UpdateOrderStatus(ordernum, uid);

            Assert.AreEqual<int>(1, result);

            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
        /// <summary>
        /// 测试续租支付回调接口
        /// </summary>
        [TestMethod]
        public void TestTenancyOrderRelet()
        {

            KYJ.ZS.BLL.Payments.PaymentBll _payBll = new BLL.Payments.PaymentBll();

            var ordernum = "160114061016074001";
            var uid = 397;

            var result = _payBll.OrderRelet(ordernum, uid);

            Assert.AreEqual<int>(1, result);

            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
        /// <summary>
        /// 测试是否允许续租或者购买接口
        /// </summary>
        [TestMethod]
        public void TestTenancyIsRelet()
        {

            KYJ.ZS.BLL.Payments.PaymentBll _payBll = new BLL.Payments.PaymentBll();

            var ordernum = "160114061016074001";
            var uid = 397;

            var result = _payBll.IsRelet(ordernum, uid);

            Assert.AreEqual<bool>(true, result);

            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
        /// <summary>
        /// 测试购买支付回调接口
        /// </summary>
        [TestMethod]
        public void TestTenancyOrderPayout()
        {

            KYJ.ZS.BLL.Payments.PaymentBll _payBll = new BLL.Payments.PaymentBll();

            var ordernum = "160114060613382001";
            var uid = 397;

            var result = _payBll.OrderBuyout(ordernum, uid);

            Assert.AreEqual<int>(1, result);

            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
        /// <summary>
        /// 测试购买支付回调接口
        /// </summary>
        [TestMethod]
        public void TestClaimOrderCliam()
        {

            KYJ.ZS.BLL.Payments.PaymentBll _payBll = new BLL.Payments.PaymentBll();

            var ordernum = "160114053015415901";
            var uid = 397;

            var result = _payBll.OrderCliam(ordernum, uid);

            Assert.AreEqual<int>(1, result);

            //Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
