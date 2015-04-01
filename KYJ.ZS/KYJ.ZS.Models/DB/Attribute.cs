using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014-04-17
    /// 描述：商品属性实体类
    /// </summary>
    public class Attribute
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
