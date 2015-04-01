//  ┏┓　　　┏┓
// ┏┛┻━━━┛┻┓
// ┃　　　　　　　┃ 　
// ┃　　　━　　　┃
// ┃　┳┛　┗┳　┃
// ┃　　　　　　　┃
// ┃　　　┻　　　┃
// ┃　　　　　　　┃
// ┗━┓　　　┏━┛
//     ┃　　　┃   神兽保佑　　　　　　　　　
//     ┃　　　┃   代码无BUG！
//     ┃　　　┗━━━┓
//     ┃　　　　　　　┣┓
//     ┃　　　　　　　┏┛
//     ┗┓┓┏━┳┓┏┛
//       ┃┫┫　┃┫┫
//       ┗┻┛　┗┻┛
// 
// Author:		maq
// Description:	
// CreateDate: 	2014-07-11 13:54
// ModifyDate:	2014-07-11 13:54

using System.Linq;
using System.Linq.Expressions;
using JiePan.Ass.IBll;
using JiePan.Ass.IDal;

namespace JiePan.Ass.Bll
{
    public abstract class BaseBll<T>:IBaseBll<T> where T : class,new()
    {
        public abstract IBaseDal<T> GetDal();
        public long Insert(T model)
        {
           return GetDal().Insert(model);
        }

        public int UpDate(T model)
        {
            return GetDal().UpDate(model);
        }

        public int Delete(object id)
        {
            return GetDal().Delete(id);
        }

        public T GetModel(object id)
        {
            return GetDal().GetModel(id);
        }

        public IQueryable<T> GetList(Expression<System.Func<T, bool>> where)
        {
            return GetDal().GetList(where);
        }

        public IQueryable<T> GetListByPage<Tkey>(Expression<System.Func<T, bool>> where, Expression<System.Func<T, Tkey>> orderby, int pageIndex, int pageSize)
        {
            return GetDal().GetListByPage(where, orderby, pageIndex, pageSize);
        }
    }
}