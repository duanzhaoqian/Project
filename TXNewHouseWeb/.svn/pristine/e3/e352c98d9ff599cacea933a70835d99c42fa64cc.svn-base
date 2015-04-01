using System;
using System.Linq;
using TXModel.Models;
using TXOrm;

namespace TXDal.WebSite
{
    public class ModelDal
    {
        /// <summary>
        /// 获取指定id的模块信息及父级信息
        /// </summary>
        /// <param name="modelId">模块ID</param>
        /// <returns></returns>
        public Model_AdminPageInfo Model_GetModelsInfo(int modelId)
        {
            try
            {
                using (var webEntity = new kyj_WebDBEntities())
                {
                    var query = (from model in webEntity.Models
                        join m2 in webEntity.Models on model.PId equals m2.Id into m3
                        from pmodel in m3.DefaultIfEmpty()
                        where model.Id == modelId
                        select new Model_AdminPageInfo
                        {
                            ItemId = model.Id,
                            ItemName = model.Name,
                            FatherItemId = model.PId,
                            FatherItemName = pmodel.Name
                        });
                    return query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Log4netService.RecordLog.RecordException("李雨钊", string.Format("modelId: {0}", modelId), ex);
                return null;
            }
        }
    }
}