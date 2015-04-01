using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListenRedis
{
    /// <summary>
    /// 请求Redis
    /// </summary>
    public class GetDataFromRedis
    {
        public T Get<T>(string key)
        {
            return KYJ.ServiceStack.Redis.Get<T>(key);
        }
    }
}
