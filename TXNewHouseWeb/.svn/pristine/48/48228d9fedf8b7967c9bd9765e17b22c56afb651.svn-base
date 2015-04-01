using System;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 管理后台摇号活动实体
    /// </summary>
    public class PVM_NH_YaoHao
    {
        #region Activities
        /*Activities活动表*/
        /// <summary>
        /// Activities活动id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Activities经纪人id
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// Activities活动名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Activities活动类型1摇号2限时折扣3排号购房4阶梯团购5竞价6砍价7秒杀8一口价
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// Activities有效报名人数
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// Activities参加活动房源数量
        /// </summary>
        public int HouseCount { get; set; }
        /// <summary>
        /// Activities价格（起拍价格 保底价格 秒杀价格 一口价）
        /// </summary>
        public decimal BidPrice { get; set; }
        /// <summary>
        /// Activities加价幅度
        /// </summary>
        public decimal AddPrice { get; set; }
        /// <summary>
        /// Activities最大幅度
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// Activities保证金
        /// </summary>
        public decimal Bond { get; set; }
        /// <summary>
        /// Activities活动状态 0未开始 1已开始 2已结束
        /// </summary>
        public int ActivitieState { get; set; } 
        /// <summary>
        /// Activities是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// Activities活动开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// Activities活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// Activities修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Activities创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /*             end           */
        #endregion


        #region ActivitiesYaoHaoInfos
        /*ActivitiesYaoHaoInfos 摇号活动信息表*/
        /// <summary>
        /// ActivitiesYaoHaoInfos报名开始时间
        /// </summary>
        public DateTime? SignupBeginTime { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos报名结束时间
        /// </summary>
        public DateTime? SignupEndTime { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos摇号地点
        /// </summary>
        public string ActivitieLocation { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos公证处
        /// </summary>
        public string NotarialOffice { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos选房时间
        /// </summary>
        public DateTime ChooseHouseTime { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos活动介绍
        /// </summary>
        public string Introduction { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos摇号须知
        /// </summary>
        public string Notice { get; set; }
        /// <summary>
        /// ActivitiesYaoHaoInfos摇号流程
        /// </summary>
        public string Process { get; set; }
        /// <summary>
        /// 摇号期数
        /// </summary>
        public int Periods { get; set; }
        /// <summary>
        /// 参与摇号的楼栋编号集合（逗号分隔）
        /// </summary>
        public string BuildingIds { get; set; }
        /*         end            */
        #endregion


        #region ActivitiesHouse
        /// <summary>
        /// ActivitiesHouse摇号的楼盘
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// ActivitiesHouse摇号的楼栋
        /// </summary>
        public int BuildingId { get; set; }
        /// <summary>
        /// ActivitiesHouse摇号的单元
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// ActivitiesHouse摇号的房源
        /// </summary>
        public int HouseId { get; set; }
        /// <summary>
        /// ActivitiesHouse折扣
        /// </summary>
        public int Discount { get; set; }
        /// <summary>
        /// ActivitiesHouse摇号的楼盘Name
        /// </summary>
        public string PremisesName { get; set; }
        /// <summary>
        /// ActivitiesHouse摇号的楼盘Name
        /// </summary>
        public string BuildingName { get; set; }
        /// <summary>
        /// ActivitiesHouse摇号的房源Name
        /// </summary>
        public string HouseName { get; set; }

        #endregion


        #region 
        /// <summary>
        /// 是否为整个楼盘
        /// </summary>
        public bool IsAllPremises { get; set; }

        /// <summary>
        /// 标记状态0 待处理 1 已通过 2 未通过 3未联系 4 已联系 5 已通过并开启报名入口
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplicationTime { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public int CompanyType { get; set; }

        #endregion
        /// <summary>
        /// 申请记录id
        /// </summary>
        public int ActivitiesYaoHaoApplyId { get; set; }

        public string StateStr { get; set; }
        public string ActivitieStateStr { get; set; }
        public string CompanyTypeStr { get; set; }
    }

    public class PVM_NH_YaoHaoApply
    {
        /// <summary>
        /// 摇号申请id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 是否为整个楼盘
        /// </summary>
        public bool IsAllPremises { get; set; }
        /// <summary>
        /// 参与摇号的楼栋编号集合（逗号分隔）
        /// </summary>
        public string BuildingIds { get; set; }
        /// <summary>
        /// 标记状态0 待处理 1 已通过 2 未通过 3未联系 4 已联系 5 已通过并开启报名入口
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string UserMobile { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public int ProvinceID { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 省份id
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 公司类型
        /// </summary>
        public int CompanyType { get; set; }
        /// <summary>
        /// 摇号的楼盘Name
        /// </summary>
        public string PremisesName { get; set; }
        public string StateStr { get; set; }
        public string CompanyTypeStr { get; set; }
    }
}
