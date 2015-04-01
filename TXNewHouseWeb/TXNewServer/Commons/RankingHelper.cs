using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TXCommons.Index;

namespace Commons
{
    public class RankingHelper
    {
        /// <summary>
        /// 设置排行全局词典
        /// </summary>
        public static void SetDictionary(IEnumerable<IndexRanking> indexRanking)
        {
            DictionaryManager<IEnumerable<IndexRanking>>.dictionary.Add("ranking", indexRanking);
        }
    }
}
