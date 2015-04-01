using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.DAL.Categories;
using KYJ.ZS.Commons.Index;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Commons;

namespace KYJ.ZS.DAL.RentalGoodses
{

    /// <summary>
    /// 作者：wwang
    /// 时间：2014-04-28
    /// 描述: 根据条件获取商品列表集合
    /// </summary>
    /// <param name="conditions">URL参数字符串</param>
    /// <param name="getdatapagename">Lucene搜索URL地址</param>
    /// <returns></returns>
    public class GetRentalGoodsListDal
    {
        CategoryAttributeDal catdal = new CategoryAttributeDal();


        public List<RentalGoodsListItemEntity> RentalGoodsListResutl(string conditions, string getdatapagename, out int totalcount)
        {

            try
            {
                IndexGoodsConditionInfo info = IndexCondition.GetSearchCondiction(conditions);


                return GetList(info, out totalcount);
            }
            catch (Exception ex)
            {
                totalcount = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException<string>("wwang", conditions + "," + getdatapagename, ex);
                return null;
            }

        }


        public List<RentalGoodsListItemEntity> GetList(IndexGoodsConditionInfo info, out int totalcount)
        {
            try
            {
                #region 参数
                PagePmsDal pagePmsDal = new PagePmsDal();
                int index = Convert.ToInt32(info.PageIndex);
                int pagesize = info.PageSize;
                string where = " rental_isdel ='false' and rental_status=1 and rental_state=2";
                string orderby = " rental_salesvolume DESC";
                string tablelist = @"[RentalGoodses] rg with(nolock) left join (select goods_id,stuff((select ','+cast(attrval_id as nvarchar(50)) FROM [GoodsAttributes] with(nolock) WHERE goods_id=T.goods_id FOR XML PATH('')),1,1,'') as attrvals_id from [GoodsAttributes] T with(nolock) group by T.goods_id) as ga  on  rg.[rental_id]=ga.goods_id left join (select goods_id,stuff((select ','+cast(norm_id as nvarchar(50)) FROM dbo.GoodsNorms WHERE goods_id=T.goods_id FOR XML PATH('')),1,1,'') as norms_id from [GoodsNorms] T group by T.goods_id) gn on rg.[rental_id]=gn.goods_id left join (select goods_id,min(goods_price) as goods_price from [GoodsPrices] with(nolock) group by goods_id) gp on rg.rental_id=gp.goods_id ";
                //string collist = @"rg.[rental_id] as GoodsId,rg.[rental_title] as GoodsName,case when min(gp.goods_price) is null then rg.[rental_monthprice] else min(gp.goods_price)end as MonthPrice,rg.[rental_price] as Price,rg.[rental_guid] as Guid,rg.[rental_categoryid] as CategoryId";
                string collist = @"distinct rg.[rental_id] as GoodsId,rg.[rental_title] as GoodsName ,case when gp.goods_price is null then rg.rental_monthprice else gp.goods_price end as MonthPrice,rg.[rental_price] as Price,rg.[rental_guid] as Guid,rg.[rental_categoryid] as CategoryId";
                int totalpage = 0;
                #endregion

                #region 拼接查询参数

                if (!string.IsNullOrEmpty(info.ThirdlyCatId))
                    where += " and rg.rental_categoryid = '" + info.ThirdlyCatId + "'";
                if (!string.IsNullOrEmpty(info.BrandId))
                    where += " and rg.rental_brandid='" + info.BrandId + "'";
                if (!string.IsNullOrEmpty(info.GoodsNormId))
                    where += " and charindex('," + info.GoodsNormId + ",',','+gn.norms_id+',')>0";
                if (!string.IsNullOrEmpty(info.KeyWord))
                    where += " and rg.[rental_title] like '%" + info.KeyWord + "%'";
                if (!string.IsNullOrEmpty(info.PriceBegin) && !string.IsNullOrEmpty(info.PriceEnd))
                {
                    where += " and case when gp.goods_price is null then rg.rental_monthprice else gp.goods_price end between " + info.PriceBegin + " and " + info.PriceEnd;
                }
                switch (info.Sort)
                {
                    case "0":
                        orderby = " rental_salesvolume DESC";
                        break;
                    case "1":
                        orderby = " case when gp.goods_price is null then rg.rental_monthprice else gp.goods_price end ";
                        break;
                    case "2":
                        orderby = " case when gp.goods_price is null then rg.rental_monthprice else gp.goods_price end  DESC";
                        break;
                    case "3":
                        orderby = " rental_updatetime DESC";
                        break;
                }
                if (info.Attrs.Count > 0)
                {
                    foreach (var i in info.Attrs)
                        where += " and charindex('," + i.AttributeValueId + ",',','+ga.attrvals_id+',')>0";
                }
                //where += " group by rg.[rental_id],[rental_title],rg.[rental_monthprice],[rental_price],[rental_guid],[rental_categoryid],[rental_salesvolume],[rental_updatetime]";
                #endregion

                var dt = KYJ_ZushouRDB.GetPages(index, where, orderby, collist, tablelist, pagesize, true, out totalcount, out totalpage);

                if (!Auxiliary.CheckDt(dt))
                    return new List<RentalGoodsListItemEntity>();

                return DataHelper<RentalGoodsListItemEntity>.GetEntityList(dt);
            }
            catch (Exception ex)
            {
                totalcount = 0;
                KYJ.ZS.Log4net.RecordLog.RecordException<IndexGoodsConditionInfo>("wwang", info, ex);
                return null;
            }
        }
        /// <summary>
        /// 作者：wwang
        /// 时间：2014-04-28
        /// 描述: 通过分类ID获取属性集合
        /// </summary>
        /// <param name="categoryid">分类ID</param>
        /// <returns></returns>
        public List<CategoryAttribute> GetCateroryAttrsById(int categoryid)
        {
            try
            {

                var list = catdal.GetCategoryAttributeValue(categoryid);

                List<CategoryAttribute> attrs = new List<CategoryAttribute>();
                if (list != null)
                {
                    foreach (var i in list)
                    {
                        //去除重复
                        if (attrs.Find((d) => d.AttributeName.CategoryAttrId == i.AttributeId) == null)
                        {
                            CategoryAttribute attr = new CategoryAttribute();
                            attr.AttributeName = new AttributeNameAttribute() { Name = i.AttributeName, CategoryAttrId = i.AttributeId };
                            attr.AttributeValues =
                                list.FindAll((a) => a.AttributeId == i.AttributeId)
                                .Select((s) => new AttributeValueAttribute() { Name = s.AttributeValName, AttributeValueId = s.AttributeValId }).
                                ToList();
                            attrs.Add(attr);
                        }
                    }
                }

                //通过Lucene获取分类属性集合

                return attrs;
            }
            catch (Exception ex)
            {
                KYJ.ZS.Log4net.RecordLog.RecordException<int>("wwang", categoryid, ex);
                return null;
            }
        }
    }
}
