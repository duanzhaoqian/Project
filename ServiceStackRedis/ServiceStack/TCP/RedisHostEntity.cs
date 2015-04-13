using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.TCP
{
    public class RedisHostEntity
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Key { get; set; }
        //public int DB { get; set; }
        //public string Connections { get; set; }
    }
}
