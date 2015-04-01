using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Tags
{
    /// <summary>
    /// Author：ningjd
    /// Time：2014-05-27
    /// Desc：标签管理Entity
    /// </summary>
    public class TagEntity
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 信息数
        /// </summary>
        public int GoodsCount { get; set; }
    }
}
