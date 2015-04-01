using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Module;

namespace SyncData
{
    public class Process
    {
        public void ProcessData()
        {
            SyncDataModule syncData = new SyncDataModule();//逻辑处理对象
            ListenTime listenTime = new ListenTime();//监听时间对象
            listenTime.ListenedTime += syncData.SyncSQLData;//绑定事件
            listenTime.BeginListenTime();//开始监听
        }
    }
}
