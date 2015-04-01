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
using System.Runtime.Serialization;

namespace TXSearch.Area
{
    /// <summary>
    /// GetAreaIndex 的摘要说明
    /// </summary>
    public class GetAreaIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "AreaIndex/";
     
      
       //public static  Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
       //   public static   Lucene.Net.Store.Directory  directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
       //   public static IndexSearcher indexSearcher = new IndexSearcher(directory, true);

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string CityPY = string.IsNullOrEmpty(context.Request.Params["CityPY"]) ? "" : context.Request.Params["CityPY"];
            string DPY = string.IsNullOrEmpty(context.Request.Params["DPY"]) ? "" : context.Request.Params["DPY"];
            string type = string.IsNullOrEmpty(context.Request.Params["type"]) ? "" : context.Request.Params["type"];
            if (!string.IsNullOrEmpty(CityPY))
            {
                try
                {
                    if (type == "xml")
                    {
                        System.Data.DataSet ds = searchXML(CityPY, DPY);
                        string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
                        if (ds == null || ds.Tables.Count == 0)
                        {
                            result += "\n<Summary>";
                           
                            result += "\n</Summary>";
                        }
                        else
                        {
                            result += "\n" + ds.GetXml();
                        }
                        context.Response.Write(result);
                    }
                    else if (type == "json")
                    {
                        string json = string.Empty;
                        if (string.IsNullOrEmpty(DPY))
                            json = SearchJson(CityPY,string .Empty);
                        else
                            json = SearchJson(CityPY, DPY);
                        if (string.IsNullOrEmpty(json))
                            json += "[]";
                        if (!string.IsNullOrEmpty(context.Request.QueryString["callback"]))
                        {

                            context.Response.Write(context.Request.QueryString["callback"].ToString() + "(" + json + ");");
                        }
                        else
                        {
                            context.Response.Write(json);
                        }
                    }
                    else
                    {
                        string json = string.Empty;
                        if (string.IsNullOrEmpty(DPY))
                            json = Search(CityPY);
                        else
                            json = Search(CityPY, DPY);
                        if (string.IsNullOrEmpty(json))
                            json += "[]";
                        if (!string.IsNullOrEmpty(context.Request.QueryString["callback"]))
                        {

                            context.Response.Write(context.Request.QueryString["callback"].ToString() + "(" + json + ");");
                        }
                        else
                        {
                            context.Response.Write(json);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    context.Response.Write("");
                    logger.Error(ex.Message);
                }
            };
        }

        public string Search(string CityPY)
        {

            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            Dictionary<int, Area> dlist = null;
           
            
            try
            {
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                indexSearcher = new IndexSearcher(directory, true);
                 dlist = new Dictionary<int, Area>();
                #region 固定值字段
                // Query bq = new MatchAllDocsQuery();

                BooleanQuery bq = new BooleanQuery();

                if (!string.IsNullOrEmpty(CityPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("CNamePY", CityPY)), BooleanClause.Occur.MUST);
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("BId", "0")), BooleanClause.Occur.MUST);
                }
              
               

                #endregion


                #region 查询及结果

                TopDocs topdocs = null;
                topdocs = indexSearcher.Search(bq, 1000);
                int hitscount = topdocs.totalHits;

                ScoreDoc[] hits = topdocs.scoreDocs;
                foreach (ScoreDoc doc in hits)
                {
                    Area d = new Area();
                    d.Id = int.Parse ( indexSearcher.Doc(doc.doc).Get("DId"));
                    d.Name = indexSearcher.Doc(doc.doc).Get("DName");
                    d.NamePY = indexSearcher.Doc(doc.doc).Get("DNamePY");
                  
                    dlist.Add(d.Id, d);
                   
                }
               
                StringBuilder text = new StringBuilder("");
                StringBuilder value = new StringBuilder("");
                foreach (KeyValuePair<int, Area> kv in dlist)
                {
                    text.Append(",\"" + kv.Value.Name + "\"");
                    value.Append(",\"" + kv.Value.NamePY + "\"");
                }
                return "{\"text\":[\"不限\"" + text.ToString() + "],\"value\":[\"\"" + value.ToString() + "]}";
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
            return null;
        }

        public string Search(string CityPY, string DPY)
        {

            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            List<Area> blist = new List<Area>();


            try
            {
                 analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                 directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                 indexSearcher = new IndexSearcher(directory, true);
                #region 固定值字段
                // Query bq = new MatchAllDocsQuery();

                BooleanQuery bq = new BooleanQuery();

                if (!string.IsNullOrEmpty(CityPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("CNamePY", CityPY)), BooleanClause.Occur.MUST);
                    if (string.IsNullOrEmpty(DPY))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("BId", "0")), BooleanClause.Occur.MUST);
                    }
                }
                if (!string.IsNullOrEmpty(DPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("DNamePY", DPY)), BooleanClause.Occur.MUST);

                }


                #endregion


                #region 查询及结果

                TopDocs topdocs = null;
                topdocs = indexSearcher.Search(bq, 1000);
                int hitscount = topdocs.totalHits;

                ScoreDoc[] hits = topdocs.scoreDocs;
                foreach (ScoreDoc doc in hits)
                {
                    Area b = new Area();
                    b.Id = int.Parse ( indexSearcher.Doc(doc.doc).Get("BId"));
                    b.Name = indexSearcher.Doc(doc.doc).Get("BName");
                    b.NamePY = indexSearcher.Doc(doc.doc).Get("BNamePY");
                    blist.Add(b);
                }
                StringBuilder text = new StringBuilder("");
                StringBuilder value = new StringBuilder("");
                foreach (Area a in blist)
                {
                    if (!string.IsNullOrEmpty(a.Name))
                    {
                        text.Append(",\"" + a.Name + "\"");
                        value.Append(",\"" + a.NamePY + "\"");
                    }
                }
                return "{\"text\":[\"不限\"" + text.ToString() + "],\"value\":[\"\"" + value.ToString() + "]}";
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
            return null;
        }

       

        public string SearchJson(string CityPY, string DPY)
        {

            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            List<Area> blist = new List<Area>();


            try
            {
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                indexSearcher = new IndexSearcher(directory, true);
                #region 固定值字段
                // Query bq = new MatchAllDocsQuery();

                BooleanQuery bq = new BooleanQuery();

                if (!string.IsNullOrEmpty(CityPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityId", CityPY)), BooleanClause.Occur.MUST);
                    if (string.IsNullOrEmpty(DPY))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("BId", "0")), BooleanClause.Occur.MUST);
                    }
                }
                if (!string.IsNullOrEmpty(DPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("DId", DPY)), BooleanClause.Occur.MUST);

                }


                #endregion


                #region 查询及结果

                TopDocs topdocs = null;
                topdocs = indexSearcher.Search(bq, 1000);
                int hitscount = topdocs.totalHits;

                ScoreDoc[] hits = topdocs.scoreDocs;
                foreach (ScoreDoc doc in hits)
                {
                    Area b = new Area();
                    if (!string.IsNullOrEmpty(DPY))
                    {
                        b.Id = int.Parse(indexSearcher.Doc(doc.doc).Get("BId"));
                        b.Name = indexSearcher.Doc(doc.doc).Get("BName");
                        b.AdPrice = indexSearcher.Doc(doc.doc).Get("AdPrice");
                    }
                    else
                    {
                        b.Id = int.Parse(indexSearcher.Doc(doc.doc).Get("DId"));
                        b.Name = indexSearcher.Doc(doc.doc).Get("DName");
                        b.AdPrice = indexSearcher.Doc(doc.doc).Get("AdPrice");
                       
                    }
                    blist.Add(b);
                }

                return TXCommons.Xml.ComObjOrXml.ToJsJson(blist);
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
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CityPY"></param>
        /// <param name="DPY"></param>
        /// <returns></returns>
        public DataSet searchXML(string CityPY, string DPY)
        {
            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            DataSet ds = new DataSet("Results");
            try
            {
                #region 固定值字段
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                indexSearcher = new IndexSearcher(directory, true);

                BooleanQuery bq = new BooleanQuery();

                if (!string.IsNullOrEmpty(CityPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("CNamePY", CityPY)), BooleanClause.Occur.MUST);
                    if (string.IsNullOrEmpty(DPY))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("BId", "0")), BooleanClause.Occur.MUST);
                    }
                }
                if (!string.IsNullOrEmpty(DPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("DNamePY", DPY)), BooleanClause.Occur.MUST);

                }


                #endregion


                #region 查询及结果

                TopDocs topdocs = null;
                topdocs = indexSearcher.Search(bq, 1000);
                ScoreDoc[] hits = topdocs.scoreDocs;
                int hitscount = topdocs.totalHits;

                if (hits.Length > 0)
                {
                    DataTable dt = new DataTable("Record");
                    IList fds = indexSearcher.Doc(hits[0].doc).GetFields();
                    for (int i = 0; i < fds.Count; i++)
                    {
                        dt.Columns.Add(((Field)fds[i]).Name());

                    }
                    
                    for (int i = 0; i < hitscount; i++)
                    {
                        try
                        {
                            Lucene.Net.Documents.Document hitDoc = indexSearcher.Doc(hits[i].doc);
                            DataRow dr = dt.NewRow();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                string value = ((Field)hitDoc.GetFields()[k]).StringValue();
                                dr[k] = string.IsNullOrEmpty(value) ? string.Empty : value;
                            }
                            dt.Rows.Add(dr);
                        }
                        catch (Exception ex)
                        {
                            logger.Error("数据源序列：" + i.ToString() + " 错误:" + ex.Message);
                        }
                    }


                    DataTable summary_dt = new DataTable("Summary");
                    summary_dt.Columns.Add("TotalRecords");
                    DataRow summary_dr = summary_dt.NewRow();
                    summary_dr[0] = hitscount;
                    summary_dt.Rows.Add(summary_dr);
                    ds.Tables.Add(summary_dt);
                    ds.Tables.Add(dt);


                    return ds;
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
            return null;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
     [DataContract(Namespace = "http://www.kuaiyoujia.com/")]
    public class Area
    {
         [DataMember]
        public int Id { get; set; }
         [DataMember]
      public   string Name { get; set; }

         [DataMember]
         public string AdPrice { get; set; }
      public string NamePY { get; set; }
      
    }

   

    
}