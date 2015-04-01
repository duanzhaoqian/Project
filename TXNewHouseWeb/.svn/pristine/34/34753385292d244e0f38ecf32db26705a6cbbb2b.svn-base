using System;
using System.Collections.Generic;

namespace TXModel.AdminPVM
{
    public class PVM_NH_Premises
    {
        /// <summary>
        /// 楼盘Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 开发商Id
        /// </summary>
        public int DeveloperId { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public string InnerCode { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public int DId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string DName { get; set; }

        /// <summary>
        /// 商圈Id
        /// </summary>
        public int BId { get; set; }

        /// <summary>
        /// 商圈名称
        /// </summary>
        public string BName { get; set; }

        /// <summary>
        /// 楼盘对应地铁 Id逗号分隔 
        /// </summary>
        public string TId { get; set; }

        /// <summary>
        /// 楼盘对应地铁(JSON格式)
        /// </summary>
        public string Tid_JsonString { set; get; }

        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参考均价
        /// </summary>
        public decimal ReferencePrice { get; set; }

        /// <summary>
        /// 咨询电话
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        /// 楼盘状态( 0在售  1售罄 )
        /// </summary>
        public int SalesStatus { get; set; }

        public string SalesStatusString { get; set; }

        /// <summary>
        /// 环线位置( 1 一环  2二环 3 三环  4四环  5五环 6六环 )
        /// </summary>
        public int Ring { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string PremisesAddress { get; set; }

        /// <summary>
        /// 售楼地址
        /// </summary>
        public string SalesAddress { get; set; }

        /// <summary>
        /// 开发商
        /// </summary>
        public string Developer { get; set; }

        /// <summary>
        /// 代理商
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 预售许可证
        /// </summary>
        public string SalePermit { get; set; }

        /// <summary>
        /// 预售许可证列表
        /// </summary>
        public List<PVS_NH_Premises_SalePermit> SalePermitList { set; get; }

        /// <summary>
        /// 产权
        /// </summary>
        public string PropertyRight { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildingArea { get; set; }

        /// <summary>
        /// 占地面积
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// 总户数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 项目特色
        /// </summary>
        public string Characteristic { get; set; }

        /// <summary>
        /// 项目特色(列表)
        /// </summary>
        public List<object> PremisesFeatures { get; set; }

        /// <summary>
        /// 项目特色(Json格式)
        /// </summary>
        public string Characteristic_JsonString { set; get; }

        /// <summary>
        /// 建筑类别
        /// </summary>
        public int BuildingType { get; set; }

        /// <summary>
        /// 物业类型
        /// </summary>
        public string PropertyType { get; set; }

        public string PropertyTypeString { get; set; }

        /// <summary>
        /// 容积率
        /// </summary>
        public double AreaRatio { get; set; }

        /// <summary>
        /// 得房率
        /// </summary>
        public double RoomRate { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal PropertyPrice { get; set; }

        /// <summary>
        /// 车位信息
        /// </summary>
        public string ParkingLot { get; set; }

        /// <summary>
        /// 装修程度(1毛坯2简装修、3中等装修、4精装修、5豪华装修)
        /// </summary>
        public int Renovation { get; set; }

        /// <summary>
        /// 物业公司
        /// </summary>
        public string PropertyCompany { get; set; }

        /// <summary>
        /// 绿化率
        /// </summary>
        public double GreeningRate { get; set; }

        /// <summary>
        /// 交通介绍
        /// </summary>
        public string TrafficIntroduction { get; set; }

        /// <summary>
        /// 配套介绍
        /// </summary>
        public string SupportingIntroduction { get; set; }

        /// <summary>
        /// 楼盘介绍
        /// </summary>
        public string PremisesIntroduction { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// 开盘时间
        /// </summary>
        public DateTime OpeningTime { get; set; }

        /// <summary>
        /// 交房时间
        /// </summary>
        public DateTime OthersTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 楼盘搜索实体
        /// </summary>
        public PVS_NH_Premises PvsPremises { get; set; }

        public string LoadingSank { get; set; }

        // 400电话
        public string Phone400 { set; get; }

        // 是否接入400
        public bool IsShow400 { set; get; }

        // 信息来源页面
        public string PageUrl { set; get; }

        // 楼盘信息是否在前台显示
        //public bool IsShow { set; get; }
    }

    /// <summary>
    /// 预售许可证
    /// </summary>
    public class PVS_NH_Premises_SalePermit
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDel { set; get; }
    }
}