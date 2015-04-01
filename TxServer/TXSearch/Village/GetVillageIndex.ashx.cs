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

namespace TXSearch.Village
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class GetVillageIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "VillageIndex/";
        SearchRentHouseConditionInfo info = new SearchRentHouseConditionInfo();

        //public static Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //public static Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
        //public static IndexSearcher indexSearcher = new IndexSearcher(directory, true);
       
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            
            string CityId = string.IsNullOrEmpty(context.Request.Params["CityId"]) ? "" : context.Request.Params["CityId"];
            string DId = string.IsNullOrEmpty(context.Request.Params["DId"]) ? "" : context.Request.Params["DId"];
            string BId = string.IsNullOrEmpty(context.Request.Params["BId"]) ? "" : context.Request.Params["BId"];
            string q = string.IsNullOrEmpty(context.Request.Params["q"]) ? "" : context.Request.Params["q"];
            string type = string.IsNullOrEmpty(context.Request.Params["type"]) ? "" : context.Request.Params["type"];
            string size = string.IsNullOrEmpty(context.Request.Params["size"]) ? "10" : context.Request.Params["size"];
            if (!string.IsNullOrEmpty(CityId))
            {
                try
                {
                    System.Data.DataSet ds = (System.Data.DataSet)Search(CityId, DId, BId, q,size);
                    if (type == "xml")
                    {
                        context.Response.ContentType = "text/xml";
                        string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
                        if (ds == null || ds.Tables.Count == 0)
                        {
                            result += "\n<Summary>";
                            result += "\n<TotalRecords>0</TotalRecords>\n<PageSize>" + info.PageSize + "</PageSize>\n<PageIndex>" + info.PageIndex + "</PageIndex>\n<TotalPage>1</TotalPage>\n<RealRecords>0</RealRecords>";
                            result += "\n</Summary>";
                        }
                        else
                        {
                            result += "\n" + ds.GetXml();
                        }
                        context.Response.Write(result);
                    }

                    else if (type == "array")
                    {
                        string json = string.Empty;
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                        {
                            StringBuilder str = new StringBuilder("");
                            foreach (DataRow row in ds.Tables[1].Rows)
                            {
                                str.Append("{");
                                str.Append("Id:'"+row["Id"].ToString()+"',");
                                str.Append("CityId:'" + row["CityId"].ToString() + "',");
                                str.Append("DId:'" + row["DId"].ToString() + "',");
                                str.Append("BId:'" + row["BId"].ToString() + "',");
                                str.Append("Name:'" + row["Name"].ToString() + "',");
                                str.Append("Address:'" + row["Address"].ToString() + "',");
                                str.Append("Lng:'" + row["Lng"].ToString() + "',");
                                str.Append("Lat:'" + row["Lat"].ToString() + "',");
                                str.Append("DName:'" + row["DName"].ToString() + "',");
                                str.Append("BName:'" + row["BName"].ToString() + "',");
                                str.Append("Guid:'" + row["Guid"].ToString() + "',");
                                str.Remove(str.Length - 1,1);
                                str.Append(@"}\n");
                            }
                            json = str.ToString();
                        }
                        if (string.IsNullOrEmpty(json))
                            json += "{}";
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
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            json = TXCommons.ToJSon.CreateJsonParameters(ds.Tables[1]);
                        }
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

        public object Search(string CityId, string DId, string BId, string q,string size)
        {

            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
         
            DataSet ds = new DataSet("Results");
            List<SearchRentHouseConditionInfo> list = new List<SearchRentHouseConditionInfo>();
            try
            {
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                indexSearcher = new IndexSearcher(directory, true);
                #region 固定值字段
                // Query bq = new MatchAllDocsQuery();

                BooleanQuery bq = new BooleanQuery();

                if (!string.IsNullOrEmpty(CityId))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityId", CityId)), BooleanClause.Occur.MUST);

                }
                if (!string.IsNullOrEmpty(DId))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("DId", DId)), BooleanClause.Occur.MUST);

                }
                if (!string.IsNullOrEmpty(BId))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("BId", BId)), BooleanClause.Occur.MUST);

                }

                q = q.Trim().Replace("*", "").Replace("?", "");
                if (!string.IsNullOrEmpty(q))
                {
                    if (TXCommons.Util.IsEnglish(q)&&q.Length>1)
                    {
                        PrefixQuery pq = new PrefixQuery(new Lucene.Net.Index.Term("NamePY", q));
                       //var aq  = "*" + q + "*";
                       // Query pq = new WildcardQuery(new Lucene.Net.Index.Term("NamePY", aq));
                        bq.Add(pq, BooleanClause.Occur.MUST);
                    }
                    else 
                    {
                        Lucene.Net.Search.Query query = new Lucene.Net.QueryParsers.MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, new string[] { "Name" }, analyzer).Parse(q);
                        bq.Add(query, BooleanClause.Occur.MUST);
                    }
                }


                #endregion

                #region 范围值字段



                #endregion

                #region 排序字段

                Sort sort = new Sort();



                #endregion

                #region 查询对象



                info.PageSize = size;


                info.PageIndex = "1";

                #endregion

                #region 特殊字段查询

                #endregion

                #region 查询及结果

                TopFieldDocs topdocs = null;

                topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex),sort);
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
                    int begin = (Convert.ToInt32(info.PageIndex) - 1) * Convert.ToInt32(info.PageSize);
                    int end = Convert.ToInt32(info.PageIndex) * Convert.ToInt32(info.PageSize); ;
                    int min = Math.Min(end, hitscount);
                    for (int i = begin; i < min; i++)
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
                    summary_dt.Columns.Add("PageSize");
                    summary_dt.Columns.Add("PageIndex");
                    summary_dt.Columns.Add("TotalPage");
                    summary_dt.Columns.Add("RealRecords");
                    DataRow summary_dr = summary_dt.NewRow();
                    summary_dr[0] = (hitscount > 100 * Convert.ToInt32(info.PageSize)) ? (100 * Convert.ToInt32(info.PageSize)).ToString() : hitscount.ToString();
                    summary_dr[1] = info.PageSize;
                    summary_dr[2] = info.PageIndex;
                    int totalpage = hitscount % Convert.ToInt32(info.PageSize) == 0 ? (hitscount / Convert.ToInt32(info.PageSize)) : ((hitscount / Convert.ToInt32(info.PageSize)) + 1);
                    summary_dr[3] = totalpage > 100 ? "100" : totalpage.ToString();
                    summary_dr[4] = hitscount;
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
            return ds;
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