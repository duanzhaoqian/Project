using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    /// <summary>
    /// 订阅消息数据格式
    /// </summary>
    public class SubData
    {
        public string Type { get; set; }
        public string[] SubStrings { get; set; }
    }
}
