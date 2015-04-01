using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.LocalUsers;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Common;
using ServiceStack;
using KYJ.ZS.Models.LocalUsers;

namespace KYJ.ZS.BLL.LocalUsers
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-04-28
    /// Desc:本地用户的增删改查
    /// </summary>
    public class LocalUserBll
    {
        private LocalUserDal _dal = null;

        public LocalUserBll()
        {
            if (_dal == null)
            {
                _dal = new LocalUserDal();
            }
        }

        #region 用户登录

        /// <summary>
        /// 用户后台登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="isautologin">是否</param>
        /// <returns></returns>
        public int Login(string loginName, string passWord, bool isautologin)
        {

            var userXml = _dal.GetUserInfoByService(loginName, passWord);
            if (!userXml.IsNullOrEmpty())
            {
                //反序列化xml字符串
                var user = Auxiliary.Deserialize(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userXml) as KYJ.ZS.DAL.UserWebService.UserInfo;
                if (user != null)
                {
                    if (user.AllState == 1)
                    {
                        return 2;
                    }
                    var syncUser = new KYJ.ZS.Models.LocalUsers.SyncUserEntity();
                    syncUser.UserId = user.Id;
                    syncUser.Email = user.Email;
                    syncUser.PWD = user.Pwd;
                    syncUser.LoginName = user.LoginName;
                    syncUser.Mobile = user.Mobile;
                    syncUser.NickName = user.NikeName;
                    syncUser.RealName = user.RealName;
                    syncUser.Sex = user.Sex;
                    syncUser.State = user.AllState;
                    syncUser._GUID = user.Guid;
                    try
                    {
                        DateTime last;
                        last = _dal.GetLocalUserByUserID(user.Id).LastLoginTime;
                        //同步公共平台用户和本地平台用户
                        if (_dal.IsExistLoginUser(user.Id))
                            _dal.Update(syncUser);
                        else
                            _dal.Create(syncUser);
                        var cookieName = Auxiliary.ConfigKey("cookie_name");
                        CookieTool.SetLoginCookie(syncUser._GUID, cookieName, isautologin);
                        var loginUser = _dal.GetLoginUserInfoByUserID(syncUser.UserId);
                        RedisTool.RemoveLoginUserInfo(syncUser._GUID, UserInfoType.USERLOGIN);
                        loginUser.LastLoginTime = last.ToString("yyyy-MM-dd HH:mm");
                        RedisTool.AddLoginUserInfo(loginUser, syncUser._GUID, isautologin, UserInfoType.USERLOGIN);
                        return 1;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }

                }

            }
            return 0;
        }

        #endregion

        #region 用户注册
        /// <summary>
        /// 租售项目，普通用户注册
        /// </summary>
        /// <param name="entity">RegisterUserEntity数据实体</param>
        /// <returns>RegisterType注册提醒枚举</returns>
        public RegisterType Register(RegisterUserEntity entity)
        {
            var guid = string.Empty;
            if (!_dal.CheckUserLoginName(entity.LoginName))
            {
                var uid = _dal.AddUserToUserInfo(entity.LoginName, entity.Password, out guid);
                if (uid > 0)
                {
                    //添加本地帐号
                    var result = _dal.Create(new SyncUserEntity() { UserId = uid, _GUID = guid, LoginName = entity.LoginName, Mobile = entity.LoginName, PWD = entity.Password, Sex = 0, State = 0 });
                    if (result > 0)
                        return RegisterType.SUCCESS;
                    else
                        return RegisterType.PASS;
                }
            }
            return RegisterType.FAILURE;
        }
        /// <summary>
        /// 检查用户名手机号码是否允许注册
        /// </summary>
        /// <param name="mobile">用户名手机号码</param>
        /// <returns>	允许注册 true，不允许注册 false</returns>
        public bool CheckMobile(string mobile)
        {
            return !_dal.CheckUserLoginName(mobile) && !_dal.CheckUserMobile(mobile);
        }

        #endregion

        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:查询个人用户基本信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>基本信息实体</returns>
        public LocalUser GetLocalUser(int userId)
        {
            return _dal.GetLocalUser(userId);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-07
        /// Desc:获取指定用户ID的用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>LocalUserEntity数据实体</returns>
        public LocalUserEntity GetLocalUserByUserID(int userId)
        {
            return _dal.GetLocalUserByUserID(userId);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// 更新用户基本资料信息
        /// </summary>
        /// <param name="entity">UserInfoEntity数据实体</param>
        /// <returns>1 成功 0 失败</returns>
        public int UpdateUserInfo(UserInfoEntity entity)
        {
            return _dal.UpdateUserInfo(entity);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// 更新用户教育情况信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateUserEducation(UserEducationEntity entity)
        {
            return _dal.UpdateUserEducation(entity);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// 更新用户密码
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="oldpassword">旧密码</param>
        /// <param name="newpassword">新密码</param>
        /// <returns></returns>
        public int UpdatePwd(int userid, string oldpassword, string newpassword)
        {
            return _dal.UpdatePwd(userid, oldpassword, newpassword);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// 更新用户密码
        /// </summary>
        /// <param name="loginname">用户名</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        public int UpdatePwd(string loginname, string password)
        {
            return _dal.UpdatePwd(loginname, password);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// 更新用户名片
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="companyname">公司名称</param>
        /// <param name="companyposition">公司职位</param>
        /// <returns></returns>
        public int UpdateCard(int userid, string companyname, string companyposition)
        {
            return _dal.UpdateCard(userid, companyname, companyposition);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// 申请认证
        /// </summary>
        /// <param name="entity">AuthenticationEntity数据实体</param>
        /// <returns></returns>
        public int ApplyAuthentication(AuthenticationEntity entity)
        {
            //默认为认证中，带审核状态
            entity.PapersStatus = 2;
            return _dal.ApplyAuthentication(entity);
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        public AuthenticationEntity GetAuthenticationByUserID(int userid)
        {
            return _dal.GetAuthenticationByUserID(userid);
        }

        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-15
        /// Desc:获得个人用户帐户信息
        /// </summary>
        /// <returns>个人帐户实体类</returns>
        public UserAccountEntity GetUserAccountInfo(int userId)
        {
            return _dal.GetUserAccountInfo(userId);
        }
        /// <summary>
        /// uthor:baiyan
        /// Time:2014-4-17
        /// Desc:修改个人用户基本信息
        /// </summary>
        /// <param name="userInfo">用户基本信息实体</param>
        /// <returns></returns>
        public int UpdateLocalUser(LocalUser userInfo)
        {
            return _dal.UpdateLocalUser(userInfo);
        }



        #region 管理普通用户---ningjd

        /// <summary>
        /// 获取普通用户列表
        /// </summary>
        /// <param name="loginName">账号</param>
        /// <param name="status">身份认证</param>
        /// <param name="realName">姓名</param>
        /// <param name="state">状态</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<LocalUserManageEntity> GetUserManageList(string loginName, int status, string realName, int state, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return _dal.GetUserManageList(loginName, status, realName, state, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 获取普通用户列表
        /// </summary>
        /// <param name="loginName">账号</param>
        /// <param name="realName">姓名</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<LocalUserManageEntity> GetUserManageList(string loginName, string realName, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return _dal.GetUserManageList(loginName, 2, realName, -1, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="papersRemark">认证描述</param>
        /// <param name="isValidate">是否通过认证</param>
        /// <returns></returns>
        public bool Audited(int id, string papersRemark, bool isValidate)
        {
            return _dal.Audited(id, papersRemark, isValidate) > 0;
        }

        /// <summary>
        /// 冻结操作
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isFreeze">是否冻结</param>
        /// <returns></returns>
        public bool Freeze(int id, bool isFreeze)
        {
            return _dal.Freeze(id, isFreeze) > 0;
        }

        #endregion
    }
}
