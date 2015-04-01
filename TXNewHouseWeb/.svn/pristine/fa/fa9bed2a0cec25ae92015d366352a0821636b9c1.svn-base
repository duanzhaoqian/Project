using System;
using System.Collections.Generic;
using System.Linq;
using TXModel.Commons;
using TXOrm;

namespace TXDal.NHouseActivies.Yaohao
{
    /// <summary>
    /// 摇号
    /// </summary>
    public partial class YaohaoDal
    {
        #region 通用方法

        /// <summary>
        /// 添加摇号
        /// </summary>
        /// <param name="entity">摇号实体</param>
        /// <returns>返回：新添加摇号编号</returns>
        public int Add(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 根据摇号编号更新摇号信息
        /// </summary>
        /// <param name="entity">摇号实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 逻辑删除摇号信息
        /// </summary>
        /// <param name="id">摇号编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return 0;
        }

        /// <summary>
        /// 根据编号获取摇号实体
        /// </summary>
        /// <param name="id">摇号编号</param>
        /// <returns>返回：摇号实体</returns>
        public Activity GetEntity_ById(int id)
        {
            try
            {
                using (var db = new kyj_NewHouseDBEntities())
                {
                    return db.Activities.FirstOrDefault(it => it.Id == id && it.Type == 1 && it.IsDel == false);
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("Id:{0}", new object[] { id }), ex);
                return null;
            }
        }

        /// <summary>
        /// 根据编号集合获取实体列表
        /// </summary>
        /// <param name="ids">编号集合字符串(逗号分隔)</param>
        /// <returns>返回：列表</returns>
        public List<object> GetList_ByIds(string ids)
        {
            return null;
        }

        /// <summary>
        /// 根据搜素条件获取分页列表
        /// </summary>
        /// <param name="search">搜索条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页记录数</param>
        /// <returns></returns>
        public Paging<object> GetPageList_BySearch(object search, int pageIndex, int pageSize)
        {
            return null;
        }

        #endregion
    }
}