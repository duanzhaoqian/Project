using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using Directory = Lucene.Net.Store.Directory;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using FSDirectory = Lucene.Net.Store.FSDirectory;
using IndexWriter = Lucene.Net.Index.IndexWriter;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using System.Threading;
using ServiceStack;

namespace TXSearchService
{
    public class LuceneOpt
    {
        /// <summary>
        /// 商品目录
        /// </summary>
        public string GoodsIndexPath { get; set; }



        internal void AddDocForLongHouse(string houseid)
        {
            throw new NotImplementedException();
        }

        internal void DeleteHouseDoc(string houseid, string p, string GoodsIndexPath)
        {
            throw new NotImplementedException();
        }
    }
}
