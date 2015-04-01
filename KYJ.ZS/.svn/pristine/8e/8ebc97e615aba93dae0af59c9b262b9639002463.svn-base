using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace KYJ.ZS.Commons
{
    public class ManagerSiteUrlHelper
    {
        private static readonly ManagerSiteUrlHelper instance = new ManagerSiteUrlHelper();

        private static string BaseUrl;

        internal static ManagerSiteUrlHelper Instance
        {
            get
            {
                if (string.IsNullOrEmpty(BaseUrl))
                    BaseUrl = PubConstant.ManagerBaseUrl;
                return instance;
            }
        }

        private ManagerSiteUrlHelper()
        {
        }

        internal UrlHelper UrlHelper { get; private set; }

        internal ManagerSiteUrlHelper SetContext(UrlHelper context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            this.UrlHelper = context;
            return this;
        }

        #region URL集装箱

        #region  作者：孟国栋  时间：2014-06-3 描述：管理后台登陆及验证码
        /// <summary>
        /// 管理后台登陆
        /// </summary>
        public string Login
        {
            get { return this.UrlHelper.RouteAction("Default", "login","common"); }
        }
        /// <summary>
        /// 管理后台验证码
        /// </summary>
        public string Common_Code
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "code", "common"); }
        }
        #endregion

        #region 作者：孟国栋 时间：2014-05-27 描述：管理员退出
        public string Common_LoginOut
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "loginout", "managermanage"); }
        }
        #endregion

        #region 作者：孟国栋 时间：2014-06-05 描述：商品排序管理
        /// <summary>
        /// 核审商品排序页
        /// </summary>
        public string Generalize_ShowAduitInfo
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "showaduitinfo", "generalize"); } 
        }

        /// <summary>
        /// 商品排序详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_ShowInfo(int id,bool isAdvert)
        {
           return BaseUrl + this.UrlHelper.RouteAction("Default", "showinfo", "generalize",new {id=id,isAdvert=isAdvert});
        }
        /// <summary>
        /// 商品排序展示
        /// </summary>
        public string Generalize_GeneralizeShow
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "generalizeshow", "generalize");} 
        }
        /// <summary>
        /// 商品排序修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_GeneralizeUpdate(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "generalizeupdate", "generalize", new { id = id });
        }
        /// <summary>
        /// 提交核审
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_GeneralizeApply(int id,int state,int pageIndex)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "generalizeapply", "generalize", new { id = id,state=state,pageIndex=pageIndex });
        }
        /// <summary>
        /// 删除排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_GeneralizeDelete(int id,int pageIndex,int state)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "generalizedelete", "generalize", new { id = id,pageIndex=pageIndex,state=state });
        }
        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_GeneralizeDownLine(int id, int state, int pageIndex)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "generalizedownline", "generalize", new { id = id ,state=state,pageIndex=pageIndex });
        }
        /// <summary>
        /// 通过核审
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_PassAduit(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default","passaduit","generalize",new { id=id});
        }
        /// <summary>
        /// 驳回核审
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Generalize_BackAduit(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default","backaduit","generalize",new {id=id});
        }
        /// <summary>
        /// 添加商品排序
        /// </summary>
        public string Generalize_GeneralizeAdd
        {
            get
            {
                return
                    BaseUrl + this.UrlHelper.RouteAction("Default", "generalizeadd", "generalize");
            }
           
        }

        /// <summary>
        /// 推广位置
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public string GeneralizeLocation(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "generalize");
        }

        /// <summary>
        /// 允许发布推广数量
        /// </summary>
        public string Generalize_MaxNum
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "maxnum", "generalize"); }
        }


        #endregion

        #region 作者：孟国栋 时间：2014-06-3  描述：角色权限管理
        /// <summary>
        /// 管理角色
        /// </summary>
        public string RoleManager_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "rolemanager"); }
        }

        /// <summary>
        /// 添加(修改)角色权限
        /// </summary>
        public string RoleManager_RoleAdd(int? roleId, string type)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "roleadd", "rolemanager", new { roleId = roleId, type = type });
        }

        /// <summary>
        /// 确认添加(修改)角色权限
        /// </summary>
        public string RoleManager_RoleUpdate
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "roleupdate", "rolemanager"); }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        public string RoleManager_Delete
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "rolemanager"); }
        }

        /// <summary>
        /// 删除校验
        /// </summary>
        public string RoleManager_DeleteValidate
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deletevalidate", "rolemanager"); }
        }
        #endregion

        #region 作者：ningjd 时间：2014-05-27  描述：平台管理中心

        /// <summary>
        /// 平台管理中心
        /// </summary>
        public string Home_Index
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "index", "home"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-23  描述：商户管理相关

        /// <summary>
        /// 商户管理-商户列表
        /// </summary>
        public string MerchantManage_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "merchantmanage"); }
        }

        /// <summary>
        /// 企业详情-企业基本资料
        /// </summary>
        public string MerchantManage_BasicInfo(int merchantId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "basicinfo", "merchantmanage", new { merchantId = merchantId });
        }

        /// <summary>
        /// 企业详情-租售记录
        /// </summary>
        public string MerchantManage_OrderRecord(int merchantId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "orderrecord", "merchantmanage", new { merchantId = merchantId });
        }

        /// <summary>
        /// 企业详情-信誉记录
        /// </summary>
        public string MerchantManage_CreditRecord(int merchantId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "creditrecord", "merchantmanage", new { merchantId = merchantId });
        }

        /// <summary>
        /// 企业详情-保障服务
        /// </summary>
        public string MerchantManage_SecurityServices(int merchantId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "securityservices", "merchantmanage", new { merchantId = merchantId });
        }

        /// <summary>
        /// 企业详情-提现日志
        /// </summary>
        public string MerchantManage_WithdrawalsLog(int merchantId)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "withdrawalslog", "merchantmanage", new { merchantId = merchantId });
        }

        /// <summary>
        /// 商户管理-添加商户
        /// </summary>
        public string MerchantManage_Create
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "create", "merchantmanage"); }
        }

        /// <summary>
        /// 商户管理-添加商户(POST提交)
        /// </summary>
        public string MerchantManage_CreateMerchant
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "createmerchant", "merchantmanage"); }
        }

        /// <summary>
        /// 商户账号校验
        /// </summary>
        public string MerchantManage_ValidateLoginName
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "validateloginname", "merchantmanage"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-23  描述：出租商品管理相关

        /// <summary>
        /// 出租商品管理-商品列表
        /// </summary>
        public string RentalGoods_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "rentalgoods"); }
        }

        /// <summary>
        /// 出租商品管理-商品审核（确认违规、解除违规）
        /// </summary>
        public string RentalGoods_AuditRentalGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "auditrentalgoods", "rentalgoods"); }
        }

        /// <summary>
        /// 出租商品管理-违规商品
        /// </summary>
        public string RentalGoods_ViolationGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "violationgoods", "rentalgoods"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-27  描述：订单管理相关

        /// <summary>
        /// 订单管理
        /// </summary>
        public string Order_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "order"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-27  描述：类目列表相关

        /// <summary>
        /// 类目列表
        /// </summary>
        /// <param name="actionName">ActionName</param>
        public string CategoryLocation(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "common");
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-27  描述：信息标签管理相关

        /// <summary>
        /// 信息标签管理
        /// </summary>
        public string Tag_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "tag"); }
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        public string Tag_CreateTag
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "createtag", "tag"); }
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        public string Tag_DeleteTag
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deleteTag", "tag"); }
        }

        /// <summary>
        /// 删除校验
        /// </summary>
        public string Tag_DeleteValidate
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "deletevalidate", "tag"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-28  描述：商户提现管理相关

        /// <summary>
        /// 商户提现管理
        /// </summary>
        public string WithdrawCash_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "withdrawcash"); }
        }

        /// <summary>
        /// 提现详情
        /// </summary>
        public string WithdrawCash_Detail(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "detail", "withdrawcash", new { id = id });
        }

        /// <summary>
        /// 驳回
        /// </summary>
        public string WithdrawCash_Reject
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "reject", "withdrawcash"); }
        }

        /// <summary>
        /// 确认提现
        /// </summary>
        public string WithdrawCash_ConfirmWithdrawal
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "confirmwithdrawal", "withdrawcash"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-05-29  描述：闲置物品信息管理相关

        /// <summary>
        /// 闲置物品信息管理-管理信息
        /// </summary>
        public string SaleGoods_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "salegoods"); }
        }

        /// <summary>
        /// 闲置物品信息管理-商品审核（确认违规、解除违规）
        /// </summary>
        public string SaleGoods_AuditRentalGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "auditrentalgoods", "salegoods"); }
        }

        /// <summary>
        /// 闲置物品信息管理-违规信息
        /// </summary>
        public string SaleGoods_ViolationGoods
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "violationgoods", "salegoods"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-06-03  描述：商品分类管理相关

        /// <summary>
        /// 基础数据-商品分类管理
        /// </summary>
        public string Category_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "category"); }
        }

        /// <summary>
        /// 添加分类
        /// </summary>
        public string Category_Create
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "create", "category"); }
        }

        /// <summary>
        /// 修改分类
        /// </summary>
        public string Category_Edit
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "edit", "category"); }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        public string Category_Delete
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "category"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-06-03  描述：日志查阅相关

        /// <summary>
        /// 基础数据-日志查阅
        /// </summary>
        public string Log_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "log"); }
        }

        /// <summary>
        /// 模块列表加载
        /// </summary>
        public string Log_GetModules
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "getmodules", "log"); }
        }

        /// <summary>
        /// 权限角色列表加载
        /// </summary>
        public string Log_GetRoles
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "getroles", "log"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-06-04  描述：属性规格管理相关

        /// <summary>
        /// 基础数据-属性规格管理
        /// </summary>
        public string AttrCategory_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "attrcategory"); }
        }

        /// <summary>
        /// 属性规格列表加载
        /// </summary>
        public string AttrCategory_GetAttrCategories(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "attrcategory");
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public string AttrCategory_IsShow(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "attrcategory");
        }

        /// <summary>
        /// 更改显示状态
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public string AttrCategory_ChangeShow(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "attrcategory");
        }

        /// <summary>
        /// 添加
        /// </summary>
        public string AttrCategory_Create(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "create" + actionName, "attrcategory");
        }

        /// <summary>
        /// 修改
        /// </summary>
        public string AttrCategory_Edit(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "edit" + actionName, "attrcategory");
        }

        /// <summary>
        /// 删除
        /// </summary>
        public string AttrCategory_Delete(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "delete" + actionName, "attrcategory");
        }

        #endregion

        #region 作者：ningjd 时间：2014-06-04  描述：普通用户管理相关

        /// <summary>
        /// 普通用户管理
        /// </summary>
        public string LocalUser_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "localuser"); }
        }

        /// <summary>
        /// 模块列表加载
        /// </summary>
        public string LocalUser_Detail(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "detail", "localuser", new { id = id });
        }

        /// <summary>
        /// 认证审核
        /// </summary>
        public string LocalUser_AuditManage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "auditmanage", "localuser"); }
        }

        /// <summary>
        /// 审核（审核通过、审核驳回）
        /// </summary>
        public string LocalUser_Audit
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "audit", "localuser"); }
        }

        /// <summary>
        /// 审核（审核通过、审核驳回）
        /// </summary>
        public string LocalUser_Freeze
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "freeze", "localuser"); }
        }

        #endregion

        #region 作者：ningjd 时间：2014-06-06  描述：职员管理相关

        /// <summary>
        /// 管理职员权限
        /// </summary>
        public string Admin_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "admin"); }
        }

        /// <summary>
        /// 添加职员
        /// </summary>
        public string Admin_CreateAdmin
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "createadmin", "admin"); }
        }

        /// <summary>
        /// 添加、修改职员
        /// </summary>
        public string Admin_Create
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "create", "admin"); }
        }

        /// <summary>
        /// 删除职员
        /// </summary>
        public string Admin_Delete
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "admin"); }
        }

        /// <summary>
        /// 职员账号校验
        /// </summary>
        public string Admin_ValidateLoginName
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "validateloginname", "admin"); }
        }

        #endregion

        #region 作者：cheny 时间：2014-06-05  描述：广告管理相关
        /// <summary>
        /// 广告管理
        /// </summary>
        public string AdvertManage_Manage
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "manage", "advertmanage"); }
        }

        /// <summary>
        /// 广告详情
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        public string AdvertManage_Detail(int id, bool isCheckAdvert)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "detail", "advertmanage", new { id = id, isCheckAdvert = isCheckAdvert });
        }

        /// <summary>
        /// 发布广告
        /// </summary>
        public string AdvertManage_Publish(bool isSave)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "publish", "advertmanage", new { isSave = isSave });
        }

        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        public string AdvertManage_Modif(int id)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", "modify", "advertmanage", new { id = id });
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="id">广告ID</param>
        /// <returns></returns>
        public string AdvertManage_Delete
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "delete", "advertmanage"); }
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string AdvertManage_Apply
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "apply", "advertmanage"); }
        }

        /// <summary>
        /// 审核广告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string AdvertManage_CheckAdvert
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "checkadvert", "advertmanage"); }
        }


        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string AdvertManage_DownLine
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "downline", "advertmanage"); }
        }

        /// <summary>
        /// 通过广告审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string AdvertManage_PassApply
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "passapply", "advertmanage"); }
        }
        /// <summary>
        /// 驳回广告审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string AdvertManage_RejectApply
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "rejectapply", "advertmanage"); }
        }
        /// <summary>
        /// 广告位置
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public string AdvertLocation(string actionName)
        {
            return BaseUrl + this.UrlHelper.RouteAction("Default", actionName, "advertmanage");
        }

        /// <summary>
        /// 允许发布广告数量（图片）
        /// </summary>
        public string AdvertManage_MaxNum
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "maxnum", "advertmanage"); }
        }

        /// <summary>
        /// 广告尺寸
        /// </summary>
        public string AdvertManage_AdvertSize
        {
            get { return BaseUrl + this.UrlHelper.RouteAction("Default", "advertsize", "upload"); }
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
