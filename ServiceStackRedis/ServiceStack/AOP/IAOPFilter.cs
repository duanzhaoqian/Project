using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.AOP
{
    /// <summary>
    /// AopFilter接口
    /// </summary>
    internal interface IAOPFilter
    {
        /// <summary>
        /// 执行方法时触发的方法
        /// </summary>
        /// <param name="processer"></param>
        void Execute(IProcess processer);
    }
}
