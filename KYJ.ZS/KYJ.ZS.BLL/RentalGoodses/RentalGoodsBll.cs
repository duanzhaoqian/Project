using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using KYJ.ZS.Commons;
using KYJ.ZS.DAL.RentalGoodses;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.Models.View;
using KYJ.ZS.BLL.BrowseAmounts;
using KYJ.ZS.Commons.Common;
namespace KYJ.ZS.BLL.RentalGoodses
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月17日
    /// 描述：出租获取数据访问类
    /// </summary>
    public partial class RentalGoodsBll
    {
        RentalGoodsDal dal = new RentalGoodsDal();
        #region 邓福伟
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-22
        /// 描述：获取Web前端，商品详情Model
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_RentalGoodsEntity Web_GetRentalGoodsEntity(int rental_id)
        {
            Web_RentalGoodsEntity entity = dal.Web_GetRentalGoodsEntity(rental_id);
            if (entity != null)
            {
                entity.OtherAttrs = entity.OtherAttrs.Decompress();
                entity.OtherColors = entity.OtherColors.Decompress();
                entity.OtherNorms = entity.OtherNorms.Decompress();
                entity.OtherPrices = entity.OtherPrices.Decompress();
            }
            return entity;
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-22
        /// 描述：Web前端租商品详情页,租商品详情Model预览页
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品详情的Web实体</returns>
        public Web_RentalGoodsEntity Web_GetRentalGoodsPreviewEntity(int rental_id)
        {
            Web_RentalGoodsEntity entity = dal.Web_GetRentalGoodsPreviewEntity(rental_id);
            if (entity != null)
            {
                entity.OtherAttrs = entity.OtherAttrs.Decompress();
                entity.OtherColors = entity.OtherColors.Decompress();
                entity.OtherNorms = entity.OtherNorms.Decompress();
                entity.OtherPrices = entity.OtherPrices.Decompress();
            }
            return entity;
        }
        /// <summary>
        /// 作者: 邓福伟
        /// 时间：2014-04-22
        /// 描述: 根据商铺Id随机获取10个商品
        /// </summary>
        /// <param name="merchant_id">商铺Id</param>
        /// <returns></returns>
        public List<Web_RentalGoodsEntity> Web_GetRentalGoodsListByMerchantId(int rental_id, int merchant_id)
        {
            List<Web_RentalGoodsEntity> list = dal.Web_GetRentalGoodsListByMerchantId(rental_id, merchant_id);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Web_RentalGoodsEntity entity = list[i];
                    if (entity.Title != null)
                    {
                        list[i].Title = entity.Title.Length <= 10 ? entity.Title : entity.Title.Substring(0, 10)+"...";
                    }
                    //获取浏览数
                    //list[i].BrowseAmount = GetBrowseAmount(entity);
                }
            }
            else
            {
                list = new List<Web_RentalGoodsEntity>();
            }
            return list;
        }
        /// <summary>
        /// 作者: 邓福伟
        /// 时间：2014-04-22
        /// 描述: 根据分类随机获取10个商品
        /// </summary>
        /// <param name="merchant_id">分类Id</param>
        /// <returns></returns>
        public List<Web_RentalGoodsEntity> Web_GetRentalGoodsListByCategoryId(int rental_id, int rental_categoryid)
        {
            List<Web_RentalGoodsEntity> list = dal.Web_GetRentalGoodsListByCategoryId(rental_id, rental_categoryid);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Web_RentalGoodsEntity entity = list[i];
                    if (entity.Title != null)
                    {
                        list[i].Title = entity.Title.Length <= 10 ? entity.Title : entity.Title.Substring(0, 10) + "...";
                    }
                    //获取浏览数
                    //list[i].BrowseAmount = GetBrowseAmount(entity);
                }
            }
            else
            {
                list = new List<Web_RentalGoodsEntity>();
            }
            return list;
        }
        private int GetBrowseAmount(Web_RentalGoodsEntity entity)
        {
            string key = "Z" + entity.Id.ToString();
            return RedisTool.GetValue<int>(key, FunctionType.ZuShouBrowseAmount, 0);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-6-12
        /// 描述：根据商品Id取商品的价格扩展
        /// </summary>
        /// <param name="rental_id">商品Id</param>
        /// <returns>返回商品价格扩展</returns>
        public string Web_GetRentalOtherPrice(int rental_id)
        {
            return dal.Web_GetRentalOtherPrice(rental_id);
        }
        #endregion
        /// <summary>
        /// 作者：maq
        /// 时间：2014年4月25日09:52:49
        /// 描述:获取商量列表
        /// </summary>
        /// <returns></returns>
        public PageData<WareHouseGoodsEntity> GetList(int merchantId, RentalGoodsesQueryPms queryPmspms)
        {
            PagePms pms = new PagePms();
            pms.PageSize = 10;
            pms.SortColnum = " rental_createtime desc ";
            return dal.GetList(merchantId, pms, queryPmspms);
        }

        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月19日15:48:10
        /// 描述：判断租期模板是否被引用
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns>true代表被引用，false代表未被引用</returns>
        public Boolean GetUsedTemplate(int templateId)
        {
            bool result = false;
            string num = dal.GetUsedTemplate(templateId);
            if (!string.IsNullOrEmpty(num) && Convert.ToInt32(num) > 0)
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// 作者：maq
        /// 时间：2014年5月6日15:09:23
        /// 描述：违规商品列表
        /// </summary>
        /// <returns></returns>
        public PageData<RentalGoodsIllegalGoodsEntity> GetIiiegalGoods(int merchantId, RentalGoodsesQueryPms queryPmspms)
        {
            PagePms pms = new PagePms();
            pms.PageSize = 10;
            pms.SortColnum = " rental_createtime desc ";
            return dal.GetIiiegalGoods(merchantId, pms, queryPmspms);
        }

        /// <summary>
        /// 商品下架
        /// </summary>
        /// <param name="idList">id列表</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public Boolean ShelfOff(string idList, out string message)
        {
            Boolean result = false;
            List<int> list = ToIntList(idList);
            if (list != null)
            {
                int i = dal.ShelfOff(list.ToArray());
                if (i == list.Count)
                {
                    message = "成功下架" + i.ToString() + "件商品！";
                    result = true;
                }
                else
                {
                    message = "下架失败，请您重试！";
                }
            }
            else
            {
                //参数错误
                message = "下架失败，请您重试！";
            }
            return result;
        }
        /// <summary>
        /// 商品上架
        /// </summary>
        /// <param name="idList">id列表</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public Boolean Shelf(string idList, out string message)
        {
            Boolean result = false;
            List<int> list = ToIntList(idList);
            if (list != null)
            {
                int count = 0;
                foreach (int temp in list)
                {
                    int i = dal.Shelf(temp);
                    if (i > 0)
                    {
                        result = true;
                        count++;
                    }
                }
                if (count == list.Count)
                {
                    message = "成功上架" + count + "件商品！";
                    result = true;
                }
                else
                {
                    message = "成功上架" + count + "件商品！" + (list.Count - count) + "件上架失败！";
                }
            }
            else
            {
                //参数错误
                message = "上架失败，请您重试！";
            }
            return result;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Boolean DeleteList(string idList, out string message)
        {
            Boolean result = false;
            List<int> list = ToIntList(idList);
            if (list != null)
            {
                int count = 0;
                foreach (int temp in list)
                {
                    if (dal.Delete(temp))
                    {
                        result = true;
                        count++;
                    }
                }
                if (count == list.Count)
                {
                    message = "成功删除" + count + "件商品！";
                    result = true;
                }
                else
                {
                    message = "成功删除" + count + "件商品！" + (list.Count - count) + "件删除失败！";
                }
            }
            else
            {
                //参数错误
                message = "删除失败，请您重试！";
            }
            return result;
        }

        /// <summary>
        /// 对id列表进行检查
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        private bool CheckIdList(string idList)
        {
            string[] arrStr = idList.Split(',');
            foreach (string s in arrStr)
            {
                if (!Auxiliary.IsNumber(s))
                {
                    return false;
                }
            }
            return true;
        }

        private List<int> ToIntList(string idList)
        {
            string[] arrStr = idList.Split(',');
            List<int> list = new List<int>();
            foreach (string s in arrStr)
            {
                if (!Auxiliary.IsNumber(s))
                {
                    return null;
                }
                else
                {
                    list.Add(Convert.ToInt32(s));
                }
            }
            return list;
        }
        /// <summary>
        /// 作者：wangyu
        /// 时间：2014/4/28 10:13:41
        /// 描述：发布商品（租）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool PublishGoods(RentalGoodsEntity model)
        {
            if (model == null) return false;

            return dal.AddRentalGoods(model);
        }

        /// <summary>
        /// 作者：wangyu
        /// 时间：2014/4/28 10:13:41
        /// 描述：修改商品（租） 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoods(RentalGoodsEntity model)
        {
            if (model == null) return false;

            return dal.UpdateRentalGoods(model);
        }

        public RentalGoods GetRentalGoodsById(int id)
        {
            return dal.Get(id);
        }

        public RentalOther GetRentalOtherById(int id)
        {
            return new RentalOtherDal().Get(id);
        }

        public RentalGoodsEntity GetRentalGoods(int id)
        {
            var entity = new RentalGoodsEntity();
            entity.RentalGoods = GetRentalGoodsById(id);
            entity.RentalOther = GetRentalOtherById(id);
            if (entity.RentalOther != null)
            {
                entity.GoodsAttribute = entity.RentalOther.Attrs.Decompress().FromJson<List<GoodsAttribute>>();
                entity.GoodsColor = entity.RentalOther.Colors.Decompress().FromJson<List<GoodsColor>>();
                entity.GoodsNorm = entity.RentalOther.Norms.Decompress().FromJson<List<GoodsNorm>>();
                entity.GoodsPrice = entity.RentalOther.Prices.Decompress().FromJson<List<GoodsPrice>>();
            }
            
            return entity;
        }

        /// <summary>
        /// Author：ningjd
        /// Time：2014-04-29
        /// Desc：判断运费模板是否被商品使用
        /// </summary>
        /// <param name="freightTemplateID">运费模板ID</param>
        /// <returns></returns>
        public bool isUsing(int freightTemplateID)
        {
            return dal.isUsing(freightTemplateID);
        }

        /// <summary>
        /// 获取各个状态的商品
        /// </summary>
        /// <param name="merId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetRentalGoodsStateNum(int merId)
        {
            return dal.GetRentalGoodsStateNum(merId);
        }

        #region 商品管理（审核）-----ningjd

        /// <summary>
        /// Author:ningjd
        /// Date:2014-05-22
        /// Desc:商品管理
        /// </summary>
        /// <param name="areaEnum">商品区域</param>
        /// <param name="title">商品名称</param>
        /// <param name="firstCategoryId">一级类目</param>
        /// <param name="secondCategoryId">二级类目</param>
        /// <param name="thirdCategoryId">三级类目</param>
        /// <param name="merchantName">商户名称</param>
        /// <param name="code">商家编号</param>
        /// <param name="index">当前页</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <param name="totalRecord">总个数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public IList<RentalGoodsIndexEntity> GetRentalGoodsList(RentalGoodsAreaEnum areaEnum, string title, int firstCategoryId, int secondCategoryId, int thirdCategoryId, string merchantName, string code, int index, int pageSize, out int totalRecord, out int totalPage)
        {
            return dal.GetRentalGoodsList(areaEnum, title, firstCategoryId, secondCategoryId, thirdCategoryId, merchantName, code, index, pageSize, out totalRecord, out totalPage);
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
    }
}
