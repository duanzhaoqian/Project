using System;
using System.Collections.Generic;
using Apache.NMS;
using Log4netService;
namespace TXCommons.MsgQueue
{
    public class Sender
    {
        private string queuename = string.Empty;
        public Sender()
        {
            MQ.ProducerConfiguration.IPAddress = GetConfig.MQIPAddress;
            MQ.ProducerConfiguration.ConnectionTimeOut = new TimeSpan(0, 0, Convert.ToInt32(GetConfig.MQConnectionTimeOut));
            MQ.ProducerConfiguration.RetryCount = Convert.ToInt32(GetConfig.MQRetryCount);
        }

        public void SetQueueName(string name)
        {
            queuename = name;
        }

        public bool Send(String msg)
        {
            try
            {
                MQ.IQueueProducer queueProducer = MQ.ProducerFactory.CreateQueueProducer(queuename);
                if (queueProducer.Send(msg, MsgPriority.Normal))
                {
                    return true;
                }
                else
                {
                    RecordLog.RecordException("宋德华", "Sender.Send [" + queueProducer.ErrorMessage + "]");
                    return false;
                }
            }
            catch(Exception ex)
            {   
                RecordLog.RecordException("宋德华","Sender.Send(catch)", ex);
                return false;
            }
        }

        public bool Send(Dictionary<string, object> msg)
        {
            try
            {
                MQ.IQueueProducer queueProducer = MQ.ProducerFactory.CreateQueueProducer(queuename);
                if (queueProducer.Send(msg, MsgPriority.Normal))
                {
                    return true;
                }
                else
                {
                    RecordLog.RecordException("宋德华", "Sender.Send [" + queueProducer.ErrorMessage+"]");
                    return false;
                }
            }
            catch(Exception ex)
            {
                RecordLog.RecordException("宋德华", "Sender.Send(catch)", ex);
                return false;
            }
        }
    }
}
