using System.Collections.Generic;
using TXDal.HouseData;
using TXModel.Commons;
using TXOrm;

namespace TXBll.HouseData
{
    /// <summary>
    /// 楼盘BLL类
    /// </summary>
    public partial class PremisesBll
    {
        private readonly PremisesDal _premisesDal = new PremisesDal();

        #region 通用方法

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">对象实体</param>
        /// <returns>新增ID</returns>
        public int Add(Premis entity)
        {
            return _premisesDal.Add(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>影响行数</returns>
        public int Del_ById(int id)
        {
            return _premisesDal.Del_ById(id);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>影响行数</returns>
        public int Update_ById(int id)
        {
            return _premisesDal.Update_ById(id);
        }

        /// <summary>
        /// 获取分页集合
        /// </summary>
        /// <returns>结果集</returns>
        public Paging<object> GetPageList_BySearch(Paging<object> paging)
        {
            return _premisesDal.GetPageList_BySearch(paging);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">对象Id</param>
        /// <returns>结果集</returns>
        public Premis GetPremises_ById(int id)
        {
            return _premisesDal.GetPremises_ById(id);
        }
        public Premis GetPremises_ById2(int id)
        {
            return _premisesDal.GetPremises_ById2(id);
        }
        #region 根据开发商Id获取楼盘id和名称

        /// <summary>
        /// 根据开发商Id获取楼盘id和名称
        /// author: 汪敏
        /// </summary>
        /// <param name="developerId">开发商ID</param>
        /// <returns></returns>
        public List<Premis> GetPremisByDeveloperId(int developerId)
        {
            return _premisesDal.GetPremisByDeveloperId(developerId);
        }

        #endregion

        #endregion

        #region 发布楼盘

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/27 19:01:45
        /// 功能描述：发布楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PermitPresale"></param>
        /// <returns></returns>
        public int Add(Premis model, List<Traffic> subways)
        {
            return _premisesDal.Add(model, subways);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2013/11/27 19:01:45
        /// 功能描述：发布楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="PermitPresale"></param>
        /// <returns></returns>
        public int Add(Premis model, List<SandTableLabel> list, List<Traffic> subways)
        {
            return _premisesDal.Add(model, list, subways);
        }

        #endregion

        #region 修改楼盘

        /// <summary>
        /// 修改楼盘
        /// Author:wangzhikun
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="permitPresale">参数</param>
        /// <returns></returns>
        public int Update(Premis model, List<Traffic> subways)
        {
            return _premisesDal.Update(model, subways);
        }

        /// <summary>
        /// 作者：谢江
        /// 时间：2014/1/3 16:08:54
        /// 功能描述：修改楼盘
        /// </summary>
        /// <param name="model"></param>
        /// <param name="permitPresale"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int Update(Premis model, List<SandTableLabel> list, List<Traffic> subways)
        {
            return _premisesDal.Update(model, list, subways);
        }

        #endregion

        /// <summary>
        /// 多表进行修改
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="permitPresale">参数</param>
        /// <returns></returns>
        public int ModifyPremise(Premis model, string[] permitPresale)
        {
            return _premisesDal.ModifyPremise(model, permitPresale);
        }



        ///// <summary>
        ///// 提交报名信息
        ///// Author:sunlin
        ///// </summary>
        ///// <param name="activitiesUser"></param>
        ///// <param name="houseId"></param>
        ///// <returns></returns>
        //public int SubOfferInfo(TXOrm.BidPrice activitiesUser, int houseId)
        //{
        //    return _premisesDal.SubOfferInfo(activitiesUser, houseId);
        //}

        /// <summary>
        /// 提交报价信息
        /// Author:sunlin
        /// </summary>
        /// <param name="bidPrice"></param>
        /// <returns></returns>
        public int SubBidPriceInfo(BidPrice bidPrice)
        {
            return _premisesDal.SubBidPriceInfo(bidPrice);
        }

        /// <summary>
        /// 提交报价信息
        /// </summary>
        /// <param name="bidPrice"></param>
        /// <param name="isUpdateSalesStatus"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public int SubBidPriceInfo(BidPrice bidPrice, bool isUpdateSalesStatus, int houseId)
        {
            return _premisesDal.SubBidPriceInfo(bidPrice, isUpdateSalesStatus, houseId);
        }

        /// <summary>
        /// 返回户型图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetROOMTYPEImageUseCount(string id)
        {
            return _premisesDal.GetROOMTYPEImageUseCount(id);
        }

        /// <summary>
        /// 跟新已使用户型图
        /// </summary>
        /// <param name="id"></param>
        public string UpdateROOMTYPEImageUse(string id)
        {
            return _premisesDal.UpdateROOMTYPEImageUse(id);
        }
        /// <summary>
        /// 更新已使用平面图
        /// </summary>
        /// <param name="id"></param>
        public string UpdatePlaneImageUse(string id)
        {
            return _premisesDal.UpdatePlaneImageUse(id);
        }

        /// <summary>
        /// 返回平面图使用数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetPlaneImageUseCount(string id)
        {
            return _premisesDal.GetPlaneImageUseCount(id);
        }


        #region 同地区的楼盘名称不可重复，如重复则提示“楼盘名称已存在”
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/2/11 15:37:45
        /// 功能描述：同地区的楼盘名称不可重复，如重复则提示“楼盘名称已存在”
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Did"></param>
        /// <returns></returns>
        public int CheckPremises(string Name, int Did)
        {
            return _premisesDal.CheckPremises(Name, Did);
        }
        #endregion


        #region 根据楼盘ID 查询楼盘状态
        /// <summary>
        /// 作者：谢江
        /// 时间：2014/3/24 15:49:49
        /// 功能描述：根据楼盘ID 查询楼盘状态
        /// </summary>
        /// <param name="PremisesId"></param>
        /// <returns></returns>
        public int GetPremisesState(int PremisesId)
        {
            return _premisesDal.GetPremisesState(PremisesId);
        }
        #endregion

        /// <summary>
        /// 根据楼盘id获取楼盘浏览量和排名
        /// Author:maboxun
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TXOrm.PremisesViewCount GetPremisesViewCount(int id)
        {
            return dal.GetPremisesViewCount(id);
        }

        /// <summary>
        /// 取今日成交信息
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public TXOrm.PremisStatisticsInfo GetPremisStatisticsInfo(int premisesId)
        {
            return dal.GetPremisStatisticsInfo(premisesId);
        }

        /// <summary>
        /// 添加今日成交信息
        /// </summary>
        /// <param name="id"></param>
        public int SubBargainInfo(string premisesId, string SubscribedCount, string BargainCount)
        {
            return dal.SubBargainInfo(premisesId, SubscribedCount, BargainCount);
        }
    }
}