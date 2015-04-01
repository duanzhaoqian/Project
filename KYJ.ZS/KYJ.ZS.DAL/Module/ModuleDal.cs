using System;
using System.Collections.Generic;
using System.Data;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Module;

namespace KYJ.ZS.DAL.Module
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-06-04
    /// Desc：操作模块表
    /// </summary>
    public class ModuleDal
    {
        /// <summary>
        /// 获取模块下拉列表集合
        /// </summary>
        /// <returns></returns>
        public IList<ModuleSelectEntity> GetSelectModules()
        {
            try
            {
                var sql = "select module_id as ModuleCode,module_name as ModuleName from Modules(NOLOCK)";
                DataTable dt = KYJ_ZSPlatformRDB.GetTable(sql);
                if (!Auxiliary.CheckDt(dt))
                    return null;
                return DataHelper<ModuleSelectEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException("ningjd", ex.Message, ex);
                return null;
            }
        }
    }
}
