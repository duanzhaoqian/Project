
namespace TXModel.Web
{
    public class PremisesCompare
    {
        /// <summary>
        /// 楼盘Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public string InnerCode { get; set; }

        /// <summary>
        /// 参考均价
        /// </summary>
        public decimal ReferencePrice { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string DName { get; set; }

        /// <summary>
        /// 环线位置（1一环，2二环，3三环，4四环，5五环，6六环）
        /// </summary>
        public int Ring { get; set; }

        /// <summary>
        /// 商圈名称
        /// </summary>
        public string BName { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string PremisesAddress { get; set; }

        /// <summary>
        /// 开发商
        /// </summary>
        public string Developer { get; set; }

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
        /// 建筑类别（1板楼，2塔楼，3砖楼，4砖混，5平房，6钢混）
        /// </summary>
        public int BuildingType { get; set; }
        public string View_BuildingType { get; set; }

        /// <summary>
        /// 项目特色
        /// </summary>
        public string Characteristic { get; set; }

        /// <summary>
        /// 物业类型（1住宅，2写字楼，3别墅，4商业）
        /// </summary>
        public string PropertyType { get; set; }

        /// <summary>
        /// 容积率
        /// </summary>
        public double AreaRatio { get; set; }

        /// <summary>
        /// 物业费
        /// </summary>
        public decimal PropertyPrice { get; set; }

        /// <summary>
        /// 车位信息
        /// </summary>
        public string ParkingLot { get; set; }

        /// <summary>
        /// 绿化率
        /// </summary>
        public double GreeningRate { get; set; }

        /// <summary>
        /// 配套介绍
        /// </summary>
        public string SupportingIntroduction { get; set; }

        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 购物
        /// </summary>
        public string Shopping { get; set; }

        /// <summary>
        /// 医院
        /// </summary>
        public string Hospital { get; set; }

        /// <summary>
        /// 生活
        /// </summary>
        public string Life { get; set; }

        /// <summary>
        /// 娱乐
        /// </summary>
        public string Entertainment { get; set; }

        /// <summary>
        /// 餐饮
        /// </summary>
        public string Catering { get; set; }

        /// <summary>
        /// 公交
        /// </summary>
        public string Bus { get; set; }

        /// <summary>
        /// 地铁
        /// </summary>
        public string Subway { get; set; }
    }
}
