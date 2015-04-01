﻿using System.Collections.Generic;
using System.Linq;
using TXOrm;
using TXModel.Commons;

namespace TXDal.Dev
{
    /// <summary>
    /// 开发商
    /// </summary>
    public partial class DevelopersDal
    {
        #region 通用方法

        /// <summary>
        /// 添加开发商
        /// </summary>
        /// <param name="entity">开发商实体</param>
        /// <returns>返回：新添加开发商编号</returns>
        public int Add(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 更新开发商编号更新开发商信息
        /// </summary>
        /// <param name="entity">开发商实体</param>
        /// <returns>返回：影响行数</returns>
        public int Update_ById(object entity)
        {
            return 0;
        }

        /// <summary>
        /// 逻辑删除开发商信息
        /// </summary>
        /// <param name="id">开发商编号</param>
        /// <returns>返回：影响行数</returns>
        public int Del_ById(int id)
        {
            return 0;
        }

        /// <summary>
        /// 根据编号获取开发商实体
        /// </summary>
        /// <param name="id">开发商编号</param>
        /// <returns>返回：开发商实体</returns>
        public object GetEntity_ById(int id)
        {
            using (var houseDB = new kyj_NewHouseDBEntities())
            {
                return houseDB.Developers.FirstOrDefault<Developer>(entity => entity.Id == id);
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