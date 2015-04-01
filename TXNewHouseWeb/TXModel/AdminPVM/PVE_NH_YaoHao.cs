using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVE_NH_YaoHao
    {
        /// <summary>
        /// 活动/申请id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 报名开始时间
        /// </summary>
        public DateTime? ApplyBeginTime { get; set; }
        /// <summary>
        /// 报名结束时间
        /// </summary>
        public DateTime? ApplyEndTime { get; set; }
        /// <summary>
        /// 保证金
        /// </summary>
        public decimal Bond { get; set; }
        /// <summary>
        /// 公证处
        /// </summary>
        public string NotarialOffice { get; set; }
        /// <summary>
        /// 选房时间
        /// </summary>
        public DateTime LectHouseTime { get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        public string ActivitiesIntroduction { get; set; }
        /// <summary>
        /// 摇号须知
        /// </summary>
        public string ActivitiesNotice { get; set; }
        /// <summary>
        /// 摇号流程
        /// </summary>
        public string ActivitiesProcess { get; set; }

        /// <summary>
        /// 公证处列表
        /// </summary>
        public List<SelectListItem> NotarialOfficeList { get; set; }
    }
}
