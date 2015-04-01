using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Manager;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Commons.Common;
using KYJ.ZS.Commons;
using KYJ.ZS.BLL.Authority;
using KYJ.ZS.Models.Authority;

namespace KYJ.ZS.BLL.Manager
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-21
    /// 描述：登录
    /// </summary>
    public class ManagerBll
    {
        ManagerDal dal = new ManagerDal();
        LoginFilterBll loginBll = new LoginFilterBll();
        public bool Login(string adminName,string adminPwd,out string backMessage)
        {
            string Result = string.Empty;
            bool result = false;
            Admins admin = null;
             admin = dal.GetAdmin(adminName);//获取用户信息
            if (admin == null)
            {
                backMessage = "该用户不存在！";
            }
            else
            {
                if (admin.PassWord == adminPwd)//检查密码是否一致
                {
                          
                        result = true;
                        LoginUserInfo loginUser = new LoginUserInfo();
                        loginUser.LastLoginTime = admin.LastLoginTime.ToString();
                        loginUser.LoginName = admin.AdminName;
                        loginUser.UserID = admin.AdminId;
                        loginUser.RealName = admin.RealName;
                        loginUser.Mobile = admin.Mobile;
                        loginUser._Guid = Guid.NewGuid().ToString();
                        #region 设置缓存，保存用户登录状态
                        var cookieName = Auxiliary.ConfigKey("cookie_name");
                        CookieTool.SetLoginCookie(loginUser._Guid, cookieName, false);
                        RedisTool.AddLoginUserInfo(loginUser,loginUser._Guid, false, UserInfoType.MANAGERLOGIN);
                        #endregion
                        backMessage = "登录成功！";
                        #region 获取角色下的所有权限，存入cookie
                        List<RoleManagerEntity> list = loginBll.GetRoleManagerEntity(admin.RoleId);
                        Result = SerializeHelper.ToJson(list);
                        CookieTool.SetCookie(Result, "userPermission",DateTime.Now.AddDays(7));
                        #endregion
                        dal.UpDatelastLoginTime(admin.AdminId);
                  
                }
                else
                {
                    backMessage = "登录失败，密码错误！";
                }
            }

            return result;
        }
        }
 }
