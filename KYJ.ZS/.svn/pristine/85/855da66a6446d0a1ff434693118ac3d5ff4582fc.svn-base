using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ZS.Models.Carts;
using KYJ.ZS.DAL.Carts;
using KYJ.ZS.Commons.Common;
using ServiceStack;

namespace KYJ.ZS.BLL.Carts
{
    /// <summary>
    ///作者：邓福伟
    ///时间：204-4-17
    ///描述：购物车业务逻辑层
    /// </summary>
    public class CartBll
    {
        #region NoSql购物车
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-21
        /// 描述：获取购物车数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<CartEntity> GetCartList(string key)
        {
            return GetCartRedis(key);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-21
        /// 描述：获取购物车数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool Add(string key, CartEntity entity, DateTime? dateTime)
        {
            List<CartEntity> list = GetCartRedis(key);
            //判断是否初次添加购物车。初次直接添加
            if (list == null)
            {
                list = new List<CartEntity>();
                list.Add(entity);
            }
            //非初次需要判断是否有相同商品
            else
            {
                bool bl = true; //判断是商品是否直接添加
                foreach (CartEntity cartEntity in list)
                {
                    if (cartEntity.GoodsId == entity.GoodsId && //商品Id
                        cartEntity.ColorId == entity.ColorId && //颜色Id
                        cartEntity.NormId == entity.NormId) //规格
                    {
                        cartEntity.GoodsNum = cartEntity.GoodsNum + 1; //修改商品数量
                        bl = false; //商品可不用添加
                        break;
                    }
                }
                //商品直接添加
                if (bl)
                {
                    list.Add(entity);
                }
            }

            return SetCartRedis(key, list, dateTime);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-21
        /// 描述：购物车修改数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool Update(string key, CartEntity entity, DateTime? dateTime)
        {
            List<CartEntity> list = GetCartRedis(key);
            //根据Guid循环找出需要修改的商品
            foreach (CartEntity cartEntity in list)
            {
                if (cartEntity.CartGuid == entity.CartGuid)
                {
                    //修改商品数量
                    cartEntity.GoodsNum = entity.GoodsNum;
                    break;
                }
            }
            return SetCartRedis(key, list, dateTime);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-04-21
        /// 描述：购物车删除数据，删除购物车其中一条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(string key, CartEntity entity)
        {
            List<CartEntity> list = GetCartRedis(key);
            //根据Guid循环找出需要删除的商品
            foreach (CartEntity cartEntity in list)
            {
                if (cartEntity.CartGuid == entity.CartGuid)
                {
                    //删除商品
                    list.Remove(cartEntity);
                    break;
                }
            }
            return SetCartRedis(key, list);
        }
        /// <summary>
        /// 邓福伟
        /// 时间：2014-04-21
        /// 描述：删除购物车，整个购物车都删除(包括全部数据)
        /// </summary>
        /// <param name="key"></param>
        public void DeleteAll(string key)
        {
            RemoveCartRedis(key);
        }
        #endregion

        #region  购物车Redis的基础操作
        /// <summary>
        /// Author：邓福伟
        /// Time: 204-4-18
        /// Desc: 赋值Redis里数据
        /// </summary>
        /// <param name="key">键值必须唯一</param>
        /// <param name="list">键值对应的内容Value</param>
        /// <returns>执行结果：False或True</returns>
        private bool SetCartRedis(string key, List<CartEntity> list)
        {
            return RedisTool.SetValue<List<CartEntity>>(key, list, null, FunctionType.ZuShouCar, 0);
        }
        /// <summary>
        /// Author：邓福伟
        /// Time: 204-4-18
        /// Desc: 赋值Redis里数据
        /// </summary>
        /// <param name="key">键值必须唯一</param>
        /// <param name="list">键值对应的内容Value</param>
        /// <param name="dateTime">有效时间</param>
        /// <returns>执行结果：False或True</returns>
        private bool SetCartRedis(string key, List<CartEntity> list, DateTime? dateTime)
        {
            dateTime = dateTime == null ? DateTime.Now.AddDays(1) : dateTime;
            return RedisTool.SetValue<List<CartEntity>>(key, list, dateTime, FunctionType.ZuShouCar, 0);
        }
        /// <summary>
        /// Author：邓福伟
        /// Time: 204-4-18
        /// Desc: 获取Redis里数据
        /// </summary>
        /// <param name="key">键值必须唯一</param>
        /// <returns>返回购物车泛型集合</returns>
        private List<CartEntity> GetCartRedis(string key)
        {
            return RedisTool.GetValue<List<CartEntity>>(key, FunctionType.ZuShouCar, 0);
        }
        /// <summary>
        /// Author：邓福伟
        /// Time: 204-4-18
        /// Desc: 删除Redis里添加数据
        /// </summary>
        /// <param name="key">键值必须唯一</param>
        /// <returns>没有返回</returns>
        private bool RemoveCartRedis(string key)
        {
            return RedisTool.RemoveAllFromList(key, FunctionType.ZuShouCar, 0);
        }
        #endregion


    }
}
