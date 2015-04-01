using System.Collections.Generic;

namespace TXCommons.LuceneSearch
{
    public class SearchNHouseResultInfo
    {
        /// <summary>
        /// 房源种类：租房、二手房
        /// </summary>
        public string SearchType { get; set; }
        /// <summary>
        /// 房源类型：1、报价、2砍价、3竞价
        /// </summary>
        public string PType { get; set; }
        /// <summary>
        /// 发房源人类型（0房东，1经纪人）
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// 房源Id
        /// </summary>
        public string Id { get; set; }
        public string UId { get; set; }
        public string TId { get; set; }
        public string PId { get; set; }
        /// <summary>
        /// 城市Id
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 城市拼音
        /// </summary>
        public string CityPY { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 物业类型
        /// </summary>
        public string PropertyType { get; set; }
        /// <summary>
        /// 房屋类型
        /// </summary>
        public string HouseType { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string DName { get; set; }
        /// <summary>
        /// 区域名称拼音
        /// </summary>
        public string DNamePY { get; set; }
        /// <summary>
        /// 商圈名称
        /// </summary>
        public string BName { get; set; }
        /// <summary>
        /// 商圈名称拼音
        /// </summary>
        public string BNamePY { get; set; }
        /// <summary>
        /// 小区名称
        /// </summary>
        public string VName { get; set; }
        /// <summary>
        /// 租赁方式（1整套出租、2单间出租）
        /// </summary>
        public string RentType { get; set; }
        /// <summary>
        /// 室
        /// </summary>
        public string Room { get; set; }
        /// <summary>
        /// 厅
        /// </summary>
        public string Hall { get; set; }
        /// <summary>
        /// 房屋面积
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 配套设施
        /// </summary>
        public string Facilities { get; set; }
        /// <summary>
        /// 装修程度
        /// </summary>
        public string Renovation { get; set; }
        /// <summary>
        /// 朝向
        /// </summary>
        public string Orientation { get; set; }
        /// <summary>
        /// 第几层
        /// </summary>
        public string TheFloar { get; set; }
        /// <summary>
        /// 共几层
        /// </summary>
        public string AllFloar { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 每平米的价格（二手房）
        /// </summary>
        public string PerPrice { get; set; }
        /// <summary>
        /// 房源标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否唯一（针对二手房）
        /// </summary>
        public string IsUnique { get; set; }
        /// <summary>
        /// 是否免税（针对二手房）
        /// </summary>
        public string IsTxfree { get; set; }
        /// <summary>
        /// 是否满5年（针对二手房）
        /// </summary>
        public string IsFullYears { get; set; }
        /// <summary>
        /// 建筑结构（1平层、2复式、3跃层、4开间、5错层）
        /// </summary>
        public string BuildingStructure { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }
        /// <summary>
        /// 合同寄送地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 出价状态出价状态（0待出价, 1出价中，2已成交，3已过期）
        /// </summary>
        public string BidStatus { get; set; }
        /// <summary>
        /// 出价状态
        /// </summary>
        public string BidStatusOrder { get; set; }
        /// <summary>
        /// 出价次数
        /// </summary>
        public string BidCount { get; set; }
        /// <summary>
        /// 出价开始时间/砍价起始时间
        /// </summary>
        public string BidStartTime { get; set; }
        /// <summary>
        /// 出价结束时间/砍价结束时间
        /// </summary>
        public string BidEndTime { get; set; }
        /// <summary>
        /// 房源发布时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 卧室图/室内图
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// 客厅图/户型图
        /// </summary>
        public string LivingPictureUrl { get; set; }


        /// <summary>
        /// 第三张图
        /// </summary>
        public string HousePicUrl { get; set; }
        /// <summary>
        /// 房东报价/经纪人报价/砍价/竞价
        /// </summary>
        public string Price { get; set; }
        ///// <summary>
        ///// 经纪人报价
        ///// </summary>
        //public string BrokerPrice { get; set; }
        /// <summary>
        /// 经纪公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 最高出价
        /// </summary>
        public string MaxPrice { get; set; }
        /// <summary>
        /// 起拍价
        /// </summary>
        public string MinPrice { get; set; }
        /// <summary>
        /// 允许报价最大值
        /// </summary>
        public string QuotedMaxPrice { get; set; }
        /// <summary>
        /// 允许报价最小值
        /// </summary>
        public string QuotedMinPrice { get; set; }


        /// <summary>
        /// 房东真实姓名/经纪人真实姓名
        /// </summary>
        public string UName { get; set; }
        /// <summary>
        /// 登录名（用户或经纪人）
        /// </summary>
        public string LoginName { get; set; }


        /// <summary>
        /// 协议达成价/中标价格
        /// </summary>
        public string BidPrice { get; set; }

        /// <summary>
        /// 出价人手机号
        /// </summary>
        public string BidUserMobile { get; set; }

        /// <summary>
        /// 房东手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 出价人姓名/房客
        /// </summary>
        public string BidUserName { get; set; }
        /// <summary>
        /// 是否一口价（0或1）
        /// </summary>
        public string IsOnePrice { get; set; }
        /// <summary>
        /// 报价房状态（无标签、一口价、竞价、砍价）
        /// </summary>
        public string PTypeState { get; set; }
        /// <summary>
        /// 是否允许议价（0：不允许，1:  允许竞价，2:  允许砍价）
        /// </summary>
        public string IsBargaining { get; set; }
        /// <summary>
        /// 是否免佣金（0或1），只在经纪人房源出现
        /// </summary>
        public string IsAgency { get; set; }
        /// <summary>
        /// 是否支付保证金（0或1）
        /// </summary>
        public string IsGuarantee { get; set; }
        /// <summary>
        /// 是否是东东优家
        /// </summary>
        public string IsSVip { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string BuildingArea { get; set; }
        /// <summary>
        /// 参与人数（砍价王）
        /// </summary>
        public string BidUserNum { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 是否多图（0或1）
        /// </summary>
        public string IsMorePic { get; set; }

        /// <summary>
        /// 图片类型（1:卧室第二张图，2:客厅第二张图, 3:小区图, 4:户型图）
        /// </summary>
        public string PicType { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public string IsRecommend { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public string HouseScore { get; set; }

        /// <summary>
        /// 是否真实房源
        /// </summary>
        public string IsReal { get; set; }

        /// <summary>
        /// 姓别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 是否优秀经纪人 1是 0否
        /// </summary>
        public string IsExcellent { get; set; }
        /// <summary>
        ///  是否返现
        /// </summary>
        public string IsReturn { get; set; }
        /// <summary>
        /// 电子信箱
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// qq号码
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// 是否允许显示
        /// 0：不允许
        /// 1：允许显示手机号
        /// 2：允许显示QQ
        /// 3：允许显示Email
        /// 4: 允许显示微信
        /// 多选用逗号分隔
        /// </summary>
        public string IsAllowShow { get; set; }
        /// <summary>
        /// 是否急售
        /// 0：否
        ///1：未审核
        ///2：已通过
        /// </summary>
        public string IsWorry { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public string ClickCount { get; set; }

    }


    public class SearchRentHouseResultInfoList
    {
        public List<SearchNHouseResultInfo> list
        {
            get;
            set;
        }

        /// <summary>
        /// search:搜索结果  new:最新房源  none:暂无房源
        /// </summary>
        public string Type
        {
            get;
            set;
        }


    }
}
