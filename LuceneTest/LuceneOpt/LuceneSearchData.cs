using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Command;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Function;
using Lucene.Net.Store;
using TXOrm;
using Version = Lucene.Net.Util.Version;

namespace LuceneOpt
{
    public class LuceneSearchData
    {

        public List<S_LongHouseBase> Seclect(string key)
        {
            List<S_LongHouseBase> list = new List<S_LongHouseBase>();

            FSDirectory deDirectory =
                FSDirectory.Open(
                    new DirectoryInfo(@"D:\Visual Studio 2010\Projects\LuceneTest\LuceneDataInit\bin\Debug\LuceneData"));
            IndexSearcher index = new IndexSearcher(deDirectory, true);
            IndexReader indexReader = IndexReader.Open(deDirectory, true);
            BooleanQuery bq = new BooleanQuery();
            int num = index.MaxDoc();
            int a = indexReader.NumDocs();
            int b = indexReader.MaxDoc();
            Query query = new TermQuery(new Term("UName", key));

            bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
            bq.Add(query, BooleanClause.Occur.MUST);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TopDocs topDocses = index.Search(bq, null, 10);
            stopwatch.Stop();
            Log.WriteLog(string.Format("消耗时间{0}", stopwatch.Elapsed.ToString()));
            ScoreDoc[] scoreDocs = topDocses.scoreDocs;
            for (int i = 0; i < scoreDocs.Count(); i++)
            {
                S_LongHouseBase sLongHouse = new S_LongHouseBase();
                sLongHouse.UName = index.Doc(scoreDocs[i].doc).Get("UName");
                list.Add(sLongHouse);
            }
            return list;
        }

    }
}
