using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Services.Description;
using KYJ.ZS.Models.OrderGoodses;
using KYJ.ZS.Models.RentalGoodses;

namespace KYJ.ZS.Commons
{
    public class MerchantSiteUrlHelper
    {
        private static readonly MerchantSiteUrlHelper instance = new MerchantSiteUrlHelper();

        private static string BaseUrl;

        internal static MerchantSiteUrlHelper Instance
        {
            get
            {
                if (string.IsNullOrEmpty(BaseUrl))
                    BaseUrl = PubConstant.MerchantBaseUrl;
                return instance;
            }
        }

        private MerchantSiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal MerchantSiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #region URL集装箱

        #region Author:zhuzh Date:2014-04-30 Desc:首页、登录页等
        /// <summary>
        /// 商户后台首页
        /// </summary>
        public string Index
        {
            get { return BaseUrl + this.UrlHelper.Content("~"); }
        }
        /// <summary>
        /// 商户后台登录
        /// </summary>
        public string Login
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "login", "common"); }
        }
        /// <summary>
        /// 商户后台验证码
        /// </summary>
        public string Common_Code
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "code", "common"); }
        }

        #endregion

        #region 作者：cheny 时间：2013-05-23 简介：订单和银行账户相关
        /// <summary>
        /// 出租的订单
        /// </summary>
        public string RentalOrder
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "rentalorder", "transmanage"); }
            //get { return "http://localhost:18202" + this.UrlHelper.RouteAction("Default", "rentalorder", "transmanage"); }
        }
        /// <summary>
        /// 代售的订单
        /// </summary>
        public string BookingOrder
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "bookingorder", "transmanage"); }
        }
        /// <summary>
        /// 改变订单状态
        /// </summary>
        public string ChangeOrderStatus
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "changeorderstatus", "transmanage"); }
            //get { return "http://localhost:18202" + this.UrlHelper.RouteAction("Default", "changeorderstatus", "transmanage"); }

        }
        /// <summary>
        /// 站内信
        /// </summary>
        public string SiteNotice
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "sitenotice", "message"); }
        }
        /// <summary>
        /// 站内信已读操作
        /// </summary>
        public string IsRead
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "isread", "message"); }
        }

        /// <summary>
        /// 申请提现
        /// </summary>
        public string ApplyFor
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "applyfor", "accountmanage"); }
        }
        /// <summary>
        /// 确认提现
        /// </summary>
        public string Apply
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "apply", "accountmanage"); }
        }
        /// <summary>
        /// 账户冻结信息
        /// </summary>
        public string FreezeInfo
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "freezeinfo", "accountmanage"); }
        }

        /// <summary>
        /// 流水明细
        /// </summary>
        public string WaterSubsidiary
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "watersubsidiary", "operationlog"); }
        }
        /// <summary>
        /// 管理银行账户
        /// </summary>
        public string ManageBank
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "managebank", "accountmanage"); }
        }
        /// <summary>
        /// 添加银行账户
        /// </summary>
        public string AddBankAccount
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "addbankaccount", "accountmanage"); }
        }
        /// <summary>
        /// 删除银行账户
        /// </summary>
        public string DelBankAccount
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delbankaccount", "accountmanage"); }
        }
        /// <summary>
        /// 修改银行账户
        /// </summary>
        public string EditBankAccount
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "editbankaccount", "accountmanage"); }
        }
        /// <summary>
        /// 判断用户输入密码是否正确
        /// </summary>
        public string CheckPwd
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "checkpwd", "merchantmanage"); }
        }
        /// <summary>
        /// 修改商户登录密码
        /// </summary>
        public string ModifyPwd
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "modifypwd", "merchantmanage"); }
        }
        /// <summary>
        /// 订单管理，租用中的订单
        /// </summary>
        /// <param name="rentOrderPms"></param>
        /// <returns></returns>
        public string OrderManager(int areaType)
        {
            return BaseUrl + UrlHelper.RouteAction("Default", "ordermanager", "order", new { areaType = areaType });
        }
        /// <summary>
        /// 充值
        /// </summary>
        public string Recharge
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "recharge", "accountmanage"); }
        }
        /// <summary>
        /// 发送充值请求
        /// </summary>
        public string Send
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "send", "accountmanage"); }
        }

        /// <summary>
        /// 接收支付后请求
        /// </summary>
        /// <param name="ordernum">订单号</param>
        /// <param name="status">支付类型：0 未知，1 订单支付，2 充值</param>
        /// <param name="callbackurl">回调URL</param>
        public string Receive(string ordernum, int status, string callbackurl)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "receive", "accountmanage", new { ordernum = ordernum, status = status, callbackurl = callbackurl });
        }
        #endregion

        #region 作者：wangyu 时间：2013-04-24 简介：类目相关
        /// <summary> 
        /// 描述：类目地址，默认根类目
        /// </summary>
        public string Category_Index
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "index", "category"); }
        }
        /// <summary> 
        /// 描述：类目地址
        /// </summary>
        /// <param name="id">商品Id</param>
        /// <returns></returns>
        public string Category_GetSonCategory(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "getsoncategory", new { id = id });
        }

        public string RentalGoodses_PublishGoods
        {
            get
            {
                return BaseUrl + this.UrlHelper.RouteAction("Default", "publishgoods", "rentalgoodses");
            }
        }

        public string RentalGoodses_SavePublishGoods
        {
            get
            {
                return BaseUrl + this.UrlHelper.RouteAction("Default", "savepublishgoods", "rentalgoodses");
            }
        }

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns></returns>
        public string RentalGoodses_UpdateGoods(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "updategoods", "rentalgoodses", new { id = id });
        }
        public string RentalGoodses_SaveUpdateGoods
        {
            get
            {
                return BaseUrl + this.UrlHelper.RouteAction("Default", "saveupdategoods", "rentalgoodses");
            }
        }


        #endregion

        #region 作者：maq 时间：2014年4月28日14:56:37  描述：商户 商品管理相关
        /// <summary>
        /// 获取商品图片地址
        /// </summary>
        public string RentalGoodses_GetGoodsPic
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "getgoodspic", "rentalgoodses"); }
        }
        /// <summary>
        /// 商品上架地址
        /// </summary>
        public string RentalGoodses_Shelves
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "shelves", "rentalgoodses"); }
        }
        //商品下架地址
        public string RentalGoodses_ShelvesOff
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "shelvesoff", "rentalgoodses"); }
        }
        //商品删除地址
        public string RentalGoodses_Delete
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "rentalgoodses"); }
        }
        //出租的商品，默认图片地址
        public string RentalGoodses_DefultPic
        {
            get { return "http://static.kyjzs.com/static_v1.0/web/merchant/images/img/img_01.jpg"; }
        }

        /// <summary>
        /// 商品列表地址
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public string RentalGoodses_Index()
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "index", "rentalgoodses");
        }

        public string RentalGoods_IllegalGoods()
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "illegalgoods");
        }

        #endregion

        #region 作者：maq 时间：2014年5月4日11:24:31 描述：商户后台主页


        /// <summary>
        /// 商户登录后的主页
        /// </summary>
        /// <returns></returns>
        public string Order_Main()
        {
            return BaseUrl + UrlHelper.RouteAction("Default", "main", "order");
        }
        /// <summary>
        /// 商户登出
        /// </summary>
        public string Common_LoginOut
        {
            get { return BaseUrl + UrlHelper.RouteAction("Default", "loginout", "merchantmanage"); }
        }
        /// <summary>
        /// 签署协议
        /// </summary>
        public string SignAgreement
        {
            get { return BaseUrl + UrlHelper.RouteAction("Default", "signagreement", "common"); }
        }
        //商户默认logo
        public string Merchant_DefultLogo
        {
            get { return "http://static.kyjzs.com/static_v1.0/web/merchant/images/klk_logo.png"; }
        }

        #endregion

        #region 作者：maq  时间：2014年5月4日15:25:23  商户后台租期模板
        /// <summary>
        /// 商户后台租期模板列表页
        /// </summary>
        /// <returns></returns>
        public string TenancyTemplate_Index
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "index", "tenancytemplate"); }
        }
        /// <summary>
        /// 新增租期模板
        /// </summary>
        public string TenancyTemplate_Insert
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "insert", "tenancytemplate"); }
        }
        /// <summary>
        /// 修改租期模板
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public string TenancyTemplate_UpDate(int tid)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "update", "tenancytemplate", new { tid = tid });
        }

        /// <summary>
        /// 删除租期模板
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public string TenancyTemplate_Delete(int tid)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "tenancytemplate", new { tid = tid });
        }

        #endregion

        #region 作者：maq  时间：2014年5月28日09:13:33   描述：租用中的订单操作
        /// <summary>
        /// 发货
        /// </summary>
        public string Order_SendGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "sendgoods", "order"); }
        }
        /// <summary>
        /// 发送起租协议
        /// </summary>
        public string Order_SendAgreement
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "sendagreement", "order"); }
        }
        /// <summary>
        /// 同意退租
        /// </summary>
        public string Order_BackRentAgree
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "backrentagree", "order"); }
        }
        /// <summary>
        /// 返回上级订单状态
        /// </summary>
        public string Order_GoBack
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "goback", "order"); }
        }
        /// <summary>
        /// 赔损金额
        /// </summary>
        public string Order_LossMoney
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "lossmoney", "order"); }
        }
        /// <summary>
        /// 修改金额，不改变订单状态
        /// </summary>
        public string Order_ChangeMoney
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "changemoney", "order"); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderState"></param>
        /// <returns></returns>
        public string Order_Manager()
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "ordermanager", "order");
        }
        /// <summary>
        /// 换货同意
        /// </summary>
        public string Order_ExchangeGoodsAgree
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "exchangegoodsagree", "order"); }
        }
        /// <summary>
        /// 换货验收撤销
        /// </summary>
        public string Order_CancleExchange
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "cancleexchange", "order"); }
        }
        /// <summary>
        /// 退货同意
        /// </summary>
        public string BackGoodsAgree
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "backgoodsagree", "order"); }
        }

        /// <summary>
        /// 商户结算
        /// </summary>
        public string Order_MerchantSettlement
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "merchantsettlement", "order"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-04-30  描述：运费模板相关

        /// <summary>
        /// 运费模板首页加载
        /// </summary>
        public string FreightTemplates_Index
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "index", "freighttemplates"); }
        }

        /// <summary>
        /// 新增、修改运费模板
        /// </summary>
        public string FreightTemplates_Create
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "create", "freighttemplates"); }
        }

        /// <summary>
        /// 删除运费模板
        /// </summary>
        public string FreightTemplates_Delete
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "freighttemplates"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-04-30  描述：发货管理相关

        /// <summary>
        /// 发货管理
        /// </summary>
        public string ShipManage_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "shipmanage"); }
        }

        /// <summary>
        /// 修改发货信息
        /// </summary>
        public string ShipManage_Edit
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "edit", "shipmanage"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-07  描述：地理位置相关

        /// <summary>
        /// 地理位置
        /// </summary>
        /// <param name="actionName">ActionName</param>
        public string GeographyLocation(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "common");
        }

        #endregion

        #endregion

        #region URL工具方法

        /// <summary>
        /// 获取Action的绝对URL
        /// </summary>
        /// <param name="actionName">action的名称</param>
        /// <param name="controllerName">controller的名称</param>
        /// <returns></returns>
        private string ActionUrlAbsolute(string actionName, string controllerName)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.Action(actionName, controllerName)).AbsoluteUri;
        }

        /// <summary>
        /// 获取Action的绝对URL
        /// </summary>
        /// <param name="actionName">action的名称</param>
        /// <param name="controllerName">controller的名称</param>
        /// <param name="routeValues">路由数据</param>
        /// <returns></returns>
        private string ActionUrlAbsolute(string actionName, string controllerName, object routeValues)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.Action(actionName, controllerName, routeValues)).AbsoluteUri;
        }

        /// <summary>
        /// 获取Route的绝对URL
        /// </summary>
        /// <param name="routeName">路由的名称</param>
        /// <param name="routeValues">路由数据</param>
        /// <returns></returns>
        private string RouteUrlAbsolute(string routeName, object routeValues)
        {
            return new Uri(GetBaseUrl(UrlHelper), UrlHelper.RouteUrl(routeName, routeValues)).AbsoluteUri;
        }

        private Uri GetBaseUrl(UrlHelper url)
        {
            Uri contextUri = new Uri(url.RequestContext.HttpContext.Request.Url,
                url.RequestContext.HttpContext.Request.RawUrl);
            UriBuilder realmUri = new UriBuilder(contextUri)
            {
                Path = url.RequestContext.HttpContext.Request.ApplicationPath,
                Query = null,
                Fragment = null
            };
            return realmUri.Uri;
        }

        #endregion


    }
}
