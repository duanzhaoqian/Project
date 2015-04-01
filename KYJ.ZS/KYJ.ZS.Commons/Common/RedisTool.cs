using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.Xml;
using KYJ.ZS.Commons.PictureModular;
using KYJ.ZS.Models.Authority;

namespace KYJ.ZS.Commons.Common
{
    public class RedisTool
    {
        public static string ImgpathBase { get { return RedisHelper.IMGPATH_BASE; } }
        public static string UserImgPathBase{
            get { return RedisHelper.USER_IMGPATH_BASE; }
        }

        public static string ImgwebBase
        {
            get { return RedisHelper.IMGWEB_BASE; }
        }

        public static string UserImgwebBase {
            get { return RedisHelper.USER_IMGWEB_BASE; }
        }

        public static string WaterMarkPic {
            get { return RedisHelper.WaterMarkPic; }
        }

        //public static string DocumentBase {
        //    get { return RedisHelper.}
        //}
        //public static string DocumentWebBase;
        //public static string VillageBase;
        //public static string VillageWebBase;
        //public static string KbapplyBase {
        //    get { return RedisHelper.}
        //}
        //public static string KbapplyWebBase;

        public static int GetSortedSetCount<T>(string key, Enum FunctionTypeEnum, int CityId)
        {
            return RedisHelper.GetSortedSetCount<T>(key, FunctionTypeEnum, CityId);
        }


        #region 用户登录

        /// <summary>
        /// Author:zhuzh
        /// Date:2014-4-25
        /// Desc:获取当前登录用户信息
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <param name="loginUserType">登录用户类型</param>
        /// <returns>LoginUserInfo数据实体</returns>
        public static LoginUserInfo GerLoginUserInfo(string guid, UserInfoType loginUserType)
        {
            string key = KeyManager.GetUserLoginKey(guid, loginUserType);
            return RedisHelper.GetValue<LoginUserInfo>(key, FunctionType.ZuShouUserInfo, 0);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date:2014-4-25
        /// Desc: 移除当前登录用户信息
        /// </summary>
        /// <param name="guid">用户GUID</param>
        /// <param name="loginUserType">登录用户类型</param>
        public static void RemoveLoginUserInfo(string guid, UserInfoType loginUserType)
        {
            string key = KeyManager.GetUserLoginKey(guid, loginUserType);

            RedisHelper.RemoveAllFromList(key, FunctionType.ZuShouUserInfo, 0);
        }

        public static void AddLoginUserInfo(LoginUserInfo loginUser, string guid, bool isautologin, UserInfoType loginUserType)
        {
            var key = KeyManager.GetUserLoginKey(guid, loginUserType);
            var time = DateTime.Now.AddHours(1);
            if (isautologin)
                time = DateTime.Now.AddDays(30);
            RedisHelper.SetValue<LoginUserInfo>(key, loginUser, time, FunctionType.ZuShouUserInfo, 0); //缓存用户信息
        }


        #endregion

        #region  Redis的基础操作

        /// <summary>
        /// 向指定城市Redis服务器存储对应的KEY和VALUES
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="info">类型 值</param>
        /// <param name="key">KEY</param>
        /// <param name="expireDateTime">有效期</param>
        /// <param name="functionType"></param>
        /// <param name="cityId">城市ID</param>
        /// <returns>是否成功</returns>
        public static bool AddItemToList<T>(T info, string key, DateTime? expireDateTime, FunctionType functionType, int cityId)
        {
            return RedisHelper.AddItemToList(info, key, expireDateTime, functionType, cityId); //缓存用户信息
        }

        /// <summary>
        /// 向Redis里写入数据
        /// </summary>
        /// <typeparam name="T">存储类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="info">存储数据</param>
        /// <param name="expireDateTime">有效时间（1、永久为null 2不永久输入有效时间）</param>
        /// <param name="functionType"></param>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public static bool SetValue<T>(string key, T info, DateTime? expireDateTime, FunctionType functionType, int cityId)
        {
            return RedisHelper.SetValue(key, info, expireDateTime, functionType, cityId);
        }

        /// <summary>
        /// 向Redis里获取数据
        /// </summary>
        /// <typeparam name="T">获取类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="functionType"></param>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public static T GetValue<T>(string key, FunctionType functionType, int cityId)
        {
            return RedisHelper.GetValue<T>(key, functionType, cityId);
        }

        /// <summary>
        /// 删除Redis里的数据
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="functionType"></param>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public static bool RemoveAllFromList(string key, FunctionType functionType, int cityId)
        {
            return RedisHelper.RemoveAllFromList(key, functionType, cityId);
        }

        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-27
        /// 描述：List的Redis里删除一条数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="info">数据信息</param>
        /// <param name="FunctionTypeEnum">枚举</param>
        /// <param name="CityId">城市Id</param>
        /// <returns></returns>
        public static bool RemoveItemFromList<T>(string key, T info, FunctionType functionType, int CityId)
        {
            return RedisHelper.RemoveItemFromList<T>(key, info, functionType, CityId);
        }
        /// <summary>
        /// 作者：邓福伟
        /// 时间：2014-05-27
        /// 描述：List的Redis获取全部数据
        /// </summary>
        /// <typeparam name="T">获取类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="FunctionTypeEnum"></param>
        /// <param name="CityId">城市Id</param>
        /// <returns></returns>
        public static T GetAllItemsFromList<T>(string key, FunctionType functionType, int CityId)
        {
            return RedisHelper.GetAllItemsFromList<T>(key, functionType, CityId);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date  :2014-05-28
        /// Desc  :获取自增数
        /// </summary>
        /// <param name="functionType">FunctionType枚举</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="key">键</param>
        /// <param name="expireDateTime">有效期</param>
        /// <returns></returns>
        public static double GetNextSequence(FunctionType functionType, int CityId, string key, DateTime expireDateTime)
        {
            return RedisHelper.GetNextSequence(functionType, CityId, key, expireDateTime);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date  :2014-05-28
        /// Desc  :获取自增数
        /// </summary>
        /// <param name="functionType">FunctionType枚举</param>
        /// <param name="CityId">城市ID</param>
        /// <param name="key">键</param>
        public static double GetNextSequence(FunctionType functionType, int CityId, string key)
        {
            return RedisHelper.GetNextSequence(functionType, CityId, key);
        }
        #endregion






    }

    /// <summary>
    /// Author:zhuzh
    /// Date:2014-04-28
    /// Desc:Redis的FunctionType的缩减版
    /// </summary>
    public enum FunctionType
    {
        [Description("租售购物车")]
        ZuShouCar = 31,
        [Description("租售验证码")]
        ZuShouVerificationCode = 32,
        [Description("租售用户信息")]
        ZuShouUserInfo = 33,
        [Description("租售商品图片")]
        ZuShouGoodsPicture = 34,
        [Description("租售用户资料图片")]
        ZuShouPapersPicture = 35,
        [Description("租售品牌图片")]
        ZuShouBrandPicture = 36,
        [Description("租售浏览量")]
        ZuShouBrowseAmount = 37,
        [Description("租售浏览历史")]
        ZuShouBrowseHistory = 38,
        [Description("租售辅助记录")]
        ZuShouCommon = 39,
        [Description("租售广告图片")]
        ZuShouAdvertisement = 40
    }
}
