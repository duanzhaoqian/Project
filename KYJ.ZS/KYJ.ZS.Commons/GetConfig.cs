
namespace KYJ.ZS.Commons
{
    public class GetConfig
    {
        public static string MQIPAddress
        {
            get { return PubConstant.GetConfigString("MQIPAddress"); }
        }

        public static string MQConnectionTimeOut
        {
            get { return PubConstant.GetConfigString("MQConnectionTimeOut"); }
        }

        public static string MQRetryCount
        {
            get { return PubConstant.GetConfigString("MQRetryCount"); }
        }

        public static string MQGoodsPicQueueName
        {
            get { return PubConstant.GetConfigString("MQGoodsPicQueueName"); }
        }
    }
}
