using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webdiyer.WebControls.Mvc;
namespace KYJ.ZS.BLL.SaleGoodses
{
    using System.Transactions;

    using KYJ.ZS.DAL.SaleGoodses;
    using KYJ.ZS.Models.DB;
    using KYJ.ZS.Models.SaleGoodses;
    using KYJ.ZS.Commons.Index;
    using KYJ.ZS.Commons.Common;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/25 10:38:38
    /// 描述：出售商品操作
    /// </summary>
    public class SaleGoodsBll
    {
        private readonly SaleGoodsDal dal;
        public SaleGoodsBll()
        {
            if (dal == null) dal = new SaleGoodsDal();
        }

        public SaleGoods GetSaleGoodsById(int id)
        {
            return dal.Get(id);
        }

        public SaleOther GetSaleOtherById(int id)
        {
            return new DAL.SaleGoodses.SaleOtherDal().Get(id);
        }

        public SaleGoodsEntity GetSaleGoods(int id)
        {
            var entity = new SaleGoodsEntity();
            entity.SaleGoods = GetSaleGoodsById(id);
            entity.SaleOther = GetSaleOtherById(id);

            return entity;
        }

        /// <summary>
        /// 发布商品（出售）
        /// </summary>
        /// <returns></returns>
        public bool AddSaleGoods(SaleGoodsEntity model)
        {
            if (model == null) return false;

            return dal.AddSaleGoods(model);
        }

        /// <summary>
        /// 更新商品（售）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateSaleGoods(SaleGoodsEntity model)
        {
            if (model == null) return false;

            return dal.UpdateSaleGoods(model);
        }
        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-30
        /// 描述：Web前端售商品详情页,售商品详情Model
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_SaleGoodsEntity Web_GetSaleGoodsEntity(int sale_id)
        {
            return dal.Web_GetSaleGoodsEntity(sale_id);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-30
        /// 描述：Web前端售商品详情页,售商品详情Model预览页
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_SaleGoodsEntity Web_GetSaleGoodsPreviewEntity(int sale_id)
        {
            return dal.Web_GetSaleGoodsPreviewEntity(sale_id);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-17
        /// 描述：返回用户的商品数
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int Web_GetSaleGoodsCount(int userId)
        {
           return dal.Web_GetSaleGoodsCount(userId);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-21
        /// 描述：返回用户的商品列表
        /// </summary>
        /// <param name="sale_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Web_SaleGoodsEntity> Web_GetSaleGoodsListByUserId(int sale_id, int user_id)
        {
            List<Web_SaleGoodsEntity> list = dal.Web_GetSaleGoodsListByUserId(sale_id, user_id);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Web_SaleGoodsEntity entity = list[i];
                    if (entity.Title != null) //判断商品标题是否为空
                    {
                        list[i].Title = entity.Title.Length <= 10 ? entity.Title : entity.Title.Substring(0, 10) + "...";
                    }
                    //获取浏览数
                    //list[i].BrowseAmount = GetBrowseAmount(entity);
                }
            }
            else
            {
                list = new List<Web_SaleGoodsEntity>(); 
            }
            return list;
        }
        private int GetBrowseAmount(Web_SaleGoodsEntity entity)
        {
            string key = "S" + entity.Id.ToString();
            return RedisTool.GetValue<int>(key, FunctionType.ZuShouBrowseAmount, 0);
        }
        #endregion
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-6
        /// 描述：通过搜索索引从Lucene获取二手商品集合
        /// </summary>
        /// <param name="conditions">条件索引</param>
        /// <param name="getdatapagename"></param>
        /// <returns></returns>
        public PagedList<Web_SaleGoodsEntity> GetSaleGoodsList(string conditions, string getdatapagename)
        {
            int totalcount=0;
            SaleGoodsConditionInfo info = SaleGoodsCondition.GetSearchCondiction(conditions);

            List<Web_SaleGoodsEntity> list = dal.GetSaleGoodsList(conditions, getdatapagename, out totalcount);

            PagedList<Web_SaleGoodsEntity> pagelist = new PagedList<Web_SaleGoodsEntity>(list, info.PageIndex, info.PageSize, totalcount);

            //转换成分页集合
            return pagelist;
        }

        /// <summary>
        /// 作者：wwang
        /// 时间：2014-5-29
        /// 描述：获取闲置物品总条数
        /// </summary>
        /// <returns></returns>
        public int GetSaleGoodsTotalCount()
        {
            return dal.GetSaleGoodsTotalCount();
        }

        /// <summary>
        /// Author:baiyan
        /// Date:2014-05-14
        /// Desc:用户后台首页已发布信息个数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetSaleGoodsCountByUserId(int userId)
        {
            return dal.GetSaleGoodsCountByUserId(userId);
        }

        #region 商品管理（审核）-----ningjd

        /// <summary>
        /// Author:ningjd
        /// Date:2014-05-22
        /// Desc:商品管理
        /// </summary>
        /// <param name="areaEnum">商品所在区域Enum</param>
        /// <param name="userLoginName">用户账户</param>
        /// <param name="title">信息名称</param>
        /// <param name="number">信息编号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<SaleGoodsIndexEntity> GetManageList(SaleGoodsAreaEnum areaEnum, string userLoginName, string title, string number, DateTime? beginTime, DateTime? endTime, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetManageList(areaEnum, userLoginName, title, number, beginTime, endTime, index, pageSize, out totalRecord, out totalPage);
        }

        /// <summary>
        /// Author:ningjd
        /// Date:2014-05-21
        /// Desc:商品审核
        /// </summary>
        /// <param name="goodsId">商品ID</param>
        /// <param name="shelfReason">违规原因</param>
        /// <param name="isAudited">是否通过审核</param>
        /// <returns></returns>
        public bool Audited(int goodsId, string shelfReason, bool isAudited)
        {
            return dal.Audited(goodsId, shelfReason, isAudited) > 0;
        }

        #endregion

        #region 根据用户Id获取用户的认证状态 +int GetPapersstatusByUserId(int userId)
        /// <summary>
        /// 根据用户Id获取用户的认证状态
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns>返回 认证的状态值</returns>
        public int GetPapersstatusByUserId(int userId)
        {
            return dal.GetPapersstatusByUserId(userId);
        } 
        #endregion

    }
}
