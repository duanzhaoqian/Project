using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Com.Alipay;
using System.Collections.Specialized;
using TXBll.Dev;
namespace TXDevelopersWeb.Controllers
{
    public class RechargeController : Controller
    {
        Developer_AccountBll _rbll = new  Developer_AccountBll ();

        #region  支付宝异步调用(支付宝)
        /// <summary>
        /// 支付宝异步调用(支付宝)
        /// </summary>
        public ActionResult RechargeAlipayNotify_Alipay()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0) //判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult) //验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.Form["out_trade_no"];

                    //支付宝交易号

                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    string trade_status = Request.Form["trade_status"];


                    if (Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。

                        RechargeOrderPaySuc(out_trade_no);

                    }
                    else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。

                        RechargeOrderPaySuc(out_trade_no);

                    }
                    else
                    {
                        //Response.Write("充值发生错误");
                        return View();
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    //Response.Write("success");  //请不要修改或删除
                    ViewData["status"] = "success";
                    return View();

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else //验证失败
                {
                    ViewData["status"] = "fail";
                    return View();

                }
            }
            else
            {
                ViewData["status"] = "无通知参数";
                return View();
            }
        }

        #endregion

        #region  银行异步调用
        /// <summary>
        /// 银行异步调用()
        /// </summary>
        public void RechargeAlipayNotify_Bank()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0) //判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult) //验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];

                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    string trade_status = Request.Form["trade_status"];

                    if (Request.Form["trade_status"] == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。

                        RechargeOrderPaySuc(out_trade_no);

                    }
                    else if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。

                        RechargeOrderPaySuc(out_trade_no);

                    }
                    else
                    {
                        Response.Write("充值出错");
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    Response.Write("success"); //请不要修改或删除

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else //验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        #endregion

        #region  充值成功
        /// <summary>
        /// 充值成功
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        private void RechargeOrderPaySuc(string out_trade_no)
        {
            // Status	Int	否	充值状态
            //0 待充值
            //1 充值成功
            //2 充值失败
            try
            {
                string price = Request.Form["total_fee"];
                string uid = Request.Form["extra_common_param"];
                 string[] temp = _rbll.GetAccountMoneyAndTotalRechargeById(Convert.ToInt32(uid));
                 if (temp.Length != 0)
                 {
                     bool f = _rbll.RechargeChangeState(out_trade_no, 1, decimal.Parse(price));
                 }
                 else
                 {
                     bool f = _rbll.RechargeChangeState(out_trade_no, 2, decimal.Parse(price));
                 }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region  获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        #endregion
    }
}
