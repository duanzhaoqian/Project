

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KYJ.ZS.Commons.Common;
using ServiceStack;

namespace KYJ.ZS.Commons.PictureModular
{
    public class GetPicture
    {
        #region 工具
        /// <summary>
        /// 格式化guid路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns>路径</returns>
        private static string FormatGuid(string guid)
        {
            string result = guid.Substring(0, 2) + "/" + guid.Substring(2, 2) + "/" + guid.Substring(4, 2) + "/" + guid + "/";
            return result;
        }

        /// <summary>
        /// 图片相对路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetPicturePath(string guid)
        {
            string result = FormatGuid(guid);
            return result;
        }

        /// <summary>
        /// 用户图片绝对路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetUserPictureBasePath(string guid)
        {
            string result = RedisTool.UserImgPathBase + GetPicturePath(guid);
            return result;
        }

        /// <summary>
        /// 图片绝对路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetPictureBasePath(string guid)
        {
            string result = RedisTool.ImgpathBase + GetPicturePath(guid);
            return result;
        }

        /// <summary>
        /// 图片web路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetPictureWebPath(string guid)
        {
            return RedisTool.ImgwebBase + GetPicturePath(guid);
        }


        /// <summary>
        /// 获取随机名称
        /// </summary>
        /// <param name="num">位数</param>
        /// <returns>名称</returns>
        public static string GetRandomFileName(int num)
        {
            string result = string.Empty;
            char[] chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            var random = new Random();
            for (int i = 0; i < num; i++)
            {
                int index = random.Next(0, chars.Length);
                result += chars[index].ToString(CultureInfo.InvariantCulture);
            }
            return result;
        }

        /// <summary>
        /// 用户图片web路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetUserPictureWebPath(string guid)
        {
            string result = RedisTool.UserImgwebBase + GetPicturePath(guid);
            return result;
        }

        #endregion


        #region 排序
        public static void NewSortGoodsPicture(string guid, string picturetype,string newSortIds)
        {
            string key = KeyManager.GetGoodsPictureKey(guid, picturetype);

            IDictionary<GoodsInfo, double> imglist =
                RedisHelper.GetAllWithScoresFromSortedSet<GoodsInfo>(key, FunctionType.ZuShouGoodsPicture, 0);
            var count = 1;

            var newIdShorts = newSortIds.Split(',').Distinct().ToList();
            string scort = "1.";
            var newId = imglist.Keys.ToDictionary(pic => pic.Id);
            RedisHelper.RemoveAllFromList(key, FunctionType.ZuShouGoodsPicture, 0);
            foreach (var id in newIdShorts)
            {
                var newscore = Convert.ToDouble(scort + count);
                var item = newId[Convert.ToInt32(id)];
                RedisHelper.AddItemToSortedSet(item, key, newscore, null, FunctionType.ZuShouGoodsPicture, 0);
                if (count % 10 == 9)
                {
                    scort = scort + count;
                    count = 1;
                }
                else
                    count++;
            }
        }

        #endregion


        /// <summary>
        /// 获取用户图片信息
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns></returns>
        public static UserPictureInfo GetUserPictureInfo(string guid, bool fullpath, string picturetype)
        {
            var key = KeyManager.GetUserPictureKey(guid, picturetype);

            var result = new UserPictureInfo();

            var list = RedisHelper.GetAllItemsFromSortedSet<List<UserPictureInfo>>(key, FunctionTypeEnum.UserHeadImage, 0);

            if (list != null && list.Count > 0)
            {
                result = list[0];
                if (fullpath)
                    result.Path = RedisHelper.USER_IMGWEB_BASE + result.Path;
            }
            return result;
        }

        /// <summary>
        /// 获取用户图片信息
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns></returns>
        public static List<UserPictureInfo> GetUserPictureInfos(string guid, bool fullpath, string picturetype)
        {
            string key = KeyManager.GetUserPictureKey(guid, picturetype);

            var list = RedisHelper.GetAllItemsFromSortedSet<List<UserPictureInfo>>(key, FunctionTypeEnum.UserHeadImage, 0);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (UserPictureInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Path))
                            info.Path = RedisTool.UserImgwebBase + info.Path;
                        else
                            info.Path = string.Empty;
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 获取用户资料图片不过滤
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns></returns>
        public static List<UserPapersPictureInfo> GetUserPapersPictureListNoFilter(string guid, bool fullpath, string picturetype)
        {
            string key = KeyManager.GetUserPapersPictureKey(guid, picturetype);

            var list = RedisHelper.GetAllItemsFromSortedSet<List<UserPapersPictureInfo>>(key, FunctionType.ZuShouPapersPicture, 0);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (UserPapersPictureInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Path))
                            info.Path = RedisTool.ImgwebBase + info.Path;
                        else
                            info.Path = string.Empty;
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获取商品图片不过滤
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns></returns>
        public static List<GoodsInfo> GetGoodsPictureListNoFilter(string guid, bool fullpath, string picturetype)
        {
            string key = KeyManager.GetGoodsPictureKey(guid, picturetype);

            var list = RedisHelper.GetAllItemsFromSortedSet<List<GoodsInfo>>(key, FunctionType.ZuShouGoodsPicture, 0);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (GoodsInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Path))
                            info.Path = RedisTool.ImgwebBase + info.Path;
                        else
                            info.Path = string.Empty;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取品牌图片不过滤
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <returns></returns>
        public static List<BrandPictuteInfo> GetBrandPictureListNoFilter(string guid, bool fullpath)
        {
            string key = KeyManager.GetBrandPictureKey(guid);

            var list = RedisHelper.GetAllItemsFromSortedSet<List<BrandPictuteInfo>>(key, FunctionType.ZuShouBrandPicture, 0);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (BrandPictuteInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Path))
                            info.Path = RedisTool.ImgwebBase + info.Path;
                        else
                            info.Path = string.Empty;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取广告图片不过滤
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <returns></returns>
        public static List<AdvertisementPictureInfo> GetAdvertisementPictureListNoFilter(string guid, bool fullpath)
        {
            string key = KeyManager.GetAdvertisementKey(guid);

            var list = RedisHelper.GetAllItemsFromSortedSet<List<AdvertisementPictureInfo>>(key, FunctionType.ZuShouAdvertisement, 0);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (AdvertisementPictureInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Path))
                            info.Path = RedisTool.ImgwebBase + info.Path;
                        else
                            info.Path = string.Empty;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 返回商品图片的第一张图片
        /// </summary>
        /// <param name="guid">商品的Guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">返回图片类型</param>
        /// <returns></returns>
        public static string GetFirstGoodsPicture(string guid, bool fullpath, string picturetype)
        {
            string key = KeyManager.GetGoodsPictureKey(guid, picturetype);

            var list = RedisHelper.GetAllItemsFromSortedSet<List<GoodsInfo>>(key, FunctionType.ZuShouGoodsPicture, 0);
            if (list != null && list.Count > 0)
            {
                var goodinfo = list.First();
                if (!string.IsNullOrEmpty(goodinfo.Path))
                    return RedisTool.ImgwebBase + goodinfo.Path;
            }
            return string.Empty;
        }


        /// <summary>
        /// 返回所有图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="fullpath"></param>
        /// <param name="picturetype"></param>
        /// <returns></returns>
        public static List<GoodsInfo> GetGoodsPictureList(string guid, bool fullpath, string picturetype)
        {
            string key = KeyManager.GetUserPapersPictureKey(guid, picturetype);

            List<GoodsInfo> list = GoodsPictureFilter(RedisHelper.GetAllItemsFromSortedSet<List<GoodsInfo>>(key, FunctionType.ZuShouGoodsPicture, 0));
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (GoodsInfo info in list)
                    {

                        if (!string.IsNullOrEmpty(info.Path))
                            info.Path = RedisTool.ImgwebBase + info.Path;
                        else
                            info.Path = string.Empty;
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 过滤图片列表，将新图片(没有缩略图的)去掉
        /// </summary>
        /// <param name="list">图片列表</param>
        /// <returns>过滤后的图片列表</returns>
        private static List<GoodsInfo> GoodsPictureFilter(List<GoodsInfo> list)
        {
            var result = new List<GoodsInfo>();
            if (list != null && list.Count > 0)
            {
                var linqresult = from item in list
                                 where item.IsNew == 0
                                 orderby item.Id ascending
                                 select item;
                result.AddRange(linqresult);
                return result;
            }
            return null;
        }




    }
}
