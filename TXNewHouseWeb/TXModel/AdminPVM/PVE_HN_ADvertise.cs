using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVE_HN_ADvertise
    {
        /// <summary>
        /// 省份Id
        /// </summary>
        public int ProvinceID { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public List<SelectListItem> Provinces { set; get; }

        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }
        public List<SelectListItem> Cityes { get; set; }
        /// <summary>
        /// 楼盘Id
        /// </summary>
        public int PremisesId { get; set; }
        /// <summary>
        /// 楼盘名称
        /// </summary>
        public string PremisesName { set; get; }
        /// <summary>
        /// 广告类型(1热点新盘推荐 2最新楼盘推荐 3精品楼盘 4即将开盘 )
        /// </summary>
        public int Type { get; set; }
        public List<SelectListItem> Types
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem(){ Text="请选择", Value=""},
                    new SelectListItem(){ Text="热点新盘推荐", Value="1"},
                    new SelectListItem(){ Text="最新楼盘推荐", Value="2"},
                    new SelectListItem(){ Text="精品楼盘", Value="3"},
                    new SelectListItem(){ Text="即将开盘", Value="4"}
                };
            }
        }
    }
}
