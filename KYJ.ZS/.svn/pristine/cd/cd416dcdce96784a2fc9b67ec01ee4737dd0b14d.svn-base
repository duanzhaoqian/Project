using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.DAL.Categories;
using KYJ.ZS.Models.Categories;
using KYJ.ZS.Models.DB;
using KYJ.ZS.DAL.Generalizes;
using KYJ.ZS.Models.Pages;
using KYJ.ZS.DAL.DB;
using KYJ.ZS.Models.Generalizes;
using KYJ.ZS.Models.RentalGoodses;
using KYJ.ZS.DAL.RentalGoodses;


namespace KYJ.ZS.BLL.Generalizes
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-28
    /// 描述：管理商品排序
    /// </summary>
   public class GeneralizeManageBll
    {
       GeneralizeManageDal dal = new GeneralizeManageDal();
       RentalGoodsDal rentalDal = new RentalGoodsDal();
       CategoryDal categorydal = new CategoryDal();
       /// <summary>
       /// 商品排序信息的列表
       /// </summary>
       /// <param name="pageIndex">当前页码</param>
       /// <param name="searchEntity">搜索条件</param>
       /// <returns></returns>
       public PageData<Generalize> GetGeneralizeDataNoAduit(int pageIndex,GeneralizeSearchEntity searchEntity)
       {
           PagePmsDal pmsDal = new PagePmsDal();
           pmsDal.PageIndex = pageIndex;
           pmsDal.PageSize = 10;
           return dal.GetPageData(pmsDal, searchEntity); 
           
       }
       /// <summary>
       /// 删除排序
       /// </summary>
       /// <param name="generalize"></param>
       /// <returns></returns>
       public bool DeleteGeneralize(Generalize generalize)
       {
          if(dal.DeleteGeneralize(generalize)>0)
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
       /// <param name="generalize"></param>
       /// <returns></returns>
       public bool DownLine(Generalize generalize)
       {
           if(dal.DownLine(generalize)>0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       /// <summary>
       /// 获取排序的位置信息
       /// </summary>
       /// <param name="generalizeId">排序Id</param>
       /// <returns></returns>
       public  Manager_GeneralizeLocationEntity GetLacationById(int generalizeId)
       {
           Manager_GeneralizeLocationEntity locationEntity = new Manager_GeneralizeLocationEntity();
           Generalize generalize = new Generalize();
           generalize = dal.GetGeneralizeById(generalizeId);
           locationEntity = dal.GetLoactionInfo(generalize.GeneralizeLocationId);
           return locationEntity;
       }
       /// <summary>
       /// 根据位置ID获取位置信息
       /// </summary>
       /// <param name="locationId"></param>
       /// <returns></returns>
        public Manager_GeneralizeLocationEntity GetLocationEntityById(int locationId)
        {
            Manager_GeneralizeLocationEntity locationEntity = new Manager_GeneralizeLocationEntity();
            locationEntity = dal.GetLoactionInfo(locationId);
            return locationEntity;
        }

        /// <summary>
       /// 获取商品排序信息
       /// </summary>
       /// <param name="generalizeId"></param>
       /// <returns></returns>
       public Generalize GetGeneralizeInfoById(int generalizeId)
       {
           return dal.GetGeneralizeById(generalizeId);
       }
       /// <summary>
       /// 获取排序商品
       /// </summary>
       /// <param name="generalizeId"></param>
       /// <returns></returns>
       public List<Manager_RentalGoodsAduitEntity>  GetGeneralizeGoods(int generalizeId)
       {
           //所进行排序的商品
           RentalGoods rentGood = null;
           List<Manager_RentalGoodsAduitEntity> list = dal.GetRentalInfoByGeneralizeId(generalizeId);
           for (int i = 0; i < list.Count; i++)
           {
               rentGood = rentalDal.Get(list[i].GoodsId);
               list[i].Brand = rentGood.BrandName;
               list[i].GoodsCode = rentGood.Code;
               list[i].GoodsName = rentGood.Title;
               list[i].MerchantName = dal.GetMerchantName(rentGood.MerchantId);
               list[i].MonthPrice = rentGood.MonthPrice;
           }
           return list;
       }
       /// <summary>
       /// 根据位置信息获取当前位置
       /// </summary>
       /// <returns></returns>
       public List<Manager_GeneralizeLocationEntity> GetLocation(Manager_GeneralizeLocationEntity location)
       {
           return dal.GetLoactionId(location);
       }
       /// <summary>
       /// 排序商品搜索
       /// </summary>
       /// <param name="index">当前页码</param>
       /// <param name="rentalSearch">搜索条件</param>
       /// <returns></returns>
       public PageData<Manager_RentalGoodsAduitEntity> GetGeneralizeGoods(int index, GeneralizeRentalGoodsSearchEntity rentalSearch)
       {
           PagePmsDal pagePms = new PagePmsDal();
           pagePms.PageIndex = index;
           pagePms.PageSize = 10;
           return dal.GetGeneralizeGoods(pagePms,rentalSearch);
       }
       /// <summary>
       /// 删除排序商品
       /// </summary>
       /// <param name="GeneralizeId"></param>
       /// <returns></returns>
       public bool DelelteGoods(int GeneralizeId)
       {
           if (dal.DeleteGeneralizeGoods(GeneralizeId) > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       /// <summary>
       /// 更新商品排序信息
       /// </summary>
       /// <param name="generalizeId"></param>
       /// <returns></returns>
       public int UpdateGeneralize(Generalize generalize)
       {
           generalize.UpdateTime = DateTime.Now;
           return dal.UpdateGeneralize(generalize);
       }
       /// <summary>
       /// 更新排序商品列表
       /// </summary>
       /// <param name="rentalGoods"></param>
       /// <returns></returns>
       public bool UpdateGeneralizeGoods(Manager_RentalGoodsAduitEntity rentalGoods,int generalizeId)
       {
          if(dal.AddGeneralizeGoods(rentalGoods,generalizeId)>0)
          {
              return true;
          }
           else
          {
              return false;
          }
       }
       /// <summary>
       /// 申请审核
       /// </summary>
       /// <param name="generalizeId"></param>
       /// <returns></returns>
       public bool ApplyGeneralize(Generalize generalize)
       {
           if (dal.ApplyAduit(generalize) > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       /// <summary>
       /// 审核商品排序
       /// </summary>
       /// <param name="generalize">商品信息</param>
       /// <returns></returns>
       public bool CheckGeneralize(Generalize generalize)
       {
           if(dal.CheckGeneralize(generalize)>0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
       /// <summary>
       /// 根据分类ID取父节点信息
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public List<Category> GetParentCategoryName(int id,List<Category> list )
        {
            Category category = new Category();
           category = categorydal.Get(id);
           if (category==null)
           {
               return list;
           }
           if (category.PId>0)
           {
               GetParentCategoryName(category.PId, list);
           }
            list.Add(category);
           return list;
           
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <returns></returns>
        public List<GeneralizeTypes> GetGeneralizeTypesData()
        {
            return dal.GetGeneralizetypesData();
        }
        /// <summary>
        /// 添加商品排序(返回ID)
        /// </summary>
        /// <param name="advert"></param>
        /// <returns></returns>
        public int PublishGeneralize(Generalize generalize)
        {
            return dal.PublishGeneralize(generalize);
        } 

    }
}
