using System.Collections.Generic;
using KYJ.ZS.Models.DB;
using System;
using System.ComponentModel.DataAnnotations;

namespace KYJ.ZS.Models.FreightTemplates
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-17
    /// Desc：运费模板自定义类
    /// </summary>
    public class FreightTemplateEntity
    {
        /// <summary>
        /// 运费模板
        /// </summary>
        public FreightTemplate FreightTemplate { get; set; }

        /// <summary>
        /// 运费计算集合
        /// </summary>
        public List<FreightCost> FreightCostList { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-24
    /// Desc：运费首页列表Model
    /// </summary>
    public class FreightIndexEntity
    {
        /// <summary>
        /// 运费模板ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 运费模版标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发货城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 是否包邮
        /// </summary>
        public bool IsFreeship { get; set; }

        /// <summary>
        /// 是否支持快递
        /// </summary>
        public bool IsExpress { get; set; }

        /// <summary>
        /// 是否支持EMS
        /// </summary>
        public bool IsEms { get; set; }

        /// <summary>
        /// 是否支持平邮
        /// </summary>
        public bool IsSnailmail { get; set; }

        /// <summary>
        /// 是否支持物流
        /// </summary>
        public bool IsLogistics { get; set; }

        /// <summary>
        /// 计价方式：0 未知，1 按件数，2 按重量，3 按体积
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 运费计算列表
        /// </summary>
        public IList<FreightCostIndexEntity> FreightCostList { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-24
    /// Desc：运费计算列表Model
    /// </summary>
    public class FreightCostIndexEntity
    {
        /// <summary>
        /// 运费计算ID
        /// </summary>
        public int CostId { get; set; }

        /// <summary>
        /// 运费类型：0 未知，1 快递，2 EMS， 3 平邮，4 物流
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 运费使用城市（格式 北京ID,上海ID,深圳ID）
        /// </summary>
        public string CityIds { get; set; }

        /// <summary>
        /// 运费首件
        /// </summary>
        public decimal FirstIme { get; set; }

        /// <summary>
        /// 首件价格
        /// </summary>
        public decimal FirstPrice { get; set; }

        /// <summary>
        /// 运费续件
        /// </summary>
        public decimal ContinuetIme { get; set; }

        /// <summary>
        /// 续件价格
        /// </summary>
        public decimal ContinuePrice { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-29
    /// Desc：运费模板Json
    /// </summary>
    public class FreightJsonEntity
    {
        /// <summary>
        /// 运费模板ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [Required(ErrorMessage = "请重新登陆")]
        public int MerchantId { get; set; }

        /// <summary>
        /// 运费模版标题
        /// </summary>
        [Required(ErrorMessage = "请输入模板名称")]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        [Required(ErrorMessage = "请选择省份")]
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        [Required(ErrorMessage = "请选择省份")]
        [StringLength(50)]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        [Required(ErrorMessage = "请选择城市")]
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        [Required(ErrorMessage = "请选择城市")]
        [StringLength(50)]
        public string CityName { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        [Required(ErrorMessage = "请选择区域")]
        public int DistrictId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [Required(ErrorMessage = "请选择区域")]
        [StringLength(50)]
        public string DistrictName { get; set; }

        /// <summary>
        /// 是否包邮
        /// </summary>
        [Required(ErrorMessage = "请选择是否包邮")]
        public bool IsFreeship { get; set; }

        /// <summary>
        /// 计价方式：0 未知，1 按件数，2 按重量，3 按体积
        /// </summary>
        [Required(ErrorMessage = "请选择计价方式")]
        public int Mode { get; set; }

        /// <summary>
        /// 运送方式：0 未知，1 快递，2 EMS， 3 平邮，4 物流
        /// </summary>
        [Required(ErrorMessage = "请选择运送方式")]
        public int Type { get; set; }

        /// <summary>
        /// 运送费用集合
        /// </summary>
        public List<FreightCostJsonEntity> CostList { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-04-29
    /// Desc：运送费用Json
    /// </summary>
    public class FreightCostJsonEntity
    {
        /// <summary>
        /// 运费使用城市（格式 北京ID,上海ID,深圳ID）
        /// </summary>
        [Required(ErrorMessage = "请选择运费使用城市")]
        [StringLength(1000)]
        public string CityIds { get; set; }

        /// <summary>
        /// 运费首件
        /// </summary>
        [Required(ErrorMessage = "请输入运费首件")]
        [RegularExpression(@"[1-9]\d*")]
        public decimal FirstIme { get; set; }

        /// <summary>
        /// 首件价格
        /// </summary>
        [Required(ErrorMessage = "请输入首件价格")]
        [RegularExpression(@"[0-9]+\.{0,1}[0-9]{0,2}")]
        public decimal FirstPrice { get; set; }

        /// <summary>
        /// 运费续件
        /// </summary>
        [Required(ErrorMessage = "请输入运费续件")]
        [RegularExpression(@"[1-9]\d*")]
        public decimal ContinuetIme { get; set; }

        /// <summary>
        /// 续件价格
        /// </summary>
        [Required(ErrorMessage = "请输入续件价格")]
        [RegularExpression(@"[0-9]+\.{0,1}[0-9]{0,2}")]
        public decimal ContinuePrice { get; set; }
    }

    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-06
    /// Desc：运费模板下拉列表
    /// </summary>
    public class FreightSelectEntity
    {
        /// <summary>
        /// 模板ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }
    }
}
