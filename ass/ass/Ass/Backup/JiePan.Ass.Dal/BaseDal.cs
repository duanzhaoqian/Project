using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using JiePan.Ass.IDal;
using JiePan.Ass.Model;

namespace JiePan.Ass.Dal
{
    public class BaseDal<TSouce> : IBaseDal<TSouce>
        where TSouce : class,new()
    {
        public JiePan_AssEntities _dbContext;
        public JiePan_AssEntities DbContext
        {
            get
            {
                _dbContext = CallContext.GetData("dbcontext") as JiePan_AssEntities;
                if (_dbContext == null)
                {
                    _dbContext = new JiePan_AssEntities();
                    CallContext.SetData("dbcontext", _dbContext);
                }
                return _dbContext;
            }
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(TSouce model)
        {
            DbContext.Set<TSouce>().Add(model);
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpDate(TSouce model)
        {
            DbContext.Entry(model).State = EntityState.Modified;
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(object id)
        {
            TSouce tSouce = DbContext.Set<TSouce>().Find(id);
            if (tSouce == null)
            {
                return 0;
            }
            else
            {
                DbContext.Entry(tSouce).State = EntityState.Deleted;
                return DbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 获取model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TSouce GetModel(object id)
        {
            return DbContext.Set<TSouce>().Find(id);
        }


        public IQueryable<TSouce> GetList(Expression<Func<TSouce, bool>> where)
        {
            return DbContext.Set<TSouce>().Where(where);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IQueryable<TSouce> GetListByPage<Tkey>(Expression<Func<TSouce, bool>> where,
            Expression<Func<TSouce, Tkey>> orderby,
            int pageIndex,
            int pageSize)
        {
            return DbContext.Set<TSouce>().
                Where(where).
                OrderBy(orderby).
                Skip((pageIndex - 1)).
                Take(pageSize);
        }

    }
}
