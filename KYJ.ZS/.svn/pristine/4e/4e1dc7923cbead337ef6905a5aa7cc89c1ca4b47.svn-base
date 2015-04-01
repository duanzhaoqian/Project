using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/4/22 14:52:28
    /// 描述：出租商品实体类
    /// </summary>
    public class RentalGoods
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 出售商品标题
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// 出租数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 租用量
        /// </summary>
        public int RentNum { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int SalesVolume { get; set; }
        /// <summary>
        /// 商家编码
        /// </summary>
        [StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 二维码
        /// </summary>
        [StringLength(50)]
        public string Barcode { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 月租金
        /// </summary>
        public double MonthPrice { get; set; }

        /// <summary>
        /// 押金
        /// </summary>
        public double Deposit { get; set; }
        /// <summary>
        /// 滞纳金
        /// </summary>
        public double LateFees { get; set; }

        /// <summary>
        /// 租期模版ID
        /// </summary>
        public int TtempId { get; set; }

        /// <summary>
        /// 运费模板ID
        /// </summary>
        public int FtempId { get; set; }

        /// <summary>
        /// 储存方式：0 未知，1 立刻，2 放入仓库，3 自定义时间
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 有效期：0 未知，1（1周），2 (1个月)
        /// </summary>
        public int Validity { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// GUID唯一标识
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 显示状态：0 未知，1上架，2下架
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审核状态：0未知，1待审核，2 通过，3未通过
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 商品标签
        /// </summary>
        [StringLength(50)]
        public string Tags { get; set; }

        /// <summary>
        /// 商品重量
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 商品体积
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId { get; set; }

    }
}
