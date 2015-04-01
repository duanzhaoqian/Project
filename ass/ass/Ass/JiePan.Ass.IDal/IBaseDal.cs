using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JiePan.Ass.IDal
{
    public interface IBaseDal<T> where T : class ,new()
    {
        
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long Insert(T model);

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpDate(T model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        int Delete(object id);
        /// <summary>
        /// 更具主键获取实体
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetModel(object id);

        /// <summary>
        /// 得到T的对象的list集合
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> where);
        /// <summary>
        /// 对象的List集合
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序规则(eg:id desc)</param>
        /// <param name="pageIndex">起始位置</param>
        /// <param name="pageSize">结束位置</param>
        /// <returns>返回Articel_Words的List集合类型的数据</returns>
        IQueryable<T> GetListByPage<Tkey>(Expression<Func<T, bool>> where,
            Expression<Func<T, Tkey>> orderby,
            int pageIndex,
            int pageSize);
    }
}
