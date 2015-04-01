using System.Data.SqlClient;
using System.Linq;
using TXModel.AdminPVM;
using TXOrm;

namespace TXDal.Dev
{
    /// <summary>
    /// 开发商账号(网站管理平台)
    /// </summary>
    public partial class Developer_AccountDal
    {

        #region 开发商账号状态锁定(解锁)操作
        /// <summary>
        /// 开发商账号状态锁定(解锁)操作
        /// Author:wangzhikun
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态</param>
        public void Locked(int id, int state)
        {
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var user = houseDB.Developers.FirstOrDefault(it => it.IsDel == false && it.Id == id);
                    if (user != null)
                    {
                        user.LockState = state;
                        //user.UpdateTime = DateTime.Now;
                        houseDB.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0},{1})", id, state), e);
                throw;
            }
        }
        #endregion
        #region 开发商帐号推荐（取消推荐）操作
        /// <summary>
        /// 开发商帐号推荐（取消推荐）操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restate"></param>
        public void Recommend(int id, int restate)
        {
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    var user = houseDB.Developers.FirstOrDefault(it => it.IsDel == false && it.Id == id);
                    if (user != null)
                    {
                        user.IsRecommend = restate == 1;
                        //user.UpdateTime = DateTime.Now;
                        houseDB.SaveChanges();
                    }
                }
            }
            catch (SqlException e)
            {
                Log4netService.RecordLog.RecordException("马欢", string.Format("({0},{1})", id, restate), e);
                throw;
            }
        }

        #endregion
        #region 修改开发商资料
        /// <summary>
        /// 修改开发商资料
        /// </summary>
        /// <param name="operDev">实体</param>
        /// <param name="tabType">tab选项卡(1基本资料 2头像 3密码  4身份认证)</param>  
        public bool Update(PVM_NH_Developer operDev, int tabType)
        {
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    bool flag = false;
                    var deve = houseDB.Developers.FirstOrDefault(it => it.Id == operDev.Id && it.IsDel == false);
                    if (deve != null)
                    {
                        switch (tabType)
                        {
                            case 1:
                                deve.ProvinceId = operDev.ProvinceId;
                                deve.ProvinceName = operDev.ProvinceName;
                                deve.CityId = operDev.CityId;
                                deve.CityName = operDev.CityName;
                                deve.Email = operDev.Email ?? string.Empty;
                                deve.Mobile = operDev.Mobile ?? string.Empty;
                                deve.Telephone = operDev.Telephone ?? string.Empty;
                                deve.SpareTelephone = operDev.SpareTelephone ?? string.Empty;
                                break;
                            case 2:
                                deve.InnerCode = operDev.InnerCode ?? string.Empty;
                                break;
                            case 3:
                                deve.Pwd = operDev.Pwd;
                                break;
                        }

                        //deve.UpdateTime = DateTime.Now;
                        houseDB.SaveChanges();
                        flag = true;
                    }
                    return flag;
                }
            }
            catch (SqlException e)
            {
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0},{1})", operDev, tabType), e);
                return false;
                throw;
            }
        }

        #endregion

        #region 修改开发商身份认证信息
        /// <summary>
        /// 修改开发商身份认证信息
        /// </summary>
        /// <param name="operDev">实体</param>
        public bool UpdateIdentity(PVM_NH_Developer operDev)
        {
            try
            {
                using (var houseDB = new kyj_NewHouseDBEntities())
                {
                    bool flag = false;
                    var deve = houseDB.Developer_Identity.FirstOrDefault(it => it.DeveloperId == operDev.Id && it.IsDel == false);
                    if (deve != null)
                    {
                        deve.ProvinceId = operDev.ProvinceId;
                        deve.ProvinceName = operDev.ProvinceName;
                        deve.CityId = operDev.CityId;
                        deve.CityName = operDev.CityName;
                        deve.DId = operDev.DistrictId;
                        deve.DName = operDev.DistrictName;
                        deve.Type = operDev.Type;
                        deve.CompanyName = operDev.Name ?? string.Empty;
                        deve.CompanyAddress = operDev.Address ?? string.Empty;
                        deve.UserName = operDev.RealName ?? string.Empty;
                        deve.UserMobile = operDev.Mobile ?? string.Empty;
                        deve.UserEmail = operDev.Email ?? string.Empty;
                        houseDB.SaveChanges();
                        flag = true;
                    }
                    return flag;
                }
            }
            catch (SqlException e)
            {
                Log4netService.RecordLog.RecordException("王志坤", string.Format("({0})", operDev), e);
                return false;
                throw;
            }
        }
        #endregion

        #region 根据开发商id获取实体
        /// <summary>
        /// 根据开发商id获取实体
        /// author:wangzhikun
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.Developer GetDevelopersById(int id)
        {
            using (var newhouseDb = new kyj_NewHouseDBEntities())
            {
                var model = newhouseDb.Developers.FirstOrDefault(it => it.Id == id && it.IsDel == false);
                return model;
            }
        }
        #endregion


    }
}