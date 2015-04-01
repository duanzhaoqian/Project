using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace DicManager
{
    /// <summary>
    /// 全局字典，存储订阅信息
    /// </summary>
    public class SubInfo
    {
        private static Dictionary<string, Action<RelData>> _subDictionary;
        /// <summary>
        /// 全局字典，存储订阅信息
        /// </summary>
        public static Dictionary<string, Action<RelData>> SubInfoDictionary
        {
            get
            {
                if (_subDictionary == null)
                {
                    _subDictionary = new Dictionary<string, Action<RelData>>();
                }
                return _subDictionary;
            }
        }

    }
}
