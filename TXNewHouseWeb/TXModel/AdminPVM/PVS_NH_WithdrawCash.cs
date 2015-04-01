using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// PageViewSearch WithdrawCash
    /// Author: liyuzhao
    /// </summary>
    public class PVS_NH_WithdrawCash
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 用户类型(1:个人 2:经纪人 3:开发商)
        /// </summary>
        public int UserType { set; get; }

        /// <summary>
        /// 用户类型列表
        /// </summary>
        private List<SelectListItem> _userTypes = new List<SelectListItem>();

        /// <summary>
        /// 用户类型
        /// </summary>
        public List<SelectListItem> UserTypes
        {
            get { return _userTypes; }
            set { _userTypes = value; }
        }
    }
}