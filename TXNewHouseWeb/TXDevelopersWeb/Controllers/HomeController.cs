using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using TXOrm;
using TXModel.Commons;
using TXBll.Dev;
using TXModel.Dev;
using TXCommons;

namespace TXDevelopersWeb.Controllers
{
    /// <summary>
    /// 用户登录后控制类
    /// Author:台永海
    /// </summary>
    public class HomeController : BaseController
    {
        #region 首页
        /// <summary>
        /// 开发商首页
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData["CurrentMenu"] = 10;
            DevelopersBll bll = new DevelopersBll();
            CT_DeveAndIdenInfo entity = bll.GetDeveAndIdenInfo(LoginUserInfo.Id);
            //得到用户图片
            TXCommons.PictureModular.UserPictureInfo picture = TXCommons.PictureModular.GetPicture.GetUserPictureInfo(entity.InnerCode, true, TXCommons.PictureModular.UserPictureType.LOGO.ToString());
            ViewData["UserPhoto"] = picture;
            return View(entity);
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改用户密码
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePassword()
        {
            ViewData["CurrentMenu"] = 71;
            return View();
        }
        /// <summary>
        /// 提交修改用户密码
        /// Author:台永海
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public int DoUpdatePassword(string oldPwd, string newPwd)
        {
            DevelopersBll bll = new DevelopersBll();
            return bll.UpdatePassword(LoginUserInfo.Id, oldPwd, newPwd);
        }
        #endregion

        #region 修改头像
        /// <summary>
        /// 修改头像
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePhoto()
        {

            ViewData["CurrentMenu"] = 72;
            DevelopersBll bll = new DevelopersBll();
            Developer entity = bll.GetEntity_ById(LoginUserInfo.Id) as Developer;
            GetUserPicListToViewData(entity.InnerCode, 1);
            return View(entity);
        }
        /// <summary>
        /// 修改头像高级
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePhotoExpert()
        {
            ViewData["CurrentMenu"] = 72;
            DevelopersBll bll = new DevelopersBll();
            Developer entity = bll.GetEntity_ById(LoginUserInfo.Id) as Developer;
            GetUserPicListToViewData(entity.InnerCode, 1);
            return View(entity);
        }
        #endregion

        #region 修改信息
        /// <summary>
        /// 修改用户信息
        /// Author:台永海
        /// </summary>
        public ActionResult UpdateUserInfo()
        {
            ViewData["CurrentMenu"] = 73;
            DevelopersBll bll = new DevelopersBll();
            Developer entity = bll.GetEntity_ById(LoginUserInfo.Id) as Developer;
            return View(entity);
        }
        /// <summary>
        /// 提交修改用户信息
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public int DoUpdateUserInfo(string email, string tel, string spatel, int? pid, string pname, int? cid, string cname)
        {
            Developer entity = new Developer();
            entity.Id = LoginUserInfo.Id;
            entity.Email = email;
            entity.Telephone = tel;
            entity.SpareTelephone = spatel;
            entity.ProvinceId = pid.HasValue ? pid.Value : 0;
            entity.ProvinceName = pname;
            entity.CityId = cid.HasValue ? cid.Value : 0;
            entity.CityName = cname;
            DevelopersBll bll = new DevelopersBll();
            return bll.UpdateUserInfo(entity);
        }
        #endregion

        #region 修改手机
        /// <summary>
        /// 修改手机号码
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateMobile()
        {
            DevelopersBll bll = new DevelopersBll();
            Developer entity = bll.GetEntity_ById(LoginUserInfo.Id) as Developer;
            return View(entity);
        }
        /// <summary>
        /// 提交修改手机号码
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public int DoUpdateMobile(string code, string pwd, string mobile)
        {
            int result = 0;
            UserController uc = new UserController();
            JsonResult flag = uc.AjaxCheckValidate(mobile, code);
            if (flag.Data.ToBool())
            {
                //删除验证码
                Redis.RemoveAllFromList(KeyList.GetRedisMobileValidateCodeKey(mobile), ServiceStack.FunctionTypeEnum.NewHouseVerificationCode, 0);
                DevelopersBll bll = new DevelopersBll();
                result = bll.UpdateMobile(LoginUserInfo.Id, pwd, mobile);
            }
            return result;
        }
        #endregion

        #region 身份认证
        /// <summary>
        /// 身份认证
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult Identification()
        {
            ViewData["CurrentMenu"] = 74;
            DevelopersBll bll = new DevelopersBll();
            CT_DeveAndIdenInfo entity = bll.GetDeveAndIdenInfo(LoginUserInfo.Id);
            GetUserPicListToViewData(entity.InnerCode, 2);
            GetDocumentInfoListToViewData(entity.InnerCode, 0);
            return View(entity);
        }
        /// <summary>
        /// 提交身份认证信息
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public ActionResult DoIdentification()
        {
            int type = PubConstant.ToInt32(Request.Form["selType"]);
            string companyName = Request.Form["txtCompanyName"];
            int pid = PubConstant.ToInt32(Request.Form["selProvince"]);
            string pname = Request.Form["hidProvince"];
            int cid = PubConstant.ToInt32(Request.Form["selCity"]);
            string cname = Request.Form["hidCity"];
            int did = PubConstant.ToInt32(Request.Form["selDistrict"]);
            string dname = Request.Form["hidDistrict"];
            string companyAddress = Request.Form["txtCompanyAddress"];
            string uname = Request.Form["txtUserName"];
            string umobile = Request.Form["txtUserMobile"];
            string uemail = Request.Form["txtUserEmail"];

            Developer_Identity entity = new Developer_Identity();
            entity.Type = type;
            entity.CompanyName = companyName;
            entity.ProvinceId = pid;
            entity.ProvinceName = pname;
            entity.CityId = cid;
            entity.CityName = cname;
            entity.DId = did;
            entity.DName = dname;
            entity.CompanyAddress = companyAddress;
            entity.UserName = uname;
            entity.UserMobile = umobile;
            entity.UserEmail = uemail;
            entity.DeveloperId = LoginUserInfo.Id;

            DevelopersBll bll = new DevelopersBll();
            int result = bll.UpdateIdentification(entity);
            return RedirectToAction("Identification");
        }
        #endregion

        #region 站内信
        /// <summary>
        /// 站内信
        /// Author:台永海
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult Message(int? page, string key)
        {
            ViewData["CurrentMenu"] = 81;

            #region 设置分页参数
            Paging<DeveloperMessage> paging = new Paging<DeveloperMessage>();
            paging.PageIndex = page.HasValue ? page.Value : 1;
            paging.PageSize = 10;
            #endregion

            key = String.IsNullOrWhiteSpace(key) ? String.Empty : key;
            ViewData["Keyword"] = key;

            #region 读取分页数据
            DeveloperMessageBll bll = new DeveloperMessageBll();
            paging = bll.GetPageMessage(paging, LoginUserInfo.Id, key);
            PagedList<DeveloperMessage> pagelist = new PagedList<DeveloperMessage>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
            #endregion

            //检查是否为Ajax请求
            if (Request.IsAjaxRequest())
                return PartialView("MessageTable", pagelist);
            else
                return View(pagelist);
        }
        /// <summary>
        /// 修改站内信已读或删除
        /// Author:台永海
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int AjaxUpdateMessageReadOrDel(int mid, bool flag)
        {
            DeveloperMessageBll bll = new DeveloperMessageBll();
            return bll.UpdateMessageReadOrDel(LoginUserInfo.Id, mid, flag);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得用户图片集合存入ViewData
        /// Author:台永海
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="picType">图片类型：1头像，2身份认证</param>
        /// <returns></returns>
        public void GetUserPicListToViewData(string guid, int picType)
        {
            //图片参数
            string GUID = guid;
            string pictureType = String.Empty;
            switch (picType)
            {
                case 1: pictureType = TXCommons.PictureModular.UserPictureType.LOGO.ToString(); break;
                case 2: pictureType = TXCommons.PictureModular.UserPictureType.Identification.ToString(); break;
                default:
                    break;
            }

            int cityId = 0;
            //得到图片集合
            TXCommons.PictureModular.UserPictureInfo picList = null;
            picList = TXCommons.PictureModular.GetPicture.GetUserPictureInfo(GUID, true, pictureType);
            //存入视图
            ViewData["GUID"] = GUID;
            ViewData["pictureType"] = pictureType;
            ViewData["cityId"] = cityId;
            ViewData["picList"] = picList;
        }

        /// <summary>
        /// 获得用户文件存入ViewData
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="docType">文件类型：0合同，1附件</param>
        public void GetDocumentInfoListToViewData(string guid, int docType)
        {
            //文件参数
            string GUID = guid;
            string documentType = String.Empty;
            switch (docType)
            {
                case 0:
                    documentType = TXCommons.PictureModular.DocumentType.PACT.ToString();
                    break;
                case 1:
                    documentType = TXCommons.PictureModular.DocumentType.ANNEX.ToString();
                    break;
                default:
                    break;
            }

            int cityId = 0;
            //得到文件集合
            TXCommons.PictureModular.DocumentInfo docList = null;
            docList = TXCommons.PictureModular.GetPicture.GetDocumentInfo(GUID, true, documentType);
            //存入视图
            ViewData["GUID"] = GUID;
            ViewData["documentType"] = documentType;
            ViewData["cityId"] = cityId;
            ViewData["filelist"] = docList;
        }

        /// <summary>
        /// 得到当前用户VIP状态（0未认证；1已通过；2未通过；3审核中）
        /// Author:台永海
        /// </summary>
        /// <returns></returns>
        public int GetCurrentUserVipState()
        {
            var bll = new DevelopersBll();
            var entity = bll.GetEntity_ById(LoginUserInfo.Id) as Developer;
            return entity == null ? 0 : entity.State;
        }

        #endregion

        #region 用户图片处理

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadAuthenticationPhoto(string guid, string picturetype, int cityId)
        {
            GetUserPicListToViewData(guid, 2);
            List<TXCommons.PictureModular.UserPictureInfo> list = new List<TXCommons.PictureModular.UserPictureInfo>();
            list = TXCommons.PictureModular.GetPicture.GetUserPictureInfos(guid, true, picturetype);
            ViewData["list"] = list != null && list.Count > 0 ? list[0] : null;
            return View();
        }



        #endregion

        #region 用户合同处理
        
        /// <summary>
        /// 用户合同处理
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadAuthenticationHeTong(string guid, string filetype, int cityId)
        {
            GetDocumentInfoListToViewData(guid, 0);
            var file = new TXCommons.PictureModular.DocumentInfo();
            file = TXCommons.PictureModular.GetPicture.GetDocumentInfo(guid, true, filetype.ToString(CultureInfo.InvariantCulture));
            ViewData["file"] = file;
            return View();
        } 

        #endregion
    }
}
