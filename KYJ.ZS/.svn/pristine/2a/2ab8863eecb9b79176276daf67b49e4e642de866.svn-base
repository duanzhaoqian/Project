using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using IndexSearcher = Lucene.Net.Search.IndexSearcher;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Search;
using Lucene.Net.Index;

using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text;
using KYJ.ZS.Commons.Index;
using KYJ.ZS.Commons;
namespace TXSearch.Attributes
{
    /// <summary>
    /// GetAttributes 的摘要说明
    /// </summary>
    public class GetAttributes : IHttpHandler
    { 
         public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
         public static string IndexPath = BasePath + "AttributesIndex/";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
            List<IndexResult> relusts = new List<IndexResult>();
            Search(relusts);
            if (relusts != null && relusts.Count > 0)
            {

                result = Auxiliary.SerializeWrite<List<IndexResult>>(relusts);
            }
            context.Response.Write(result);
        }
        public void Search(List<IndexResult> results)
        {

           
            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            //IndexSearcher indexSearcher = new IndexSearcher(IndexPath);


            try
            {
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                indexSearcher = new IndexSearcher(directory, true);
                #region 固定值字段

                BooleanQuery bq = GetQuery(analyzer);

                #endregion

                #region 范围值字段



                #endregion

                #region 排序字段
                Sort sort = null;

                sort = GetSort();


                #endregion

                #region 查询对象


                #endregion

                #region 特殊字段查询

                #endregion

                #region 查询及结果


                int hitscount = 0;


                results = GetList(indexSearcher, bq, sort, out hitscount);
                



                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
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

        }




        public BooleanQuery GetQuery(Analyzer analyzer)
        {
            #region 固定值字段

            BooleanQuery bq = new BooleanQuery();
            
                bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
           
            return bq;
            #endregion
        }

        public Sort GetSort()
        {
            Sort sort = null;
           
            return sort;
        }


        public List<IndexResult> GetList(IndexSearcher indexSearcher, BooleanQuery bq, Sort sort, out int count)
        {
            TopFieldDocs topdocs = null;

            topdocs = indexSearcher.Search(bq, null,50, sort);
            ScoreDoc[] hits = topdocs.scoreDocs;

            count = topdocs.totalHits;
            int bi =1;
            int ei =50;
            List<IndexResult> list = new List<IndexResult>();
            if (hits.Length > 0 && bi < count)
            {
                for (int i = bi; i < ei; i++)
                {
                    if (i >= count)
                        break;
                    IndexResult result = new IndexResult();
                    ScoreDoc doc = hits[i];

                    result.Id = indexSearcher.Doc(doc.doc).Get("attr_id");
                    result.Name = indexSearcher.Doc(doc.doc).Get("attr_name");
             
                    if (list.Count(r => r.Id == result.Id) == 0)
                        list.Add(result);
                }
            }
            return list;
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