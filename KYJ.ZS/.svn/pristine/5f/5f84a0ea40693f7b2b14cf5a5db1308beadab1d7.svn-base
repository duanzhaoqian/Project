using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webdiyer.WebControls.Mvc;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.Commons.Index;
using KYJ.ZS.DAL.RentalGoodses;
using KYJ.ZS.Models.Pages;

namespace KYJ.ZS.BLL.RentalGoodses
{
    /// 作者：wwang
    /// 时间：2014-04-17
    /// 描述: 获取商品列表
    public class GetRentalGoodsListBll
    {
        private GetRentalGoodsListDal _dal = new GetRentalGoodsListDal();
        /// <summary>
        /// 根据条件获取商品集合
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="getdatapagename"></param>
        /// <returns></returns>
        public PagedList<RentalGoodsListItemEntity> RentalGoodsListResutl(string conditions, string getdatapagename)
        {
            int totalcount=0;
            //生成搜索对象
            IndexGoodsConditionInfo info = IndexCondition.GetSearchCondiction(conditions);

            //通过Lucene取得搜索结果
            List<RentalGoodsListItemEntity> listform = _dal.RentalGoodsListResutl(conditions, getdatapagename,out totalcount);
            PagedList<RentalGoodsListItemEntity> pagelist = new PagedList<RentalGoodsListItemEntity>(listform, info.PageIndex, info.PageSize, totalcount);
            //转换成分页集合
            return pagelist;

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
            return _dal.GetCateroryAttrsById(categoryid);
        }

    }
}
