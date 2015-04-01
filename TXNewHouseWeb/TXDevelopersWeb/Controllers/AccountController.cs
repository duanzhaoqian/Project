using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TXModel.Commons;
using TXCommons.bd.cjkjb.webservice;
using TXCommons.user.cjkjb.webservice;
using Webdiyer.WebControls.Mvc;
using Com.Alipay;
using System.Collections.Specialized;
using TXBll.Order;
using TXCommons;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace TXDevelopersWeb.Controllers
{
    public class AccountController : BaseController
    {
        private bool testflag = false;
        private TXBll.Dev.Developer_AccountBll _rbll = new TXBll.Dev.Developer_AccountBll();
        private OperaUserService ouService = new OperaUserService();
        private OrderBll _obll = new OrderBll();
        public ActionResult Index(int? page)
        {
            if (Request.QueryString["second"] != null)
            {
                ViewData["CurrentMenu"] = Convert.ToInt32(Request.QueryString["second"]);
            }
            else
            {
                ViewData["CurrentMenu"] = 61;
            }
            ViewData["LoginName"] = LoginUserInfo.LoginName;
            Paging<TXOrm.Developer_AccountLog> paging = new Paging<TXOrm.Developer_AccountLog>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            TXBll.Dev.Developer_AccountBll _bll = new TXBll.Dev.Developer_AccountBll();
            string[] temp = _bll.GetAccountMoneyAndTotalRechargeById(LoginUserInfo.Id);
            ViewData["AccountMoney"] = temp[0];
            ViewData["TotalRecharge"] = temp[1];
            ViewData["Totaldraw"] = temp[2];
            //
            BaseDataWebService _dbs = new BaseDataWebService();
            var privent = _dbs.SearchProvinceList(1);
            ViewData["privent"] = privent;
            //
            string starttime = DateTime.Now.Year.ToString() + "-01-01";
            string endtime = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(Request.QueryString["starttime"]))
            {
                starttime = Request.QueryString["starttime"].ToString();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["endtime"]))
            {
                endtime = Request.QueryString["endtime"].ToString();
            }
            int type = Convert.ToInt32(Request.QueryString["type"]);
            ViewData["starttime"] = starttime;
            ViewData["endtime"] = endtime;
            ViewData["type"] = type;
            decimal TotalExpenses = 0;//总支出
            decimal TotalIncome = 0;//总收入
            paging = _bll.GetAccountLog(LoginUserInfo.Id, Convert.ToDateTime(starttime), Convert.ToDateTime(endtime), type, paging, out TotalExpenses, out TotalIncome);
            PagedList<TXOrm.Developer_AccountLog> pagelist = new PagedList<TXOrm.Developer_AccountLog>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            ViewData["TotalItemCount"] = pagelist.TotalItemCount;
            ViewData["TotalExpenses"] = TotalExpenses;
            ViewData["TotalIncome"] = TotalIncome;
            if (Request.IsAjaxRequest())
            {
                return PartialView("AccountTable", pagelist);
            }
            else
            {
                return View(pagelist);
            }
        }

        #region 充值提交
        [HttpPost]
        public ActionResult Recharge(FormCollection Form)
        {
            //判断是否为kuaiyoujia域名充值 如果不是 跳转到首页
            string url = Request.Url.Host.ToLower().Substring(Request.Url.Host.IndexOf("."), Request.Url.Host.Length - Request.Url.Host.IndexOf("."));
            if (!GetConfig.Domain.Equals(url))
            {
                return Redirect(TXCommons.GetConfig.BaseUrl);
            }
            ViewData["CurrentMenu"] = 62;
            string price = string.Empty;
            string RechargeNo = string.Empty;
            try
            {
                //string price = "0.01";
                //生成充值编号
                //string RechargeNo = _obll.GetRechargeNo(_ous.GetRechargeMaxId("binPath", "classPath").ToString());
                RechargeNo = _rbll.GetRechargeNo(_rbll.GetRechargeMaxId().ToString());
                ViewData["RechargeNo"] = RechargeNo;

                string Rechargeprice = Convert.ToString(Request.Form["radio"]);
                //其他金额
                string qtMoney = Request.Form["qtMoney"];
                decimal iqtMoney = 0;
                decimal.TryParse(qtMoney, out iqtMoney);
                ViewData["price"] = Rechargeprice;

                switch (Rechargeprice)
                {
                    case "1": price = "200"; break;
                    case "2": price = "400"; break;
                    case "3": price = "600"; break;
                    default:
                        {
                            if (iqtMoney > 0)
                            {
                                price = qtMoney;
                                ViewData["qtMoney"] = qtMoney;
                            }
                            else
                            {
                                price = "200";
                            }
                            break;
                        }
                }
                ViewData["Rechargeprice"] = price;
            }
            catch (Exception ex)
            {
                throw;
            }
            //=================================
            // string bgUrl = TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/receive?id=" + orderID.ToString() + "&price=" + (double)activity.Bond + "&type=" + type;    // ----加站点前缀
            string bgUrl = "www.cjkjb.com";
            string url2 = string.Format("?id={0}&pprice={1}&type=5&bgurl={2}", ViewData["RechargeNo"], ViewData["Rechargeprice"], HttpUtility.UrlEncode(bgUrl));
            ViewData["url2"] = url2;
            Log4netService.RecordLog.RecordException("马波讯", "开发商后台账户管理我要充值：" + url2);
            //=================================
            //==============
            decimal itempprice = 0;
            decimal.TryParse(price, out itempprice);
            if (itempprice > 0)
            {
            }
            else
            {
                return View("Index?second=62");
            }
            //==============
            if (!string.IsNullOrEmpty(price))
                return View("OnlinePay");
            else
                return View("Index?second=62");
        }
        #endregion

        #region 选择支付方式
        public ActionResult OnlinePay(string RechargeNo)
        {
            decimal price = 0;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["price"]) && !string.IsNullOrEmpty(Request.QueryString["rtype"]))
                {
                    RechargeNo = _obll.GetRechargeNo(ouService.GetRechargeMaxId("binPath", "classPath").ToString());
                    string rtype = Request.QueryString["rtype"];
                    decimal.TryParse(Request.QueryString["price"], out price);
                    var ainfo = ouService.GetUserAccountInfo("binPath", "classPath", this.LoginUserInfo.Id);
                    ViewData["Rechargeprice"] = Convert.ToDouble(price);
                    ViewData["qtMoney"] = price;
                    ViewData["RechargeNo"] = RechargeNo;
                    ViewData["canshu"] = "?rtype=" + Request.QueryString["rtype"] + "&return=" + Request.QueryString["return"];
                }
                ViewData["para"] = 52;
                if (null == RechargeNo)
                {
                    return Redirect("~/Account/Index?second=62");
                }
            }
            catch (Exception ex)
            {
                return Redirect("~/Account/Index?second=62");
            }
            return View();
        }
        #endregion

        #region 确定支付
        [HttpPost]
        public ActionResult AlipayOrderConfirm()
        {
            ViewData["CurrentMenu"] = 62;
            string htmltext = string.Empty;
            string RechargeNo = string.Empty;
            try
            {
                int uid = LoginUserInfo.Id;
                if (!string.IsNullOrEmpty(Request.Form["RechargeNo"]))
                {
                    RechargeNo = Request.Form["RechargeNo"];
                }
                else
                {
                    RechargeNo = "-1";
                }

                string tmpPayType = Convert.ToString(Request.Form["banks"]);

                string Rechargeprice = Convert.ToString(Request.Form["Rechargeprice"]);
                //其他金额
                string qtMoney = Convert.ToString(Request.Form["qtMoney"]); ;
                decimal iqtMoney = 0;
                decimal.TryParse(qtMoney, out iqtMoney);
                string price = string.Empty;
                switch (Rechargeprice)
                {
                    case "1": price = "200"; break;
                    case "2": price = "400"; break;
                    case "3": price = "600"; break;
                    default:
                        {
                            if (iqtMoney > 0)
                            {
                                price = qtMoney;
                            }
                            else
                            {
                                price = "200";
                            }
                            break;
                        }
                }
                string logstr1 = string.Format(" price:{0}, RechargeNo:{1}, tmpPayType:{2}  ", price, RechargeNo, tmpPayType);
                Log4netService.RecordLog.RecordException("马波讯", "开发商后台账户管理我要充值-AlipayOrderConfirm：" + logstr1);
                //添加一条记录
                //var f = _ous.AddRechargeInfo("binPath", "classPath", uid, decimal.Parse(price), RechargeNo, 0);
                var f = _rbll.AddRechargeInfo(uid, decimal.Parse(price), RechargeNo, 0);
                if (!f)
                {
                    //添加充值记录失败
                    //支付失败
                    return Redirect("~/Account/Index?second=62");
                }

                if (tmpPayType.Equals("BANK_ZFB"))
                {

                    htmltext = AlipayOrderConfirm_Alipay(RechargeNo);
                }
                else
                {

                    htmltext = AlipayOrderConfirm_Bank(RechargeNo);
                }
                ViewData["str_html"] = htmltext;

                Log4netService.RecordLog.RecordException("马波讯", "开发商后台账户管理我要充值-AlipayOrderConfirm-str_html：" + htmltext);
     
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", "开发商后台账户管理我要充值-AlipayOrderConfirm-Exception：" + ex.Message + ex.StackTrace);
                throw;
            }
            return View();
        }

        #endregion

        #region 纯银行网管支付
        private string AlipayOrderConfirm_Bank(string RechargeNo)
        {
            int tUserId = LoginUserInfo.Id;

            #region 支付银行

            string tmpDefaultbank = Convert.ToString(Request.Form["banks"]);

            #endregion


            #region 请求参数

            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            // "http://www.xxx.com/create_direct_pay_by_user-CSHARP-UTF-8/notify_url.aspx";
            string notify_url = TXCommons.GetConfig.BaseUrl + "Recharge/RechargeAlipayNotify_Bank";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            //"http://www.xxx.com/create_direct_pay_by_user-CSHARP-UTF-8/return_url.aspx";
            string return_url = "";
            if (!string.IsNullOrEmpty(Request.QueryString["rtype"]))
            {
                return_url = TXCommons.GetConfig.BaseUrl + "Account/AlipayReturnPage_Bank?rtype=" + Request.QueryString["rtype"] + "&return=" + Request.QueryString["return"];
            }
            else
            {
                return_url = TXCommons.GetConfig.BaseUrl + "Account/AlipayReturnPage_Bank";
            }
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //卖家支付宝帐户
            //string seller_email = WIDseller_email.Text.Trim();
            string seller_id = Com.Alipay.Config.Partner;
            //必填

            //商户订单号
            string out_trade_no = RechargeNo; // Convert.ToString(orderId);
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = "账户充值";
            //必填

            //付款金额
            string Rechargeprice = Request.Form["Rechargeprice"];
            string qtMoney = Request.Form["qtMoney"];
            decimal iqtMoney = 0;
            decimal.TryParse(qtMoney, out iqtMoney);
            string total_fee = string.Format("{0:00}", Rechargeprice);
            if (testflag)
            {
                switch (Rechargeprice)
                {
                    case "1": total_fee = string.Format("{0:00}", "0.01"); break;
                    case "2": total_fee = string.Format("{0:00}", "0.015"); break;
                    case "3": total_fee = string.Format("{0:00}", "0.030"); break;
                    default: total_fee = string.Format("{0:00}", "0.03"); break;
                }

            }
            else
            {
                switch (Rechargeprice)
                {
                    case "1": total_fee = string.Format("{0:00}", "200"); break;
                    case "2": total_fee = string.Format("{0:00}", "400"); break;
                    case "3": total_fee = string.Format("{0:00}", "600"); break;
                    default:
                        {
                            if (iqtMoney > 0)
                            {
                                total_fee = string.Format("{0:00}", qtMoney);
                            }
                            else
                            {
                                total_fee = string.Format("{0:00}", "200");
                            }
                            break;
                        }
                }
            }
            //必填

            //订单描述

            string body = string.Format("充值编号：{0}  充值时间：", RechargeNo,
                                    string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
            //默认支付方式
            string paymethod = "bankPay";
            //必填
            //默认网银
            string defaultbank = tmpDefaultbank;
            //必填，银行简码请参考接口技术文档

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_id", seller_id);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("extra_common_param", tUserId.ToString());


            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

            #endregion

            return sHtmlText;
        }
        #endregion

        #region 支付宝支付
        /// <summary>
        /// 支付宝支付
        /// </summary>
        private string AlipayOrderConfirm_Alipay(string RechargeNo)
        {
            int tUserId = LoginUserInfo.Id;
            #region 请求参数

            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //支付类型
            string payment_type = "1";
            //必填，不能修改
            //服务器异步通知页面路径
            // "http://www.xxx.com/create_direct_pay_by_user-CSHARP-UTF-8/notify_url.aspx";
            string notify_url = TXCommons.GetConfig.BaseUrl + "Recharge/RechargeAlipayNotify_Alipay.aspx";
            //需http://格式的完整路径，不能加?id=123这类自定义参数

            //页面跳转同步通知页面路径
            //"http://www.xxx.com/create_direct_pay_by_user-CSHARP-UTF-8/return_url.aspx";
            string return_url = "";
            if (!string.IsNullOrEmpty(Request.QueryString["rtype"]))
            {
                return_url = TXCommons.GetConfig.BaseUrl + "Account/AlipayReturnPage_Alipay?rtype=" + Request.QueryString["rtype"] + "&return=" + Request.QueryString["return"];
            }
            else
            {
                return_url = TXCommons.GetConfig.BaseUrl + "Account/AlipayReturnPage_Alipay";
            }
            //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/

            //卖家支付宝帐户
            //string seller_email = WIDseller_email.Text.Trim();
            string seller_id = Com.Alipay.Config.Partner;
            //必填

            //商户订单号
            string out_trade_no = RechargeNo; //Convert.ToString(orderId);
            //商户网站订单系统中唯一订单号，必填

            //订单名称
            string subject = "充值";
            //必填

            //付款金额
            //string total_fee = string.Format("{0:00}", order.TotalPrice);
            string Rechargeprice = Request.Form["Rechargeprice"];
            string qtMoney = Request.Form["qtMoney"];
            decimal iqtMoney = 0;
            decimal.TryParse(qtMoney, out iqtMoney);
            string total_fee = string.Format("{0:00}", Rechargeprice);

            if (testflag)
            {
                switch (Rechargeprice)
                {
                    case "1": total_fee = string.Format("{0:00}", "0.01"); break;
                    case "2": total_fee = string.Format("{0:00}", "0.015"); break;
                    case "3": total_fee = string.Format("{0:00}", "0.030"); break;
                    default: total_fee = string.Format("{0:00}", "0.03"); break;
                }
            }
            else
            {
                switch (Rechargeprice)
                {
                    case "1": total_fee = string.Format("{0:00}", "200"); break;
                    case "2": total_fee = string.Format("{0:00}", "400"); break;
                    case "3": total_fee = string.Format("{0:00}", "600"); break;
                    default:
                        {
                            if (iqtMoney > 0)
                            {
                                total_fee = string.Format("{0:00}", qtMoney);
                            }
                            else
                            {
                                total_fee = string.Format("{0:00}", "200");
                            }
                            break;
                        }
                }
            }
            //total_fee = string.Format("{0:00}", "0.01");
            //必填

            //订单描述
            //house = rhbll.GetS_HouseByHouseId(order.ShId, "Other");
            //houseOther = _longRentHouseBll.GetLongHouseOtherByHouseId(order.ShId);

            string body = string.Format("充值编号：{0}  充值时间：", RechargeNo,
                                        string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "create_direct_pay_by_user");
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_id", seller_id);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);

            sParaTemp.Add("extra_common_param", tUserId.ToString());

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");

            #endregion

            return sHtmlText;
        }
        #endregion

        #region  获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
        #endregion

        #region  更改状态
        /// <summary>
        /// 更改状态
        /// </summary>
        /// <param name="out_trade_no">充值编号</param>
        /// <param name="Status">状态</param>
        public void RechargeOrderPay(string out_trade_no)
        {
            try
            {
                string price = Request.QueryString["total_fee"];
                string uid = Request.QueryString["extra_common_param"];
                string[] temp = _rbll.GetAccountMoneyAndTotalRechargeById(Convert.ToInt32(uid));
                if (temp.Length != 0)
                {
                    _rbll.RechargeChangeState(out_trade_no, 1, decimal.Parse(price));
                }
                else
                {
                    _rbll.RechargeChangeState(out_trade_no, 2, decimal.Parse(price));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RechargeOrderPayNew(string out_trade_no, string price)
        {
            try
            {
                string[] temp = _rbll.GetAccountMoneyAndTotalRechargeById(LoginUserInfo.Id);
                if (temp.Length != 0)
                {
                    _rbll.RechargeChangeState(out_trade_no, 1, decimal.Parse(price));
                }
                else
                {
                    _rbll.RechargeChangeState(out_trade_no, 2, decimal.Parse(price));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region  支付后立刻回调页面 支付宝
        /// <summary>
        /// 支付后立刻回调页面 支付宝
        /// </summary>
        /// <returns></returns>
        public ActionResult AlipayReturnPage_Alipay()
        {
            try
            {
                SortedDictionary<string, string> sPara = GetRequestGet();

                string out_trade_no = "-1";

                if (sPara.Count > 0) //判断是否有带返回参数
                {
                    Notify aliNotify = new Notify();
                    bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"],
                                                         Request.QueryString["sign"]);

                    if (verifyResult) //验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //请在这里加上商户的业务逻辑程序代码


                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                        //充值编号

                        out_trade_no = Request.QueryString["out_trade_no"];

                        //支付宝交易号

                        string trade_no = Request.QueryString["trade_no"];

                        //交易状态
                        string trade_status = Request.QueryString["trade_status"];



                        if (Request.QueryString["trade_status"] == "TRADE_FINISHED" ||
                            Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序

                            //支付成功
                            string url = "";
                            if (!string.IsNullOrEmpty(Request.QueryString["rtype"]))
                            {//动作充值，并扣款
                                string rtype = Request.QueryString["rtype"];

                                string[] param = string.IsNullOrEmpty(Request.QueryString["return"]) ? new string[1] { "" } : Request.QueryString["return"].Split('_');
                            }
                            if (!string.IsNullOrEmpty(url))
                            {
                                return Redirect(url);
                            }
                            return RedirectToAction("AlipayOrderSuc");
                        }
                        else
                        {
                            //支付失败
                            return Redirect("~/Account/Index?second=62");
                        }

                        //打印页面
                        //Response.Write("验证成功<br />");
                        //return RedirectToAction("OrderList");

                        //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    else //验证失败
                    {
                        //Response.Write("验证失败");
                        //支付失败

                        //RechargeOrderPay(out_trade_no, 2);

                        return Redirect("~/Account/Index?second=62");
                    }
                }
                else
                {
                    //支付失败
                    //Response.Write("无返回参数");
                    return Redirect("~/Account/Index?second=62");
                }
            }
            catch (Exception)
            {
                return Redirect("~/Account/Index?second=62");
            }
        }

        #endregion

        #region  支付后立刻回调页面 纯网管
        /// <summary>
        /// 支付后立刻回调页面 纯网管
        /// </summary>
        /// <returns></returns>
        public ActionResult AlipayReturnPage_Bank()
        {
            try
            {
                SortedDictionary<string, string> sPara = GetRequestGet();

                //用户编号
                int uid = LoginUserInfo.Id;
                string out_trade_no = "-1";

                if (sPara.Count > 0) //判断是否有带返回参数
                {
                    Notify aliNotify = new Notify();
                    bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"],
                                                         Request.QueryString["sign"]);

                    if (verifyResult) //验证成功
                    {
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //请在这里加上商户的业务逻辑程序代码

                        //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                        //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                        //商户订单号
                        out_trade_no = Request.QueryString["out_trade_no"];

                        //支付宝交易号
                        string trade_no = Request.QueryString["trade_no"];

                        //交易状态
                        string trade_status = Request.QueryString["trade_status"];

                        if (Request.QueryString["trade_status"] == "TRADE_FINISHED" ||
                            Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                        {
                            //判断该笔订单是否在商户网站中已经做过处理
                            //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                            //如果有做过处理，不执行商户的业务程序
                            RechargeOrderPay(out_trade_no);
                            return RedirectToAction("AlipayOrderSuc");
                        }
                        else
                        {
                            return Redirect("~/Account/Index?second=62");
                        }

                        //打印页面
                        // Response.Write("验证成功<br />");
                        //return RedirectToAction("OrderList");

                        //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    else //验证失败
                    {
                        //RechargeOrderPay(out_trade_no, 2);
                        //Response.Write("无返回参数");
                        return Redirect("~/Account/Index?second=62");
                    }
                }
                else
                {
                    //Response.Write("无返回参数");
                    return Redirect("~/Account/Index?second=62");
                }
            }
            catch (Exception ex)
            {
                return Redirect("~/Account/Index?second=62");
            }
        }

        #endregion

        #region  支付成功
        /// <summary>
        /// 支付成功
        /// </summary>
        /// <returns></returns>
        public ActionResult AlipayOrderSuc()
        {
            return View();
        }

        #endregion

        #region 获取城市
        public ActionResult GetCityList()
        {
            BaseDataWebService bdwService = new BaseDataWebService();
            string pid = Request.QueryString["pid"];
            var List = bdwService.SearchCityList(Convert.ToInt32(pid));
            return Json(List, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 申请提现
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Withdrawal()
        {
            decimal drawMoney = Convert.ToDecimal(Request.Form["drawMoney"]);//提现金额
            int type = Convert.ToInt32(Request.Form["receiveType"]);//收款方式
            string Name = Request.Form["reallyname"];//真实姓名
            string zfbaccount = Request.Form["zfbaccount"];//支付宝账户
            string province = Request.Form["province"];
            string city = Request.Form["city"];
            string bank = Request.Form["bank"];//开户银行
            string bankson = Request.Form["bankson"];//开户支行
            string accountname = Request.Form["accountname"];//户名
            string bankNo = Request.Form["bankno"];//银行账户(卡号)
            TXOrm.Developer_WithdrawCash model = new TXOrm.Developer_WithdrawCash();
            model.DeveloperId = LoginUserInfo.Id;
            model.Price = drawMoney;
            model.CreateTime = DateTime.Now;
            model.AdminId = 0;
            model.AdminName = "";
            model.Status = 0;
            model.ReceiveType = type;
            if (type == 0)
            {
                model.RealName = Name;
                model.ALiPayAccount = zfbaccount;
                model.CityId = 0;
                model.CityName = "";
                model.ProvinceId = 0;
                model.ProvinceName = "";
                model.BankName = "";
                model.PubBankName = "";
                model.BankAccount = "";
            }
            else
            {
                model.RealName = accountname;
                model.ALiPayAccount = "";
                model.ProvinceId = Convert.ToInt32(province.Split(',')[0]);
                model.ProvinceName = province.Split(',')[1];
                model.CityId = Convert.ToInt32(city.Split(',')[0]); ;
                model.CityName = city.Split(',')[1];
                model.BankName = bank;
                model.PubBankName = bankson;
                model.BankAccount = bankNo;
            }
            _rbll.DrawCash(model);
            return RedirectToAction("Index");
        }
        #endregion



        public ActionResult Send()
        {
            var id = Request["id"];
            var price = Request["price"];
            string pName = LoginUserInfo.LoginName;
            string pDesc = "";
            string orderNum = "";

            try
            {
                string logstr1 = string.Format("id:{0} price:{1}  LoginUserInfo.LoginName:{2}", id, price, pName);
                Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-Send" + logstr1);
            }
            catch (Exception)
            {

            }

            //添加一条记录
            var b = _rbll.AddRechargeInfo(LoginUserInfo.Id, decimal.Parse(price), id, 0);

            if (!b)
            {
                //添加充值记录失败
                //支付失败
                return Redirect("~/Account/Index?second=62");
            }

            try
            {
                #region 第三方支付页
                #region 参数说明
                //人民币网关账号，该账号为11位人民币网关商户编号+01,该参数必填。
                string merchantAcctId = "1002290589901";
                //编码方式，1代表 UTF-8; 2 代表 GBK; 3代表 GB2312 默认为1,该参数必填。
                string inputCharset = "1";
                //接收支付结果的页面地址，该参数一般置为空即可。
                string pageUrl = "";
                //服务器接收支付结果的后台地址，该参数务必填写，不能为空。
                string bgUrl = TXCommons.GetConfig.BaseUrl + "/Account/receive?id=" + id + "&price=";    // ----加站点前缀
                //网关版本，固定值：v2.0,该参数必填。
                string version = "v2.0";
                //语言种类，1代表中文显示，2代表英文显示。默认为1,该参数必填。
                string language = "1";
                //签名类型,该值为4，代表PKI加密方式,该参数必填。
                string signType = "4";
                //支付人姓名,可以为空。
                string payerName = pName;               // ----支付人姓名
                //支付人联系类型，1 代表电子邮件方式；2 代表手机联系方式。可以为空。
                string payerContactType = "2";        // ----
                //支付人联系方式，与payerContactType设置对应，payerContactType为1，则填写邮箱地址；payerContactType为2，则填写手机号码。可以为空。
                string payerContact = "";
                //商户订单号，以下采用时间来定义订单号，商户可以根据自己订单号的定义规则来定义该值，不能为空。
                string orderId = id;
                //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试。该参数必填。
                string orderAmount = (Convert.ToDouble(price) * 100).ToString();              // ----
                //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101，不能为空。
                string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //商品名称，可以为空。
                string productName = pName;
                //商品数量，可以为空。
                string productNum = "1";
                //商品代码，可以为空。
                string productId = id;
                //商品描述，可以为空。
                string productDesc = pDesc;
                //扩展字段1，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
                string ext1 = "";
                //扩展自段2，商户可以传递自己需要的参数，支付完快钱会原值返回，可以为空。
                string ext2 = "";
                //支付方式，一般为00，代表所有的支付方式。如果是银行直连商户，该值为10，必填。
                string payType = "00";
                //银行代码，如果payType为00，该值可以为空；如果payType为10，该值必须填写，具体请参考银行列表。
                string bankId = "";
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
                signMsgVal = appendParam(signMsgVal, "redoFlag", redoFlag);
                signMsgVal = appendParam(signMsgVal, "pid", pid);

                try
                {
                    string log = string.Format("signMsgVal:{0}", signMsgVal);
                    Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-Send" + signMsgVal);
                }
                catch (Exception)
                {

                }

                //PKI加密
                //编码方式UTF-8 GB2312  用户可以根据自己系统的编码选择对应的加密方式
                //byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
                X509Certificate2 cert = new X509Certificate2(Server.MapPath("~/Views/99bill/KYJFangchan-rsa.pfx"), "11111111", X509KeyStorageFlags.MachineKeySet);
                RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PrivateKey;
                RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
                byte[] result;
                f.SetHashAlgorithm("SHA1");
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                result = sha.ComputeHash(bytes);
                signMsg = System.Convert.ToBase64String(f.CreateSignature(result));

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

                try
                {
                    string logstr11 = string.Format("sHtmlText:{0}", sb.ToString());
                    Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-Send" + logstr11);
                }
                catch (Exception)
                {

                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-Send" + "PayOrder", ex);
            }
            return View();
        }

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


        public ActionResult receive()
        {
            string id = Request.QueryString["id"];
            string price = Request.QueryString["price"];

            try
            {
                string logstr11 = string.Format("id:{0}  price:{1}", id,price);
                Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-receive" + logstr11);
            }
            catch (Exception)
            {
            }
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


            try
            {
                try
                {
                    string log = string.Format("signMsgVal:{0}", signMsgVal);
                    Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-receive" + signMsgVal);
                }
                catch (Exception)
                {

                }
                //UTF-8编码  GB2312编码  用户可以根据自己网站的编码格式来选择加密的编码方式
                //byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
                byte[] SignatureByte = Convert.FromBase64String(signMsg);
                X509Certificate2 cert = new X509Certificate2(Server.MapPath("~/Views/99bill/99bill.cert.rsa.20140728.cer"), "");
                RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PublicKey.Key;
                rsapri.ImportCspBlob(rsapri.ExportCspBlob(false));
                RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapri);
                byte[] result;
                f.SetHashAlgorithm("SHA1");
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                result = sha.ComputeHash(bytes);

                if (f.VerifySignature(result, SignatureByte))
                {
                    //逻辑处理  写入数据库
                    if (payResult == "10")
                    {
                        try
                        {
                            string log123 = string.Format("  id:{0} price:{1} ", id, price);
                            Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-receive-RechargeOrderPayNew" + log123);
                        }
                        catch (Exception)
                        {
                        }
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        RechargeOrderPayNew(id, price);
                        strResult = 1;
                    }
                    else
                    {
                        strResult = 2;
                    }
                }
                else
                {
                    strResult = 2;
                }

            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", "开发商后台-账户充值-receive" + "PayOrder", ex);
            }

            if (strResult == 1)
            {
                return RedirectToAction("AlipayOrderSuc");
            }
            else
            {
                return Redirect("~/Account/Index?second=62");
            }
        }
    }
}
