using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Models
{
    public class ActivityTiming
    {
        public int TimingID { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int CityID { get; set; }
        /// <summary>
        /// 开发商ID
        /// </summary>
        public int DeveloperId { get; set; }
        /// <summary>
        /// 开发商名称
        /// </summary>
        public string DeveloperName { get; set; }

        public int ActivityID { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 楼盘ID
        /// </summary>
        public int PremisesID { get; set; }
        /// <summary>
        /// 房源ID
        /// </summary>
        public int HouseID { get; set; }
        /// <summary>
        /// 房源标题
        /// </summary>
        public string HouseTitle { get; set; }

        public string OperID { get; set; }

        public int TimingType { get; set; }

        public int Status { get; set; }

        public string EndTime { get; set; }
        /// <summary>
        /// 楼栋类型
        /// </summary>
        public string BuildingTypeName { get; set; }
    }
}
