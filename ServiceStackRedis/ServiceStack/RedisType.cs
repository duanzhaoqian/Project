using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ServiceStack;
using System.ComponentModel;

namespace ServiceStack
{
    public enum CityEnum
    {
        BeiJing,
        ShangHai,
        GuangZhou,
        ShenZhen,
        TianJin,
        QingDao,
        HangZhou,
        ChengDu,
        NanJing,
        Dalian,
        [Description("98个新开通城市")]
        Other,
        Default
    }
    public enum FunctionTypeEnum
    {
        [Description("二手房业务缓存")]
        HouseDBData,
        [Description("公共平台数据")]
        WebDBData,
        [Description("二手房首页静态化")]
        HouseIndexStatic,
        [Description("房源图片")]
        HouseImage,
        [Description("用户头象")]
        UserHeadImage,
        [Description("出价")]
        BidPrice,
        [Description("验证码")]
        VerificationCode,
        [Description("预约刷新")]
        AppointmentRefresh,
        [Description("缓存用户信息-60分钟")]
        UserInfo,
        [Description("保存文件")]
        DocumentInfo,
        [Description("小区图片")]
        VillageImage,
        [Description("浏览次数")]
        ViewCount,
        [Description("自增列")]
        Identity,
        [Description("房源临时图片")]
        HouseImageTemp,
        [Description("快宝支付")]
        KBZF,
        [Description("浏览记录")]
        ViewHistory,
        [Description("广告位")]
        ADPosition,
        [Description("活动相关")]
        Activities,
        [Description("新闻资讯")]
        NewsAndInformation,
        [Description("首页楼盘信息")]
        IndexPremises,
        [Description("房模图片")]
        HouseModel,
        //新房
        [Description("新房验证码")]
        NewHouseVerificationCode,
        [Description("楼盘主页轮播图图片获取")]
        NewHouseCarouselFigure,
        [Description("楼盘户型图")]
        NewHouseStatisticalunits,
        [Description("楼盘相册")]
        NewHousePropertyAlbum,
        [Description("新房用户信息缓存")]
        NewHouseUserInfo,
        [Description("新房房源浏览量")]
        NewHouseViewCount,
        [Description("新房基础数据图片")]
        NewHouseBasicDataPicture,
        [Description("新房楼盘最新图片")]
        NewHousePropertyNewPiceture,
        [Description("新房楼盘关注数量")]
        NewHouseFocusPropertyCount,
        [Description("新房房源关注数量")]
        NewHouseFocusHouseCount,
        [Description("新房户型图浏览数量")]
        NewHouseSizeChartViewCount,
        [Description("新房营销活动房源推荐")]
        NewHouseRelatedRecommendations,
        [Description("新房楼盘排名")]
        NewHousePropertyRank,
        [Description("新房楼盘特色")]
        NewHousePropertyFeatures,
        //快有贷
        [Description("快有贷验证码")]
        KYDVerificationCode,
        Test,
        //租售
        [Description("租售购物车")]
        ZuShouCar,
        [Description("租售验证码")]
        ZuShouVerificationCode,
        [Description("租售用户信息")]
        ZuShouUserInfo,
        //公共基础数据
        [Description("公共基础数据")]
        BaseData,
        //金点子列表缓存
        [Description("金点子列表缓存")]
        EssenceOpinionWin
    }



    /// <summary>
    /// Author:zhuzh
    /// Date:2014-04-28
    /// Desc:Redis的FunctionType的缩减版
    /// </summary>
    public enum FunctionType
    {
        [Description("租售购物车")]
        ZuShouCar = 31,
        [Description("租售验证码")]
        ZuShouVerificationCode = 32,
        [Description("租售用户信息")]
        ZuShouUserInfo = 33,
        [Description("租售商品图片")]
        ZuShouGoodsPicture = 34,
        [Description("租售用户资料图片")]
        ZuShouPapersPicture = 35,
        [Description("租售品牌图片")]
        ZuShouBrandPicture = 36,
        [Description("租售浏览量")]
        ZuShouBrowseAmount = 37,
        [Description("租售浏览历史")]
        ZuShouBrowseHistory = 38,
        [Description("租售辅助记录")]
        ZuShouCommon = 39,
        [Description("租售广告图片")]
        ZuShouAdvertisement = 40
    }

}
