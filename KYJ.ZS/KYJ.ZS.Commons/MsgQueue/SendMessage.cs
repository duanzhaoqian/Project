using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.MsgQueue
{
    public class SendMessage
    {
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="guid">房源guid</param>
        /// <param name="pictureType">图片类型</param>
        /// <param name="goodsid">房源Id</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="fullpath">图片全路径</param>
        public static void SendPictureMessage(string guid, string pictureType, string goodsid, string fullpath)
        {
            Sender sender = new Sender();
            sender.SetQueueName(GetConfig.MQGoodsPicQueueName);
            Dictionary<string, object> sendMessage = new Dictionary<string, object>();
            sendMessage.Add("MethodType", 1);
            ArrayList arr = new ArrayList();
            arr.Add(guid);
            arr.Add(goodsid);
            arr.Add(pictureType);
            arr.Add(fullpath);
            arr.Add(false);
            arr.Add(false);
            
            sendMessage.Add("MethodParameter", arr);
            sender.Send(sendMessage);
        }
    }
}
