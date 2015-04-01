using System;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.DAL.Merchants;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Merchants;
using System.Collections.Generic;

namespace KYJ.ZS.BLL.Merchants
{
    /// <summary>
    /// 作者:maq
    /// 时间：2014年4月24日
    /// 描述：商户后台
    /// </summary>
    public partial class MerchantBll
    {
        MerchantDal dal = new MerchantDal();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public Boolean Login(string loginName, string loginPwd, out string backMessage)
        {
            Boolean result = false;
            Merchant m = null;
            m = dal.GetModel(loginName);//获取用户信息
            if (m == null)
            {
                backMessage = "账号或密码有误，请重新输入！";
            }
            else
            {
                if (m.Pwd == loginPwd)//检查密码是否一致
                {
                    if (m.State == 0)//检查用户状态是否锁定
                    {
                        result = true;
                        LoginUserInfo loginUser = new LoginUserInfo();
                        loginUser.Email = m.Email;
                        loginUser.LastLoginTime = DateTime.Now.ToString();
                        loginUser.LoginName = m.LoginName;
                        loginUser.UserID = m.Id;
                        loginUser.NickName = m.Name;
                        loginUser.RealName = m.RealName;
                        loginUser.Mobile = m.Mobile;
                        loginUser._Guid = m.Guid;
                        #region 设置缓存，保存用户登录状态
                        var cookieName = Auxiliary.ConfigKey("cookie_name");
                        CookieTool.SetLoginCookie(m.Guid, cookieName, false);
                        RedisTool.AddLoginUserInfo(loginUser, m.Guid, false, UserInfoType.MERCHANTLOGIN);
                        #endregion
                        backMessage = "登录成功！";
                        dal.UpDatelastLoginTime(m.Id);
                    }
                    else
                    {
                        backMessage = "您的账户当前处于锁定状态！";
                    }
                }
                else
                {
                    backMessage = "账号或密码有误，请重新输入！";
                }
            }

            return result;
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public void LoginOut(string userGuid)
        {
            var cookieName = Auxiliary.ConfigKey("cookie_name");
            CookieTool.RemoveCookie(cookieName);
            RedisTool.RemoveLoginUserInfo(userGuid, UserInfoType.MERCHANTLOGIN);
        }


        /// <summary>
        /// Auther:李晓波
        /// Time:2014-4-24
        /// 检查当前商户输入密码是否正确
        /// </summary>    
        /// <param name="pwd">商户id</param>
        /// <param name="pwd">用户输入密码</param>
        /// <returns></returns>
        public bool getPwd(int id, string pwd)
        {
            return pwd == dal.getPwd(id);
        }

        /// <summary>
        /// 根据用户ID获取其PWD
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserPWD(int id)
        {
            return dal.getPwd(id);
        }
        /// <summary>
        /// 修改商户登录密码
        /// </summary>
        /// <param name="id">商户id</param>
        /// <param name="newPwd">新密码</param>
        public int UpdatePwd(int id,string oldPwd, string newPwd)
        {
            return dal.UpdatePwd(id, oldPwd, newPwd);
        }

        #region 商户管理----ningjd

        /// <summary>
        /// 账户验证
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool ValidateLoginName(string loginName)
        {
            return dal.ValidateLoginName(loginName) <= 0;
        }

        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="entity">商户添加Entity</param>
        /// <returns></returns>
        public bool Create(MerchantCreateEntity entity)
        {
            return dal.Create(entity) > 0;
        }

        /// <summary>
        /// 获取商户管理列表
        /// </summary>
        /// <param name="name">企业名称</param>
        /// <param name="loginName">企业账号</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<MerchantIndexEntity> GetMerchantList(string name, string loginName, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetMerchantList(name, loginName, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 获取商户名称
        /// </summary>
        /// <param name="id">商户ID</param>
        /// <returns></returns>
        public string GetMerchantName(int id)
        {
            return dal.GetMerchantName(id);
        }

        #endregion
    }
}
