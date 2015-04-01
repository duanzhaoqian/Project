#define notes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.BLL.BankAccounts;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.BankAccounts;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.Models.View;
using KYJ.ZS.Commons;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace KYJ.ZS.Merchant.Controllers
{
    /// <summary>
    /// Auther:李晓波
    /// Time:2014-4-24
    /// Desc:账户管理
    /// </summary>   
    public class AccountManageController : BaseController
    {
        BankAccountBll bll = new BankAccountBll();

        #region 提现管理-申请提现[HttpGet] +ActionResult ApplyFor()
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24
        /// 提现管理-申请提现
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplyFor()
        {
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            List<BankAccountEntity> liBankAccount = bll.GetBankList(id);
            ViewBag.Tag = 1;
            bool AccountStatus = bll.GetAccountStatus(id);//账户是否冻结
            if (AccountStatus)
            {
                string url = Url.MerchantSiteUrl().FreezeInfo;
                return Redirect(url);
            }
            decimal accountPrice = 0;
            decimal.TryParse(bll.GetAccountPrice(id), out accountPrice);
            ViewBag.AccountPrice = accountPrice;   //控制器向视图传参
            //ViewBag.AccountPrice = Convert.ToDecimal(bll.GetAccountPrice(id));   //可能出现类型转换错误！  注释
            return View(liBankAccount);
        }
        #endregion

        #region 账户冻结信息 +ActionResult FreezeInfo()
        /// <summary>
        /// 账户冻结信息
        /// </summary>
        /// <returns></returns>
        public ActionResult FreezeInfo()
        {
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID;
            //如果后台已经处理提现  则跳转到 ApplyFor 界面
            bool AccountStatus = bll.GetAccountStatus(id);//账户是否冻结
            if (!AccountStatus)
            {
                string url = Url.MerchantSiteUrl().ApplyFor;
                return Redirect(url);
            }
            BankAccountEntity mdlBankAccoun = new BankAccountEntity();
            ViewBag.Tag = 1;
            mdlBankAccoun = bll.GetWithdrawCashs(id);
            //ViewBag.AccountPrice = Convert.ToDecimal(bll.GetAccountPrice(id));   20140528  注释  by cy
            decimal accountPrice = 0;
            decimal.TryParse(bll.GetAccountPrice(id), out accountPrice);   //尝试转换
            ViewBag.AccountPrice = accountPrice;
            return View(mdlBankAccoun);

        }
        #endregion

        #region 提现管理-申请提现[HttpPost] +ActionResult Apply(decimal withdraw)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-5
        /// 提现管理-申请提现
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Apply(decimal withdraw)
        {
            //int id, decimal withdraw, string mbank_number, string mbank_name, string mbank_bankname, string log_desc
            BackMessge bm = new BackMessge();
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            string message;
            BankAccountEntity mdlAccountEntity = InitBankAccount(withdraw);
            decimal withdrawbalance = 0;   //提现后的余额
            decimal.TryParse(Request["withdrawbalance"], out withdrawbalance);
            //获取提现订单号
            string orderNumber = Tool.Instance.GetOrderNumber(4);
            ViewBag.Tag = 1;
            bm.Success = bll.ApplyFor(id, mdlAccountEntity, withdraw, withdrawbalance, orderNumber, out message);
            bm.Message = message;
            //bm.Message = "111";
            //bm.Success = true;

            return Json(bm);
        }
        #endregion

        #region 提现管理-管理银行账户 +ActionResult ManageBank()
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24
        /// 提现管理-管理银行账户
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageBank()
        {
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            IList<BankAccount> liBankAccount = bll.GetBankAccountList(id);
            ViewBag.Tag = 3;
            return View(liBankAccount);
        }
        #endregion

        #region 提现管理-添加银行账户[HttpGet] +ActionResult AddBankAccount()
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24
        /// 提现管理-添加银行账户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddBankAccount()
        {
            ViewBag.Tag = 4;
            return View();
        }
        #endregion

        #region 提现管理-添加银行账户[HttpPost] +ActionResult AddBankAccount(string name, string number, string bankname, string remark)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-29
        /// 提现管理-添加银行账户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddBankAccount(string name, string number, string bankname, string remark)
        {
            ViewBag.Tag = 4;
            if (ModelState.IsValid)
            {
                BankAccount mdlBankAccount = new BankAccount();
                mdlBankAccount.Name = name;
                mdlBankAccount.Number = number;
                mdlBankAccount.BankName = bankname;
                mdlBankAccount.Remark = remark;
                mdlBankAccount.MerchantId = _ServiceContext.CurrentUser.UserID;//获取商户ID
                bool succeed = bll.Create(mdlBankAccount);
                if (succeed)
                {
                    return RedirectToAction("ManageBank");
                }
                else
                {
                    return View();
                }

            }

            return View();

        }
        #endregion

        #region 修改银行账户[HttpGet] +ActionResult EditBankAccount(int id)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-29
        /// 修改银行账户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditBankAccount(int id)
        {
            ViewBag.Tag = 3;
            BankAccount mdlBankAccount = bll.GetBankAccount(id);
            return View(mdlBankAccount);
        }
        #endregion

        #region 修改银行账户[HttpPost] +ActionResult EditBankAccount(BankAccount mdlBankAccount)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-29
        /// 修改银行账户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditBankAccount(BankAccount mdlBankAccount)
        {
            ViewBag.Tag = 3;
            if (ModelState.IsValid)
            {
                //20140603 add by cy  对于可选项 remark 作出的判断
                if (string.IsNullOrEmpty(mdlBankAccount.Remark))
                {
                    mdlBankAccount.Remark = string.Empty;
                }
                bool succeed = bll.Update(mdlBankAccount);
                if (succeed)
                {
                    return RedirectToAction("ManageBank");
                }
                else
                {
                    return View(mdlBankAccount);
                }
            }
            return View(mdlBankAccount);
        }
        #endregion

        #region 删除银行账户 +JsonResult DelBankAccount(int id)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-29
        /// 删除银行账户
        /// </summary>
        /// <returns></returns>       
        public JsonResult DelBankAccount(int id)
        {
            BackMessge bm = new BackMessge();
            string message;
            bm.Success = bll.Delete(id, out message);
            bm.Message = message;
            return Json(bm);
        }
        #endregion

        #region 充值[HttpGet] +ActionResult Recharge()
        /// <summary>
        /// 充值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Recharge()
        {
            //查询账户余额
            int id = _ServiceContext.CurrentUser.UserID;//获取商户ID
            string totalAmount = bll.GetAccountPrice(id);
            ViewBag.totalAmount = totalAmount;
            ViewBag.Tag = 5;
            return View();
        }
        #endregion

        #region 充值[HttpPost] +ActionResult Recharge(decimal rechargeAmount, decimal totalAmount)
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="rechargeAmount">充值金额</param>
        /// <param name="mode">充值类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recharge(decimal totalAmount, decimal rechargeAmount, string mode)
        {
            string str = string.Format("账户余额={0}，充值金额：{1}，充值类型：{2}，充值后余额：{3}", totalAmount, rechargeAmount, mode, totalAmount = rechargeAmount);
            //生成充值订单号
            string orderNo = Tool.Instance.GetOrderNumber(3);

            return Json(new { str = str });
        }
        #endregion

        #region 接受前台传过来的数据 +BankAccountEntity InitBankAccount(decimal withdraw)
        /// <summary>
        /// Auther:李晓波
        /// Time:2014-5-5
        /// Desc:接受前台传过来的数据
        /// </summary>          
        private BankAccountEntity InitBankAccount(decimal withdraw)
        {
            //string mbank_number, string mbank_name, string mbank_bankname, string log_desc
            //name: name, number: number, bankname: bankname, withdraw: withdraw 
            BankAccountEntity mdlBankAccount = new BankAccountEntity();
            mdlBankAccount.Number = Request["number"];
            mdlBankAccount.Name = Request["name"];
            mdlBankAccount.BankName = Request["bankname"];
            mdlBankAccount.Desc = "商户提现金额" + withdraw + "元";
            mdlBankAccount.Remark = Request["bankremark"];   //20140527  add by cy
            return mdlBankAccount;
        }
        #endregion

        #region 发送支付请求 +ActionResult Send(string rechargeMoney, string mode)
        /// <summary>
        /// 发送支付请求
        /// </summary>
        /// <returns></returns>
        [KYJ.ZS.Merchant.Controllers.ActionFilters.LoginChecked]
        [HttpGet]
        public ActionResult Send(string rechargeMoney, string mode)
        {
            //获取订单号
            string ordernum = Tool.Instance.GetOrderNumber(3);
            //uid
            int uid = _ServiceContext.CurrentUser.UserID;
            //type
            int type = 2;
            //string callbackurl = Url.MerchantSiteUrl().ApplyFor;

            var pprice = rechargeMoney;//Request["pprice"].ToString();
            var pk = mode;//Request.Form["bank"].ToString();
#if !notes
            var _paymentBll = new KYJ.ZS.BLL.Payments.PaymentBll();
            //当前登录 用户ID
            var uid = _ServiceContext.CurrentUser.UserID;
            //获取当前登录用户指定订单号的支付信息
            var order = _paymentBll.GetClaimByOrderNum(ordernum, uid);
            //判断订单是否需要支付，如果不需要跳转用户中心页
            if (order != null)
            {
                pprice = order.TotalPrice.ToString("#0.00");
            }
            else
            {
                return Redirect(Url.UserSiteUrl().Index);
            }  
#endif

            string pName = "商户充值";
            string pDesc = "商户充值";

            //Redis.SetValue("kuaiqian:" + orderNum, "1", DateTime.Now.AddMinutes(30), FunctionTypeEnum.UserInfo, 0);
            #region 第三方支付页

            #region 参数说明
            //人民币网关账号，该账号为11位人民币网关商户编号+01,该参数必填。
            string merchantAcctId = "1002290589901";
            //编码方式，1代表 UTF-8; 2 代表 GBK; 3代表 GB2312 默认为1,该参数必填。
            string inputCharset = "1";
            //接收支付结果的页地址，面该参数一般置为空即可。
            string pageUrl = "";
            //服务器接收支付结果的后台地址，该参数务必填写，不能为空。
            //string bgUrl = PubConstant.MerchantBaseUrl + string.Format("/accountmanage/receive.html?ordernum={0}&uid={1}&pprice={2}&type={3}&callbackurl={4}", ordernum, uid, pprice, type, callbackurl);//Url.PaySiteUrl().Receive(ordernum, status, callbackurl);//PubConstant.BaseUrl + "Landlord/receive?id=" + ordernum.ToString() + "&uid=" + this.GetUserInfo.Id + "&type=" + status + "&pprice=" + pprice;
            string bgUrl = PubConstant.MerchantBaseUrl + string.Format("/accountmanage/receive.html?ordernum={0}&uid={1}&pprice={2}", ordernum, uid, pprice);
            //网关版本，固定值：v2.0,该参数必填。
            string version = "v2.0";
            //语言种类，1代表中文显示，2代表英文显示。默认为1,该参数必填。
            string language = "1";
            //签名类型,该值为4，代表PKI加密方式,该参数必填。
            string signType = "4";
            //支付人姓名,可以为空。
            string payerName = _ServiceContext.CurrentUser.LoginName;
            //支付人联系类型，1 代表电子邮件方式；2 代表手机联系方式。可以为空。
            string payerContactType = "2";
            //支付人联系方式，与payerContactType设置对应，payerContactType为1，则填写邮箱地址；payerContactType为2，则填写手机号码。可以为空。
            string payerContact = _ServiceContext.CurrentUser.Mobile ?? string.Empty;
            //商户订单号，以下采用时间来定义订单号，商户可以根据自己订单号的定义规则来定义该值，不能为空。
            string orderId = ordernum;
            //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试。该参数必填。
            string orderAmount = (Convert.ToDouble(pprice) * 100).ToString();
            //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101，不能为空。
            string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //商品名称，可以为空。
            string productName = pName;
            //商品数量，可以为空。
            string productNum = "1";
            //商品代码，可以为空。
            string productId = ordernum;
            //商品描述，可以为空。
            string productDesc = pDesc;
            //扩展字段1，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
            string ext1 = "";
            //扩展自段2，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
            string ext2 = "";
            string payType = "";
            string bankId = "";
            if (pk == "KQ")
            {
                payType = "10-1";
                //银行代码，如果payType为00，该值可以为空；如果payType为10，该值必须填写，具体请参考银行列表。
                bankId = "";
            }
            else
            {//10
                payType = "10";
                //银行代码，如果payType为00，该值可以为空；如果payType为10，该值必须填写，具体请参考银行列表。
                bankId = pk;
            }
            //同一订单禁止重复提交标志，实物购物车填1，虚拟产品用0。1代表只能提交一次，0代表在支付不成功情况下可以再提交。可为空。
            string redoFlag = "";
            //快钱合作伙伴的帐户号，即商户编号，可为空。
            string pid = "";
            // signMsg 签名字符串 不可空，生成加密签名串
            string signMsg = "";
            #endregion

            //拼接字符串
            string signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset);
            signMsgVal = appendParam(signMsgVal, "pageUrl", pageUrl);
            signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "payerName", payerName);
            signMsgVal = appendParam(signMsgVal, "payerContactType", payerContactType);
            signMsgVal = appendParam(signMsgVal, "payerContact", payerContact);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "productName", productName);
            signMsgVal = appendParam(signMsgVal, "productNum", productNum);
            signMsgVal = appendParam(signMsgVal, "productId", productId);
            signMsgVal = appendParam(signMsgVal, "productDesc", productDesc);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            if (pk != "kq") { signMsgVal = appendParam(signMsgVal, "bankId", bankId); }
            signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag);
            signMsgVal = appendParam(signMsgVal, "pid", pid);

            ///PKI加密
            ///编码方式UTF-8 GB2312  用户可以根据自己系统的编码选择对应的加密方式
            //byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
            X509Certificate2 cert = new X509Certificate2(Server.MapPath("/Views/99bill/KYJFangchan-rsa.pfx"), "11111111", X509KeyStorageFlags.MachineKeySet);
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PrivateKey;
            RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
            byte[] result;
            f.SetHashAlgorithm("SHA1");
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(bytes);
            signMsg = System.Convert.ToBase64String(f.CreateSignature(result)).ToString();

            //建立请求
            string strAction = "https://www.99bill.com/gateway/recvMerchantInfoAction.htm";

            StringBuilder sb = new StringBuilder();
            sb.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + strAction + "' method='post'>");
            sb.Append(" <input type=\"hidden\" name=\"inputCharset\" value=\"" + inputCharset + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"pageUrl\" value=\"" + pageUrl + "\" />");
            sb.Append(" <input type=\"hidden\" name=\"bgUrl\" value=\"" + bgUrl + "\" />");
            sb.Append(" <input type=\"hidden\" name=\"version\" value=\"" + version + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"language\" value=\"" + language + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"signType\" value=\"" + signType + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"signMsg\" value=\"" + signMsg + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"merchantAcctId\" value=\"" + merchantAcctId + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"payerName\" value=\"" + payerName + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"payerContactType\" value=\"" + payerContactType + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"payerContact\" value=\"" + payerContact + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"orderId\" value=\"" + orderId + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"orderAmount\" value=\"" + orderAmount + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"orderTime\" value=\"" + orderTime + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"productName\" value=\"" + productName + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"productNum\" value=\"" + productNum + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"productId\" value=\"" + productId + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"productDesc\" value=\"" + productDesc + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"ext1\" value=\"" + ext1 + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"ext2\" value=\"" + ext2 + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"payType\" value=\"" + payType + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"bankId\" value=\"" + bankId + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"redoFlag\" value=\"" + redoFlag + "\"/>");
            sb.Append(" <input type=\"hidden\" name=\"pid\" value=\"" + pid + "\"/>");
            sb.Append("</form>");
            sb.Append("<script>document.forms['alipaysubmit'].submit();</script>");
            #endregion

            ViewData["sHtmlText"] = sb.ToString();
            return View();
        } 
        #endregion

        #region Receive +ContentResult Receive()
        public ContentResult Receive()
        {
            string ordernum = Request.QueryString["ordernum"].ToString();
            string pprice = Request.QueryString["pprice"].ToString();
            string type = Request.QueryString["status"].ToString();
            string callbackurl = Request.QueryString["callbackurl"].ToString();
            var uid = Auxiliary.ToInt32(Request.QueryString["uid"].ToString());
            // TXOrm.KBApply kbModel = _kbbl.GetKBApplyById(int.Parse(id));
            int strResult = 0;

            //人民币网关账号，该账号为11位人民币网关商户编号+01,该值与提交时相同。
            string merchantAcctId = Request.QueryString["merchantAcctId"].ToString();
            //网关版本，固定值：v2.0,该值与提交时相同。
            string version = Request.QueryString["version"].ToString();
            //语言种类，1代表中文显示，2代表英文显示。默认为1,该值与提交时相同。
            string language = Request.QueryString["language"].ToString();
            //签名类型,该值为4，代表PKI加密方式,该值与提交时相同。
            string signType = Request.QueryString["signType"].ToString();
            //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10,该值与提交时相同。
            string payType = Request.QueryString["payType"].ToString();
            //银行代码，如果payType为00，该值为空；如果payType为10,该值与提交时相同。
            string bankId = Request.QueryString["bankId"].ToString();
            //商户订单号，,该值与提交时相同。
            string orderId = Request.QueryString["orderId"].ToString();
            //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101,该值与提交时相同。
            string orderTime = Request.QueryString["orderTime"].ToString();
            //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试,该值与支付时相同。
            string orderAmount = Request.QueryString["orderAmount"].ToString();
            // 快钱交易号，商户每一笔交易都会在快钱生成一个交易号。
            string dealId = Request.QueryString["dealId"].ToString();
            //银行交易号 ，快钱交易在银行支付时对应的交易号，如果不是通过银行卡支付，则为空
            string bankDealId = Request.QueryString["bankDealId"].ToString();
            //快钱交易时间，快钱对交易进行处理的时间,格式：yyyyMMddHHmmss，如：20071117020101
            string dealTime = Request.QueryString["dealTime"].ToString();
            //商户实际支付金额 以分为单位。比方10元，提交时金额应为1000。该金额代表商户快钱账户最终收到的金额。
            string payAmount = Request.QueryString["payAmount"].ToString();
            //费用，快钱收取商户的手续费，单位为分。
            string fee = Request.QueryString["fee"].ToString();
            //扩展字段1，该值与提交时相同。
            string ext1 = Request.QueryString["ext1"].ToString();
            //扩展字段2，该值与提交时相同。
            string ext2 = Request.QueryString["ext2"].ToString();
            //处理结果， 10支付成功，11 支付失败，00订单申请成功，01 订单申请失败
            string payResult = Request.QueryString["payResult"].ToString();
            //错误代码 ，请参照《人民币网关接口文档》最后部分的详细解释。
            string errCode = Request.QueryString["errCode"].ToString();
            //签名字符串 
            string signMsg = Request.QueryString["signMsg"].ToString();
            string signMsgVal = "";
            signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
            signMsgVal = appendParam(signMsgVal, "version", version);
            signMsgVal = appendParam(signMsgVal, "language", language);
            signMsgVal = appendParam(signMsgVal, "signType", signType);
            signMsgVal = appendParam(signMsgVal, "payType", payType);
            signMsgVal = appendParam(signMsgVal, "bankId", bankId);
            signMsgVal = appendParam(signMsgVal, "orderId", orderId);
            signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
            signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount);
            signMsgVal = appendParam(signMsgVal, "dealId", dealId);
            signMsgVal = appendParam(signMsgVal, "bankDealId", bankDealId);
            signMsgVal = appendParam(signMsgVal, "dealTime", dealTime);
            signMsgVal = appendParam(signMsgVal, "payAmount", payAmount);
            signMsgVal = appendParam(signMsgVal, "fee", fee);
            signMsgVal = appendParam(signMsgVal, "ext1", ext1);
            signMsgVal = appendParam(signMsgVal, "ext2", ext2);
            signMsgVal = appendParam(signMsgVal, "payResult", payResult);
            signMsgVal = appendParam(signMsgVal, "errCode", errCode);

            ///UTF-8编码  GB2312编码  用户可以根据自己网站的编码格式来选择加密的编码方式
            ///byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
            byte[] SignatureByte = Convert.FromBase64String(signMsg);
            X509Certificate2 cert = new X509Certificate2(Server.MapPath("/Views/99bill/99bill.cert.rsa.20140728.cer"), "");
            RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PublicKey.Key;
            rsapri.ImportCspBlob(rsapri.ExportCspBlob(false));
            RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapri);
            byte[] result;
            f.SetHashAlgorithm("SHA1");
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(bytes);

            var issuccess = false;

            if (f.VerifySignature(result, SignatureByte))
            {
                //逻辑处理  写入数据库
                if (payResult == "10")   //成功
                {
                    #region 业务逻辑
                    string message = string.Empty;
                    if (bll.Recharge(Auxiliary.ToInt32(uid), Auxiliary.ToDecimal(pprice), out message))
                    {
                        issuccess = true;
                    }
                    #endregion
                }

            }
            string url = "";

            if (issuccess)
                url = "<result>1</result><redirecturl>" + callbackurl + "</redirecturl>";

            return Content(url);
        }
        #endregion

        #region 在线支付押金

        //功能函数。将变量值不为空的参数组成字符串
        String appendParam(String returnStr, String paramId, String paramValue)
        {
            if (returnStr != "")
            {
                if (paramValue != "")
                {
                    returnStr += "&" + paramId + "=" + paramValue;
                }
            }
            else
            {
                if (paramValue != "")
                {
                    returnStr = paramId + "=" + paramValue;
                }
            }

            return returnStr;
        }
        #endregion

    }

}
