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
using Lucene.Net.Index;

using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text;
namespace TXSearch.RentHouse
{

    //public class TimeComparatorSource:FieldComparatorSource
    //{
    //    private DateTime datetime = new DateTime ();
    //    public  TimeComparatorSource(DateTime t)
    //    {
    //        datetime = t;
    //    }

    //    public FieldComparator  NewComparator(string fieldname, int numHits, int sortPos, bool reversed)
    //    {
    //        return new TimeScoreDocLookupComparator(fieldname, numHits,datetime);
    //    }

    //    private class TimeScoreDocLookupComparator:FieldComparator
    //    {
    //        private double[] values;
    //        private string fieldName;
    //        private int[] UpdateTime;
    //        private double  bottom;
    //        private DateTime time = new DateTime();
    //        public TimeScoreDocLookupComparator(string fieldName, int numHits,DateTime time)
    //        {
    //            values = new double[numHits];
    //            this.fieldName = fieldName;
    //            this.time = time;
    //        }
    //        public void SetNextReader(IndexReader reader, int docBase)
    //        {
    //            this.UpdateTime = FieldCache_Fields.DEFAULT.GetInts(reader, "UpdateTime");

    //        }
    //        public int Compare(int slot1, int slot2)
    //        {
    //            if (values[slot1] < values[slot2])
    //            {
    //                return -1;
    //            }
    //            if (values[slot1] > values[slot2])
    //            {
    //                return 1;
    //            }
    //            return 0;
    //        }

    //        public void SetBottom(int slot)
    //        {
    //            bottom = values[slot];
    //        }

    //        public int CompareBottom(int doc)
    //        {
    //            double t=GetTime();
    //            if (bottom < t)
    //                return -1;
    //            if (bottom > t)
    //                return 1;
    //            return 0;
    //        }

    //        public double GetTime()
    //        {
    //            return TXCommons.Util.UNIX_TIMESTAMP(time);
    //        }

    //    }

        
    //}

    

    /// <summary>
    /// GetLongHouseIndex 的摘要说明
    /// </summary>
    public class GetLongHouseIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "LongHouseIndex/";
        public static string IndexAdvertPath = BasePath + "AdvertIndex/";
        SearchRentHouseConditionInfo info;
        SearchData search = new SearchData();
       
        public void ProcessRequest(HttpContext context)
        {
            string condition = context.Request.Params["condition"];
            //string BNamePY = TXCommons.Han2Ping.Convert("长湴");
           
                try
                {
                    info = SearchRentHouseConditionInfo.GetSearchCondiction(condition);
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
                    
                    Relusts<Summary, House> relusts = new Relusts<Summary, House>();

                    relusts.Summary = new Summary();
                    relusts.Houses = new List<House>();
                    relusts.AdvertLeft =new List<House>();
                    relusts.AdvertRight = new List<House>();
                    string isre = info.PageIndex == "1" ? "1" : "0";
                   relusts=  search.Search(context.Server.UrlDecode(condition), "1", null, relusts,IndexPath,info);

                    context.Response.ContentType = "text/xml";
                    string result = "<?xml version=\"1.0\"  encoding=\"UTF-8\" ?>";
                    if (relusts == null && relusts.Houses == null || relusts.Houses .Count() == 0)
                    {
                        relusts.Summary.TotalRecords = 0;
                        relusts.Summary.PageSize =int.Parse ( info.PageSize);
                        relusts.Summary.PageIndex =int.Parse ( info.PageIndex);

                        relusts.Summary.TotalPage = 1;
                        relusts.Summary.RealRecords = 0;
                       
                    }
                    else
                    {

                        if (relusts != null && relusts.Houses != null && relusts.Houses.Count() != 0)
                        {
                         


                            //if (relusts == null || relusts.Agent == null || relusts.Agent.Count == 0)
                            //{
                            //    SearchAdvertCopy(context.Server.UrlDecode(condition), relusts);
                            //}

                        }



                    }
                    if (string.IsNullOrEmpty(info.BusinessPY) && Convert.ToInt16(info.PageIndex) > 1)
                    {
                        string advertconfig = search.GetAdvertConfigData(info);

                       // relusts.AdvertLeft = search.SearchAdvert(context.Server.UrlDecode(condition), relusts, info, 2, IndexAdvertPath, advertconfig);

                        relusts.AdvertRight = search.SearchAdvert(context.Server.UrlDecode(condition), relusts, info, 3, IndexAdvertPath, advertconfig);
                    }
                    else
                    {
                        string advertconfig = search.GetAdvertConfigData(info);

                        relusts.AdvertLeft = search.SearchAdvert(context.Server.UrlDecode(condition), relusts, info, 2, IndexAdvertPath, advertconfig);

                        relusts.AdvertRight = search.SearchAdvert(context.Server.UrlDecode(condition), relusts, info, 3, IndexAdvertPath, advertconfig);
                    }
                   
                    result = TXCommons.Xml.ComObjOrXml.SerializeWrite<Relusts<Summary, House>>(relusts);
                  
                    context.Response.Write(result);
                  
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            
        }

        //public void Search(string conditions, string issvip, string IsRecommend, Relusts<Summary, House> results)
        //{
           
        //    if (info == null)
        //        return;
        //    Analyzer analyzer = null;
        //    Lucene.Net.Store.Directory directory = null;
        //    IndexSearcher indexSearcher = null;
        //    //IndexSearcher indexSearcher = new IndexSearcher(IndexPath);
           
         
        //    try
        //    {
        //        analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //        directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
        //        indexSearcher = new IndexSearcher(directory, true);
        //        #region 固定值字段

        //        BooleanQuery bq = GetQuery(analyzer,conditions, issvip,IsRecommend);
               
        //        #endregion

        //        #region 范围值字段



        //        #endregion

        //        #region 排序字段
        //        Sort sort =null;
               
        //         sort = GetSort();
                
               
        //        #endregion

        //        #region 查询对象

               
        //        #endregion

        //        #region 特殊字段查询

        //        #endregion

        //        #region 查询及结果


        //         int hitscount = 0;



        //         results.Houses = GetList(indexSearcher, bq, sort,  out hitscount);
        //                if (hitscount > 0)
        //                {
        //                    results.Summary.TotalRecords = (hitscount > 100 * Convert.ToInt32(info.PageSize)) ? 100 * Convert.ToInt32(info.PageSize) : hitscount;
        //                    results.Summary.PageSize = int.Parse(info.PageSize);
        //                    results.Summary.PageIndex = int.Parse(info.PageIndex);
        //                    int totalpage = hitscount % Convert.ToInt32(info.PageSize) == 0 ? (hitscount / Convert.ToInt32(info.PageSize)) : ((hitscount / Convert.ToInt32(info.PageSize)) + 1);
        //                    results.Summary.TotalPage = totalpage;
        //                    results.Summary.RealRecords = hitscount;
        //                }
                       
                    
                    
                    
                

        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //    }
        //    finally
        //    {
        //        if (directory != null)
        //            directory.Close();
        //        if (indexSearcher != null)
        //            indexSearcher.Close();
        //        if (analyzer != null)
        //            analyzer.Close();
        //    }
           
        //}




        //public BooleanQuery GetQuery(Analyzer analyzer, string conditions, string issvip, string IsRecommend)
        //{
        //    #region 固定值字段

        //    BooleanQuery bq = new BooleanQuery();
        //    if (string.IsNullOrEmpty(conditions))
        //    {
        //        bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
        //    }
        //    else
        //    {
        //        //if (info.SearchType != "esf")
        //        //{
        //            //if (!string.IsNullOrEmpty(issvip))
        //            //{
        //            //    bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSVip", issvip)), BooleanClause.Occur.MUST);

        //            //}
        //        //}
        //        //if (!string.IsNullOrEmpty(IsRecommend))
        //        //{
        //        //    bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsRecommend", IsRecommend)), BooleanClause.Occur.MUST);
        //        //}
        //    }
        //    if (!string.IsNullOrEmpty(info.CityPY))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityPY", info.CityPY)), BooleanClause.Occur.MUST);

        //    }
        //    if (!string.IsNullOrEmpty(info.SearchType))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("SearchType", info.SearchType)), BooleanClause.Occur.MUST);

        //    }
        //    if (!string.IsNullOrEmpty(info.PropertyType) && info.PageSize != "2")
        //    {
        //        if (info.PropertyType != "all")
        //        {

                  
        //            if (info.SearchType == "esf" && info.PropertyType == "flats")
        //            {

        //                bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", "house")), BooleanClause.Occur.MUST);
        //                bq.Add(new TermQuery(new Lucene.Net.Index.Term("HouseType", "2")), BooleanClause.Occur.MUST);
                       
        //            }
        //            else
        //            {
        //                bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", info.PropertyType)), BooleanClause.Occur.MUST);
        //            }
        //        }
        //    }


        //    if (!string.IsNullOrEmpty(info.DistrictPY))
        //    {
        //        if (info.Type == "quyu")
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("DNamePY", info.DistrictPY)), BooleanClause.Occur.MUST);
        //        if (info.Type == "sub")
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("PId", info.DistrictPY)), BooleanClause.Occur.MUST);
        //    }

        //    if (!string.IsNullOrEmpty(info.BusinessPY))
        //    {
        //        if (info.BusinessPY == "cbd")
        //            info.BusinessPY = "CBD";
        //        if (info.Type == "quyu")
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("BNamePY", info.BusinessPY)), BooleanClause.Occur.MUST);
        //        if (info.Type == "sub")
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("TId", info.BusinessPY)), BooleanClause.Occur.MUST);
        //    }

        //    if (!string.IsNullOrEmpty(info.RentType))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("RentType", info.RentType)), BooleanClause.Occur.MUST);

        //    }

        //    if (!string.IsNullOrEmpty(info.PriceBegin) || !string.IsNullOrEmpty(info.PriceEnd))
        //    {
        //        int pricebegin = string.IsNullOrEmpty(info.PriceBegin) ? 0 : int.Parse(info.PriceBegin);
        //        int priceend = string.IsNullOrEmpty(info.PriceEnd) || info.PriceEnd == "0" ? 99999 : int.Parse(info.PriceEnd);

        //        if (info.SearchType == "esf")
        //        {
        //            pricebegin = pricebegin * 10000;
        //            priceend = priceend * 10000;
        //        }

        //        bq.Add(NumericRangeQuery.NewDoubleRange("QuotedMinPrice", pricebegin, priceend, true, true), BooleanClause.Occur.MUST);

        //    }
        //    if (!string.IsNullOrEmpty(info.AreaBegin) || !string.IsNullOrEmpty(info.AreaEnd))
        //    {
        //        int areabegin = string.IsNullOrEmpty(info.AreaBegin) ? 0 : int.Parse(info.AreaBegin);
        //        int areaend = string.IsNullOrEmpty(info.AreaEnd) || info.AreaEnd == "0" ? 9999999 : int.Parse(info.AreaEnd);
        //        if (info.SearchType == "esf")
        //        {
        //            bq.Add(NumericRangeQuery.NewDoubleRange("BuildingArea", areabegin, areaend, true, true), BooleanClause.Occur.MUST);
        //        }
        //        else
        //        {
        //            bq.Add(NumericRangeQuery.NewDoubleRange("Area", areabegin, areaend, true, true), BooleanClause.Occur.MUST);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(info.Room))
        //    {
        //        string[] r = info.Room.Split(",".ToCharArray());
        //        if (r.Length >= 2)
        //        {
        //            bq.Add(NumericRangeQuery.NewIntRange("Room", int.Parse(r[0]), int.Parse(r[1]), true, true), BooleanClause.Occur.MUST);
        //        }
        //        else
        //        {
        //            bq.Add(NumericRangeQuery.NewIntRange("Room", int.Parse(r[0]), int.Parse(r[0]), true, true), BooleanClause.Occur.MUST);
        //        }


        //    }
        //    if (!string.IsNullOrEmpty(info.Facilities))
        //    {
        //        string[] fls = info.Facilities.Split(",".ToCharArray());
        //        foreach (string f in fls)
        //        {
        //            Lucene.Net.Search.Query query = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Facilities", analyzer).Parse(f);
        //            bq.Add(query, BooleanClause.Occur.MUST);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(info.Renovation))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("Renovation", info.Renovation)), BooleanClause.Occur.MUST);

        //    }


        //    if (!string.IsNullOrEmpty(info.PType))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("PType", info.PType)), BooleanClause.Occur.MUST);

        //    }
        //    if (!string.IsNullOrEmpty(info.IsOnePrice))
        //    {
        //        if (info.IsOnePrice == "1")
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsGuarantee", "1")), BooleanClause.Occur.MUST);
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("UserType", "0")), BooleanClause.Occur.MUST);
        //        }
        //        else if (info.IsOnePrice == "2")
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsDaiK", "1")), BooleanClause.Occur.MUST);

        //        }
        //        else if (info.IsOnePrice == "3")
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsPlat", "1")), BooleanClause.Occur.MUST);

        //        }
        //        else if (info.IsOnePrice == "4")
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsReal", "1")), BooleanClause.Occur.MUST);

        //        }
        //        else if (info.IsOnePrice == "5")
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSheng", "1")), BooleanClause.Occur.MUST);
        //        }
        //        else if (info.IsOnePrice == "6")
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsPlat", "1")), BooleanClause.Occur.MUST);
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(info.Orientation))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("Orientation", info.Orientation)), BooleanClause.Occur.MUST);

        //    }


        //    if (!string.IsNullOrEmpty(info.UserType))
        //    {
        //        //bq.Add(new TermQuery(new Lucene.Net.Index.Term("UserType", info.UserType)), BooleanClause.Occur.MUST);
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSVip", info.UserType == "1" ? "0" : "1")), BooleanClause.Occur.MUST);
        //    }
        //    else
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSVip", "1")), BooleanClause.Occur.MUST);
        //    }
        //    if (!string.IsNullOrEmpty(info.SearchKeyword))
        //    {
        //        //Lucene.Net.Search.Query query = new Lucene.Net.QueryParsers.MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, new string[] { "VName", "Address","Title" }, analyzer).Parse(info.SearchKeyword);
        //        //bq.Add(query, BooleanClause.Occur.MUST);
        //        BooleanQuery qq = new BooleanQuery();
        //        Query q1 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "VName", analyzer).Parse(info.SearchKeyword);
        //        Query q2 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Address", analyzer).Parse(info.SearchKeyword);
        //        Query q3 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Title", analyzer).Parse(info.SearchKeyword);
        //        qq.Add(q1, BooleanClause.Occur.SHOULD);
        //        qq.Add(q2, BooleanClause.Occur.SHOULD);
        //        qq.Add(q3, BooleanClause.Occur.SHOULD);
        //        bq.Add(qq, BooleanClause.Occur.MUST);
        //    }



        //    if (!string.IsNullOrEmpty(info.HouseType))
        //    {
        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("HouseType", info.HouseType)), BooleanClause.Occur.MUST);

        //    }
        //    return bq;
        //    #endregion
        //}

        //public Sort GetSort()
        //{
        //    Sort sort = null;
        //    string Name = "";
        //    bool Order = false;
        //    if (!string.IsNullOrEmpty(info.OrderByName))
        //    {
        //        if (info.OrderByName == "0")
        //        {
        //            Name = "UpdateTime";
        //            Order = false;

        //            SortField[] sf = { new SortField(Name, SortField.LONG, Order) };
        //            sort = new Sort(sf);

        //        }
        //        if (info.OrderByName == "1")
        //        {
        //            Name = "UpdateTime";
        //            Order = true;
                   
        //                SortField[] sf = { new SortField(Name, SortField.LONG, Order) };
        //                sort = new Sort(sf);
                   
        //        }
        //        if (info.OrderByName == "2")
        //        {
        //            Name = "QuotedMinPrice";
        //            Order = false;

        //            SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
        //            sort = new Sort(sf);

        //        }
        //        if (info.OrderByName == "3")
        //        {
        //            Name = "QuotedMinPrice";
        //            Order = true;

        //            SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
        //            sort = new Sort(sf);

        //        }
        //        if (info.OrderByName == "4")
        //        {
        //            if (info.SearchType == "esf")
        //                Name = "BuildingArea";
        //            else
        //                Name = "Area";
        //            Order = false;
        //            if (info.IsOnePrice == "2" && info.UserType == "1")
        //            {
        //                SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order), new SortField("Price", SortField.DOUBLE, false) };
        //                sort = new Sort(sf);
        //            }
        //            else
        //            {
        //                SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
        //                sort = new Sort(sf);
        //            }
        //        }
        //        if (info.OrderByName == "5")
        //        {
        //            if (info.SearchType == "esf")
        //                Name = "BuildingArea";
        //            else
        //                Name = "Area";
        //            Order = true;

        //            if (info.IsOnePrice == "2" && info.UserType == "1")
        //            {
        //                SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order), new SortField("Price", SortField.DOUBLE, false) };
        //                sort = new Sort(sf);
        //            }
        //            else
        //            {
        //                SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
        //                sort = new Sort(sf);
        //            }
        //        }
        //        if (info.OrderByName == "6")
        //        {

        //            Name = "HouseScore";
        //            Order = true;

        //            if (info.IsOnePrice == "2" && info.UserType == "1")
        //            {
        //                SortField[] sf = { new SortField(Name, SortField.INT, Order), new SortField("Price", SortField.DOUBLE, false) };
        //                sort = new Sort(sf);
        //            }
        //            else
        //            {
        //                SortField[] sf = { new SortField(Name, SortField.INT, Order) };
        //                sort = new Sort(sf);
        //            }
        //        }

        //    }
               
        //    else
        //    {
        //        if (info.SearchType == "esf")
        //        {
        //            SortField[] sf = { new SortField("IsReturn", SortField.STRING, true), new SortField("UpdateTime", SortField.LONG, true) };
        //            sort = new Sort(sf);
        //        }
        //        else
        //        {
        //            SortField[] sf = { new SortField("IsGuarantee", SortField.STRING, true),  new SortField("UpdateTime", SortField.LONG, true) };
        //            sort = new Sort(sf);
        //        }
               
        //    }
        //    return sort;
        //}


        //public List<House> GetList(IndexSearcher indexSearcher, BooleanQuery bq, Sort sort, out int count)
        //{
        //    TopFieldDocs topdocs = null;

        //    topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex), sort);
        //    ScoreDoc[] hits = topdocs.scoreDocs;

        //    count = topdocs.totalHits;
        //    int bi = Convert.ToInt32(info.PageSize) * (Convert.ToInt32(info.PageIndex) - 1);
        //    int ei = Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex);
        //    List<House> list = new List<House>();
        //    if (hits.Length > 0 && bi < count)
        //    {
        //        for (int i = bi; i < ei; i++)
        //        {
        //            if (i >= count)
        //                break;
        //            House result = new House();
        //            ScoreDoc doc = hits[i];
        //            result.SearchType = indexSearcher.Doc(hits[i].doc).Get("SearchType");
        //            result.PType = indexSearcher.Doc(doc.doc).Get("PType");
        //            result.UserType = indexSearcher.Doc(doc.doc).Get("UserType");
        //            result.Id = indexSearcher.Doc(doc.doc).Get("Id");
        //            result.UId = indexSearcher.Doc(doc.doc).Get("UId");
        //            result.TId = indexSearcher.Doc(doc.doc).Get("TId");
        //            result.PId = indexSearcher.Doc(doc.doc).Get("PId");
        //            result.CityId = indexSearcher.Doc(doc.doc).Get("CityId");
        //            result.CityName = indexSearcher.Doc(doc.doc).Get("CityName");
        //            result.CityPY = indexSearcher.Doc(doc.doc).Get("CityPY");
        //            result.DNamePY = indexSearcher.Doc(doc.doc).Get("DNamePY");
        //            result.BNamePY = indexSearcher.Doc(doc.doc).Get("BNamePY");
        //            result.PropertyType = indexSearcher.Doc(doc.doc).Get("PropertyType");
        //            result.HouseType = indexSearcher.Doc(doc.doc).Get("HouseType");
        //            result.DName = indexSearcher.Doc(doc.doc).Get("DName");
        //            result.BName = indexSearcher.Doc(doc.doc).Get("BName");
        //            result.VName = indexSearcher.Doc(doc.doc).Get("VName");
        //            result.RentType = indexSearcher.Doc(doc.doc).Get("RentType");

        //            result.Room = indexSearcher.Doc(doc.doc).Get("Room");
        //            result.Hall = indexSearcher.Doc(doc.doc).Get("Hall");
        //            result.Area = indexSearcher.Doc(doc.doc).Get("Area");
        //            result.Renovation = indexSearcher.Doc(doc.doc).Get("Renovation");
        //            result.TheFloar = indexSearcher.Doc(doc.doc).Get("TheFloar");
        //            result.AllFloar = indexSearcher.Doc(doc.doc).Get("AllFloar");
        //            result.Title = indexSearcher.Doc(doc.doc).Get("Title");

        //            result.PictureUrl = indexSearcher.Doc(doc.doc).Get("PictureUrl");
        //            result.Price = indexSearcher.Doc(doc.doc).Get("QuotedMinPrice");
        //            result.CompanyName = indexSearcher.Doc(doc.doc).Get("CompanyName");
        //            result.UName = indexSearcher.Doc(doc.doc).Get("UName");
        //            result.IsAgency = indexSearcher.Doc(doc.doc).Get("IsAgency");
        //            result.IsGuarantee = indexSearcher.Doc(doc.doc).Get("IsGuarantee");
        //            result.IsSVip = indexSearcher.Doc(doc.doc).Get("IsSVip");
        //            result.BuildingArea = indexSearcher.Doc(doc.doc).Get("BuildingArea");
        //            result.UpdateTime = indexSearcher.Doc(doc.doc).Get("UpdateTime");

        //            result.IsSheng = indexSearcher.Doc(doc.doc).Get("IsSheng");
        //            result.IsRecommend = indexSearcher.Doc(doc.doc).Get("IsRecommend");
        //            result.picNum = indexSearcher.Doc(doc.doc).Get("picNum");
        //            result.IsPlat = indexSearcher.Doc(doc.doc).Get("IsPlat");
        //            result.IsDaiK = indexSearcher.Doc(doc.doc).Get("IsDaiK");
        //            result.IsReal = indexSearcher.Doc(doc.doc).Get("IsReal");
        //            result.Orientation = indexSearcher.Doc(doc.doc).Get("Orientation");
        //            if (list.Count(r => r.Id == result.Id) == 0)
        //                list.Add(result);
        //        }
        //    }
        //    return list;
        //}

        //public void SearchAdvert(string conditions, Relusts<Summary, House, House> results,bool isb)
        //{

        //    if (info == null)
        //        return;
        //    Analyzer analyzer = null;
        //    Lucene.Net.Store.Directory directory = null;
        //    IndexSearcher indexSearcher = null;
        //    //IndexSearcher indexSearcher = new IndexSearcher(IndexPath);


        //    try
        //    {
        //        analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //        directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexAdvertPath));
        //        indexSearcher = new IndexSearcher(directory, true);
        //        #region 固定值字段

        //        BooleanQuery bq = new BooleanQuery();
        //        if (string.IsNullOrEmpty(conditions))
        //        {
        //            bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
        //        }
        //        if (!string.IsNullOrEmpty(info.SearchType))
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("SearchType", info.SearchType)), BooleanClause.Occur.MUST);

        //        }
        //        if (!string.IsNullOrEmpty(info.CityPY))
        //        {
                   
        //                bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaCityPY", info.CityPY)), BooleanClause.Occur.MUST);

                    
        //        }
        //        //if (!string.IsNullOrEmpty(info.PropertyType))
        //        //{
        //        //    if (info.PropertyType != "all")
        //        //    {

        //        //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", info.PropertyType)), BooleanClause.Occur.MUST);

        //        //    }
        //        //}
        //        //if (isb)
        //        //{
        //        //    if (!string.IsNullOrEmpty(info.CityPY))
        //        //    {
        //        //        
        //        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaCityPY", info.CityPY)), BooleanClause.Occur.MUST);
                          
        //        //        
        //        //    }
        //        //}
        //        //else
        //        //{

                  

        //            //if (!string.IsNullOrEmpty(info.CityPY) && string.IsNullOrEmpty(info.DistrictPY) && string.IsNullOrEmpty(info.BusinessPY))
        //            //{
        //            //    {
        //            //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaPY", info.CityPY)), BooleanClause.Occur.MUST);
        //            //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaType", "0")), BooleanClause.Occur.MUST);
        //            //    }
        //            //}
        //            //else if (!string.IsNullOrEmpty(info.DistrictPY) && string.IsNullOrEmpty(info.BusinessPY))
        //            //{
        //            //    if (info.Type == "quyu")
        //            //    {
        //            //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaPY", info.DistrictPY)), BooleanClause.Occur.MUST);
        //            //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaType", "1")), BooleanClause.Occur.MUST);
        //            //    }
        //            //}

        //            //else if (!string.IsNullOrEmpty(info.BusinessPY))
        //            //{
        //            //    if (info.BusinessPY == "cbd")
        //            //        info.BusinessPY = "CBD";
        //            //    if (info.Type == "quyu")
        //            //    {
        //            //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaPY", info.BusinessPY)), BooleanClause.Occur.MUST);
        //            //        bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaType", "2")), BooleanClause.Occur.MUST);
        //            //    }

        //            //}
        //       // }

               
             

        //        #endregion

        //        #region 范围值字段



        //        #endregion

        //        #region 排序字段
        //        Sort sort = new Sort();

        //        SortField[] sf = { new SortField("AdvertCreateTime", SortField.LONG, false) };
        //        sort = new Sort(sf);


        //        #endregion

        //        #region 查询对象


        //        #endregion

        //        #region 特殊字段查询

        //        #endregion

        //        #region 查询及结果


        //        int hitscount = 0;
        //        //if (isb)
        //        //{
        //        //    List<House> nlist= GetList(indexSearcher, bq, sort, out hitscount);
        //        //    foreach(House h in nlist)
        //        //    {
        //        //        if (results.Agent.Count < int.Parse(info.PageSize)&&results.Agent.Count (r=>r.Id==h.Id)==0)
        //        //        {
        //        //            results.Agent.Add(h);
        //        //        }
                       
                       
        //        //    }
                    
        //        //}
        //        //else
        //        //{

        //            results.Agent = GetList(indexSearcher, bq, sort, out hitscount);
        //       // }



        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //    }
        //    finally
        //    {
        //        if (directory != null)
        //            directory.Close();
        //        if (indexSearcher != null)
        //            indexSearcher.Close();
        //        if (analyzer != null)
        //            analyzer.Close();
        //    }

        //}
        /// <summary>
        /// 广告数据为空时返回数据
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="results"></param>
        //public void SearchAdvertCopy(string conditions, Relusts<Summary, House, House> results)
        //{

        //    if (info == null)
        //        return;
        //    Analyzer analyzer = null;
        //    Lucene.Net.Store.Directory directory = null;
        //    IndexSearcher indexSearcher = null;
        //    //IndexSearcher indexSearcher = new IndexSearcher(IndexPath);


        //    try
        //    {
        //        analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //        directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(IndexPath));
        //        indexSearcher = new IndexSearcher(directory, true);
        //        #region 固定值字段

        //        BooleanQuery bq = new BooleanQuery();
        //        if (string.IsNullOrEmpty(conditions))
        //        {
        //            bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
        //        }
        //        if (!string.IsNullOrEmpty(info.SearchType))
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("SearchType", info.SearchType)), BooleanClause.Occur.MUST);

        //        }
        //        if (!string.IsNullOrEmpty(info.PropertyType) && info.PageSize != "2")
        //        {
        //            if (info.PropertyType != "all")
        //            {

        //                bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", info.PropertyType)), BooleanClause.Occur.MUST);

        //            }
        //        }
        //        if (!string.IsNullOrEmpty(info.CityPY))
        //        {
        //            bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityPY", info.CityPY)), BooleanClause.Occur.MUST);

        //        }





        //        #endregion

        //        #region 范围值字段



        //        #endregion

        //        #region 排序字段
        //        Sort sort = new Sort();

        //        SortField[] sf = { new SortField("UpdateTime", SortField.LONG, true) };
        //        sort = new Sort(sf);


        //        #endregion

        //        #region 查询对象


        //        #endregion

        //        #region 特殊字段查询

        //        #endregion

        //        #region 查询及结果


        //        int hitscount = 0;


        //        results.Agent = GetList(indexSearcher, bq, sort, out hitscount);




        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex.Message);
        //    }
        //    finally
        //    {
        //        if (directory != null)
        //            directory.Close();
        //        if (indexSearcher != null)
        //            indexSearcher.Close();
        //        if (analyzer != null)
        //            analyzer.Close();
        //    }

        //}

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
    public class FieldInfo
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public BooleanClause.Occur Occur { get; set; }
    }
}