using System;
using System.Collections.Generic;
using System.Text;

namespace ServMonitor
{
    public class ServInfoState
    {

        /// <summary>
        /// 本地服务器ID
        /// </summary>
        public string ID;
        /// <summary>
        /// 本地服务器名称
        /// </summary>                        
        public string NAME;
        /// <summary>
        /// 本地服务器类型
        /// </summary>
        public string TYPE;
        /// <summary>
        /// 本地服务器状态
        /// </summary>
        public string STATE;

        public override string ToString()
        {
            return "ID=" + ID + "\n" + "NAME=" + NAME + "\n" + "TYPE=" + TYPE + "\n" + "STATE=" + STATE + "\n";
        }
    }
}
