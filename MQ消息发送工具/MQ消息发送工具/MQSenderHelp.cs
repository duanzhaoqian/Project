using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;

namespace MQ消息发送工具
{
    class MQSenderHelp
    {
        public string MQAddress { get; set; }
        public string MQName { get; set; }

        public void SendTextMsg(string msg)
        {
            using (IMessageProducer messageProducer = GetMessageProducer())
            {

                ITextMessage textMessage = messageProducer.CreateTextMessage(msg);

                messageProducer.Send(textMessage);
            }

        }

        public void SendObjectMsg(object obj)
        {

            using (IMessageProducer messageProducer = GetMessageProducer())
            {

                IObjectMessage objectMessage = messageProducer.CreateObjectMessage(obj);

                messageProducer.Send(objectMessage);
            }

        }

        private IMessageProducer GetMessageProducer()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(new Uri("tcp://" + MQAddress));
            IConnection connection = connectionFactory.CreateConnection();

            ISession session = connection.CreateSession();
               
                    IDestination destination = new ActiveMQQueue(MQName);
                    IMessageProducer
                    messageProducer = session.CreateProducer(destination);
           
                    return messageProducer;

        }
    }
}
