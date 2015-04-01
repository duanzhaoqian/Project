using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.SaleGoodses;
using KYJ.ZS.Models.SaleGoodses;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.Commons;
using KYJ.ZS.Commons.Common;

namespace KYJ.ZS.BLL.SaleGoodses
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014.04.24
    /// 描述：用户后台，出售信息管理操作类
    /// </summary>
    public class SaleGoods_InformationManagementBll
    {
        SaleGoods_InformationManagementDal dal = new SaleGoods_InformationManagementDal();
        /// <summary>
        /// 获取当前页的数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <returns>PageData实体对象</returns>
        public PageData<SaleGoods_InformatioManagementEntity> GetPageData(int pageIndex,int userId)
        {

            //标签id数组
            string[] tagsid;
            //标签id
            int tagid;
            List<string> list = null;//存储获得的标签名
            PageData<SaleGoods_InformatioManagementEntity> pageData = null;//声明一个分页数据类的实例
            PagePms pagePms = new PagePms();//创建一个分页属性对象的实例
            pagePms.PageIndex = pageIndex;
            pagePms.PageSize = 5;
            pageData = dal.GetPageData(pagePms, userId);//获得分页数据
            //根据拿到的标签文本去查询标签名称
            for (int i = 0; i < pageData.DataList.Count; i++)
            {
                list = new List<string>();
                if (pageData.DataList[i].Tags != null)
                {
                    tagsid = pageData.DataList[i].Tags.Split(new char[] { ',' });//以“，”分割拿到的标签id文本
                    for (int j = 0; j < tagsid.Length; j++)
                    {
                        tagid = Auxiliary.ToInt32(tagsid[j]);
                        list.Add(dal.GetTag(tagid));//通过每个标签id查询标签名车，并加入到集合
                        pageData.DataList[i].TagsList = list;//将标签名称集合赋值给每条数据的tagslist属性
                    }

                }
                else
                {
                    pageData.DataList[i].TagsList = new List<string>();
                }
                string key = "S" + pageData.DataList[i].Id.ToString();
                pageData.DataList[i].BrowseCount = RedisTool.GetValue<int>(key, FunctionType.ZuShouBrowseAmount, 0);
                
            }
            return pageData;

        }
        /// <summary>
        /// 显示/隐藏商品（上架/下架）
        /// </summary>
        /// <param name="saleId">商品ID</param>
        /// <returns></returns>
        public bool ShowGoods(int saleId, int actionFlag,int userId)
        {
            if (dal.ShowGoods(saleId, actionFlag, userId) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除商品信息(标识删除)
        /// </summary>
        /// <param name="saleId">商品ID</param>
        /// <returns></returns>
        public bool DeleteGoods(int saleId,int userId)
        {
            if (dal.DeleteGoods(saleId, userId) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
