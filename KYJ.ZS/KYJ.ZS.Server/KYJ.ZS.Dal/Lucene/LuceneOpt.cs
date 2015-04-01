using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
using ServiceStack;
using KYJ.ZS.Dal;

namespace KYJ.ZS.LuceneOpt
{
    public class LuceneOpt
    {
        /// <summary>
        /// 商品目录
        /// </summary>
        public string AttributesIndexPath { get; set; }

        public string BrandsIndexPath { get; set; }

        public string CategoriesIndexPath { get; set; }

        public string SaleGoodsesIndexPath { get; set; }

        public string RentalGoodsesIndexPath { get; set; }
    

        public string LogPath { get; set; }

        public SearchDal searchDal = new SearchDal();

        /// <summary>
        /// 添加出租商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddDocForRentalGoodses(string id)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string idstr = "0";
            try
            {
                var table = searchDal.GetRentalGoodses(id);
                if (table != null && table.Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(id);
                    directory = FSDirectory.Open(new DirectoryInfo(RentalGoodsesIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        DataRow dr = table.Rows[k];
                        idstr = dr["rental_id"].ToString();
                        Console.WriteLine("id:" + idstr);

                        try
                        {
                            Console.WriteLine("id:" + idstr);
                            Document document = GetRentalGoodsesDocument(dr);

                            indexWriter.AddDocument(document);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            Commons.Tool.Log("添加出租商品索引异常", "HouseID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Commons.Tool.Log("添加出租商品索引异常", "GoodsID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
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
        /// 添加出售商品索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddDocForSaleGoodses(string id)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string idstr = "0";
            try
            {
                var table = searchDal.GetSaleGoodses(id);
                if (table != null && table.Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(id);
                    directory = FSDirectory.Open(new DirectoryInfo(SaleGoodsesIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        DataRow dr = table.Rows[k];
                        idstr = dr["sale_id"].ToString();
                        Console.WriteLine("id:" + idstr);

                        try
                        {
                            Console.WriteLine("id:" + idstr);
                            Document document = GetSaleGoodsesDocument(dr);

                            indexWriter.AddDocument(document);
                            count++;
                        }
                        catch (Exception ex)
                        {
                           Commons.Tool.Log("添加出售商品索引异常", "HouseID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Commons.Tool.Log("添加出售商品索引异常", "GoodsID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
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
        /// 添加品牌索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddDocForBrands(string id)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string idstr = "0";
            try
            {
                DataTable table = searchDal.GetBrands(id);
                if (table != null  && table.Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(id);
                    directory = FSDirectory.Open(new DirectoryInfo(BrandsIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);


                    for (int k = 0; k < table.Rows.Count; k++)
                    {

                        DataRow dr = table.Rows[k];
                        idstr = dr["brand_id"].ToString();
                        try
                        {
                            Console.WriteLine("id:" + idstr);
                            Lucene.Net.Documents.Document document = GetBrandsDocument(dr);

                            indexWriter.AddDocument(document);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            KYJ.ZS.Commons.Tool.Log("添加品牌索引异常", "HouseID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
                        }
                    }
                    KYJ.ZS.Commons.Tool.Log("添加品牌索引完成", "count:" + count, LogPath);
                    return count;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Commons.Tool.Log("添加品牌索引异常", "HouseID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
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
        /// 添加出租商品信息索引
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        Document GetRentalGoodsesDocument(DataRow dr)
        {
            var document = new Document();
            var id = Convert.ToString(dr["rental_id"]);//商品Id
            var provinceid = Convert.ToString(dr["rental_provinceid"]);//省份Id
            var provincename = Convert.ToString(dr["rental_provincename"]);//省份名称
            var cityid = Convert.ToString(dr["rental_cityid"]);//城市Id
            var cityname = Convert.ToString(dr["rental_cityname"]);//城市名称
            var districtid = Convert.ToString(dr["rental_districtid"]);//区域Id
            var districtname = Convert.ToString(dr["rental_districtname"]);//区域名称
            //var brandid = Convert.ToString(dr["rental_brandid"]);//品牌Id
            //var brandname = Convert.ToString(dr["rental_brandname"]);//品牌名称
            var title = Convert.ToString(dr["rental_title"]);//出售商品标题
            //var number = Convert.ToString(dr["rental_number"]);//出售数量
            var rentnum = Convert.ToString(dr["rental_rentnum"]);//租用量
            var salesvolume = Convert.ToString(dr["rental_salesvolume"]);//销量
            var price = Convert.ToString(dr["rental_price"]);//市场价
            var monthprice = Convert.ToString(dr["rental_monthprice"]);//月租金
            var tags = Convert.ToString(dr["rental_tags"]);//标签
            var begintime= UNIX_TIMESTAMP(Convert.ToDateTime(dr["rental_begintime"])).ToString(CultureInfo.InvariantCulture);//开始时间
            var endtime = UNIX_TIMESTAMP(Convert.ToDateTime(dr["rental_endtime"])).ToString(CultureInfo.InvariantCulture);//结束时间
            var guid = Convert.ToString(dr["rental_guid"]);//guid
            var status = Convert.ToString(dr["rental_status"]);//显示状态
            var state = Convert.ToString(dr["rental_state"]);//审核状态
            var createtime = Convert.ToString(dr["rental_createtime"]);//添加时间

            //IndexWriter writer = new IndexWriter(indexDir, new Lucene.Net.Analysis.KTDictSeg.KTDictSegAnalyzer(), false);
            //Field.Index 说明（摘自百度）
            //Field.Index.ANALYZED 分词建索引 (老版本为Field.Index.TOKENIZED)
            //Field.Index.ANALYZED_NO_NORMS:分词建索引，但是Field的值不像通常那样被保存，而是只取一个byte，这样节约存储空间
            //Field.Index.NOT_ANALYZED:不分词且索引 （老版本为Field.Index.UN_TOKENIZED）
            //Field.Index.NOT_ANALYZED_NO_NORMS:不分词建索引，Field的值去一个byte保存 （老版本为Field.Index.NO_NORMS） 
            //Field.Index.NO:不建立索引

            //Field.Store 说明
            //Field.Store.YES:存储字段值（未分词前的字段值） 
            //Field.Store.NO:不存储,存储与索引没有关系 
            //Field.Store.COMPRESS:压缩存储,用于长文本或二进制，但性能受损
            document.Add(new Field("rental_id", id, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("rental_provinceid", provinceid, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_provincename", provincename, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("rental_cityid", cityid, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_cityname", cityname, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("rental_districtid", districtid, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_districtname", districtname, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("rental_title", title, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            document.Add(new Field("rental_salesvolume", salesvolume, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_rentnum", rentnum, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_price", price, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_monthprice", monthprice, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_tags", tags, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            document.Add(new Field("rental_begintime", begintime, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_begintime", begintime, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_endtime", endtime, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_guid", guid, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("rental_status", status, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_state", state, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("rental_createtime", createtime, Field.Store.YES, Field.Index.NOT_ANALYZED));


            return document;
        }

        /// <summary>
        /// 添加出售商品信息索引
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        Document GetSaleGoodsesDocument(DataRow dr)
        {
            var document = new Document();
            var id = Convert.ToString(dr["sale_id"]); //商品Id
            var provinceid = Convert.ToString(dr["sale_provinceid"]);//省份Id
            var provincename = Convert.ToString(dr["sale_provincename"]);//省份名称
            var cityid = Convert.ToString(dr["sale_cityid"]);//城市Id
            var cityname = Convert.ToString(dr["sale_cityname"]);//城市名称
            var districtid = Convert.ToString(dr["sale_districtid"]);//区域Id
            var districtname = Convert.ToString(dr["sale_districtname"]);//区域名称
            var title = Convert.ToString(dr["sale_title"]);//商品标题
            var degree = Convert.ToString(dr["sale_degree"]);//新旧程度
            var price = Convert.ToString(dr["sale_price"]);//出售价格
            var contact = Convert.ToString(dr["sale_contact"]);//联系人
            var contactphone = Convert.ToString(dr["sale_contactphone"]);//联系电话
            var guid = Convert.ToString(dr["sale_guid"]);//guid
            var status = Convert.ToString(dr["sale_status"]);//显示状态
            var state = Convert.ToString(dr["sale_state"]);//审核状态
            var createtime = UNIX_TIMESTAMP(Convert.ToDateTime(dr["sale_createtime"])).ToString(); //添加时间

            
            document.Add(new Field("sale_id", id, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_provinceid", provinceid, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_provincename", provincename, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_cityid", cityid, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_cityname", cityname, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_districtid", districtid, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_districtname", districtname, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_title", title, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            document.Add(new Field("sale_degree", degree, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_price", price, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_contact", contact, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_contactphone", contactphone, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_guid", guid, Field.Store.YES, Field.Index.NO));
            document.Add(new Field("sale_status", status, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_state", state, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("sale_createtime", createtime, Field.Store.YES, Field.Index.NOT_ANALYZED));

            return document;

        }

        /// <summary>
        /// 时间转成Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static long UNIX_TIMESTAMP(DateTime dateTime)
        {
            try
            {
                long i = (dateTime.Ticks - DateTime.Parse("2010-01-01 00:00:00").Ticks) / 10000000;//自己改的时间
                if (i == 0 || i == null || i < 1000)
                    i = 1000;
                return i;
            }
            catch { return 1000; }

        }


        /// <summary>
        /// 添加品牌索引
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        Lucene.Net.Documents.Document GetBrandsDocument(DataRow dr)
        {
            Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
            string Id = Convert.ToString(dr["brand_id"]);
            document.Add(new Field("brand_id", Id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("brand_name", Convert.ToString(dr["brand_name"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

            DataTable table = searchDal.GetCategoryBrands(Id);
            StringBuilder strs = new StringBuilder("");
            if (table != null && table.Rows.Count > 0)
            {
               
                foreach (DataRow row in table.Rows)
                {
                    strs.Append(row["category_id"].ToString());
                    strs.Append(",");
                }

            }
            document.Add(new Field("category_ids", strs.ToString(), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));


            return document;
        }
        /// <summary>
        /// 添加属性索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  int  AddDocForAttributes(string id)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string idstr = "0";
            try
            {
                DataTable table = searchDal.GetAttributes(id);
                if (table != null && table.Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(id);
                    directory = FSDirectory.Open(new DirectoryInfo(AttributesIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);


                    for (int k = 0; k < table.Rows.Count; k++)
                    {

                        DataRow dr = table.Rows[k];
                        idstr = dr["attr_id"].ToString();
                        Console.WriteLine("id:" + idstr);
                        try
                        {
                            Lucene.Net.Documents.Document document = GetAttributesDocument(dr);

                            indexWriter.AddDocument(document);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            KYJ.ZS.Commons.Tool.Log("添加属性索引异常", "ID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
                        }
                    }
                    KYJ.ZS.Commons.Tool.Log("添加属性索引完成", "count:" + count, LogPath);
                    return count;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Commons.Tool.Log("添加属性索引异常", "ID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
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
        /// 添加属性索引
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        Lucene.Net.Documents.Document GetAttributesDocument(DataRow dr)
        {
            Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
            string Id = Convert.ToString(dr["attr_id"]);
            document.Add(new Field("attr_id", Id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("attr_name", Convert.ToString(dr["attr_name"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

            DataTable table = searchDal.GetCategoryAttributes(Id);
            StringBuilder strs = new StringBuilder("");
            if (table != null && table.Rows.Count > 0)
            {

                foreach (DataRow row in table.Rows)
                {
                    strs.Append(row["category_id"].ToString());
                    strs.Append(",");
                }

            }
            document.Add(new Field("category_ids", strs.ToString(), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));


            return document;
        }


        /// <summary>
        /// 添加分类索引
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddDocForCategories(string id)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string idstr = "0";
            try
            {
                DataTable table = searchDal.GetCategories();
                if (table != null && table.Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(id);
                    directory = FSDirectory.Open(new DirectoryInfo(CategoriesIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);


                    for (int k = 0; k < table.Rows.Count; k++)
                    {

                        DataRow dr = table.Rows[k];
                        idstr = dr["category_id"].ToString();
                        Console.WriteLine("id:" + idstr);
                        try
                        {
                            Lucene.Net.Documents.Document document = GetCategoriesDocument(dr);

                            indexWriter.AddDocument(document);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            KYJ.ZS.Commons.Tool.Log("添加属性索引异常", "ID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
                        }
                    }
                    KYJ.ZS.Commons.Tool.Log("添加属性索引完成", "count:" + count, LogPath);
                    return count;
                }
            }
            catch (Exception ex)
            {
                KYJ.ZS.Commons.Tool.Log("添加属性索引异常", "ID:" + idstr + "索引添加错误：" + ex.Message, LogPath);
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
        /// 添加属性索引
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        Lucene.Net.Documents.Document GetCategoriesDocument(DataRow dr)
        {
            Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
            string Id = Convert.ToString(dr["category_id"]);
            document.Add(new Field("category_id", Id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("category_name", Convert.ToString(dr["category_name"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));


            document.Add(new Field("category_pid", Convert.ToString(dr["category_pid"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

            document.Add(new Field("category_level", Convert.ToString(dr["category_level"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            document.Add(new Field("category_type", Convert.ToString(dr["category_type"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            return document;
        }
    }
}
