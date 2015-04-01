using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Analysis;
using Commons.Sources;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using System.Data;
using System.Collections;
using TXCommons.Index;
using Lucene.Net.QueryParsers;
using TXCommons.PictureModular;
using Lucene.Net.Analysis.PanGu;
using PanGu;

namespace Commons
{
    public class LuceneOpt
    {
        //private string PremisesIndexPath = Profile.IniReadValue("TXPremisesService", "PremisesIndexPath");//ConfigurationManager.AppSettings["PremisesIndexPath"].ToString();
        //private string RankingIndexPath = Profile.IniReadValue("TXPremisesService", "RankingIndexPath");//ConfigurationManager.AppSettings["RankingIndexPath"].ToString();

        #region 添加/更新/删除楼盘索引
        private static object MyLock = new object();
        /// <summary>
        /// 重新生成所有索引信息
        /// </summary>
        /// <returns></returns>
        public int AddDocumentForPremiseses(int Id, bool isCreate = true)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                lock (MyLock)
                {
                    var lucene = MQHelper.GetLucenexmlInfomation(Id);

                    if (lucene == null || string.IsNullOrEmpty(lucene.Path))
                    {
                        Console.WriteLine("城市赞未开通！城市ID为：" + lucene.CityIds);
                        return 0;
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(lucene.Path))
                        {
                            System.IO.Directory.CreateDirectory(lucene.Path);
                        }
                    }
                    var premises = _dal.GetPremises(lucene.CityIds);
                    if (premises != null && premises.Count > 0)
                    {

                        //加载楼盘索引分词
                        PerFieldAnalyzerWrapper warp = InitPremisesAnalyzer();

                        directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucene.Path));
                        indexWriter = new IndexWriter(directory, false, warp, isCreate);
                        indexWriter.SetMaxFieldLength(15000);
                        foreach (var item in premises)
                        {
                            count++;
                            try
                            {
                                var document = GetPremisesDocument(item);
                                indexWriter.AddDocument(document);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("楼盘ID" + item.PremisesID + "异常" + ex.Message);
                            }
                            Console.WriteLine("楼盘ID：" + item.PremisesID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "添加楼盘索引：" + ex.Message, false);
            }
            finally
            {
                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                {
                    indexWriter.Optimize();
                    indexWriter.Close();
                }
                if (directory != null)
                    directory.Close();
            }

            return count;
        }
        /// <summary>
        /// 更新或者添加指定楼盘ID的索引
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int AddDocumentForPremises(string ids, string mqname)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                var lucenexml = MQHelper.GetLucenexmlInfomationByMqName(mqname);

                if (lucenexml == null || string.IsNullOrEmpty(lucenexml.Path))
                {
                    OptLog.Log("Info：", "TXPremises", "赞未开通！", false);
                    return 0;
                }

                Console.WriteLine("ids:" + ids);
                var premises = _dal.GetPremisesByIds(ids);
                if (premises != null && premises.Count > 0)
                {
                    //加载楼盘索引分词
                    PerFieldAnalyzerWrapper warp = InitPremisesAnalyzer();

                    directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucenexml.Path));
                    indexWriter = new IndexWriter(directory, true, warp, false);
                    indexWriter.SetMaxFieldLength(15000);
                    Console.WriteLine("premisescoun:" + premises.Count);
                    foreach (var item in premises)
                    {
                        //Console.WriteLine("foreach" + item.PremisesID);
                        count++;
                        //删除索引
                        Term t = new Term("PremisesID", item.PremisesID);
                        indexWriter.DeleteDocuments(t);
                        Document document = null;
                        try
                        {
                            document = GetPremisesDocument(item);
                            indexWriter.AddDocument(document);
                        }
                        catch (Exception e)
                        {
                            OptLog.Log("Error：", "Error", "添加：" + e.Message, false);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "添加楼盘索引：" + ex.Message, false);
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
        /// 删除指定楼盘ID的索引
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public void DeleteDocumentForPremises(string[] ids, string mqname)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                var lucenexml = MQHelper.GetLucenexmlInfomationByMqName(mqname);
                if (lucenexml == null || string.IsNullOrEmpty(lucenexml.Path))
                {
                    OptLog.Log("Info：", "TXPremises", "赞未开通！", false);
                    return;
                }
                //加载楼盘分词
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucenexml.Path));
                indexWriter = new IndexWriter(directory, false, analyzer, false);
                indexWriter.SetMaxFieldLength(15000);
                for (int i = 0; i < ids.Length; i++)
                {
                    //删除索引
                    Term t = new Term("PremisesID", ids[i]);
                    indexWriter.DeleteDocuments(t);
                }
                //OptLog.Log("Info：", "TXPremises", "删除楼盘ID：" + id, false);
            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "删除楼盘索引：" + ex.Message, false);
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

        }

        /// <summary>
        /// 获取单条索引信息
        /// </summary>
        /// <param name="premises"></param>
        /// <returns></returns>
        private Document GetPremisesDocument(IndexPremises premises)
        {
            var _dal = new PremisesDal();

            Document document = new Document();

            document.Add(new Field("PremisesID", premises.PremisesID, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("ProvinceID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.ProvinceID)));

            document.Add(new Field("ProvinceName", premises.ProvinceName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("ProvinceNamePY", TXCommons.Han2Ping.Convert(premises.ProvinceName.Replace("市", "")), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("CityID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.CityID)));

            document.Add(new Field("CityName", premises.CityName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("CityNamePY", TXCommons.Han2Ping.Convert(premises.CityName.Replace("市", "")), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("DistrictID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.DistrictID)));

            document.Add(new Field("DistrictName", premises.DistrictName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("DistrictNamePY", TXCommons.Han2Ping.Convert(premises.DistrictName), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("BusinessID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.BusinessID)));

            document.Add(new Field("BusinessName", premises.BusinessName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("BusinessNamePY", TXCommons.Han2Ping.Convert(premises.BusinessName), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            var traffics = _dal.GetTrafficsByPremisesID(premises.PremisesID);
            if (traffics != null && traffics.Count > 0)
            {
                document.Add(new Field("Traffics", string.Join(",", traffics), Field.Store.YES, Field.Index.ANALYZED));
            }
            else
            {
                document.Add(new Field("Traffics", "", Field.Store.YES, Field.Index.ANALYZED));
            }
            document.Add(new Field("PremisesName", premises.PremisesName, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field("PremisesPyName", TXCommons.Han2Ping.Convert(premises.PremisesName), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field("PremisesAddress", premises.PremisesAddress, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field("SalesAddress", premises.SalesAddress, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field("Developer", premises.Developer, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            //document.Add(new Field("DeveloperPY", TXCommons.Han2Ping.Convert(premises.Developer), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("Lng", premises.Lng.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("Lat", premises.Lat, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("ReferencePrice", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(premises.ReferencePrice)));

            document.Add(new Field("TelePhone", premises.TelePhone, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("SalesStatus", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.SalesStatus)));

            int order = 0;

            switch (premises.SalesStatus)
            {
                case "1":
                    order = 0;
                    break;
                case "0":
                    order = 1;
                    break;
                default:
                    order = 2;
                    break;
            }

            //排序字段
            document.Add(new NumericField("OrderStatus", Field.Store.YES, true).SetIntValue(order));

            document.Add(new NumericField("Ring", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.Ring)));

            document.Add(
                new NumericField("IsRecommend", Field.Store.YES, true).SetIntValue(
                    Convert.ToInt32(premises.IsRecommend.ToLower() == "true" ? 1 : 0)));
            //document.Add(new Field("IsRecommend", premises.IsRecommend.ToLower() == "true" ? "1" : "0", Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new NumericField("BuildingArea", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(premises.BuildingArea)));

            document.Add(new NumericField("Area", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(premises.Area)));

            //document.Add(new NumericField("BuildingType", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.BuildingType)));

            document.Add(new Field("BuildingType", premises.BuildingType, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field("PropertyType", premises.PropertyType, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field("Renovation", premises.Renovation, Field.Store.YES, Field.Index.ANALYZED));

            //户型统计
            var rooms = _dal.GetRoomsByPremisesID(premises.PremisesID);

            if (rooms != null && rooms.Count > 0)
            {
                var housetypes = string.Join(",", rooms.Select(it => it.RoomNumber));

                document.Add(new Field("HouseTypes", housetypes, Field.Store.YES, Field.Index.ANALYZED));

                var statistics = string.Join(",", rooms.Select(it => it.RoomCount));

                document.Add(new Field("HouseTypesStatistics", statistics, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            }
            else
            {
                document.Add(new Field("HouseTypes", "", Field.Store.YES, Field.Index.ANALYZED));

                document.Add(new Field("HouseTypesStatistics", "", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            }

            document.Add(new Field("ListPictureUrl", premises.ListPictureUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("DeveloperLOGOUrl", premises.DeveloperLOGOUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("EffectPictureUrl", GetEffectPictureImg(premises.InnerCode, Convert.ToInt32(premises.CityID), TXCommons.PictureModular.PremisesPictureType.Effect.ToString()), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("ADPictureUrl", GetEffectPictureImg(premises.InnerCode, Convert.ToInt32(premises.CityID), TXCommons.PictureModular.PremisesPictureType.AdvertList.ToString()), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("PictureCount", Field.Store.YES, true).SetIntValue(Convert.ToInt32(premises.PictureCount)));
            #region 特色

            //int CharacteristicCount = 0;

            //var characteristic = premises.Characteristic.Split(',');
            //新排列特色
            //var tics = string.Empty;

            //if (characteristic != null && characteristic.Length > 0)
            //{
            //    try
            //    {

            //        var characteristicNames = _dal.GetCharacteristicByID(premises.Characteristic);

            //        声明新数组长度
            //        string[] characteristics = new string[characteristic.Length];

            //        int j = 0;
            //        倒序排列特色
            //        for (int i = characteristic.Length - 1; i >= 0; i--)
            //        {
            //            var id = Convert.ToInt32(characteristic[i]);
            //            characteristics[j] = characteristicNames.First(it => it.ID == id).Name;
            //            j++;
            //        }
            //        CharacteristicCount = j;
            //        拼接新特色房源
            //        tics = string.Join(",", characteristics);

            //    }
            //    catch
            //    {
            //    }
            //}
            //document.Add(new NumericField("CharacteristicCount", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(CharacteristicCount)));

            //document.Add(new Field("Characteristic", premises.Characteristic, Field.Store.YES, Field.Index.ANALYZED));

            //document.Add(new Field("CharacteristicName", tics, Field.Store.YES, Field.Index.ANALYZED));
            #endregion


            //document.Add(new NumericField("BrowseCount", Field.Store.YES, true).SetIntValue(premises.BrowseCount));

            document.Add(new NumericField("UpdateTime", Field.Store.YES, true).SetLongValue(Constant.UNIX_TIMESTAMP(Convert.ToDateTime(premises.UpdateTime))));

            //最新活动房源
            var houses = _dal.GetHouseByNewActivities(premises.PremisesID, "2", 1, 2);

            var hs = houses != null && houses.Count > 0 ? Newtonsoft.Json.JsonConvert.SerializeObject(houses) : "";

            document.Add(new Field("Houses", hs, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("OpeningTime", Field.Store.YES, true).SetLongValue(Constant.UNIX_TIMESTAMP(Convert.ToDateTime(premises.OpeningTime))));

            document.Add(new NumericField("CheckInTime", Field.Store.YES, true).SetLongValue(Constant.UNIX_TIMESTAMP(Convert.ToDateTime(premises.CheckInTime))));

            //document.Add(new NumericField("HouseMaxArea", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(premises.HouseMaxArea)));
            //document.Add(new NumericField("HouseMinArea", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(premises.HouseMinArea)));
            //document.Add(new Field("HouseMaxArea", premises.HouseMaxArea, Field.Store.YES, Field.Index.NOT_ANALYZED));

            //document.Add(new Field("HouseMinArea", premises.HouseMinArea, Field.Store.YES, Field.Index.NOT_ANALYZED));

            document.Add(new Field("HouseAreas", premises.HouseAreas, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("PropertyRight", premises.PropertyRight, Field.Store.YES, Field.Index.ANALYZED));

            return document;
        }

        /// <summary>
        /// 根据楼盘Id获取楼盘效果图第一张图片的详细信息
        /// </summary>
        /// <param name="InnerCode"></param>
        /// <param name="CityId"></param>
        /// <param name="PremisesPictureType">
        /// 效果图：TXCommons.PictureModular.PremisesPictureType.Effect.ToString()
        /// 广告图：
        /// </param>
        /// 
        /// <returns></returns>
        private string GetEffectPictureImg(string InnerCode, int CityId, string PremisesPictureType)
        {
            try
            {
                List<PremisesPictureInfo> lsEffect =
                   TXCommons.PictureModular.GetPicture.GetPremisesPictureList(InnerCode, true, PremisesPictureType, CityId);
                if (null != lsEffect && lsEffect.Count > 0)
                {
                    lsEffect = lsEffect.OrderByDescending(t => t.CreateTime).ToList();
                }
                return lsEffect != null ? lsEffect.First().Path : string.Empty;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 加载楼盘搜索分词
        /// </summary>
        /// <param name="analyzer"></param>
        private PerFieldAnalyzerWrapper InitPremisesAnalyzer()
        {
           Analyzer analyzer = new Lucene.Net.Analysis.PanGu.PanGuAnalyzer();// new ProductAnalyzer();//新房分词器
            Lucene.Net.Analysis.Standard.StandardAnalyzer an_2v9 = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);//标准分词器
            PerFieldAnalyzerWrapper warp = new PerFieldAnalyzerWrapper(an_2v9);
            //warp.AddAnalyzer("PremisesName", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("PremisesAddress", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("HouseTypes", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("Characteristic", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("CharacteristicName", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("Traffics", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("PropertyType", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            warp.AddAnalyzer("Renovation", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
            //warp.AddAnalyzer("PremisesPyName",analyzer);//楼盘拼音，盘古分词
            return warp;
        }

        #region 楼盘搜索

        public object Search(string conditions)
        {
            if (string.IsNullOrEmpty(conditions))
                return null;
            var info = IndexConditionInfo.GetSearchCondiction(conditions);
            var cityid = MQHelper.GetCityId(info.CityName);
            var lucenexml = MQHelper.GetLucenexmlInfomation(cityid.ToString());
            if (lucenexml == null || string.IsNullOrEmpty(lucenexml.Path))
            {
                OptLog.Log("Info：", "TXPremises", "城市赞未开通！城市ID为：" + cityid, false);
                return null;
            }
            PerFieldAnalyzerWrapper warp = InitPremisesAnalyzer();
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(lucenexml.Path));
            IndexSearcher indexSearcher = new IndexSearcher(directory, true);
            DataSet ds = new DataSet("Results");
            try
            {
                #region 查询及结果

                TopFieldDocs topdocs = null;

                BooleanQuery bq = new BooleanQuery();

                if (string.IsNullOrEmpty(conditions))
                {
                    bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
                }
                else
                {
                    if (!string.IsNullOrEmpty(info.PremisesID))
                    {

                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("PremisesID", info.PremisesID)), BooleanClause.Occur.MUST);
                    }

                    #region 地理位置

                    if (!string.IsNullOrEmpty(info.ProvinceID))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("ProvinceID", info.ProvinceID)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.ProvinceName))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("ProvinceNamePY", info.ProvinceName)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.CityID))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityID", info.CityID)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.CityName))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("CityNamePY", info.CityName)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.DistrictID))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("DistrictID", info.DistrictID)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.DistrictName))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("DistrictNamePY", info.DistrictName)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.BusinessID))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("BusinessID", info.BusinessID)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.BusinessName))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("BusinessNamePY", info.BusinessName)), BooleanClause.Occur.MUST);
                    }

                    if (!string.IsNullOrEmpty(info.TrafficLine))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("Traffics", info.TrafficLine)), BooleanClause.Occur.MUST);
                    }

                    if (!string.IsNullOrEmpty(info.TrafficStation))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("Traffics", info.TrafficStation)), BooleanClause.Occur.MUST);
                    }

                    #endregion

                    if (!string.IsNullOrEmpty(info.SalesStatus))
                    {
                        var status = Convert.ToInt32(info.SalesStatus);
                        bq.Add(NumericRangeQuery.NewIntRange("SalesStatus", status, status, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.PropertyType))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("PropertyType", info.PropertyType)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.Characteristic))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("Characteristic", info.Characteristic)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.Renovation))
                    {
                        var renovationKey = GetKeyWordsSplitBySpace(info.Renovation, new PanGuTokenizer());
                        var queryParser = new QueryParser("Renovation", new PanGuAnalyzer(true));
                        Query renovation = queryParser.Parse(renovationKey);
                        bq.Add(renovation, BooleanClause.Occur.MUST);
                        //bq.Add(new TermQuery(new Lucene.Net.Index.Term("Renovation", info.Renovation)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.PriceBegin) || !string.IsNullOrEmpty(info.PriceEnd))
                    {
                        int pricebegin = string.IsNullOrEmpty(info.PriceBegin) ? 0 : int.Parse(info.PriceBegin);
                        int priceend = string.IsNullOrEmpty(info.PriceEnd) ? 0 : int.Parse(info.PriceEnd);
                        if (priceend == 0)
                            priceend = 1000 * 1000 * 10;
                        bq.Add(NumericRangeQuery.NewDoubleRange("ReferencePrice", pricebegin, priceend, true, true), BooleanClause.Occur.MUST);
                    }
                    #region 过滤面积

                    //if (!string.IsNullOrEmpty(info.AreaBegin) || !string.IsNullOrEmpty(info.AreaEnd))
                    //{
                    //    int areabegin = string.IsNullOrEmpty(info.AreaBegin) ? 0 : int.Parse(info.AreaBegin);
                    //    int areaend = string.IsNullOrEmpty(info.AreaEnd) ? 0 : int.Parse(info.AreaEnd);
                    //    int maxarea = 99999999;
                    //    if (areaend == 0)
                    //        areaend = maxarea;
                    //    //bq.Add(NumericRangeQuery.NewDoubleRange("BuildingArea", areabegin, areaend, true, true), BooleanClause.Occur.MUST);
                    //    BooleanQuery bqHouseArea = new BooleanQuery();
                    //    bqHouseArea.Add(NumericRangeQuery.NewDoubleRange("HouseMinArea", areabegin, areaend, true, true), BooleanClause.Occur.SHOULD);

                    //    bqHouseArea.Add(NumericRangeQuery.NewDoubleRange("HouseMaxArea", areabegin, areaend, true, true), BooleanClause.Occur.SHOULD);
                    //    bq.Add(bqHouseArea, BooleanClause.Occur.MUST);
                    //}

                    if (!string.IsNullOrEmpty(info.HouseArea))
                    {
                        var houseAreaKey = GetKeyWordsSplitBySpace(info.HouseArea, new PanGuTokenizer());
                        var queryParser = new QueryParser("HouseAreas", new PanGuAnalyzer(true));
                        Query renovation = queryParser.Parse(houseAreaKey);
                        bq.Add(renovation, BooleanClause.Occur.MUST);
                    }

                    #endregion

                    if (!string.IsNullOrEmpty(info.Ring))
                    {
                        var ring = int.Parse(info.Ring);
                        bq.Add(NumericRangeQuery.NewIntRange("Ring", ring, ring, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.BuildingType))
                    {
                        var buildingTypeKey = GetKeyWordsSplitBySpace(info.BuildingType, new PanGuTokenizer());
                        var queryParser = new QueryParser("BuildingType", new PanGuAnalyzer(true));
                        Query buildingType = queryParser.Parse(buildingTypeKey);
                        bq.Add(buildingType, BooleanClause.Occur.MUST);
                        //bq.Add(new TermQuery(new Lucene.Net.Index.Term("BuildingType", info.BuildingType)), BooleanClause.Occur.MUST);
                    }
                    //居室
                    if (!string.IsNullOrEmpty(info.Room))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("HouseTypes", info.Room)), BooleanClause.Occur.MUST);
                    }
                    //开盘时间
                    if (!string.IsNullOrEmpty(info.OpeningTime))
                    {
                        var beginopentime = "";
                        var endopentime = "";
                        switch (info.OpeningTime)
                        {
                            case "1":
                                beginopentime = DateTime.Now.Date.ToString("yyyy-MM");
                                endopentime = DateTime.Now.AddMonths(1).Date.ToString("yyyy-MM");
                                break;
                            case "2":
                                beginopentime = DateTime.Now.AddMonths(1).Date.ToString("yyyy-MM");
                                endopentime = DateTime.Now.AddMonths(2).Date.ToString("yyyy-MM");
                                break;
                            case "3":
                                beginopentime = DateTime.Now.Date.ToString("yyyy-MM");
                                endopentime = DateTime.Now.AddMonths(3).Date.ToString("yyyy-MM");
                                break;
                            case "4":
                                beginopentime = DateTime.Now.Date.ToString("yyyy-MM");
                                endopentime = DateTime.Now.AddMonths(6).Date.ToString("yyyy-MM");
                                break;
                            case "5":
                                beginopentime = DateTime.Now.Date.ToString("yyyy-MM");
                                endopentime = DateTime.Now.AddMonths(-3).Date.ToString("yyyy-MM");
                                break;
                            case "6":
                                beginopentime = DateTime.Now.Date.ToString("yyyy-MM");
                                endopentime = DateTime.Now.AddMonths(-6).Date.ToString("yyyy-MM");
                                break;
                            default:
                                break;
                        }

                        var begin = Constant.UNIX_TIMESTAMP(Convert.ToDateTime(beginopentime));

                        var end = Constant.UNIX_TIMESTAMP(Convert.ToDateTime(endopentime));

                        //当搜索条件为几个月前开盘时，begin要大于end
                        bq.Add(
                            begin < end
                                ? NumericRangeQuery.NewLongRange("OpeningTime", begin, end, true, true)
                                : NumericRangeQuery.NewLongRange("OpeningTime", end, begin, true, true),
                            BooleanClause.Occur.MUST);
                    }

                    //搜索关键字
                    if (!string.IsNullOrEmpty(info.KeyWord))
                    {
                        BooleanQuery qq = new BooleanQuery();
                        Query q1 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "PremisesName", warp).Parse(info.KeyWord);
                        Query q2 = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "PremisesAddress", warp).Parse(info.KeyWord);
                        qq.Add(q1, BooleanClause.Occur.SHOULD);
                        qq.Add(q2, BooleanClause.Occur.SHOULD);
                        bq.Add(qq, BooleanClause.Occur.MUST);
                    }
                }

                #region sort

                Sort sort = new Sort();
                if (!string.IsNullOrEmpty(info.PriceSort))
                {
                    var reverse = false;
                    if ("1".Equals(info.PriceSort))
                        reverse = true;
                    SortField[] sf = { new SortField("IsRecommend", SortField.INT, true), new SortField("ReferencePrice", SortField.DOUBLE, reverse), new SortField("OrderStatus", SortField.INT, false) };
                    sort.SetSort(sf);
                }
                else if (!string.IsNullOrEmpty(info.CheckInSort))
                {
                    var reverse = false;
                    if ("5".Equals(info.CheckInSort))
                        reverse = true;
                    SortField[] sf = { new SortField("IsRecommend", SortField.INT, true), new SortField("CheckInTime", SortField.LONG, reverse), new SortField("OrderStatus", SortField.INT, false) };
                    sort.SetSort(sf);
                }
                else if (!string.IsNullOrEmpty(info.OpeningSort))
                {
                    var reverse = false;
                    if ("3".Equals(info.OpeningSort))
                        reverse = true;
                    SortField[] sf = { new SortField("IsRecommend", SortField.INT, true), new SortField("OpeningTime", SortField.LONG, reverse), new SortField("OrderStatus", SortField.INT, false) };
                    sort.SetSort(sf);
                }
                else if (!string.IsNullOrEmpty(info.CharacteristicCount))
                {
                    var reverse = true;
                    if ("7".Equals(info.CharacteristicSort))
                        reverse = false;
                    SortField[] sf = { new SortField("IsRecommend", SortField.INT, true), new SortField("CharacteristicCount", SortField.DOUBLE, reverse) };
                    sort.SetSort(sf);
                }
                else if (string.IsNullOrEmpty(info.PriceSort) && string.IsNullOrEmpty(info.CheckInSort) && string.IsNullOrEmpty(info.OpeningSort))
                {
                    SortField[] sf = { new SortField("IsRecommend", SortField.INT, true), new SortField("OrderStatus", SortField.INT, false), new SortField("UpdateTime", SortField.LONG, true) };//PremisesID
                    sort.SetSort(sf);
                }
                #endregion



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
                                string key = ((Field)hitDoc.GetFields()[k]).Name();
                                if (key.ToLower() == "premisespyname")
                                {
                                    //如果是楼盘拼音就过滤掉
                                    continue;
                                }
                                string value = ((Field)hitDoc.GetFields()[k]).StringValue();
                                dr[k] = string.IsNullOrEmpty(value) ? string.Empty : value;
                            }
                            dt.Rows.Add(dr);
                        }
                        catch (Exception ex)
                        {

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
            }
            finally
            {
                if (directory != null)
                    directory.Close();
                if (indexSearcher != null)
                    indexSearcher.Close();
                //if (analyzer != null)
                //    analyzer.Close();
            }
            return ds;

        }


        static public string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            StringBuilder result = new StringBuilder();

            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);

            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }

                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }

            return result.ToString().Trim();
        }

        #region  楼盘名称搜索
        /// <summary>
        /// 查询楼盘名称
        /// </summary>
        /// <param name="KeyWord">查询关键字</param>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public object NameSearch(string KeyWord, int CityId, int PageIndex, int PageSize)
        {
            var lucenexml = MQHelper.GetLucenexmlInfomation(CityId.ToString());
            if (lucenexml == null || string.IsNullOrEmpty(lucenexml.Path))
            {
                OptLog.Log("Info：", "TXPremises", "城市赞未开通！城市ID为：" + CityId, false);
                return null;
            }
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            //PerFieldAnalyzerWrapper warp = InitPremisesAnalyzer();
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(lucenexml.Path));
            IndexSearcher indexSearcher = new IndexSearcher(directory, true);
            DataSet ds = new DataSet("Results");
            try
            {
                TopFieldDocs topdocs = null;
                BooleanQuery bq = new BooleanQuery();

                bq.Add(NumericRangeQuery.NewIntRange("CityID", CityId, CityId, true, true), BooleanClause.Occur.MUST);
                if (!string.IsNullOrEmpty(KeyWord))
                {
                    BooleanQuery bqKeyWord = new BooleanQuery();
                    bqKeyWord.Add(new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_29, "PremisesName", analyzer).Parse(KeyWord), BooleanClause.Occur.SHOULD);

                    bqKeyWord.Add(new PrefixQuery(new Lucene.Net.Index.Term("PremisesPyName", KeyWord)),BooleanClause.Occur.SHOULD);
                    bq.Add(bqKeyWord, BooleanClause.Occur.MUST);
                }
                topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(PageSize) * Convert.ToInt32(PageIndex), new Sort());
                ScoreDoc[] hits = topdocs.scoreDocs;
                int hitscount = topdocs.totalHits;
                if (hits.Length > 0)
                {
                    DataTable dt = new DataTable("Record");
                    dt.Columns.Add("CityID");
                    dt.Columns.Add("CityName");
                    dt.Columns.Add("DistrictID");
                    dt.Columns.Add("DistrictName");
                    dt.Columns.Add("BusinessID");
                    dt.Columns.Add("BusinessName");
                    dt.Columns.Add("PremisesID");
                    dt.Columns.Add("PremisesName");
                    int begin = (Convert.ToInt32(PageIndex) - 1) * Convert.ToInt32(PageSize);
                    int end = Convert.ToInt32(PageIndex) * Convert.ToInt32(PageSize); ;
                    int min = Math.Min(end, hitscount);
                    for (int i = begin; i < min; i++)
                    {
                        try
                        {
                            Lucene.Net.Documents.Document hitDoc = indexSearcher.Doc(hits[i].doc);
                            DataRow dr = dt.NewRow();
                            dr["CityID"] = hitDoc.Get("CityID");
                            dr["CityName"] = hitDoc.Get("CityName");
                            dr["DistrictID"] = hitDoc.Get("DistrictID");
                            dr["DistrictName"] = hitDoc.Get("DistrictName");
                            dr["BusinessID"] = hitDoc.Get("BusinessID");
                            dr["BusinessName"] = hitDoc.Get("BusinessName");
                            dr["PremisesID"] = hitDoc.Get("PremisesID");
                            dr["PremisesName"] = hitDoc.Get("PremisesName");
                            dt.Rows.Add(dr);
                        }
                        catch (Exception) { }
                    }
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch (Exception)
            {
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
        #endregion
        #endregion

        #endregion

        #region 添加/更新/删除户型索引
        /// <summary>
        /// 重新生成所有索引信息
        /// </summary>
        /// <returns></returns>
        public int AddDocumentForRooms(int Id, bool isCreate = true)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                var lucene = MQHelper.GetLucenexmlInfomation(Id);

                if (lucene == null || string.IsNullOrEmpty(lucene.RoomPath))
                {
                    Console.WriteLine("城市赞未开通！城市ID为：" + lucene.CityIds);
                    return 0;
                }
                else
                {
                    if (!System.IO.Directory.Exists(lucene.RoomPath))
                    {
                        System.IO.Directory.CreateDirectory(lucene.RoomPath);
                    }
                }

                var rooms = _dal.GetRooms(lucene.CityIds);

                if (rooms != null && rooms.Count > 0)
                {
                    //给户型图添加一个锁

                    analyzer = new ProductAnalyzer();//新房分词器
                    //加载户型索引分词
                    //InitRoomAnalyzer(analyzer);

                    directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucene.RoomPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);

                    foreach (var item in rooms)
                    {
                        count++;

                        var document = GetRoomDocument(item);

                        indexWriter.AddDocument(document);

                        Console.WriteLine("楼盘ID：" + item.PremisesID);
                    }
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "添加房源户型索引：" + ex.Message, false);
            }
            finally
            {
                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                {
                    indexWriter.Optimize();
                    indexWriter.Close();
                }
                if (directory != null)
                    directory.Close();
            }

            return count;
        }

        /// <summary>
        /// 更新或者添加指定楼盘ID的索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddDocumentForRoom(string premisesids, string mqname)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                var lucenexml = MQHelper.GetLucenexmlInfomationByRoomMqName(mqname);
                if (lucenexml == null || string.IsNullOrEmpty(lucenexml.RoomPath))
                {
                    OptLog.Log("Info：", "TXRoom", "赞未开通！", false);
                    return 0;
                }
                var rooms = _dal.GetRoomByPremisesIds(premisesids);

                if (rooms != null && rooms.Count > 0)
                {
                    analyzer = new ProductAnalyzer();//新房分词器
                    //加载楼盘分词
                    InitRoomAnalyzer(analyzer);
                    directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucenexml.RoomPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, false);
                    indexWriter.SetMaxFieldLength(15000);

                    foreach (var item in rooms)
                    {
                        count++;
                        //删除索引
                        Term t = new Term("HouseID", item.HouseID);

                        indexWriter.DeleteDocuments(t);

                        var document = GetRoomDocument(item);

                        indexWriter.AddDocument(document);

                    }

                }
                else
                {
                    OptLog.Log("Info：", "TXRoom", "房源户型为空！", false);
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "添加房源户型索引：" + ex.Message, false);
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
        /// 删除指定楼盘ID的索引
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public void DeleteDocumentForRoom(string[] houseIds, string mqname)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            try
            {
                var lucenexml = MQHelper.GetLucenexmlInfomationByRoomMqName(mqname);
                if (lucenexml == null || string.IsNullOrEmpty(lucenexml.RoomPath))
                {
                    OptLog.Log("Info：", "TXRoom", "赞未开通！", false);
                    return;
                }

                //加载楼盘分词
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucenexml.RoomPath));
                indexWriter = new IndexWriter(directory, false, analyzer, false);
                indexWriter.SetMaxFieldLength(15000);

                for (int i = 0; i < houseIds.Length; i++)
                {
                    //删除索引
                    Term t = new Term("HouseID", houseIds[i]);
                    indexWriter.DeleteDocuments(t);
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "删除房源户型索引：" + ex.Message, false);
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

        }

        /// <summary>
        /// 获取单条索引信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        private Document GetRoomDocument(IndexRoom room)
        {
            var _dal = new PremisesDal();

            Document document = new Document();

            document.Add(new Field("PremisesID", room.PremisesID, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("HouseID", room.HouseID, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("UnitId", Field.Store.YES, true).SetIntValue(Convert.ToInt32(room.UnitId)));

            document.Add(new NumericField("BuildingId", Field.Store.YES, true).SetIntValue(Convert.ToInt32(room.BuildingId)));

            document.Add(new Field("BuildingName", room.BuildingName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("BuildingNameType", room.BuildingNameType, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("RID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(room.RID)));

            document.Add(new Field("Title", room.Title, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("Room", Field.Store.YES, true).SetIntValue(Convert.ToInt32(room.Room)));

            document.Add(new NumericField("Hall", Field.Store.YES, true).SetIntValue(Convert.ToInt32(room.Hall)));

            document.Add(new NumericField("Toilet", Field.Store.YES, true).SetIntValue(Convert.ToInt32(room.Toilet)));

            document.Add(new NumericField("BuildingArea", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(room.BuildingArea)));

            document.Add(new NumericField("Area", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(room.Area)));

            document.Add(new Field("PicUrl", room.PicUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("PicDesc", room.PicDesc, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("CreateTime", Field.Store.YES, true).SetLongValue(Constant.UNIX_TIMESTAMP(Convert.ToDateTime(room.CreateTime))));

            return document;
        }

        /// <summary>
        /// 加载楼盘搜索分词
        /// </summary>
        /// <param name="analyzer"></param>
        private void InitRoomAnalyzer(Analyzer analyzer)
        {
            Lucene.Net.Analysis.Standard.StandardAnalyzer an_2v9 = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);//标准分词器
            PerFieldAnalyzerWrapper warp = new PerFieldAnalyzerWrapper(an_2v9);
            //warp.AddAnalyzer("HouseTypes", analyzer);//默认是标准分词，如果是房屋类型就选择新房分词
        }

        public object SearchRoom(string conditions)
        {
            var info = IndexConditionInfo.GetSearchRoomCondiction(conditions);
            if (info == null)
                return null;

            var lucene = MQHelper.GetLucenexmlInfomation(info.CityId);

            if (lucene == null || string.IsNullOrEmpty(lucene.RoomPath))
            {
                return null;
            }
            Analyzer analyzer = new StandardAnalyzer();
            InitRoomAnalyzer(analyzer);
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(lucene.RoomPath));
            IndexSearcher indexSearcher = new IndexSearcher(directory, true);
            DataSet ds = new DataSet("Results");
            try
            {
                #region 查询及结果


                BooleanQuery bq = new BooleanQuery();

                if (string.IsNullOrEmpty(conditions))
                {
                    bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
                }
                else
                {
                    #region 搜索条件

                    if (!string.IsNullOrEmpty(info.PremisesID))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("PremisesID", info.PremisesID)), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.BuildingId))
                    {
                        var buildingId = Convert.ToInt32(info.BuildingId);
                        bq.Add(NumericRangeQuery.NewIntRange("BuildingId", buildingId, buildingId, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.Room))
                    {
                        var room = Convert.ToInt32(info.Room);
                        bq.Add(NumericRangeQuery.NewIntRange("Room", room, room, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.RID))
                    {
                        var rid = Convert.ToInt32(info.RID);
                        bq.Add(NumericRangeQuery.NewIntRange("RID", rid, rid, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.BeginArea) || !string.IsNullOrEmpty(info.EndArea))
                    {
                        var beginArea = Convert.ToDouble(string.IsNullOrEmpty(info.BeginArea) ? "0" : info.BeginArea);
                        var endArea = Convert.ToDouble(string.IsNullOrEmpty(info.EndArea) ? "99999999" : info.EndArea);
                        bq.Add(NumericRangeQuery.NewDoubleRange("BuildingArea", beginArea, endArea, true, true), BooleanClause.Occur.MUST);
                    }

                    if (string.IsNullOrEmpty(info.PremisesID) && string.IsNullOrEmpty(info.BuildingId) &&
                        string.IsNullOrEmpty(info.Room) && string.IsNullOrEmpty(info.RID) &&
                        string.IsNullOrEmpty(info.BeginArea) && string.IsNullOrEmpty(info.EndArea))
                    {
                        bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
                    }

                    #endregion
                }

                Sort sort = new Sort();

                sort.SetSort(new SortField("CreateTime", SortField.LONG, true));
                //SortField[] sf = { new SortField("UpdateTime", SortField.LONG, true) };
                TopFieldDocs topdocs = indexSearcher.Search(bq, null, Convert.ToInt32(info.PageSize) * Convert.ToInt32(info.PageIndex), sort);
                //Hits Hitstopdocs = indexSearcher.Search(bq);
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
                        }
                    }
                    //DataTable summary_dt = new DataTable("Summary");
                    //summary_dt.Columns.Add("TotalRecords");
                    //summary_dt.Columns.Add("PageSize");
                    //summary_dt.Columns.Add("PageIndex");
                    //summary_dt.Columns.Add("TotalPage");
                    //summary_dt.Columns.Add("RealRecords");
                    //DataRow summary_dr = summary_dt.NewRow();
                    //summary_dr[0] = (hitscount > 100 * Convert.ToInt32(info.PageSize)) ? (100 * Convert.ToInt32(info.PageSize)).ToString() : hitscount.ToString();
                    //summary_dr[1] = info.PageSize;
                    //summary_dr[2] = info.PageIndex;
                    //int totalpage = hitscount % Convert.ToInt32(info.PageSize) == 0 ? (hitscount / Convert.ToInt32(info.PageSize)) : ((hitscount / Convert.ToInt32(info.PageSize)) + 1);
                    //summary_dr[3] = totalpage > 100 ? "100" : totalpage.ToString();
                    //summary_dr[4] = hitscount;
                    //summary_dt.Rows.Add(summary_dr);
                    DataTable newDataTable = GetNewRoomDataTable(dt);
                    ds.Tables.Add(newDataTable);
                    return ds;
                }


                #endregion
            }
            catch (Exception ex)
            {
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
        /// <summary>
        /// 过滤面积、户型ID相同的房源
        /// </summary>
        /// <param name="OldDataRoom">老的数据集</param>
        /// <returns>过滤后的数据集</returns>
        private DataTable GetNewRoomDataTable(DataTable OldDataRoom)
        {
            DataTable NewDataTable = OldDataRoom.Clone();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (DataRow item in OldDataRoom.Rows)
            {
                //string key = item["RID"] + "_" + item["Area"];
                //if (dict.ContainsKey(key))
                //{
                //    continue;
                //}
                //dict.Add(key, string.Empty);
                DataRow row = NewDataTable.NewRow();
                row.ItemArray = item.ItemArray;
                NewDataTable.Rows.Add(row);//把不同面积、户型ID放到新的集合里
            }
            return NewDataTable;
        }
        #endregion

        #region 添加/更新/删除小区索引

        //public int Addvillage(int Id, bool isCreate = true)
        //{
        //    Directory directory = null;
        //    Analyzer analyzer = null;
        //    IndexWriter indexWriter = null;
        //    var _dal = new PremisesDal();

        //}

        #endregion

        #region 排行榜
        /// <summary>
        /// 生成所有排序信息
        /// </summary>
        /// <param name="Id">分组ID</param>
        /// <param name="isCreate">是否创建</param>
        /// <returns></returns>
        public int AddDocumentForRankings(int Id, bool isCreate = true)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                var lucene = MQHelper.GetLucenexmlInfomation(Id);

                if (lucene == null || string.IsNullOrEmpty(lucene.RankPath))
                {
                    Console.WriteLine("城市暂未开通！城市ID为：" + lucene.CityIds);
                    return 0;
                }
                else
                {
                    if (!System.IO.Directory.Exists(lucene.RankPath))
                    {
                        System.IO.Directory.CreateDirectory(lucene.RankPath);
                    }
                }
                var rankings = _dal.GetPremisesByRanking(lucene.CityIds);

                if (rankings != null && rankings.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucene.RankPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);

                    foreach (var item in rankings)
                    {
                        count++;

                        var document = GetRankingDocument(item);

                        indexWriter.AddDocument(document);

                        Console.WriteLine("排行楼盘ID：" + item.PremisesID);
                    }
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "添加排行索引：" + ex.Message, false);
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

        public int AddDocumentForRanking(int Id)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            var _dal = new PremisesDal();
            int count = 0;
            try
            {
                var lucene = MQHelper.GetLucenexmlInfomation(Id);

                if (lucene == null || string.IsNullOrEmpty(lucene.Path))
                {
                    Console.WriteLine("城市暂未开通！城市ID为：" + lucene.CityIds);
                    return 0;
                }

                var rankings = _dal.GetPremisesByRanking(lucene.CityIds);

                if (rankings != null && rankings.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    directory = FSDirectory.Open(new System.IO.DirectoryInfo(lucene.RankPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, false);
                    indexWriter.SetMaxFieldLength(15000);

                    foreach (var item in rankings)
                    {
                        count++;
                        //删除索引
                        Term t = new Term("PremisesID", item.PremisesID);

                        indexWriter.DeleteDocuments(t);

                        var document = GetRankingDocument(item);

                        indexWriter.AddDocument(document);

                    }
                }

            }
            catch (Exception ex)
            {
                OptLog.Log("Error：", "Error", "添加排行索引：" + ex.Message, false);
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

        private Document GetRankingDocument(IndexRanking ranking)
        {
            Document document = new Document();

            //document.Add(new NumericField("PremisesID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(ranking.PremisesID)));

            document.Add(new Field("PremisesID", ranking.PremisesID, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("ProvinceID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(ranking.ProvinceID)));

            document.Add(new NumericField("CityID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(ranking.CityID)));

            document.Add(new NumericField("DistrictID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(ranking.DistrictID)));

            document.Add(new Field("DistrictName", ranking.DistrictName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("BusinessID", Field.Store.YES, true).SetIntValue(Convert.ToInt32(ranking.BusinessID)));

            document.Add(new Field("BusinessName", ranking.BusinessName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("PremisesName", ranking.PremisesName, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new NumericField("ReferencePrice", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(ranking.ReferencePrice)));

            document.Add(new NumericField("ClickRate", Field.Store.YES, true).SetIntValue(Convert.ToInt32(ranking.ClickRate)));

            document.Add(new NumericField("CreateTime", Field.Store.YES, true).SetLongValue(Constant.UNIX_TIMESTAMP(Convert.ToDateTime(ranking.CreateTime))));

            document.Add(new NumericField("OpeningTime", Field.Store.YES, true).SetLongValue(Constant.UNIX_TIMESTAMP(Convert.ToDateTime(ranking.OpeningTime))));

            return document;
        }

        public object SearchRanking(string conditions)
        {
            var info = IndexConditionInfo.GetSearchRankingCondiction(conditions);
            if (info == null)
                return null;

            var lucene = MQHelper.GetLucenexmlInfomation(info.CityID);

            if (lucene == null || string.IsNullOrEmpty(lucene.Path))
            {
                return null;
            }
            Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(lucene.RankPath));
            IndexSearcher indexSearcher = new IndexSearcher(directory, true);
            DataSet ds = new DataSet("Results");
            try
            {
                #region 查询及结果

                TopFieldDocs topdocs = null;

                BooleanQuery bq = new BooleanQuery();

                if (string.IsNullOrEmpty(conditions))
                {
                    bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
                }
                else
                {
                    #region 搜索条件

                    if (!string.IsNullOrEmpty(info.ProvinceID))
                    {
                        var provinceId = Convert.ToInt32(info.ProvinceID);
                        bq.Add(NumericRangeQuery.NewIntRange("ProvinceID", provinceId, provinceId, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.CityID))
                    {
                        var cityId = Convert.ToInt32(info.CityID);
                        bq.Add(NumericRangeQuery.NewIntRange("CityID", cityId, cityId, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.DistrictID))
                    {
                        var districtId = Convert.ToInt32(info.DistrictID);
                        bq.Add(NumericRangeQuery.NewIntRange("DistrictID", districtId, districtId, true, true), BooleanClause.Occur.MUST);
                    }
                    if (!string.IsNullOrEmpty(info.BusinessID))
                    {
                        var businessId = Convert.ToInt32(info.BusinessID);
                        bq.Add(NumericRangeQuery.NewIntRange("BusinessID", businessId, businessId, true, true), BooleanClause.Occur.MUST);
                    }

                    if (string.IsNullOrEmpty(info.ProvinceID) && string.IsNullOrEmpty(info.CityID) && string.IsNullOrEmpty(info.DistrictID) && string.IsNullOrEmpty(info.BusinessID))
                    {
                        bq.Add(new MatchAllDocsQuery(), BooleanClause.Occur.MUST);
                    }
                    //去重去掉本楼盘
                    if (!string.IsNullOrEmpty(info.PremisesID))
                    {
                        bq.Add(new TermQuery(new Lucene.Net.Index.Term("PremisesID", info.PremisesID)), BooleanClause.Occur.MUST_NOT);
                    }

                    #endregion
                }

                Sort sort = new Sort();

                if (!string.IsNullOrEmpty(info.Ranking))
                {
                    switch (info.Ranking)
                    {
                        case "1":
                            sort.SetSort(new SortField("ClickRate", SortField.INT, true));
                            break;
                        case "2":
                            sort.SetSort(new SortField("CreateTime", SortField.LONG, true));
                            break;
                        case "3":
                            sort.SetSort(new SortField("ReferencePrice", SortField.DOUBLE, true));
                            break;
                        case "4":
                            sort.SetSort(new SortField("OpeningTime", SortField.LONG, true));
                            break;
                        default:
                            break;
                    }
                }

                //SortField[] sf = { new SortField("UpdateTime", SortField.LONG, true) };

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
        #endregion
    }
}
