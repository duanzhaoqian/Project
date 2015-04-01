using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.DB;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.Models.LocalUsers;

namespace KYJ.ZS.DAL.LocalUsers
{
    /// <summary>
    /// Author:zhuzh
    /// Time:2014-4-17
    /// Desc:操作数据库表LocalUsers，包括查询、添加、修改
    /// </summary>
    public class LocalUserDal
    {

        #region 登录

        /// <summary>
        /// 用户后台登录
        /// </summary>
        /// <param name="loginName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public string GetUserInfoByService(string loginName, string passWord)
        {
            UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
            //获取公共平台用户数据
            var userXml = _ous.Login("binPath", "ZSClassPath", loginName, passWord, "ZS");

            return userXml;
        }

        public KYJ.ZS.Commons.Common.LoginUserInfo GetLoginUserInfoByUserID(int userId)
        {
            #region SQL
            var sql = @"SELECT [user_id]
                          ,[user_loginname]
                          ,[user_email]
                          ,[user_mobile]
                          ,[user_realname]
                          ,[user_nickname]
                          ,[user_sex]
                          ,[user_state]
                          ,[user_rank]
                          ,[user_integral]
                          ,[user_guid]
                          ,[user_lastlogintime]
                      FROM [dbo].[LocalUsers] NOLOCK WHERE [user_id]=@user_id";
            #endregion

            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            #endregion

            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion

            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                KYJ.ZS.Commons.Common.LoginUserInfo userInfo = new KYJ.ZS.Commons.Common.LoginUserInfo();
                var item = dt.Rows[0];
                userInfo.UserID = Auxiliary.ToInt32(item["user_id"]);
                userInfo.LoginName = item["user_loginname"].ToString();
                userInfo.Email = item["user_email"].ToString();
                userInfo.Mobile = item["user_mobile"].ToString();
                userInfo.RealName = item["user_realname"].ToString();
                userInfo.NickName = item["user_nickname"].ToString();
                userInfo.Rank = Auxiliary.ToInt32(item["user_rank"]);
                userInfo.Integral = Auxiliary.ToInt32(item["user_integral"]);
                userInfo._Guid = item["user_guid"].ToString();
                userInfo.LastLoginTime = Convert.ToDateTime(item["user_lastlogintime"]).ToString("yyyy-MM-dd HH:mm:ss");
                return userInfo;
            }
            return null;
            #endregion
        }

        #endregion

        #region 注册

        /// <summary>
        /// 添加公共平台用户数据
        /// </summary>
        /// <param name="loginname">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public int AddUserToUserInfo(string loginname, string password, out string guid)
        {
            guid = string.Empty;
            try
            {
                #region 创建UserInfo对象

                var userInfo = new KYJ.ZS.DAL.UserWebService.UserInfo();
                userInfo.LoginName = loginname;//通过活动平台注册的用户登录名是手机号码
                userInfo.Pwd = password;
                userInfo.Email = string.Empty;
                userInfo.IDNO = string.Empty;
                userInfo.Guid = Guid.NewGuid().ToString();
                userInfo.Mobile = loginname;
                userInfo.NikeName = "";
                userInfo.RealName = "";
                userInfo.Sex = 0;
                userInfo.Status = 0;
                userInfo.UpdateTime = DateTime.Now;
                userInfo.CreateTime = DateTime.Now;
                userInfo.LastLoginTime = DateTime.Now;
                userInfo.IsDel = false;
                userInfo.AllState = 0;
                userInfo.SpareTelephone = "";
                userInfo.Telephone = "";
                userInfo.IdCard = "";
                userInfo.AdminId = -1;
                userInfo.AdminName = "";
                userInfo.BId = -1;
                userInfo.BName = "";
                userInfo.CancelOrderNum = 3;
                userInfo.CityId = 0;
                userInfo.CityName = string.Empty;
                userInfo.CompanyId = -1;
                userInfo.DId = -1;
                userInfo.DName = "";
                userInfo.EshopUserType = 1;
                userInfo.FcUserType = 1;//11 房客  12房东
                userInfo.ZSUserType = 1;
                userInfo.ProvinceID = -1;
                userInfo.ProvinceName = "";
                userInfo.RepastUserType = 1;
                userInfo.Score = 0;
                userInfo.UserType = "";//各平台的用户类型组合，用“，”分割
                userInfo.TenantScore = 0;
                userInfo.Platform = "ZS"; //2013-5-23 添加
                userInfo.InfoCall = "";     //20130613
                userInfo.QQ = ""; //20130918
                userInfo.WeiXin = ""; //2013116

                #endregion

                UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();

                var tempUser = Auxiliary.Serializer(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userInfo);

                var userXml = _ous.Register("binPath", "ZSClassPath", tempUser);

                var user = Auxiliary.Deserialize(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userXml) as KYJ.ZS.DAL.UserWebService.UserInfo;

                guid = userInfo.Guid;

                if (user != null)
                {
                    return user.Id;  //获取到基础框架中用户的ID
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", "添加公共平台用户数据异常", ex);
                return 0;
            }
        }
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="loginname">用户名</param>
        /// <returns>	存在返回true，不存在返回false</returns>
        public bool CheckUserLoginName(string loginname)
        {
            try
            {
                UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
                return _ous.CheckUserLoginName("binPath", "ZSClassPath", loginname, "ZS");
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", "检查用户名是否存在异常", ex);
                return false;
            }
        }
        /// <summary>
        /// 检查手机号是否存在
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>	存在返回true，不存在返回false</returns>
        public bool CheckUserMobile(string mobile)
        {
            try
            {
                UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
                return _ous.CheckUserMobile("binPath", "ZSClassPath", mobile, "ZS");
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", "检查手机号是否存在异常", ex);
                return false;
            }

        }

        #endregion
        /// <summary>
        /// 同步公共平台帐号
        /// </summary>
        /// <param name="stncUserEntity">SyncUserEntity数据实体</param>
        /// <returns></returns>
        public int Create(SyncUserEntity stncUserEntity)
        {
            try
            {
                #region SQL
                var sql = @"BEGIN TRANSACTION
                            BEGIN
                                INSERT  INTO [dbo].[LocalUsers]
                                        ( [user_id] ,
                                          [user_loginname] ,
                                          [user_email] ,
                                          [user_mobile] ,
                                          [user_pwd] ,
                                          [user_paypwd] ,
                                          [user_realname] ,
                                          [user_nickname] ,
                                          [user_sex] ,
                                          [user_state] ,
                                          [user_rank] ,
                                          [user_integral] ,
                                          [user_guid] ,
                                          [user_createtime] ,
                                          [user_updatetime] ,
                                          [user_lastlogintime] ,
                                          [user_constellation]
                                        )
                                VALUES  ( @user_id ,
                                          @user_loginname ,
                                          @user_email ,
                                          @user_mobile ,
                                          @user_pwd ,
                                          @user_paypwd ,
                                          @user_realname ,
                                          @user_nickname ,
                                          @user_sex ,
                                          @user_state ,
                                          @user_rank ,
                                          @user_integral ,
                                          @user_guid ,
                                          @user_createtime ,
                                          @user_updatetime ,
                                          @user_lastlogintime ,
                                          @user_constellation
                                        )
                                IF @@ERROR <> 0 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  0  
                                        RETURN          
                                    END     
                                INSERT  INTO dbo.LocalUserOthers
                                        ( [user_id] )
                                VALUES  ( @user_id )
                                IF @@ERROR <> 0 
                                    BEGIN
                                        ROLLBACK TRANSACTION
                                        SELECT  0  
                                        RETURN
                                    END   
                                COMMIT TRANSACTION 
                                SELECT  1
                            END";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", stncUserEntity.UserId);
                param.AddParam("@user_loginname", stncUserEntity.LoginName);
                param.AddParam("@user_email", string.IsNullOrEmpty(stncUserEntity.Email) ? "" : stncUserEntity.Email);
                param.AddParam("@user_mobile", string.IsNullOrEmpty(stncUserEntity.Mobile) ? "" : stncUserEntity.Mobile);
                param.AddParam("@user_pwd", stncUserEntity.PWD);
                param.AddParam("@user_paypwd", string.Empty);
                param.AddParam("@user_realname", string.IsNullOrEmpty(stncUserEntity.RealName) ? "" : stncUserEntity.RealName);
                param.AddParam("@user_nickname", string.IsNullOrEmpty(stncUserEntity.NickName) ? "" : stncUserEntity.NickName);
                param.AddParam("@user_sex", stncUserEntity.Sex);
                param.AddParam("@user_papers", string.Empty);
                param.AddParam("@user_state", 0);
                param.AddParam("@user_rank", 0);
                param.AddParam("@user_integral", 0);
                param.AddParam("@user_guid", stncUserEntity._GUID);
                param.AddParam("@user_createtime", DateTime.Now);
                param.AddParam("@user_updatetime", DateTime.Now);
                param.AddParam("@user_lastlogintime", DateTime.Now);
                param.AddParam("@user_constellation", 0);

                #endregion

                #region 执行

                return KYJ_ZushouWDB.SqlExecute(sql, param);

                #endregion
            }
            catch (Exception ex)
            {
                //记录日志
                KYJ.ZS.Log4net.RecordLog.RecordException<SyncUserEntity>("zhuzh", stncUserEntity, ex);
                return 0;
            }
        }

        /// <summary>
        /// 用户登录数据同步(公共平台数据库同步)
        /// </summary>
        /// <returns></returns>
        public int Update(SyncUserEntity stncUserEntity)
        {
            #region SQL
            var sql = @"UPDATE  [dbo].[LocalUsers]
                        SET     
                                [user_loginname] = @user_loginname ,
                                [user_pwd] = @user_pwd ,
                                [user_realname] = @user_realname ,
                                [user_nickname] = @user_nickname ,
                                [user_email] = @user_email ,
                                [user_sex] = @user_sex ,
                                [user_mobile] = @user_mobile ,
                                [user_state] = @user_state,
                                [user_lastlogintime]=GETDATE()
                        WHERE   [user_id] = @user_id";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@user_loginname", stncUserEntity.LoginName);
            param.AddParam("@user_pwd", stncUserEntity.PWD);
            param.AddParam("@user_realname", stncUserEntity.RealName ?? string.Empty);
            param.AddParam("@user_nickname", stncUserEntity.NickName ?? string.Empty);
            param.AddParam("@user_email", stncUserEntity.Email ?? string.Empty);
            param.AddParam("@user_sex", stncUserEntity.Sex);
            param.AddParam("@user_mobile", stncUserEntity.Mobile);
            param.AddParam("@user_state", stncUserEntity.State);
            param.AddParam("@user_id", stncUserEntity.UserId);

            #endregion

            #region 执行

            return KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
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
            #region SQL
            var sql = @"BEGIN TRANSACTION
                        BEGIN
                            UPDATE  [dbo].[LocalUsers]
                            SET     [user_realname] = @user_realname ,
                                    [user_nickname] = @user_nickname ,
                                    [user_sex] = @user_sex ,
                                    [user_birthdayyear] = @user_birthdayyear ,
                                    [user_birthdaymonth] = @user_birthdaymonth ,
                                    [user_birthdayday] = @user_birthdayday ,
                                    [user_constellation] = @user_constellation,
                                    [user_updatetime] = GETDATE()
                            WHERE   [user_id] = @user_id
                            IF @@ERROR <> 0 
                                BEGIN
                                    ROLLBACK TRANSACTION
                                    SELECT  0  
                                    RETRUN  
                                END  
                            UPDATE  [dbo].[LocalUserOthers]
                            SET     [live_provinceid] = @live_provinceid ,
                                    [live_provincename] = @live_provincename ,
                                    [live_cityid] = @live_cityid ,
                                    [live_cityname] = @live_cityname ,
                                    [live_districtid] = @live_districtid ,
                                    [live_districtname] = @live_districtname ,
                                    [home_provinceid] = @home_provinceid ,
                                    [home_provincename] = @home_provincename ,
                                    [home_cityid] = @home_cityid ,
                                    [home_cityname] = @home_cityname ,
                                    [home_districtid] = @home_districtid ,
                                    [home_districtname] = @home_districtname
                            WHERE   [user_id] = @user_id
                            IF @@ERROR <> 0 
                                BEGIN
                                    ROLLBACK TRANSACTION
                                    SELECT  0  
                                    RETURN  
                                END  
                            COMMIT TRANSACTION 
                            SELECT  1
                        END";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@user_realname", entity.RealName);
            param.AddParam("@user_nickname", entity.NickName);
            param.AddParam("@user_sex", entity.Sex);
            param.AddParam("@user_birthdayyear", entity.BirthdayYear);
            param.AddParam("@user_birthdaymonth", entity.BirthdayMonth);
            param.AddParam("@user_birthdayday", entity.BirthdayDay);
            param.AddParam("@user_constellation", entity.Constellation);
            param.AddParam("@user_id", entity.UserId);

            param.AddParam("@live_provinceid", entity.LiveProvinceId);
            param.AddParam("@live_provincename", entity.LiveProvinceName);
            param.AddParam("@live_cityid", entity.LiveCityId);
            param.AddParam("@live_cityname", entity.LiveCityName);
            param.AddParam("@live_districtid", entity.LiveDistrictId);
            param.AddParam("@live_districtname", entity.LiveDistrictName);
            param.AddParam("@home_provinceid", entity.HomeProvinceId);
            param.AddParam("@home_provincename", entity.HomeProvinceName);
            param.AddParam("@home_cityid", entity.HomeCityId);
            param.AddParam("@home_cityname", entity.HomeCityName);
            param.AddParam("@home_districtid", entity.HomeDistrictId);
            param.AddParam("@home_districtname", entity.HomeDistrictName);

            #endregion

            #region 执行

            var result = KYJ_ZushouWDB.SqlExecute(sql, param);

            //修改成功，同步公共平台数据
            if (result > 0)
            {
                UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
                //获取公共平台用户数据
                var userXml = _ous.GetUserById("binPath", "ZSClassPath", entity.UserId, "ZS");
                if (!userXml.IsNullOrEmpty())
                {
                    //反序列化xml字符串
                    var user = Auxiliary.Deserialize(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userXml) as KYJ.ZS.DAL.UserWebService.UserInfo;
                    user.RealName = entity.RealName;
                    user.NikeName = entity.NickName;
                    user.Sex = Convert.ToByte(entity.Sex);
                    user.Platform = "ZS";
                    var newuserxml = Auxiliary.Serializer(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), user);
                    //同步用户数据
                    _ous.UpdateUser("binPath", "ZSClassPath", newuserxml);
                }

            }

            return result;

            #endregion
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-09
        /// 更新用户基本资料信息
        /// </summary>
        /// <param name="entity">UserInfoEntity数据实体</param>
        /// <returns>1 成功 0 失败</returns>
        public int UpdateUserEducation(UserEducationEntity entity)
        {
            #region SQL
            var sql = @"UPDATE  [dbo].[LocalUserOthers]
                        SET     [other_college] = @other_college,
                                [other_highschool] = @other_highschool,
                                [other_middleschool] = @other_middleschool,
                                [other_primaryschool] = @other_primaryschool
                        WHERE   [user_id] = @user_id
                        UPDATE  [dbo].[LocalUsers]
                        SET     [user_updatetime] = GETDATE()
                        WHERE   [user_id] = @user_id
                        ";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@other_college", entity.College);
            param.AddParam("@other_highschool", entity.HighSchool);
            param.AddParam("@other_middleschool", entity.MiddleSchool);
            param.AddParam("@other_primaryschool", entity.PrimarySchool);
            param.AddParam("@user_id", entity.UserId);

            #endregion

            #region 执行

            return KYJ_ZushouWDB.SqlExecute(sql, param);

            #endregion
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
            try
            {

                #region SQL
                var sql = @"IF EXISTS ( SELECT  [user_id]
                                    FROM    [dbo].[LocalUsers]
                                    WHERE   [user_id] = @user_id
                                            AND [user_pwd] = @user_oldpwd ) 
                            BEGIN
                                UPDATE  [dbo].[LocalUsers]
                                SET     [user_pwd] = @user_newpwd
                                WHERE   [user_id] = @user_id
                                SELECT  1
                            END
                        ELSE 
                            BEGIN
                                SELECT  0  
                            END    ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@user_oldpwd", oldpassword);
                param.AddParam("@user_newpwd", newpassword);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                //修改成功，同步公共平台数据
                if (Auxiliary.ToInt32(result) > 0)
                {
                    UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
                    //获取公共平台用户数据
                    var userXml = _ous.GetUserById("binPath", "ZSClassPath", userid, "ZS");
                    if (!userXml.IsNullOrEmpty())
                    {
                        //反序列化xml字符串
                        var user = Auxiliary.Deserialize(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userXml) as KYJ.ZS.DAL.UserWebService.UserInfo;
                        user.Pwd = newpassword;
                        user.Platform = "ZS";
                        var newuserxml = Auxiliary.Serializer(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), user);
                        //同步用户数据
                        _ous.UpdateUser("binPath", "ZSClassPath", newuserxml);
                    }

                }

                return Auxiliary.ToInt32(result);

                #endregion

            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", userid + "," + oldpassword + "," + newpassword, ex);
                return 0;
            }
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
            try
            {

                #region SQL
                var sql = @"IF EXISTS ( SELECT  [user_id]
                                    FROM    [dbo].[LocalUsers]
                                    WHERE   [user_loginname] = @user_loginname OR [user_mobile] = @user_loginname ) 
                            BEGIN
                                DECLARE @user_id INT  
                                UPDATE  [dbo].[LocalUsers]
                                SET     [user_pwd] = @user_pwd
                                WHERE   [user_loginname] = @user_loginname
                                SELECT  TOP 1 @user_id = [user_id]
                                FROM    [dbo].[LocalUsers]
                                WHERE   [user_loginname] = @user_loginname
                                        OR [user_mobile] = @user_loginname
                                SELECT  @user_id
                            END
                        ELSE 
                            BEGIN
                                SELECT  0  
                            END    ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_loginname", loginname);
                param.AddParam("@user_pwd", password);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                //修改成功，同步公共平台数据
                if (Auxiliary.ToInt32(result) > 0)
                {
                    UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
                    //获取公共平台用户数据
                    var userXml = _ous.GetUserById("binPath", "ZSClassPath", Auxiliary.ToInt32(result), "ZS");
                    if (!userXml.IsNullOrEmpty())
                    {
                        //反序列化xml字符串
                        var user = Auxiliary.Deserialize(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userXml) as KYJ.ZS.DAL.UserWebService.UserInfo;
                        user.Pwd = password;
                        user.Platform = "ZS";
                        var newuserxml = Auxiliary.Serializer(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), user);
                        //同步用户数据
                        _ous.UpdateUser("binPath", "ZSClassPath", newuserxml);
                    }

                }

                return Auxiliary.ToInt32(result);

                #endregion

            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("zhuzh", loginname + "," + password, ex);
                return 0;
            }
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
            try
            {

                #region SQL
                var sql = @"IF EXISTS ( SELECT  [user_id]
                                    FROM    [dbo].[LocalUserOthers]
                                    WHERE   [user_id] = @user_id) 
                            BEGIN
                                UPDATE  [dbo].[LocalUsers]
                                SET     [user_updatetime] = GETDATE()
                                WHERE   [user_id] = @user_id
                                UPDATE  [dbo].[LocalUserOthers]
                                SET     [other_companyname] = @other_companyname,
                                        [other_companyposition]=@other_companyposition
                                WHERE   [user_id] = @user_id
                                SELECT  1
                            END
                        ELSE 
                            BEGIN
                                SELECT  0  
                            END    ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", userid);
                param.AddParam("@other_companyname", companyname);
                param.AddParam("@other_companyposition", companyposition);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);

                #endregion

            }
            catch (Exception)
            {

                return 0;
            }
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
            try
            {
                #region SQL
                var sql = @"IF EXISTS ( SELECT  [user_id]
                                        FROM    [dbo].[LocalUserOthers]
                                        WHERE   [user_id] = @user_id ) 
                                BEGIN
                                    UPDATE  [dbo].[LocalUserOthers]
                                    SET     [other_papers] = @user_papers ,
                                            [other_papersname] = @user_papersname ,
                                            [other_paperstype] = @user_paperstype ,
                                            [other_papersstatus] = @user_papersstatus,
                                            [other_paperstime] = GETDATE()
                                    WHERE   [user_id] = @user_id
                                    SELECT  1
                                END
                            ELSE 
                                BEGIN
                                    SELECT  0  
                                END  ";
                #endregion

                #region 参数

                SqlParam param = new SqlParam();
                param.AddParam("@user_id", entity.UserID);
                param.AddParam("@user_papers", entity.Papers);
                param.AddParam("@user_papersname", entity.RealName);
                param.AddParam("@user_paperstype", entity.PapersType);
                param.AddParam("@user_papersstatus", entity.PapersStatus);

                #endregion

                #region 执行

                var result = KYJ_ZushouWDB.GetFirst(sql, param);

                return Auxiliary.ToInt32(result);

                #endregion

            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 判断当前用户ID 本地用户表是否存在
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否存在</returns>
        public bool IsExistLoginUser(int userId)
        {
            #region SQL
            var sql = @"SELECT  [user_id]
                        FROM    [dbo].[LocalUsers]
                        WHERE   [user_id] = @user_id
                                AND [user_isdel] = 0";
            #endregion

            #region 参数

            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);

            #endregion

            #region 执行

            var result = KYJ_ZushouWDB.GetFirst(sql, param);

            if (!result.IsNullOrEmpty())
                return true;
            else
                return false;

            #endregion
        }
        /// <summary>
        /// 接口测试
        /// </summary>
        /// <returns></returns>
        public void getopencity()
        {
            //UserWebService.OperaUserServiceSoapClient client = new UserWebService.OperaUserServiceSoapClient();
            //client.getcity
            BaseDataWebService.BaseDataWebServiceSoapClient client = new BaseDataWebService.BaseDataWebServiceSoapClient();
            KYJ.ZS.DAL.BaseDataWebService.Area area = client.GetCityById(253);
            //return area;
        }
        /// <summary>
        /// Author:baiyan
        /// Time:2014-5-15
        /// Desc:获得个人用户帐户信息
        /// </summary>
        /// <returns>个人帐户实体类</returns>
        public UserAccountEntity GetUserAccountInfo(int userId)
        {
            UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
            UserWebService.UserAccountInfo account = _ous.GetUserAccountInfo("binPath", "ZSClassPath", userId);
            //  _ous.ChangeAccount("binPath", "ZSClassPath", userId, "500");
            UserAccountEntity userAccount = new UserAccountEntity();
            userAccount.AId = account.AId;
            userAccount.UserId = account.UserId;
            userAccount.Price = account.Price;
            userAccount.BackPrice = account.BackPrice;
            userAccount.DrawMoneyTimes = account.DrawMoneyTimes;
            userAccount.UpdateTime = account.UpdateTime;
            userAccount.RechargeTotalPrice = account.RechargeTotalPrice;
            userAccount.InvesteTotalPrice = account.InvesteTotalPrice;
            userAccount.InvesteIncomeTotalPrice = account.InvesteIncomeTotalPrice;
            userAccount.WithdrawCashTotalPrice = account.WithdrawCashTotalPrice;
            return userAccount;
        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年6月5日11:02:38
        /// 描述：给本地用户账户添加金额
        /// </summary>
        /// <param name="money"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Boolean UserMoneyAdd(decimal money, int userId)
        {
            UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
            decimal m = _ous.GetUserAccountInfo("binPath", "ZSClassPath", userId).Price + money;
            return _ous.ChangeAccount("binPath", "ZSClassPath", userId, m.ToString());
        }
        /// <summary>
        /// 作者：maq
        /// 时间：2014年6月5日11:05:01
        /// 描述：减少用户账户中的金额
        /// </summary>
        /// <param name="money"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Boolean UserMoneyReduce(decimal money, int userId)
        {
            UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
            decimal m = _ous.GetUserAccountInfo("binPath", "ZSClassPath", userId).Price - money;
            if (m < 0)
            {
                return false;
            }
            return _ous.ChangeAccount("binPath", "ZSClassPath", userId, m.ToString());
        }

        #region 查询个人用户基本信息
        /// <summary>
        /// Author:baiyan
        /// Time:2014-4-17
        /// Desc:查询个人用户基本信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>基本信息实体</returns>
        public LocalUser GetLocalUser(int userId)
        {
            #region SQL
            var sql = @"SELECT [user_id]
                          ,[user_loginname]
                          ,[user_email]
                          ,[user_mobile]
                          ,[user_pwd]
                          ,[user_paypwd]
                          ,[user_realname]
                          ,[user_nickname]
                          ,[user_sex]
                          ,[user_papers]
                          ,[user_state]
                          ,[user_rank]
                          ,[user_integral]
                          ,[user_guid]
                          ,[user_createtime]
                          ,[user_updatetime]
                          ,[user_lastlogintime]
                          ,[user_isdel]
                      FROM [dbo].[LocalUsers] NOLOCK WHERE [user_id]=@user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            #endregion
            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion
            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                LocalUser user = new LocalUser();
                var item = dt.Rows[0];
                user.UserID = Auxiliary.ToInt32(item["user_id"]);
                user.LoginName = item["user_loginname"].ToString();
                user.Email = item["user_email"].ToString();
                user.Mobile = item["user_mobile"].ToString();
                user.Pwd = item["user_pwd"].ToString();
                user.PayPwd = item["user_paypwd"].ToString();
                user.RealName = item["user_realname"].ToString();
                user.NickName = item["user_nickname"].ToString();
                user.Sex = Auxiliary.ToInt32(item["user_sex"]);
                //user.Birthday = Convert.ToDateTime(item["other_birthday"]);
                user.IdCard = item["user_papers"].ToString();
                user.State = Auxiliary.ToInt32(item["user_state"]);
                user.Rank = Auxiliary.ToInt32(item["user_rank"]);
                user.InteGral = Auxiliary.ToInt32(item["user_integral"]);
                user.Guid = item["user_guid"].ToString();
                user.CreateTime = Convert.ToDateTime(item["user_createtime"]);
                user.UpdateTime = Convert.ToDateTime(item["user_updatetime"]);
                user.LastLoginTime = Convert.ToDateTime(item["user_lastlogintime"]);
                user.IsDel = Auxiliary.ToBoolen(item["user_isdel"]);
                return user;
            }
            return null;
            #endregion
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
            #region SQL
            var sql = @"SELECT  [U].[user_id] ,
                                [U].[user_loginname] ,
                                [U].[user_email] ,
                                [U].[user_mobile] ,
                                [U].[user_pwd] ,
                                [U].[user_paypwd] ,
                                [U].[user_realname] ,
                                [U].[user_nickname] ,
                                [U].[user_sex] ,
                                [U].[user_birthdayyear] ,
                                [U].[user_birthdaymonth] ,
                                [U].[user_birthdayday] ,
                                [U].[user_constellation],
                                [U].[user_state] ,
                                [U].[user_rank] ,
                                [U].[user_integral] ,
                                [U].[user_guid] ,
                                [U].[user_createtime] ,
                                [U].[user_updatetime] ,
                                [U].[user_lastlogintime] ,
                                [UO].[live_provinceid] ,
                                [UO].[live_provincename] ,
                                [UO].[live_cityid] ,
                                [UO].[live_cityname] ,
                                [UO].[live_districtid] ,
                                [UO].[live_districtname] ,
                                [UO].[home_provinceid] ,
                                [UO].[home_provincename] ,
                                [UO].[home_cityid] ,
                                [UO].[home_cityname] ,
                                [UO].[home_districtid] ,
                                [UO].[home_districtname] ,
                                [UO].[other_college] ,
                                [UO].[other_highschool] ,
                                [UO].[other_middleschool] ,
                                [UO].[other_primaryschool],
                                [UO].[other_companyname],
                                [UO].[other_companyposition],
                                [UO].[other_papers],
                                [UO].[other_papersname],
                                [UO].[other_paperstype],
                                [UO].[other_papersstatus],
                                [UO].[other_papersremark]
                        FROM    [dbo].[LocalUsers] AS [U] ( NOLOCK )
                                INNER JOIN [dbo].[LocalUserOthers] AS [UO] ( NOLOCK ) ON [UO].[user_id] = [U].[user_id]
                        WHERE   [U].[user_isdel] = 0
                                AND [U].[user_id] = @user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userId);
            #endregion
            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion
            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                LocalUserEntity userEntity = new LocalUserEntity();
                var item = dt.Rows[0];
                userEntity.UserId = Auxiliary.ToInt32(item["user_id"]);
                userEntity.LoginName = item["user_loginname"] == DBNull.Value ? string.Empty : item["user_loginname"].ToString();
                userEntity.Email = item["user_email"] == DBNull.Value ? string.Empty : item["user_email"].ToString();
                userEntity.Mobile = item["user_mobile"] == DBNull.Value ? string.Empty : item["user_mobile"].ToString();
                userEntity.RealName = item["user_realname"] == DBNull.Value ? string.Empty : item["user_realname"].ToString();
                userEntity.NickName = item["user_nickname"] == DBNull.Value ? string.Empty : item["user_nickname"].ToString();
                userEntity.Sex = item["user_sex"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["user_sex"]);

                userEntity.BirthdayYear = item["user_birthdayyear"] == DBNull.Value ? string.Empty : item["user_birthdayyear"].ToString();
                userEntity.BirthdayMonth = item["user_birthdaymonth"] == DBNull.Value ? string.Empty : item["user_birthdaymonth"].ToString();
                userEntity.BirthdayDay = item["user_birthdayday"] == DBNull.Value ? string.Empty : item["user_birthdayday"].ToString();
                userEntity.Constellation = item["user_constellation"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["user_constellation"]);
                userEntity.State = item["user_state"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["user_state"]);
                userEntity.Rank = item["user_rank"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["user_rank"]);
                userEntity.Integral = item["user_integral"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["user_integral"]);
                userEntity.Guid = item["user_guid"] == DBNull.Value ? string.Empty : item["user_guid"].ToString();
                userEntity.CreateTime = item["user_createtime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["user_createtime"]);
                userEntity.UpdateTime = item["user_updatetime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["user_updatetime"]);
                userEntity.LastLoginTime = item["user_lastlogintime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(item["user_lastlogintime"]);

                userEntity.LiveProvinceId = item["live_provinceid"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["live_provinceid"]);
                userEntity.LiveProvinceName = item["live_provincename"] == DBNull.Value ? string.Empty : item["live_provincename"].ToString();
                userEntity.LiveCityId = item["live_cityid"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["live_cityid"]);
                userEntity.LiveCityName = item["live_cityname"] == DBNull.Value ? string.Empty : item["live_cityname"].ToString();
                userEntity.LiveDistrictId = item["live_districtid"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["live_districtid"]);
                userEntity.LiveDistrictName = item["live_districtname"] == DBNull.Value ? string.Empty : item["live_districtname"].ToString();

                userEntity.HomeProvinceId = item["home_provinceid"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["home_provinceid"]);
                userEntity.HomeProvinceName = item["home_provincename"] == DBNull.Value ? string.Empty : item["home_provincename"].ToString();
                userEntity.HomeCityId = item["home_cityid"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["home_cityid"]);
                userEntity.HomeCityName = item["home_cityname"] == DBNull.Value ? string.Empty : item["home_cityname"].ToString();
                userEntity.HomeDistrictId = item["home_districtid"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["home_districtid"]);
                userEntity.HomeDistrictName = item["home_districtname"] == DBNull.Value ? string.Empty : item["home_districtname"].ToString();

                userEntity.College = item["other_college"].ToString();
                userEntity.HighSchool = item["other_highschool"] == DBNull.Value ? string.Empty : item["other_highschool"].ToString();
                userEntity.MiddleSchool = item["other_middleschool"] == DBNull.Value ? string.Empty : item["other_middleschool"].ToString();
                userEntity.PrimarySchool = item["other_primaryschool"] == DBNull.Value ? string.Empty : item["other_primaryschool"].ToString();

                userEntity.CompanyName = item["other_companyname"] == DBNull.Value ? string.Empty : item["other_companyname"].ToString();
                userEntity.CompanyPosition = item["other_companyposition"] == DBNull.Value ? string.Empty : item["other_companyposition"].ToString();
                userEntity.Papers = item["other_papers"] == DBNull.Value ? string.Empty : item["other_papers"].ToString();
                userEntity.PapersName = item["other_papersname"] == DBNull.Value ? string.Empty : item["other_papersname"].ToString();
                userEntity.PapersType = item["other_paperstype"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["other_paperstype"]);
                userEntity.PapersStatus = item["other_papersstatus"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["other_papersstatus"]);
                userEntity.PapersRemark = item["other_papersremark"] == DBNull.Value ? string.Empty : item["other_papersremark"].ToString();



                return userEntity;
            }
            return null;
            #endregion
        }

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-05-14
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        public AuthenticationEntity GetAuthenticationByUserID(int userid)
        {
            #region SQL
            var sql = @"SELECT  [user_id],
                                [other_papersname],
                                [other_papers],
                                [other_paperstype],
                                [other_papersstatus],
                                [other_papersremark]
                        FROM    [dbo].[LocalUserOthers] ( NOLOCK )
                        WHERE   [user_id] = @user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_id", userid);
            #endregion
            #region 执行
            DataTable dt = KYJ_ZushouRDB.GetTable(sql, param);
            #endregion
            #region 返回值
            if (Auxiliary.CheckDt(dt))
            {
                var enriry = new AuthenticationEntity();
                var item = dt.Rows[0];
                enriry.UserID = item["user_id"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["user_id"]);
                enriry.RealName = item["other_papersname"] == DBNull.Value ? string.Empty : item["other_papersname"].ToString();
                enriry.Papers = item["other_papers"] == DBNull.Value ? string.Empty : item["other_papers"].ToString();
                enriry.PapersType = item["other_paperstype"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["other_paperstype"]);
                enriry.PapersStatus = item["other_papersstatus"] == DBNull.Value ? 0 : Auxiliary.ToInt32(item["other_papersstatus"]);
                enriry.PapersRemark = item["other_papersremark"] == DBNull.Value ? string.Empty : item["other_papersremark"].ToString();

                return enriry;
            }
            return null;
            #endregion
        }
        #endregion

        #region 修改个人用户基本信息
        /// <summary>
        /// uthor:baiyan
        /// Time:2014-4-17
        /// Desc:修改个人用户基本信息
        /// </summary>
        /// <param name="userInfo">用户基本信息实体</param>
        /// <returns></returns>
        public int UpdateLocalUser(LocalUser userInfo)
        {
            #region SQL
            var sql = @"UPDATE [dbo].[LocalUsers]
                       SET [user_loginname] = @user_loginname
                          ,[user_email] = @user_email
                          ,[user_mobile] = @user_mobile
                          ,[user_pwd] = @user_pwd
                          ,[user_paypwd] = @user_paypwd
                          ,[user_realname] = @user_realname
                          ,[user_nickname] = @user_nickname
                          ,[user_sex] = @user_sex
                          ,[other_birthday] = @other_birthday
                          ,[user_papers] = @user_papers
                          ,[user_state] = @user_state
                          ,[user_rank] = @user_rank
                          ,[user_integral] = @user_integral
                          ,[user_guid] = @user_guid
                          ,[user_updatetime] = @user_updatetime
                     WHERE [user_id]=@user_id";
            #endregion
            #region 参数
            SqlParam param = new SqlParam();
            param.AddParam("@user_loginname", userInfo.LoginName);
            param.AddParam("@user_email", userInfo.Email ?? string.Empty);
            param.AddParam("@user_mobile", userInfo.Mobile);
            param.AddParam("@user_pwd", userInfo.Pwd);
            param.AddParam("@user_paypwd", userInfo.PayPwd);
            param.AddParam("@user_realname", userInfo.RealName ?? string.Empty);
            param.AddParam("@user_nickname", userInfo.NickName ?? string.Empty);
            param.AddParam("@user_sex", userInfo.Sex);
            param.AddParam("@other_birthday", userInfo.Birthday);
            param.AddParam("@user_papers", userInfo.IdCard);
            param.AddParam("@user_state", userInfo.State);
            param.AddParam("@user_rank", userInfo.Rank);
            param.AddParam("@user_integral", userInfo.InteGral);
            param.AddParam("@user_guid", userInfo.Guid);
            param.AddParam("@user_updatetime", userInfo.UpdateTime);
            param.AddParam("@user_id", userInfo.UserID);
            #endregion
            #region 执行返回值
            return KYJ_ZushouWDB.SqlExecute(sql, param);
            #endregion
        }
        #endregion

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
            try
            {
                // 表名
                string tableName = "LocalUsers(NOLOCK) t1 left join LocalUserOthers(NOLOCK) t2 on t1.user_id=t2.user_id";
                // 查询条件
                string where = " t1.user_isdel = 'false' and t2.other_papersstatus<>0";
                // 账号过滤
                if (!string.IsNullOrEmpty(loginName))
                    where += " and t1.user_loginname like '%" + loginName.Trim() + "%'";
                // 身份认证过滤
                if (status > 0)
                    where += " and t2.other_papersstatus =" + status;
                // 姓名过滤
                if (!string.IsNullOrEmpty(realName))
                    where += " and t1.user_realname like '%" + realName.Trim() + "%'";
                // 状态过滤
                if (state >= 0)
                    where += " and t1.user_state =" + state;

                // 排序
                string orderBy = " t1.user_createtime desc";
                // 列名
                string columnList = "t1.user_id as Id,t1.user_loginname as LoginName,user_realname as RealName,user_sex as Sex,user_createtime as CreateTime,user_lastlogintime as LastLoginTime,other_papersstatus as PapersStatus,user_state as State,t1.user_guid as Guid,t2.other_paperstime as PapersTime";

                DataTable dt = KYJ_ZushouRDB.GetPages(index, where, orderBy, columnList, tableName, pageSize, true, out totalRecord, out totalPage);

                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<LocalUserManageEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalRecord = 0;
                totalPage = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// 认证审核
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="papersRemark">认证描述</param>
        /// <param name="isValidate">是否通过认证</param>
        /// <returns></returns>
        public int Audited(int id, string papersRemark, bool isValidate)
        {
            try
            {
                var sql = "update LocalUserOthers set other_papersstatus=@other_papersstatus"
                    + (isValidate ? "" : ",other_papersremark=@other_papersremark")
                    + " where user_id=@user_id";
                var param = new SqlParam();
                param.AddParam("@user_id", id);
                param.AddParam("@other_papersstatus", isValidate ? 4 : 3);
                if (!isValidate)
                    param.AddParam("@other_papersremark", papersRemark);
                return KYJ_ZushouWDB.SqlExecute(sql, param);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id + "," + papersRemark + "," + isValidate, ex);
                return 0;
            }
        }

        /// <summary>
        /// 冻结操作
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="isFreeze">是否冻结</param>
        /// <returns></returns>
        public int Freeze(int id, bool isFreeze)
        {
            try
            {
                var sql = "update LocalUsers set user_state=@user_state where user_id=@user_id";
                var param = new SqlParam();
                param.AddParam("@user_id", id);
                param.AddParam("@user_state", isFreeze ? 1 : 0);
                int result = KYJ_ZushouWDB.SqlExecute(sql, param);

                //修改成功，同步公共平台数据
                if (result > 0)
                {
                    UserWebService.OperaUserServiceSoapClient _ous = new UserWebService.OperaUserServiceSoapClient();
                    //获取公共平台用户数据
                    var userXml = _ous.GetUserById("binPath", "ZSClassPath", id, "ZS");
                    if (!userXml.IsNullOrEmpty())
                    {
                        //反序列化xml字符串
                        var user = Auxiliary.Deserialize(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), userXml) as KYJ.ZS.DAL.UserWebService.UserInfo;
                        user.AllState = (isFreeze ? (byte)1 : (byte)0);
                        user.Platform = "ZS";
                        var newuserxml = Auxiliary.Serializer(typeof(KYJ.ZS.DAL.UserWebService.UserInfo), user);
                        //同步用户数据
                        _ous.UpdateUser("binPath", "ZSClassPath", newuserxml);
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", id + "," + isFreeze, ex);
                return 0;
            }
        }

        #endregion

    }
}
