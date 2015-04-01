using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TimeTest
{
    /// <summary>
    /// 记录时间
    /// </summary>
    public class Time
    {
        private static Stopwatch _stopwatch;
        /// <summary>
        /// 全局记录时间对象
        /// </summary>
        public static Stopwatch Stopwatch
        {
            get
            {
                if (_stopwatch == null)
                {
                    _stopwatch = new Stopwatch();
                }
                return _stopwatch;
            }
        }
    }
}
