using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TXModel.User;
using TXBll.User;
using Webdiyer.WebControls.Mvc;
using TXModel.Commons;
using TXCommons.MsgQueue;
using TXCommons.Index;
using TXCommons;
using System.Data;
using System.Reflection;
using System.Collections;
namespace TXNewHouseWeb.Controllers
{
    public class UserController : BaseController
    {
        MyNewHousesBll _bll = new MyNewHousesBll();
        /// <summary>
        /// 楼盘BLL类-前台
        /// </summary>
        readonly TXBll.HouseData.PremisesBll _premisesbll = new TXBll.HouseData.PremisesBll();

        /// <summary>
        /// 活动BLL类
        /// </summary>
        readonly TXBll.Activitys.ActivitysBll _activitysbll = new TXBll.Activitys.ActivitysBll();

        /// <summary>
        /// 我参与的竞购
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult Bid(int? page)
        {
            ViewData["newhouse"] = "1";
            if (IsLogin)
            {
                Paging<TXModel.User.CT_Bid> paging = new Paging<TXModel.User.CT_Bid>();
                paging.PageIndex = page.HasValue ? page.Value : 1;
                paging.PageSize = 10;
                int type = Request.QueryString["type"] == null ? 0 : Convert.ToInt32(Request.QueryString["type"]);
                ViewData["type"] = type;
                try
                {
                    paging = _bll.GetMyBidList(GetUserInfo.Id, type, paging);
                    PagedList<TXModel.User.CT_Bid> pagelist = new PagedList<TXModel.User.CT_Bid>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                    if (pagelist != null && pagelist.Count > 0)
                    {
                        foreach (var item in pagelist)
                        {
                            //调取楼盘的效果图。如无数据则提示：“暂无数据”。                   
                            TXCommons.PictureModular.PremisesPictureInfo listCarouseEffect = new TXCommons.PictureModular.PremisesPictureInfo();
                            listCarouseEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureInfo(item.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), item.CityId);
                            if (listCarouseEffect != null && listCarouseEffect.Path != null)
                            {
                                item.InnerCode = listCarouseEffect.Path.ToString();
                            }
                            else
                            {
                                item.InnerCode = TXCommons.GetConfig.ImgUrl + "images/w174_h122.gif";
                            }
                            string tempName = String.Format("{0} {1}{2} {3} {4}层{5}号房", item.PremisesName,
                                                            item.BuildingName, item.BuildingNameType, item.Unit,
                                                            item.Floor, item.Name);
                            if (tempName.Length > 24)
                            {
                                item.Name = tempName.Substring(0, 24);
                            }
                            else
                            {
                                item.Name = tempName;
                            }
                            if (item.ActivieAddress.Length > 25)
                            {
                                item.ActivieAddress = item.ActivieAddress.Substring(0, 25);
                            }

                            item.Clicks = TXCommons.Util.ToInt(TXCommons.Redis.GetOneViewCountValue(string.Format("h_{0}", item.HouseId), ServiceStack.FunctionTypeEnum.NewHouseFocusHouseCount, item.CityId));
                        }
                    }
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("MyNewHouses/BidTable", pagelist);
                    }
                    else
                    {
                        return View("MyNewHouses/Bid", pagelist);
                    }
                }
                catch (Exception)
                {
                    return Redirect(TXCommons.GetConfig.LoginUrl);
                }
            }
            else
            {
                return Redirect(TXCommons.GetConfig.LoginUrl);
            }
        }
        /// <summary>
        /// 我的出价记录
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult BidRecord(int? page)
        {
            ViewData["newhouse"] = "1";
            if (IsLogin)
            {
                Paging<TXOrm.BidPrice> paging = new Paging<TXOrm.BidPrice>();
                paging.PageIndex = page.HasValue ? page.Value : 1;
                paging.PageSize = 20;
                int houseid = Request.QueryString["houseid"] == null ? 0 : Convert.ToInt32(Request.QueryString["houseid"]);
                string name = Request.QueryString["name"] == null ? "" : HttpUtility.UrlDecode(Request.QueryString["name"]);
                ViewData["Title"] = name;

                try
                {
                    paging = _bll.GetMyBidRecord(houseid, GetUserInfo.Id, paging);
                    ViewData["TotalItemCount"] = paging.TotalCount;
                    var pagedList = new PagedList<TXOrm.BidPrice>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("MyNewHouses/BidRecordTable", pagedList);
                    }
                    else
                    {
                        return View("MyNewHouses/BidRecord", pagedList);
                    }
                }
                catch (Exception)
                {
                    return Redirect(TXCommons.GetConfig.LoginUrl);
                }
            }
            else
            {
                return Redirect(TXCommons.GetConfig.LoginUrl);
            }

        }
        /// <summary>
        /// 我参与的网络摇号
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult Yaohao()
        {
            ViewData["newhouse"] = "1";
            if (IsLogin)
            {
                int totalCount = 0;
                int CurrentPage = Request.QueryString["pageindex"] == null ? 1 : Convert.ToInt32(Request.QueryString["pageindex"]);
                try
                {
                    List<CT_Yaohao> list = _bll.GetMyYaohaoList(GetUserInfo.Id, CurrentPage, 10, out totalCount);
                    var pagedList = new PagedList<CT_Yaohao>(list, CurrentPage, 10, totalCount);
                    return View("MyNewHouses/Yaohao", pagedList);
                }
                catch (Exception)
                {
                    return Redirect(TXCommons.GetConfig.LoginUrl);
                }
            }
            else
            {
                return Redirect(TXCommons.GetConfig.LoginUrl);
            }
        }
        /// <summary>
        /// 我收藏的楼盘
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult CollectionsPremise(int? page)
        {
            ViewData["newhouse"] = "1";
            if (IsLogin)
            {
                Paging<TXModel.User.CT_FavoritePrem> paging = new Paging<TXModel.User.CT_FavoritePrem>();
                paging.PageIndex = page.HasValue ? page.Value : 1;
                paging.PageSize = 10;
                try
                {
                    TXCommons.NHWebFavorite favorite = new TXCommons.NHWebFavorite();
                    TXCommons.user.cjkjb.webservice.UserFavoritePage lst = favorite.GetMyPremisesFavorite(GetUserInfo.Id, "400", -1, -1, -1, "");
                    System.Text.StringBuilder pids = new System.Text.StringBuilder();
                    foreach (var item in lst.UserFavoriteList)
                    {
                        //pids.Append(item.BusinessId);
                        //pids.Append(",");

                        pids.Append(item.BusinessId + "|" + item.CreateTime);
                        pids.Append(",");
                    }
                    paging = _bll.GetMyPremisesFavorite(pids.ToString(), paging);
                    var pagedList = new PagedList<CT_FavoritePrem>(paging.ResultData, paging.PageIndex, paging.PageSize, paging.TotalCount);
                    if (pagedList != null && pagedList.Count > 0)
                    {
                        foreach (var item in pagedList)
                        {
                            //调取楼盘的效果图。如无数据则提示：“暂无数据”。                   
                            TXCommons.PictureModular.PremisesPictureInfo listCarouseEffect = new TXCommons.PictureModular.PremisesPictureInfo();
                            listCarouseEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureInfo(item.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), item.CityId);
                            if (listCarouseEffect != null && listCarouseEffect.Path != null)
                            {
                                item.InnerCode = listCarouseEffect.Path.ToString();
                            }
                            else
                            {
                                item.InnerCode = TXCommons.GetConfig.ImgUrl + "images/w174_h122.gif";
                            }
                            if (item.Name.Length > 24)
                            {
                                item.Name = item.Name.Substring(0, 24);
                            }
                            if (item.salesAddress.Length > 25)
                            {
                                item.salesAddress = item.salesAddress.Substring(0, 25);
                            }
                            if (item.Developer.Length > 30)
                            {
                                item.Developer = item.Developer.Substring(0, 30);
                            }
                            foreach (var f in lst.UserFavoriteList)
                            {
                                if (item.PremisId == Convert.ToInt32(f.BusinessId))
                                {
                                    item.FavoriteId = f.Id;
                                }
                            }


                            #region 楼盘户型图 取Lucene数据

                            //Lucene地址
                            string url = MQHelp.GetLuceneRoomUrlByCityId(Util.ToString(ViewData["CityId"]));
                            List<IndexRoom> sizeChartList = new List<IndexRoom>();
                            var index = new IndexRoomConditionInfo();
                            index.CityId = Util.ToString(ViewData["CityId"]);
                            index.PremisesID = item.PremisId.ToString();
                            index.PageSize = "100";
                            var condtion = IndexConditionInfo.GetSearchRoomCondiction(index);
                            string premises_xml_url = url + "?condition=" + condtion;//查询生成xml文件
                            DataSet ds = new DataSet("Results");
                            ds.ReadXml(premises_xml_url);
                            if (ds.Tables.Count <= 0)
                            {
                                sizeChartList = null;
                            }
                            else
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
                            ViewData["sizeChartListViewData"] = sizeChartList;

                            if (null != sizeChartList)
                            {
                                var lsHouseRoomInfoList = sizeChartList.OrderBy(s => Util.ToInt(s.Room)).Select(s => Util.ToInt(s.Room)).Distinct().ToArray();
                                if (lsHouseRoomInfoList != null)
                                {
                                    var arrRoom = new ArrayList();
                                    foreach (var room in lsHouseRoomInfoList)
                                    {
                                        int roomNum = sizeChartList.Where(s => Util.ToInt(s.Room) == room).Count();
                                        arrRoom.Add(string.Format("<a href=\"{0}\" target=\"_blank\" class=\"linkD mr5\">{1}居({2})</a>", TXCommons.NHWebUrl.AlbumUrl("hxt", index.PremisesID, "r"+room.ToString()), room, roomNum));
                                    }

                                    item.Rooms = string.Join("", arrRoom.ToArray());
                                }
                            }

                            #endregion


                            if (!string.Empty.Equals(item.Features.Trim()))
                            {
                                List<TXOrm.PremisesFeature> PremisesFeaturels = _premisesbll.GetPremisesFeatureList(item.Features.Trim());

                                item.Features = string.Join(",", PremisesFeaturels.Select(c => c.Name).ToArray());
                            }
                        }
                    }

                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("MyNewHouses/CollectionTable", pagedList);
                    }
                    else
                    {
                        return View("MyNewHouses/CollectionsPremise", pagedList);
                    }
                }
                catch (Exception)
                {
                    return Redirect(TXCommons.GetConfig.LoginUrl);
                }
            }
            else
            {
                return Redirect(TXCommons.GetConfig.LoginUrl);
            }

        }

        /// <summary>
        /// 获取楼盘预览房源信息
        /// author:汪敏
        /// </summary>
        /// <param name="PremisId">楼盘Id</param>
        /// <returns></returns>
        public JsonResult GetPreviewHouseByPremisId()
        {
            ViewData["newhouse"] = "1";
            int premisId = Convert.ToInt32(Request.QueryString["pid"]);
            List<CT_Bid> list = _bll.GetPreviewHouseByPremisId(premisId);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    //调取楼盘的效果图。如无数据则提示：“暂无数据”。                   
                    TXCommons.PictureModular.PremisesPictureInfo listCarouseEffect = new TXCommons.PictureModular.PremisesPictureInfo();
                    listCarouseEffect = TXCommons.PictureModular.GetPicture.GetPremisesPictureInfo(item.InnerCode, true, TXCommons.PictureModular.PremisesPictureType.Effect.ToString(), item.CityId);
                    if (item.InnerCode == "")
                    {
                        item.InnerCode = TXCommons.GetConfig.GetQTBaseUrl + "xinfang/images/pic-demo3.gif";
                    }
                    else
                    {
                        item.InnerCode = string.Format("{0}.71_53.jpg", listCarouseEffect.Path.ToString());
                    }
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取消收藏
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            ViewData["newhouse"] = "1";
            if (IsLogin)
            {
                int result = 0;
                try
                {
                    int pid = Convert.ToInt32(Request.QueryString["pid"]);
                    TXCommons.NHWebFavorite favorite = new TXCommons.NHWebFavorite();
                    bool flag = favorite.DeletePremisesFavorite(pid);
                    if (flag)
                    {
                        result = 1;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return Content(result.ToString());
            }
            else
            {
                return Redirect(TXCommons.GetConfig.LoginUrl);
            }
        }
        /// <summary>
        /// 支付保证金(竞价，砍价)
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult PayBond()
        {
            ViewData["newhouse"] = "1";
            ViewData["Bond"] = Request.QueryString["bond"];
            ViewData["OrderId"] = Request.QueryString["orderid"];
            return View("MyNewHouses/PayBond");
        }
        /// <summary>
        /// 支付保证金（秒杀，一口价）
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult Upinfo()
        {
            ViewData["newhouse"] = "1";
            ViewData["Mobile"] = GetUserInfo.Mobile;
            ViewData["RName"] = GetUserInfo.RealName;
            ViewData["IDCard"] = GetUserInfo.IDNO;
            ViewData["QQ"] = GetUserInfo.QQ;
            ViewData["Email"] = GetUserInfo.Email;
            ViewData["Bond"] = Request.QueryString["bond"];
            ViewData["OrderId"] = Request.QueryString["orderid"];
            ViewData["House"] = Request.QueryString["house"] == null ? "" : HttpUtility.UrlDecode(Request.QueryString["house"]);
            return View("MyNewHouses/Upinfo");
        }
        /// <summary>
        /// 更新用户信息
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateUserInfo()
        {
            ViewData["newhouse"] = "1";
            try
            {
                TXCommons.NHUserAccount user = new TXCommons.NHUserAccount();
                TXCommons.user.cjkjb.webservice.UserInfo userInfo = GetUserInfo;
                userInfo.Mobile = Request.QueryString["mobile"];
                userInfo.RealName = Request.QueryString["rname"];
                userInfo.IDNO = Request.QueryString["idcard"];
                userInfo.QQ = Request.QueryString["qq"];
                userInfo.Email = Request.QueryString["email"];
                userInfo.Platform = "FC";
                string xmlUser = TXCommons.Xml.ComObjOrXml.Serializer(typeof(TXCommons.user.cjkjb.webservice.UserInfo), userInfo);
                user.UpdateUser(xmlUser);
                return Content("1");
            }
            catch (Exception)
            {
                return Content("0");
            }

        }
        /// <summary>
        /// 修改订单状态（竞价，砍价）
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckMoneyAndpayfor()
        {
            ViewData["newhouse"] = "1";
            TXCommons.NHUserAccount user = new TXCommons.NHUserAccount();
            decimal accountMoney = user.GetUserAccountInfo(GetUserInfo.Id).Price;//账户余额
            decimal bond = Convert.ToDecimal(Request.QueryString["bond"]);
            if (accountMoney >= bond)
            {
                int orderID = Convert.ToInt32(Request.QueryString["orderid"]);
                var order = _activitysbll.GetActivityOrderByID(orderID);
                TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

                string msg = string.Format("新房-支付房源{0}{1}保证金", order.HouseId, TXCommons.NewHouseWeb.ActivitiesType.ActivitiesTypeByNo(activity.Type));
                bool flag = user.RecordAccountForBid(GetUserInfo.Id, bond, msg);//扣款，添加订单
                if (flag)
                {
                    TXBll.Activitys.ActivitysBll _abll = new TXBll.Activitys.ActivitysBll();
                    _abll.UpdateActivityOrderSuc(orderID);
                    return Content("1");
                }
                else
                {
                    return Content("2");
                }
            }
            else
            {
                return Content("0");
            }
        }
        /// <summary>
        /// 修改订单状态（秒杀，一口价）
        /// author:汪敏
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckMoneyAndpayforMs()
        {
            ViewData["newhouse"] = "1";
            TXCommons.NHUserAccount user = new TXCommons.NHUserAccount();
            decimal accountMoney = user.GetUserAccountInfo(GetUserInfo.Id).Price;//账户余额
            decimal bond = Convert.ToDecimal(Request.QueryString["bond"]);
            if (accountMoney >= bond)
            {
                int orderID = Convert.ToInt32(Request.QueryString["orderid"]);
                var order = _activitysbll.GetActivityOrderByID(orderID);
                TXOrm.Activity activity = _premisesbll.GetHouseActivityById(order.ActivitiesId);

                string msg = string.Format("新房-支付房源{0}{1}保证金", order.HouseId, TXCommons.NewHouseWeb.ActivitiesType.ActivitiesTypeByNo(activity.Type));
                bool flag = user.RecordAccountForBid(GetUserInfo.Id, bond, msg);//扣款，添加订单
                if (flag)
                {
                    TXBll.Activitys.ActivitysBll _abll = new TXBll.Activitys.ActivitysBll();
                    _abll.UpdateMSOrderSuc(orderID);
                    return Content("1");
                }
                else
                {
                    return Content("2");
                }
            }
            else
            {
                return Content("0");
            }
        }
    }
}
