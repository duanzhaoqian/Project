using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TXCommons.LuceneSearch;
using System.Data;
using System.Text;
using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using IndexSearcher = Lucene.Net.Search.IndexSearcher;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Search;
namespace TXSearch.SubWayVillage
{
    /// <summary>
    /// GetTrafficIndex 的摘要说明
    /// </summary>
    public class GetSubWayVillageIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string VillageSubWayIndexPath = BasePath + "VillageSubWayIndex/";
       
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
          
            string vid = string.IsNullOrEmpty(context.Request.Params["vid"]) ? "" : context.Request.Params["vid"];
         
            ArrayList arr = GetVidBySubWay(vid);
            StringBuilder strB = new StringBuilder("");
            foreach (string str in arr)
            {
                strB.Append(str+",");
            }
            context.Response.Write(strB.ToString());
        }

        public ArrayList GetVidBySubWay(string vid)
        {
            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
             ArrayList arr=new ArrayList ();
            try
            {
                 analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                 directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(VillageSubWayIndexPath));
                 indexSearcher = new IndexSearcher(directory, true);
                #region 固定值字段
                // Query bq = new MatchAllDocsQuery();

                BooleanQuery bq = new BooleanQuery();

                if (!string.IsNullOrEmpty(vid))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("vid", vid)), BooleanClause.Occur.MUST);

                }
               


                #endregion

                #region 查询及结果

                TopDocs topdocs = null;
                topdocs = indexSearcher.Search(bq, 1000);
                int hitscount = topdocs.totalHits;
              
                ScoreDoc[] hits = topdocs.scoreDocs;
                foreach (ScoreDoc doc in hits)
                {
                    arr.Add(indexSearcher.Doc(doc.doc).Get("tid"));
                    arr.Add(indexSearcher.Doc(doc.doc).Get("pid"));
                }

                #endregion
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            finally
            {
                if (directory != null)
                    directory.Close();
                if (indexSearcher != null)
                    indexSearcher.Close();
                if (analyzer != null)
                    analyzer.Close();
            }
            return arr;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}