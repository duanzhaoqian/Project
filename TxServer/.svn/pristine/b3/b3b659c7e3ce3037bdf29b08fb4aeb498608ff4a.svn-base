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

namespace TXRefurbish
{
    public class LuceneOpt
    {
        #region 属性
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
        #endregion

      

        #region 普通租房索引生成
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public Document GetHouseDocument(DataRow dr)
        {
           
            Lucene.Net.Documents.Document document = new Lucene.Net.Documents.Document();
           
            document.Add(new Field("Id", Convert.ToString(dr["Id"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("CityId", Convert.ToString(dr["CityId"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("SearchType", GetHouseTypestr(Convert.ToString(dr["SearchType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("PropertyType", GetPropertyTypestr(Convert.ToString(dr["PropertyType"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("CityName", Convert.ToString(dr["CityName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            string cityPY = TXCommons.Han2Ping.Convert(dr["CityName"].ToString().Replace("市", ""));
            document.Add(new Field("CityPY", cityPY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));



            document.Add(new Field("DName", Convert.ToString(dr["DName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            string DNamePY = TXCommons.Han2Ping.Convert(dr["DName"].ToString());
            document.Add(new Field("DNamePY", DNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("BName", Convert.ToString(dr["BName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            string BNamePY = TXCommons.Han2Ping.Convert(dr["BName"].ToString());
            document.Add(new Field("BNamePY", BNamePY, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));



            document.Add(new Field("RentType", Convert.ToString(dr["RentType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("Mobile", Convert.ToString(dr["Mobile"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new NumericField("Price", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["QuotedPrice"])));
            document.Add(new NumericField("Area", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["Area"])));

            document.Add(new NumericField("Room", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["Room"])));
          
            document.Add(new Field("Hall", Convert.ToString(dr["Hall"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("Facilities", Convert.ToString(dr["Facilities"]).Replace(",", " "), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            document.Add(new Field("Renovation", Convert.ToString(dr["Renovation"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            int bidStatus = Convert.ToInt32(dr["BidStatus"]);
            document.Add(new NumericField("BidStatus", Field.Store.YES, true).SetIntValue(bidStatus));


            int BidStatusOrder = 5;
            if (bidStatus == 1) BidStatusOrder = 0;
            else if (bidStatus == 0) BidStatusOrder = 1;
            else if (bidStatus == 4) BidStatusOrder = 2;
            else if (bidStatus == 2) BidStatusOrder = 3;
            else if (bidStatus == 3) BidStatusOrder = 4;

            document.Add(new NumericField("BidStatusOrder", Field.Store.YES, true).SetIntValue(BidStatusOrder));

          

            document.Add(new Field("IsAgency", Convert.ToString(Convert.ToInt16( dr["IsAgency"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("UserType", Convert.ToString(dr["UserType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field("VName", Convert.ToString(dr["VName"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

            document.Add(new Field("Address", Convert.ToString(dr["Address"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

            document.Add(new Field("PType", Convert.ToString(dr["PType"]), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            string vid = dr["VId"].ToString();
            ArrayList arr = GetVidBySubWay(vid);
            if (arr != null)
            {
                document.Add(new Field("TId", arr[0].ToString(), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

                document.Add(new Field("PId", arr[1].ToString(), Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            }
            else
            {
                document.Add(new Field("TId", "0", Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));

                document.Add(new Field("PId", "0", Field.Store.YES, Field.Index.ANALYZED_NO_NORMS));
            }



            double BidPrice = 0;
            string BidUserMobile = string.Empty;
            string BidUserName = string.Empty;
            if (bidStatus == 2 || bidStatus == 4)
            {
                DataSet dsBid = HouseDal.GetDataSetForBid(Convert.ToString(dr["Id"]));
                if (dsBid != null && dsBid.Tables[0].Rows.Count > 0)
                {
                    BidPrice = Convert.ToDouble(dsBid.Tables[0].Rows[0]["BidPrice"]);
                    BidUserMobile = Convert.ToString(dsBid.Tables[0].Rows[0]["BidUserMobile"]);
                    BidUserName = Convert.ToString(dsBid.Tables[0].Rows[0]["BidUserName"]);
                }
            }
            document.Add(new NumericField("BidPrice", Field.Store.YES, true).SetDoubleValue(BidPrice));

            document.Add(new Field("BidUserMobile", BidUserMobile, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("BidUserName", BidUserName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new NumericField("BidCount", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["BidCount"])));

            document.Add(new NumericField("BidStartTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["BidStartTime"]))));
            document.Add(new NumericField("BidEndTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["BidEndTime"]))));

            document.Add(new Field("Title", Convert.ToString(dr["Title"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));


            //卧室图
            string PictureUrl = string.Empty;
            string BedroomSecondPicUrl = string.Empty;
            //获取客厅图
            string LivingPictureUrl = string.Empty;
            string LivingSecondPicUrl = string.Empty;
            //小区图
            string VillagePicUrl = string.Empty;
            //户型图 
            string HousePicUrl = string.Empty;
            string IsMorePic = "0";
            string PicType = "1";
            int searchType = Convert.ToInt32(dr["SearchType"]);
            try
            {
                TXCommons.PictureModular.RentHousePictureInfo ipic = TXCommons.PictureModular.GetPicture.GetFirstRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.LONGLIST.ToString());
                if (ipic != null)
                {
                    PictureUrl = string.IsNullOrEmpty(ipic.Path) ? "" : ipic.Path;
                }
                List<TXCommons.PictureModular.RentHousePictureInfo> LivingPictureList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.LIVINGROOM.ToString());
                if (LivingPictureList != null && LivingPictureList.Count >= 1 && searchType == 0)
                    LivingPictureUrl = string.IsNullOrEmpty(LivingPictureList[0].Path) ? "" : LivingPictureList[0].Path;
                 List<TXCommons.PictureModular.RentHousePictureInfo> RoomPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.ROOMTYPE.ToString());
                 if (RoomPicList != null && RoomPicList.Count >= 1 && searchType == 1)
                     LivingPictureUrl = string.IsNullOrEmpty(RoomPicList[0].Path) ? "" : RoomPicList[0].Path;
                List<TXCommons.PictureModular.RentHousePictureInfo> BedRoomPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.BEDROOM.ToString());
               
                List<TXCommons.PictureModular.RentHousePictureInfo> VillagePicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.VILLAGE.ToString());
                List<TXCommons.PictureModular.RentHousePictureInfo> OtherPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.OTHER.ToString());
                List<TXCommons.PictureModular.RentHousePictureInfo> KITCHENPicList = TXCommons.PictureModular.GetPicture.GetRentHousePictureList(Convert.ToString(dr["InnerCode"]), false, TXCommons.PictureModular.RentHousePictureType.KITCHEN.ToString());
                if (BedRoomPicList != null && BedRoomPicList.Count >= 2 && string.IsNullOrEmpty(HousePicUrl) && searchType == 0)
                {
                    HousePicUrl = string.IsNullOrEmpty(BedRoomPicList[1].Path) ? "" : BedRoomPicList[1].Path;
                   
                }
                else if (LivingPictureList != null && LivingPictureList.Count >= 2 && string.IsNullOrEmpty(HousePicUrl) )
                {
                    HousePicUrl = string.IsNullOrEmpty(LivingPictureList[1].Path) ? "" : LivingPictureList[1].Path;
                    PicType = "2";
                }

                else if (VillagePicList != null && VillagePicList.Count >= 1 && string.IsNullOrEmpty(HousePicUrl) && searchType == 0)
                {
                    HousePicUrl = string.IsNullOrEmpty(VillagePicList[0].Path) ? "" : VillagePicList[0].Path;
                    PicType = "3";
                }

                else if (RoomPicList != null && RoomPicList.Count >= 1 && string.IsNullOrEmpty(HousePicUrl)  && searchType == 0)
                {
                    HousePicUrl = string.IsNullOrEmpty(RoomPicList[0].Path) ? "" : RoomPicList[0].Path;
                    PicType = "4";
                }

               
                if (BedRoomPicList != null && LivingPictureList != null && RoomPicList != null && KITCHENPicList != null && searchType == 0)
                {
                    if (BedRoomPicList.Count >= 2 && LivingPictureList.Count >= 2 && RoomPicList.Count >= 1 && KITCHENPicList.Count >= 1)
                    {
                        IsMorePic = "1";
                    }
                }
                if (LivingPictureList != null && RoomPicList != null &&VillagePicList!=null&& searchType == 1)
                {
                    if (LivingPictureList.Count >= 4 && RoomPicList.Count >= 1&&VillagePicList.Count>=1)
                    {
                        IsMorePic = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("img:", ex.Message, LogPath);
            }
            document.Add(new Field("IsMorePic", IsMorePic, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("PictureUrl", PictureUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           
            document.Add(new Field("LivingPictureUrl", LivingPictureUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
          
            
            document.Add(new Field("HousePicUrl", HousePicUrl, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("PicType", PicType, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
          
            DateTime dateTime = DateTime.Now;


            document.Add(new NumericField("UpdateTime", Field.Store.YES, true).SetLongValue(TXCommons.Util.UNIX_TIMESTAMP(Convert.ToDateTime(dr["updatetime"]))));
            document.Add(new NumericField("MaxPrice", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["MaxPrice"])));
            document.Add(new NumericField("MinPrice", Field.Store.YES, true).SetDoubleValue(Convert.ToDouble(dr["MinPrice"])));
           
         
            document.Add(new Field("PayType", Convert.ToString(dr["PayType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsGuarantee", Convert.ToString(Convert.ToInt16(dr["IsGuarantee"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsSVip", Convert.ToString(Convert.ToInt16(dr["IsSVip"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("Orientation", Convert.ToString(dr["Orientation"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("BidUserNum", Convert.ToString(dr["BidUserNum"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("UName", Convert.ToString(dr["UName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
           

            document.Add(new NumericField("BuildingArea", Field.Store.YES, true).SetIntValue(Convert.ToInt32(dr["BuildingArea"])));

            document.Add(new Field("IsTxfree", Convert.ToString(Convert.ToInt16(dr["IsTxfree"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsFullYears", Convert.ToString(Convert.ToInt16(dr["IsFullYears"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsUnique", Convert.ToString(Convert.ToInt16(dr["IsUnique"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("BuildingStructure", Convert.ToString(Convert.ToInt16(dr["BuildingStructure"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("PTypeState", Convert.ToString(Convert.ToInt16(dr["PTypeState"])), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("CompanyName", Convert.ToString(dr["CompanyName"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("AllFloar", Convert.ToString(dr["AllFloar"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("TheFloar", Convert.ToString(dr["TheFloar"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("CreateTime", Convert.ToString(dr["CreateTime"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("IsBargaining", Convert.ToString(dr["IsBargaining"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field("HouseType", Convert.ToString(dr["HouseType"]), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new NumericField("IsRecommend", Field.Store.YES, true).SetIntValue(Convert.ToInt16(dr["IsRecommend"])));
            
            return document;
        }

   

    
        /// <summary>
        /// 批量跟新房源索引
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="indexpath"></param>
        /// <returns></returns>
        public bool UpdateHouseDoc(string houseid, IndexWriter indexWriter, string indexpath,bool clearcdn)
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
                    if (clearcdn)
                        ClearCDN(Convert.ToString(ds.Tables[0].Rows[0]["Id"]), Convert.ToString(ds.Tables[0].Rows[0]["CityId"]), Convert.ToString(ds.Tables[0].Rows[0]["SearchType"]), Convert.ToString(ds.Tables[0].Rows[0]["UserType"]));
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



   

        #region 地铁小区关系生成
    

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



    


       

        #region Lucene相关





     /// <summary>
     /// 
     /// </summary>
     /// <param name="houseid"></param>
     /// <param name="cityid"></param>
        /// <param name="type">房源类型0 长租，1二手</param>
        /// <param name="usertype">0个人，1经纪人</param>
        public void ClearCDN(string houseid,string cityid,string type,string usertype)
        {
           
           
            try
            {
                string housetype = "0";
                if (type == "0" && usertype == "0")
                    housetype = "0";
                if (type == "0" && usertype == "1")
                    housetype = "1";
                if (type == "1")
                    housetype = "2";
                var url = TXCommons.UrlRewrite.GetDetailUrl(int.Parse(cityid), int.Parse(houseid), housetype, null, null, Domain,CDNUrl);
                TXCommons.GenerateHtml.UpdateCDNCache(CDNUrl, url);
                TXCommons.Refurbish.LogTool.Log("CDN:", cityid + "-" + url + "-" + "完成", LogPath);
            }
            catch (Exception ex)
            {
                TXCommons.Refurbish.LogTool.Log("CDN:", cityid + "-" + ex.Message, LogPath);
            }
        }
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
