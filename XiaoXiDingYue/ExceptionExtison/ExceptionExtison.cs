using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExceptionExtison
{
    /// <summary>
    /// 自定义异常对象
    /// </summary>
    public class ExceptionExtison : Exception
    {
        /// <summary>
        /// 用于存储出异常的对象
        /// </summary>
        public object ExClient { get; set; }
    }
}
