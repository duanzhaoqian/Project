using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TXModel.AdminPVM;
using TXModel.Commons;
using TXModel.Geography;
using TXOrm;

namespace TXDal.HouseData
{
    /// <summary>
    /// 预售许可证(网站管理平台)
    /// </summary>
    public partial class PermitPresaleDal
    {
        /// <summary>
        /// 根据楼盘编号获取预售许可证列表
        /// author: liyuzhao
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<Z_GeographyItem> GetPermitPresaleByPremisesId(int premisesId)
        {
            try
            {
                using (var webEntity = new kyj_NewHouseDBEntities())
                {
                    return webEntity.PermitPresales.Where(it => it.PremisesId == premisesId && it.IsDel == false)
                        .OrderBy(it => it.CreateTime)
                        .Select(it => new Z_GeographyItem {GeographyCode = it.Id, GeographyName = it.Name}).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return null;
            }
        }

        /// <summary>
        /// 根据楼盘编号获取预售许可证列表(发布楼盘、编辑楼盘)
        /// author: liyuzhao
        /// </summary>
        /// <param name="premisesId">楼盘编号</param>
        /// <returns></returns>
        public List<PVS_NH_Premises_SalePermit> GetPermitPresalesByPremisesId(int premisesId)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.PermitPresales.Where(it => it.PremisesId == premisesId && it.IsDel == false)
                        .OrderBy(it => it.CreateTime)
                        .Select(it => new PVS_NH_Premises_SalePermit {Id = it.Id, Name = it.Name, IsDel = 0}).ToList();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return null;
            }
        }

        /// <summary>
        /// 添加新预售许可证
        /// </summary>
        /// <param name="permitPresale"></param>
        /// <returns></returns>
        public int AddNewPermitPresale(PVE_PermitPresale permitPresale)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    string sql = @"
IF EXISTS ( SELECT  1
            FROM    PermitPresale (NOLOCK)
            WHERE   PremisesId = @PremisesId
                    AND Name = @Name ) 
    BEGIN
        SELECT  '0' AS Code, '该预售许可证已经存在'
        RETURN
    END
ELSE 
    BEGIN
        INSERT  INTO PermitPresale ( PremisesId, Name, CreateTime, UpdateTime, IsDel )
        VALUES  ( @PremisesId, @Name, GETDATE(), GETDATE(), 0 )
        SELECT  '1' AS Code, '操作成功'
    END";
                    var sqlparms = new object[]
                    {
                        new SqlParameter("PremisesId", permitPresale.PremisesId),
                        new SqlParameter("Name", permitPresale.Name)
                    };

                    var result = db.ExecuteStoreQuery<ESqlResult>(sql, sqlparms).ToList();
                    if (0 < result.Count)
                    {
                        return Convert.ToInt32(result[0].Code);
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", null, ex);
                return 0;
            }
        }
    }
}