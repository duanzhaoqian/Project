using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListenMQ;
using Module;

namespace GetMQMessage
{
    public class Process
    {
        public void ProcessData()
        {
            CacheDataModule getMqMessageService = new CacheDataModule(); //逻辑处理对象
            ListenMQ.GetDataFromMQ getDataFromMq = new GetDataFromMQ();//监听MQ对象
            getDataFromMq.Listened += getMqMessageService.ProcessMQData<Model.S_LongHouseCache>;//绑定监听方法
            getDataFromMq.BeginListen();//开始监听
        }
    }
}
