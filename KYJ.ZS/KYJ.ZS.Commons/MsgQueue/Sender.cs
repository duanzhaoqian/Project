using System;
using System.Collections.Generic;
using Apache.NMS;

namespace KYJ.ZS.Commons.MsgQueue
{
    public class Sender
    {
        private string _queuename = string.Empty;
        public Sender()
        {
            MQ.ProducerConfiguration.IPAddress = GetConfig.MQIPAddress;
            MQ.ProducerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(GetConfig.MQConnectionTimeOut));
            MQ.ProducerConfiguration.RetryCount = Convert.ToInt32(GetConfig.MQRetryCount);
        }

        public void SetQueueName(string name)
        {
            _queuename = name;
        }

        public bool Send(String msg)
        {
            try
            {
                MQ.IQueueProducer queueProducer = MQ.ProducerFactory.CreateQueueProducer(_queuename);
                if (queueProducer.Send(msg, MsgPriority.Normal))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Send(Dictionary<string, object> msg)
        {
            try
            {
                MQ.IQueueProducer queueProducer = MQ.ProducerFactory.CreateQueueProducer(_queuename);
                if (queueProducer.Send(msg, MsgPriority.Normal))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
