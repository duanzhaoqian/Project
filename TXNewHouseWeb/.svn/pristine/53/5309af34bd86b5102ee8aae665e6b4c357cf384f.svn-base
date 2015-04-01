using System.Collections.Generic;
using TXDal.HouseData;
using TXOrm;
using TXModel.Commons;

namespace TXBll.HouseData
{
    public partial class HouseTemplateBll
    {
        private readonly HouseTemplateDal _houseTemplateDal = new HouseTemplateDal();


        #region 添加房源模版
        /// <summary>
        /// 添加房源模版
        /// </summary>
        /// <param name="entity">房源模版实体</param>
        /// <returns>返回：新添加房源模版编号</returns>
        public int Add(HouseTemplate entity)
        {
            return _houseTemplateDal.Add(entity);
        }

        #endregion

        #region 修改模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/27 15:02:53
        /// 功能描述：修改模版
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int update(HouseTemplate model)
        {
            return _houseTemplateDal.update(model);
        }
        #endregion

        #region 房源模版列表
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/16 16:33:27
        /// 功能描述：房源模版列表
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public Paging<HouseTemplate> GetHouseTemplateList(Paging<HouseTemplate> paging, int DeveloperId)
        {
            return _houseTemplateDal.GetHouseTemplateList(paging, DeveloperId);
        }

        #endregion

        #region 根据开发商Id 查询模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/18 13:54:35
        /// 功能描述：根据开发商Id 查询模版
        /// </summary>
        /// <param name="DeveloperId"></param>
        /// <returns></returns>
        public List<HouseTemplate> GetHouseTemplateByDeveloperId(int DeveloperId)
        {
            return _houseTemplateDal.GetHouseTemplateByDeveloperId(DeveloperId);
        }
        #endregion

        #region 根据Id 查询模版
        /// <summary>
        /// 作者：谢江
        /// 时间：2013/12/18 16:46:02
        /// 功能描述：根据Id 查询模版
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public HouseTemplate GetHouseTemplateById(int Id)
        {
            return _houseTemplateDal.GetHouseTemplateById(Id);
        }
        #endregion

        #region 删除单个房源模版
        ///作者：谢江
        ///时间：2014/1/20 17:14:06
        ///功能描述：删除单个房源模版
        public int DelHouseTemplateByID(int Id)
        {
            return _houseTemplateDal.DelHouseTemplateByID(Id);
        }
        #endregion

    }
}
