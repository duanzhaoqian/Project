using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Categories;

namespace KYJ.ZS.Models.RentalGoodses
{
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-5
    /// 描述：分类菜单模型
    /// </summary>
    public class RentalGoodsCatMenuEntity
    {
       public List<CatMenuItem> CatMenuItems { get; set; }
    }
    /// <summary>
    /// 作者：wwang
    /// 时间：2014-5-5
    /// 描述：菜单元素类
    /// </summary>
    public class CatMenuItem
    {
        /// <summary>
        /// 二级类节点
        /// </summary>
        public CatResult Channel { get; set; }

        /// <summary>
        /// 三级类节点集合
        /// </summary>
        public List<CatResult> Categories { get; set; }
    }
}
