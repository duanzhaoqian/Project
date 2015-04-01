using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXOrm;

namespace TXDal.Dev
{
    /// <summary>
    /// 开发商(网站管理平台)
    /// </summary>
    public partial class DevelopersDal
    {
        #region 根据搜索条件获取开发商表信息

        /// <summary>
        /// 根据搜索条件获取开发商表信息
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public List<PVM_NH_Developer> GetPageList_BySearch_Admin(PVS_NH_Developer search, int pageIndex, int pageSize)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = from deve in kyj.Developers
                                join identity in kyj.Developer_Identity on deve.Id equals identity.DeveloperId
                                where deve.IsDel == false && identity.IsDel == false
                                select new
                                {
                                    deve,
                                    identity
                                };

                    #region 筛选

                    //审核状态 Developers
                    if (search.State >= 0)
                    {
                        query = query.Where(it => it.deve.State == search.State);
                    }
                    //锁定状态 Developers
                    if (search.LockState >= 0)
                    {
                        query = query.Where(it => it.deve.LockState == search.LockState);
                    }
                    //省份 Developer_Identity
                    if (search.ProvinceID > 0)
                    {
                        query = query.Where(it => it.identity.ProvinceId == search.ProvinceID);
                    }
                    //城市 Developer_Identity
                    if (search.CityId > 0)
                    {
                        query = query.Where(it => it.identity.CityId == search.CityId);
                    }
                    //时间 Developer_Identity
                    if (!string.IsNullOrWhiteSpace(search.BeginTime) && string.IsNullOrWhiteSpace(search.EndTime))
                    {
                        DateTime begin = Convert.ToDateTime(search.BeginTime);
                        query = query.Where(it => it.identity.CreateTime >= begin);
                    }
                    else if (string.IsNullOrWhiteSpace(search.BeginTime) && !string.IsNullOrWhiteSpace(search.EndTime))
                    {
                        DateTime end = Convert.ToDateTime(search.EndTime).AddDays(1);
                        query = query.Where(it => it.identity.CreateTime <= end);
                    }
                    else if (!string.IsNullOrWhiteSpace(search.BeginTime) && !string.IsNullOrWhiteSpace(search.EndTime))
                    {
                        DateTime begin = Convert.ToDateTime(search.BeginTime);
                        DateTime end = Convert.ToDateTime(search.EndTime).AddDays(1);
                        query = query.Where(it => it.identity.CreateTime >= begin && it.identity.CreateTime <= end);
                    }
                    //公司名称 Developer_Identity
                    if (!string.IsNullOrWhiteSpace(search.Name))
                    {
                        //if (search.Name.Length > 20)
                        //    search.Name = search.Name.Substring(0, 20);
                        query = query.Where(it => it.identity.CompanyName.Contains(search.Name));
                    }
                    //登录名称 Developers
                    if (!string.IsNullOrWhiteSpace(search.LoginName))
                    {
                        //if (search.LoginName.Length > 8)
                        //    search.LoginName = search.LoginName.Substring(0, 8);
                        query = query.Where(it => it.deve.LoginName.Contains(search.LoginName));
                    }

                    #endregion

                    #region 返回数据

                    return
                        query.OrderByDescending(it => it.identity.CreateTime)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList()
                            .ConvertAll(it => new PVM_NH_Developer
                            {
                                Id = it.deve.Id,
                                State = it.deve.State,
                                LockState = it.deve.LockState,
                                LoginName = it.deve.LoginName,
                                InnerCode = it.deve.InnerCode,
                                LoginTime = it.deve.LoginTime,
                                OldLoginTime = it.deve.OldLoginTime,

                                Type = it.identity.Type,
                                ProvinceId = it.identity.ProvinceId,
                                CityId = it.identity.CityId,
                                ProvinceName = it.identity.ProvinceName,
                                CityName = it.identity.CityName,
                                Address = it.identity.CompanyAddress,
                                Name = it.identity.CompanyName,
                                Mobile = it.identity.UserMobile,
                                Email = it.identity.UserEmail,
                                RealName = it.identity.UserName,
                                CreateTime = it.identity.CreateTime,
                                //UpdateTime = it.identity.UpdateTime,
                                IsDel = it.identity.IsDel,
                                IsRecommend = it.deve.IsRecommend
                            });

                    #endregion
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", search, e);
                throw;
            }
        }

        #endregion

        #region 根据搜索条件获取开发商表信息记录数

        /// <summary>
        /// 根据搜索条件获取开发商表信息记录数
        /// author:huhangfei
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <returns></returns>
        public int GetPageCount_BySearch_Admin(PVS_NH_Developer search)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var query = from deve in kyj.Developers
                                join identity in kyj.Developer_Identity on deve.Id equals identity.DeveloperId
                                where deve.IsDel == false && identity.IsDel == false
                                select new
                                {
                                    deve,
                                    identity
                                };

                    #region 筛选

                    //审核状态 Developers
                    if (search.State >= 0)
                    {
                        query = query.Where(it => it.deve.State == search.State);
                    }
                    //锁定状态 Developers
                    if (search.LockState >= 0)
                    {
                        query = query.Where(it => it.deve.LockState == search.LockState);
                    }
                    //省份 Developer_Identity
                    if (search.ProvinceID > 0)
                    {
                        query = query.Where(it => it.identity.ProvinceId == search.ProvinceID);
                    }
                    //城市 Developer_Identity
                    if (search.CityId > 0)
                    {
                        query = query.Where(it => it.identity.CityId == search.CityId);
                    }
                    //时间 Developer_Identity
                    if (!string.IsNullOrWhiteSpace(search.BeginTime) && string.IsNullOrWhiteSpace(search.EndTime))
                    {
                        DateTime begin = Convert.ToDateTime(search.BeginTime);
                        query = query.Where(it => it.identity.CreateTime >= begin);
                    }
                    else if (string.IsNullOrWhiteSpace(search.BeginTime) && !string.IsNullOrWhiteSpace(search.EndTime))
                    {
                        DateTime end = Convert.ToDateTime(search.EndTime).AddDays(1);
                        query = query.Where(it => it.identity.CreateTime <= end);
                    }
                    else if (!string.IsNullOrWhiteSpace(search.BeginTime) && !string.IsNullOrWhiteSpace(search.EndTime))
                    {
                        DateTime begin = Convert.ToDateTime(search.BeginTime);
                        DateTime end = Convert.ToDateTime(search.EndTime).AddDays(1);
                        query = query.Where(it => it.identity.CreateTime >= begin && it.identity.CreateTime <= end);
                    }
                    //公司名称 Developer_Identity
                    if (!string.IsNullOrWhiteSpace(search.Name))
                    {
                        if (search.Name.Length > 20)
                            search.Name = search.Name.Substring(0, 20);
                        query = query.Where(it => it.identity.CompanyName.Contains(search.Name));
                    }
                    //登录名称 Developers
                    if (!string.IsNullOrWhiteSpace(search.LoginName))
                    {
                        if (search.LoginName.Length > 8)
                            search.LoginName = search.LoginName.Substring(0, 8);
                        query = query.Where(it => it.deve.LoginName.Contains(search.LoginName));
                    }

                    #endregion

                    #region 返回数据

                    return query.Count();

                    #endregion
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", search, e);
                throw;
            }
        }

        #endregion

        #region 根据开发商id获取实体

        /// <summary>
        /// 根据开发商id获取实体
        /// author:huhangfei
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PVM_NH_Developer GetDevelopersById(int id)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    var quer = from deve in kyj.Developers
                               join identity in kyj.Developer_Identity on deve.Id equals identity.DeveloperId
                               where deve.IsDel == false && identity.IsDel == false && deve.Id == id
                               select new PVM_NH_Developer
                               {
                                   Id = deve.Id,
                                   Pwd = deve.Pwd,
                                   InnerCode = deve.InnerCode,
                                   CreateTime = deve.CreateTime,
                                   State = deve.State,
                                   Telephone = deve.Telephone,
                                   SpareTelephone = deve.SpareTelephone,
                                   LoginName = deve.LoginName,
                                   Type = identity.Type,

                                   ProvinceId = identity.ProvinceId,
                                   CityId = identity.CityId,
                                   LockState = deve.LockState,
                                   ProvinceName = identity.ProvinceName,
                                   CityName = identity.CityName,
                                   Address = identity.CompanyAddress,
                                   Name = identity.CompanyName,
                                   Mobile = identity.UserMobile,
                                   Email = identity.UserEmail,
                                   RealName = identity.UserName,
                                   //UpdateTime = deve.UpdateTime,
                                   IsDel = identity.IsDel,
                                   Refuse = identity.Refuse,

                                   NHDeveloper = new PVM_NH_Developer.N_Developer
                                   {
                                       Id = deve.Id,
                                       ProvinceId = deve.ProvinceId,
                                       ProvinceName = deve.ProvinceName,
                                       CityId = deve.CityId,
                                       CityName = deve.CityName,
                                       Mobile = deve.Mobile,
                                       Email = deve.Email,
                                       Telephone = deve.Telephone,
                                       SpareTelephone = deve.SpareTelephone,
                                       LoginName = deve.LoginName,
                                       RealName = deve.RealName,
                                       State = deve.State,
                                       LockState = deve.LockState,
                                       InnerCode = deve.InnerCode,
                                       LoginTime = deve.LoginTime,
                                       OldLoginTime = deve.OldLoginTime,
                                       CreateTime = deve.OldLoginTime,
                                       IsDel = deve.IsDel
                                   },

                                   NHDeveloperIdentity = new PVM_NH_Developer.N_DeveloperIdentity
                                   {
                                       Id = identity.Id,
                                       DeveloperId = identity.DeveloperId,
                                       UserName = identity.UserName,
                                       UserMobile = identity.UserMobile,
                                       UserEmail = identity.UserEmail,
                                       Type = identity.Type,
                                       ProvinceId = identity.ProvinceId,
                                       ProvinceName = identity.ProvinceName,
                                       CityId = identity.CityId,
                                       CityName = identity.CityName,
                                       DId = identity.DId,
                                       DName = identity.DName,
                                       CompanyName = identity.CompanyName,
                                       CompanyAddress = identity.CompanyAddress,
                                       Refuse = identity.Refuse,
                                       CreateTime = identity.CreateTime,
                                       IsDel = identity.IsDel
                                   }
                               };
                    return quer.FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", id, e);
                throw;
            }
        }

        #endregion

        #region 根据开发商id修改开发商状态

        /// <summary>
        /// 根据开发商id修改开发商状态
        /// author:huhangfei
        /// </summary>
        /// <param name="id">开发商id</param>
        /// <param name="state">0未认证 1已通过 2未通过</param>
        /// <param name="refuse">拒绝原因</param>
        /// <param name="adminId"></param>
        /// <param name="adminName"></param>
        /// <returns></returns>
        public ESqlResult UpdateDevelopersStateById(int id, int state, string refuse, int adminId, string adminName)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    string sql = string.Format(@"DECLARE @t_id INT ,
                                                @t_state INT
                                            SELECT  @t_id = Id, @t_state = State
                                            FROM    Developer (NOLOCK)
                                            WHERE   Id = {0}

                                            IF ( @t_id IS NULL ) 
                                                BEGIN
                                                    SELECT  '0' AS Code, '开发商不存在' AS Msg
                                                    RETURN
                                                END
    
                                            IF ( @t_state <> 3 ) 
                                                BEGIN
                                                    SELECT  '0' AS Code, '状态已改变' AS Msg
                                                    RETURN
                                                END
                                            
                                            BEGIN TRY
                                            BEGIN TRAN
                                            --------------更新Developer state------------
                                            UPDATE  Developer
                                            SET     State = {1}
                                                    --UpdateTime=GETDATE()
                                            WHERE   Id = @t_id
                                            --------------更新Developer_Identity Refuse----
                                            UPDATE  Developer_Identity
                                            SET     Refuse='{2}'
                                                    --UpdateTime=GETDATE()
                                            WHERE   DeveloperId = @t_id
                                            COMMIT
                                            END TRY
                                            BEGIN CATCH
                                                ROLLBACK TRAN
                                                SELECT  '0' AS Code, '操作失败' AS Msg
                                            END CATCH
                                            IF ( @@ERROR > 0 ) 
                                                BEGIN
                                                    SELECT  '0' AS Code, '操作失败' AS Msg
                                                    RETURN
                                                END
                                            SELECT  '1' AS Code, '操作成功' AS Msg
                                            ", id, state, refuse);
                    var result = kyj.ExecuteStoreQuery<ESqlResult>(sql).FirstOrDefault();

                    return result;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", string.Format("{0}-{1}-{2}-{3}-{4}", id, state, refuse, adminId, adminName), e);
                throw;
            }
        }

        #endregion

        #region 根据关键字搜索开放商列表

        /// <summary>
        /// 根据关键字搜索开放商列表
        /// updator: liyuzhao 2014-02-17
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public List<PVM_NH_Developer> GetDevelopersByKeyWord(string key)
        {
            try
            {
                using (var kyj = new kyj_NewHouseDBEntities())
                {
                    //var query = kyj.Developer_Identity.Where(it => it.IsDel == false && it.CompanyName.Contains(key));
                    //return query.ToList().ConvertAll(it => new PVM_NH_Developer
                    //{
                    //    Id = it.DeveloperId,
                    //    Name=it.CompanyName
                    //});

                    string sql = @"
SELECT  DISTINCT
        Developer AS Name
FROM    Premises (NOLOCK)
WHERE   1 = 1
        AND ( Developer LIKE '%{0}%' )
UNION
SELECT  DISTINCT
        Agent AS Name
FROM    Premises (NOLOCK)
WHERE   1 = 1
        AND ( Agent LIKE '%{0}%' )";

                    sql = string.Format(sql, key);

                    var list = kyj.ExecuteStoreQuery<PVM_NH_Developer>(sql).ToList();

                    return list;
                }
            }
            catch (Exception e)
            {
                //记录日志
                Log4netService.RecordLog.RecordException("胡航飞", key, e);
                throw;
            }
        }

        #endregion
    }
}