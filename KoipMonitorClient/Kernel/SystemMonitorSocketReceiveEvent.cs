using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel
{
    public delegate void SystemMonitorSocketReceiveEvent(StateObject sender);
    /// <summary>
    /// 连接数据统计事件
    /// </summary>
    /// <param name="sender"></param>
    public delegate void ConnectCountEvent(StateObject sender);
}
