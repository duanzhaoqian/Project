using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TXBll.Dev;
using TXModel.Dev;
using TXModel.Web;
using System.Text;
using TXCommons;
using System.Text.RegularExpressions;
using TXCommons.Index;
using TXCommons.PictureModular;
using TXNewHouseWeb.Common;
using TXCommons.user.cjkjb.webservice;
using TXCommons.Xml;
using ServiceStack;
using System.Reflection;
using System.Data;
using TXCommons.MsgQueue;
using System.Collections;
using Webdiyer.WebControls.Mvc;
using TXBll.NHouseSearch;
using TXOrm;
namespace TXNewHouseWeb.Controllers
{
    public class PremisesController : BaseController
    {
        /// <summary>
        /// 楼盘BLL类-前台
        /// </summary>
        readonly TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

        /// <summary>
        /// 活动BLL类
        /// </summary>
        readonly TXBll.Activitys.ActivitysBll _activitysbll = new TXBll.Activitys.ActivitysBll();

        /// <summary>
        /// 竞价活动管理BLL类
        /// </summary>
        readonly TXBll.NHouseActivies.Biding.BidingBll _bidingbll = new TXBll.NHouseActivies.Biding.BidingBll();

        #region Index
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 新房前台楼盘主页

        /// <summary>
        /// 新房前台楼盘主页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PremisesIndex(string id)
        {
            //id = "1";
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);

                if (model != null && model.Id > 0)
                {
                    #region SEO处理

                    SEO seo = new SEO();
                    ViewData["Seo"] = seo.SeoPremisesIndex(new SEOModel(), ViewData["cityName"] as string, model.Name);

                    #endregion

                    //=================获取广告图片============================
                    //http://nhindex.kuaiyoujia.com/premises/index.ashx?condition=beijing-xinfang-quyu-a297
                    string cityId = ViewData["cityId"] as string; //取城市
                    if (string.IsNullOrWhiteSpace(cityId))
                    {
                        cityId = "253";
                    }
                    PagedList<IndexPremises> searchList = null;
                    string city = ViewData["cityPinyin"] as string;
                    // string Id = string.Format("http://nhindex.kuaiyoujia.com/premises/index.ashx?condition={0}-xinfang-quyu-a{1}", city, model.Id);
                    //beijing-xinfang-quyu
                    string Id = string.Format("{0}-xinfang-quyu-a{1}", city, model.Id);
                    //Id:条件，地址，城市
                    searchList = SearchBll.NHouseListResult(Id.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                    ViewData["ADPictureUrl"] = "";
                    ViewData["DeveloperLOGOUrl"] = "";
                    ViewData["IsRecommend"] = "0";
                    if (null != searchList && searchList.Count > 0)
                    {
                        if (null != searchList[0])
                        {
                            IndexPremises indexpremisestemp = searchList[0];
                            ViewData["ADPictureUrl"] = indexpremisestemp.ADPictureUrl;
                            //开发商LOGO图
                            ViewData["DeveloperLOGOUrl"] = indexpremisestemp.DeveloperLOGOUrl;
                            ViewData["IsRecommend"] = indexpremisestemp.IsRecommend;
                        }
                    }
                    //=================获取广告图片============================


                    ////获取楼盘的区域参考均价
                    //decimal avgreferenceprice = _premisesbll.GetAvgReferencePrice(model);
                    //ViewData["avgreferenceprice"] = avgreferenceprice;

                    #region 项目成交信息

                    //显示当前楼盘今日的认购套数、成交套数。如无则不显示

                    #endregion

                    try
                    {
                        #region 楼盘户型图

                        //===================

                        #region 取Lucene数据

                        //Lucene地址
                        string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(ViewData["CityId"]));
                        List<IndexRoom> sizeChartList = new List<IndexRoom>();
                        var index = new IndexRoomConditionInfo();
                        index.CityId = Util.ToString(ViewData["CityId"]);
                        index.PremisesID = premisesId.ToString();
                        index.PageSize = "9999";
                        var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
                        string premises_xml_url = url + "?condition=" + condtion; //查询生成xml文件
                        DataSet ds = new DataSet("Results");
                        ds.ReadXml(premises_xml_url);
                        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                        {
                            DataRow PageRow = ds.Tables[0].Rows[0];
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                List<IndexRoom> listform = new List<IndexRoom>();
                                IndexRoom indexRoom = new IndexRoom();
                                //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataRow dr = dt.Rows[i];
                                    indexRoom = new IndexRoom();
                                    for (int k = 0; k < dt.Columns.Count; k++)
                                    {
                                        //获取属性值需要指定属性名
                                        PropertyInfo ProInfo = indexRoom.GetType().GetProperty(dt.Columns[k].ColumnName);
                                        ProInfo.SetValue(indexRoom, Convert.ToString(dr[k]), null);
                                    }
                                    if (indexRoom.PicUrl != "")
                                        listform.Add(indexRoom);
                                }
                                sizeChartList = listform;

                            }
                        }
                        else
                        {
                            sizeChartList = null;
                        }
                        ViewData["sizeChartListViewData"] = sizeChartList;

                        if (null != sizeChartList)
                        {
                            ViewData["ViewDatalistROOMTYPECount"] = sizeChartList.Count;
                            var lsHouseRoomInfoList = sizeChartList.OrderBy(s => Util.ToInt(s.Room)).Select(s => Util.ToInt(s.Room)).Distinct().ToArray();
                            ViewData["RoomType"] = lsHouseRoomInfoList;
                        }
                        else
                        {
                            ViewData["ViewDatalistROOMTYPECount"] = 0;
                            ViewData["RoomType"] = new int[] { };
                        }

                        #endregion

                        //===================

                        #endregion
                    }
                    catch (Exception ex)
                    {

                    }


                    ViewData["UserId"] = GetUserInfo == null ? 0 : GetUserInfo.Id;

                    #region 浏览量、排名

                    //浏览量(浏览量+1，并读取出来)

                    //浏览量
                    ViewData["ViewCount"] = TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseViewCount, model.CityId, string.Format("NewHouseView_{0}", premisesId)); //TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", premisesId), FunctionTypeEnum.NewHouseViewCount, model.CityId);

                    //排名
                    var Rank = TXCommons.Redis.GetValue<int>("NewHouseRank_" + model.Id, FunctionTypeEnum.NewHousePropertyRank, model.CityId); //TXCommons.Redis.GetValue<int>(string.Format("NewHouseRank_{0}", model.Id), FunctionTypeEnum.NewHousePropertyRank, model.CityId);
                    ViewData["Rank"] = Rank;

                    #endregion


                    #region 统计日志

                    // add: liyuzhao
                    CT_DeveAndIdenInfo dev = null;
                    if (0 < model.DeveloperId)
                    {
                        dev = new DevelopersBll().GetDeveAndIdenInfo(model.DeveloperId);
                    }
                    Log4netService.RecordLog.Logging(model.Id.ToString(), dev == null ? "" : dev.Type.ToString(), model.DId.ToString(), model.BName, model.Name, model.DeveloperId.ToString(), "", DateTime.Now.ToString());

                    #endregion

                    if (null != model.Name)
                    {
                        model.Name = Util.getStrLenB(model.Name, 0, 24);
                    }
                    return View(model);
                }
                else
                {
                    return Content(NoHouseUrl());
                }
            }
            else
            {
                return Content(NoHouseUrl());
            }
        }

        #region 同区热门楼盘列表

        /// <summary>
        /// 同区热门楼盘列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="d"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ActionResult HotPremisesList(string id)
        {
            // 描述：
            //根据当前楼盘所在区域推荐同区域浏览数最多的楼盘，从高到低排序
            List<IndexRecList> list = new List<IndexRecList>();
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId); //_premisesbll.Get_Premises_HotPremisesList(premisesId); // 
                if (model != null && model.Id > 0)
                {
                    SearchBll _searchBll = new SearchBll();
                    list = _searchBll.PremisesList(0, FunctionTypeEnum.NewHousePropertyRank, CityId, model.DId.ToString());
                    //IndexRankingConditionInfo conditionInfo = new IndexRankingConditionInfo();
                    //conditionInfo.ProvinceID = model.ProvinceId.ToString();
                    //conditionInfo.CityID = model.CityId.ToString();
                    //conditionInfo.DistrictID = model.DId.ToString();
                    ////conditionInfo.BusinessID = model.BId.ToString();
                    //conditionInfo.PremisesID = model.Id.ToString();
                    ///// 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
                    //conditionInfo.Ranking = "1";
                    //lsHotPremisesList = _premisesbll.GetHotPremisesList(conditionInfo);

                    ////获取点击次数
                    //if (lsHotPremisesList != null)
                    //{
                    //    lsHotPremisesList.ForEach(p =>
                    //        {
                    //            p.ClickRate = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", p.PremisesID), FunctionTypeEnum.NewHouseViewCount, Util.ToInt(p.CityID)).ToString();
                    //            p.PremisesName = Util.getStrLenB(p.PremisesName, 0, 24);
                    //        });
                    //}
                }
            }
            return PartialView("PremisesIndex/_HotPremisesListPI", list);
        }
        #endregion

        #region 同区最新楼盘列表
        /// <summary>
        /// 同区最新楼盘列表
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="d"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public ActionResult NewPremisesList(string id)
        {
            // 描述：
            //根据当前楼盘所在区域推荐同区域最新发布的楼盘，按发布时间倒序排序。
            List<IndexRecList> list = new List<IndexRecList>();
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    SearchBll _searchBll = new SearchBll();
                    list = _searchBll.PremisesList(1, FunctionTypeEnum.NewHousePropertyRank, CityId, model.DId.ToString());
                    //IndexRankingConditionInfo conditionInfo = new IndexRankingConditionInfo();
                    //conditionInfo.ProvinceID = model.ProvinceId.ToString();
                    //conditionInfo.CityID = model.CityId.ToString();
                    //conditionInfo.DistrictID = model.DId.ToString();
                    ////conditionInfo.BusinessID = model.BId.ToString();
                    //conditionInfo.PremisesID = model.Id.ToString();
                    ///// 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
                    //conditionInfo.Ranking = "2";
                    //lsHotPremisesList = _premisesbll.GetHotPremisesList(conditionInfo);

                    ////获取点击次数
                    //if (lsHotPremisesList != null)
                    //{
                    //    lsHotPremisesList.ForEach(p =>
                    //    {
                    //        p.ClickRate = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", p.PremisesID), FunctionTypeEnum.NewHouseViewCount, Util.ToInt(p.CityID)).ToString();
                    //        p.PremisesName = Util.getStrLenB(p.PremisesName, 0, 24);
                    //    });
                    //}
                }
            }
            return PartialView("PremisesIndex/_NewPremisesListPI", list);
        }

        #endregion

        /// <summary>
        /// 异步调取户型列表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult HXPremisesList(string premisesid, string id, string pnum)
        {
            ViewData["pnum"] = pnum;
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            int ipnum;
            if (int.TryParse(id, out ipnum))
            {
                int premisesId;
                if (int.TryParse(premisesid, out premisesId))
                {
                    //=============
                    //获取楼栋基本信息
                    TXOrm.Building buildingtemp = _premisesbll.GetBuildingInfobyPNum(premisesId, ipnum);
                    if (null != buildingtemp && buildingtemp.Id > 0)
                    {
                        List<TXOrm.Unit> UnitList = _premisesbll.GetUnitListbyBuildingId(buildingtemp.Id);
                        if (null != UnitList && UnitList.Count > 0)
                        {
                            ViewData["UnitListCount"] = UnitList.Count;
                        }
                        else
                        {
                            ViewData["UnitListCount"] = 0;
                        }
                    }
                    ViewData["buildingtemp"] = buildingtemp;

                    //============
                    TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                    if (model != null && model.Id > 0)
                    {
                        ViewData["PremisesName"] = model.Name;
                        ViewData["PremisesId"] = model.Id;
                        #region 户型列表（待完善）
                        //当用户没有点击某楼栋则显示当前楼盘所有户型，点击某楼栋则显示某楼栋的基本信息及户型
                        int intbuildingId = 0;
                        if (buildingtemp != null && buildingtemp.Id > 0)
                        {
                            intbuildingId = buildingtemp.Id;
                        }

                        //===================
                        #region 取Lucene数据
                        //Lucene地址
                        string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(model.CityId));
                        List<IndexRoom> sizeChartList = new List<IndexRoom>();
                        var index = new IndexRoomConditionInfo();
                        index.CityId = Util.ToString(model.CityId);
                        index.PremisesID = premisesId.ToString();
                        index.BuildingId = intbuildingId.ToString();
                        index.PageSize = "100";
                        var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
                        string premises_xml_url = url + "?condition=" + condtion;//查询生成xml文件
                        DataSet ds = new DataSet("Results");
                        ds.ReadXml(premises_xml_url);
                        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                        {
                            DataRow PageRow = ds.Tables[0].Rows[0];
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                List<IndexRoom> listform = new List<IndexRoom>();
                                IndexRoom indexRoom = new IndexRoom();
                                //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataRow dr = dt.Rows[i];
                                    indexRoom = new IndexRoom();
                                    for (int k = 0; k < dt.Columns.Count; k++)
                                    {
                                        //获取属性值需要指定属性名
                                        PropertyInfo ProInfo = indexRoom.GetType().GetProperty(dt.Columns[k].ColumnName);
                                        ProInfo.SetValue(indexRoom, Convert.ToString(dr[k]), null);
                                    }
                                    if (indexRoom.PicUrl != "")
                                        listform.Add(indexRoom);
                                }
                                sizeChartList = listform;
                            }
                        }
                        else
                        {
                            sizeChartList = null;
                        }
                        ViewData["sizeChartListViewData"] = sizeChartList;

                        if (sizeChartList != null)
                        {
                            List<TXModel.Web.PremisesHouseRoom> lsPremisesHouseRoomList = new List<PremisesHouseRoom>();

                            List<int> lsHouseRoomInfoList = sizeChartList.OrderBy(s => Util.ToInt(s.Room)).Select(s => Util.ToInt(s.Room)).Distinct().ToList();
                            if (null != lsHouseRoomInfoList && lsHouseRoomInfoList.Count > 0)
                            {
                                int iforeachtime = 0;
                                foreach (int ihouseroominfo in lsHouseRoomInfoList)
                                {
                                    if (iforeachtime < 4)
                                    {
                                        TXModel.Web.PremisesHouseRoom premiseshouseroom = new PremisesHouseRoom();

                                        int iHouseRoomCount = sizeChartList.Where(s => Util.ToInt(s.Room) == ihouseroominfo).Count();
                                        premiseshouseroom.Room = ihouseroominfo;
                                        premiseshouseroom.RoomCount = iHouseRoomCount;
                                        lsPremisesHouseRoomList.Add(premiseshouseroom);
                                        iforeachtime++;
                                    }
                                }
                            }
                            ViewData["lsPremisesHouseRoomList"] = lsPremisesHouseRoomList;
                        }

                        #endregion
                        //===================

                        #endregion
                    }
                }
            }

            return PartialView("PremisesIndex/_HXPremisesListNew", lsHotPremisesList);
        }
        /// <summary>
        /// 异步调取楼盘相册信息
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult AlbumPremises(string premisesid)
        {
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    if (null != model && null != model.Name)
                    {
                        if (model.Name.Length > 16)
                        {
                            model.Name = model.Name.Substring(0, 16);
                        }
                        else
                        {
                            model.Name = model.Name.Substring(0, model.Name.Length);
                        }
                    }
                    ViewData["PremisesmodelName"] = model.Name;
                    ViewData["PremisesmodelId"] = model.Id;
                    #region  	楼盘相册
                    //=====================
                    //                    获取一下，调用这个方法
                    //TXCommons.PictureModular.GetPicture.GetPremisesPictureOrder
                    List<TXCommons.PictureModular.PremisesPictureInfo> tempPictureOrder = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    List<TXCommons.PictureModular.PremisesPictureInfo> listPremisesPictureOrder = TXCommons.PictureModular.GetPicture.GetPremisesPictureOrder(model.InnerCode, true, model.CityId);
                    if (null != listPremisesPictureOrder && listPremisesPictureOrder.Count > 0)
                    {
                        foreach (TXCommons.PictureModular.PremisesPictureInfo temp in listPremisesPictureOrder)
                        {
                            if (null != temp.Title)
                            {
                                int intLength = "富力飞常HAOSC户型图3".Length;
                                string strTitle = temp.Title.ToString();
                                if (strTitle.Length > intLength)
                                {
                                    temp.Title = strTitle.Substring(0, intLength);
                                }
                            }
                            tempPictureOrder.Add(temp);
                        }
                    }
                    //=============
                    //xgt效果图|hxt户型图|ght规划图|wzt位置图|ldt楼栋平面图|sjt实景图|jtt交通图
                    if (null != listPremisesPictureOrder && listPremisesPictureOrder.Count > 0)
                    {
                        foreach (TXCommons.PictureModular.PremisesPictureInfo tPicture in listPremisesPictureOrder)
                        {
                            switch (tPicture.PictureType)
                            {
                                case "EFFECT":
                                    tPicture.PictureType = "xgt";
                                    break;
                                case "ROOMTYPE":
                                    tPicture.PictureType = "hxt";
                                    break;
                                case "PLAN":
                                    tPicture.PictureType = "ght";
                                    break;
                                case "LOCATION":
                                    tPicture.PictureType = "wzt";
                                    break;
                                case "PLANE":
                                    tPicture.PictureType = "ldt";
                                    break;
                                case "SCENE":
                                    tPicture.PictureType = "sjt";
                                    break;
                                case "TRAFFIC":
                                    tPicture.PictureType = "jtt";
                                    break;
                            }
                        }
                    }
                    //============
                    ViewData["listPremisesPictureOrder"] = tempPictureOrder;
                    //=================================================================
                    //楼盘相册中的图片
                    //处理：
                    //按照图片的发布时间倒序排序。
                    //输出：
                    //楼盘相册中的图片。
                    //无数据提示：暂无数据
                    //楼盘沙盘图
                    int iPremisesLISTcount = 0;
                    //户型图
                    int iROOMTYPEcount = 0;
                    ///楼栋规划图
                    int iPlancount = 0;
                    //楼栋平面图
                    int iPlanecount = 0;
                    //交通图
                    int iTRAFFICcount = 0;
                    //位置图
                    int iLocationcount = 0;
                    //实景图
                    int iScenecount = 0;
                    //效果图
                    int iEffectcount = 0;
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseEffect = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarouseEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), model.CityId);

                    List<TXCommons.PictureModular.PremisesPictureInfo> listPremisesLIST = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listPremisesLIST = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.PremisesLIST.ToString(), 0);

                    List<TXCommons.PictureModular.PremisesPictureInfo> listROOMTYPE = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listROOMTYPE = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString(), model.CityId);
                    //楼栋平面图
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarousePlane = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarousePlane = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Plane.ToString(), model.CityId);

                    //实景图
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseScene = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarouseScene = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Scene.ToString(), model.CityId);

                    //楼盘沙盘图
                    if (null != listPremisesLIST && listPremisesLIST.Count > 0)
                    {
                        iPremisesLISTcount = listPremisesLIST.Count;
                    }
                    ////户型图
                    if (null != listROOMTYPE && listROOMTYPE.Count > 0)
                    {
                        iROOMTYPEcount = listROOMTYPE.Count;
                    }
                    ////楼栋规划图
                    List<PremisesPictureInfo> lsPlan = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Plan.ToString(), model.CityId);
                    if (null != lsPlan && lsPlan.Count > 0)
                    {
                        iPlancount = lsPlan.Count;
                    }
                    ////楼栋平面图
                    if (null != listCarousePlane && listCarousePlane.Count > 0)
                    {
                        iPlanecount = listCarousePlane.Count;
                    }
                    ////交通图
                    List<PremisesPictureInfo> lsTRAFFIC = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.TRAFFIC.ToString(), model.CityId);
                    if (null != lsTRAFFIC && lsTRAFFIC.Count > 0)
                    {
                        iTRAFFICcount = lsTRAFFIC.Count;
                    }
                    ////位置图
                    List<PremisesPictureInfo> lsLocation = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Location.ToString(), model.CityId);
                    if (null != lsLocation && lsLocation.Count > 0)
                    {
                        iLocationcount = lsLocation.Count;
                    }
                    ////实景图
                    if (null != listCarouseScene && listCarouseScene.Count > 0)
                    {
                        iScenecount = listCarouseScene.Count;
                    }
                    ////效果图
                    //List<PremisesPictureInfo> lsEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), model.CityId);
                    if (null != listCarouseEffect && listCarouseEffect.Count > 0)
                    {
                        iEffectcount = listCarouseEffect.Count;
                    }
                    ViewData["iPremisesLISTcount"] = iPremisesLISTcount;
                    ViewData["iROOMTYPEcount"] = iROOMTYPEcount;
                    ViewData["iPlancount"] = iPlancount;
                    ViewData["iPlanecount"] = iPlanecount;
                    ViewData["iTRAFFICcount"] = iTRAFFICcount;
                    ViewData["iLocationcount"] = iLocationcount;
                    ViewData["iScenecount"] = iScenecount;
                    ViewData["iEffectcount"] = iEffectcount;
                    ViewData["iPremisesPicturecount"] = iPremisesLISTcount + iROOMTYPEcount + iPlancount + iPlanecount + iTRAFFICcount + iLocationcount + iScenecount + iEffectcount;
                    #endregion
                }
            }

            return PartialView("PremisesIndex/_AlbumPremises", lsHotPremisesList);
        }
        /// <summary>
        /// 异步调取楼盘联播图相关信息
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult CarouselPremises(string premisesid)
        {
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region 楼盘轮播图片
                    //调取楼盘的效果图、交通图、户型图、视频、样板间图的第一张，默认第一张显示楼盘效果图。如无数据则提示：“暂无数据”。
                    #region 测试数据
                    List<TXModel.Web.PremisesCarousel> lsCarouselList = new List<PremisesCarousel>();
                    //TXModel.Web.PremisesCarousel carousel = new PremisesCarousel();
                    //carousel.PremisesId = model.Id;
                    //carousel.Id = 1;
                    //carousel.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w430_h296.jpg";
                    //carousel.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60.jpg";
                    //carousel.ImgDesc = "效果图";
                    //lsCarouselList.Add(carousel);

                    //TXModel.Web.PremisesCarousel carousel1 = new PremisesCarousel();
                    //carousel1.PremisesId = model.Id;
                    //carousel1.Id = 2;
                    //carousel1.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w430_h296_1.jpg";
                    //carousel1.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_1.jpg";
                    //carousel1.ImgDesc = "实景图";
                    //lsCarouselList.Add(carousel1);

                    //TXModel.Web.PremisesCarousel carousel2 = new PremisesCarousel();
                    //carousel2.PremisesId = model.Id;
                    //carousel2.Id = 3;
                    //carousel2.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w430_h296_2.jpg";
                    //carousel2.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_2.jpg";
                    //carousel2.ImgDesc = "规划图";
                    //lsCarouselList.Add(carousel2);

                    //TXModel.Web.PremisesCarousel carousel3 = new PremisesCarousel();
                    //carousel3.PremisesId = model.Id;
                    //carousel3.Id = 4;
                    //carousel3.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w430_h296_3.jpg";
                    //carousel3.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_3.jpg";
                    //carousel3.ImgDesc = "样板图";
                    //lsCarouselList.Add(carousel3);

                    //TXModel.Web.PremisesCarousel carousel4 = new PremisesCarousel();
                    //carousel4.PremisesId = model.Id;
                    //carousel4.Id = 5;
                    //carousel4.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w430_h296_4.jpg";
                    //carousel4.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_4.jpg";
                    //carousel4.ImgDesc = "画报";
                    //lsCarouselList.Add(carousel4);
                    #endregion

                    //测试 调取楼盘的效果图、交通图、户型图、规划图、实景图的第一张
                    //调取楼盘的     效果图、交通图、户型图、视频、样板间图的第一张，默认第一张显示楼盘效果图。如无数据则提示：“暂无数据”。
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseEffect = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarouseEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), model.CityId);
                    if (null != listCarouseEffect && listCarouseEffect.Count > 0)
                    {
                        PremisesPictureInfo tlistCarouseEffect = listCarouseEffect.OrderBy(r => r.CreateTime).First();
                        TXModel.Web.PremisesCarousel carousel = new PremisesCarousel();
                        carousel.PremisesId = model.Id;
                        carousel.Id = 1;
                        if (null != tlistCarouseEffect.Path)
                        {
                            //carousel.ImgSrc = tlistCarouseEffect.Path.ToString();
                            carousel.ImgSrc = string.Format("{0}.500_350.jpg", tlistCarouseEffect.Path.ToString());
                            carousel.SmallImgSrc = string.Format("{0}.85_65.jpg", tlistCarouseEffect.Path.ToString());
                        }
                        else
                        {
                            carousel.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w500_h350.jpg";
                        }
                        carousel.ImgDesc = "效果图";
                        lsCarouselList.Add(carousel);

                    }

                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseTRAFFIC = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarouseTRAFFIC = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.TRAFFIC.ToString(), model.CityId);
                    if (null != listCarouseTRAFFIC && listCarouseTRAFFIC.Count > 0)
                    {
                        PremisesPictureInfo tlistCarouseTRAFFIC = listCarouseTRAFFIC.OrderBy(r => r.CreateTime).First();
                        TXModel.Web.PremisesCarousel carousel1 = new PremisesCarousel();
                        carousel1.PremisesId = model.Id;
                        carousel1.Id = 2;
                        if (null != tlistCarouseTRAFFIC.Path)
                        {
                            // carousel1.ImgSrc = tlistCarouseTRAFFIC.Path.ToString();
                            carousel1.ImgSrc = string.Format("{0}.500_350.jpg", tlistCarouseTRAFFIC.Path.ToString());
                            carousel1.SmallImgSrc = string.Format("{0}.85_65.jpg", tlistCarouseTRAFFIC.Path.ToString());
                        }
                        else
                        {
                            //img_w500_h350.jpg
                            carousel1.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w500_h350.jpg";
                            //img_w80_h60.jpg
                            carousel1.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_1.jpg";
                        }
                        carousel1.ImgDesc = "交通图";
                        lsCarouselList.Add(carousel1);
                    }
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseROOMTYPE = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarouseROOMTYPE = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString(), model.CityId);
                    if (null != listCarouseROOMTYPE && listCarouseROOMTYPE.Count > 0)
                    {
                        PremisesPictureInfo tlistCarouseROOMTYPE = listCarouseROOMTYPE.OrderBy(r => r.CreateTime).First();
                        TXModel.Web.PremisesCarousel carousel2 = new PremisesCarousel();
                        carousel2.PremisesId = model.Id;
                        carousel2.Id = 3;
                        if (null != tlistCarouseROOMTYPE.Path)
                        {
                            // carousel2.ImgSrc = tlistCarouseROOMTYPE.Path.ToString();
                            carousel2.ImgSrc = string.Format("{0}.500_350.jpg", tlistCarouseROOMTYPE.Path.ToString());
                            carousel2.SmallImgSrc = string.Format("{0}.85_65.jpg", tlistCarouseROOMTYPE.Path.ToString());
                        }
                        else
                        {
                            carousel2.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w500_h350.jpg";
                            carousel2.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_2.jpg";
                        }
                        carousel2.ImgDesc = "户型图";
                        lsCarouselList.Add(carousel2);
                    }
                    //实景图
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseScene = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarouseScene = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Scene.ToString(), model.CityId);
                    if (null != listCarouseScene && listCarouseScene.Count > 0)
                    {
                        PremisesPictureInfo tlistCarouseScene = listCarouseScene.OrderBy(r => r.CreateTime).First();
                        TXModel.Web.PremisesCarousel carousel3 = new PremisesCarousel();
                        carousel3.PremisesId = model.Id;
                        carousel3.Id = 4;
                        if (null != tlistCarouseScene.Path)
                        {
                            //carousel3.ImgSrc = string.Format("{0}", tlistCarouseScene.Path.ToString());
                            carousel3.ImgSrc = string.Format("{0}.500_350.jpg", tlistCarouseScene.Path.ToString());
                            carousel3.SmallImgSrc = string.Format("{0}.85_65.jpg", tlistCarouseScene.Path.ToString());
                        }
                        else
                        {
                            carousel3.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w500_h350.jpg";
                            carousel3.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_3.jpg";
                        }
                        carousel3.ImgDesc = "实景图";
                        lsCarouselList.Add(carousel3);
                    }
                    //规划图
                    List<TXCommons.PictureModular.PremisesPictureInfo> listCarousePlane = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listCarousePlane = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Plan.ToString(), model.CityId);
                    if (null != listCarousePlane && listCarousePlane.Count > 0)
                    {
                        PremisesPictureInfo tlistCarousePlane = listCarousePlane.OrderBy(r => r.CreateTime).First();
                        TXModel.Web.PremisesCarousel carousel4 = new PremisesCarousel();
                        carousel4.PremisesId = model.Id;
                        carousel4.Id = 5;
                        if (null != tlistCarousePlane.Path)
                        {
                            //carousel4.ImgSrc = tlistCarousePlane.Path.ToString();
                            carousel4.ImgSrc = string.Format("{0}.500_350.jpg", tlistCarousePlane.Path.ToString());
                            carousel4.SmallImgSrc = string.Format("{0}.85_65.jpg", tlistCarousePlane.Path.ToString());

                        }
                        else
                        {
                            carousel4.ImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w500_h350.jpg";
                            carousel4.SmallImgSrc = TXCommons.GetConfig.GetQTBaseUrl + "images/img_w80_h60_4.jpg";
                        }
                        carousel4.ImgDesc = "规划图";
                        lsCarouselList.Add(carousel4);
                    }
                    if (null != lsCarouselList && lsCarouselList.Count > 0)
                    {
                        ViewData["lsCarouselListImgSrc"] = lsCarouselList[0].ImgSrc;
                    }
                    ViewData["lsCarouselList"] = lsCarouselList;
                    #endregion
                }
            }

            return PartialView("PremisesIndex/_CarouselPremises", lsHotPremisesList);

        }
        /// <summary>
        /// 异步调取楼盘参加活动的房源
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult ActivitiesHousePremises(string premisesid)
        {
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    ViewData["PremismodelName"] = model.Name;
                    ViewData["PremismodelId"] = model.Id;
                    #region  通过楼盘ID获取参加营销活动的房源列表
                    //新房前台楼盘主页
                    //参加营销活动的房源列表
                    //展示的楼盘图片取：楼盘效果图(如果多张楼盘效果图,取最新的那张)

                    //  参加营销活动的房源
                    //?	处理：
                    //按参加的时间倒序排序。
                    //?	输出：
                    //?	房源
                    //?	无数据则提示：“暂无数据”/

                    //户型图
                    List<PremisesPictureInfo> lsPictureList = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.List.ToString(), model.CityId);
                    ViewData["lsPictureListImgUrl"] = TXCommons.GetConfig.ImgUrl + "images/img_w71_h53.jpg";

                    if (null != lsPictureList && lsPictureList.Count > 0)
                    {
                        if (null != lsPictureList[0].Path)
                        {
                            // string.Format("{0}.180_130.jpg", listCarouseEffect[0].Path.ToString());
                            //ViewData["lsPictureListImgUrl"] = lsPictureList[0].Path.ToString();
                            ViewData["lsPictureListImgUrl"] = string.Format("{0}.71_53.jpg", lsPictureList[0].Path.ToString());
                        }
                    }

                    //读取缓存中的营销活动，没有的话走数据库读取，读取后添加到缓存
                    List<PremisesActivitiesHouse> lsPremises;
                    lsPremises = TXCommons.Redis.GetValue<List<PremisesActivitiesHouse>>(string.Format("ActivitiesHoust_{0}", model.Id), FunctionTypeEnum.NewHouseRelatedRecommendations, model.CityId);

                    if (lsPremises == null)
                    {
                        //通过楼盘ID获取参加营销活动的房源列表
                        lsPremises = _premisesbll.GetActivitiesHouseByIdnew(model.Id);
                        foreach (var item in lsPremises)
                        {
                            item.PremisesName = model.Name;
                            //获取活动房间浏览量
                            item.Hits = TXCommons.Redis.GetOneViewCountValue(string.Format("h_{0}", item.HouseId), FunctionTypeEnum.NewHouseViewCount, model.CityId).ToString();
                        }

                        //数据库读取数据后，添加到缓存
                        TXCommons.Redis.SetValue<List<PremisesActivitiesHouse>>(string.Format("ActivitiesHoust_{0}", model.Id), lsPremises, DateTime.Now.AddMinutes(1), FunctionTypeEnum.NewHouseRelatedRecommendations, model.CityId);
                    }


                    ViewData["lsPremises"] = lsPremises;
                    #endregion
                }
            }

            return PartialView("PremisesIndex/_HousePremises", lsHotPremisesList);

        }
        /// <summary>
        /// 异步获取楼盘特色
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public string TSPremises(string premisesid)
        {
            string html = string.Empty;
            html = "<strong class=\"font14 col666\">项目特色：</strong>";
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region 楼盘项目特色
                    //3	楼盘项目特色	
                    //按照后台添加的时间倒序排序。
                    //楼盘项目特色将来可能单独设计一个表
                    if (null != model && null != model.Characteristic)
                    {
                        if (!string.Empty.Equals(model.Characteristic.Trim()))
                        {
                            string strPremisesCharacteristic = string.Empty;
                            //项目特色基础数据写入Redis服务器
                            //List<TXOrm.PremisesFeature> PremisesFeaturels = _premisesbll.GetPremisesFeatureList("9,10,8");
                            //if (null != PremisesFeaturels && PremisesFeaturels.Count > 0)
                            //{
                            //    foreach (TXOrm.PremisesFeature tPremisesFeature in PremisesFeaturels)
                            //    {
                            //        TXCommons.Redis.SetValue<string>(string.Format("Characteristic_{0}", tPremisesFeature.Id), tPremisesFeature.Name, DateTime.Now.AddMinutes(3000), FunctionTypeEnum.NewHouseViewCount, model.CityId);
                            //    }
                            //}
                            //项目特色基础数据写入Redis服务器
                            string[] pCharacteristic = model.Characteristic.Trim().Split(',');
                            int intpCharacteristic = -1;
                            for (int i = pCharacteristic.Length - 1; i >= 0; i--)
                            {
                                string pCharacteristicstr = pCharacteristic[i].ToString();
                                int.TryParse(pCharacteristicstr, out intpCharacteristic);
                                var CharacteristicRedis = TXCommons.Redis.GetValue<string>(string.Format("Characteristic_{0}", intpCharacteristic), FunctionTypeEnum.NewHousePropertyFeatures, model.CityId);
                                if (CharacteristicRedis != null)
                                {
                                    strPremisesCharacteristic += string.Format("<a href=\"#\" class=\"border_4d94f5\">{0}</a>", CharacteristicRedis) + "  ";
                                }
                            }
                            //============
                            html = html + strPremisesCharacteristic;
                        }
                    }
                    #endregion
                }
            }

            return html;
        }
        /// <summary>
        /// 异步调取楼盘沙盘图信息
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult SPPremises(string premisesid)
        {
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            PremisStatisticsInfo psi = null;
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    if (null != model && null != model.Name)
                    {
                        if (model.Name.Length > 16)
                        {
                            model.Name = model.Name.Substring(0, 16);
                        }
                        else
                        {
                            model.Name = model.Name.Substring(0, model.Name.Length);
                        }
                    }
                    psi = _premisesbll.GetPremisStatisticsInfo(model.Id);
                    ViewData["tempPictureName"] = model.Name;
                    ViewData["tempPictureId"] = model.Id;
                    ViewData["tempPictureUserCount"] = model.UserCount;
                    ViewData["tempPicturemodel"] = model;
                    ViewData["SubscribedCount"] = psi == null ? 0 : psi.SubscribedCount;//认购套数
                    ViewData["BargainCount"] = psi == null ? 0 : psi.BargainCount;//成交套数
                    #region 沙盘图
                    //=================
                    List<TXOrm.SandTableLabel> SandTableLabelList = _premisesbll.GetSandTableLabelList(model.Id);
                    if (null != SandTableLabelList && SandTableLabelList.Count > 0)
                    {
                        foreach (TXOrm.SandTableLabel sSandTableLabel in SandTableLabelList)
                        {
                            //                            销售状态
                            //1 待售
                            //2 在售
                            //3 售罄
                            //4 建设中
                            //5 规划中
                            //6 开发商保留
                            //class="lp_bg_blue"
                            // <span class="lp_bg_red" style="position:absolute; left:100px; top:120px;">1号楼</span>
                            //<span class="lp_bg_blue" style="position:absolute; left:150px; top:120px;">1号楼</span>
                            //<span class="lp_bg_green" style="position:absolute; left:200px; top:120px;">1号楼</span>
                            //<span class="lp_bg_grey" style="position:absolute; left:250px; top:120px;">1号楼</span> 
                            string sclass = "class=\"lp_bg_blue\"";
                            if (sSandTableLabel.DeveloperId == 2)
                            {
                                sclass = "class=\"lp_bg_red\"";
                            }
                            if (sSandTableLabel.DeveloperId == 3)
                            {
                                sclass = "class=\"lp_bg_grey\"";
                            }
                            if (sSandTableLabel.DeveloperId == 4)
                            {
                                sclass = "class=\"lp_bg_green\"";
                            }
                            if (sSandTableLabel.DeveloperId == 5)
                            {
                                sclass = "class=\"lp_bg_blue\"";
                            }
                            if (sSandTableLabel.DeveloperId == -100)
                            {
                                sclass = "class=\"lp_bg_blue\"";
                            }
                            sSandTableLabel.SandBox = sclass;
                        }

                    }
                    ViewData["SandTableLabelList"] = SandTableLabelList;
                    //=================
                    //调取后台添加的沙盘图
                    List<TXCommons.PictureModular.PremisesPictureInfo> listPremisesLIST = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                    listPremisesLIST = TXCommons.PictureModular.GetPicture.GetPremisesPictureListNoFilter(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.PremisesLIST.ToString(), 0);
                    ViewData["tempPictureModularPath"] = listPremisesLIST != null && listPremisesLIST.Count > 0 ? listPremisesLIST[0].Path : TXCommons.GetConfig.ImgUrl + "images/w830_h430.jpg";

                    #region before 默认循环获取 最后一个沙盘图地址
                    //if (null != listPremisesLIST && listPremisesLIST.Count > 0)
                    //{
                    //    foreach (TXCommons.PictureModular.PremisesPictureInfo tempPictureModular in listPremisesLIST)
                    //    {
                    //        ViewData["tempPictureModularPath"] = tempPictureModular.Path;
                    //    }
                    //}

                    //if (string.IsNullOrEmpty(Convert.ToString(ViewData["tempPictureModularPath"])))
                    //{
                    //    ViewData["tempPictureModularPath"] = TXCommons.GetConfig.ImgUrl + "images/w830_h430.jpg";
                    //}
                    #endregion

                    #endregion

                    #region 户型列表

                    //===================
                    #region 取Lucene数据
                    //Lucene地址
                    string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(model.CityId));
                    List<IndexRoom> sizeChartList = new List<IndexRoom>();
                    var index = new IndexRoomConditionInfo();
                    index.CityId = Util.ToString(model.CityId);
                    index.PremisesID = premisesId.ToString();
                    index.PageSize = "100";
                    var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
                    string premises_xml_url = url + "?condition=" + condtion;//查询生成xml文件
                    DataSet ds = new DataSet("Results");
                    ds.ReadXml(premises_xml_url);
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        DataRow PageRow = ds.Tables[0].Rows[0];
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            List<IndexRoom> listform = new List<IndexRoom>();
                            IndexRoom indexRoom = new IndexRoom();
                            //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow dr = dt.Rows[i];
                                indexRoom = new IndexRoom();
                                for (int k = 0; k < dt.Columns.Count; k++)
                                {
                                    //获取属性值需要指定属性名
                                    PropertyInfo ProInfo = indexRoom.GetType().GetProperty(dt.Columns[k].ColumnName);
                                    ProInfo.SetValue(indexRoom, Convert.ToString(dr[k]), null);
                                }
                                if (indexRoom.PicUrl != "")
                                    listform.Add(indexRoom);
                            }
                            sizeChartList = listform;
                        }
                    }
                    else
                    {
                        sizeChartList = null;

                    }
                    ViewData["sizeChartListViewData"] = sizeChartList;

                    if (sizeChartList != null)
                    {
                        List<TXModel.Web.PremisesHouseRoom> lsPremisesHouseRoomList = new List<PremisesHouseRoom>();

                        List<int> lsHouseRoomInfoList = sizeChartList.OrderBy(s => Util.ToInt(s.Room)).Select(s => Util.ToInt(s.Room)).Distinct().ToList();
                        if (null != lsHouseRoomInfoList && lsHouseRoomInfoList.Count > 0)
                        {
                            int iforeachtime = 0;
                            foreach (int ihouseroominfo in lsHouseRoomInfoList)
                            {
                                //if (iforeachtime < 100)
                                //{
                                TXModel.Web.PremisesHouseRoom premiseshouseroom = new PremisesHouseRoom();

                                int iHouseRoomCount = sizeChartList.Where(s => Util.ToInt(s.Room) == ihouseroominfo).Count();
                                premiseshouseroom.Room = ihouseroominfo;
                                premiseshouseroom.RoomCount = iHouseRoomCount;
                                lsPremisesHouseRoomList.Add(premiseshouseroom);
                                iforeachtime++;
                                //}
                            }
                        }
                        ViewData["lsPremisesHouseRoomList"] = lsPremisesHouseRoomList;
                    }

                    #endregion
                    //===================


                    #endregion
                }
            }

            return PartialView("PremisesIndex/_SPPremises", lsHotPremisesList);
        }
        /// <summary>
        /// 异步获取楼盘pk数据（可能感兴趣的楼盘）
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult InterestPremises(string premisesid)
        {
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    ViewData["InterestPremisesName"] = model.Name;
                    ViewData["InterestPremisesReferencePrice"] = Convert.ToDouble(model.ReferencePrice);
                    ViewData["PremisesIndexUrl"] = NHWebUrl.PremisesIndexUrl(model.Id.ToString());
                    List<TXCommons.PictureModular.PremisesPictureInfo> listEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), model.CityId);
                    if (null != listEffect && listEffect.Count > 0)
                    {
                        if (null != listEffect[0].Path)
                        {
                            ViewData["ViewDatalistzlhxPath"] = listEffect[0].Path.ToString();
                        }
                    }
                    #region  	 	可能感兴趣的楼盘
                    //根据当前楼盘所在区域推荐一些同均价的楼盘，并且用户可逐个对推荐的楼盘进行对比。
                    //获取可能感兴趣的楼盘列表
                    List<TXOrm.Premis> lsInterestPremises = _premisesbll.GetInterestPremisesList(model);
                    foreach (var item in lsInterestPremises)
                    {
                        //使用PremisesIntroduction字段存储效果图路径，传递到页面。
                        item.PremisesIntroduction = "";

                        listEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(item.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), item.CityId);
                        if (null != listEffect && listEffect.Count > 0)
                        {
                            if (null != listEffect[0].Path)
                            {
                                //item.PremisesIntroduction = listEffect[0].Path.ToString();
                                item.PremisesIntroduction = string.Format("{0}.175_125.jpg", listEffect[0].Path.ToString());
                            }
                        }
                    }
                    ViewData["lsInterestPremises"] = lsInterestPremises;
                    #endregion
                }
            }

            return PartialView("PremisesIndex/_InterestPremises", lsHotPremisesList);
        }

        /// <summary>
        /// 周边地图
        /// </summary>
        /// <param name="premisesid"></param>
        /// <returns></returns>
        public ActionResult MapPremises(string premisesid)
        {
            TXOrm.Premis model = new TXOrm.Premis();
            int premisesId;
            if (int.TryParse(premisesid, out premisesId))
            {
                model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null)
                {
                    //附近楼盘（3公里内）
                    ViewData["NearbyPremises"] = NearbyPremises(model.Lng, model.Lat, "3");
                }
            }
            return PartialView("PremisesIndex/_MapPremises", model);
        }

        /// <summary>
        /// 异步获取楼盘相关信息（装修情况、物业类型、开盘时间、预售许可证）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetAsyPremisesInfo(int id)
        {
            string _zxqk = string.Empty;
            string _wylx = string.Empty;
            string _kpsj = string.Empty;
            string _jfsj = string.Empty;
            string _ysxkz = string.Empty;
            string _zlhxt = string.Empty;

            TXOrm.Premis model = _premisesbll.GetPremisesbyId(id);

            if (model != null && model.Id > 0)
            {
                #region  获取楼盘的下所有楼栋装修情况
                List<int> lsRenovationInfo = _premisesbll.GetRenovationInfobyPremisesId(model);
                if (lsRenovationInfo != null)
                {
                    var arrRenovationInfo = lsRenovationInfo.Select(r => TXCommons.NewHouseWeb.BuildingType.GetRenovation(r)).Distinct().ToArray();
                    _zxqk = string.Join(",", arrRenovationInfo);
                }
                #endregion

                #region  获取楼盘的物业类型信息
                if (!string.IsNullOrEmpty(model.PropertyType))
                {
                    var arrPropertyType = model.PropertyType.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (arrPropertyType != null)
                    {
                        var listPropertyType = arrPropertyType.ToList().Select(a => TXCommons.PremisesType.GetTypeName(a)).ToList();
                        _wylx = string.Join(",", listPropertyType.ToArray());
                    }
                }

                #endregion

                #region 开盘时间&交房时间
                //开盘时间&交房时间
                //开盘时间取在售楼栋最新的2个，
                //入住时间取开盘时间对应的入住时间，
                //多个以逗号分割；
                //（开盘时间和入住时间格式：期数+日期）；
                var lsBuildingList = _premisesbll.GetBuildingListbyPremisesId(model)
                    .Where(b => b.State == 2).OrderByDescending(b => b.OpeningTime).ToList();

                var arrOpeningTime = lsBuildingList
                    .Select(b => string.Format("{0}期  {1}", b.Periods, b.OpeningTime.ToString("yyyy-MM-dd")))
                    .Distinct().Take(3).ToArray();
                var arrOthersTime = lsBuildingList
                    .Select(b => string.Format("{0}期  {1}", b.Periods, b.OthersTime.ToString("yyyy-MM-dd")))
                    .Distinct().Take(3).ToArray();

                _kpsj = string.Join(",", arrOpeningTime);
                _jfsj = string.Join(",", arrOthersTime);

                #endregion

                #region 楼盘预售许可证
                //按照  添加时间倒序排序 
                // 取前2个，调号分割
                //获取某个楼盘的预售许可证列表
                string strPermitPresaleTemp = string.Empty;
                List<TXOrm.PermitPresale> lsPermitPresale = _premisesbll.GetPermitPresaleList(model);
                if (null != lsPermitPresale && lsPermitPresale.Count > 0)
                {
                    lsPermitPresale = lsPermitPresale.Take(2).ToList();
                    _ysxkz = string.Join(",", lsPermitPresale.Select(p => p.Name).ToArray());
                }
                #endregion

                #region 获取楼盘主力户型

                //获取楼盘主力户型
                //户型图
                //List<PremisesPictureInfo> listzlhx = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString(), model.CityId);
                //if (null != listzlhx && listzlhx.Count > 0)
                //{
                //    var arrzlhx = listzlhx.Where(m => m.IsForce == 0).Select(m => m.Title).Take(3).ToArray();
                //    if (arrzlhx != null && arrzlhx.Length > 0)
                //        _zlhxt = string.Join(",", arrzlhx);
                //}
                #endregion
            }
            var result = new { zxqk = _zxqk, wylx = _wylx, kpsj = _kpsj, jfsj = _jfsj, ysxkz = _ysxkz, zlhxt = _zlhxt };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新房前台楼盘详情信息页
        /// <summary>
        /// 新房前台楼盘详情信息页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PremisesDetail(string id)
        {
            //id = "1";
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region  获取广告图片
                    //http://nhindex.kuaiyoujia.com/premises/index.ashx?condition=beijing-xinfang-quyu-a297
                    string cityId = ViewData["cityId"] as string;//取城市
                    if (string.IsNullOrWhiteSpace(cityId))
                    {
                        cityId = "253";
                    }
                    PagedList<IndexPremises> searchList = null;
                    string city = ViewData["cityPinyin"] as string;
                    // string Id = string.Format("http://nhindex.kuaiyoujia.com/premises/index.ashx?condition={0}-xinfang-quyu-a{1}", city, model.Id);
                    //beijing-xinfang-quyu
                    string Id = string.Format("{0}-xinfang-quyu-a{1}", city, model.Id);
                    //Id:条件，地址，城市
                    searchList = SearchBll.NHouseListResult(Id.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                    ViewData["ADPictureUrl"] = "";
                    //开发商LOGO图
                    ViewData["DeveloperLOGOUrl"] = "";
                    ViewData["IsRecommend"] = "0";
                    if (null != searchList && searchList.Count > 0)
                    {

                        if (null != searchList[0])
                        {
                            IndexPremises indexpremisestemp = searchList[0];
                            ViewData["ADPictureUrl"] = indexpremisestemp.ADPictureUrl;
                            //开发商LOGO图
                            ViewData["DeveloperLOGOUrl"] = indexpremisestemp.DeveloperLOGOUrl;
                            ViewData["IsRecommend"] = indexpremisestemp.IsRecommend;
                        }
                    }
                    #endregion

                    #region SEO处理
                    SEO seo = new SEO();
                    ViewData["Seo"] = seo.SeoPremisesDetail(new SEOModel(), ViewData["cityName"] as string, model);
                    #endregion

                    #region 获取楼盘下的楼栋相关信息
                    //获取楼盘下的楼栋相关信息
                    List<TXOrm.Building> lsBuilding = _premisesbll.GetBuildingListbyPremisesId(model);
                    ViewData["lsBuilding"] = lsBuilding;
                    #endregion

                    #region 楼层状态
                    string strUnderloorTemp = string.Empty;
                    //楼层状态
                    if (null != lsBuilding && lsBuilding.Count > 0)
                    {
                        foreach (TXOrm.Building building in lsBuilding)
                        {
                            //1栋27层 9栋22层
                            strUnderloorTemp = strUnderloorTemp + string.Format("{0}{1}{2}层  ", building.Name, building.NameType, building.TheFloor);
                        }
                    }
                    if (!"".Equals(strUnderloorTemp.Trim()))
                    {
                        strUnderloorTemp = strUnderloorTemp.Substring(0, strUnderloorTemp.Trim().Length);
                    }
                    ViewData["strUnderloorTemp"] = strUnderloorTemp;

                    #endregion

                    #region 楼盘预售许可证
                    //按照  添加时间倒序排序 
                    // 取第一个
                    //获取某个楼盘的预售许可证列表
                    string strPermitPresaleTemp = string.Empty;
                    List<TXOrm.PermitPresale> lsPermitPresale = _premisesbll.GetPermitPresaleList(model);
                    if (null != lsPermitPresale && lsPermitPresale.Count > 0)
                    {
                        strPermitPresaleTemp = lsPermitPresale[0].Name;
                    }
                    ViewData["strPermitPresaleTemp"] = strPermitPresaleTemp;
                    #endregion

                    #region  获取楼盘的物业类型信息
                    //获取楼盘的物业类型信息
                    //2,3
                    string strPropertyTypedesc = string.Empty;
                    string strPropertyType = string.Empty;
                    if (null != strPropertyType && null != model.PropertyType)
                    {
                        strPropertyType = model.PropertyType;
                    }
                    if (!string.Empty.Equals(strPropertyType.Trim()))
                    {
                        string[] propertytypes = strPropertyType.Split(',');
                        if (null != propertytypes && propertytypes.Length > 0)
                        {
                            foreach (string s in propertytypes)
                            {
                                if (!"".Equals(s))
                                {
                                    strPropertyTypedesc = strPropertyTypedesc + TXCommons.PremisesType.GetTypeName(s) + ",";
                                }
                            }
                        }
                    }
                    if (!"".Equals(strPropertyTypedesc.Trim()))
                    {

                        strPropertyTypedesc = strPropertyTypedesc.Substring(0, strPropertyTypedesc.Trim().Length - 1);
                    }
                    ViewData["strPropertyTypedesc"] = strPropertyTypedesc;

                    #endregion

                    #region  获取楼栋的均价
                    List<TXModel.Web.PremisesBuilding> lsBuildingNew = new List<TXModel.Web.PremisesBuilding>();
                    //楼栋开盘信息	根据后台所输入楼盘期数调取相关楼盘展示，
                    //并算出这些楼栋的均价（楼栋下房源价格总和除以房源数）
                    if (null != lsBuilding && lsBuilding.Count > 0)
                    {
                        foreach (TXOrm.Building building in lsBuilding)
                        {
                            TXModel.Web.PremisesBuilding premisesbuildingtemp = new PremisesBuilding();
                            decimal houseSinglePrice = _premisesbll.GetHouseSinglePrice(model, building.Id);
                            premisesbuildingtemp.Id = building.Id;
                            premisesbuildingtemp.Periods = building.Periods;
                            premisesbuildingtemp.OpeningTime = building.OpeningTime;
                            premisesbuildingtemp.FamilyNum = building.FamilyNum;
                            premisesbuildingtemp.Renovation = building.Renovation;
                            premisesbuildingtemp.PropertyType = building.PropertyType;
                            premisesbuildingtemp.TheFloor = building.TheFloor;
                            premisesbuildingtemp.houseSinglePrice = houseSinglePrice;
                            //premisesbuildingtemp.Id = building.Id;
                            premisesbuildingtemp.Name = building.Name;
                            premisesbuildingtemp.NameType = building.NameType;
                            premisesbuildingtemp.OthersTime = building.OthersTime;
                            lsBuildingNew.Add(premisesbuildingtemp);
                        }
                    }
                    ViewData["lsBuildingNew"] = lsBuildingNew;
                    #endregion

                    #region 楼盘项目特色
                    //3	楼盘项目特色	
                    //按照后台添加的时间倒序排序。
                    //楼盘项目特色将来可能单独设计一个表
                    string strPremisesCharacteristic = string.Empty;

                    if (null != model && null != model.Characteristic)
                    {
                        if (!string.Empty.Equals(model.Characteristic.Trim()))
                        {
                            List<TXOrm.PremisesFeature> PremisesFeaturels = _premisesbll.GetPremisesFeatureList(model.Characteristic);

                            strPremisesCharacteristic = string.Join(",", PremisesFeaturels.Select(c => c.Name).ToArray());

                            //if (null != PremisesFeaturels && PremisesFeaturels.Count > 0)
                            //{
                            //    foreach (TXOrm.PremisesFeature Feature in PremisesFeaturels)
                            //    {
                            //        if (null != Feature.Name)
                            //        {
                            //            strPremisesCharacteristic = strPremisesCharacteristic + Feature.Name.ToString() + ",";
                            //        }
                            //    }

                            //}
                            ViewData["PremisesCharacteristic"] = strPremisesCharacteristic;
                        }
                    }
                    #endregion

                    #region 浏览量、排名

                    //浏览量
                    ViewData["ViewCount"] = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", premisesId), FunctionTypeEnum.NewHouseViewCount, model.CityId);

                    //排名
                    var Rank = TXCommons.Redis.GetValue<int>(string.Format("NewHouseRank_{0}", model.Id), FunctionTypeEnum.NewHousePropertyRank, model.CityId);
                    ViewData["Rank"] = Rank;

                    #endregion


                    return View(model);
                }
                else
                {
                    return Content(NoHouseUrl());
                }
            }
            else
            {
                return Content(NoHouseUrl());
            }
        }

        #region 同区域最热楼盘排行

        /// <summary>
        /// 同区域最热楼盘排行
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TQYHotPremisesList(string id)
        {
            //根据当前楼盘所在区域推荐同区域浏览数最多的楼盘，从高到低排序。
            //List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            List<IndexRecList> list = new List<IndexRecList>();
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    SearchBll _searchBll = new SearchBll();
                    list = _searchBll.PremisesList(0, FunctionTypeEnum.NewHousePropertyRank, CityId, model.DId.ToString());
                    //if (list != null && list.Count > 0)
                    //    list.RemoveAll(m => m.PremisesID == model.Id);
                    //if (list != null && list.Count == 11)
                    //    list.RemoveAt(10);
                    //IndexRankingConditionInfo conditionInfo = new IndexRankingConditionInfo();
                    //conditionInfo.ProvinceID = model.ProvinceId.ToString();
                    //conditionInfo.CityID = model.CityId.ToString();
                    //conditionInfo.DistrictID = model.DId.ToString();
                    ////conditionInfo.BusinessID = model.BId.ToString();
                    //conditionInfo.PremisesID = model.Id.ToString();
                    ///// 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
                    //conditionInfo.Ranking = "1";
                    //lsHotPremisesList = _premisesbll.GetHotPremisesList(conditionInfo);
                }
            }
            return PartialView("PremisesDetail/_TQYHotPremisesList", list);
        }
        #endregion

        #region 同价位最热楼盘排行
        /// <summary>
        /// 同价位最热楼盘排行
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TJWHotPremisesList(string id)
        {
            //根据当前楼盘的均价推荐同均价浏览数最多的楼盘，从高到低排序。
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    IndexRankingConditionInfo conditionInfo = new IndexRankingConditionInfo();
                    conditionInfo.ProvinceID = model.ProvinceId.ToString();
                    conditionInfo.CityID = model.CityId.ToString();
                    conditionInfo.DistrictID = model.DId.ToString();
                    //conditionInfo.BusinessID = model.BId.ToString();
                    conditionInfo.PremisesID = model.Id.ToString();
                    /// 排序 (下标 5)（1 点击率 ，2 最新发布 ，3 均价 4 最新开盘）
                    conditionInfo.Ranking = "3";
                    lsHotPremisesList = _premisesbll.GetHotPremisesList(conditionInfo);
                }
            }
            return PartialView("PremisesDetail/_TJWHotPremisesList", lsHotPremisesList);
        }

        #endregion
        #endregion

        #region 楼盘房源页
        /// <summary>
        /// 楼盘房源页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PremisesHouse(string id)
        {

            string strRequesturlpremisesId = string.Empty;
            string strRequesturlbuildingId = string.Empty;
            string strRequesturlunitId = string.Empty;
            string strRequesturlfloor = string.Empty;
            //p5_b0_u0_f0
            #region
            var key = id.Split('-');
            if (key.Length > 0 && !string.IsNullOrEmpty(key[0]) && key[0].Length > 1)
            {
                strRequesturlpremisesId = key[0].Substring(1, key[0].Length - 1).Trim();
                //strRequesturlpremisesId = key[0][1].ToString().Trim();
            }
            if (key.Length > 1 && !string.IsNullOrEmpty(key[1]) && key[1].Length > 1)
            {
                strRequesturlbuildingId = key[1].Substring(1, key[1].Length - 1).Trim();
                //strRequesturlbuildingId = key[1][1].ToString().Trim();
            }
            if (key.Length > 2 && !string.IsNullOrEmpty(key[2]) && key[2].Length > 1)
            {
                strRequesturlunitId = key[2].Substring(1, key[2].Length - 1).Trim();
                //strRequesturlunitId = key[2][1].ToString().Trim();
            }
            if (key.Length > 3 && !string.IsNullOrEmpty(key[3]))
            {
                if (key[3].Length > 1)
                {
                    strRequesturlfloor = key[3].Substring(1, key[3].Length - 1).Trim();
                    //strRequesturlfloor = key[3][1].ToString().Trim();
                }
                else
                {
                    if (key.Length > 4)
                    {
                        //strRequesturlfloor = key[4].Substring(0, key[4].Length - 1).Trim();
                        strRequesturlfloor = string.Format("-{0}", key[4]);
                    }
                }
            }
            #endregion
            //id = "5";
            #region 接受节点树参数
            string strqtpremisesId = string.Empty;
            string strqtbuildingId = string.Empty;
            string strqtunitId = string.Empty;
            string strqtFloor = string.Empty;
            ////楼盘id
            strqtpremisesId = strRequesturlpremisesId;
            ////楼栋id
            strqtbuildingId = strRequesturlbuildingId;
            ////单元id
            strqtunitId = strRequesturlunitId;
            ////楼层
            strqtFloor = strRequesturlfloor;

            if (!string.Empty.Equals(strqtpremisesId.Trim()))
            {
                if (!string.Empty.Equals(strqtbuildingId.Trim()))
                {
                    ViewData["toggleVendorid"] = string.Format("{0}{1}", strqtpremisesId.Trim(), strqtbuildingId.Trim());
                }
            }
            if (!string.Empty.Equals(strqtpremisesId.Trim()))
            {
                if (!string.Empty.Equals(strqtbuildingId.Trim()))
                {
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        ViewData["toggleVendorsubid"] = string.Format("sub{0}{1}{2}", strqtpremisesId.Trim(), strqtbuildingId.Trim(), strqtunitId.Trim());
                    }
                }
            }
            if (!string.Empty.Equals(strqtpremisesId.Trim()))
            {
                if (!string.Empty.Equals(strqtbuildingId.Trim()))
                {
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        if (!string.Empty.Equals(strqtFloor.Trim()))
                        {
                            ViewData["toggleVendorfloor"] = string.Format("lisubsub{0}{1}{2}{3}", strqtpremisesId.Trim(), strqtbuildingId.Trim(), strqtunitId.Trim(), strqtFloor.Trim());
                        }
                    }
                }
            }
            #endregion 接受节点树参数
            int premisesId;
            if (int.TryParse(strRequesturlpremisesId, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region  参数接收
                    int ibuildingid = -1000;
                    int iunitId = -1000;
                    int ifloor = -1000;
                    //节点树参数接收之后，动态取页面右侧信息
                    if (!string.Empty.Equals(strqtbuildingId.Trim()))
                    {
                        int.TryParse(strqtbuildingId, out ibuildingid);
                    }
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        int.TryParse(strqtunitId, out iunitId);
                    }
                    if (!string.Empty.Equals(strqtFloor.Trim()))
                    {
                        int.TryParse(strqtFloor, out ifloor);
                    }
                    #endregion

                    #region  获取广告图片
                    //http://nhindex.kuaiyoujia.com/premises/index.ashx?condition=beijing-xinfang-quyu-a297
                    string cityId = ViewData["cityId"] as string;//取城市
                    if (string.IsNullOrWhiteSpace(cityId))
                    {
                        cityId = "253";
                    }
                    PagedList<IndexPremises> searchList = null;
                    string city = ViewData["cityPinyin"] as string;
                    // string Id = string.Format("http://nhindex.kuaiyoujia.com/premises/index.ashx?condition={0}-xinfang-quyu-a{1}", city, model.Id);
                    //beijing-xinfang-quyu
                    string Id = string.Format("{0}-xinfang-quyu-a{1}", city, model.Id);
                    //Id:条件，地址，城市
                    searchList = SearchBll.NHouseListResult(Id.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                    ViewData["ADPictureUrl"] = "";
                    //开发商LOGO图
                    ViewData["DeveloperLOGOUrl"] = "";
                    ViewData["IsRecommend"] = "0";
                    if (null != searchList && searchList.Count > 0)
                    {
                        if (null != searchList[0])
                        {
                            IndexPremises indexpremisestemp = searchList[0];
                            ViewData["ADPictureUrl"] = indexpremisestemp.ADPictureUrl;
                            //开发商LOGO图
                            ViewData["DeveloperLOGOUrl"] = indexpremisestemp.DeveloperLOGOUrl;
                            ViewData["IsRecommend"] = indexpremisestemp.IsRecommend;
                        }
                    }
                    #endregion

                    #region SEO处理
                    SEO seo = new SEO();
                    ViewData["Seo"] = seo.SeoHouseList(new SEOModel(), ViewData["cityName"] as string, model.Name);
                    #endregion
                    //===========================
                    ViewData["premisesIdViewDatatemp"] = premisesId;
                    ViewData["ibuildingidViewDatatemp"] = ibuildingid;
                    ViewData["iunitIdViewDatatemp"] = iunitId;
                    ViewData["ifloorViewDatatemp"] = ifloor;
                    //===========================
                    #region 后台生成节点树html
                    string strhrefurl1 = string.Empty;
                    string strhrefurl2 = string.Empty;
                    string strhrefurl3 = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    //获取楼盘下的楼栋相关信息
                    List<TXOrm.Building> lsBuilding = _premisesbll.GetBuildingListbyPremisesId(model);
                    ViewData["ldCount"] = lsBuilding.Count;
                    if (null != lsBuilding && lsBuilding.Count > 0)
                    {
                        if (ViewData["toggleVendorid"] == null)
                        {
                            ViewData["toggleVendorid"] = string.Format("{0}{1}", model.Id, lsBuilding[0].Id);
                        }
                        foreach (TXOrm.Building building in lsBuilding)
                        {
                            //int premisesId, int buildingId, int unitId
                            // http://beijing.kyjob.com/xinfang/lp_fy_p1_b2_u1_f1
                            // href="<%=TXCommons.GetConfig.GetQTBaseUrl%>xinfang/lp_fy_p<%=Model.Id %>_b0_u0_f0"
                            //============
                            int lsUnittemp1Num = 0;
                            List<TXOrm.Unit> lsUnittemp1 = _premisesbll.GetUnitListbyBuildingId(building.Id);
                            if (null != lsUnittemp1 && lsUnittemp1.Count > 0)
                            {
                                if (null != lsUnittemp1[0])
                                {
                                    lsUnittemp1Num = lsUnittemp1[0].Num;
                                }
                            }
                            //============
                            strhrefurl1 = string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, model.Id, building.Id, lsUnittemp1Num, 0);
                            string strtempHousecount = _premisesbll.GetHousecountbyunit(model.Id, building.Id, -10000, -10000).ToString();
                            //  sb.Append(" <li id=\"li" + model.Id + building.Id + "\" onclick=\"toggleVendor('" + model.Id + building.Id + "')\" class=\"down\"><a href=\"/Premises/PremisesHouse?premisesId=" + model.Id + "&buildingId=" + building.Id + "\">" + building.Name + "栋（" + building.FamilyNum + "户）</a></li> ");
                            //sb.Append(" <li id=\"li" + model.Id + building.Id + "\" onclick=\"toggleVendor('" + model.Id + building.Id + "')\" class=\"down\"><a href=\"" + strhrefurl1 + "\">" + building.Name + "栋（" + building.FamilyNum + "户）</a></li> ");
                            sb.Append("<li id=\"li" + model.Id + building.Id + "\" onclick=\"toggleVendor('" + model.Id + building.Id + "')\" class=\"down\"><a href=\"" + strhrefurl1 + "\">" + Util.getStrLenB(building.Name, 0, 10) + building.NameType + "（" + strtempHousecount + "户）</a></li> ");
                            sb.Append("<li id=\"ul" + model.Id + building.Id + "\" class=\"dd\"  style=\"display:none;\"><ul>");
                            //int buildingId
                            List<TXOrm.Unit> lsUnit = _premisesbll.GetUnitListbyBuildingId(building.Id);
                            if (null != lsUnit && lsUnit.Count > 0)
                            {
                                if (ViewData["toggleVendorsubid"] == null)
                                {
                                    ViewData["toggleVendorsubid"] = string.Format("sub{0}{1}{2}", model.Id, building.Id, lsUnit[0].Num);
                                }
                                foreach (TXOrm.Unit unit in lsUnit)
                                {
                                    string temp = _premisesbll.GetHousecountbyunit(model.Id, building.Id, unit.Num, -10000).ToString();
                                    strhrefurl2 = string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, model.Id, building.Id, unit.Num, 0);
                                    //  sb.Append("  <li id=\"lisub" + model.Id + building.Id + unit.Id + "\" onclick=\"toggleVendor('sub" + model.Id + building.Id + unit.Id + "')\" class=\"down\"><a href=\"/Premises/PremisesHouse?premisesId=" + model.Id + "&buildingId=" + building.Id + "&unitId=" + unit.Id + "\">" + unit.Name + "（" + temp + "户）</a></li> ");
                                    sb.Append("<li id=\"lisub" + model.Id + building.Id + unit.Num + "\" onclick=\"toggleVendor('sub" + model.Id + building.Id + unit.Num + "')\" class=\"down\"><a href=\"" + strhrefurl2 + "\">" + unit.Name + "（" + temp + "户）</a></li>");
                                    sb.Append("<li id=\"ulsub" + model.Id + building.Id + unit.Num + "\"  class=\"dd\" style=\"display:none;\"><ul>");
                                    List<TXModel.Web.HouseFloorCount> lsHouse = _premisesbll.GetHouseFloorList(model.Id, building.Id, unit.Num);
                                    if (null != lsHouse && lsHouse.Count > 0)
                                    {
                                        foreach (TXModel.Web.HouseFloorCount house in lsHouse)
                                        {
                                            strhrefurl3 = string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, model.Id, building.Id, unit.Num, house.Floor);
                                            //sb.Append("   <li  id=\"lisubsub" + model.Id + building.Id + unit.Id + house.Floor + "\"><a href=\"/Premises/PremisesHouse?premisesId=" + model.Id + "&buildingId=" + building.Id + "&unitId=" + unit.Id + "&Floor=" + house.Floor + "\">" + house.Floor + "层（" + house.Floorcount + "户）</a></li>");
                                            sb.Append("<li  id=\"lisubsub" + model.Id + building.Id + unit.Num + house.Floor + "\"><a href=\"" + strhrefurl3 + "\">" + house.Floor + "层（" + house.Floorcount + "户）</a></li>");

                                        }
                                    }
                                    sb.Append("</ul></li>");
                                }

                            }
                            sb.Append("</ul></li>");
                        }

                    }
                    ViewData["html"] = sb.ToString();
                    #endregion 后台生成节点树html

                    #region 浏览量、排名

                    //浏览量
                    ViewData["ViewCount"] = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", premisesId), FunctionTypeEnum.NewHouseViewCount, model.CityId);

                    //排名
                    var Rank = TXCommons.Redis.GetValue<int>(string.Format("NewHouseRank_{0}", model.Id), FunctionTypeEnum.NewHousePropertyRank, model.CityId);
                    ViewData["Rank"] = Rank;

                    #endregion

                    return View(model);
                }
                else
                {
                    return Content(NoHouseUrl());
                }
            }
            else
            {
                return Content(NoHouseUrl());
            }
        }
        /// <summary>
        /// 获取楼盘房源页面右侧信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildingid"></param>
        /// <param name="unitId"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public ActionResult PremisesHouseRightInfo(string id, string buildingid, string unitId, string floor)
        {
            //==================
            string strqtpremisesId = string.Empty;
            string strqtbuildingId = string.Empty;
            string strqtunitId = string.Empty;
            string strqtFloor = string.Empty;
            ////楼盘id
            strqtpremisesId = id;
            ////楼栋id
            strqtbuildingId = buildingid;
            ////单元id
            strqtunitId = unitId;
            ////楼层
            strqtFloor = floor;
            //==================
            TXOrm.Premis model = new TXOrm.Premis();
            PremisStatisticsInfo psi = null;
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region  参数接收
                    int ibuildingid = -1000;
                    int iunitId = -1000;
                    int ifloor = -1000;
                    //节点树参数接收之后，动态取页面右侧信息
                    if (!string.Empty.Equals(strqtbuildingId.Trim()))
                    {
                        int.TryParse(strqtbuildingId, out ibuildingid);
                    }
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        int.TryParse(strqtunitId, out iunitId);
                    }
                    if (!string.Empty.Equals(strqtFloor.Trim()))
                    {
                        int.TryParse(strqtFloor, out ifloor);
                    }
                    #endregion

                    #region 获取楼栋基本信息
                    //获取楼栋基本信息
                    TXOrm.Building buildingtemp = _premisesbll.GetBuildingInfobyId(premisesId, ibuildingid);
                    if (ibuildingid == -1000)
                    {
                        if (null != buildingtemp && buildingtemp.Id > 0)
                        {
                            ibuildingid = buildingtemp.Id;
                        }
                    }
                    ViewData["buildingtemp"] = buildingtemp;
                    List<TXOrm.Unit> UnitList = new List<TXOrm.Unit>();
                    //===================
                    if (null != buildingtemp && buildingtemp.Id > 0)
                    {
                        UnitList = _premisesbll.GetUnitListbyBuildingId(buildingtemp.Id);
                    }
                    //===================
                    #endregion

                    #region  获取房源信息列表
                    //===========================
                    //ViewData["premisesIdViewDatatemp"] = premisesId;
                    //ViewData["ibuildingidViewDatatemp"] = ibuildingid;
                    //ViewData["iunitIdViewDatatemp"] = iunitId;
                    //ViewData["ifloorViewDatatemp"] = ifloor;
                    //===========================
                    if (null != UnitList && UnitList.Count > 0)
                    {
                        if (iunitId <= 0)
                        {
                            if (null != UnitList[0])
                            {
                                iunitId = UnitList[0].Num;
                            }
                        }
                    }
                    //=============================
                    List<TXOrm.House> lsHousetemp = _premisesbll.GetHouseList(premisesId, ibuildingid, iunitId, ifloor);
                    string strwhere = "(-1000";
                    if (null != lsHousetemp && lsHousetemp.Count > 0)
                    {
                        foreach (TXOrm.House tempHouse in lsHousetemp)
                        {
                            strwhere = strwhere + "," + tempHouse.UnitId;
                        }

                    }
                    strwhere = strwhere + ")";
                    //================================
                    string strwhereFloorInfo = "(-1000";
                    if (null != lsHousetemp && lsHousetemp.Count > 0)
                    {
                        foreach (TXOrm.House tempHouse in lsHousetemp)
                        {
                            strwhereFloorInfo = strwhereFloorInfo + "," + tempHouse.Floor;
                        }

                    }
                    strwhereFloorInfo = strwhereFloorInfo + ")";
                    //================================
                    List<PremisesActivitiesHouse> lsPremisesActivitiesHouse = _premisesbll.newGetActivitiesHouseById(model.Id);
                    if (null != lsHousetemp && lsHousetemp.Count > 0)
                    {
                        foreach (TXOrm.House tHouse in lsHousetemp)
                        {
                            tHouse.PictureData = "";

                            if (null != lsPremisesActivitiesHouse && lsPremisesActivitiesHouse.Count > 0)
                            {

                                foreach (PremisesActivitiesHouse tPremisesActivitiesHouse in lsPremisesActivitiesHouse)
                                {
                                    if (tHouse.Id == tPremisesActivitiesHouse.HouseId)
                                    {

                                        //================
                                        /// 活动类型
                                        //1摇号
                                        //2限时折扣
                                        //3排号购房
                                        //4阶梯团购
                                        //5竞价
                                        //6砍价
                                        //7秒杀
                                        //8一口价
                                        switch (tPremisesActivitiesHouse.Type)
                                        {
                                            case 7:
                                                tHouse.PictureData = "<span class=\"ms_ic ml10\"/>";
                                                break;
                                            case 6:
                                                tHouse.PictureData = "<span class=\"kj_ic ml10\"/>";
                                                break;
                                            case 5:
                                                tHouse.PictureData = "<span class=\"jj_ic ml10\"/>";
                                                break;
                                            case 8:
                                                tHouse.PictureData = " <span class=\"bg_6aa700 ml10\">一口价</span>";
                                                break;
                                            default:
                                                tHouse.PictureData = ""; break;
                                        }
                                        //================
                                    }
                                }
                            }
                        }
                    }
                    //=================================
                    ViewData["lsHousetemp"] = lsHousetemp;

                    List<TXModel.Web.HouseFloor> lsHouseFloorInfoList = _premisesbll.GetHouseFloorInfoList(model.Id, ibuildingid, ifloor, strwhereFloorInfo);
                    ViewData["lsHouseFloorInfoList"] = lsHouseFloorInfoList;
                    #endregion

                    #region  获取单元信息
                    //获取单元信息
                    List<TXOrm.Unit> lsUnittemp = _premisesbll.GetUnitListbyBuildingId(premisesId, ibuildingid, iunitId, strwhere);
                    ViewData["lsUnittemp"] = lsUnittemp;
                    #endregion
                    psi = _premisesbll.GetPremisStatisticsInfo(model.Id);
                    ViewData["tempPictureUserCount"] = model.UserCount;
                    ViewData["SubscribedCount"] = psi == null ? 0 : psi.SubscribedCount;//认购套数
                    ViewData["BargainCount"] = psi == null ? 0 : psi.BargainCount;//成交套数
                }
            }
            return PartialView("PremisesHouse/_Right", model);
        }
        /// <summary>
        /// 异步获取楼盘房源页面顶部信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="buildingid"></param>
        /// <param name="unitId"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public ActionResult PremisesHouseTopInfo(string id, string buildingid, string unitId, string floor)
        {
            //==================
            string strqtpremisesId = string.Empty;
            string strqtbuildingId = string.Empty;
            string strqtunitId = string.Empty;
            string strqtFloor = string.Empty;
            ////楼盘id
            strqtpremisesId = id;
            ////楼栋id
            strqtbuildingId = buildingid;
            ////单元id
            strqtunitId = unitId;
            ////楼层
            strqtFloor = floor;
            //==================
            TXOrm.Premis model = new TXOrm.Premis();
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    ViewData["PremisesmodelId"] = model.Id;
                    //楼盘效果图
                    List<PremisesPictureInfo> lsPlane = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), model.CityId);
                    ViewData["lsPlane"] = lsPlane;
                    #region  参数接收
                    int ibuildingid = -1000;
                    int iunitId = -1000;
                    int ifloor = -1000;
                    //节点树参数接收之后，动态取页面右侧信息
                    if (!string.Empty.Equals(strqtbuildingId.Trim()))
                    {
                        int.TryParse(strqtbuildingId, out ibuildingid);
                    }
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        int.TryParse(strqtunitId, out iunitId);
                    }
                    if (!string.Empty.Equals(strqtFloor.Trim()))
                    {
                        int.TryParse(strqtFloor, out ifloor);
                    }
                    #endregion

                    #region 获取楼栋基本信息
                    //获取楼栋基本信息
                    TXOrm.Building buildingtemp = _premisesbll.GetBuildingInfobyId(premisesId, ibuildingid);
                    ViewData["buildingtemp"] = buildingtemp;
                    List<TXOrm.Unit> UnitList = new List<TXOrm.Unit>();
                    //===================
                    if (null != buildingtemp && buildingtemp.Id > 0)
                    {
                        UnitList = _premisesbll.GetUnitListbyBuildingId(buildingtemp.Id);
                        if (null != UnitList && UnitList.Count > 0)
                        {
                            ViewData["UnitListCount"] = UnitList.Count;
                        }
                        else
                        {
                            ViewData["UnitListCount"] = 0;
                        }

                        //===================

                        //获取某个楼盘下面的所有房源的户型基本信息
                        //List<TXModel.Web.PremisesHouseHuX> lsPremisesHouseHuX = _premisesbll.GetPremisesHouseHuXList(model, buildingtemp.Id);

                        List<TXModel.Web.BuildingHouseHuX> lsBuildingHouseHuX = _premisesbll.GetPremisesHouseHuXListNew(model, buildingtemp.Id);

                        if (lsBuildingHouseHuX.Count > 3)
                        {
                            ViewData["zhankai"] = "";
                        }
                        else
                        {
                            ViewData["zhankai"] = "style=\"display: none;\"";
                        }
                        ViewData["lsBuildingHouseHuX"] = lsBuildingHouseHuX;
                    }
                    #endregion
                }
            }
            return PartialView("PremisesHouse/_TopPremisesHouse", model);
        }
        /// <summary>
        /// 异步获取楼盘房源页面左侧节点树目录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TreePremises(string id)
        {
            List<IndexRanking> lsHotPremisesList = new List<IndexRanking>();
            //===================
            string strRequesturlpremisesId = string.Empty;
            string strRequesturlbuildingId = string.Empty;
            string strRequesturlunitId = string.Empty;
            string strRequesturlfloor = string.Empty;
            //p5_b0_u0_f0
            #region
            var key = id.Split('-');
            if (key.Length > 0)
            {
                if (null != key[0])
                {
                    if (key[0] != "")
                    {
                        if (key[0].Length > 1)
                        {
                            strRequesturlpremisesId = key[0].Substring(1, key[0].Length - 1).Trim();
                            //strRequesturlpremisesId = key[0][1].ToString().Trim();
                        }
                    }
                }

            }
            if (key.Length > 1)
            {
                if (null != key[1])
                {
                    if (key[1] != "")
                    {
                        if (key[1].Length > 1)
                        {
                            strRequesturlbuildingId = key[1].Substring(1, key[1].Length - 1).Trim();
                            //strRequesturlbuildingId = key[1][1].ToString().Trim();
                        }
                    }
                }

            }
            if (key.Length > 2)
            {
                if (null != key[2])
                {
                    if (key[2] != "")
                    {
                        if (key[2].Length > 1)
                        {
                            strRequesturlunitId = key[2].Substring(1, key[2].Length - 1).Trim();
                            //strRequesturlunitId = key[2][1].ToString().Trim();
                        }
                    }
                }

            }
            if (key.Length > 3)
            {
                if (null != key[3])
                {
                    if (key[3] != "")
                    {
                        if (key[3].Length > 1)
                        {
                            strRequesturlfloor = key[3].Substring(1, key[3].Length - 1).Trim();
                            //strRequesturlfloor = key[3][1].ToString().Trim();
                        }
                        else
                        {
                            if (key.Length > 4)
                            {
                                //strRequesturlfloor = key[4].Substring(0, key[4].Length - 1).Trim();
                                strRequesturlfloor = string.Format("-{0}", key[4]);
                            }
                        }
                    }
                }

            }
            #endregion
            //id = "5";
            #region 接受节点树参数
            string strqtpremisesId = string.Empty;
            string strqtbuildingId = string.Empty;
            string strqtunitId = string.Empty;
            string strqtFloor = string.Empty;
            ////楼盘id
            strqtpremisesId = strRequesturlpremisesId;
            ////楼栋id
            strqtbuildingId = strRequesturlbuildingId;
            ////单元id
            strqtunitId = strRequesturlunitId;
            ////楼层
            strqtFloor = strRequesturlfloor;

            if (!string.Empty.Equals(strqtpremisesId.Trim()))
            {
                if (!string.Empty.Equals(strqtbuildingId.Trim()))
                {
                    ViewData["toggleVendorid"] = string.Format("{0}{1}", strqtpremisesId.Trim(), strqtbuildingId.Trim());
                }
            }
            if (!string.Empty.Equals(strqtpremisesId.Trim()))
            {
                if (!string.Empty.Equals(strqtbuildingId.Trim()))
                {
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        ViewData["toggleVendorsubid"] = string.Format("sub{0}{1}{2}", strqtpremisesId.Trim(), strqtbuildingId.Trim(), strqtunitId.Trim());
                    }
                }
            }
            if (!string.Empty.Equals(strqtpremisesId.Trim()))
            {
                if (!string.Empty.Equals(strqtbuildingId.Trim()))
                {
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        if (!string.Empty.Equals(strqtFloor.Trim()))
                        {
                            ViewData["toggleVendorfloor"] = string.Format("lisubsub{0}{1}{2}{3}", strqtpremisesId.Trim(), strqtbuildingId.Trim(), strqtunitId.Trim(), strqtFloor.Trim());
                        }
                    }
                }
            }
            #endregion 接受节点树参数
            int premisesId;
            if (int.TryParse(strRequesturlpremisesId, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region  参数接收
                    int ibuildingid = -1000;
                    int iunitId = -1000;
                    int ifloor = -1000;
                    //节点树参数接收之后，动态取页面右侧信息
                    if (!string.Empty.Equals(strqtbuildingId.Trim()))
                    {
                        int.TryParse(strqtbuildingId, out ibuildingid);
                    }
                    if (!string.Empty.Equals(strqtunitId.Trim()))
                    {
                        int.TryParse(strqtunitId, out iunitId);
                    }
                    if (!string.Empty.Equals(strqtFloor.Trim()))
                    {
                        int.TryParse(strqtFloor, out ifloor);
                    }
                    #endregion

                    #region SEO处理
                    SEO seo = new SEO();
                    ViewData["Seo"] = seo.SeoHouseList(new SEOModel(), ViewData["cityName"] as string, model.Name);
                    #endregion
                    //===========================
                    ViewData["premisesIdViewDatatemp"] = premisesId;
                    ViewData["ibuildingidViewDatatemp"] = ibuildingid;
                    ViewData["iunitIdViewDatatemp"] = iunitId;
                    ViewData["ifloorViewDatatemp"] = ifloor;
                    //===========================
                    #region 后台生成节点树html
                    string strhrefurl1 = string.Empty;
                    string strhrefurl2 = string.Empty;
                    string strhrefurl3 = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    //获取楼盘下的楼栋相关信息
                    List<TXOrm.Building> lsBuilding = _premisesbll.GetBuildingListbyPremisesId(model);
                    if (null != lsBuilding && lsBuilding.Count > 0)
                    {

                        foreach (TXOrm.Building building in lsBuilding)
                        {
                            //int premisesId, int buildingId, int unitId
                            // http://beijing.kyjob.com/xinfang/lp_fy_p1_b2_u1_f1
                            // href="<%=TXCommons.GetConfig.GetQTBaseUrl%>xinfang/lp_fy_p<%=Model.Id %>_b0_u0_f0"
                            //============
                            int lsUnittemp1Num = 0;
                            List<TXOrm.Unit> lsUnittemp1 = _premisesbll.GetUnitListbyBuildingId(building.Id);
                            if (null != lsUnittemp1 && lsUnittemp1.Count > 0)
                            {
                                if (null != lsUnittemp1[0])
                                {
                                    lsUnittemp1Num = lsUnittemp1[0].Num;
                                }
                            }
                            //============
                            strhrefurl1 = string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, model.Id, building.Id, lsUnittemp1Num, 0);
                            string strtempHousecount = _premisesbll.GetHousecountbyunit(model.Id, building.Id, -10000, -10000).ToString();
                            //  sb.Append(" <li id=\"li" + model.Id + building.Id + "\" onclick=\"toggleVendor('" + model.Id + building.Id + "')\" class=\"down\"><a href=\"/Premises/PremisesHouse?premisesId=" + model.Id + "&buildingId=" + building.Id + "\">" + building.Name + "栋（" + building.FamilyNum + "户）</a></li> ");
                            //sb.Append(" <li id=\"li" + model.Id + building.Id + "\" onclick=\"toggleVendor('" + model.Id + building.Id + "')\" class=\"down\"><a href=\"" + strhrefurl1 + "\">" + building.Name + "栋（" + building.FamilyNum + "户）</a></li> ");
                            sb.Append(" <li id=\"li" + model.Id + building.Id + "\" onclick=\"toggleVendor('" + model.Id + building.Id + "')\" class=\"down\"><a href=\"" + strhrefurl1 + "\">" + building.Name + building.NameType + "（" + strtempHousecount + "户）</a></li> ");
                            sb.Append("  <ul id=\"ul" + model.Id + building.Id + "\" class=\"dd\"  style=\"display:none;\"> ");
                            //int buildingId
                            List<TXOrm.Unit> lsUnit = _premisesbll.GetUnitListbyBuildingId(building.Id);
                            if (null != lsUnit && lsUnit.Count > 0)
                            {
                                foreach (TXOrm.Unit unit in lsUnit)
                                {
                                    string temp = _premisesbll.GetHousecountbyunit(model.Id, building.Id, unit.Num, -10000).ToString();
                                    strhrefurl2 = string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, model.Id, building.Id, unit.Num, 0);
                                    //  sb.Append("  <li id=\"lisub" + model.Id + building.Id + unit.Id + "\" onclick=\"toggleVendor('sub" + model.Id + building.Id + unit.Id + "')\" class=\"down\"><a href=\"/Premises/PremisesHouse?premisesId=" + model.Id + "&buildingId=" + building.Id + "&unitId=" + unit.Id + "\">" + unit.Name + "（" + temp + "户）</a></li> ");
                                    sb.Append("  <li id=\"lisub" + model.Id + building.Id + unit.Num + "\" onclick=\"toggleVendor('sub" + model.Id + building.Id + unit.Num + "')\" class=\"down\"><a href=\"" + strhrefurl2 + "\">" + unit.Name + "（" + temp + "户）</a></li> ");
                                    sb.Append("   <ul id=\"ulsub" + model.Id + building.Id + unit.Num + "\"  class=\"dd\" style=\"display:none;\"> ");
                                    List<TXModel.Web.HouseFloorCount> lsHouse = _premisesbll.GetHouseFloorList(model.Id, building.Id, unit.Num);
                                    if (null != lsHouse && lsHouse.Count > 0)
                                    {
                                        foreach (TXModel.Web.HouseFloorCount house in lsHouse)
                                        {
                                            strhrefurl3 = string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, model.Id, building.Id, unit.Num, house.Floor);
                                            //sb.Append("   <li  id=\"lisubsub" + model.Id + building.Id + unit.Id + house.Floor + "\"><a href=\"/Premises/PremisesHouse?premisesId=" + model.Id + "&buildingId=" + building.Id + "&unitId=" + unit.Id + "&Floor=" + house.Floor + "\">" + house.Floor + "层（" + house.Floorcount + "户）</a></li>");
                                            sb.Append("   <li  id=\"lisubsub" + model.Id + building.Id + unit.Num + house.Floor + "\"><a href=\"" + strhrefurl3 + "\">" + house.Floor + "层（" + house.Floorcount + "户）</a></li>");

                                        }
                                    }
                                    sb.Append("  </ul>");
                                }

                            }
                            sb.Append("  </ul>");
                        }

                    }
                    ViewData["html"] = sb.ToString();
                    #endregion 后台生成节点树html
                }
            }
            //==================
            return PartialView("PremisesIndex/_TreePremises", lsHotPremisesList);
        }

        #endregion

        #region 楼盘不存在页面
        /// <summary>
        /// 楼盘不存在页面
        /// </summary>
        /// <returns></returns>
        private string NoHouseUrl()
        {
            string strgetbaseurl = NHWebUrl.NewHouseUrl();
            return "<script>alert('该楼盘不存在！');location.href='" + strgetbaseurl + "'</script>";
        }
        #endregion

        #region 房间详情
        /// <summary>
        /// Author：崔利国A
        /// Time：2013-11-29
        /// 房间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult House(string id)
        {
            int houseId;
            if (int.TryParse(id, out houseId))
            {
                PremisesHouse model = _premisesbll.GetPremisesHouseById(houseId);
                #region SEO处理
                SEO seo = new SEO();
                string title = "";
                var actId = 0;
                int.TryParse(model.ActivityId.ToString(), out actId);
                //第1：如果没有参加活动，或者 （参加的活动已经结束并或到期且房源还处于在售状态）则只显示房源信息部分
                if (actId == 0 || (model.SalesStatus == 2 && (model.ActivityState == 2 || model.ActEndTime < DateTime.Now)))
                {
                    title = model.PremisesName + model.BuildingName + "栋" + model.UnitName + model.Floor.ToString() + model.HouseName + "号房";
                }
                else
                {
                    switch (model.ActivityType)
                    {
                        case 3://排号活动房
                            title = "排号活动房"; break;
                        case 4://阶梯价格活动房
                            title = "阶梯价格活动房"; break;
                        case 5://竞价活动房
                            title = "竞价活动房"; break;
                        case 6://砍价活动房
                            title = "砍价活动房"; break;
                        case 7://秒杀活动房
                            title = "秒杀活动房"; break;
                        case 8://一口价活动房
                            title = "一口价活动房"; break;
                        default:
                            title = model.PremisesName + model.BuildingName + "栋" + model.UnitName + model.Floor.ToString() + model.HouseName + "号房"; break;
                    }

                }
                ViewData["Seo"] = seo.SeoHouseIndex(new SEOModel(), ViewData["cityName"] as string, title);
                #endregion

                if (model != null && model.HouseId > 0)
                {
                    //取房源所在楼盘的楼栋列表
                    TXOrm.Premis _premis = new TXOrm.Premis();
                    _premis = _premisesbll.GetPremisesbyId(model.PremisesId);
                    ViewData["Premis"] = _premis;

                    if (_premis != null)
                    {
                        #region  获取广告图片
                        //http://nhindex.kuaiyoujia.com/premises/index.ashx?condition=beijing-xinfang-quyu-a297
                        string cityId = ViewData["cityId"] as string;//取城市
                        if (string.IsNullOrWhiteSpace(cityId))
                        {
                            cityId = "253";
                        }
                        PagedList<IndexPremises> searchList = null;
                        string city = ViewData["cityPinyin"] as string;
                        // string Id = string.Format("http://nhindex.kuaiyoujia.com/premises/index.ashx?condition={0}-xinfang-quyu-a{1}", city, model.Id);
                        //beijing-xinfang-quyu
                        string Id = string.Format("{0}-xinfang-quyu-a{1}", city, _premis.Id);
                        //Id:条件，地址，城市
                        searchList = SearchBll.NHouseListResult(Id.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                        ViewData["ADPictureUrl"] = "";
                        //开发商LOGO图
                        ViewData["DeveloperLOGOUrl"] = "";
                        ViewData["IsRecommend"] = "0";
                        ViewData["DName"] = "";
                        if (null != searchList && searchList.Count > 0)
                        {
                            if (null != searchList[0])
                            {
                                IndexPremises indexpremisestemp = searchList[0];
                                ViewData["ADPictureUrl"] = indexpremisestemp.ADPictureUrl;
                                //开发商LOGO图
                                ViewData["DeveloperLOGOUrl"] = indexpremisestemp.DeveloperLOGOUrl;
                                ViewData["IsRecommend"] = indexpremisestemp.IsRecommend;
                                ViewData["DName"] = indexpremisestemp.DistrictName;
                            }
                        }
                        #endregion

                        var buildings = _premisesbll.GetBuildingListbyPremisesId(_premis).ToList().ConvertAll(it => new SelectListItem
                        {
                            Text = it.Name,
                            Value = it.Id.ToString()
                        });
                        model.Buildings = buildings;
                        //取房源所在楼栋的单元列表
                        var units = _premisesbll.GetUnitListbyBuildingId(model.BuildingId).ToList().ConvertAll(it => new SelectListItem
                        {
                            Text = it.Name,
                            Value = it.Num.ToString()
                        });
                        model.Units = units;
                        //取房源所在楼栋的层数
                        List<SelectListItem> floors = new List<SelectListItem>();
                        for (int i = 1; i <= model.TheFloor; i++)
                        {
                            floors.Add(new SelectListItem { Text = i.ToString() + "层", Value = i.ToString() });
                        }
                        model.Floors = floors;
                        //取房源所在单元楼层的所有房间列表
                        var houses = _premisesbll.GetHouseList(model.PremisesId, model.BuildingId, model.UnitId, model.Floor).ToList().ConvertAll(it => new SelectListItem
                        {
                            Text = it.Name,
                            Value = it.Id.ToString()
                        });
                        model.Houses = houses;

                        double count = TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseFocusHouseCount, _premis.CityId, "h_" + houseId.ToString());

                        #region 浏览量、排名

                        //浏览量
                        ViewData["ViewCount"] = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", model.PremisesId), FunctionTypeEnum.NewHouseViewCount, _premis.CityId);

                        //排名
                        var Rank = TXCommons.Redis.GetValue<int>(string.Format("NewHouseRank_{0}", _premis.Id), FunctionTypeEnum.NewHousePropertyRank, _premis.CityId);
                        ViewData["Rank"] = Rank;

                        #endregion
                    }
                    return View(model);
                }
                else
                {
                    return Content(NoHouseUrl());
                }
            }
            else
            {
                return Content(NoHouseUrl());
            }
        }

        /// <summary>
        /// 跟据楼栋Id取单元信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public string UnitListbyBuildingId(int buildingId)
        {
            var units = _premisesbll.GetUnitListbyBuildingId(buildingId).ToList();
            return ToJSon.ObjectToJson(units);
        }

        /// <summary>
        /// 根据房源Id取楼层
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public string FoorListbyHouseId(int houseId)
        {
            PremisesHouse model = _premisesbll.GetPremisesHouseById(houseId);
            List<SelectListItem> floors = new List<SelectListItem>();
            for (int i = 1; i <= model.TheFloor; i++)
            {
                floors.Add(new SelectListItem { Text = i.ToString() + "层", Value = i.ToString() });
            }
            model.Floors = floors;
            return ToJSon.ObjectToJson(model.Floors);
        }

        /// <summary>
        /// 跟据楼栋Id取单元信息
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public string GetHouse(int premisesId, int buildingId, int floor, int unitId)
        {
            var units = _premisesbll.GetHouseList(premisesId, buildingId, unitId, floor).ToList();
            return ToJSon.ObjectToJson(units);
        }

        /// <summary>
        /// 房源详情局部视图
        /// </summary>
        /// <param name="premiseshouse"></param>
        /// <returns></returns>
        public ActionResult HouseDetail(int houseid)
        {

            var premiseshouse = _premisesbll.GetPremisesHouseById(houseid);
            int actId = 0;
            int.TryParse(premiseshouse.ActivityId.ToString(), out actId);

            #region  楼栋平面图
            int premisesId = 0;
            if (null != premiseshouse)
            {
                int.TryParse(premiseshouse.PremisesId.ToString(), out premisesId);
            }
            TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);

            if (model != null && model.Id > 0)
            {
                //楼栋平面图
                List<TXCommons.PictureModular.PremisesPictureInfo> listCarousePlane = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                listCarousePlane = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Plane.ToString(), model.CityId);

                TXBll.HouseData.HouseBll _housebll = new TXBll.HouseData.HouseBll();
                TXOrm.House house = _housebll.GetHouseModel(premiseshouse.HouseId);
                TXBll.HouseData.BuildingBll _buildingBll = new TXBll.HouseData.BuildingBll();
                TXOrm.Building building = _buildingBll.GetEntity_ById(house.BuildingId);

                if (null != listCarousePlane && listCarousePlane.Count > 0)
                {
                    if (building != null)
                    {
                        var temp = listCarousePlane.Where(b => b.ID.ToString() == building.PictureData).FirstOrDefault();
                        if (temp != null)
                        {

                            //   carousel4.ImgSrc = listCarousePlane[0].Path.ToString();
                            //       carousel4.SmallImgSrc = string.Format("{0}.180_130.jpg", listCarousePlane[0].Path.ToString());

                            ViewData["Planeimgsrc"] = temp.Path.ToString();
                            ViewData["roomtypeimgsrcName"] = house.Name.ToString();
                            ViewData["roomtypeimgsrcstyle"] = string.Format(" style=\"position: absolute; cursor: pointer; z-index: 99; left: {0}px; top: {1}px;\"", house.CoordX - 5, house.CoordY + 8);
                        }
                    }
                }

                //======================================================
                //户型图
                List<TXCommons.PictureModular.PremisesPictureInfo> listCarouseROOMTYPE = new List<TXCommons.PictureModular.PremisesPictureInfo>();
                listCarouseROOMTYPE = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.ROOMTYPE.ToString(), model.CityId);
                if (null != listCarouseROOMTYPE && listCarouseROOMTYPE.Count > 0)
                {
                    var temp = listCarouseROOMTYPE.Where(t => t.ID == house.RId).FirstOrDefault();
                    if (temp != null)
                    {
                        ViewData["roomtypeimgsrc"] = temp.Path.ToString();
                    }
                }
                //=======================================================
            }

            #endregion

            //浏览人次
            var viewCount = TXCommons.Redis.GetOneViewCountValue(string.Format("h_{0}", houseid), FunctionTypeEnum.NewHouseFocusHouseCount, model.CityId);
            ViewData["viewCount"] = viewCount;
            premiseshouse.ViewCount = Util.ToInt(viewCount);

            //第1：如果没有参加活动，或者 （参加的活动已经结束并或到期且房源还处于在售状态）则只显示房源信息部分
            if (actId == 0 || (premiseshouse.SalesStatus == 2 && premiseshouse.ActivityState == 2))
            {
                return PartialView("~/Views/Shared/_HouseDetail.ascx", premiseshouse);
            }
            //第2：如果参加了活动，则需要显示活动信息和房源信息
            else
            {
                //第2.1：获取用户登录状态
                ViewData["IsLogin"] = this.IsLogin;
                //第2.2：获取活动信息
                TXOrm.Activity _activity = _premisesbll.GetHouseActivityById(actId);
                ViewData["activity"] = _activity;

                //获取竞价的出价信息
                List<TXOrm.BidPrice> _bidPrice = _premisesbll.GetBidPriceList((int)premiseshouse.ActivityId, premiseshouse.HouseId);

                //List<TXOrm.BidPrice> _bidPrice = _premisesbll.GetBidPriceListTXCommons.PictureModular.premiseshouse.ActivityId, premiseshouse.HouseId);
                ViewData["BidPrice"] = _bidPrice;

                //登录用户信息
                var userInfo = GetUserInfo;
                ViewData["UserId"] = userInfo != null ? userInfo.Id : 0;
                ViewData["realName"] = userInfo != null ? userInfo.RealName : "";
                ViewData["mobile"] = userInfo != null ? userInfo.Mobile : "";
                ViewData["code"] = userInfo != null ? userInfo.IdCard : "";
                ViewData["email"] = userInfo != null ? userInfo.Email : "";
                ViewData["qq"] = userInfo != null ? userInfo.QQ : "";


                //第2.3：根据参加活动的类别来调研不同的活动信息视图
                switch (premiseshouse.ActivityType)
                {
                    case 3://排号活动房
                        return PartialView("~/Views/Shared/_PHHouseDetail.ascx", premiseshouse);
                    case 4://阶梯价格活动房
                        return PartialView("~/Views/Shared/_TGHouseDetail.ascx", premiseshouse);
                    case 5://竞价活动房
                        //是否支付保证金
                        ViewData["payBond"] = false;

                        if (userInfo != null)
                        {
                            //查看用户是否有该活动已支付完成的出价订单
                            ViewData["payBond"] = _activitysbll.GetActivityOrderByUserID(userInfo.Id, actId).Where(o => o.BondStatus == 1).Count() > 0;
                        }

                        ArrayList arr = _activitysbll.GetActivityBidNum(actId);
                        if (arr == null || arr.Count != 2)
                        {
                            arr = new ArrayList();
                            arr.Add(0);
                            arr.Add(0);
                        }
                        arr.Add(viewCount);
                        ViewData["bidNum"] = arr;

                        return PartialView("~/Views/Shared/_JJHouse.ascx", premiseshouse);
                    case 6://砍价活动房
                        //是否支付保证金
                        ViewData["payBond"] = false;

                        if (userInfo != null)
                        {
                            //查看用户是否有该活动已支付完成的出价订单
                            ViewData["payBond"] = _activitysbll.GetActivityOrderByUserID(userInfo.Id, actId).Where(o => o.BondStatus == 1).Count() > 0;
                        }

                        arr = _activitysbll.GetActivityBidNum(actId);
                        if (arr == null || arr.Count != 2)
                        {
                            arr = new ArrayList();
                            arr.Add(0);
                            arr.Add(0);
                        }
                        arr.Add(viewCount);
                        ViewData["bidNum"] = arr;

                        return PartialView("~/Views/Shared/_KJHouse.ascx", premiseshouse);
                    case 7://秒杀活动房
                        return PartialView("~/Views/Shared/_MSHouseDetail.ascx", premiseshouse);
                    case 8://一口价活动房
                        return PartialView("~/Views/Shared/_YKJHouseDetail.ascx", premiseshouse);
                    default:
                        return PartialView("~/Views/Shared/_HouseDetail.ascx", premiseshouse);
                }
            }
        }

        /// <summary>
        /// 出价列表分页
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="HouseId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        public ActionResult BidPricesPage(int ActivityId, int HouseId, int pageIndex, int type)
        {
            UserInfo user = GetUserInfo;
            int pageSize = 10;//每页大小
            int totalCount = 0;//总记录数
            List<TXOrm.BidPrice> _jjBidPrices = _premisesbll.GetBidPriceList(ActivityId, HouseId, pageIndex, pageSize, out totalCount, type, user == null ? 0 : user.Id);
            int pageCount = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
            ViewData["pageCount"] = pageCount;
            ViewData["BidPrice"] = _jjBidPrices;
            ViewData["pageIndex"] = pageIndex;
            if (_jjBidPrices.Count != 0)
            {
                StringBuilder sb = new StringBuilder("{list:[");
                int count = _jjBidPrices.Count;
                for (int i = 0; i < count; i++)
                {
                    sb.Append("{id:'").Append(_jjBidPrices[i].Id).Append("',BidUserPrice:'");
                    sb.Append((double)_jjBidPrices[i].BidUserPrice).Append("',CreateTime:'").Append(_jjBidPrices[i].CreateTime);
                    sb.Append("',BidUserMobile:'").Append(_jjBidPrices[i].BidUserMobile).Append("',Status:'").Append(_jjBidPrices[i].Status + "'}");
                    if (i < _jjBidPrices.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append("],page:{pagecount:").Append(pageCount).Append(",index:").Append(pageIndex).Append(",pagesize:").Append(pageSize).Append(",totalcount:").Append(totalCount).Append("}}");
                return Content(sb.ToString());
            }
            else
            {
                return Content("0");
            }
        }
        #endregion

        #region 楼盘户型图

        /// <summary>
        /// 取户型图集合（公用）
        /// Author:sunlin
        /// </summary>
        /// <param name="mianji">面积</param>
        /// <param name="tingshi">厅室</param>
        /// <param name="loudong">楼栋</param>
        /// <param name="premisesId">楼盘ID</param>
        /// <param name="model">楼盘信息</param>
        /// <param name="sizeChartList"></param>
        private void GetChartList(string mianji, string tingshi, string loudong, int premisesId, out List<IndexPremisesPic> sizeChartList)
        {
            sizeChartList = null;//新集合
            //Lucene地址
            string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(ViewData["CityId"]));

            #region 全部
            ////如果是全部，循环R与L做比对，把面积厅室保存到集合中
            if (mianji == "" && tingshi == "" && loudong == "")
            {
                #region 取Lucene数据
                var index = new IndexRoomConditionInfo();
                index.CityId = Util.ToString(ViewData["CityId"]);
                index.PremisesID = premisesId.ToString();
                index.PageSize = "9999";
                var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
                string premises_xml_url = url + "?condition=" + condtion;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        List<IndexRoom> listform = new List<IndexRoom>();
                        IndexRoom indexRoom = new IndexRoom();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            indexRoom = new IndexRoom();
                            for (int k = 0; k < dt.Columns.Count; k++)
                            {
                                //获取属性值需要指定属性名
                                PropertyInfo ProInfo = indexRoom.GetType().GetProperty(dt.Columns[k].ColumnName);
                                ProInfo.SetValue(indexRoom, Convert.ToString(dr[k]), null);
                            }
                            if (indexRoom.PicUrl != "")
                                listform.Add(indexRoom);
                        }
                        List<IndexPremisesPic> picList = new List<IndexPremisesPic>();
                        foreach (IndexRoom ir in listform)
                        {
                            IndexPremisesPic premisesPic = new IndexPremisesPic();
                            premisesPic.PicType = "hxt";
                            premisesPic.Hits = TXCommons.Redis.GetOneViewCountValue(string.Format("sizechart_{1}_{0}", ir.RID, ir.BuildingArea), FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"])).ToString();
                            premisesPic.CreateTime = ir.CreateTime.ToString();
                            premisesPic.PicUrl = ir.PicUrl;
                            premisesPic.PicDesc = ir.PicDesc;
                            premisesPic.PicID = Util.ToInt(ir.RID);
                            premisesPic.PremisesID = premisesId.ToString();
                            premisesPic.Title = ir.Title;
                            premisesPic.DetailUrl = ImageDetailUrl("hxt", premisesId.ToString(), ir.RID.ToString(), "", ir.HouseID);
                            premisesPic.BuildingArea = ir.BuildingArea;
                            premisesPic.HouseID = ir.HouseID;
                            premisesPic.Hall = ir.Hall;
                            premisesPic.Room = ir.Room;
                            premisesPic.Toilet = ir.Toilet;
                            picList.Add(premisesPic);
                        }
                        sizeChartList = picList;
                        SetHits(sizeChartList);
                    }
                }
                else
                {
                    sizeChartList = null;
                }
                #endregion
                ViewData["type"] = "";
                ViewData["albumType"] = "hxt";
            }
            #endregion

            #region 楼栋查询
            if (loudong != "")
            {
                //取接口户型图集合L
                string premises_xml_url = GetSizeChartList("d", loudong, premisesId, url);
                sizeChartList = XMLtoList(sizeChartList, premises_xml_url);
                // 点击量
                SetHits(sizeChartList);
                ViewData["type"] = "d";
            }
            #endregion

            #region 面积查询
            if (mianji != "")
            {
                //取接口户型图集合L
                string premises_xml_url = GetSizeChartListByMj(mianji, premisesId, url);
                sizeChartList = XMLtoList(sizeChartList, premises_xml_url);
                //点击量
                SetHits(sizeChartList);
                //动态查询面积标签
                ViewData["type"] = "m";
            }
            #endregion

            #region 厅室查询
            if (tingshi != "")
            {
                ViewData["where"] = tingshi;
                //取接口户型图集合L
                string premises_xml_url = GetSizeChartList("r", tingshi, premisesId, url);
                sizeChartList = XMLtoList(sizeChartList, premises_xml_url);
                //点击量
                SetHits(sizeChartList);
                ViewData["type"] = "r";
            }
            #endregion

        }

        /// <summary>
        /// 取户型图集合（面积）
        /// Author:sunlin
        /// </summary>
        /// <param name="mianji">面积</param>
        /// <param name="premisesId">楼盘ID</param>
        /// <param name="url">Lucene地址</param>
        /// <returns></returns>
        private string GetSizeChartListByMj(string mianji, int premisesId, string url)
        {
            var index = new IndexRoomConditionInfo();
            index.CityId = Util.ToString(ViewData["CityId"]);
            index.PremisesID = premisesId.ToString();
            index.PageSize = "9999";
            #region 注释
            switch (Regex.Match(mianji, @"\d{1,}").ToString())
            {//50平方米以下、51-70平方米、71-90平方米、91-120平方米、121-150平方米、151平方米以上
                case "1":
                    index.BeginArea = "0";
                    index.EndArea = "50";
                    break;
                case "2":
                    index.BeginArea = "51";
                    index.EndArea = "70";
                    break;
                case "3":
                    index.BeginArea = "71";
                    index.EndArea = "90";
                    break;
                case "4":
                    index.BeginArea = "91";
                    index.EndArea = "120";
                    break;
                case "5":
                    index.BeginArea = "121";
                    index.EndArea = "150";
                    break;
                case "6":
                    index.BeginArea = "151";
                    index.EndArea = "1000000000";
                    break;
            }
            #endregion
            var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
            string premises_xml_url = url + "?condition=" + condtion;//查询生成xml文件
            return premises_xml_url;
        }

        /// <summary>
        /// 取接口户型图集合
        /// Author:sunlin
        /// </summary>
        /// <param name="type">d:楼栋,r:厅室</param>
        /// <param name="value">条件值</param>
        /// <param name="premisesId">楼盘ID</param>
        /// <param name="url">Lucene地址</param>
        /// <returns></returns>
        private string GetSizeChartList(string type, string value, int premisesId, string url)
        {
            var index = new IndexRoomConditionInfo();
            index.CityId = Util.ToString(ViewData["CityId"]);
            index.PremisesID = premisesId.ToString();
            if (type == "d")
            {
                if (Regex.Match(value, @"\d{1,}").ToString() != "0")
                    index.BuildingId = Regex.Match(value, @"\d{1,}").ToString();
            }
            if (type == "r")
            {
                if (Regex.Match(value, @"\d{1,}").ToString() != "0")
                    index.Room = Regex.Match(value, @"\d{1,}").ToString();
            }
            index.PageSize = "9999";
            var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
            string premises_xml_url = url + "?condition=" + condtion;//查询生成xml文件
            return premises_xml_url;
        }

        /// <summary>
        /// XmlToList
        /// Author:sunlin
        /// </summary>
        /// <param name="sizeChartList">户型图集合</param>
        /// <param name="premises_xml_url">Lucene地址</param>
        /// <returns></returns>
        private List<IndexPremisesPic> XMLtoList(List<IndexPremisesPic> sizeChartList, string premises_xml_url)
        {
            DataSet ds = new DataSet("Results");
            ds.ReadXml(premises_xml_url);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DataRow PageRow = ds.Tables[0].Rows[0];
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    List<IndexRoom> listform = new List<IndexRoom>();
                    IndexRoom indexRoom = new IndexRoom();
                    //PropertyInfo[] ProInfos = sf.GetType().GetProperties();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        indexRoom = new IndexRoom();
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            //获取属性值需要指定属性名
                            PropertyInfo ProInfo = indexRoom.GetType().GetProperty(dt.Columns[k].ColumnName);
                            ProInfo.SetValue(indexRoom, Convert.ToString(dr[k]), null);
                        }
                        if (indexRoom.PicUrl != "")
                            listform.Add(indexRoom);
                    }
                    List<IndexPremisesPic> picList = new List<IndexPremisesPic>();
                    foreach (IndexRoom ir in listform)
                    {
                        IndexPremisesPic premisesPic = new IndexPremisesPic();
                        premisesPic.PicType = "hxt";
                        premisesPic.Hits = TXCommons.Redis.GetOneViewCountValue(string.Format("sizechart_{1}_{0}", ir.RID, ir.BuildingArea), FunctionTypeEnum.NewHousePropertyAlbum, Util.ToInt(ViewData["cityId"])).ToString();
                        premisesPic.CreateTime = ir.CreateTime.ToString();
                        premisesPic.PicUrl = ir.PicUrl;
                        premisesPic.PicDesc = ir.PicDesc;
                        premisesPic.PicID = Util.ToInt(ir.RID);
                        premisesPic.PremisesID = ir.PremisesID.ToString();
                        premisesPic.Title = ir.Title;
                        premisesPic.DetailUrl = ImageDetailUrl("hxt", ir.PremisesID.ToString(), ir.RID.ToString(), "", ir.HouseID);
                        premisesPic.BuildingArea = ir.BuildingArea;
                        premisesPic.HouseID = ir.HouseID;
                        premisesPic.Hall = ir.Hall;
                        premisesPic.Room = ir.Room;
                        premisesPic.Toilet = ir.Toilet;
                        premisesPic.BuildingId = ir.BuildingId;
                        premisesPic.BuildingName = ir.BuildingName;
                        premisesPic.BuildingNameType = ir.BuildingNameType;
                        picList.Add(premisesPic);
                    }
                    sizeChartList = picList;
                    SetHits(sizeChartList);
                }
            }
            else
            {
                sizeChartList = null;
            }
            return sizeChartList;
        }

        /// <summary>
        /// 取厅室标签
        /// Author:sunlin
        /// </summary>
        /// <param name="premisesId">楼盘ID</param>
        ///<param name="ts">厅室</param>
        ///<returns></returns>
        public JsonResult TsTab(string ts, string premisesId)
        {
            ViewData["albumType"] = "hxt";
            string str = "";
            string s = "";
            string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(ViewData["CityId"]));
            #region 取全部数据
            List<IndexPremisesPic> sizeChartList = new List<IndexPremisesPic>();
            string premises_xml_url = GetSizeChartList("r", "", Util.ToInt(premisesId), url);
            sizeChartList = XMLtoList(sizeChartList, premises_xml_url);
            #endregion
            #region 取条件数据
            List<IndexPremisesPic> sizeChartListAll = new List<IndexPremisesPic>();
            string all = GetSizeChartList("r", ts, Util.ToInt(premisesId), url);
            sizeChartListAll = XMLtoList(sizeChartListAll, all);
            #endregion
            List<int> arr = new List<int>();
            if (sizeChartList != null)
            {
                #region 取厅室标签
                for (int i = 0; i < sizeChartList.Count; i++)
                {
                    if (i == 0)
                    {
                        str += sizeChartList[i].Room + ",";
                        arr.Add(Util.ToInt(sizeChartList[i].Room));
                    }
                    s = str; int k = 0;
                    for (int j = 0; j < s.TrimEnd(',').Split(',').Length; j++)
                    {
                        if (str.Split(',')[j] != sizeChartList[i].Room)
                        {
                            k = 1;
                        }
                        else
                        {
                            k = 0;
                            break;
                        }
                    }
                    if (k == 1)
                    {
                        str += sizeChartList[i].Room + ",";
                        arr.Add(Util.ToInt(sizeChartList[i].Room));
                    }
                }
                arr.Sort();
                #endregion

                for (int i = 0; i < sizeChartListAll.Count; i++)
                {
                    sizeChartListAll[i].DetailUrl = NHWebUrl.ImageDetailsUrl("hxt", sizeChartListAll[i].PremisesID, sizeChartListAll[i].PicID.ToString(), "-r" + sizeChartListAll[i].Room, sizeChartListAll[i].HouseID);
                }
                if (ts == "0")//无条件（默认第一个标签）
                    sizeChartListAll = sizeChartListAll.Where(t => t.Room == arr[0].ToString()).ToList();
                else//有条件
                    sizeChartListAll = sizeChartListAll.Where(t => t.Room == ts).ToList();
                str = string.Join(",", arr);
                return Json(new { str, sizeChartListAll });
            }
            else
            {
                return Json(new { str = "", sizeChartListAll });
            }
        }

        /// <summary>
        /// 取楼栋标签
        /// Author:sunlin
        /// </summary>
        /// <param name="ld">楼栋</param>
        /// <param name="premisesId">楼盘ID</param>
        /// <returns></returns>
        public JsonResult LdTab(string ld, string premisesId)
        {
            string str = "";
            string s = "";
            string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(ViewData["CityId"]));
            #region 取全部数据
            List<IndexPremisesPic> sizeChartList = new List<IndexPremisesPic>();
            string premises_xml_url = GetSizeChartList("d", "", Util.ToInt(premisesId), url);
            sizeChartList = XMLtoList(sizeChartList, premises_xml_url);
            #endregion
            #region 取楼栋条件数据
            List<IndexPremisesPic> sizeChartListAll = new List<IndexPremisesPic>();
            string all = GetSizeChartList("d", ld, Util.ToInt(premisesId), url);
            sizeChartListAll = XMLtoList(sizeChartListAll, all);
            #endregion
            if (sizeChartList != null)
            {
                #region 取楼栋标签
                for (int i = 0; i < sizeChartList.Count; i++)
                {
                    if (i == 0)
                    {
                        str += sizeChartList[i].BuildingId + ":" + sizeChartList[i].BuildingName + ":" + sizeChartList[i].BuildingNameType + ",";
                    }
                    s = str; int k = 0;
                    for (int j = 0; j < s.TrimEnd(',').Split(',').Length; j++)
                    {
                        if (str.Split(',')[j].Split(':')[0] != sizeChartList[i].BuildingId)
                        {
                            k = 1;
                        }
                        else
                        {
                            k = 0;
                            break;
                        }

                    } if (k == 1)
                    {
                        str += sizeChartList[i].BuildingId + ":" + sizeChartList[i].BuildingName + ":" + sizeChartList[i].BuildingNameType + ",";
                    }
                }
                str = str.TrimEnd(',');
                #endregion
                for (int i = 0; i < sizeChartListAll.Count; i++)
                {
                    sizeChartListAll[i].DetailUrl = NHWebUrl.ImageDetailsUrl("hxt", sizeChartListAll[i].PremisesID, sizeChartListAll[i].PicID.ToString(), "-d" + sizeChartListAll[i].Room, sizeChartListAll[i].HouseID);
                }
                if (ld == "0")//无条件（取第一个楼栋条件数据）
                    sizeChartListAll = sizeChartListAll.Where(t => t.BuildingId == str.Split(',')[0].Split(':')[0].ToString()).ToList();
                return Json(new { str, sizeChartListAll });
            }
            else
            {
                return Json(new { str = "", sizeChartListAll });
            }
        }

        /// <summary>
        /// 取面积标签
        /// Author:sunlin
        /// </summary>
        /// <param name="mj">面积</param>
        /// <param name="premisesId">楼盘ID</param>
        /// <returns></returns>
        public JsonResult MjTab(string mj, string premisesId)
        {
            string strD = "0,0,0,0,0,0";
            string[] s = strD.Split(',');
            string mjArr = "50平方米以下:1, 51-70平方米:2, 71-90平方米:3, 91-120平方米:4, 121-150平方米:5, 151平方米以上:6";
            string[] m = mjArr.Split(',');
            //Lucene地址
            string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(ViewData["CityId"]));
            #region 取全部数据
            List<IndexPremisesPic> sizeChartList = new List<IndexPremisesPic>();
            string premises_xml_url = GetSizeChartListByMj("", Util.ToInt(premisesId), url);
            sizeChartList = XMLtoList(sizeChartList, premises_xml_url);
            #endregion
            #region 取面积条件数据
            List<IndexPremisesPic> sizeChartListAll = new List<IndexPremisesPic>();
            string all = GetSizeChartListByMj(mj, Util.ToInt(premisesId), url);
            sizeChartListAll = XMLtoList(sizeChartListAll, all);
            #endregion
            var str = "";
            if (sizeChartList != null)
            {
                #region 取面积标签
                for (int i = 0; i < sizeChartList.Count; i++)
                {
                    if (sizeChartList[i].BuildingArea != "")
                    {
                        if (Util.ToDouble(sizeChartList[i].BuildingArea, 0) <= 50)
                        {
                            s[0] = "1";
                        }
                        if (Util.ToDouble(sizeChartList[i].BuildingArea, 0) > 50 && Util.ToDouble(sizeChartList[i].BuildingArea, 0) <= 70)
                        {
                            s[1] = "1";
                        }
                        if (Util.ToDouble(sizeChartList[i].BuildingArea, 0) > 70 && Util.ToDouble(sizeChartList[i].BuildingArea, 0) <= 90)
                        {
                            s[2] = "1";
                        }
                        if (Util.ToDouble(sizeChartList[i].BuildingArea, 0) > 90 && Util.ToDouble(sizeChartList[i].BuildingArea, 0) <= 120)
                        {
                            s[3] = "1";
                        }
                        if (Util.ToDouble(sizeChartList[i].BuildingArea, 0) > 120 && Util.ToDouble(sizeChartList[i].BuildingArea, 0) <= 150)
                        {
                            s[4] = "1";
                        }
                        if (Util.ToDouble(sizeChartList[i].BuildingArea, 0) > 150)
                        {
                            s[5] = "1";
                        }
                    }
                }
                string x = "";
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != "0")
                        x += m[i] + ",";
                }
                str = x.TrimEnd(',');
                #endregion
                for (int i = 0; i < sizeChartListAll.Count; i++)
                {
                    sizeChartListAll[i].DetailUrl = NHWebUrl.ImageDetailsUrl("hxt", sizeChartListAll[i].PremisesID, sizeChartListAll[i].PicID.ToString(), "-m" + sizeChartListAll[i].Room, sizeChartListAll[i].HouseID);
                }
                if (mj == "0")//无条件（取第一个面积数据）
                {
                    sizeChartListAll = sizeChartListAll.Where(t => GetBuildingArea(t.BuildingArea) == str.Split(',')[0].Split(':')[1].ToString()).ToList();
                }
                return Json(new { str, sizeChartListAll });
            }
            else
            {
                return Json(new { str, sizeChartListAll });
            }

        }

        /// <summary>
        /// 建筑面积
        /// Author:sunlin
        /// </summary>
        /// <param name="buildingArea">建筑面积</param>
        /// <returns></returns>
        private string GetBuildingArea(string buildingArea)
        {
            if (Util.ToDouble(buildingArea, 0) <= 50)
            {
                return "1";
            }
            else if (Util.ToDouble(buildingArea, 0) > 50 && Util.ToDouble(buildingArea, 0) <= 70)
            {
                return "2";
            }
            else if (Util.ToDouble(buildingArea, 0) > 70 && Util.ToDouble(buildingArea, 0) <= 90)
            {
                return "3";
            }
            else if (Util.ToDouble(buildingArea, 0) > 90 && Util.ToDouble(buildingArea, 0) <= 120)
            {
                return "4";
            }
            else if (Util.ToDouble(buildingArea, 0) > 120 && Util.ToDouble(buildingArea, 0) <= 150)
            {
                return "5";
            }
            else if (Util.ToDouble(buildingArea, 0) > 150)
            {
                return "6";
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 设置点击量
        /// Author:sunlin
        /// </summary>
        /// <param name="sizeChartList"></param>
        private void SetHits(List<IndexPremisesPic> sizeChartList)
        {
            #region 注释
            //if (sizeChartList != null && sizeChartList.Count > 0)
            //{
            //    int b = sizeChartList.Count % 20;//10
            //    int a = sizeChartList.Count / 20;//0
            //    List<string> count = null;
            //    //户型图大于20条
            //    if (sizeChartList.Count > 20)
            //    {
            //        for (int i = 0; i < a + 1; i++)//循环几次
            //        {
            //            var keys = "";
            //            //循环Keys
            //            for (int j = i * 20; j < ((sizeChartList.Count > ((i * 20) + 20)) ? ((i * 20) + 20) : (i * 20) + b); j++)
            //            {
            //                keys += "sizechart_" + sizeChartList[j].BuildingArea + "_" + sizeChartList[j].RID + ",";
            //            }
            //            //取点击量集合
            //            count = TXCommons.Redis.GetValues<List<string>>(FunctionTypeEnum.NewHouseSizeChartViewCount, 0, keys.TrimEnd(','));
            //            if (count != null)
            //            {
            //                int m = 0;
            //                for (int k = i * 20; k < ((sizeChartList.Count > ((i * 20) + 20)) ? ((i * 20) + 20) : (i * 20) + b); k++)
            //                {
            //                    m++;
            //                    if (count[m] != "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1#2B#B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee#2B##2B#999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9#2B#fB8/Ij76N3/lwe79j/6fAAAA//#2B#qiwZXBwAAAA==" && count[i + 1] != null)
            //                    {
            //                        sizeChartList[k].Hits = count[m].ToString() == "" ? "0" : count[m].ToString();
            //                    }
            //                    else
            //                    {
            //                        TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"]), "sizechart_" + sizeChartList[k].BuildingArea + "_" + sizeChartList[k].RID).ToString();
            //                        sizeChartList[k].Hits = "1";
            //                    }

            //                }
            //            }
            //            else
            //            {
            //                for (var m = 0; m < sizeChartList.Count; m++)
            //                {
            //                    sizeChartList[m].Hits = "0";
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {//户型图小于等于20条 
            //        var keys = "";
            //        for (int i = 0; i < sizeChartList.Count; i++)
            //        {
            //            keys += "sizechart_" + sizeChartList[i].BuildingArea + "_" + sizeChartList[i].RID + ",";
            //        }
            //        count = TXCommons.Redis.GetValues<List<string>>(FunctionTypeEnum.NewHouseSizeChartViewCount, 0, keys.TrimEnd(','));
            //        if (count != null)
            //        {
            //            for (int i = 0; i < sizeChartList.Count; i++)
            //            {
            //                if (count[i + 1] == "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1#2B#B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee#2B##2B#999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9#2B#fB8/Ij76N3/lwe79j/6fAAAA//#2B#qiwZXBwAAAA=="||count[i + 1] ==null)
            //                {
            //                    TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"]), "sizechart_" + sizeChartList[i].BuildingArea + "_" + sizeChartList[i].RID).ToString();
            //                    sizeChartList[i].Hits = "1";
            //                }
            //                else
            //                {
            //                    sizeChartList[i].Hits = count[i + 1].ToString() == "" ? "0" : count[i + 1].ToString();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (var i = 0; i < sizeChartList.Count; i++)
            //            {
            //                sizeChartList[i].Hits = "0";
            //            }
            //        }
            //    }

            //}
            #endregion
            double count = 0;
            if (sizeChartList != null && sizeChartList.Count > 0)
            {
                var key = "";
                for (int i = 0; i < sizeChartList.Count; i++)
                {
                    key = "sizechart_" + sizeChartList[i].BuildingArea + "_" + sizeChartList[i].PicID;
                    count = TXCommons.Redis.GetOneViewCountValue(key, FunctionTypeEnum.NewHouseSizeChartViewCount, 0);
                    sizeChartList[i].Hits = count == 0 ? "0" : count.ToString();
                }
            }
        }

        /// <summary>
        /// 取户型图（与房源无关联）
        /// </summary>
        /// <param name="premisesId"></param>
        /// <returns></returns>
        public void GetSizeChartRedis(string premisesId, out List<IndexPremisesPic> indexPremisesPic)
        {
            var model = _premisesbll.GetPremisesbyId(Util.ToInt(premisesId));
            var picture = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, PremisesPictureType.ROOMTYPE.ToString(), 0);
            List<IndexPremisesPic> picList = new List<IndexPremisesPic>();
            if (null != picture && picture.Count > 0)
            {
                picture = picture.OrderByDescending(t => t.CreateTime).ToList();
                foreach (PremisesPictureInfo t in picture)
                {
                    IndexPremisesPic premisesPic = new IndexPremisesPic();
                    premisesPic.PicType = "hxt";
                    premisesPic.Hits = TXCommons.Redis.GetOneViewCountValue(string.Format("sizechart_{1}_{0}", t.ID, 0), FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"])).ToString();
                    premisesPic.CreateTime = t.CreateTime.ToString();
                    premisesPic.PicUrl = t.Path;
                    premisesPic.PicDesc = t.Desc;
                    premisesPic.PicID = t.ID;
                    premisesPic.PremisesID = premisesId.ToString();
                    premisesPic.Title = t.Title;
                    premisesPic.DetailUrl = ImageDetailUrl("hxt", premisesId.ToString(), t.ID.ToString(), "", "0");
                    premisesPic.BuildingArea = "0";
                    premisesPic.HouseID = "0";
                    premisesPic.Hall = "0";
                    premisesPic.Room = "0";
                    premisesPic.Toilet = "0";
                    premisesPic.BuildingId = "0";
                    premisesPic.BuildingName = "";
                    premisesPic.BuildingNameType = "";
                    picList.Add(premisesPic);
                }
            }
            indexPremisesPic = picList;
        }

        /// <summary> 
        /// 前台取户型图（Json）（与房源无关联）
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="indexPremisesPic"></param>
        /// <returns></returns>
        public JsonResult RedisSizeChart(string premisesId)
        {
            List<IndexPremisesPic> indexPremisesPic = new List<IndexPremisesPic>();
            GetSizeChartRedis(premisesId, out indexPremisesPic);
            return Json(indexPremisesPic);
        }

        #endregion

        #region 楼盘相册

        /// <summary>
        /// 楼盘相册
        /// author：sunlin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Album(string id)
        {
            #region SEO处理
            SEO seo = new SEO();
            ViewData["Seo"] = seo.SeoAlbum(new SEOModel(), ViewData["cityName"] as string);
            #endregion
            var key = id.Split('-');
            int premisesId = Regex.Match(id, @"-\d{1,}").ToString() == "" ? 0 : Util.ToInt(Regex.Match(Regex.Match(id, @"-\d{1,}").ToString(), @"\d{1,}").ToString());//楼盘Id
            var albumType = Regex.Match(id, @"[a-z]{1,}").ToString();//相册分类
            ViewData["albumType"] = albumType == "" ? "xgt" : albumType;
            var page = Regex.Match(id, @"-i\d{1,}").ToString();//当前页
            int currentPage = page == "" ? 1 : Util.ToInt(Regex.Match(page, @"\d{1,}"));
            string ts = Regex.Match(id, @"-r\d{1,}").ToString();//取厅室
            ViewData["TS"] = Regex.Match(ts, @"\d{1,}").ToString();
            TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
            if (null != model && model.Id > 0)
            {
                #region  获取广告图片
                //http://nhindex.kuaiyoujia.com/premises/index.ashx?condition=beijing-xinfang-quyu-a297
                string cityId = ViewData["cityId"] as string;//取城市
                if (string.IsNullOrWhiteSpace(cityId))
                {
                    cityId = "253";
                }
                PagedList<IndexPremises> searchList = null;
                string city = ViewData["cityPinyin"] as string;
                string Id = string.Format("{0}-xinfang-quyu-a{1}", city, model.Id);
                //Id:条件，地址，城市
                searchList = SearchBll.NHouseListResult(Id.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                ViewData["ADPictureUrl"] = "";
                //开发商LOGO图
                ViewData["DeveloperLOGOUrl"] = "";
                ViewData["IsRecommend"] = "0";
                if (null != searchList && searchList.Count > 0)
                {
                    if (null != searchList[0])
                    {
                        IndexPremises indexpremisestemp = searchList[0];
                        ViewData["ADPictureUrl"] = indexpremisestemp.ADPictureUrl;
                        //开发商LOGO图
                        ViewData["DeveloperLOGOUrl"] = indexpremisestemp.DeveloperLOGOUrl;
                        ViewData["IsRecommend"] = indexpremisestemp.IsRecommend;
                    }
                }
                #endregion
            }

            #region 浏览量、排名

            //浏览量
            ViewData["ViewCount"] = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", premisesId), FunctionTypeEnum.NewHouseViewCount, model.CityId);

            //排名
            var Rank = TXCommons.Redis.GetValue<int>(string.Format("NewHouseRank_{0}", model.Id), FunctionTypeEnum.NewHousePropertyRank, model.CityId);
            ViewData["Rank"] = Rank;

            #endregion

            return View(model);
        }

        /// <summary>
        /// 取楼盘相册
        /// author：sunlin
        /// </summary>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="picturetype">相册类型</param>
        /// <param name="isallflag">是否是相册调取</param>
        /// <param name="model">楼盘实体</param>
        /// <returns></returns>
        public List<IndexPremisesPic> getAlbumlist(int premisesId, string picturetype, bool isallflag, string mj, string ts, string ld)
        {
            ViewData["albumType"] = picturetype;
            List<IndexPremisesPic> picList = new List<IndexPremisesPic>();
            var model = _premisesbll.GetPremisesbyId(premisesId);
            string picType = "";
            if (null != model && model.Id > 0)
            {
                //==>xgt效果图|hxt户型图|ght规划图|wzt位置图|ldt楼栋平面图|sjt实景图|jtt交通图
                switch (picturetype)
                {
                    case "xgt":
                        picType = PremisesPictureType.Effect.ToString();// "Effect"效果图;
                        break;
                    case "hxt":
                        picType = PremisesPictureType.ROOMTYPE.ToString();// "ROOMTYPE"户型图;
                        break;
                    case "ght":
                        picType = PremisesPictureType.Plan.ToString();// "Plan"规划图;
                        break;
                    case "wzt":
                        picType = PremisesPictureType.Location.ToString();//"Location"位置图;
                        break;
                    case "ldt":
                        picType = PremisesPictureType.Plane.ToString();//"Plane"楼栋图;
                        break;
                    case "sjt":
                        picType = PremisesPictureType.Scene.ToString();// "Scene"实景图;
                        break;
                    case "jtt":
                        picType = PremisesPictureType.TRAFFIC.ToString();//"TRAFFIC"交通图;
                        break;
                }
                if (picType == PremisesPictureType.ROOMTYPE.ToString())//户型图数据
                {
                    List<IndexPremisesPic> indexRoomList = new List<IndexPremisesPic>();
                    GetChartList(mj, ts, ld, premisesId, out indexRoomList);
                    picList = indexRoomList;
                }
                else//相册其他类型数据
                {
                    #region 取相册图片数据
                    var picture = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(model.InnerCode, true, picType, 0);
                    if (null != picture && picture.Count > 0)
                    {
                        picture = picture.OrderByDescending(t => t.CreateTime).ToList();
                        foreach (PremisesPictureInfo t in picture)
                        {
                            IndexPremisesPic premisesPic = new IndexPremisesPic();
                            premisesPic.PicType = picturetype;
                            premisesPic.Hits = TXCommons.Redis.GetOneViewCountValue(string.Format("lp-" + picturetype + "-{0}", t.ID), FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"])).ToString();
                            premisesPic.CreateTime = t.CreateTime.ToString();
                            premisesPic.PicUrl = t.Path;
                            premisesPic.PicDesc = t.Desc;
                            premisesPic.PicID = t.ID;
                            premisesPic.PremisesID = premisesId.ToString();
                            premisesPic.Title = t.Title;
                            premisesPic.DetailUrl = ImageDetailUrl(picturetype, premisesId.ToString(), t.ID.ToString(), "", "");
                            picList.Add(premisesPic);
                        }
                    }
                    #endregion
                }
            }
            return picList;
        }

        /// <summary>
        /// 前台异步调用取楼盘相册
        /// author：sunlin
        /// </summary>
        /// <param name="premisesId"></param>
        /// <param name="picturetype"></param>
        /// <param name="isallflag"></param>
        /// <param name="mj"></param>
        /// <param name="ts"></param>
        /// <param name="ld"></param>
        /// <returns></returns>
        public JsonResult AlbumList(int premisesId, string picturetype, bool isallflag, string mj, string ts, string ld)
        {
            return Json(getAlbumlist(premisesId, picturetype, isallflag, mj, ts, ld));
        }

        /// <summary>
        /// 取图片详情页Url
        /// author：sunlin
        /// </summary>
        /// <param name="type">图片类型</param>
        /// <param name="premisesId">楼盘Id</param>
        /// <param name="picId">图片ID</param>
        /// <param name="where">条件</param>
        /// <param name="houseID">房源Id(用于户型 图)</param>
        /// <returns></returns>
        private static string ImageDetailUrl(string type, string premisesId, string picId, string where, string houseID)
        {
            return NHWebUrl.ImageDetailsUrl(type, premisesId, picId, where, houseID);
        }

        #endregion

        #region 图片详情页

        /// <summary>
        /// 图片详情页
        /// author：sunlin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ImageDetails(string id)
        {
            var key = Regex.Matches(id, @"\d{1,}");
            int premiseId = Util.ToInt(key[0].ToString());
            int picId = Util.ToInt(key[1].ToString());
            var picType = id.Split('-');
            string type = picType[1].ToString();
            TXOrm.Premis model = null;
            ViewData["type"] = type;
            if (type == "hxt")
            {
                List<IndexPremisesPic> sizeChartList;
                var mianji = Regex.Match(id, @"-m\d{1,}").ToString();//面积
                var tingshi = Regex.Match(id, @"-r\d{1,}").ToString();//厅室
                var loudong = Regex.Match(id, @"-d\d{1,}").ToString();//楼栋
                var page = Regex.Match(id, @"-i\d{1,}").ToString();//当前页
                var houseid = Regex.Match(Regex.Match(id, @"-h\d{1,}").ToString(), @"\d{1,}").ToString();//房源ID
                GetChartList(mianji, tingshi, loudong, premiseId, out sizeChartList);
                if (sizeChartList == null || sizeChartList.Count <= 0)
                {
                    GetSizeChartRedis(premiseId.ToString(), out sizeChartList);
                }
                ViewData["SizeChartList"] = sizeChartList;
                IndexPremisesPic indexRoom = null;
                if (sizeChartList != null)
                {
                    for (int i = 0; i < sizeChartList.Count; i++)
                    {
                        if (picId != 0)
                        {
                            if (picId == sizeChartList[i].PicID)
                            {
                                indexRoom = sizeChartList[i];
                                SetHits(sizeChartList[i].BuildingArea, sizeChartList[i].PicID.ToString(), type);
                                #region SEO处理
                                SEO seo = new SEO();
                                ViewData["Seo"] = seo.SeoSizeChartDetail(new SEOModel(), ViewData["cityName"] as string, sizeChartList[i].Room == "0" ? "" : sizeChartList[i].Room + "室", type);
                                #endregion
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {
                                indexRoom = sizeChartList[i];
                                SetHits(sizeChartList[i].BuildingArea, sizeChartList[i].PicID.ToString(), type);
                                #region SEO处理
                                SEO seo = new SEO();
                                ViewData["Seo"] = seo.SeoSizeChartDetail(new SEOModel(), ViewData["cityName"] as string, sizeChartList[i].Room == "0" ? "" : sizeChartList[i].Room + "室", type);
                                #endregion
                            }
                        }
                    }
                }
                ViewData["type"] = "hxt";
                ViewData["detail"] = indexRoom;
            }
            else
            {
                List<IndexPremisesPic> AlbumList = new List<IndexPremisesPic>();
                //xgt效果图|ght规划图|wzt位置图|ldt楼栋平面图|sjt实景图|jtt交通图
                List<IndexPremisesPic> picList = getAlbumlist(premiseId, type, false, "", "", "");
                foreach (IndexPremisesPic p in picList)
                {
                    p.BuildingArea = "0";
                    p.HouseID = "0";
                }
                #region SEO处理
                SEO seo = new SEO();
                ViewData["Seo"] = seo.SeoSizeChartDetail(new SEOModel(), ViewData["cityName"] as string, "", type);
                #endregion

                ViewData["SizeChartList"] = picList;
                IndexPremisesPic indexRoom = null;
                for (int i = 0; i < picList.Count; i++)
                {
                    if (picList[i].PicID == picId)
                    {
                        //图片浏览量+1 并读取
                        TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"]),
                            string.Format("lp-{0}-{1}", type, picId));
                        indexRoom = picList[i];
                    }
                }
                if (picList.Count > 0 && indexRoom == null)//详情页选择
                {
                    indexRoom = picList[0];
                    //图片浏览量+1 并读取
                    TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"]), string.Format("lp-{0}-{1}", type, picId));
                }
                ViewData["detail"] = indexRoom;
            }
            model = _premisesbll.GetPremisesbyId(premiseId);
            return View(model);
        }

        /// <summary>
        /// 设置户型图点击量
        /// author：sunlin
        /// </summary>
        /// <param name="sizeChartList"></param>
        /// <param name="i"></param>
        private void SetHits(string buildingArea, string rID, string type)
        {
            if (type == "hxt")
            {
                string k = "sizechart_" + Regex.Match(buildingArea, @"\d{1,}").ToString() + "_" + rID;
                TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"]), k).ToString();
            }
            else
            {
                TXCommons.Redis.GetNextSequence(FunctionTypeEnum.NewHouseSizeChartViewCount, Util.ToInt(ViewData["cityId"]),
                                string.Format("lp-{0}-{1}", type, rID));
            }
        }

        #endregion

        #region 新房前台楼盘交通配套页面
        /// <summary>
        /// 新房前台楼盘交通配套页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TrafficFacilities(string id)
        {
            #region SEO处理
            SEO seo = new SEO();
            ViewData["Seo"] = seo.SeoTrafficFacilities(new SEOModel(), ViewData["cityName"] as string);
            #endregion
            int premisesId;
            if (int.TryParse(id, out premisesId))
            {
                TXOrm.Premis model = _premisesbll.GetPremisesbyId(premisesId);
                if (model != null && model.Id > 0)
                {
                    #region  获取广告图片
                    //http://nhindex.kuaiyoujia.com/premises/index.ashx?condition=beijing-xinfang-quyu-a297
                    string cityId = ViewData["cityId"] as string;//取城市
                    if (string.IsNullOrWhiteSpace(cityId))
                    {
                        cityId = "253";
                    }
                    PagedList<IndexPremises> searchList = null;
                    string city = ViewData["cityPinyin"] as string;
                    // string Id = string.Format("http://nhindex.kuaiyoujia.com/premises/index.ashx?condition={0}-xinfang-quyu-a{1}", city, model.Id);
                    //beijing-xinfang-quyu
                    string Id = string.Format("{0}-xinfang-quyu-a{1}", city, model.Id);
                    //Id:条件，地址，城市
                    searchList = SearchBll.NHouseListResult(Id.Replace("я", "·"), "GetLongHouseIndex.ashx", Convert.ToInt32(cityId));
                    ViewData["ADPictureUrl"] = "";
                    //开发商LOGO图
                    ViewData["DeveloperLOGOUrl"] = "";
                    ViewData["IsRecommend"] = "0";
                    if (null != searchList && searchList.Count > 0)
                    {
                        if (null != searchList[0])
                        {
                            IndexPremises indexpremisestemp = searchList[0];
                            ViewData["ADPictureUrl"] = indexpremisestemp.ADPictureUrl;
                            //开发商LOGO图
                            ViewData["DeveloperLOGOUrl"] = indexpremisestemp.DeveloperLOGOUrl;
                            ViewData["IsRecommend"] = indexpremisestemp.IsRecommend;
                        }
                    }
                    #endregion
                    TXOrm.PremisesOther premisesother = _premisesbll.GetPremisesOthersbyId(premisesId);

                    StringBuilder html = new StringBuilder();
                    if (null != premisesother && premisesother.PremisesId > 0)
                    {

                        if (!string.Empty.Equals(premisesother.Bus.Trim()))
                        {
                            html.Append("<dl>");
                            html.Append("<dt class=\"icon_gj1\">公交：</dt>");
                            html.Append("<dd>" + premisesother.Bus + "</dd>");
                            html.Append("</dl>");
                        }
                        if (!string.Empty.Equals(premisesother.Subway.Trim()))
                        {
                            html.Append("<dl>");
                            html.Append("<dt class=\"icon_gj2\">地铁：</dt>");
                            html.Append("<dd>" + premisesother.Subway + "</dd>");
                            html.Append("</dl>");
                        }
                    }
                    ViewData["html"] = html.ToString();

                    //附近楼盘（3公里内）
                    ViewData["NearbyPremises"] = NearbyPremises(model.Lng, model.Lat, "3");


                    #region 浏览量、排名

                    //浏览量
                    ViewData["ViewCount"] = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView_{0}", premisesId), FunctionTypeEnum.NewHouseViewCount, model.CityId);

                    //排名
                    var Rank = TXCommons.Redis.GetValue<int>(string.Format("NewHouseRank_{0}", model.Id), FunctionTypeEnum.NewHousePropertyRank, model.CityId);
                    ViewData["Rank"] = Rank;

                    #endregion

                    return View(model);
                }
                else
                {
                    return Content(NoHouseUrl());
                }
            }
            else
            {
                return Content(NoHouseUrl());
            }
        }
        #endregion

        #region 提交出价信息（新）

        /// <summary>
        /// 提交出价信息
        /// Author:sunlin
        /// 修改:马波讯 2014/01/03
        /// 修改说明：用户已支付保证金，直接添加信息到出价表；未支付，添加订单，支付完成根据订单添加出价信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SubBidPriceInfo(string updatesalesstatus, string status, string bidtype, string price, string realName, string code, string mobile, string qq, string email, int activitiesId, int developerId, int houseId)
        {
            TXCommons.user.cjkjb.webservice.UserInfo userInfo = GetUserInfo;

            int iupdatesalesstatus = 0;
            int istatus = 0;
            int ibidtype = 0;
            int.TryParse(status, out istatus);
            int.TryParse(bidtype, out ibidtype);
            int.TryParse(updatesalesstatus, out iupdatesalesstatus);
            decimal dprice = 0;
            decimal.TryParse(price, out dprice);

            #region 完善用户信息（?完善信息后记着更新cookie）

            if (!string.IsNullOrEmpty(realName))
                userInfo.RealName = realName.Trim();
            if (!string.IsNullOrEmpty(mobile))
                userInfo.Mobile = mobile.Trim();
            if (!string.IsNullOrEmpty(qq))
                userInfo.QQ = qq.Trim();
            if (!string.IsNullOrEmpty(email))
                userInfo.Email = email.Trim();
            //用户信息缺少身份证字段
            if (!string.IsNullOrEmpty(code))
            {
                userInfo.IdCard = code;
            }
            userInfo.Platform = "FC";

            string xmlUser = ComObjOrXml.Serializer(typeof(UserInfo), userInfo);
            NHUserAccount userAccout = new NHUserAccount();
            userAccout.UpdateUser(xmlUser);

            //更新缓存用户信息
            HttpCookie privateAuthCookie = Request.Cookies[GetConfig.PrivateCookie];
            if (privateAuthCookie != null)
            {
                CookieHelper.DelCookie(GetConfig.PrivateCookie);
            }

            string userData = string.Format("{0};{1};FC;{2}", userInfo.Id, userInfo.Guid, userInfo.LoginName);
            TXCommons.CookieHelper.SetCookie(GetConfig.PrivateCookie, userData);
            TXCommons.Redis.AddItemToList<string>(xmlUser, userInfo.Guid, DateTime.Now.AddMinutes(60), FunctionTypeEnum.UserInfo, 0); //缓存用户信息

            #endregion


            TXOrm.BidPrice bidPrice = new TXOrm.BidPrice();
            //活动Id
            bidPrice.ActivitiesId = activitiesId;
            //房源Id
            bidPrice.HouseId = houseId;
            bidPrice.BidUserId = userInfo.Id;//用户Id
            //出价人姓名
            bidPrice.BidUserName = userInfo.LoginName;//用户名
            //出价金额
            bidPrice.BidUserPrice = dprice;
            //出价人qq号码
            bidPrice.BidUserQQ = qq ?? "";
            //出价人手机号
            bidPrice.BidUserMobile = mobile ?? "";
            //出价人邮箱
            bidPrice.BidUserMail = email ?? "";
            bidPrice.InnerCode = "";
            //出价次数
            bidPrice.BidCount = 1;
            //0未成交 1已成交 2已中标
            bidPrice.Status = istatus;
            // 出价类型 1竞价 2砍价 3秒杀 4一口价
            //bidPrice.BidType = ibidtype;
            //身份证
            bidPrice.IDCard = code ?? "";
            //创建时间
            bidPrice.CreateTime = DateTime.Now;//创建时间 
            //更新时间
            bidPrice.UpdateTime = DateTime.Now;//更新时间 
            bidPrice.IsDel = false;//是否删除
            bidPrice.BidRealName = realName;
            bool isUpdateSalesStatus = iupdatesalesstatus > 0;

            //是否支付保证金
            bool isPayBond = false;

            //是否已存在活动订单
            bool hasOrder = false;
            //查看用户是否有该活动已支付完成的出价订单,有即视为已支付保证金
            var listOrder = _activitysbll.GetActivityOrderByUserID(userInfo.Id, activitiesId);
            hasOrder = listOrder.Count > 0;
            isPayBond = listOrder.Where(o => o.BondStatus == 1).Count() > 0;

            TXOrm.Order order = listOrder.FirstOrDefault();
            // 0：添加失败  1：添加出价信息  2：添加订单 3:存在未支付订单
            int re = 0;

            if (isPayBond)
            {
                //类型为秒杀或一口价时不添加出价信息
                if (ibidtype == 3 || ibidtype == 4)
                {
                    re = 0;
                }
                else
                {
                    bidPrice.IDCard = order.IDCard;//身份证
                    re = _premisesbll.SubBidPriceInfo(bidPrice, isUpdateSalesStatus, houseId);
                }
                if (re > 0)
                {
                    re = 1;
                }
            }
            else
            {
                //没有订单才添加新订单
                if (!hasOrder)
                {
                    //添加订单信息
                    order = new TXOrm.Order
                    {
                        ActivitiesId = bidPrice.ActivitiesId,
                        BidCount = 0,
                        BidRealName = bidPrice.BidRealName,
                        BidUserMail = bidPrice.BidUserMail,
                        BidUserMobile = bidPrice.BidUserMobile,
                        BidUserPrice = bidPrice.BidUserPrice,
                        BidUserQQ = bidPrice.BidUserQQ,
                        CreateTime = DateTime.Now,
                        Desc = string.Empty,
                        HouseId = bidPrice.HouseId,
                        IDCard = code,
                        IsDel = false,
                        BidUserName = bidPrice.BidUserName,
                        OrderNo = string.Empty,
                        UpdateTime = DateTime.Now,
                        BidUserId = bidPrice.BidUserId,
                        Status = 0,
                        BidUserNumber = 0,
                        BondStatus = 0,
                        InnerCode = ""
                    };

                    re = _activitysbll.AddActivityOrder(order);
                    if (re > 0)
                    {
                        order = _activitysbll.GetActivityOrderByUserID(userInfo.Id, activitiesId).FirstOrDefault();
                        re = 2;
                    }
                }
                else
                {
                    //修改已有订单中的用户信息(秒杀时添加的订单信息不全)
                    order.BidRealName = bidPrice.BidRealName;
                    order.BidUserMail = bidPrice.BidUserMail;
                    order.BidUserMobile = bidPrice.BidUserMobile;
                    order.BidUserPrice = bidPrice.BidUserPrice;
                    order.BidUserQQ = bidPrice.BidUserQQ;
                    order.IDCard = code;
                    order.BidUserName = bidPrice.BidUserName;
                    order.Status = 0;
                    _activitysbll.UpdateOrderUserInfo(order);

                    re = 3;
                }
            }

            var result = new { state = re, orderID = order.Id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新订单状态为已支付
        /// author: maboxun
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateOrder(int orderID)
        {
            var order = _activitysbll.GetActivityOrderByID(orderID);
            TXCommons.user.cjkjb.webservice.UserInfo userInfo = GetUserInfo;
            //判断订单、用户信息是否存在且对应
            if (order == null || userInfo == null || order.BidUserId != userInfo.Id)
            {
                return 0;
            }
            else
            {
                //获得活动信息（获得该活动的保证金）
                TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

                //获取用户账户信息
                NHUserAccount userAccout = new NHUserAccount();
                UserAccountInfo uaInfo = userAccout.GetUserAccountInfo(userInfo.Id);
                //判断账户余额
                if (uaInfo.Price > activity.Bond)//???order.Bond
                {
                    //余额充足：扣费、更新订单
                    string msg = string.Format("新房-支付房源{0}{1}保证金", order.HouseId, TXCommons.NewHouseWeb.ActivitiesType.ActivitiesTypeByNo(activity.Type));
                    bool flag = userAccout.RecordAccountForBid(userInfo.Id, activity.Bond, msg);
                    if (flag)
                    {
                        return _activitysbll.UpdateActivityOrderSuc(orderID) > 0 ? 1 : 0;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    //余额不足：跳转第三方支付
                    //return _activitysbll.UpdateActivityOrderSuc(orderID) > 0 ? 2 : 0;
                    return 2;
                }
            }
        }

        /// <summary>
        /// 更新秒杀活动的订单状态
        /// author: maboxun
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateMSOrder(int orderID)
        {
            var order = _activitysbll.GetActivityOrderByID(orderID);

            TXCommons.user.cjkjb.webservice.UserInfo userInfo = GetUserInfo;
            //判断订单、用户信息是否存在且对应
            if (order == null || userInfo == null || order.BidUserId != userInfo.Id)
            {
                return 0;
            }
            else
            {
                //获得活动信息（获得该活动的保证金）
                TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

                //获取用户账户信息
                NHUserAccount userAccout = new NHUserAccount();
                UserAccountInfo uaInfo = userAccout.GetUserAccountInfo(userInfo.Id);
                //判断账户余额
                if (uaInfo.Price > activity.Bond)
                {
                    //余额充足：扣费、更新订单
                    string msg = string.Format("新房-支付房源{0}{1}保证金", order.HouseId, TXCommons.NewHouseWeb.ActivitiesType.ActivitiesTypeByNo(activity.Type));
                    bool flag = userAccout.RecordAccountForBid(userInfo.Id, activity.Bond, msg);
                    if (flag)
                    {
                        return _activitysbll.UpdateMSOrderSuc(orderID) > 0 ? 1 : 0;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    //余额不足：跳转第三方支付
                    //return _activitysbll.UpdateMSOrderSuc(orderID) > 0 ? 2 : 0;
                    return 2;
                }
            }
        }

        /// <summary>
        /// 提交秒杀相关数据 返回秒杀成功状态
        /// author: maboxun
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public int SubMSInfo(int houseId, int activityId)
        {
            TXCommons.user.cjkjb.webservice.UserInfo userInfo = GetUserInfo;
            if (userInfo == null)
                return 0;

            //是否已存在活动订单
            bool hasOrder = false;
            var listOrder = _activitysbll.GetActivityOrderByUserID(userInfo.Id, activityId);
            hasOrder = listOrder.Count > 0;
            TXOrm.Order order = listOrder.FirstOrDefault();

            //是否有该订单的定时服务（有：直接继续支付）
            if (hasOrder)
            {
                TXOrm.ActivitiesTiming at = new TXOrm.ActivitiesTiming()
                {
                    ActivityID = activityId,
                    OperId = order.Id.ToString()
                };
                if (_activitysbll.HasActivitiesTimingByOrder(at))
                {
                    return (!string.IsNullOrEmpty(order.IDCard) && order.Status != 3) ? -order.Id : 1;
                    //return (!string.IsNullOrEmpty(order.IDCard) && DateTime.Now.AddMinutes(-TXCommons.GetConfig.MSOperatingTime) < order.UpdateTime) ? 2 : 1;
                }
            }

            //添加 唯一活动房源（UniqueActivitiesHouse） 数据
            //添加成功视为秒杀成功，添加失败即为秒杀失败 
            TXOrm.UniqueActivitiesHouse uaHouse = new TXOrm.UniqueActivitiesHouse()
            {
                HouseId = houseId,
            };
            int result = _activitysbll.AddUniqueActivitiesHouse(uaHouse);
            if (result <= 0)
                return 0;

            //获取活动信息
            TXOrm.Activity _activity = _premisesbll.GetHouseActivityById(activityId);

            #region 没有订单添加新订单

            if (!hasOrder)
            {
                //添加订单信息
                order = new TXOrm.Order
                {
                    ActivitiesId = activityId,
                    BidCount = 0,
                    BidRealName = userInfo.RealName,
                    BidUserMail = userInfo.Email,
                    BidUserMobile = userInfo.Mobile,
                    BidUserPrice = _activity.BidPrice,
                    BidUserQQ = userInfo.QQ,
                    CreateTime = DateTime.Now,
                    Desc = string.Empty,
                    HouseId = houseId,
                    IDCard = "",
                    IsDel = false,
                    BidUserName = userInfo.LoginName,
                    OrderNo = string.Empty,
                    UpdateTime = DateTime.Now,
                    BidUserId = userInfo.Id,
                    Status = 0,
                    BidUserNumber = 0,
                    BondStatus = 0,
                    InnerCode = ""
                };

                int flag = _activitysbll.AddActivityOrder(order);
                if (flag > 0)
                {
                    order = _activitysbll.GetActivityOrderByUserID(userInfo.Id, activityId).FirstOrDefault();
                }
            }
            #endregion

            //添加 活动定时服务（ActivitiesTiming） 数据（订单：空、状态：待处理、结束时间：添加时间+配置时间）
            TXOrm.ActivitiesTiming activitiesTiming = new TXOrm.ActivitiesTiming()
            {
                ActivityID = activityId,
                HouseId = houseId,
                OperId = order.Id.ToString(),
                Status = 1,
                Type = 1
            };
            result = _activitysbll.AddActivitiesTiming(activitiesTiming, TXCommons.GetConfig.MSOperatingTime);

            return result;
        }

        #endregion

        #region 附近楼盘

        /// <summary>
        /// 附近楼盘 
        /// </summary>
        /// <param name="Lng">经度</param>
        /// <param name="Lat">纬度</param>
        /// <param name="km">公里</param>
        /// <returns></returns>
        public List<TXOrm.Premis> NearbyPremises(string Lng, string Lat, string km)
        {
            if (string.IsNullOrEmpty(Lng) || string.IsNullOrEmpty(Lat))
                return null;
            return _premisesbll.GetNearbyPremises(Lng, Lat, km);
        }

        #endregion

    }

}
