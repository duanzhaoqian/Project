using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;

namespace MongoService
{
    [ServiceContract]
    public interface IMongoHouseService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/get/{id}",ResponseFormat = WebMessageFormat.Json)]
        [Description("获取指定ID的数据，返回格式为XML")]
        [ServiceKnownType(typeof(XElement))]
        [ServiceKnownType(typeof(Message))]
        Message Get(string id);
        [OperationContract]
        [Description("删除指定ID的数据")]
        [WebGet(UriTemplate = "/delete/{id}", ResponseFormat = WebMessageFormat.Json)]
        bool Delete(string id);
        [OperationContract]
        [Description("更新指定ID的数据")]
        [WebGet(UriTemplate = "/upsert/{id}", ResponseFormat = WebMessageFormat.Json)]
        bool UpSert(string id);
    }
}
