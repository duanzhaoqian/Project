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

namespace TXSearch.Advert
{
    /// <summary>
    /// GetAdvertIndex 的摘要说明
    /// </summary>
    public class GetAdvertIndex : IHttpHandler
    {
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string IndexPath = BasePath + "LongHouseIndex/";
        public static string IndexAdvertPath = BasePath + "AdvertIndex/";
        SearchRentHouseConditionInfo info=new SearchRentHouseConditionInfo ();
        SearchData search = new SearchData();
        public void ProcessRequest(HttpContext context)
        {
            string condition = context.Request.Params["condition"];
            if (!string.IsNullOrEmpty(condition))
            {
                info.CityPY = condition.Split("-".ToCharArray())[0];
                info.SearchType = condition.Split("-".ToCharArray())[1];
            }
            string advertconfig = search.GetAdvertConfigData(info);
            context.Response.ContentType = "text/xml";
           List<House> list= search.SearchAdvert(context.Server.UrlDecode(condition), null, info, 1, IndexAdvertPath, advertconfig);
           info.PageIndex = "1";
           info.PageSize = (Convert.ToInt16(info.PageSize) - list.Count).ToString();
           Relusts<Summary, House> relusts = new Relusts<Summary, House>();
           relusts.Summary = new Summary();
           IList<House> listhouse = search.Search(context.Server.UrlDecode(condition), "1", null, relusts, IndexPath, info).Houses;

           foreach (House h in listhouse)
           {
               list.Add(h);
           }
           
            //  int countp = int.Parse(info.PageSize);
            //if (relusts.Agent.Count < countp)
            //{
            //if ( !string.IsNullOrEmpty(info.DistrictPY) || !string.IsNullOrEmpty(info.BusinessPY))
            //{
            //SearchAdvert(context.Server.UrlDecode(condition), relusts, true);

            // }
            //}
           string result = TXCommons.Xml.ComObjOrXml.SerializeWrite<List<House>>(list);

            context.Response.Write(result);
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