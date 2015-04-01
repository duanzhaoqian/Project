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
using System.Configuration;
using ServiceStack;


public class SearchData
{
    #region 广告搜索

    public List<House> SearchAdvert(string conditions, Relusts<Summary, House> results, SearchRentHouseConditionInfo info, int buytype, string IndexPath, string advertconfig)
    {

        if (info == null)
            return null;
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

            BooleanQuery bq = GetQuerydvert(analyzer, conditions, info, buytype);

            #endregion

            #region 范围值字段



            #endregion

            #region 排序字段
            Sort sort = null;

            sort = GetSortdvert();


            #endregion

            #region 查询对象


            #endregion

            #region 特殊字段查询

            #endregion

            #region 查询及结果


            int hitscount = 0;



            return GetAdvertList(indexSearcher, bq, sort, info, advertconfig, buytype, out hitscount);
            #endregion
        }
        catch (Exception ex)
        {
            //logger.Error(ex.Message);
            return null;
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

    public BooleanQuery GetQuerydvert(Analyzer analyzer, string conditions, SearchRentHouseConditionInfo info, int buytype)
    {
        #region 固定值字段

        BooleanQuery bq = new BooleanQuery();
        if (string.IsNullOrEmpty(conditions))
        {
            bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
        }
        else
        {


            if (!string.IsNullOrEmpty(info.SearchType))
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("SearchType", info.SearchType)), BooleanClause.Occur.MUST);

            }
            if (!string.IsNullOrEmpty(info.CityPY) && string.IsNullOrEmpty(info.DistrictPY) && string.IsNullOrEmpty(info.BusinessPY))
            {
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaPY", info.CityPY)), BooleanClause.Occur.MUST);
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaType", "0")), BooleanClause.Occur.MUST);
                }
            }
            else if (!string.IsNullOrEmpty(info.DistrictPY) && string.IsNullOrEmpty(info.BusinessPY))
            {
                if (info.Type == "quyu")
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaPY", info.DistrictPY)), BooleanClause.Occur.MUST);
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaType", "1")), BooleanClause.Occur.MUST);
                }
            }

            else if (!string.IsNullOrEmpty(info.BusinessPY))
            {
                if (info.BusinessPY == "cbd")
                    info.BusinessPY = "CBD";
                if (info.Type == "quyu")
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaPY", info.BusinessPY)), BooleanClause.Occur.MUST);
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("AreaType", "2")), BooleanClause.Occur.MUST);
                }

            }
            if (!string.IsNullOrEmpty(info.PropertyType) && buytype != 1)
            {

                bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", info.PropertyType)), BooleanClause.Occur.MUST);

            }
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("BuyType", buytype.ToString())), BooleanClause.Occur.MUST);
        }
        return bq;
        #endregion
    }

    public Sort GetSortdvert()
    {
        #region 排序字段
        Sort sort = new Sort();

        SortField[] sf = { new SortField("AdvertCreateTime", SortField.LONG, false) };
        sort = new Sort(sf);

        return sort;
        #endregion
    }

    public List<House> GetAdvertList(IndexSearcher indexSearcher, BooleanQuery bq, Sort sort, SearchRentHouseConditionInfo info, string advertconfig, int buytype, out int count)
    {
        TopFieldDocs topdocs = null;
        info.PageIndex = "1";
        info.PageSize = "1000";
        topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex), sort);
        ScoreDoc[] hits = topdocs.scoreDocs;

        count = topdocs.totalHits;
        info = SetInfo(info, advertconfig, buytype, count);
        int bi = Convert.ToInt32(info.PageSize) * (Convert.ToInt32(info.PageIndex) - 1);
        int ei = Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex);
        List<House> list = new List<House>();
        if (hits.Length > 0 && bi < count)
        {
            for (int i = bi; i < ei; i++)
            {
                if (i >= count)
                    break;
                House result = new House();
                ScoreDoc doc = hits[i];
                result.SearchType = indexSearcher.Doc(hits[i].doc).Get("SearchType");
                result.PType = indexSearcher.Doc(doc.doc).Get("PType");
                result.UserType = indexSearcher.Doc(doc.doc).Get("UserType");
                result.Id = indexSearcher.Doc(doc.doc).Get("Id");
                result.UId = indexSearcher.Doc(doc.doc).Get("UId");
                result.TId = indexSearcher.Doc(doc.doc).Get("TId");
                result.PId = indexSearcher.Doc(doc.doc).Get("PId");
                result.CityId = indexSearcher.Doc(doc.doc).Get("CityId");
                result.CityName = indexSearcher.Doc(doc.doc).Get("CityName");
                result.CityPY = indexSearcher.Doc(doc.doc).Get("CityPY");
                result.DNamePY = indexSearcher.Doc(doc.doc).Get("DNamePY");
                result.BNamePY = indexSearcher.Doc(doc.doc).Get("BNamePY");
                result.PropertyType = indexSearcher.Doc(doc.doc).Get("PropertyType");
                result.HouseType = indexSearcher.Doc(doc.doc).Get("HouseType");
                result.DName = indexSearcher.Doc(doc.doc).Get("DName");
                result.BName = indexSearcher.Doc(doc.doc).Get("BName");
                result.VName = indexSearcher.Doc(doc.doc).Get("VName");
                result.RentType = indexSearcher.Doc(doc.doc).Get("RentType");

                result.Room = indexSearcher.Doc(doc.doc).Get("Room");
                result.Hall = indexSearcher.Doc(doc.doc).Get("Hall");
                result.Area = indexSearcher.Doc(doc.doc).Get("Area");
                result.Renovation = indexSearcher.Doc(doc.doc).Get("Renovation");
                result.TheFloar = indexSearcher.Doc(doc.doc).Get("TheFloar");
                result.AllFloar = indexSearcher.Doc(doc.doc).Get("AllFloar");
                result.Title = indexSearcher.Doc(doc.doc).Get("Title");

                result.PictureUrl = indexSearcher.Doc(doc.doc).Get("PictureUrl");
                result.Price = indexSearcher.Doc(doc.doc).Get("QuotedMinPrice");
                result.CompanyName = indexSearcher.Doc(doc.doc).Get("CompanyName");
                result.UName = indexSearcher.Doc(doc.doc).Get("UName");
                result.IsAgency = indexSearcher.Doc(doc.doc).Get("IsAgency");
                result.IsGuarantee = indexSearcher.Doc(doc.doc).Get("IsGuarantee");
                result.IsSVip = indexSearcher.Doc(doc.doc).Get("IsSVip");
                result.BuildingArea = indexSearcher.Doc(doc.doc).Get("BuildingArea");
                result.UpdateTime = indexSearcher.Doc(doc.doc).Get("UpdateTime");

                result.IsSheng = indexSearcher.Doc(doc.doc).Get("IsSheng");
                result.IsRecommend = indexSearcher.Doc(doc.doc).Get("IsRecommend");
                result.picNum = indexSearcher.Doc(doc.doc).Get("picNum");
                result.IsPlat = indexSearcher.Doc(doc.doc).Get("IsPlat");
                result.IsDaiK = indexSearcher.Doc(doc.doc).Get("IsDaiK");
                result.IsReal = indexSearcher.Doc(doc.doc).Get("IsReal");
                result.Orientation = indexSearcher.Doc(doc.doc).Get("Orientation");
                if (list.Count(r => r.Id == result.Id) == 0)
                    list.Add(result);
            }
        }
        return list;
    }

    public string GetAdvertConfigData(SearchRentHouseConditionInfo info)
    {

        string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        string searchstr = "";
        string resultstr = "10-10-10-10-10,25-25-25-25-25,40-40-40-40-40";
        try
        {
            if (!string.IsNullOrEmpty(info.CityPY) && string.IsNullOrEmpty(info.DistrictPY) && string.IsNullOrEmpty(info.BusinessPY))
            {
                searchstr = info.CityPY;
            }
            else if (!string.IsNullOrEmpty(info.DistrictPY) && string.IsNullOrEmpty(info.BusinessPY))
            {
                if (info.Type == "quyu")
                {
                    searchstr = info.DistrictPY;
                }
            }

            else if (!string.IsNullOrEmpty(info.BusinessPY))
            {
                if (info.BusinessPY == "cbd")
                    info.BusinessPY = "CBD";
                if (info.Type == "quyu")
                {
                    searchstr = info.BusinessPY;
                }

            }
            if (!string.IsNullOrEmpty(searchstr))
            {
                //string key = "AdvertConfigure_AllData";
                //DataTable dt = null;
                //dt = TXCommons.PictureModular.Redis.GetValue<DataTable>(key, FunctionTypeEnum.WebDBData, 0);
                //if (dt == null)
                //{
                //    string sql = " select ShowCount_q,ShowCount_c,ShowCount_b,AreaPY from AdvertConfigure(nolock) ";
                //    // where AreaPY=" + searchstr
                //    DataSet dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
                //    if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                //    {
                //        dt = dataset.Tables[0];
                //        TXCommons.PictureModular.Redis.SetValue<DataTable>(key, dt, null, FunctionTypeEnum.WebDBData, 0);
                //        //DataRow row = dataset.Tables[0].Rows[0];
                //        //resultstr = row["ShowCount_q"].ToString() + "," + row["ShowCount_c"].ToString() + "," + row["ShowCount_b"].ToString();
                //    }
                //}
                //DataRow[] rows = dt.Select("AreaPY", searchstr);
                //if (rows.Length > 0)
                //{
                //    DataRow row = rows[0];
                //    resultstr = row["ShowCount_q"].ToString() + "," + row["ShowCount_c"].ToString() + "," + row["ShowCount_b"].ToString();
                //}
            }
        }
        catch
        {

        }
        return resultstr;
    }

    public SearchRentHouseConditionInfo SetInfo(SearchRentHouseConditionInfo info, string advertconfig, int type, int count)
    {
        string[] a = advertconfig.Split(",".ToCharArray());
        string b = a[type - 1];
        string[] c = b.Split("-".ToCharArray());
        int scount = 50;
        if (info != null )
        {
            int p =0;
            if (!string.IsNullOrEmpty(info.PropertyType))
            {
                p = GetPropertyType(info.PropertyType.Trim());
            }
            scount = int.Parse(c[p]);
        }

        if (type != 3)
        {
            if (scount < count)
            {
                info.PageIndex = "1";
                info.PageSize = count.ToString();
            }
            else
            {
                info.PageIndex = "1";
                info.PageSize = scount.ToString();
            }
        }
        else
        {
            info.PageSize = scount.ToString();
            int e = Convert.ToInt16(count / int.Parse(info.PageSize)) + 1;
            int ee = DateTime.Now.Hour % e;
            info.PageIndex = (ee + 1).ToString();

        }



        return info;
    }

    public int GetPropertyType(string str)
    {
        int res = 0;
        switch (str.Trim())
        {
            case "house":
                res = 0;
                break;
            case "flats":
                res = 1;
                break;
            case "villa":
                res = 2;
                break;
            case "shop":
                res = 3;
                break;
            case "office":
                res = 4;
                break;
        }
        return res;
    }


    #endregion

    #region 房源搜索
    public Relusts<Summary, House> Search(string conditions, string issvip, string IsRecommend, Relusts<Summary, House> results, string IndexPath, SearchRentHouseConditionInfo info)
    {

        if (info == null)
            return null;
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

            BooleanQuery bq = GetQuery(analyzer, conditions, issvip, IsRecommend,info);

            #endregion

            #region 范围值字段



            #endregion

            #region 排序字段
            Sort sort = null;

            sort = GetSort(info);


            #endregion

            #region 查询对象


            #endregion

            #region 特殊字段查询

            #endregion

            #region 查询及结果


            int hitscount = 0;



            results.Houses = GetList(indexSearcher, bq, sort,info, out hitscount);
            if (hitscount > 0)
            {
                results.Summary.TotalRecords = (hitscount > 100 * Convert.ToInt32(info.PageSize)) ? 100 * Convert.ToInt32(info.PageSize) : hitscount;
                results.Summary.PageSize = int.Parse(info.PageSize);
                results.Summary.PageIndex = int.Parse(info.PageIndex);
                int totalpage = hitscount % Convert.ToInt32(info.PageSize) == 0 ? (hitscount / Convert.ToInt32(info.PageSize)) : ((hitscount / Convert.ToInt32(info.PageSize)) + 1);
                results.Summary.TotalPage = totalpage;
                results.Summary.RealRecords = hitscount;
            }


            return results;



            #endregion
        }
        catch (Exception ex)
        {
            //logger.Error(ex.Message);
            return null;
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




    public BooleanQuery GetQuery(Analyzer analyzer, string conditions, string issvip, string IsRecommend, SearchRentHouseConditionInfo info)
    {
        #region 固定值字段

        BooleanQuery bq = new BooleanQuery();
        if (string.IsNullOrEmpty(conditions))
        {
            bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
        }
        else
        {
            //if (info.SearchType != "esf")
            //{
            //if (!string.IsNullOrEmpty(issvip))
            //{
            //    bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSVip", issvip)), BooleanClause.Occur.MUST);

            //}
            //}
            //if (!string.IsNullOrEmpty(IsRecommend))
            //{
            //    bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsRecommend", IsRecommend)), BooleanClause.Occur.MUST);
            //}
        }
        if (!string.IsNullOrEmpty(info.CityPY))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityPY", info.CityPY)), BooleanClause.Occur.MUST);

        }
        if (!string.IsNullOrEmpty(info.SearchType))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("SearchType", info.SearchType)), BooleanClause.Occur.MUST);

        }
        if (!string.IsNullOrEmpty(info.PropertyType) && info.PageSize != "2")
        {
            if (info.PropertyType != "all")
            {


                if (info.SearchType == "esf" && info.PropertyType == "flats")
                {

                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", "house")), BooleanClause.Occur.MUST);
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("HouseType", "2")), BooleanClause.Occur.MUST);

                }
                else
                {
                    bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", info.PropertyType)), BooleanClause.Occur.MUST);
                }
            }
        }


        if (!string.IsNullOrEmpty(info.DistrictPY))
        {
            if (info.Type == "quyu")
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("DNamePY", info.DistrictPY)), BooleanClause.Occur.MUST);
            if (info.Type == "sub")
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("PId", info.DistrictPY)), BooleanClause.Occur.MUST);
        }

        if (!string.IsNullOrEmpty(info.BusinessPY))
        {
            if (info.BusinessPY == "cbd")
                info.BusinessPY = "CBD";
            if (info.Type == "quyu")
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("BNamePY", info.BusinessPY)), BooleanClause.Occur.MUST);
            if (info.Type == "sub")
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("TId", info.BusinessPY)), BooleanClause.Occur.MUST);
        }

        if (!string.IsNullOrEmpty(info.RentType))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("RentType", info.RentType)), BooleanClause.Occur.MUST);

        }

        if (!string.IsNullOrEmpty(info.PriceBegin) || !string.IsNullOrEmpty(info.PriceEnd))
        {
            int pricebegin = string.IsNullOrEmpty(info.PriceBegin) ? 0 : int.Parse(info.PriceBegin);
            int priceend = string.IsNullOrEmpty(info.PriceEnd) || info.PriceEnd == "0" ? 99999 : int.Parse(info.PriceEnd);

            if (info.SearchType == "esf")
            {
                pricebegin = pricebegin * 10000;
                priceend = priceend * 10000;
            }

            bq.Add(NumericRangeQuery.NewDoubleRange("QuotedMinPrice", pricebegin, priceend, true, true), BooleanClause.Occur.MUST);

        }
        if (!string.IsNullOrEmpty(info.AreaBegin) || !string.IsNullOrEmpty(info.AreaEnd))
        {
            int areabegin = string.IsNullOrEmpty(info.AreaBegin) ? 0 : int.Parse(info.AreaBegin);
            int areaend = string.IsNullOrEmpty(info.AreaEnd) || info.AreaEnd == "0" ? 9999999 : int.Parse(info.AreaEnd);
            if (info.SearchType == "esf")
            {
                bq.Add(NumericRangeQuery.NewDoubleRange("BuildingArea", areabegin, areaend, true, true), BooleanClause.Occur.MUST);
            }
            else
            {
                bq.Add(NumericRangeQuery.NewDoubleRange("Area", areabegin, areaend, true, true), BooleanClause.Occur.MUST);
            }
        }
        if (!string.IsNullOrEmpty(info.Room))
        {
            string[] r = info.Room.Split(",".ToCharArray());
            if (r.Length >= 2)
            {
                bq.Add(NumericRangeQuery.NewIntRange("Room", int.Parse(r[0]), int.Parse(r[1]), true, true), BooleanClause.Occur.MUST);
            }
            else
            {
                bq.Add(NumericRangeQuery.NewIntRange("Room", int.Parse(r[0]), int.Parse(r[0]), true, true), BooleanClause.Occur.MUST);
            }


        }
        if (!string.IsNullOrEmpty(info.Facilities))
        {
            string[] fls = info.Facilities.Split(",".ToCharArray());
            foreach (string f in fls)
            {
                Lucene.Net.Search.Query query = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Facilities", analyzer).Parse(f);
                bq.Add(query, BooleanClause.Occur.MUST);
            }
        }
        if (!string.IsNullOrEmpty(info.Renovation))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("Renovation", info.Renovation)), BooleanClause.Occur.MUST);

        }


        if (!string.IsNullOrEmpty(info.PType))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("PType", info.PType)), BooleanClause.Occur.MUST);

        }
        if (!string.IsNullOrEmpty(info.IsOnePrice))
        {
            if (info.IsOnePrice == "1")
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsGuarantee", "1")), BooleanClause.Occur.MUST);
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("UserType", "0")), BooleanClause.Occur.MUST);
            }
            else if (info.IsOnePrice == "2")
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsDaiK", "1")), BooleanClause.Occur.MUST);

            }
            else if (info.IsOnePrice == "3")
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsPlat", "1")), BooleanClause.Occur.MUST);

            }
            else if (info.IsOnePrice == "4")
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsReal", "1")), BooleanClause.Occur.MUST);

            }
            else if (info.IsOnePrice == "5")
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSheng", "1")), BooleanClause.Occur.MUST);
            }
            else if (info.IsOnePrice == "6")
            {
                bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsPlat", "1")), BooleanClause.Occur.MUST);
            }
        }
        if (!string.IsNullOrEmpty(info.Orientation))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("Orientation", info.Orientation)), BooleanClause.Occur.MUST);

        }


        if (!string.IsNullOrEmpty(info.UserType))
        {
            //bq.Add(new TermQuery(new Lucene.Net.Index.Term("UserType", info.UserType)), BooleanClause.Occur.MUST);
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSVip", info.UserType == "1" ? "0" : "1")), BooleanClause.Occur.MUST);
        }
        else
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("IsSVip", "1")), BooleanClause.Occur.MUST);
        }
        if (!string.IsNullOrEmpty(info.SearchKeyword))
        {
            //Lucene.Net.Search.Query query = new Lucene.Net.QueryParsers.MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, new string[] { "VName", "Address","Title" }, analyzer).Parse(info.SearchKeyword);
            //bq.Add(query, BooleanClause.Occur.MUST);
            BooleanQuery qq = new BooleanQuery();
            Query q1 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "VName", analyzer).Parse(info.SearchKeyword);
            Query q2 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Address", analyzer).Parse(info.SearchKeyword);
            Query q3 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "Title", analyzer).Parse(info.SearchKeyword);
            qq.Add(q1, BooleanClause.Occur.SHOULD);
            qq.Add(q2, BooleanClause.Occur.SHOULD);
            qq.Add(q3, BooleanClause.Occur.SHOULD);
            bq.Add(qq, BooleanClause.Occur.MUST);
        }



        if (!string.IsNullOrEmpty(info.BuildingType))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("BuildingType", info.BuildingType)), BooleanClause.Occur.MUST);

        }
        if (!string.IsNullOrEmpty(info.BuildingStructure))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("BuildingStructure", info.BuildingStructure)), BooleanClause.Occur.MUST);

        }

        if (!string.IsNullOrEmpty(info.BuildingYear))
        {
            string str = GetBuildingYearQuery(int.Parse(info.BuildingYear));
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("BuildingYear", str)), BooleanClause.Occur.MUST);
          
        }

        if (!string.IsNullOrEmpty(info.TheFloar))
        {
            int[] r = GetTheFloorQuery(int.Parse(info.TheFloar));

            bq.Add(NumericRangeQuery.NewIntRange("TheFloar", r[0], r[1], true, true), BooleanClause.Occur.MUST);
        }

        if (!string.IsNullOrEmpty(info.OfficeType))
        {
            bq.Add(new TermQuery(new Lucene.Net.Index.Term("OfficeType", info.OfficeType)), BooleanClause.Occur.MUST);

        }
        return bq;
        #endregion
    }

    public string GetBuildingYearQuery(int BuildingYear)
    {
        string str = "";
        switch (BuildingYear)
        {
            case 1: str = "2005年以后";
                    break;
            case 2: str = "2001~2005";
                    break;
            case 3: str = "1991~2000";
                    break;
            case 4: str = "1981~1990";
                    break;
            case 5: str = "1980年以前";
                    break;
            default: str = "";
                    break;
        }
        return str;
    }

    public int[] GetTheFloorQuery(int BuildingYear)
    {
        int[] ints = new int[2];
        switch (BuildingYear)
        {
            case 1: ints[0] = -10;
                ints[1] = 0;
                break;
            case 2: ints[0] = 0;
                ints[1] = 1;
                break;
            case 3: ints[0] = 1;
                ints[1] = 5;
                break;
            case 4: ints[0] = 6;
                ints[1] = 12;
                break;
            case 5: ints[0] = 13;
                ints[1] = 100;
                break;
            default: ints[0] = 0;
                ints[1] = 100;
                break;
        }
        return ints;
    }

    public Sort GetSort( SearchRentHouseConditionInfo info)
    {
        Sort sort = null;
        string Name = "";
        bool Order = false;
        if (!string.IsNullOrEmpty(info.OrderByName))
        {
            if (info.OrderByName == "0")
            {
                Name = "UpdateTime";
                Order = false;

                SortField[] sf = { new SortField(Name, SortField.LONG, Order) };
                sort = new Sort(sf);

            }
            if (info.OrderByName == "1")
            {
                Name = "UpdateTime";
                Order = true;

                SortField[] sf = { new SortField(Name, SortField.LONG, Order) };
                sort = new Sort(sf);

            }
            if (info.OrderByName == "2")
            {
                Name = "QuotedMinPrice";
                Order = false;

                SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
                sort = new Sort(sf);

            }
            if (info.OrderByName == "3")
            {
                Name = "QuotedMinPrice";
                Order = true;

                SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
                sort = new Sort(sf);

            }
            if (info.OrderByName == "4")
            {
                if (info.SearchType == "esf")
                    Name = "BuildingArea";
                else
                    Name = "Area";
                Order = false;
                if (info.IsOnePrice == "2" && info.UserType == "1")
                {
                    SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order), new SortField("Price", SortField.DOUBLE, false) };
                    sort = new Sort(sf);
                }
                else
                {
                    SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
                    sort = new Sort(sf);
                }
            }
            if (info.OrderByName == "5")
            {
                if (info.SearchType == "esf")
                    Name = "BuildingArea";
                else
                    Name = "Area";
                Order = true;

                if (info.IsOnePrice == "2" && info.UserType == "1")
                {
                    SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order), new SortField("Price", SortField.DOUBLE, false) };
                    sort = new Sort(sf);
                }
                else
                {
                    SortField[] sf = { new SortField(Name, SortField.DOUBLE, Order) };
                    sort = new Sort(sf);
                }
            }
            if (info.OrderByName == "6")
            {

                Name = "HouseScore";
                Order = true;

                if (info.IsOnePrice == "2" && info.UserType == "1")
                {
                    SortField[] sf = { new SortField(Name, SortField.INT, Order), new SortField("Price", SortField.DOUBLE, false) };
                    sort = new Sort(sf);
                }
                else
                {
                    SortField[] sf = { new SortField(Name, SortField.INT, Order) };
                    sort = new Sort(sf);
                }
            }

        }

        else
        {
            if (info.SearchType == "esf")
            {
                SortField[] sf = { new SortField("IsReturn", SortField.STRING, true), new SortField("UpdateTime", SortField.LONG, true) };
                sort = new Sort(sf);
            }
            else
            {
                SortField[] sf = { new SortField("IsDaiK", SortField.STRING, true), new SortField("UpdateTime", SortField.LONG, true) };
                sort = new Sort(sf);
            }

        }
        return sort;
    }


    public List<House> GetList(IndexSearcher indexSearcher, BooleanQuery bq, Sort sort, SearchRentHouseConditionInfo info, out int count)
    {
        TopFieldDocs topdocs = null;
        
        topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex), sort);
        ScoreDoc[] hits = topdocs.scoreDocs;
       
        count = topdocs.totalHits;
        int bi = Convert.ToInt32(info.PageSize) * (Convert.ToInt32(info.PageIndex) - 1);
        int ei = Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex);
        List<House> list = new List<House>();
        if (hits.Length > 0 && bi < count)
        {
            for (int i = bi; i < ei; i++)
            {
                if (i >= count)
                    break;
                House result = new House();
                ScoreDoc doc = hits[i];   
                result.SearchType = indexSearcher.Doc(hits[i].doc).Get("SearchType");
                result.PType = indexSearcher.Doc(doc.doc).Get("PType");
                result.UserType = indexSearcher.Doc(doc.doc).Get("UserType");
                result.Id = indexSearcher.Doc(doc.doc).Get("Id");
                result.UId = indexSearcher.Doc(doc.doc).Get("UId");
                result.TId = indexSearcher.Doc(doc.doc).Get("TId");
                result.PId = indexSearcher.Doc(doc.doc).Get("PId");
                result.CityId = indexSearcher.Doc(doc.doc).Get("CityId");
                result.CityName = indexSearcher.Doc(doc.doc).Get("CityName");
                result.CityPY = indexSearcher.Doc(doc.doc).Get("CityPY");
                result.DNamePY = indexSearcher.Doc(doc.doc).Get("DNamePY");
                result.BNamePY = indexSearcher.Doc(doc.doc).Get("BNamePY");
                result.PropertyType = indexSearcher.Doc(doc.doc).Get("PropertyType");
                result.HouseType = indexSearcher.Doc(doc.doc).Get("HouseType");
                result.DName = indexSearcher.Doc(doc.doc).Get("DName");
                result.BName = indexSearcher.Doc(doc.doc).Get("BName");
                result.VName = indexSearcher.Doc(doc.doc).Get("VName");
                result.RentType = indexSearcher.Doc(doc.doc).Get("RentType");

                result.Room = indexSearcher.Doc(doc.doc).Get("Room");
                result.Hall = indexSearcher.Doc(doc.doc).Get("Hall");
                result.Area = indexSearcher.Doc(doc.doc).Get("Area");
                result.Renovation = indexSearcher.Doc(doc.doc).Get("Renovation");
                result.TheFloar = indexSearcher.Doc(doc.doc).Get("TheFloar");
                result.AllFloar = indexSearcher.Doc(doc.doc).Get("AllFloar");
                result.Title = indexSearcher.Doc(doc.doc).Get("Title");

                result.PictureUrl = indexSearcher.Doc(doc.doc).Get("PictureUrl");
                result.Price = indexSearcher.Doc(doc.doc).Get("QuotedMinPrice");
                result.CompanyName = indexSearcher.Doc(doc.doc).Get("CompanyName");
                result.UName = indexSearcher.Doc(doc.doc).Get("UName");
                result.IsAgency = indexSearcher.Doc(doc.doc).Get("IsAgency");
                result.IsGuarantee = indexSearcher.Doc(doc.doc).Get("IsGuarantee");
                result.IsSVip = indexSearcher.Doc(doc.doc).Get("IsSVip");
                result.BuildingArea = indexSearcher.Doc(doc.doc).Get("BuildingArea");
                result.UpdateTime = indexSearcher.Doc(doc.doc).Get("UpdateTime");

                result.IsSheng = indexSearcher.Doc(doc.doc).Get("IsSheng");
                result.IsRecommend = indexSearcher.Doc(doc.doc).Get("IsRecommend");
                result.picNum = indexSearcher.Doc(doc.doc).Get("picNum");
                result.IsPlat = indexSearcher.Doc(doc.doc).Get("IsPlat");
                result.IsDaiK = indexSearcher.Doc(doc.doc).Get("IsDaiK");
                result.IsReal = indexSearcher.Doc(doc.doc).Get("IsReal");
                result.Orientation = indexSearcher.Doc(doc.doc).Get("Orientation");

                result.BuildingStructure = indexSearcher.Doc(doc.doc).Get("BuildingStructure");
                result.BuildingType = indexSearcher.Doc(doc.doc).Get("BuildingType");
                result.BuildingYear = indexSearcher.Doc(doc.doc).Get("BuildingYear");
              
                if (list.Count(r => r.Id == result.Id) == 0)
                    list.Add(result);
            }
        }
        return list;
    }

    #endregion
}
public class AdvertConfigureClass
{
    public string ShowCount_q { get; set; }
    public string ShowCount_c { get; set; }
    public string ShowCount_b { get; set; }
    public string AreaPY { get; set; }
}