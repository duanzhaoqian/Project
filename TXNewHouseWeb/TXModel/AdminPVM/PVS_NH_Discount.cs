
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 限时折扣
    /// </summary>
    public class PVS_NH_Discount
    {
        /// <summary>
        /// 省
        /// </summary>
        public int ProvinceId { set; get; }

        public List<SelectListItem> Provinces { set; get; }

        /// <summary>
        /// 市
        /// </summary>
        public int CityId { set; get; }

        public List<SelectListItem> Cityes { set; get; }

        /// <summary>
        /// 开发商编号
        /// </summary>
        public int DeveloperId { set; get; }

        /// <summary>
        /// 开发商名称
        /// </summary>
        public string DeveloperName { set; get; }

        /// <summary>
        /// 楼栋编号
        /// </summary>
        public int PremisesId { set; get; }

        /// <summary>
        /// 楼栋列表
        /// </summary>
        public List<SelectListItem> Premisess { set; get; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivitieState { set; get; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public List<SelectListItem> ActivitieStates { set; get; }
    }
}