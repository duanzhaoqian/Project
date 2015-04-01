using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons.Common
{
    public class KeyManager
    {
        /// <summary>
        /// 商品图片KEY
        /// </summary>
        /// <param name="Guid">GUID唯一标识</param>
        /// <param name="PictureType">图片类型</param>
        /// <returns></returns>
        public static string GetGoodsPictureKey(string Guid, string PictureType)
        {
            return ("GOODS:" + PictureType.ToUpper() + ":" + Guid);
        }
        /// <summary>
        /// 用户图片KEY
        /// </summary>
        /// <param name="Guid">GUID唯一标识</param>
        /// <param name="PictureType">图片类型</param>
        /// <returns></returns>
        public static string GetUserPictureKey(string Guid, string PictureType)
        {
            return ("USER:" + PictureType.ToUpper() + ":" + Guid);
        }
        /// <summary>
        /// 品牌图片key
        /// </summary>
        /// <param name="Guid">GUID唯一标识</param>
        /// <returns></returns>
        public static string GetBrandPictureKey(string Guid)
        {
            return ("Brand:Brand" + ":" + Guid);
        }

        /// <summary>
        /// 广告图片key
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GetAdvertisementKey(string guid)
        {
            return ("Advertisement:Advertisement" + ":" + guid);
        }

        /// <summary>
        /// 用户资料图片KEY
        /// </summary>
        /// <param name="Guid">GUID唯一标识</param>
        /// <param name="PictureType">图片类型</param>
        /// <returns></returns>
        public static string GetUserPapersPictureKey(string Guid, string PictureType)
        {
            return ("USERPAPERS:" + PictureType.ToUpper() + ":" + Guid);
        }

        public static string GetUserLoginKey(string Guid, UserInfoType userType)
        {
            return (userType.ToString().ToUpper() + ":" + Guid);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date  :2014-04-28
        /// Desc  :订单号KEY（1601yymmddhhmmss[__]）,中括号中为当前KEY存储的值。
        /// </summary>
        /// <param name="orderType">订单号类型</param>
        /// <returns></returns>
        public static string GetOrderNumberKey(string orderType)
        {
            return ("ORDERNUMBER:" + orderType.ToUpper());
        }
        /// <summary>
        /// Author:zhuzh
        /// Date  :2014-05-28
        /// Desc  :获取验证码
        /// </summary>
        /// <returns></returns>
        public static string GetVerificationCodeKey(string mobile)
        {
            return ("VERIFICATIONCODE:" + mobile);
        }
        /// <summary>
        /// Author:zhuzh
        /// Date  :2014-05-28
        /// Desc  :用户辅助KEY
        /// </summary>
        /// <param name="Guid"></param>
        /// <returns></returns>
        public static string GetUserCommonKey(string Guid)
        {
            return ("USERCOMMON:" + Guid);
        }
    }
}
