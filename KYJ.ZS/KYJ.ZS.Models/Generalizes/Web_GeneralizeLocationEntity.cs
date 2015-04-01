using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Generalizes
{
    /// <summary>
    /// 作者:邓福伟
    /// 时间：2014-05-16
    /// 描述：推广位置
    /// </summary>
    public class Web_GeneralizeLocationEntity
    {
        /// <summary>
        /// 推广位置Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 推广位置名称
        /// </summary>
        public string GLocationName { get; set; }
        /// <summary>
        /// 推广位置商品数
        /// </summary>
        public int GLocationNum { get; set; }
    }
}
