using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 管理后台摇号活动搜索实体
    /// </summary>
    public class PVS_NH_YaoHao
    {
        /// <summary>
        /// 0 活动管理 1审批管理
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 标记状态0 待处理 1 已通过 2 未通过 3未联系 4 已联系 5 已通过并开启报名入口
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 活动状态 0未开始 1已开始 2已结束
        /// </summary>
        public int ActivitieState { get; set; }
        /// <summary>
        /// 省份Id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public int CompanyType { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 活动名
        /// </summary>
        public string ActivitieName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        #region 级联选择辅助

        private List<SelectListItem> _Provinces = new List<SelectListItem>() { new SelectListItem() { Text = "选择省份", Value = "0" } };
        /// <summary>
        /// 省份绑定数据
        /// </summary>
        public List<SelectListItem> Provinces
        {
            get { return _Provinces; }
            set { _Provinces = value; }
        }

        private List<SelectListItem> _Cities = new List<SelectListItem>() { new SelectListItem() { Text = "选择城市", Value = "0" } };
        /// <summary>
        /// 城市绑定数据
        /// </summary>
        public List<SelectListItem> Cities
        {
            get { return _Cities; }
            set { _Cities = value; }
        }
        /// <summary>
        /// 标记状态下拉列表
        /// </summary>
        public List<SelectListItem> StateList { get; set; }
        /// <summary>
        /// 活动状态下拉列表
        /// </summary>
        public List<SelectListItem> ActivitieStateList { get; set; }
        /// <summary>
        /// 公司类型下拉列表
        /// </summary>
        public List<SelectListItem> CompanyTypeList { get; set; }
        #endregion
    }
}
