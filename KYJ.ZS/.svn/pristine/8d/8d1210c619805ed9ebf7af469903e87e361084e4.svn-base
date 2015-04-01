using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.BLL.DeliveryAddresses;
using System.Web.Mvc;
using KYJ.ZS.Models.DB;
using KYJ.ZS.Models.Orders;
using KYJ.ZS.Commons;
using KYJ.ZS.BLL.Orders;
using KYJ.ZS.BLL.FreightTemplates;
using KYJ.ZS.Web.Controllers.ActionFilters;
using KYJ.ZS.BLL.LocalUsers;
using KYJ.ZS.Models.LocalUsers;

namespace KYJ.ZS.Web.Controllers
{
    [LoginChecked]
    public class OrdersController : BaseController
    {
        OrderBll bll = new OrderBll();
        DeliveryAddressBll deliveryaddressBll = new DeliveryAddressBll();
        FreightTemplateBll freightTemplateBll = new FreightTemplateBll();

        
        /// <summary>
        /// 作者:邓福伟
        /// 时间：2014-5-7
        /// 描述：订单详情业
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
             #region 获取用户认证信息
            LocalUserBll localUserBll = new LocalUserBll();
            AuthenticationEntity authenticationEntity=localUserBll.GetAuthenticationByUserID(_ServiceContext.CurrentUser.UserID);
            if(authenticationEntity==null)
            {
               authenticationEntity=new AuthenticationEntity ();
            }
              #endregion
            //判断用户是否认证
            if (authenticationEntity.PapersStatus==4)
            {
                #region 获取订单参数
                Web_OrderParameterEntity opeEntity = new Web_OrderParameterEntity();
                opeEntity.GoodsId = Auxiliary.ToInt32(Request["id"]);
                opeEntity.GoodsNum = Auxiliary.ToInt32(Request["gn"]);
                opeEntity.GoodsMonth = Auxiliary.ToInt32(Request["gm"]);
                opeEntity.GoodsColorId = Auxiliary.ToInt32(Request["gcid"]);
                opeEntity.GoodsNormId = Auxiliary.ToInt32(Request["gnid"]);
                #endregion
                #region 获取参数判断结果
                int ParameterJudge = bll.Web_OrderParameterJudge(opeEntity);
                string ParameterJudgeText = string.Empty;
                //-1为未知、0为正确、1为商品Id不正确、2商品颜色Id不确定、3商品规格Id不确定、4商品数量不正确、5租期不正确
                switch (ParameterJudge)
                {
                    case -1: ParameterJudgeText = "-1为未知！"; break;
                    case 0: ParameterJudgeText = "0正确！"; break;
                    case 1: ParameterJudgeText = "1为商品Id不正确！"; break;
                    case 2: ParameterJudgeText = "2商品颜色Id不确定！"; break;
                    case 3: ParameterJudgeText = "3商品规格Id不确！"; break;
                    case 4: ParameterJudgeText = "4商品数量不正确！"; break;
                    case 5: ParameterJudgeText = "5租期不正确！"; break;
                    case 6: ParameterJudgeText = "6收获地址不正确！"; break;
                }
                ViewData["ParameterJudge"] = ParameterJudge;
                ViewData["ParameterJudgeText"] = ParameterJudgeText;
                #endregion
                #region 获取订单详情
                Web_OrderConfirm ocEntity = new Web_OrderConfirm();
                //判断参数是否正确
                if (ParameterJudge == 0)
                {
                    //订单确认页，获取订单详情
                    ocEntity = bll.Web_GetOrderConfirm(opeEntity);

                    if (ocEntity == null)
                    {
                        ocEntity = new Web_OrderConfirm();
                    }
                    else
                    {
                        //获取运费
                        ocEntity.GoodsFreightTemplate = freightTemplateBll.Web_GetFreightTemplateList(ocEntity.FtempId).ToJson();
                        //获取收货地址
                        ViewData["DeliveryAddress"] = deliveryaddressBll.Web_DeliVeryAddressList(_ServiceContext.CurrentUser.UserID);
                    }
                }
                #endregion
              return View(ocEntity);
            }
            else
            {
                //跳转到认证页面
                return Redirect(Url.UserSiteUrl().Apply);
            }

           
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-5-7
        /// 描述：订单提交，生成订单
        /// </summary>
        /// <returns></returns>
        public ActionResult SubmitOrder(FormCollection formCollection)
        {
            string strResult = string.Empty;
            if (_ServiceContext.CurrentUser != null)
            {
                #region 获取订单参数
                Web_OrderParameterEntity opeEntity = new Web_OrderParameterEntity();
                opeEntity.GoodsId = Auxiliary.ToInt32(formCollection["goods_id"]); //商品Id
                opeEntity.UserId = _ServiceContext.CurrentUser.UserID;  //用户Id
                opeEntity.UserNickName = _ServiceContext.CurrentUser.NickName; //用户名称
                opeEntity.GoodsNum = Auxiliary.ToInt32(formCollection["goods_num"]);  //商品数量
                opeEntity.GoodsMonth = Auxiliary.ToInt32(formCollection["goods_month"]); //商品租期
                opeEntity.GoodsNormId = Auxiliary.ToInt32(formCollection["goods_normid"]); //商品规格
                opeEntity.GoodsColorId = Auxiliary.ToInt32(formCollection["goods_colorid"]); //商品颜色
                opeEntity.DeliveryId = Auxiliary.ToInt32(formCollection["delivery_id"]); //收货地址Id

                opeEntity.ShippingMethod = Auxiliary.ToInt32(formCollection["shippingmethod"]) <= 3 ? Auxiliary.ToInt32(formCollection["shippingmethod"]) : 0; //发票类型
                opeEntity.IsPhoneConfirm = (Auxiliary.ToInt32(formCollection["isphoneconfirm"]) == 1); //是否电话确认
                opeEntity.OrderNumber = Tool.Instance.GetOrderNumber(1); //订单编号
                #endregion
                #region 获取参数判断结果
                int ParameterJudge = 0;
                if (opeEntity.GoodsId <= 0 || opeEntity.GoodsNum <= 0 || opeEntity.GoodsMonth <=0 || opeEntity.DeliveryId <= 0)
                {
                    strResult = "提交参数错误！";
                    ParameterJudge = 7;
                }
                else
                {
                    ParameterJudge = bll.Web_OrderParameterJudge(opeEntity);
                    string ParameterJudgeText = string.Empty;
                    //-1为未知、0为正确、1为商品Id不正确、2商品颜色Id不确定、3商品规格Id不确定、4商品数量不正确、5租期不正确、6收获地址不正确
                    switch (ParameterJudge)
                    {
                        case -1: ParameterJudgeText = "-1SQL错误-验证！"; break;
                        case 0: ParameterJudgeText = "0正确！"; break;
                        case 1: ParameterJudgeText = "1商品Id不正确！"; break;
                        case 2: ParameterJudgeText = "2商品颜色Id不确定！"; break;
                        case 3: ParameterJudgeText = "3商品规格Id不确！"; break;
                        case 4: ParameterJudgeText = "4商品数量不正确！"; break;
                        case 5: ParameterJudgeText = "5租期不正确！"; break;
                        case 6: ParameterJudgeText = "6收获地址不正确！"; break;
                    }
                    strResult = ParameterJudgeText;
                }
                #endregion
                #region 执行提交订单结果
                //判断参数是否正确
                if (ParameterJudge == 0)
                {
                    int SubmitOrderResult = bll.Web_SubmitOrder(opeEntity);
                    string SubmitOrderText = string.Empty;
                    switch (SubmitOrderResult)
                    {
                        case -1: SubmitOrderText = "-1SQL错误-生成！"; break;
                        case 0: SubmitOrderText = "0添加失败！"; break;
                        case 1: SubmitOrderText = Url.PaySiteUrl().Order(opeEntity.OrderNumber, Url.UserSiteUrl().RentOrdersList); break;
                    }
                    strResult = SubmitOrderText; 
                }
                #endregion
            }
            else
            {
                strResult = "用户上未登录！";
            }

            return Json(strResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 作者:邓福伟
        /// 时间:2014-4-29
        /// 描述:根据User_Id查找收货地址
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>收货地址集合</returns>
        public ActionResult DeliveryAddressesList()
        {
            int userid = _ServiceContext.CurrentUser.UserID;
            return Json(deliveryaddressBll.Web_DeliVeryAddressList(userid), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 作者:邓福伟
        /// 时间:2014-5-9
        /// 描述:根据收货地址Id获取收货详情
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>收货地址详情</returns>
        public ActionResult DeliveryAddressesEntity(int id)
        {
            DeliveryAddress daEntity = deliveryaddressBll.FindIdDeliveryAddresses(id, _ServiceContext.CurrentUser.UserID);
            if(daEntity==null)
            {
               daEntity=new DeliveryAddress ();
            }
            return Json(daEntity, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 作者:邓福伟
        /// 时间:2014-5-9
        /// 描述:收货地址删除或修改
        /// </summary>
        /// <returns>返回受影响的行数</returns>
        public ActionResult DeliveryAddressesAddEdit(DeliveryAddress daEntity)
        {
            #region 获取收货人信息参数
            daEntity.UserId = _ServiceContext.CurrentUser.UserID;
            daEntity.ResTel = daEntity.ResTel == null ? DBNull.Value.ToString() : daEntity.ResTel;
            daEntity.UpdateTime = DateTime.Now;
            daEntity.CreateTime = DateTime.Now;
            daEntity.IsDel = false;
            #endregion
            #region 添加或修改
            int resault = 0;
            if (ModelState.IsValid) //后台验证参数是否正确
            {
                if (daEntity.Id > 0) //修改
                {
                    resault = deliveryaddressBll.UpdateDeliveryAddress(daEntity);
                }
                else //添加
                {
                    resault = deliveryaddressBll.AddDeliveryAddress(daEntity);
                }
            }
            #endregion
            return Json(resault, JsonRequestBehavior.AllowGet);
        }

    }
}
