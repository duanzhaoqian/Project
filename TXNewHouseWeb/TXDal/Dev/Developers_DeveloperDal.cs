﻿using System;
using System.Linq;
using TXOrm;
using System.Data.SqlClient;
using TXModel.Commons;
using TXModel.Dev;

namespace TXDal.Dev
{
    /// <summary>
    /// 开发商(开发商后台)
    /// </summary>
    public partial class DevelopersDal
    {
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
            int result = 0;
            try
            {
                #region SQL语句
                string sql = @"
                BEGIN TRY
	                BEGIN TRAN
	                BEGIN
		                UPDATE Developer_Identity 
		                SET [Type] = @W_Type, CompanyName = @W_CompanyName, CompanyAddress = @W_CompanyAddress,
		                ProvinceId = @W_ProvinceId, ProvinceName = @W_ProvinceName, CityId = @W_CityId, 
		                CityName = @W_CityName,  DId = @W_DId ,DName = @W_DName,
		                UserName = @W_UserName, UserMobile = @W_UserMobile, UserEmail = @W_UserEmail
		                WHERE DeveloperId = @W_DeveloperId
                        IF(@@ERROR > 0)
		                BEGIN
							ROLLBACK TRAN
							SELECT '0' AS Code, 'Developer_Identity表修改错误' AS Msg
							RETURN
		                END
		                UPDATE Developer SET State = 3 WHERE Id = @W_DeveloperId
                        IF(@@ERROR > 0)
		                BEGIN
							ROLLBACK TRAN
							SELECT '0' AS Code, 'Developer表修改错误' AS Msg
							RETURN
		                END
	                    COMMIT TRAN
                        SELECT '1' AS Code, '执行成功' AS Msg
	                END
                END TRY
                BEGIN CATCH
	                ROLLBACK TRAN
                END CATCH";
                #endregion

                #region 参数
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@W_Type",entity.Type),
                    new SqlParameter("@W_CompanyName",entity.CompanyName),
                    new SqlParameter("@W_CompanyAddress",entity.CompanyAddress),
                    new SqlParameter("@W_ProvinceId",entity.ProvinceId),
                    new SqlParameter("@W_ProvinceName",entity.ProvinceName),
                    new SqlParameter("@W_CityId",entity.CityId),
                    new SqlParameter("@W_CityName",entity.CityName),
                    new SqlParameter("@W_DId",entity.DId),
                    new SqlParameter("@W_DName",entity.DName),
                    new SqlParameter("@W_UserName",entity.UserName),
                    new SqlParameter("@W_UserMobile",entity.UserMobile),
                    new SqlParameter("@W_UserEmail",entity.UserEmail),
                    new SqlParameter("@W_DeveloperId",entity.DeveloperId)
                };
                #endregion

                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var query = houseDB.ExecuteStoreQuery<ESqlResult>(sql, para);
                    var rowResult = query.ToList();
                    if (rowResult.Count > 0)
                    {
                        result = Convert.ToInt32(rowResult[0].Code);
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", entity, e);
                //throw;
            }
            return result;
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
            CT_DeveAndIdenInfo entity = null;
            try
            {
                string sql = @"
                SELECT deve.*, 
                iden.Id AS Id_Iden, iden.DeveloperId, iden.UserName, iden.UserMobile, iden.UserEmail, iden.[Type], 
                iden.ProvinceId AS ProvinceId_Iden, iden.ProvinceName AS ProvinceName_Iden, iden.CityId AS CityId_Iden, 
                iden.CityName AS CityName_Iden, iden.DId, iden.DName, iden.CompanyName, iden.CompanyAddress, iden.Refuse, 
                iden.CreateTime AS CreateTime_Iden, iden.IsDel AS IsDel_Iden 
                FROM Developer AS deve WITH(NOLOCK) INNER JOIN Developer_Identity AS iden WITH(NOLOCK)
                ON deve.Id = iden.DeveloperId
                WHERE deve.IsDel = 0 AND iden.IsDel = 0 AND deve.Id = ";
                sql = String.Concat(sql, userId);
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var query = houseDB.ExecuteStoreQuery<CT_DeveAndIdenInfo>(sql, null);
                    entity = query.FirstOrDefault<CT_DeveAndIdenInfo>();
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Concat("userId:", userId), e);
                //throw;
            }
            return entity;
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
            int result = 0;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    Developer entity = houseDB.Developers.FirstOrDefault(e => e.Mobile == mobile);
                    if (null != entity)
                    {
                        entity.Pwd = newPwd;
                        result = houseDB.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("mobile:{0},newPwd:{1}", mobile, newPwd), e);
                //throw;
            }
            return result;
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
            int result = 0;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    Developer entity = houseDB.Developers.FirstOrDefault(e => e.Id == userId && e.Pwd == oldPwd);
                    if (null != entity)
                    {
                        entity.Pwd = newPwd;
                        result = houseDB.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("userId:{0},oldPwd:{1},newPwd:{2}", userId, oldPwd, newPwd), e);
                //throw;
            }
            return result;
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
            int result = 0;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var entity = houseDB.Developers.FirstOrDefault(e => e.Id == userId && e.Pwd == pwd);
                    if (null != entity)
                    {
                        entity.Mobile = mobile;
                        return houseDB.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("userId:{0},pwd:{1},mobile:{2}", userId, pwd, mobile), e);
                //throw;
            }
            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// Author:台永海
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        public int UpdateUserInfo(Developer entity)
        {
            int result = 0;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var tempEntity = houseDB.Developers.FirstOrDefault(e => e.Id == entity.Id);
                    if (null != tempEntity)
                    {
                        tempEntity.Email = entity.Email;
                        tempEntity.Telephone = entity.Telephone;
                        tempEntity.SpareTelephone = entity.SpareTelephone;
                        tempEntity.ProvinceId = entity.ProvinceId;
                        tempEntity.ProvinceName = entity.ProvinceName;
                        tempEntity.CityId = entity.CityId;
                        tempEntity.CityName = entity.CityName;
                        result = houseDB.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", entity, e);
                //throw;
            }
            return result;
        }

        #endregion

        #region 登录相关

        /// <summary>
        /// 登录-根据用户名密码得到用户实体
        /// Author:台永海
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public Developer Login(string loginName, string pwd)
        {
            Developer entity = null;
            try
            {
                string sql = @"
                DECLARE @Temp INT
                SET @Temp = 0
                --查询开发商信息
                SELECT * FROM Developer WITH(NOLOCK) WHERE ((LoginName = @W_LoginName AND Pwd = @W_Pwd) OR (Mobile = @W_LoginName AND Pwd = @W_Pwd))
                --检查开发商信息是否有效
                SELECT @Temp = 1 WHERE EXISTS(SELECT * FROM Developer WITH(NOLOCK)
                WHERE ((LoginName = @W_LoginName AND Pwd = @W_Pwd) OR (Mobile = @W_LoginName AND Pwd = @W_Pwd)) AND LockState = 0 AND IsDel = 0)
                IF(@Temp = 1)
                BEGIN
                    --修改最后登录时间
	                UPDATE Developer SET OldLoginTime = LoginTime, LoginTime = GETDATE() 
	                WHERE ((LoginName = @W_LoginName AND Pwd = @W_Pwd) OR (Mobile = @W_LoginName AND Pwd = @W_Pwd)) AND LockState = 0 AND IsDel = 0
                END";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@W_LoginName", loginName),
                    new SqlParameter("@W_Pwd", pwd)
                };
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var query = houseDB.ExecuteStoreQuery<Developer>(sql, para);
                    entity = query.FirstOrDefault<Developer>();
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("W_LoginName:{0},W_Pwd:{1}", loginName, pwd), e);
                //throw;
            }
            return entity;
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
            bool result = false;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var query = (from deve in houseDB.Developers
                                 where deve.LoginName == loginName
                                 select deve);
                    if (query.FirstOrDefault<Developer>() == null)
                        result = true;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Concat("LoginName:", loginName), e);
                //throw;
            }
            return result;
        }
        /// <summary>
        /// 注册-手机号验证
        /// Author:台永海
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public bool CheckMobile(string mobile)
        {
            bool result = false;
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var query = houseDB.Developers.FirstOrDefault(e => e.Mobile == mobile);
                    if (query == null)
                        result = true;
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Concat("Mobile:", mobile), e);
                //throw;
            }
            return result;
        }
        /// <summary>
        /// 注册-开发商用户
        /// Author:台永海
        /// </summary>
        /// <param name="entity">开发商实体</param>
        /// <returns>受影响的行数</returns>
        public int Register(Developer entity)
        {
            int result = 0;
            try
            {
                #region SQL语句
                string sql = @"
                BEGIN TRY
	                BEGIN TRAN
	                BEGIN
		                DECLARE @DeveID INT, @Temp INT
		                SELECT @DeveID = 0, @Temp = 0
		                --检查登录名是否重复
		                SELECT @Temp = 1 FROM Developer WHERE LoginName = @W_LoginName
		                IF(@Temp = 0)
		                BEGIN
		                    --检查手机号是否重复
		                    SELECT @Temp = 1 FROM Developer WHERE Mobile = @W_Mobile
		                    IF(@Temp = 0)
		                    BEGIN
			                    --添加开发商信息
			                    INSERT INTO Developer (ProvinceID, ProvinceName, CityId, CityName, Mobile, Email, Telephone, SpareTelephone, LoginName, Pwd, RealName, State, LockState, InnerCode, LoginTime, OldLoginTime, CreateTime, IsDel)                        
			                    VALUES (0, N'', 0, N'', @W_Mobile, N'', N'', N'', @W_LoginName, @W_Pwd, @W_RealName, 0, 0, @W_InnerCode, GETDATE(), GETDATE(), GETDATE(), 0)
                                IF(@@ERROR > 0)
                                BEGIN
									ROLLBACK TRAN
									SELECT '0' AS Code, 'Developer表插入错误' AS Msg
									RETURN
                                END
			                    --得到开发商ID
			                    SET @DeveID = @@IDENTITY
			                    --添加开发商账户信息
			                    INSERT INTO Developer_Account (DeveloperId, Price, CreateTime, UpdateTime, IsDel)
			                    VALUES (@DeveID, 0, GETDATE(), GETDATE(), 0)
                                IF(@@ERROR > 0)
                                BEGIN
									ROLLBACK TRAN
									SELECT '0' AS Code, 'Developer_Account表插入错误' AS Msg
									RETURN
                                END
                                --添加开发商身份认证信息
                                INSERT INTO Developer_Identity (DeveloperId, UserName, UserMobile, UserEmail, [Type], ProvinceId, ProvinceName, CityId, CityName, DId, DName, CompanyName, CompanyAddress, Refuse, CreateTime, IsDel)
                                VALUES (@DeveID, N'', '', '', 0, 0, N'', 0, N'', 0, N'', N'', N'', N'', GETDATE(), 0)
                                IF(@@ERROR > 0)
                                BEGIN
									ROLLBACK TRAN
									SELECT '0' AS Code, 'Developer_Identity表插入错误' AS Msg
									RETURN
                                END
			                    COMMIT TRAN
                                SELECT '1' AS Code, '执行成功' AS Msg
		                    END
		                END
	                END
                END TRY
                BEGIN CATCH
	                ROLLBACK TRAN
                END CATCH";
                #endregion

                #region 参数
                SqlParameter[] para = new SqlParameter[]{
                    new SqlParameter("@W_Mobile",entity.Mobile),
                    new SqlParameter("@W_LoginName",entity.LoginName),
                    new SqlParameter("@W_Pwd",entity.Pwd),
                    new SqlParameter("@W_RealName",entity.RealName),
                    new SqlParameter("@W_InnerCode",entity.InnerCode)
                };
                #endregion

                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var query = houseDB.ExecuteStoreQuery<ESqlResult>(sql, para);
                    var rowResult = query.ToList();
                    if (rowResult.Count > 0)
                    {
                        result = Convert.ToInt32(rowResult[0].Code);
                    }
                }
            }
            catch (Exception e)
            {
                Log4netService.RecordLog.RecordException("台永海", String.Format("W_Mobile:{0},W_LoginName:{1},W_Pwd:{2},W_RealName{3}", entity.Mobile, entity.LoginName, entity.Pwd, entity.RealName), e);
                //throw;
            }
            return result;
        }

        #endregion

    }
}