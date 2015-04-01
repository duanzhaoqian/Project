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
using ServiceStack;
namespace TXSearchService
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
        public string CityIndexPath
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

        public DataSet AdvertConfitData
        {
            get;
            set;
        }
        #endregion



        #region 普通租房索引生成
        public int AddDocForLongHouse(string houseid)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string houseidstr = "0";
            try
            {
                DataSet ds = HouseDal.GetDataSetForLongHouse(houseid);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(houseid);
                    directory = FSDirectory.Open(new DirectoryInfo(HouseIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);


                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {

                        DataRow dr = ds.Tables[0].Rows[k];
                        houseidstr = dr["Id"].ToString();
                        try
                        {
                            Lucene.Net.Documents.Document document = GetHouseDocument(dr);

                            indexWriter.AddDocument(document);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("添加房源索引异常", "HouseID:" + houseidstr + "索引添加错误：" + ex.Message, LogPath);
                        }
                    }
                    TXCommons.Refurbish.LogTool.Log("添加房源索引完成", "count:" + count, LogPath);
                    return count;
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("添加房源索引异常", "HouseID:" + houseidstr + "索引添加错误：" + ex.Message, LogPath);
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
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public Document GetHouseDocument(DataRow dr)
        {


          
            Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
            string Id = Convert.ToString(dr["Id"]);
            string searchtype=GetHouseTypestr(Convert.ToString(dr["SearchType"]));
            document.Add(new Field("Id", Id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            int cid = Convert.ToInt32(dr["CityId"].ToString());
            document.Add(new Field("CityId", cid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("SearchType", searchtype, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("PropertyType", GetPropertyTypestr(Convert.ToString(dr["PropertyType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("CityName", Convert.ToString(dr["CityName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           



            document.Add(new Field("DName", Convert.ToString(dr["DName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           
            document.Add(new Field("BName", Convert.ToString(dr["BName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            string cityPY = TXCommons.Han2Ping.Convert(dr["CityName"].ToString().Replace("市", ""));
            document.Add(new Field("CityPY", cityPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            string DNamePY = TXCommons.Han2Ping.Convert(dr["DName"].ToString());
            document.Add(new Field("DNamePY", DNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            string BNamePY = TXCommons.Han2Ping.Convert(dr["BName"].ToString());
            document.Add(new Field("BNamePY", BNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("RentType", Convert.ToString(dr["RentType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("Price", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["QuotedPrice"])));
            document.Add(new NumericField("Area", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble((dr["Area"]))));

            document.Add(new NumericField("Room", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["Room"])));

            document.Add(new Field("Hall", Convert.ToString(dr["Hall"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("Facilities", Convert.ToString(dr["Facilities"]).Replace(",", " "), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            document.Add(new Field("Renovation", Convert.ToString(dr["Renovation"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

           


           
            string userType = Convert.ToString(dr["UserType"]);

            document.Add(new Field("UserType", userType, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            string uid = Convert.ToString(dr["UId"]);

            document.Add(new Field("UId", uid, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));


            document.Add(new Field("PType", Convert.ToString(dr["PType"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            string vid = dr["VId"].ToString();
           
            DataSet ds = SearchDal.GetVillageSubWay(vid);
           
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow rowv = ds.Tables[0].Rows[0];
                document.Add(new Field("TId", rowv["tid"].ToString(), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

                document.Add(new Field("PId", rowv["pid"].ToString(), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            }
            else
            {
                document.Add(new Field("TId", "0", Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

                document.Add(new Field("PId", "0", Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            }



           
            document.Add(new Field("VName", Convert.ToString(dr["VName"]), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Address", Convert.ToString(dr["Address"]), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("Title", Convert.ToString(dr["Title"]), Field.Store.YES, Field.Index.ANALYZED));


            //卧室图
            string PictureUrl = string.Empty;

            int picnum = 0;
            
            int searchType = Convert.ToInt32(dr["SearchType"]);
            try
            {
                TXCommons.PictureModular.RentHousePictureInfo ipic = TXCommons.PictureModular.GetPicture.GetFirstRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.LONGLIST.ToString(), cid);
                if (ipic != null)
                {
                    PictureUrl = string.IsNullOrEmpty(ipic.Path) ? "" : ipic.Path;
                }
                //List<TXCommons.PictureModular.RentHousePictureInfo> LivingPictureList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.LIVINGROOM.ToString(), cid);
                //if (LivingPictureList != null)
                //    picnum += LivingPictureList.Count;

                //List<TXCommons.PictureModular.RentHousePictureInfo> RoomPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.ROOMTYPE.ToString(), cid);
                //if (RoomPicList != null)
                //    picnum += RoomPicList.Count;

                //List<TXCommons.PictureModular.RentHousePictureInfo> VillagePicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.VILLAGE.ToString(), cid);
                //if (VillagePicList != null)
                //    picnum += VillagePicList.Count;
                //List<TXCommons.PictureModular.RentHousePictureInfo> BEDROOMPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.BEDROOM.ToString(), cid);
                //if (BEDROOMPicList != null)
                //    picnum += BEDROOMPicList.Count;
                //List<TXCommons.PictureModular.RentHousePictureInfo> TRAFFICPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.TRAFFIC.ToString(), cid);
                //if (TRAFFICPicList != null)
                //    picnum += TRAFFICPicList.Count;
                //List<TXCommons.PictureModular.RentHousePictureInfo> OTHERPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.OTHER.ToString(), cid);
                //if (OTHERPicList != null)
                //    picnum += OTHERPicList.Count;
                //List<TXCommons.PictureModular.RentHousePictureInfo> KITCHENPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.KITCHEN.ToString(), cid);
                //if (KITCHENPicList != null)
                //    picnum += KITCHENPicList.Count;
                //2014年5月24日 09:48:57 
                //窦海超注释掉，以前需求把浏览量保存到redis。现在是做的日志分析
                //TXCommons.PictureModular.Redis.GetNextSequence(FunctionTypeEnum.Identity, 0, Id);//房源浏览量 
         
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("img:", ex.Message, LogPath);
            }

            document.Add(new Field("PictureUrl", PictureUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("picNum", picnum.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            

            DateTime dateTime = DateTime.Now;

            
            document.Add(new NumericField("UpdateTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["updatetime"]))));
           
            
            document.Add(new Field("Orientation", Convert.ToString(dr["Orientation"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("BidUserNum", Convert.ToString(dr["BidUserNum"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("UName", Convert.ToString(dr["UName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));


            document.Add(new NumericField("BuildingArea", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["BuildingArea"])));

           
           
           
           
          
            document.Add(new Field("CreateTime", Convert.ToString(dr["CreateTime"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           
            document.Add(new Field("HouseType", Convert.ToString(dr["HouseType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            
           
            DataRow uRow = null;
            if (userType == "0")
            {
                uRow = HouseDal.GetUser(uid);
            }
            if (userType == "1")
            {
                uRow = HouseDal.GetAgent(uid);
            }
            //string sex = "0";
            //if (userType == "0")
            //{
            //    sex = uRow["Sex"].ToString();
            //}
           // document.Add(new Field("Sex", sex, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //int IsExcellent = 0;
            //string EMail = "";
            //string QQ = "";
            //string Wechat = "";
            //string IsAllowShow = "";
            string Mobile = "";
            string CompanyName = "";
            if (userType == "1")
            {
               
                int acid = Convert.ToInt16(dr["IsSVip"]);
                if (acid == 1)
                {
                    Mobile = Convert.ToString(dr["Mobile"]);
                    CompanyName = "个人";
                }
                else
                {
                    //IsExcellent = Convert.ToInt16(uRow["IsExcellent"]);
                    //EMail = Convert.ToString(uRow["EMail"]);
                    //QQ = Convert.ToString(uRow["QQ"]);
                    //Wechat = Convert.ToString(uRow["Wechat"]);
                    //IsAllowShow = Convert.ToString(uRow["IsAllowShow"]);
                    Mobile = Convert.ToString(uRow["Mobile"]);
                    CompanyName = Convert.ToString(dr["CompanyName"]);
                }

            }
            else
            {
                //EMail = Convert.ToString(uRow["EMail"]);
                //QQ = Convert.ToString(uRow["QQ"]);
                //Wechat = Convert.ToString(uRow["Wechat"]);
                //IsAllowShow = Convert.ToString(uRow["IsAllowShow"]);
                Mobile = Convert.ToString(uRow["Mobile"]);
                CompanyName = Convert.ToString(dr["CompanyName"]);
            }
            document.Add(new Field("CompanyName", CompanyName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           
            document.Add(new Field("Mobile", Mobile, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

           
            document.Add(new Field("QuotedMaxPrice", Convert.ToString(dr["QuotedMaxPrice"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new NumericField("QuotedMinPrice", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["QuotedMinPrice"])));
           
            document.Add(new Field("IsSheng", Convert.ToString(Convert.ToInt16(dr["IsSheng"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           
           
            document.Add(new Field("IsSVip", Convert.ToString(Convert.ToInt16(dr["IsSVip"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("IsPlat", Convert.ToString(Convert.ToInt16(dr["IsPlat"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsDK", Convert.ToString(Convert.ToInt16(dr["IsDK"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
          
            document.Add(new Field("IsGuarantee", Convert.ToString(Convert.ToInt16(dr["IsGuarantee"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("IsAgency", Convert.ToString(Convert.ToInt16(dr["IsAgency"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsDaiK", Convert.ToString(Convert.ToInt16(dr["IsDaiK"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            if (Convert.ToInt16(dr["IsPlat"]) == 1 || Convert.ToInt16(dr["IsGuarantee"]) == 1)
            {
                document.Add(new Field("IsReturn", "1", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            }
            else
            {
                document.Add(new Field("IsReturn", "0", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            }

            document.Add(new Field("IsReal", Convert.ToString(Convert.ToInt16(dr["IsReal"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("BuildingStructure", Convert.ToString(Convert.ToInt16(dr["BuildingStructure"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("BuildingType", Convert.ToString(Convert.ToInt16(dr["BuildingType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("BuildingYear", Convert.ToString(dr["BuildingYear"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
          


            document.Add(new NumericField("TheFloar", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["TheFloar"])));
            document.Add(new NumericField("AllFloar", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["AllFloar"])));
            document.Add(new Field("OfficeType", Convert.ToString(Convert.ToInt16(dr["OfficeType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           
            try
            {
                TXCommons.MsgQueue.SendMessage.SendHTMLMessage(searchtype, int.Parse(Id));
            }
            catch(Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("静态页消息", ex.Message, LogPath);
            }
            return document;
        }


        /// <summary>
        /// 添加活动房源
        /// </summary>
        /// <returns></returns>
        //public int AddActiveDocForLongHouse()
        //{
        //    int count = 0;
        //    if (HouseActiveIndexPath.IndexOf("Active") >= 0)
        //    {
        //        Directory directory = null;
        //        Analyzer analyzer = null;
        //        IndexWriter indexWriter = null;

        //        string houseidstr = "0";
        //        try
        //        {
        //            DataSet ds = HouseDal.GetActiveDataSetForLongHouse("");
        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //                bool isCreate = true;
        //                directory = FSDirectory.Open(new DirectoryInfo(HouseActiveIndexPath));
        //                indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
        //                indexWriter.SetMaxFieldLength(15000);


        //                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
        //                {

        //                    DataRow dr = ds.Tables[0].Rows[k];
        //                    houseidstr = dr["Id"].ToString();
        //                    try
        //                    {
        //                        Lucene.Net.Documents.Document document = GetActiveHouseDocument(dr);

        //                        indexWriter.AddDocument(document);
        //                        count++;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        TXCommons.Refurbish.LogTool.Log("添加房源索引异常", "HouseID:" + houseidstr + "索引添加错误：" + ex.Message, LogPath);
        //                    }
        //                }
        //                TXCommons.Refurbish.LogTool.Log("添加房源索引完成", "count:" + count, LogPath);
        //                return count;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            TXCommons.Refurbish.LogTool.Log("添加房源索引异常", "HouseID:" + houseidstr + "索引添加错误：" + ex.Message, LogPath);
        //        }
        //        finally
        //        {
        //            if (indexWriter != null)
        //                indexWriter.Optimize();
        //            if (analyzer != null)
        //                analyzer.Close();
        //            if (indexWriter != null)
        //                indexWriter.Close();
        //            if (directory != null)
        //                directory.Close();
        //        }
        //    }
        //    return count;
        //}


        /// <summary>
        /// 返回活动房源数据
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        //public Document GetActiveHouseDocument(DataRow dr)
        //{

        //    Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
        //    string Id = Convert.ToString(dr["Id"]);
        //    document.Add(new Field("Id", Id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    int cid = Convert.ToInt32(dr["CityId"].ToString());
        //    document.Add(new Field("CityId", cid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new Field("SearchType", GetHouseTypestr(Convert.ToString(dr["Type"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new Field("PropertyType", GetPropertyTypestr(Convert.ToString(dr["PropertyType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    document.Add(new Field("CityName", Convert.ToString(dr["CityName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    string cityPY = TXCommons.Han2Ping.Convert(dr["CityName"].ToString().Replace("市", ""));
        //    document.Add(new Field("CityPY", cityPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));



        //    document.Add(new Field("DName", Convert.ToString(dr["DName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    string DNamePY = TXCommons.Han2Ping.Convert(dr["DName"].ToString());
        //    document.Add(new Field("DNamePY", DNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    document.Add(new Field("BName", Convert.ToString(dr["BName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    string BNamePY = TXCommons.Han2Ping.Convert(dr["BName"].ToString());
        //    document.Add(new Field("BNamePY", BNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));



        //    document.Add(new Field("RentType", Convert.ToString(dr["RentType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    document.Add(new NumericField("Price", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["QuotedPrice"])));
        //    document.Add(new NumericField("Area", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble((dr["Area"]))));

        //    document.Add(new NumericField("Room", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["Room"])));

        //    document.Add(new Field("Hall", Convert.ToString(dr["Hall"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new Field("Facilities", Convert.ToString(dr["SupportingFacilities"]).Replace(",", " "), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
        //    document.Add(new Field("Renovation", Convert.ToString(dr["Renovation"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    int bidStatus = Convert.ToInt32(dr["BidStatus"]);
        //    document.Add(new NumericField("BidStatus", Field.Store.YES, true).SetIntValue(bidStatus));

        //    document.Add(new Field("PType", Convert.ToString(dr["PType"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));




        //    document.Add(new NumericField("BidStartTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["BidStartTime"]))));
        //    document.Add(new NumericField("BidEndTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["BidEndTime"]))));

        //    document.Add(new Field("VName", Convert.ToString(dr["VName"]), Field.Store.YES, Field.Index.ANALYZED));
        //    document.Add(new Field("Address", Convert.ToString(dr["Address"]), Field.Store.YES, Field.Index.ANALYZED));
        //    document.Add(new Field("Title", Convert.ToString(dr["Title"]), Field.Store.YES, Field.Index.ANALYZED));


        //    //卧室图
        //    string PictureUrl = string.Empty;


        //    try
        //    {
        //        TXCommons.PictureModular.RentHousePictureInfo ipic = TXCommons.PictureModular.GetPicture.GetFirstRentHousePictureList(Convert.ToString(dr["InnerCode"]), true, TXCommons.PictureModular.RentHousePictureType.PROPAGANDA.ToString(), cid);
        //        if (ipic != null)
        //        {
        //            PictureUrl = string.IsNullOrEmpty(ipic.Path) ? "" : ipic.Path;
        //        }

        //        TXCommons.PictureModular.Redis.GetNextSequence(FunctionTypeEnum.Identity, 0, Id);//房源浏览量
        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("img:", ex.Message, LogPath);
        //    }
        //    document.Add(new Field("PictureUrl", PictureUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));




        //    document.Add(new NumericField("UpdateTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["updatetime"]))));




        //    document.Add(new Field("Orientation", Convert.ToString(dr["Orientation"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));


        //    document.Add(new NumericField("BuildingArea", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["BuildingArea"])));



        //    document.Add(new Field("BuildingStructure", Convert.ToString(Convert.ToInt16(dr["BuildingStructure"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    document.Add(new Field("AllFloar", Convert.ToString(dr["AllFloar"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new Field("TheFloar", Convert.ToString(dr["TheFloar"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new NumericField("CreateTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["CreateTime"]))));

        //    document.Add(new Field("HouseType", Convert.ToString(dr["HouseType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new NumericField("IsRecommend", Field.Store.YES, true).SetIntValue(Convert.ToInt16(dr["IsRecommend"])));
        //    document.Add(new NumericField("HouseScore", Field.Store.YES, true).SetIntValue(Convert.ToInt16(dr["HouseScore"])));
        //    document.Add(new NumericField("IsReal", Field.Store.YES, true).SetIntValue(Convert.ToInt16(dr["IsReal"])));



        //    document.Add(new Field("QuotedMaxPrice", Convert.ToString(dr["QuotedMaxPrice"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    document.Add(new Field("QuotedMinPrice", Convert.ToString(dr["QuotedMinPrice"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    string userType = Convert.ToString(dr["UserType"]);

        //    document.Add(new Field("UserType", userType, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    string uid = Convert.ToString(dr["UId"]);

        //    document.Add(new Field("UId", uid, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    //DataRow uRow = null;
        //    //if (userType == "0")
        //    //{
        //    //    uRow = HouseDal.GetUser(uid);
        //    //}
        //    //if (userType == "1")
        //    //{
        //    //    uRow = HouseDal.GetAgent(uid);
        //    //}


        //    string BidUserMobile = string.Empty;
        //    string BidUserName = string.Empty;
        //    if (bidStatus == 2 || bidStatus == 4)
        //    {
        //        DataSet dsBid = HouseDal.GetDataSetForBid(Convert.ToString(dr["Id"]));
        //        if (dsBid != null && dsBid.Tables[0].Rows.Count > 0)
        //        {

        //            BidUserMobile = Convert.ToString(dsBid.Tables[0].Rows[0]["BidUserMobile"]);
        //            BidUserName = Convert.ToString(dsBid.Tables[0].Rows[0]["BidUserName"]);
        //        }
        //    }


        //    document.Add(new Field("UName", BidUserName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

        //    document.Add(new Field("Mobile", BidUserMobile, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
        //    return document;
        //}
        /// <summary>
        /// 删除房源索引数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="columnname">列名</param>
        /// <param name="indexpath">索引位置</param>
        /// <returns></returns>
        public bool DeleteHouseDoc(string id, string columnname, string indexpath)
        {
            bool b = false;
            Analyzer analyzer = null;
            Directory directory = null;
            IndexWriter indexWriter = null;
            try
            {
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                bool isCreate = false;
                directory = FSDirectory.Open(new DirectoryInfo(indexpath));

                indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                Term t = new Term(columnname, id);
                indexWriter.DeleteDocuments(t);
                TXCommons.Refurbish.LogTool.Log("删除房源索引完成", "houseid:" + id, LogPath);
                b = true;
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("删除房源索引异常", "删除索引错误(" + indexpath + "):" + id + "_" + ex.Message, LogPath);

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
            return b;
        }

        /// <summary>
        /// 修改房源索引数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="indexpath">索引位置</param>
        /// <returns></returns>
        public bool UpdateHouseDoc(string houseid, string indexpath)
        {
            bool b = false;
            Analyzer analyzer = null;
            Directory directory = null;
            IndexWriter indexWriter = null;
            try
            {
                DataSet ds = HouseDal.GetDataSetForLongHouse(houseid);
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                bool isCreate = false;
                directory = FSDirectory.Open(new DirectoryInfo(indexpath));

                indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                indexWriter.SetMaxFieldLength(15000);
                Term t = new Term("Id", houseid);
                indexWriter.DeleteDocuments(t);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    Document document = GetHouseDocument(ds.Tables[0].Rows[0]);

                    indexWriter.AddDocument(document);
                    TXCommons.Refurbish.LogTool.Log("修改房源索引完成", "houseid:" + houseid + "CityId:" + CityId, LogPath);
                    //ClearCDN(Convert.ToString(ds.Tables[0].Rows[0]["Id"]), Convert.ToString(ds.Tables[0].Rows[0]["CityId"]), Convert.ToString(ds.Tables[0].Rows[0]["SearchType"]), Convert.ToString(ds.Tables[0].Rows[0]["UserType"]));
                    b = true;
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("修改房源索引异常", ex.Message, LogPath);
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
            return b;
        }
        /// <summary>
        /// 批量跟新房源索引
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="indexpath"></param>
        /// <returns></returns>
        public bool UpdateHouseDoc(string houseid, IndexWriter indexWriter, string indexpath, bool clearcdn)
        {
            bool b = false;

            try
            {
                DataSet ds = HouseDal.GetDataSetForLongHouse(houseid);
                indexWriter.SetMaxFieldLength(15000);
                Term t = new Term("Id", houseid);
                indexWriter.DeleteDocuments(t);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    Document document = GetHouseDocument(ds.Tables[0].Rows[0]);

                    indexWriter.AddDocument(document);
                    //if (clearcdn)
                    //    ClearCDN(Convert.ToString(ds.Tables[0].Rows[0]["Id"]), Convert.ToString(ds.Tables[0].Rows[0]["CityId"]), Convert.ToString(ds.Tables[0].Rows[0]["SearchType"]), Convert.ToString(ds.Tables[0].Rows[0]["UserType"]));
                    b = true;
                }
            }
            catch (Exception ex)
            {
                b = false;
                TXCommons.Refurbish.LogTool.Log("批量修改房源索引异常", ex.Message, LogPath);
            }
            finally
            {

            }
            return b;
        }

        /// <summary>
        /// 修改房源索引数据
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="indexpath">索引位置</param>
        /// <returns></returns>
        public void UpdateHouseDoc(string[] arr, string indexpath)
        {

            Analyzer analyzer = null;
            Directory directory = null;
            IndexWriter indexWriter = null;
            try
            {
                TXCommons.Refurbish.LogTool.Log("修改房源索引修改数量:", arr.Length.ToString(), LogPath);
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                bool isCreate = false;
                directory = FSDirectory.Open(new DirectoryInfo(indexpath));

                indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                indexWriter.SetMaxFieldLength(15000);
                foreach (string houseid in arr)
                {
                    DataSet ds = HouseDal.GetDataSetForLongHouse(houseid);

                    Term t = new Term("Id", houseid);
                    indexWriter.DeleteDocuments(t);
                    TXCommons.Refurbish.LogTool.Log("修改房源索引完成", "houseid:" + houseid + "CityId:" + CityId, LogPath);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        Document document = GetHouseDocument(ds.Tables[0].Rows[0]);

                        indexWriter.AddDocument(document);
                        
                        //ClearCDN(Convert.ToString(ds.Tables[0].Rows[0]["Id"]), Convert.ToString(ds.Tables[0].Rows[0]["CityId"]), Convert.ToString(ds.Tables[0].Rows[0]["SearchType"]), Convert.ToString(ds.Tables[0].Rows[0]["UserType"]));

                    }
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("修改房源索引异常", ex.Message, LogPath);
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

        public void SetAdvertConfig()
        {
            AdvertConfitData = SearchDal.GetAdvertConfigure();

        }

        public DataRow SearchAdvertConfig(string areaid,string areatype)
        {
            string sql="  AreaId=" + areaid + " and AreaType="+areatype;
            return  AdvertConfitData.Tables[0].Select(sql)[0];
        }

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
                        CNamePY = CNamePY == "zhongqing" ? "chongqing" : CNamePY;
                        document.Add(new Field("CNamePY", CNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        try
                        {
                            string areaid = Convert.ToInt32(dr["BId"]) == 0 ? Convert.ToString(dr["DId"]) : Convert.ToString(dr["BId"]);
                            string areatype = Convert.ToInt32(dr["BId"]) == 0 ? "2" : "3";
                            DataRow dsConfigure = SearchAdvertConfig(areaid, areatype);
                            document.Add(new Field("AdPrice", Convert.ToString(dsConfigure["AddCount_q"]) + "," + Convert.ToString(dsConfigure["AddPrice_q"]) + "," + Convert.ToString(dsConfigure["AddCount_c"]) + "," + Convert.ToString(dsConfigure["AddPrice_c"]) + "," + Convert.ToString(dsConfigure["AddCount_b"]) + "," + Convert.ToString(dsConfigure["AddPrice_b"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        }
                        catch(Exception ex)
                        {
                            document.Add(new Field("AdPrice", "10-10-10-10-10,2-2-2-2-2,25-25-25-25-25,1-1-1-1-1,240-240-240-240-240,1-1-1-1-1", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                            TXCommons.Refurbish.LogTool.Log("广告数据添加错误", ex.Message, LogPath);
                        }
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
        /// <summary>
        /// 返回城市数据
        /// </summary>
        /// <param name="cityid"></param>
        /// <returns></returns>
        public int AddCityForArea(string cityid)
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = SearchDal.GetCity();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = string.IsNullOrEmpty(cityid);
                    directory = FSDirectory.Open(new DirectoryInfo(CityIndexPath));

                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);

                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();

                        document.Add(new Field("Id", Convert.ToString(dr["Id"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        document.Add(new Field("Name", Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        string NamePY = TXCommons.Han2Ping.Convert(dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Replace("市", ""));
                        NamePY = NamePY == "zhongqing" ? "chongqing" : NamePY;
                        document.Add(new Field("NamePY", NamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        try
                        {

                            DataRow dsConfigure = SearchAdvertConfig(Convert.ToString(dr["Id"]), "1");
                            document.Add(new Field("AdPrice", Convert.ToString(dsConfigure["AddCount_q"]) + "," + Convert.ToString(dsConfigure["AddPrice_q"]) + "," + Convert.ToString(dsConfigure["AddCount_c"]) + "," + Convert.ToString(dsConfigure["AddPrice_c"]) + "," + Convert.ToString(dsConfigure["AddCount_b"]) + "," + Convert.ToString(dsConfigure["AddPrice_b"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        }
                        catch (Exception ex)
                        {
                            document.Add(new Field("AdPrice", "10-10-10-10-10,2-2-2-2-2,25-25-25-25-25,1-1-1-1-1,240-240-240-240-240,1-1-1-1-1", Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                            TXCommons.Refurbish.LogTool.Log("城市数据添加错误", ex.Message, LogPath);
                        }
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

        #region 广告索引生成
        public int AddDocForAdvert()
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            try
            {
                DataSet ds = HouseDal.GetAdvert();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = true;
                    directory = FSDirectory.Open(new DirectoryInfo(AdvertIndexPath));

                    indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                    indexWriter.SetMaxFieldLength(15000);

                   
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        DataRow dr = ds.Tables[0].Rows[k];
                        Lucene.Net.Documents.Document document = updateAdvertIndex(dr);
                        if (document != null)
                        {
                            indexWriter.AddDocument(document);
                        }

                        count++;
                    }
                    return count;


                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("广告数据获取错误1", ex.Message, LogPath);
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

        public Document updateAdvertIndex(DataRow dr)
        {
           
            string houseid = Convert.ToString(dr["HouseId"] == DBNull.Value ? "" : dr["HouseId"]);
            try
            {



                DataSet dsh = HouseDal.GetDataSetForLongHouse(houseid);
                if (dsh != null && dsh.Tables.Count > 0 && dsh.Tables[0].Rows.Count > 0)
                {
                    Lucene.Net.Documents.Document document = GetHouseDocument(dsh.Tables[0].Rows[0]);
                    string areatype = Convert.ToString(dr["AreaType"] == DBNull.Value ? "" : dr["AreaType"]);
                    string areaid=  Convert.ToString(dr["AreaId"] == DBNull.Value ? "" : dr["AreaId"]);
                    if (areatype.Trim() == "0")
                    {
                        TXCommons.Refurbish.LogTool.Log("广告数据获取id:", houseid + "type:" + areatype, LogPath);
                        document.Add(new Field("AreaCityId", Convert.ToString(dr["AreaId"] == DBNull.Value ? "" : dr["AreaId"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        document.Add(new Field("AreaCityName", Convert.ToString(dr["AreaName"] == DBNull.Value ? "" : dr["AreaName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        string AreaCityPY = TXCommons.Han2Ping.Convert(dr["AreaName"].ToString().Replace("市", ""));
                        document.Add(new Field("AreaCityPY", AreaCityPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                    }
                    if (areatype.Trim() == "1")
                    {
                        TXCommons.Refurbish.LogTool.Log("广告数据获取id:", houseid + "type:" + areatype, LogPath);
                        DataSet areadata = SearchDal.GetAreaData(areaid, areatype.Trim());

                        string AreaCityId = Convert.ToString(areadata.Tables[0].Rows[0]["id"] == DBNull.Value ? "" : areadata.Tables[0].Rows[0]["id"]);
                        string AreaCityName = Convert.ToString(areadata.Tables[0].Rows[0]["name"] == DBNull.Value ? "" : areadata.Tables[0].Rows[0]["name"]);
                        document.Add(new Field("AreaCityId", AreaCityId, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        document.Add(new Field("AreaCityName", AreaCityName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        string AreaCityPY = TXCommons.Han2Ping.Convert(AreaCityName.Replace("市", ""));
                        document.Add(new Field("AreaCityPY", AreaCityPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    }

                    if (areatype.Trim() == "2")
                    {
                        TXCommons.Refurbish.LogTool.Log("广告数据获取id:", houseid + "type:" + areatype, LogPath);
                        DataSet areadata = SearchDal.GetAreaData(areaid, areatype.Trim());

                        string AreaCityId = Convert.ToString(areadata.Tables[0].Rows[0]["id"] == DBNull.Value ? "" : areadata.Tables[0].Rows[0]["id"]);
                        string AreaCityName = Convert.ToString(areadata.Tables[0].Rows[0]["name"] == DBNull.Value ? "" : areadata.Tables[0].Rows[0]["name"]);
                        document.Add(new Field("AreaCityId", AreaCityId, Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                        document.Add(new Field("AreaCityName", AreaCityName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                        string AreaCityPY = TXCommons.Han2Ping.Convert(AreaCityName.Replace("市", ""));
                        document.Add(new Field("AreaCityPY", AreaCityPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                    }



                    document.Add(new Field("AreaId", Convert.ToString(dr["AreaId"] == DBNull.Value ? "" : dr["AreaId"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

                    document.Add(new Field("AreaType", Convert.ToString(dr["AreaType"] == DBNull.Value ? "" : dr["AreaType"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                    document.Add(new Field("BuyType", Convert.ToString(dr["BuyType"] == DBNull.Value ? "" : dr["BuyType"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
                    document.Add(new Field("AreaName", Convert.ToString(dr["AreaName"] == DBNull.Value ? "" : dr["AreaName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    string AreaPY = TXCommons.Han2Ping.Convert(dr["AreaName"].ToString().Replace("市", ""));
                    document.Add(new Field("AreaPY", AreaPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    document.Add(new NumericField("AdvertCreateTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["CreateTime"]))));
                    return document;
                }
                else
                {
                   
                    TXCommons.Refurbish.LogTool.Log("房源已下架id:", houseid, LogPath);
                    return null;
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("广告数据获取错误2:" + houseid, ex.Message, LogPath);
                return null;
            }
        }
        /// <summary>
        /// 修改广告房源数据
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="indexpath"></param>
        public void UpdateAdvertDoc(string houseid)
        {

            Analyzer analyzer = null;
            Directory directory = null;
            IndexWriter indexWriter = null;
            try
            {
                TXCommons.Refurbish.LogTool.Log("修改广告索引修改id:", houseid, LogPath);
                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                bool isCreate = false;
                directory = FSDirectory.Open(new DirectoryInfo(AdvertIndexPath));

                indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
                indexWriter.SetMaxFieldLength(15000);
               
                    DataSet ds = HouseDal.pdAdvert(houseid);

                    Term t = new Term("Id", houseid);
                    indexWriter.DeleteDocuments(t);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        Document document = updateAdvertIndex(ds.Tables[0].Rows[0]);
                        if (document != null)
                        {
                            indexWriter.AddDocument(document);
                        }
                        TXCommons.Refurbish.LogTool.Log("修改广告索引完成", "houseid:" + houseid + "CityId:" + CityId, LogPath);
                        //ClearCDN(Convert.ToString(ds.Tables[0].Rows[0]["Id"]), Convert.ToString(ds.Tables[0].Rows[0]["CityId"]), Convert.ToString(ds.Tables[0].Rows[0]["SearchType"]), Convert.ToString(ds.Tables[0].Rows[0]["UserType"]));

                    }
                    else
                    {
                        TXCommons.Refurbish.LogTool.Log("广告索引不存在", "houseid:" + houseid + "CityId:" + CityId, LogPath);
                    }
                
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("修改房源索引异常", ex.Message, LogPath);
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

        public int UpdateAdvertList(string type, string houseid)
        {
            int count = 0;
             Monitor.Enter(lockobject3);
             try
             {
                 if (type == "index")
                 {
                    count= AddDocForAdvert();
                 }
                 if (type == "list")
                 {
                     UpdateAdvertDoc(houseid);
                 }
             }
             catch (Exception ex)
             {
                 TXCommons.Refurbish.LogTool.Log(type + "服务错误", ex.Message, LogPath);
             }
            finally
            {
                Monitor.Exit(lockobject3);
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

        #region   修改房源索引统一处理
        public void UpdateLucene(string houseid, string indexpath, string type, DateTime date, string userid = "0", string usertype = "0", string[] arr = null)
        {
            Monitor.Enter(lockobject1);
            try
            {
                // TXCommons.Refurbish.LogTool.Log("处理:"+type+"消息开始:---------------------",houseid, LogPath);
                if (type == "Update")
                {
                    UpdateHouseDoc(houseid, indexpath);
                }
                if (type == "UpdateRefurbish")
                {
                    //UpdateRefurbish(date);
                }
                if (type == "User")
                {
                    UpdateIndexByUser(userid, usertype);
                }
                if (type == "List")
                {
                    UpdateHouseDoc(arr, indexpath);
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log(type + "服务错误", ex.Message, LogPath);
            }
            finally
            {
                Monitor.Exit(lockobject1);
            }

        }




        /// <summary>
        /// 修改活动房源
        /// </summary>
        /// <param name="houseid"></param>
        /// <param name="indexpath"></param>
        /// <param name="type"></param>
        /// <param name="userid"></param>
        /// <param name="usertype"></param>
        //public void UpdateActiveLucene(string houseid, string indexpath, string type, string userid = "0", string usertype = "0")
        //{
        //    if (indexpath.IndexOf("Active") >= 0)
        //    {



        //        if (type == "Update")
        //        {
        //            Analyzer analyzer = null;
        //            Directory directory = null;
        //            IndexWriter indexWriter = null;
        //            Monitor.Enter(lockobject2);
        //            try
        //            {
        //                DataSet ds = HouseDal.GetActiveDataSetForLongHouse(houseid);
        //                analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
        //                bool isCreate = false;

        //                directory = FSDirectory.Open(new DirectoryInfo(indexpath));

        //                indexWriter = new IndexWriter(directory, false, analyzer, isCreate);
        //                indexWriter.SetMaxFieldLength(15000);
        //                Term t = new Term("Id", houseid);
        //                indexWriter.DeleteDocuments(t);
        //                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //                {

        //                    Document document = GetActiveHouseDocument(ds.Tables[0].Rows[0]);

        //                    indexWriter.AddDocument(document);
        //                    TXCommons.Refurbish.LogTool.Log("修改活动房源索引完成", "houseid:" + houseid + "CityId:" + CityId, LogPath);
        //                    //ClearCDN(Convert.ToString(ds.Tables[0].Rows[0]["Id"]), Convert.ToString(ds.Tables[0].Rows[0]["CityId"]), Convert.ToString(ds.Tables[0].Rows[0]["Type"]), Convert.ToString(ds.Tables[0].Rows[0]["UserType"]));

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                TXCommons.Refurbish.LogTool.Log("修改活动房源索引异常", ex.Message, LogPath);
        //            }
        //            finally
        //            {
        //                if (analyzer != null)
        //                    analyzer.Close();
        //                if (indexWriter != null)
        //                    indexWriter.Close();
        //                if (directory != null)
        //                    directory.Close();
        //                Monitor.Exit(lockobject2);
        //            }
        //        }


        //    }
        //}

        #region   预约刷新处理
        /// <summary>
        /// 更新刷新房源
        /// </summary>
        //public void UpdateRefurbish(DateTime date)
        //{

        //    Analyzer analyzer = null;
        //    Directory directory = null;
        //    IndexWriter indexWriter = null;

        //    try
        //    {


        //        int userid = 0;
        //        int houseid = 0;
        //        int rcount = 0;
        //        int acount = 0;

        //        DataSet dataset = HouseDal.GetTxRefurbishTimingData(date, CityId);
        //        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        //        {
        //            analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

        //            directory = FSDirectory.Open(new DirectoryInfo(HouseIndexPath));
        //            indexWriter = new IndexWriter(directory, false, analyzer, false);
        //            DataTable table = dataset.Tables[0];
        //            TXCommons.Refurbish.LogTool.Log("刷新数量:", dataset.Tables[0].Rows.Count.ToString() + "CityId:" + CityId, LogPath);
        //            foreach (DataRow row in table.Rows)
        //            {
        //                try
        //                {
        //                    userid = int.Parse(row["userid"].ToString());
        //                    houseid = int.Parse(row["houseid"].ToString());
        //                    rcount = int.Parse(row["rcount"].ToString());
        //                    acount = int.Parse(row["acount"].ToString());
        //                    if (rcount < acount)
        //                    {

        //                        HouseDal.RefurbishR(userid, houseid, date);
        //                        if (UpdateHouseDoc(houseid.ToString(), indexWriter, HouseIndexPath, false))
        //                        {
        //                            TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务已刷新", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString() + "-rcount" + rcount + "-acount:" + acount + "-cityid:" + CityId, LogPath);
        //                        }
        //                        else
        //                        {
        //                            TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务索引不存在", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString() + "-rcount" + rcount + "-acount:" + acount, LogPath);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        HouseDal.RefurbishF(userid, houseid, date);
        //                        TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务未刷新", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString() + "-rcount" + rcount + "-acount:" + acount, LogPath);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    TXCommons.Refurbish.LogTool.Log("房源刷新错误：", ex.Message, LogPath);
        //                }
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        TXCommons.Refurbish.LogTool.Log("房源预约刷新定时服务", "房源预约刷新定时服务执行错误:" + ex.Message, LogPath);
        //    }
        //    finally
        //    {
        //        if (analyzer != null)
        //            analyzer.Close();
        //        if (indexWriter != null)
        //            indexWriter.Close();
        //        if (directory != null)
        //            directory.Close();
        //    }

        //}
        #endregion

        #region   根据用户Id根新索引
        public void UpdateIndexByUser(string userid, string usertype)
        {
            DateTime date = DateTime.Now;

            Analyzer analyzer = null;
            Directory directory = null;
            IndexWriter indexWriter = null;

            try
            {

                DataSet dataset = HouseDal.GetDataSetForLongHouse(userid, usertype, CityId);
                if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);

                    directory = FSDirectory.Open(new DirectoryInfo(HouseIndexPath));
                    indexWriter = new IndexWriter(directory, false, analyzer, false);
                    DataTable table = dataset.Tables[0];
                    TXCommons.Refurbish.LogTool.Log("房源手机号码刷新开始", "count:" + dataset.Tables[0].Rows.Count + "CityId:" + CityId, LogPath);
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {

                            string houseid = row["Id"].ToString();



                            if (UpdateHouseDoc(houseid, indexWriter, HouseIndexPath, false))
                            {
                                TXCommons.Refurbish.LogTool.Log("房源手机号码已刷新", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString(), LogPath);
                            }
                            else
                            {
                                TXCommons.Refurbish.LogTool.Log("房源手机号码刷新索引不存在", "userid:" + userid + "-houseid:" + houseid + "-datetime:" + date.ToString(), LogPath);
                            }

                        }
                        catch (Exception ex)
                        {
                            TXCommons.Refurbish.LogTool.Log("房源手机号码刷新错误：", ex.Message, LogPath);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("房源手机号码刷新", "房源预约刷新定时服务执行错误:" + ex.Message, LogPath);
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



        #endregion
    }

        #endregion

}
