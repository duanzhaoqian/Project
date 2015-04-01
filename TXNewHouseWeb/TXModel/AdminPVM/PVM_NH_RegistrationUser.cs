using System;

namespace TXModel.AdminPVM
{
    public class PVM_NH_RegistrationUser
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivitiesId { get; set; }

        /// <summary>
        /// 出价人姓名
        /// </summary>
        public string BidUserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IDCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 出价人qq号码
        /// </summary>
        public string BidUserQQ { get; set; }

        /// <summary>
        /// 出价人邮箱
        /// </summary>
        public string BidUserMail { get; set; }

        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremiseName { get; set; }

        /// <summary>
        /// 楼栋名称
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// 单元名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 所在楼层数
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public string HouseNum { get; set; }

        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        public string CreateTimeString { get; set; }

        /// <summary>
        /// 报名号
        /// </summary>
        public long RegNum { set; get; }
    }
}
