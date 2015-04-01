using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using KYJ.ZS.BLL.Carts;
using KYJ.ZS.Models.Carts;
using KYJ.ZS.Commons;

using ServiceStack;


namespace KYJ.ZS.Web.Controllers
{
    /// <summary>
    /// 作者：邓福伟
    /// 时间: 204-4-18
    /// 描述: 购物车控制器
    /// </summary>
    public class CartController : BaseController
    {
        CartBll bll = new CartBll();


        /// <summary>
        ///购物车列表 
        /// </summary>
        /// <returns></returns>
        public JsonResult List()
        {
            string cartGuid = string.Empty; //获取购物车唯一标识
           
            //判断用户是否登录,未登录
            if (_ServiceContext.CurrentUser == null)
            {
                return Json("尚未登录，请先登录！", JsonRequestBehavior.AllowGet);
            }
            //判断用户是否登录,登录
            else
            {
                //获取用户的Guid
                cartGuid = _ServiceContext.CurrentUser._Guid.ToString();
                //向购物车中插入数据
                List<CartEntity> list = bll.GetCartList(cartGuid);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 购物车添加数据,分为：登录、未登录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult Add(CartEntity entity)
        {
            string cartGuid = string.Empty; //获取购物车唯一标识
            entity.CartGuid = Guid.NewGuid().ToString(); //给商品赋值唯一标识
            //判断用户是否登录,未登录
            if (_ServiceContext.CurrentUser == null)
            {
                //随机获取购物车唯一标识
                cartGuid = Guid.NewGuid().ToString();
                Response.Cookies["CartGuid"].Value = Auxiliary.Md5Encrypt(cartGuid);
                //向购物车中插入数据
                
                bool bl = bll.Add(cartGuid, entity, DateTime.Now.AddDays(7));

                return Json("向Cookie写入数据" + bl.ToString(), JsonRequestBehavior.AllowGet);
            }
            //判断用户是否登录,登录
            else
            {
                //获取用户的Guid
                cartGuid = _ServiceContext.CurrentUser._Guid.ToString();
                //向购物车中插入数据
                bool bl = bll.Add(cartGuid, entity,null);

                return Json("向NoSql写入数据" + bl.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 购物车修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonResult Upadte(CartEntity entity)
        {
            //获取用户的Guid
            string cartGuid = _ServiceContext.CurrentUser._Guid.ToString();
            //向购物车中插入数据
            bool bl = bll.Update(cartGuid, entity, null);

            return Json("向NoSql写入数据" + bl.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}
