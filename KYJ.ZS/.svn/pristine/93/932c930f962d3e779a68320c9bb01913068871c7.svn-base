using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Search.Api
{
    using KYJ.ZS.Commons;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.Search;

    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/6/20 9:15:34
    /// 描述：
    /// </summary>
    public class Ini
    {
        public Ini(){ }
        public Ini(string indexPath,IndexType indexType)
        {
            Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            Directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(indexPath));
            
            if(indexType == IndexType.IndexSearcher)
                IndexSearcher = new IndexSearcher(Directory, true);
            else
                IndexWriter = new IndexWriter(Directory, Analyzer, true);
        }

        public Analyzer Analyzer { get; set; }

        public Lucene.Net.Store.Directory Directory { get; set; }

        public IndexSearcher IndexSearcher { get; set; }


        public IndexWriter IndexWriter { get; set; }

        public object condition { get; set; }

    }

    public enum IndexType
    {
        /// <summary>
        /// 读索引
        /// </summary>
        IndexSearcher,
        /// <summary>
        /// 写索引
        /// </summary>
        IndexWriter
    }
}
