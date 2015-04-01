using System;

namespace TXModel.User
{
    /// <summary>
    /// 我参与的网络摇号
    ///  author:汪敏
    /// </summary>
    public partial class CT_Yaohao
    {

        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivitieId { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremisesName { get; set; }

        /// <summary>
        /// 摇号地点
        /// </summary>
        public string ActivitieLocation { get; set; }
        /// <summary>
        /// 公证处
        /// </summary>
        public string NotarialOffice { get; set; }
        /// <summary>
        /// 参与人数
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// 选房时间
        /// </summary>
        public DateTime ChooseHouseTime { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 报名开始时间
        /// </summary>
        public DateTime SignupBeginTime { get; set; }

        /// <summary>
        /// 报名结束时间
        /// </summary>
        public DateTime SignupEndTime { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivieStatus { get; set; }

        /// <summary>
        /// 保证金
        /// </summary>
        public decimal Bond { get; set; }
    }
}
