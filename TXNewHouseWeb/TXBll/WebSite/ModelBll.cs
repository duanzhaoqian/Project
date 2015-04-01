using TXDal.WebSite;
using TXModel.Models;

namespace TXBll.WebSite
{
    public class ModelBll
    {
        private ModelDal dal = null;

        public ModelBll()
        {
            if (dal == null)
                dal = new ModelDal();
        }

        #region 获取选项卡信息

        /// <summary>
        /// 获取指定id的模块信息及父级信息
        /// </summary>
        /// <param name="modelId">模块ID</param>
        /// <returns></returns>
        public Model_AdminPageInfo Model_GetModelsInfo(int modelId)
        {
            return dal.Model_GetModelsInfo(modelId);
        }

        #endregion
    }
}
