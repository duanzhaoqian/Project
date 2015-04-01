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
namespace HouseDataInit
{
    public class LuceneOpt
    {
        #region 属性

        private static object lockobject1 = new object();

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
        public string LogPath
        {
            get;
            set;
        }
        public SearchServiceDal HouseDal
        {
            get;
            set;
        }
        public SearchServiceDal SearchDal
        {
            get;
            set;
        }
        public SearchServiceDal UserDal
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



        #region 普通租房索引生成
        public int AddDocForLongHouse()
        {
            Directory directory = null;
            Analyzer analyzer = null;
            IndexWriter indexWriter = null;
            int count = 0;
            string houseidstr = "0";
            try
            {
                DataSet ds = HouseDal.GetDataSetForLongHouse(CityId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
                    bool isCreate = true;
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
                if (indexWriter != null)
                    indexWriter.Optimize();
                if (analyzer != null)
                    analyzer.Close();
                if (indexWriter != null)
                    indexWriter.Close();
                if (directory != null)
                    directory.Close();
            }
            return count;
        }
        public Document GetHouseDocument(DataRow dr)
        {



            Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
            string Id = Convert.ToString(dr["Id"]);
            document.Add(new Field("Id", Id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            int cid = Convert.ToInt32(dr["CityId"].ToString());
            document.Add(new Field("CityId", cid.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("SearchType", GetHouseTypestr(Convert.ToString(dr["SearchType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
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
           
            Console.WriteLine("HouseId:" + Id);
            return document;
        }
        // Console.WriteLine("HouseId:" + Id);
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



            
        //    document.Add(new NumericField("BidStartTime", Field.Store.YES, true).SetLongValue(TXCommons .Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["BidStartTime"]))));
        //    document.Add(new NumericField("BidEndTime", Field.Store.YES, true).SetLongValue(TXCommons .Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["BidEndTime"]))));

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




        //    document.Add(new NumericField("UpdateTime", Field.Store.YES, true).SetLongValue(TXCommons .Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["updatetime"]))));




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
        //    DataRow uRow = null;
        //    if (userType == "0")
        //    {
        //        uRow = HouseDal.GetUser(uid);
        //    }
        //    if (userType == "1")
        //    {
        //        uRow = HouseDal.GetAgent(uid);
        //    }
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
        //    Console.WriteLine("ActiveId:" + Id);
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
                    TXCommons.Refurbish.LogTool.Log("修改房源索引完成", "houseid:" + houseid, LogPath);
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
