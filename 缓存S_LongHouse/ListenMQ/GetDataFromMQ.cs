using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using Command;

namespace ListenMQ
{

    public class GetDataFromMQ
    {
        private string _mqName;
        /// <summary>
        /// 监听MQ消息
        /// </summary>
        public void BeginListen()
        {
            MQ.ConsumerConfiguration.IPAddress = Config.MQIPAddress;
            MQ.ConsumerConfiguration.ConnectionTimeOut =new TimeSpan(0, 0, Convert.ToInt32(Config.MQConnectionTimeOut));
            MQ.ConsumerConfiguration.RetryCount = Convert.ToInt32(Config.MQRetryCount);
            _mqName = Config.MQName;
            Thread thread = new Thread(() =>
            {
                MQ.IQueueConsumer queueConsumer = MQ.ConsumerFactory.CreateQueueConsumer(_mqName);
                while (true)
                {
                    string str = queueConsumer.ReceiveTextMessage();
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (Listened != null)
                        {
                            Listened.Invoke(str);
                        }
                    }
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        public event Action<string> Listened;
    }
}
