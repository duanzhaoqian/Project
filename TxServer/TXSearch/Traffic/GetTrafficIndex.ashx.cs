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
namespace TXSearch.Traffic
{
    /// <summary>
    /// GetTrafficIndex 的摘要说明
    /// </summary>
    public class GetTrafficIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "TrafficIndex/";
        SearchRentHouseConditionInfo info = new SearchRentHouseConditionInfo();

        //public static Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //public static Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
        //public static IndexSearcher indexSearcher = new IndexSearcher(directory, true);
          
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string CityPY = string.IsNullOrEmpty(context.Request.Params["CityPY"]) ? "" : context.Request.Params["CityPY"];
            string PId = string.IsNullOrEmpty(context.Request.Params["PId"]) ? "0" : context.Request.Params["PId"];
            string type = string.IsNullOrEmpty(context.Request.Params["type"]) ? "" : context.Request.Params["type"];
            if (!string.IsNullOrEmpty(CityPY))
            {
                try
                {
                    if (type == "xml")
                    {
                        System.Data.DataSet ds = searchXML(CityPY, PId);
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
                    else
                    {
                        string json = string.Empty;
                        json = Search(CityPY, PId);
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

        public string Search(string CityPY, string pid)
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

                }
                if (!string.IsNullOrEmpty(pid))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("PId", pid)), BooleanClause.Occur.MUST);

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
                    b.Id = int.Parse(indexSearcher.Doc(doc.doc).Get("TId"));
                    b.Name = indexSearcher.Doc(doc.doc).Get("TName");
                  
                    blist.Add(b);
                   
                }
               
                StringBuilder text = new StringBuilder("");
                StringBuilder value = new StringBuilder("");
                foreach (Area a in blist)
                {
                    text.Append(",\"" + a.Name + "\"");
                    value.Append(",\"" + a.Id + "\"");
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

        public DataSet searchXML(string CityPY, string pid)
        {
            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            DataSet ds = new DataSet("Results");
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

                }
                if (!string.IsNullOrEmpty(pid))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("PId", pid)), BooleanClause.Occur.MUST);

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
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NamePY { get; set; }

    }
}