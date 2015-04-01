using System;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TXModel.Web;

namespace TXNewHouseWeb.Controllers
{
    public class PayOrderController : BaseController
    {
        /// <summary>
        /// 活动BLL类
        /// </summary>
        readonly TXBll.Activitys.ActivitysBll _activitysbll = new TXBll.Activitys.ActivitysBll();

        /// <summary>
        /// 楼盘BLL类-前台
        /// </summary>
        readonly TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

        public ActionResult OnlinePayDeposit()
        {
            int type = 0;
            int.TryParse(Request.QueryString["type"], out type);
            int orderID = 0;
            int.TryParse(Request.QueryString["id"], out orderID);
            try
            {
                string logstr00 = string.Format(" orderID：{0},type：{1} ", orderID, type);
                Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口OnlinePayDeposit方法：" + logstr00);
            }
            catch { }
            //获取订单对象
            var order = _activitysbll.GetActivityOrderByID(orderID);
            //订单不存在
            if (order == null)
            {
                return Redirect("~/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单不存在！"));
            }
            //判断是否已经支付
            if (order.BondStatus > 0)
            {
                return Redirect("~/PayOrder/PaySuccess?id=" + orderID);
            }
            //获得活动信息
            TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

            //活动不存在
            if (activity == null)
            {
                return Redirect("~/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单活动不存在！"));
            }
            //============
            //id	// 订单编号
            //pprice	// 支付金额
            //type=5	// 支付类型编号：5
            //bgurl	// 回调地址(需要进行url编码)
            string bgUrl = TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/receive?id=" + orderID.ToString() + "&price=" + (double)activity.Bond + "&type=" + type;    // ----加站点前缀
            string url = string.Format("?id={0}&pprice={1}&type=5&bgurl={2}", order.OrderNo, (double)activity.Bond, HttpUtility.UrlEncode(bgUrl));
            try
            {
                Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口OnlinePayDeposit：" + url);
            }
            catch { }
            //=============
            ViewData["url"] = url;
            return View();
        }


        #region 在线支付
        public ActionResult Send()
        {
            var type = Request["type"];
            var id = Request["id"];
            var price = Request["price"];
            string pName = "";
            string pDesc = "";
            string orderNum = "";

            try
            {
                //获取订单对象
                int orderID = 0;
                int.TryParse(Request["id"], out orderID);
                var order = _activitysbll.GetActivityOrderByID(orderID);

                //订单不存在
                if (order == null)
                {
                    return Redirect("~/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单不存在！"));
                }

                //获得活动信息
                TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

                //活动不存在
                if (activity == null)
                {
                    return Redirect("~/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单活动不存在！"));
                }

                //判断是否支付 已支付跳转到成功页
                if (order.BondStatus > 0)
                {
                    return Redirect("~/PayOrder/PaySuccess?id=" + orderID);
                }

                #region 第三方支付页
                #region 参数说明
                //人民币网关账号，该账号为11位人民币网关商户编号+01,该参数必填。
                string merchantAcctId = "1002290589901";
                //编码方式，1代表 UTF-8; 2 代表 GBK; 3代表 GB2312 默认为1,该参数必填。
                string inputCharset = "1";
                //接收支付结果的页面地址，该参数一般置为空即可。
                string pageUrl = "";
                //服务器接收支付结果的后台地址，该参数务必填写，不能为空。
                string bgUrl = TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/receive?id=" + id.ToString() + "&price=" + price + "&type=" + type;    // ----加站点前缀
                //网关版本，固定值：v2.0,该参数必填。
                string version = "v2.0";
                //语言种类，1代表中文显示，2代表英文显示。默认为1,该参数必填。
                string language = "1";
                //签名类型,该值为4，代表PKI加密方式,该参数必填。
                string signType = "4";
                //支付人姓名,可以为空。
                string payerName = order.BidRealName;               // ----支付人姓名
                //支付人联系类型，1 代表电子邮件方式；2 代表手机联系方式。可以为空。
                string payerContactType = "2";        // ----
                //支付人联系方式，与payerContactType设置对应，payerContactType为1，则填写邮箱地址；payerContactType为2，则填写手机号码。可以为空。
                string payerContact = order.BidUserMobile;
                //商户订单号，以下采用时间来定义订单号，商户可以根据自己订单号的定义规则来定义该值，不能为空。
                string orderId = order.OrderNo;
                //订单金额，金额以“分”为单位，商户测试以1分测试即可，切勿以大金额测试。该参数必填。
                string orderAmount = (Convert.ToDouble(activity.Bond) * 100).ToString();              // ----
                //订单提交时间，格式：yyyyMMddHHmmss，如：20071117020101，不能为空。
                string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                //商品名称，可以为空。
                string productName = pName;
                //商品数量，可以为空。
                string productNum = "1";
                //商品代码，可以为空。
                string productId = id.ToString();
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
                    Log4netService.RecordLog.RecordException("马波讯", signMsgVal);
                }
                catch (Exception)
                {

                }

                ///PKI加密
                ///编码方式UTF-8 GB2312  用户可以根据自己系统的编码选择对应的加密方式
                //byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
                X509Certificate2 cert = new X509Certificate2(Server.MapPath(TXCommons.GetConfig.GetSiteRoot + "/Views/99bill/KYJFangchan-rsa.pfx"), "11111111", X509KeyStorageFlags.MachineKeySet);
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
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("马波讯", "PayOrder", ex);
            }
            return View();
        }

        public ActionResult receive()
        {
            try
            {
                string id = Request.QueryString["id"].ToString();
                string price = Request.QueryString["price"].ToString();
                string type = Request.QueryString["type"].ToString();
                string logstr = string.Format(" id：{0}，price：{1}，type：{2} ", id, price, type);
                Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口receive方法接受参数回传：" + logstr);
                int orderID = 0;
                int.TryParse(id, out orderID);
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
                    string log = string.Format("signMsgVal:{0}", signMsgVal);
                    Log4netService.RecordLog.RecordException("马波讯", signMsgVal);
                }
                catch { }
                ///UTF-8编码  GB2312编码  用户可以根据自己网站的编码格式来选择加密的编码方式
                ///byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(signMsgVal);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(signMsgVal);
                byte[] SignatureByte = Convert.FromBase64String(signMsg);
                X509Certificate2 cert = new X509Certificate2(Server.MapPath(TXCommons.GetConfig.GetSiteRoot + "/Views/99bill/99bill.cert.rsa.20140728.cer"), "");
                RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)cert.PublicKey.Key;
                rsapri.ImportCspBlob(rsapri.ExportCspBlob(false));
                RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapri);
                byte[] result;
                f.SetHashAlgorithm("SHA1");
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                result = sha.ComputeHash(bytes);
                if (f.VerifySignature(result, SignatureByte))
                {
                    try
                    {
                        Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口receive方法-VerifySignature方法成功");
                    }
                    catch { }
                    //逻辑处理  写入数据库
                    if (payResult == "10")
                    {
                        try
                        {
                            Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口receive方法-payResult==10");
                        }
                        catch { }
                        switch (type)
                        {
                            //1:摇号支付，2：砍价、竞价保证金支付，3：团购，4：排名购房，5：秒杀、一口价、打折
                            case "1":
                                break;
                            case "2":
                                _activitysbll.UpdateActivityOrderSuc(orderID);
                                _activitysbll.AddUserAccountInfo(TXCommons.Util.ToInt(orderId));
                                break;
                            case "3":
                                break;
                            case "4":
                                break;
                            case "5":
                                _activitysbll.UpdateMSOrderSuc(orderID);
                                _activitysbll.AddUserAccountInfo(TXCommons.Util.ToInt(orderId));
                                break;
                        }
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
                string url = "";
                if (strResult == 1)
                {
                    // url = "<result>1</result>" + "<redirecturl>http://beijing.kyjob.com/" + TXCommons.GetConfig.GetSiteRoot +"/PayOrder/PaySuccess?id=" + id + "&type=" + type + "</redirecturl>";// 
                    url = "<result>1</result>" + "<redirecturl>" + TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/PaySuccess?id=" + id + "&type=" + type + "</redirecturl>";// 
                    //return Redirect("~/PayOrder/PaySuccess?id=" + id + "&type=" + type);
                    try
                    {
                        string log0001 = string.Format("Content(url):{0}", url);
                        Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口receive方法" + log0001);
                    }
                    catch { }
                    return Content(url);
                }
                else
                {
                    url = "<result>1</result>" + "<redirecturl>" + TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单处理失败！") + "</redirecturl>";// 
                    // url = "<result>1</result>" + "<redirecturl>http://" + TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单处理失败！") + "</redirecturl>";// 
                    try
                    {
                        string log0002 = string.Format("Content(url):{0}", url);
                        Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口receive方法" + log0002);
                    }
                    catch { }
                    return Content(url);
                    //return Redirect("~/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单处理失败！"));
                }
            }
            catch (Exception ex)
            {
                string url = string.Empty;
                Log4netService.RecordLog.RecordException("马波讯", "新房活动支付保证金诚意金统一调用二手房支付接口receive方法", ex);
                url = "<result>1</result>" + "<redirecturl>" + TXCommons.GetConfig.BaseUrl + TXCommons.GetConfig.GetSiteRoot + "/PayOrder/PaySuccess?msg=" + HttpUtility.UrlEncode("订单处理失败！") + "</redirecturl>";// 
                return Content(url);
            }
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
        #endregion

        #region 支付完成

        /// <summary>
        /// 支付成功页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PaySuccess()
        {
            int orderID = 0;
            int.TryParse(Request.QueryString["id"], out orderID);

            if (Request["Msg"] != null)
            {
                ViewData["msg"] = HttpUtility.UrlDecode(Request["Msg"].ToString());
                return View();
            }

            //获取订单对象
            var order = _activitysbll.GetActivityOrderByID(orderID);

            //订单不存在
            if (order == null)
            {
                ViewData["msg"] = "订单不存在！";
                return View();
            }

            //获得活动信息
            TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

            //活动不存在
            if (activity == null)
            {
                ViewData["msg"] = "订单活动不存在！";
                return View();
            }

            //获得房间详情
            PremisesHouse house = _premisesbll.GetPremisesHouseById(order.HouseId);

            ViewData["Order"] = order;
            ViewData["Activity"] = activity;
            ViewData["House"] = house;
            return View();
        }

        #endregion
    }
}
