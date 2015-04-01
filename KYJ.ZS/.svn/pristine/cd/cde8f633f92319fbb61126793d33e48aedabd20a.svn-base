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
using KYJ.ZS.Models.Categories;

namespace TXSearch.Categories
{
    /// <summary>
    /// GetCategories 的摘要说明
    /// </summary>
    public class GetCategories : IHttpHandler
    {
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "CategoriesIndex/";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
            KYJ.ZS.Commons.Index.Relusts<CatResult> relusts = new Relusts<CatResult>();

       //     relusts = IndexCommon.GetCategories("");

            string condition=context .Request.QueryString["condition"];
            string[] conditions = null;
            string pid ="";
            string id = "";
            if (string.IsNullOrEmpty(condition))
            {

            }
            else
            {
                 conditions = condition.Split("-".ToCharArray());
                 pid = conditions[0];
                 id = conditions[1];
            }
           
            relusts.FatherCategories= Search(pid);
            relusts.SonCategories = Search(id);
            if (relusts != null && relusts.FatherCategories.Count > 0)
            {

                result = Auxiliary.SerializeWrite<Relusts<CatResult>>(relusts);
                
            }
            context.Response.Write(result);
        }
        public List<CatResult> Search(string id)
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

                BooleanQuery bq = GetQuery(analyzer,id);

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


                return GetList(indexSearcher, bq, sort, out hitscount);


                

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




        public BooleanQuery GetQuery(Analyzer analyzer,string id)
        {
            #region 固定值字段

            BooleanQuery bq = new BooleanQuery();
            if (string.IsNullOrEmpty(id))
            {
                bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
            }
            else
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("category_pid", id)), BooleanClause.Occur.MUST);
            }
            return bq;
            #endregion
        }

        public Sort GetSort()
        {
            Sort sort = null;
            string Name = "";
            bool Order = false;
            Name = "category_id";
            Order = false;

            SortField[] sf = { new SortField(Name, SortField.LONG, Order) };
            sort = new Sort(sf);
            return sort;
        }


        public List<CatResult> GetList(IndexSearcher indexSearcher, BooleanQuery bq, Sort sort, out int count)
        {
            TopFieldDocs topdocs = null;

            topdocs = indexSearcher.Search(bq, null, 50, sort);
            ScoreDoc[] hits = topdocs.scoreDocs;

            count = topdocs.totalHits;
            int bi = 1;
            int ei = 50;
            List<CatResult> list = new List<CatResult>();
            if (hits.Length > 0 && bi < count)
            {
                for (int i = bi; i < ei; i++)
                {
                    if (i >= count)
                        break;
                    CatResult result = new CatResult();
                    ScoreDoc doc = hits[i];

                    result.Id =Convert.ToInt32(indexSearcher.Doc(doc.doc).Get("category_id"));
                    result.Name = indexSearcher.Doc(doc.doc).Get("category_name");
                    result.Pid =Convert.ToInt32( indexSearcher.Doc(doc.doc).Get("category_pid"));

                    result.IsShow = true;
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