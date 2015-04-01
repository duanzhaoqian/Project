using ServiceStack;

namespace TXCommons.PictureModular
{
    public class RedisLua
    {
        public static readonly string offerPrice02 = ConfigKey("offerPrice02");
        /// <summary>
        /// 写redis
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <returns>true成功，false失败</returns>
        public static bool SetValue<T>(string key, T val)
        {
            return PictureModular.Redis.SetValue<T>(key, val, null, FunctionTypeEnum.BidPrice, 0);
        }

        /// <summary>
        /// 读redis
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回值</returns>
        public static T GetValue<T>(string key)
        {
            return PictureModular.Redis.GetValue<T>(key, FunctionTypeEnum.BidPrice, 0);
        }

        #region tool
        /// <summary>
        /// 取Web.Config值
        /// </summary>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public static string ConfigKey(string KeyName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[KeyName];
        }

        #endregion
    }
}
