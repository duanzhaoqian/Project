using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SyncData
{
    /// <summary>
    /// 监听时间
    /// </summary>
    public class ListenTime
    {
        public event Action ListenedTime;

        public void BeginListenTime()
        {
            bool isDo = false;
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    if (DateTime.Now.Hour == 2 && DateTime.Now.Minute == 0 && !isDo)
                    {
                        if (ListenedTime != null)
                        {
                            //触发绑定的方法
                            ListenedTime();
                            isDo = true;
                        }
                    }
                    if (DateTime.Now.Hour == 1 && DateTime.Now.Minute == 0)
                    {
                        isDo = false;
                    }
                }
            });
        }
    }
}
