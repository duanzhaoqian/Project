#define isUse
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Adverts;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Adverts;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.DAL.Categories;

namespace KYJ.ZS.BLL.Adverts
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-28
    /// 描述：管理广告排序
    /// </summary>
    public class AdvertManageBll
    {
        AdvertManageDal dal = new AdvertManageDal();
        CategoryDal categorydal = new CategoryDal();
         // RentalGoodsDal rentalDal = new RentalGoodsDal();
        /// <summary>
        /// 广告排序信息的列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="searchEntity">搜索条件</param>
        /// <returns></returns>
        public PageData<Advert> GetAdvertDataNoAduit(int pageIndex, AdvertsSearchEntity searchEntity)
        {
            PagePmsDal pmsDal = new PagePmsDal();
            pmsDal.PageIndex = pageIndex;
            pmsDal.PageSize = 10;
            return dal.GetPageData(pmsDal, searchEntity);

        }
        /// <summary>
        /// 提交核审
        /// </summary>
        /// <param name="AdvertId"></param>
        /// <returns></returns>
        public bool ApplyAduit(Advert advert)
        {
            if (dal.ApplyAduit(advert) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="Advert"></param>
        /// <returns></returns>
        public bool DeleteAdvert(Advert advert)
        {
            if (dal.DeleteAdvert(advert) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 强制下线
        /// </summary>
        /// <param name="Advert"></param>
        /// <returns></returns>
        public bool DownLine(Advert advert)
        {
            if (dal.DownLine(advert) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据广告ID获取广告位置信息
        /// </summary>
        /// <param name="AdvertId">广告Id</param>
        /// <returns></returns>
        public Manager_AdvertLocationEntity GetLacationById(int advertId)
        {
            Manager_AdvertLocationEntity locationEntity = new Manager_AdvertLocationEntity();
            Advert Advert = new Advert();
            Advert = dal.GetAdvertById(advertId);
            locationEntity = dal.GetLoactionInfo(Advert.AdvertLocationId);
            return locationEntity;
        }

        /// <summary>
        /// 根据广告ID获取广告信息
        /// </summary>
        /// <param name="AdvertId">广告Id</param>
        /// <returns></returns>
        public Advert GetAdvertById(int advertId)
        {
            Advert Advert = new Advert();
            Advert = dal.GetAdvertById(advertId);
            return Advert;
        }

        

        /// <summary>
        /// 删除排序商品
        /// </summary>
        /// <param name="AdvertId"></param>
        /// <returns></returns>
        public bool DelelteGoods(int advertId)
        {
            if (dal.DeleteAdvertGoods(advertId) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
#if !isUse
        /// <summary>
        /// 申请审核  已停用
        /// </summary>
        /// <param name="AdvertId"></param>
        /// <returns></returns>
        public bool ApplyAdvert(Advert advert)
        {
            if (dal.ApplyAdvert(advert) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
#endif
        /// <summary>
        /// 审核广告
        /// </summary>
        /// <param name="Advert"></param>
        /// <returns></returns>
        public bool CheckAdvert(Advert advert)
        {
            if (dal.CheckAdvert(advert) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 获取页面数据 +List<AdvertTypes> GetAdvertTypesData()
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <returns></returns>
        public List<AdvertTypes> GetAdvertTypesData()
        {
            return dal.GetAdvertTypesData();
        }  
        #endregion

        #region 根据分类ID查找其父分类名称 +string GetCategoryName(int categoryId)
        /// <summary>
        /// 根据分类ID查找其父分类名称
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <returns>父分类名称</returns>
        public string GetCategoryName(int categoryId)
        {
            return dal.GetCategoryName(categoryId);
        } 
        #endregion

        #region 根据分类ID递归取父节点信息 +List<Category> GetParentCategoryName(int id, List<Category> list)
        /// <summary>
        /// 根据分类ID递归取父节点信息
        /// </summary>
        /// <param name="id">分类ID</param>
        /// <returns></returns>
        public List<Category> GetParentCategoryName(int id, List<Category> list)
        {
            Category category = new Category();
            category = categorydal.Get(id);
            if (category == null)
            {
                return list;
            }
            if (category.PId > 0)
            {
                GetParentCategoryName(category.PId, list);
            }
            list.Add(category);
            return list;
        } 
        #endregion

        #region 根据页面类型和分类查找广告位置信息 +List<AdvertLocations> GetLocationData(int advertTpyeID, int? categoryID)
        /// <summary>
        /// 根据页面类型和分类查找广告位置信息
        /// </summary>
        /// <param name="advertTypeId">页面ID</param>
        /// <param name="categoryId">分类ID</param>
        /// <returns></returns>
        public List<AdvertLocations> GetLocationData(int advertTypeId, int? categoryId)
        {
            return dal.GetLocationData(advertTypeId, categoryId);
        }
        #endregion

        #region 获取允许发布广告数量（图片） +int GetMaxNum(int? locationID)
        /// <summary>
        /// 获取允许发布广告数量（图片）
        /// </summary>
        /// <param name="locationID">广告位置ID</param>
        /// <returns>允许发布广告数量</returns>
        public string GetMaxNum(int? locationID)
        {
            return dal.GetMaxNum(locationID);
        }
        #endregion

        #region 获取图片尺寸 +AdvertSize GetAdvertSize(int? locationId)
        /// <summary>
        /// 获取图片尺寸
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public AdvertSize GetAdvertSize(int? locationId)
        {
            return dal.GetAdvertSize(locationId);
        } 
        #endregion

        #region 发布广告 +int PublishAdvert(Advert advert)
        /// <summary>
        /// 发布广告 
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int PublishAdvert(Advert advert)
        {
            return dal.PublishAdvert(advert);
        } 
        #endregion

        #region 修改广告 +int ModifyAdvert(Advert advert)
        /// <summary>
        /// 修改广告
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int ModifyAdvert(Advert advert)
        {
            return dal.ModifyAdvert(advert);
        } 
        #endregion
    }
}
