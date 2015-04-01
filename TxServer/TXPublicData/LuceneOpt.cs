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

namespace TXPublicData
{
    public class LuceneOpt
    {
        #region 属性

        private static object lockobject1 = new object();
        private static object lockobject2 = new object();
        private static object lockobject3 = new object();
        public string HouseConnectionString
        {
            get;
            set;
        }

        public string BaseDataConnectionString
        {
            get;
            set;
        }

        public string UserConnectionString
        {
            get;
            set;
        }

        public string HouseIndexPath
        {
            get;
            set;
        }
        public string HouseActiveIndexPath
        {
            get;
            set;
        }
        public string VillageIndexPath
        {
            get;
            set;
        }
        public string VillageSubWayIndexPath
        {
            get;
            set;
        }
        public string AreaIndexPath
        {
            get;
            set;
        }
        public string TrafficIndexPath
        {
            get;
            set;
        }

        public string CompanyIndexPath
        {
            get;
            set;
        }

        public string AdvertIndexPath
        {
            get;
            set;
        }
        public string LogPath
        {
            get;
            set;
        }
        public Dal.SearchServiceDal HouseDal
        {
            get;
            set;
        }
        public Dal.SearchServiceDal SearchDal
        {
            get;
            set;
        }
        public Dal.SearchServiceDal UserDal
        {
            get;
            set;
        }

        public string Domain
        {
            get;
            set;
        }
        public string CDNUrl
        {
            get;
            set;
        }
        public string SMSUrl
        {
            get;
            set;
        }
        public string CityId
        {
            get;
            set;
        }
        #endregion





        #region 普通小区索引生成
        public int AddDocForVillage(string villageid)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = SearchDal.GetVillage(villageid);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(villageid);
                    directory = FSDirectory.Open(new DirectoryInfo(VillageIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);

                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();

                        document.Add(new Field("Id", Convert.ToString(dr["Id"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("CityId", Convert.ToString(dr["CityId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));


                        document.Add(new Field("DId", Convert.ToString(dr["DId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("BId", Convert.ToString(dr["BId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("Name", Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string NamePY = TXCommons.Han2Ping.Convert(dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString());
                        document.Add(new Field("NamePY", NamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        document.Add(new Field("Address", Convert.ToString(dr["Address"] == DBNull.Value ? "" : dr["Address"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        document.Add(new Field("Lng", Convert.ToString(dr["Lng"] == DBNull.Value ? "" : dr["Lng"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("Lat", Convert.ToString(dr["Lat"] == DBNull.Value ? "" : dr["Lat"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        document.Add(new Field("DName", Convert.ToString(dr["DName"] == DBNull.Value ? "" : dr["DName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("BName", Convert.ToString(dr["BName"] == DBNull.Value ? "" : dr["BName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("Guid", Convert.ToString(dr["Guid"] == DBNull.Value ? "" : dr["Guid"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        indexWriter.AddDocument(document);
                        count++;
                    }
                    return count;


                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("小区索引数据获取错误", ex.Message, LogPath);
            }
            finally
            {
                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                    indexWriter.Close();
                if (directory != null)
                    directory.Close();
            }
            return count;
        }




        #endregion

        #region 地铁小区关系生成
        public int AddDocForVillageSubWay(string villageid)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = SearchDal.GetVillageSubWay(villageid);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(villageid);
                    directory = FSDirectory.Open(new DirectoryInfo(VillageSubWayIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
                        document.Add(new Field("tid", Convert.ToString(dr["tid"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("vid", Convert.ToString(dr["vid"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("pid", Convert.ToString(dr["pid"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        indexWriter.AddDocument(document);
                        count++;
                    }
                    return count;


                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("小区地铁关系数据获取错误", ex.Message, LogPath);
            }
            finally
            {
                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                    indexWriter.Close();
                if (directory != null)
                    directory.Close();
            }
            return count;
        }

        /// <summary>
        /// 删除小区地铁索引数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="columnname">列名</param>
        /// <param name="indexpath">索引位置</param>
        /// <returns></returns>
        public bool DeleteVillageSubWayDoc(string id, string indexpath)
        {
            Analyzer an = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            Directory directory = FSDirectory.Open(new DirectoryInfo(VillageSubWayIndexPath));
            Lucene.Net.Index.IndexReader ireader = IndexReader.Open(directory);
            try
            {
                Term t = new Term("vid", id);
                ireader.DeleteDocuments(t);
                return true;
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("小区地铁关系", "删除索引错误(" + indexpath + "):" + id + "_" + ex.Message, LogPath);
                return false;
            }
            finally
            {
                if (an != null)
                    an.Close();
                if (ireader != null)
                    ireader.Close();
                if (directory != null)
                    directory.Close();
            }
        }

        /// <summary>
        /// 修改小区地铁索引数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="indexpath">索引位置</param>
        /// <returns></returns>
        public bool UpdateVillageSubWayDoc(string villageid, string indexpath)
        {
            bool b = false;
            try
            {
                if (DeleteVillageSubWayDoc(villageid, VillageSubWayIndexPath))
                {

                    AddDocForVillageSubWay(villageid);
                    b = true;
                }

            }
            catch
            {

            }
            return b;
        }

        /// <summary>
        /// 根据小区查找地铁站，地铁线路
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public ArrayList GetVidBySubWay(string vid)
        {
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(VillageSubWayIndexPath));
            Lucene.Net.Search.IndexSearcher indexSearcher = new Lucene.Net.Search.IndexSearcher(directory, true);
            ArrayList arr = null;

            try
            {
                #region 固定值字段
                // Query bq = new MatchAllDocsQuery();

                if (int.Parse(vid) > 0)
                {
                    Lucene.Net.Search.BooleanQuery bq = new Lucene.Net.Search.BooleanQuery();

                    bq.Add(new Lucene.Net.Search.TermQuery(new Lucene.Net.Index.Term("vid", vid)), Lucene.Net.Search.BooleanClause.Occur.MUST);


                #endregion

                    #region 查询及结果

                    Lucene.Net.Search.TopDocs topdocs = null;
                    topdocs = indexSearcher.Search(bq, 1000);
                    int hitscount = topdocs.totalHits;

                    Lucene.Net.Search.ScoreDoc[] hits = topdocs.scoreDocs;
                    if (hits.Length > 0)
                    {
                        arr = new ArrayList();
                        arr.Add(indexSearcher.Doc(hits[0].doc).Get("tid"));
                        arr.Add(indexSearcher.Doc(hits[0].doc).Get("pid"));
                    }


                }
                    #endregion
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("小区地铁关系查找", ex.Message, LogPath);

            }
            finally
            {

                if (indexSearcher != null)
                    indexSearcher.Close();
                if (analyzer != null)
                    analyzer.Close();
                if (directory != null)
                    directory.Close();
            }
            return arr;
        }

        #endregion



        #region 区域商圈索引生成
        public int AddDocForArea(string businessid)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = SearchDal.GetArea();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(businessid);
                    directory = FSDirectory.Open(new DirectoryInfo(AreaIndexPath));

                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);

                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();

                        document.Add(new Field("BId", Convert.ToString(dr["BId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("BName", Convert.ToString(dr["BName"] == DBNull.Value ? "" : dr["BName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string BNamePY = TXCommons.Han2Ping.Convert(dr["BName"] == DBNull.Value ? "" : dr["BName"].ToString()).ToLower();
                        document.Add(new Field("BNamePY", BNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));


                        document.Add(new Field("DId", Convert.ToString(dr["DId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("DName", Convert.ToString(dr["DName"] == DBNull.Value ? "" : dr["DName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string DNamePY = TXCommons.Han2Ping.Convert(dr["DName"] == DBNull.Value ? "" : dr["DName"].ToString());
                        document.Add(new Field("DNamePY", DNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        document.Add(new Field("CityId", Convert.ToString(dr["CityId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("CityName", Convert.ToString(dr["CityName"] == DBNull.Value ? "" : dr["CityName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string CNamePY = TXCommons.Han2Ping.Convert(dr["CityName"] == DBNull.Value ? "" : dr["CityName"].ToString().Replace("市", ""));
                        document.Add(new Field("CNamePY", CNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("AdPrice", Convert.ToString(dr["adprice"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        indexWriter.AddDocument(document);
                        count++;
                    }
                    return count;


                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("区域商圈索引数据获取错误", ex.Message, LogPath);
            }
            finally
            {
                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                    indexWriter.Close();
                if (directory != null)
                    directory.Close();
            }
            return count;
        }
        #endregion


        #region 地铁线路索引生成
        public int AddDocForTraffic(string tid)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = SearchDal.GetTraffic();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(tid);
                    directory = FSDirectory.Open(new DirectoryInfo(TrafficIndexPath));

                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);


                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();

                        document.Add(new Field("TId", Convert.ToString(dr["TId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("PId", Convert.ToString(dr["PId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("TName", Convert.ToString(dr["TName"] == DBNull.Value ? "" : dr["TName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

                        document.Add(new Field("CityId", Convert.ToString(dr["CityId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("CityName", Convert.ToString(dr["CityName"] == DBNull.Value ? "" : dr["CityName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string CNamePY = TXCommons.Han2Ping.Convert(dr["CityName"] == DBNull.Value ? "" : dr["CityName"].ToString().Replace("市", ""));
                        document.Add(new Field("CNamePY", CNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        indexWriter.AddDocument(document);
                        count++;
                    }
                    return count;


                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("地铁线路索引数据获取错误", ex.Message, LogPath);
            }
            finally
            {

                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                    indexWriter.Close();
                if (directory != null)
                    directory.Close();
            }
            return count;
        }
        #endregion


        #region 经纪公司索引生成
        public int AddDocForCompany()
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = HouseDal.GetCompany();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = true;
                    directory = FSDirectory.Open(new DirectoryInfo(CompanyIndexPath));

                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);


                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();

                        document.Add(new Field("VID", Convert.ToString(dr["Id"] == DBNull.Value ? "" : dr["Id"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        document.Add(new Field("Name", Convert.ToString(dr["ShortName"] == DBNull.Value ? "" : dr["ShortName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string CNamePY = TXCommons.SpellCode.GetSpellCode(dr["ShortName"] == DBNull.Value ? "" : dr["ShortName"].ToString()).ToLower();
                        document.Add(new Field("NamePY", CNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                        indexWriter.AddDocument(document);
                        count++;
                    }
                    return count;


                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("经纪公司数据获取错误", ex.Message, LogPath);
            }
            finally
            {

                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                    indexWriter.Close();
                if (directory != null)
                    directory.Close();
            }
            return count;
        }
        #endregion


        #region Lucene相关





        /// <summary>
        /// 
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="cityid"></param>
        /// <param name="type">房源类型0 长租，1二手</param>
        /// <param name="usertype">0个人，1经纪人</param>
        //public void ClearCDN(string houseid, string cityid, string type, string usertype)
        //{


        //    try
        //    {
        //        string housetype = "0";
        //        if (type == "0" && usertype == "0")
        //            housetype = "0";
        //        if (type == "0" && usertype == "1")
        //            housetype = "1";
        //        if (type == "1")
        //            housetype = "2";
        //        var url = TXCommons.UrlRewrite.GetDetailUrl(int.Parse(cityid), int.Parse(houseid), housetype, null, null, Domain, CDNUrl);
        //        TXCommons.GenerateHtml.UpdateCDNCache(CDNUrl, url);
        //        TXCommons.Refurbish.LogTool.Log("CDN:", cityid + "-" + url + "-" + "完成", LogPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("CDN:", cityid + "-" + ex.Message, LogPath);
        //    }
        //}
        #endregion

        public string GetPropertyTypestr(string str)
        {
            string pstr = "";
            switch (str.Trim())
            {
                case "1": pstr = "house"; break;
                case "2": pstr = "flats"; break;
                case "3": pstr = "villa"; break;
                case "4": pstr = "shop"; break;
                case "5": pstr = "office"; break;

                default: break;
            }
            return pstr;
        }

        public string GetHouseTypestr(string str)
        {
            string pstr = "";
            switch (str.Trim())
            {
                case "0": pstr = "zufang"; break;
                case "1": pstr = "esf"; break;

                default: break;
            }
            return pstr;
        }

    }
}
