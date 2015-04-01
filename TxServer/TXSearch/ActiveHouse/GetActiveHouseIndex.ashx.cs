using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TXCommons.LuceneSearch;
using System.Data;

using Analyzer = Lucene.Net.Analysis.Analyzer;
using StandardAnalyzer = Lucene.Net.Analysis.Standard.StandardAnalyzer;
using IndexSearcher = Lucene.Net.Search.IndexSearcher;
using Field = Lucene.Net.Documents.Field;
using Lucene.Net.Search;

namespace TXSearch.ActiveHouse
{
    public class GetActiveHouseIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "LongHouseSearchActive/";

        SearchRentHouseConditionInfo info;
        public void ProcessRequest(HttpContext context)
        {
            string condition = context.Request.Params["condition"];
            //if (!string.IsNullOrEmpty(condition))
            //{
            try
            {
                 
                System.Data.DataSet ds = (System.Data.DataSet)Search(context.Server.UrlDecode(condition));
                
                context.Response.ContentType = "text/xml";
                string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            //}
        }

        public void AddRows(DataTable t1, DataTable t2)
        {
            foreach (DataRow row in t2.Rows)
            {
                t1.Rows.Add(row.ItemArray);
            }
        }

        public object Search(string conditions)
        {
            info = SearchRentHouseConditionInfo.GetActiveSearchCondiction(conditions);
            if (info == null)
                return null;
            Analyzer analyzer = null;
            Lucene.Net.Store.Directory directory = null;
            IndexSearcher indexSearcher = null;
            //IndexSearcher indexSearcher = new IndexSearcher(IndexPath);
            DataSet ds = new DataSet("Results");

            try
            {
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
                indexSearcher = new IndexSearcher(directory, true);
                #region 固定值字段

                BooleanQuery bq = new BooleanQuery();
                if (string.IsNullOrEmpty(conditions))
                {
                    bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
                }
                if (!string.IsNullOrEmpty(info.CityPY))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityPY", info.CityPY)), BooleanClause.Occur.MUST);

                }
                if (!string.IsNullOrEmpty(info.SearchType))
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("SearchType", info.SearchType)), BooleanClause.Occur.MUST);

                }
                if (!string.IsNullOrEmpty(info.PropertyType))
                {
                    DateTime beginDate = TXCommons.Util.FROM_UNIXTIME(long.Parse(info.PropertyType));

                    DateTime endDate = beginDate.AddDays(1);
                    if (int.Parse(info.PageSize) > 10)
                    {
                        endDate = beginDate.AddYears(10);
                    }
                    

                    long ed = TXCommons.Util.UNIX_TIMESTAMP(endDate);
                    bq.Add(NumericRangeQuery.NewLongRange("BidStartTime", long.Parse(info.PropertyType), ed, true, true), BooleanClause.Occur.MUST);
                }





                #endregion

                #region 范围值字段



                #endregion

                #region 排序字段

                Sort sort = null;
                SortField[] sf = { new SortField("BidStartTime", SortField.LONG, false) };
                                sort = new Sort(sf);
                
                #endregion

                #region 查询对象

                if (string.IsNullOrEmpty(info.PageSize) || !IsNumberic(info.PageSize))
                {
                    info.PageSize = "20";
                }
                if (Convert.ToInt32(info.PageSize) > 100)
                {
                    info.PageSize = "20";
                }
                if (string.IsNullOrEmpty(info.PageIndex) || !IsNumberic(info.PageIndex))
                {
                    info.PageIndex = "1";
                }
                #endregion

                #region 特殊字段查询

                #endregion

                #region 查询及结果

                TopFieldDocs topdocs = null;

                topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex), sort);
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
                    if (int.Parse(info.PageSize) == 20)
                    {
                        DataTable table = dt.Clone();

                        DataTable rows0 = table.Clone();
                        DataTable rows1 = table.Clone();
                        DataTable rows2 = table.Clone();
                        DataTable rows3 = table.Clone();
                        DataTable rows4 = table.Clone();
                        DataTable rows5 = table.Clone();

                        foreach (DataRow row in dt.Rows)
                        {
                            if (TXCommons.Util.FROM_UNIXTIME(Convert.ToInt64(row["BidStartTime"])).Date == DateTime.Now.Date)
                            {

                                int status = Convert.ToInt16(row["BidStatus"]);
                                if (status == 0)
                                {

                                    rows0.Rows.Add(row.ItemArray);
                                }
                                if (status == 1)
                                {
                                    rows1.Rows.Add(row.ItemArray);
                                }
                                if (status == 2)
                                {
                                    rows2.Rows.Add(row.ItemArray);
                                }
                                if (status == 3)
                                {
                                    rows3.Rows.Add(row.ItemArray);
                                }
                                if (status == 4)
                                {
                                    rows4.Rows.Add(row.ItemArray);
                                }
                            }
                            else
                            {
                                rows5.Rows.Add(row.ItemArray);
                            }
                        }
                        AddRows(table, rows1);
                        AddRows(table, rows0);
                        AddRows(table, rows2);
                        AddRows(table, rows3);
                        AddRows(table, rows4);
                        AddRows(table, rows5);

                        ds.Tables.Add(table);
                      
                        rows0 = null;
                        rows1 = null;
                        rows2 = null;
                        rows3 = null;
                        rows4 = null;
                        rows5 = null;
                    }
                    else
                    {
                        ds.Tables.Add(dt);
                    }
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

        public static bool IsNumberic(string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
    }
}