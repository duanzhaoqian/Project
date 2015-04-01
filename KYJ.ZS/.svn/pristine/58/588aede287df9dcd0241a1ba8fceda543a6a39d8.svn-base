using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KYJ.ZS.Models.OrderGoodses;
using KYJ.ZS.BLL.OrderGoodses;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.BLL.DeliveryAddresses;
using KYJ.ZS.Models.DB;
using KYJ.ZS.BLL.Collections;
using KYJ.ZS.User.Controllers.ActionFilters;
using KYJ.ZS.Models.LocalUsers;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.Collections;
using KYJ.ZS.Models.BrowseHistorys;

namespace KYJ.ZS.User.Controllers
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-04-28
    /// Desc:用户操作控制器
    /// </summary>
    public class UserController : BaseController
    {

        /// <summary>
        /// 退出登录
        /// </summary>
        [HttpGet]
        public ActionResult Logout()
        {
            _ServiceContext.Refresh();
            return Redirect(Url.UserSiteUrl().Login);
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// Desc:用户后台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? pageIndex)
        {
            var Collbll = new KYJ.ZS.BLL.Collections.CollectionBll();
            var userbll = new KYJ.ZS.BLL.LocalUsers.LocalUserBll();
            var orderbll = new KYJ.ZS.BLL.OrderGoodses.OrderGoodsBll();
            var salebll = new KYJ.ZS.BLL.SaleGoodses.SaleGoodsBll();
            var historybll = new KYJ.ZS.BLL.BrowseHistorys.BrowseHistoryBll();
            int UserId = _ServiceContext.CurrentUser.UserID;
            int index = pageIndex ?? 1;
            int totalRecord = 0;
            int totalPage = 0;
            int pageSize = 20;//每页显示的个数
            //用户信息
            LocalUserEntity entity = userbll.GetLocalUserByUserID(UserId);
            //收藏
            List<CollectionEntity> CollList = Collbll.GetCollectionGoodsesPage(UserId, 1, 8, out totalRecord, out totalPage);
            //历史浏览
            List<BrowseHistoryEntity> History = historybll.GetBrowseHistoryTopList(_ServiceContext.CurrentUser._Guid, 8);
            //商品列表
            OrderGoodsEntity order = new OrderGoodsEntity();
            order.UserIndexShowState = "1,2,4,6";//这里写146的意思是订单状态为1、4、6的显示在用户后台首页中
            order.UserId = UserId;
            List<OrderGoodsEntity> OrderList = orderbll.GetUserRentOrdersPage(order, index, pageSize, out totalRecord, out totalPage);
            PagedList<OrderGoodsEntity> pageList = null;
            if (OrderList != null)
            {
                pageList = new PagedList<OrderGoodsEntity>(OrderList, index, pageSize, totalRecord);
            }
            else
            {
                if (index > 1)
                {
                    index = index - 1;
                    string url = Url.UserSiteUrl().Index + "?pageIndex=" + index;
                    return Redirect(url);
                }
            }








            //已发布信息个数
            int SaleGoodsCount = salebll.GetSaleGoodsCountByUserId(UserId);
            //个人用户帐户
            UserAccountEntity userAccount = userbll.GetUserAccountInfo(UserId);
            ViewData["Collection"] = CollList;
            ViewData["LocalUser"] = entity;
            ViewData["History"] = History;
            ViewData["OrderGoods"] = OrderList;
            ViewData["SaleGoodsCount"] = SaleGoodsCount;
            ViewData["UserAccountPrice"] = userAccount.Price.ToString("#0.00");
            return View(pageList);
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-基本资料
        /// </summary>
        [HttpGet]
        public ActionResult Information()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            //获取当前登录用户信息
            var userEntity = _bll.GetLocalUserByUserID(_ServiceContext.CurrentUser.UserID);
            return View(userEntity);
        }

        [HttpPost]
        [AjaxOnly(NoCache = true)]
        public JsonResult ResetInformation()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            UserInfoEntity entity = new UserInfoEntity();
            //初始化数据
            InitializeUserInfoEntity(entity);

            var num = _bll.UpdateUserInfo(entity);
            if (num > 0)
                return Json(new { result = true, message = "修改成功！" });
            else
                return Json(new { result = false, message = "修改失败，服务器繁忙！" });
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-教育情况
        /// </summary>
        [HttpGet]
        public ActionResult Education()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            //获取当前登录用户信息
            var userEntity = _bll.GetLocalUserByUserID(_ServiceContext.CurrentUser.UserID);
            return View(userEntity);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:重置用户中心-教育情况
        /// </summary>
        [HttpPost]
        [AjaxOnly(NoCache = true)]
        public JsonResult ResetEducation()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            UserEducationEntity entity = new UserEducationEntity();
            //初始化数据
            InitializeUserEducationEntity(entity);

            if (TryValidateModel(entity))
            {
                var num = _bll.UpdateUserEducation(entity);
                if (num > 0)
                    return Json(new { result = true, message = "修改成功！" });
                else
                    return Json(new { result = false, message = "修改失败，服务器繁忙！" });
            }
            else
            {
                return Json(new { result = false, message = "请认真填写信息！" });
            }
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-头像照片
        /// </summary>
        [HttpGet]
        public ActionResult Avatar()
        {
            //KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            ////获取当前登录用户信息
            //var userEntity = _bll.GetLocalUserByUserID(_ServiceContext.CurrentUser.UserID);
            return View();
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-修改密码
        /// </summary>
        [HttpGet]
        public ActionResult ModifyPwd()
        {
            return View();
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:重置用户中心-教育情况
        /// </summary>
        [HttpPost]
        [AjaxOnly(NoCache = true)]
        public JsonResult ResetPassword(string oldpassword, string newpassword, string repassword)
        {
            if (newpassword.Equals(repassword))
            {
                KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();

                var _userid = _ServiceContext.CurrentUser.UserID;

                var num = _bll.UpdatePwd(_userid, oldpassword, newpassword);

                if (num > 0)
                {
                    //重置登录状态
                    _ServiceContext.Refresh();
                    return Json(new { result = true, message = "修改成功！系统将重新登录！" });
                }
                else
                    return Json(new { result = false, message = "原密码不正确！" });
            }
            else
            {
                return Json(new { result = false, message = "确认密码不正确！" });
            }
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-名片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Card()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            //获取当前登录用户信息
            var userEntity = _bll.GetLocalUserByUserID(_ServiceContext.CurrentUser.UserID);
            return View(userEntity);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-重置名片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly(NoCache = true)]
        public JsonResult ResetCard(string companyname, string companyposition)
        {
            if (!string.IsNullOrEmpty(companyname) || string.IsNullOrEmpty(companyposition))
            {
                KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();

                var _userid = _ServiceContext.CurrentUser.UserID;

                var num = _bll.UpdateCard(_userid, companyname, companyposition);

                if (num > 0)
                    return Json(new { result = true, message = "修改成功！" });
                else
                    return Json(new { result = false, message = "系统繁忙，请稍后再试！" });
            }
            else
            {
                return Json(new { result = false, message = "请填写正确信息！" });
            }
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-个人认证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Authentication()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            var userid = _ServiceContext.CurrentUser.UserID;
            var entity = _bll.GetAuthenticationByUserID(userid);
            if (entity != null && entity.PapersStatus == 1)
            {
                return Redirect(Url.UserSiteUrl().Apply);
            }
            return View(entity);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-申请认证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Apply()
        {
            KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();
            var userid = _ServiceContext.CurrentUser.UserID;
            var entity = _bll.GetAuthenticationByUserID(userid);
            if (entity != null && entity.PapersStatus != 1 && entity.PapersStatus != 3)
            {
                return Redirect(Url.UserSiteUrl().Authentication);
            }
            return View();
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:用户中心-申请认证
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly(NoCache = true)]
        public JsonResult Apply(string txt_code)
        {
            var _code = new KYJ.ZS.Commons.Common.VerificationCode();
            if (!string.IsNullOrEmpty(txt_code) && _code.CheckCode(txt_code))
            {
                var entity = new AuthenticationEntity();
                //填充实体
                InitializeAuthenticationEntity(entity);
                //后台验证，暂未添加
                if (TryUpdateModel<AuthenticationEntity>(entity))
                {
                    KYJ.ZS.BLL.LocalUsers.LocalUserBll _bll = new BLL.LocalUsers.LocalUserBll();

                    var num = _bll.ApplyAuthentication(entity);

                    if (num > 0)
                        return Json(new { result = true, message = "申请成功！" });
                    else
                        return Json(new { result = false, message = "系统繁忙，请稍后再试！" });
                }
                else
                {
                    return Json(new { result = false, message = "请填写正确信息！" });
                }
            }
            else
            {
                return Json(new { result = false, message = "验证码信息错误！" });
            }
        }

        #region 填充数据

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// Desc:初始化UserInfoEntity数据
        /// </summary>
        /// <param name="entity">UserInfoEntity数据实体</param>
        public void InitializeUserInfoEntity(UserInfoEntity entity)
        {
            //当前登录用户ID
            entity.UserId = _ServiceContext.CurrentUser.UserID;

            #region 基础数据

            entity.Mobile = string.IsNullOrEmpty(Request.Form["Mobile"]) ? string.Empty : Request.Form["Mobile"].ToString();
            entity.Email = string.IsNullOrEmpty(Request.Form["Email"]) ? string.Empty : Request.Form["Email"].ToString();
            entity.RealName = string.IsNullOrEmpty(Request.Form["RealName"]) ? string.Empty : Request.Form["RealName"].ToString();
            entity.NickName = string.IsNullOrEmpty(Request.Form["NickName"]) ? string.Empty : Request.Form["NickName"].ToString();
            entity.Sex = string.IsNullOrEmpty(Request.Form["Sex"]) ? 0 : Auxiliary.ToInt32(Request.Form["Sex"]);
            entity.BirthdayYear = string.IsNullOrEmpty(Request.Form["BirthdayYear"]) ? string.Empty : Request.Form["BirthdayYear"].ToString();
            entity.BirthdayMonth = string.IsNullOrEmpty(Request.Form["BirthdayMonth"]) ? string.Empty : Request.Form["BirthdayMonth"].ToString();
            entity.BirthdayDay = string.IsNullOrEmpty(Request.Form["BirthdayDay"]) ? string.Empty : Request.Form["BirthdayDay"].ToString();
            entity.Constellation = string.IsNullOrEmpty(Request.Form["Constellation"]) ? 0 : Auxiliary.ToInt32(Request.Form["Constellation"]);

            #endregion

            #region 地理位置

            entity.LiveProvinceId = string.IsNullOrEmpty(Request.Form["LiveProvinceId"]) ? 0 : Auxiliary.ToInt32(Request.Form["LiveProvinceId"]);
            entity.LiveProvinceName = string.IsNullOrEmpty(Request.Form["LiveProvinceName"]) ? string.Empty : Request.Form["LiveProvinceName"].ToString();
            entity.LiveCityId = string.IsNullOrEmpty(Request.Form["LiveCityId"]) ? 0 : Auxiliary.ToInt32(Request.Form["LiveCityId"]);
            entity.LiveCityName = string.IsNullOrEmpty(Request.Form["LiveCityName"]) ? string.Empty : Request.Form["LiveCityName"].ToString();
            entity.LiveDistrictId = string.IsNullOrEmpty(Request.Form["LiveDistrictId"]) ? 0 : Auxiliary.ToInt32(Request.Form["LiveDistrictId"]);
            entity.LiveDistrictName = string.IsNullOrEmpty(Request.Form["LiveDistrictName"]) ? string.Empty : Request.Form["LiveDistrictName"].ToString();
            entity.HomeProvinceId = string.IsNullOrEmpty(Request.Form["HomeProvinceId"]) ? 0 : Auxiliary.ToInt32(Request.Form["HomeProvinceId"]);
            entity.HomeProvinceName = string.IsNullOrEmpty(Request.Form["HomeProvinceName"]) ? string.Empty : Request.Form["HomeProvinceName"].ToString();
            entity.HomeCityId = string.IsNullOrEmpty(Request.Form["HomeCityId"]) ? 0 : Auxiliary.ToInt32(Request.Form["HomeCityId"]);
            entity.HomeCityName = string.IsNullOrEmpty(Request.Form["HomeCityName"]) ? string.Empty : Request.Form["HomeCityName"].ToString();
            entity.HomeDistrictId = string.IsNullOrEmpty(Request.Form["HomeDistrictId"]) ? 0 : Auxiliary.ToInt32(Request.Form["HomeDistrictId"]);
            entity.HomeDistrictName = string.IsNullOrEmpty(Request.Form["HomeDistrictName"]) ? string.Empty : Request.Form["HomeDistrictName"].ToString();

            #endregion
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// Desc:初始化UserInfoEntity数据
        /// </summary>
        /// <param name="entity">UserInfoEntity数据实体</param>
        public void InitializeUserEducationEntity(UserEducationEntity entity)
        {
            //当前登录用户ID
            entity.UserId = _ServiceContext.CurrentUser.UserID;

            #region 基础数据

            entity.College = string.IsNullOrEmpty(Request.Form["College"]) ? string.Empty : Request.Form["College"].ToString();
            entity.HighSchool = string.IsNullOrEmpty(Request.Form["HighSchool"]) ? string.Empty : Request.Form["HighSchool"].ToString();
            entity.MiddleSchool = string.IsNullOrEmpty(Request.Form["MiddleSchool"]) ? string.Empty : Request.Form["MiddleSchool"].ToString();
            entity.PrimarySchool = string.IsNullOrEmpty(Request.Form["PrimarySchool"]) ? string.Empty : Request.Form["PrimarySchool"].ToString();

            #endregion

        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// Desc:初始化AuthenticationEntity数据
        /// </summary>
        /// <param name="entity">AuthenticationEntity数据实体</param>
        public void InitializeAuthenticationEntity(AuthenticationEntity entity)
        {
            //当前登录用户ID
            entity.UserID = _ServiceContext.CurrentUser.UserID;

            #region 基础数据

            entity.RealName = string.IsNullOrEmpty(Request.Form["txt_realname"]) ? string.Empty : Request.Form["txt_realname"].ToString();
            entity.PapersType = string.IsNullOrEmpty(Request.Form["sel_paperstype"]) ? 0 : Auxiliary.ToInt32(Request.Form["sel_paperstype"]);
            entity.Papers = string.IsNullOrEmpty(Request.Form["txt_papers"]) ? string.Empty : Request.Form["txt_papers"].ToString();

            #endregion

        }
        #endregion
    }
}
