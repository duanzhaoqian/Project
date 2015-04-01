using System.Collections;
using System.Collections.Generic;

namespace TXCommons.MsgQueue
{
    public class SendMessage
    {
        /// <summary>
        ///  发送新房楼盘索引消息
        /// </summary>
        /// <param name="type">新增:insert,更新:update,删除:delete</param>
        /// <param name="premisesId">楼盘Id</param>
        public static void SendPremisesIndexMessage(string type, int premisesId, int cityid)
        {
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            string sendMessage = type + "," + premisesId + "," + cityid;
            string mes = MQHelp.GetMqNameByCityId(cityid.ToString());
            //TXCommons.PictureModular.Tool.WriteTextToFile("mes:" + mes + ",cityid:" + cityid + "|", "E:\\log\\1.txt", true);
            sender.SetQueueName(mes);
            sender.Send(sendMessage);
        }
        /// <summary>
        ///  发送房源户型索引消息
        /// </summary>
        /// <param name="type">新增:insert,更新:update,删除:delete</param>
        /// <param name="id">楼盘ID/房源ID</param>
        /// <param name="cityid">城市ID</param>
        public static void SendRoomIndexMessage(string type, int id, int cityid)
        {
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            string sendMessage = type + "," + id + "," + cityid;
            string mes = MQHelp.GetRoomMqNameByCityId(cityid.ToString());
            sender.SetQueueName(mes);
            sender.Send(sendMessage);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="guid">房源guid</param>
        /// <param name="type">新房:Premises</param>
        /// <param name="opertype">图片类型</param>
        /// <param name="houseid">房源Id</param>
        /// <param name="cityId">城市ID</param>
        public static void SendPictureMessage(string guid, string type, string opertype, string houseid, int cityId)
        {
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            sender.SetQueueName(TXCommons.GetConfig.MQPremisesPicQueueName);
            Dictionary<string, object> sendMessage = new Dictionary<string, object>();
            sendMessage.Add("MethodType", 1);
            ArrayList arr = new ArrayList();
            arr.Add(guid);
            arr.Add(type);
            arr.Add(houseid);
            arr.Add(opertype);
            arr.Add(cityId.ToString());
            arr.Add(false);
            arr.Add(false);
            sendMessage.Add("MethodParameter", arr);
            sender.Send(sendMessage);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="guid">房源guid</param>
        /// <param name="type">新房:Premises</param>
        /// <param name="opertype">图片类型</param>
        /// <param name="houseid">房源Id</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="fullpath">图片全路径</param>
        public static void SendPictureMessage(string guid, string type, string opertype, string houseid, int cityId, string fullpath)
        {
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            sender.SetQueueName(TXCommons.GetConfig.MQPremisesPicQueueName);
            Dictionary<string, object> sendMessage = new Dictionary<string, object>();
            sendMessage.Add("MethodType", 1);
            ArrayList arr = new ArrayList();
            arr.Add(guid);
            arr.Add(type);
            arr.Add(houseid);
            arr.Add(opertype);
            arr.Add(cityId.ToString());
            arr.Add(false);
            arr.Add(false);
            arr.Add(fullpath);
            sendMessage.Add("MethodParameter", arr);
            sender.Send(sendMessage);
        }
        /// <summary>
        /// 发送删除图片消息
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="type"></param>
        /// <param name="opertype"></param>
        /// <param name="houseid"></param>
        /// <param name="cityId"></param>
        public static void SendDeletePictureMessage(string guid, string type, string opertype, string houseid, int cityId)
        {
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            sender.SetQueueName(TXCommons.GetConfig.MQPremisesPicQueueName);
            Dictionary<string, object> sendMessage = new Dictionary<string, object>();
            sendMessage.Add("MethodType", 2);
            ArrayList arr = new ArrayList();
            arr.Add(guid);
            arr.Add(type);
            arr.Add(houseid);
            arr.Add(opertype);
            arr.Add(cityId.ToString());
            arr.Add(true);
            arr.Add(false);
            sendMessage.Add("MethodParameter", arr);
            sender.Send(sendMessage);
        }


        /// <summary>
        /// 发送400电话新增或修改消息
        /// </summary>
        /// <param name="PremiseId">楼盘ID</param>
        /// <param name="LoginName">用户登录名</param>
        /// <param name="CompanyName">公司名称</param>
        /// <param name="RegPeople">联系人姓名</param>
        /// <param name="RegPeopleTel">联系人电话</param>
        /// <param name="RegPeopleEMail">联系人信箱</param>
        /// <param name="ExtNumber">联系人电话分机号</param>
        public static void Send400TelMessage(string LoginName, int PremiseId, string CompanyName, string RegPeople, string RegPeopleTel, string RegPeopleEMail, string ExtNumber, int CityId)
        {
            if (PremiseId <= 0)
            {
                return;
            }
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            sender.SetQueueName(TXCommons.GetConfig.MQUserInfoCallQueueName);
            Dictionary<string, object> sendMessage = new Dictionary<string, object>();
            sendMessage.Add("Type", "upsert");//新增、修改:upsert
            sendMessage.Add("LoginName", LoginName);
            sendMessage.Add("CompanyName", CompanyName);
            sendMessage.Add("RegPeople", RegPeople);
            sendMessage.Add("RegPeopleTel", RegPeopleTel);
            sendMessage.Add("RegPeopleEMail", RegPeopleEMail);
            sendMessage.Add("ExtNumber", ExtNumber);
            sendMessage.Add("PremiseId", PremiseId);
            sendMessage.Add("CityId", CityId);
            sender.Send(sendMessage);
        }
        /// <summary>
        /// 发送注销400的MQ
        /// </summary>
        /// <param name="LoginName">登录名称</param>
        /// <param name="PremiseId">楼盘ID</param>
        public static void Remove400TelMessage(string LoginName, int PremiseId, int CityId)
        {
            if (PremiseId <= 0)
            {
                return;
            }
            TXCommons.MsgQueue.Sender sender = new TXCommons.MsgQueue.Sender();
            sender.SetQueueName(TXCommons.GetConfig.MQUserInfoCallQueueName);
            Dictionary<string, object> sendMessage = new Dictionary<string, object>();
            sendMessage.Add("Type", "delete");//注销400:delete
            sendMessage.Add("LoginName", LoginName);
            sendMessage.Add("PremiseId", PremiseId);
            sendMessage.Add("CityId", CityId);
            sender.Send(sendMessage);
        }
    }
}
