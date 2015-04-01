using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 开发商搜索实体
    /// </summary>
    public class PVS_NH_Developer
    {
        /// <summary>
        /// 省份Id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public int DistrctId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 登账号名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 锁定状态
        /// </summary>
        public int LockState { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 公司类型(1开发商 2代理商)
        /// </summary>
        public int Type { get; set; }

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
        
        private List<SelectListItem> _Districts = new List<SelectListItem>() { new SelectListItem() { Text = "选择区域", Value = "0" } };
        /// <summary>
        /// 区域绑定数据
        /// </summary>
        public List<SelectListItem> Districts
        {
            get { return _Districts; }
            set { _Districts = value; }
        }

        #endregion

        /// <summary>
        /// 锁定状态下拉列表
        /// </summary>
        public List<SelectListItem> LockStateList { get; set; }
        /// <summary>
        /// 公司类型下拉列表
        /// </summary>
        public List<SelectListItem> CompanyTypeList { get; set; }

    }
}
