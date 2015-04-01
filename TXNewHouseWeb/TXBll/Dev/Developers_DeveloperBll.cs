﻿using TXOrm;
using TXDal.Dev;
using TXModel.Dev;

namespace TXBll.Dev
{
    /// <summary>
    /// 开发商 (开发商后台)
    /// </summary>
    public partial class DevelopersBll
    {
        DevelopersDal _dal = new DevelopersDal();

        #region 开发商身份信息相关

        #region 修改开发商身份信息
        /// <summary>
        /// 修改开发商身份信息
        /// Author:台永海
        /// </summary>
        /// <param name="entity">身份信息实体</param>
        /// <returns></returns>
        public int UpdateIdentification(Developer_Identity entity)
        {
            return _dal.UpdateIdentification(entity);
        }
        #endregion

        #region 得到开发商及身份信息
        /// <summary>
        /// 得到开发商及身份信息
        /// Author:台永海
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public CT_DeveAndIdenInfo GetDeveAndIdenInfo(int userId)
        {
            return _dal.GetDeveAndIdenInfo(userId);
        }
        #endregion

        #endregion

        #region 开发商信息相关

        /// <summary>
        /// 修改密码
        /// Author:台永海
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public int UpdatePassword(string mobile, string newPwd)
        {
            return _dal.UpdatePassword(mobile, newPwd);
        }

        /// <summary>
        /// 修改密码
        /// Author:台永海
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public int UpdatePassword(int userId, string oldPwd, string newPwd)
        {
            return _dal.UpdatePassword(userId, oldPwd, newPwd);
        }

        /// <summary>
        /// 修改手机号
        /// Author:台永海
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pwd">密码</param>
        /// <param name="mobile">新手机号</param>
        /// <returns></returns>
        public int UpdateMobile(int userId, string pwd, string mobile)
        {
            return _dal.UpdateMobile(userId, pwd, mobile);
        }

        /// <summary>
        /// 修改用户信息
        /// Author:台永海
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        public int UpdateUserInfo(Developer entity)
        {
            return _dal.UpdateUserInfo(entity);
        }

        #endregion

        #region 登录相关

        /// <summary>
        /// 登录-根据用户名密码得到用户实体
        /// Author:台永海
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns>0内部错误、-1用户名或密码错误、-2未认证、-3未通过审核、-4已锁定、-5已删除、大于0登录成功返回用户ID</returns>
        public int Login(string loginName, string pwd)
        {
            int result = 0;
            Developer entity = _dal.Login(loginName, pwd);
            if (entity == null)//用户名或密码错误
            {
                result = -1;
            }
            else if (entity.Pwd != pwd)//用户名或密码错误
            {
                result = -1;
            }
            else if (entity.LockState == 1)//已锁定
            {
                result = -2;
            }
            else if (entity.IsDel == true)//已删除
            {
                result = -3;
            }
            else if ((entity.LoginName == loginName && entity.Pwd == pwd) || (entity.Mobile == loginName && entity.Pwd == pwd))//登录成功
            {
                result = entity.Id;
            }
            return result;
        }

        #endregion

        #region 注册相关

        /// <summary>
        /// 注册-登录名验证
        /// Author:台永海
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        public bool CheckLoginName(string loginName)
        {
            return _dal.CheckLoginName(loginName);
        }
        /// <summary>
        /// 注册-手机号验证
        /// Author:台永海
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public bool CheckMobile(string mobile)
        {
            return _dal.CheckMobile(mobile);
        }
        /// <summary>
        /// 注册-开发商用户
        /// Author:台永海
        /// </summary>
        /// <param name="entity">开发商实体</param>
        /// <returns>受影响的行数</returns>
        public int Register(Developer entity)
        {
            return _dal.Register(entity);
        }

        #endregion

    }
}