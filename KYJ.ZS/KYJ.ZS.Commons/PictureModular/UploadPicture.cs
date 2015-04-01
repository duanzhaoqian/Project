using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using KYJ.ZS.Commons.Common;
using ServiceStack;

namespace KYJ.ZS.Commons.PictureModular
{
    public class UploadPicture
    {

        #region 用户图片


        /// <summary>
        /// 保存用户图片
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="isremove">是否移除</param>
        /// <param name="guid">用户guid</param>
        /// <param name="picturetype">用户图片类型</param>
        /// <returns>图片地址</returns>
        public static string SaveUserPicture(HttpPostedFile file, bool isremove, string guid, string picturetype)
        {
            string result = string.Empty;
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetUserPictureBasePath(guid);

                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);


                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                string fullpath = savepath + filename;

                file.SaveAs(fullpath);

                if (File.Exists(fullpath))
                {
                    var info = new UserPictureInfo
                    {
                        Id = RedisHelper.GetNextSequence<UserPictureInfo>(FunctionTypeEnum.Identity, 0),//文件存储ID
                        Path = logicpath + filename,//文件路径
                        PictureType = picturetype, //类型
                        FileName = filename//文件名
                    };

                    if (picturetype == UserPictureType.LOGO.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".50_50.jpg", 50, 50, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".115_115.jpg", 115, 115, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".260_260.jpg", 260, 260, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else if (picturetype == UserPictureType.IDCARD.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".130_100.jpg", 130, 100, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".180_123.jpg", 180, 123, "jiji", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".80_80.jpg", 80, 80, "logo", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else if (picturetype == UserPictureType.PASSPORT.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".260_260.jpg", 260, 260, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".300_300.jpg", 300, 300, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".240_150.jpg", 240, 150, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    else if (picturetype == UserPictureType.MERCHANTLOGO.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".136_136.jpg", 136, 136, "CUT", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".98_34.jpg", 98, 34, "zoom", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    string key = KeyManager.GetUserPictureKey(guid, picturetype);

                    if (isremove)
                    {
                        RedisHelper.RemoveAllFromList(key, FunctionTypeEnum.UserHeadImage, 0);
                    }
                    RedisHelper.AddItemToSortedSet(info, key, 0, null, FunctionTypeEnum.UserHeadImage, 0);

                    int num = RedisHelper.GetSortedSetCount<UserPictureInfo>(key, FunctionTypeEnum.UserHeadImage, 0);

                    result = GetPicture.GetUserPictureWebPath(guid) + filename + "," + info.Id + "," + filename + "," + num;
                }
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "SaveUserPicture--保存用户图片" + ex + " ：" + DateTime.Now);
            }
            return result;
        }


        /// <summary>
        /// 保存用户图片缩略图
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="filename"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="ow"></param>
        /// <param name="oh"></param>
        public static string SaveUserCutPicture(string guid, string filename, int w, int h, int x, int y, int ow, int oh)
        {
            //获取临时图片
            if (filename != null && !string.IsNullOrEmpty(filename))
            {
                //创建新图片路径
                string savepath = GetPicture.GetUserPictureBasePath(guid);
                string logicpath = GetPicture.GetPicturePath(guid);
                //string newpath = savepath + filename;
                string newfilename = GetPicture.GetRandomFileName(6);
                string newpath = savepath + newfilename;
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }

                string backPath = RedisTool.UserImgPathBase + logicpath + filename;
                Thumbnail.SaveCutPic(backPath, newpath, 0, 0, w, h, x, y, ow, oh);


                Thumbnail.MakeThumbnail(newpath, newpath + ".50_50.jpg", 50, 50, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(newpath, newpath + ".115_115.jpg", 115, 115, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(newpath, newpath + ".260_260.jpg", 260, 260, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);

                //string tempkey = KeyManager.GetUserPictureKey(guid, UserPictureType.LOGO.ToString());
                //Redis.RemoveAllFromList(tempkey);

                if (File.Exists(newpath))
                {
                    var info = new UserPictureInfo
                    {
                        Id = RedisHelper.GetNextSequence<UserPictureInfo>(FunctionTypeEnum.Identity, 0),
                        Path = logicpath + newfilename,
                        PictureType = UserPictureType.LOGO.ToString(),
                        FileName = newfilename
                    };

                    string key = KeyManager.GetUserPictureKey(guid,
                        UserPictureType.LOGO.ToString());
                    RedisHelper.RemoveAllFromList(key, FunctionTypeEnum.UserHeadImage, 0);
                    RedisHelper.AddItemToSortedSet(info, key, 0, null, FunctionTypeEnum.UserHeadImage, 0);
                }


                //删除原图和原图的缩略图
                if (File.Exists(backPath))
                {
                    try
                    {
                        var thumbnailFileName5050 = backPath + ".50_50.jpg";
                        var thumbnailFileName115115 = backPath + ".115_115.jpg";
                        var thumbnailFileName260260 = backPath + ".260_260.jpg";
                        File.Delete(backPath);
                        File.Delete(thumbnailFileName5050);
                        File.Delete(thumbnailFileName115115);
                        File.Delete(thumbnailFileName260260);
                    }
                    catch
                    {
                    }
                }

                string result = "result:\"" + newfilename + "\"";
                return result;
            }
            return "error";
        }



        public static string SaveUserPortraitRdies(string guid, string filename, string picturetype)
        {
            string result = string.Empty;
            try
            {
                //删除同类型的图片
                DeleteUserPicture(guid, picturetype);

                string logicpath = GetPicture.GetPicturePath(guid);

                var info = new UserPictureInfo
                {
                    Id = RedisHelper.GetNextSequence<UserPictureInfo>(FunctionTypeEnum.Identity, 0),//文件存储ID
                    Path = logicpath + filename,//文件路径
                    PictureType = picturetype,//类型
                    FileName = filename//文件名
                };

                string key = KeyManager.GetUserPictureKey(guid, picturetype);

                RedisHelper.RemoveAllFromList(key, FunctionTypeEnum.UserHeadImage, 0);

                RedisHelper.AddItemToSortedSet(info, key, 0, null, FunctionTypeEnum.UserHeadImage, 0);

                result = "result:\"true\"";
            }
            catch (Exception ex)
            {
                //RecordLog.RecordException("祝志华", "SaveUserPortraitRdies--保存用户图片Redis信息", ex);
                PicLog.Log("Error：", "Error", "SaveUserPortraitRdies--保存用户图片Redis信息：" + ex + "：" + DateTime.Now);
            }
            return result;
        }


        public static string SaveUserPortraitPicture(HttpPostedFile file, string guid, string picturetype)
        {
            string result = string.Empty;
            try
            {
                //GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetUserPictureBasePath(guid);

                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }

                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                string fullpath = savepath + filename;

                file.SaveAs(fullpath);

                if (File.Exists(fullpath))
                {
                    if (picturetype == UserPictureType.LOGO.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".50_50.jpg", 50, 50, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".115_115.jpg", 115, 115, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".260_260.jpg", 260, 260, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    #region 保存图片信息到REDIS

                    #endregion

                    result = GetPicture.GetUserPictureWebPath(guid) + filename + "," + filename;
                }
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "SaveUserPortraitPicture--保存用户图片：" + ex + "：" + DateTime.Now);
            }
            return result;
        }


        /// <summary>
        /// 删除指定图片ID 的用户图片
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="id">图片id</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns>是否删除</returns>
        public static bool DeleteUserPicture(string guid, int id, string picturetype)
        {
            bool result = false;
            try
            {
                List<UserPictureInfo> list = GetPicture.GetUserPictureInfos(guid, false, picturetype);
                if (list != null && list.Count > 0)
                {
                    foreach (UserPictureInfo info in list)
                    {
                        if (info.Id == id)
                        {
                            string key = KeyManager.GetUserPictureKey(guid, picturetype);

                            string[] thumbnails = null;

                            if (picturetype == UserPictureType.LOGO.ToString())
                            {
                                thumbnails = new[] { "50_50", "115_115", "260_260" };
                            }
                            else if (picturetype == UserPictureType.IDCARD.ToString())
                            {
                                thumbnails = new[] { "130_100", "80_80" };
                            }

                            if (DeleteImages(RedisTool.UserImgPathBase + info.Path, thumbnails))
                            {
                                result = RedisHelper.RemoveItemFromSortedSet(key, info, FunctionTypeEnum.UserHeadImage, 0);
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "DeleteUserPicture--删除用户图片" + ex + " ：" + DateTime.Now);
                result = false;
            }
            return result;
        }


        /// <summary>
        /// 删除用户图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype">图片类型</param>
        /// <returns>是否删除</returns>
        public static bool DeleteUserPicture(string guid, string picturetype)
        {
            bool result = false;
            try
            {
                var userinfo = GetPicture.GetUserPictureInfo(guid, false, picturetype);

                if (userinfo != null && userinfo.Path != null)
                {
                    string key = KeyManager.GetGoodsPictureKey(guid, picturetype);
                    string[] thumbnails = null;
                    if (picturetype == UserPictureType.LOGO.ToString())
                    {
                        thumbnails = new[] { "50_50", "80_80", "180_180", "300_300", "1000_800" };
                    }
                    else if (picturetype == UserPictureType.IDCARD.ToString())
                    {
                        thumbnails = new[] { "130_100", "80_80" };
                    }
                    if (DeleteImages(RedisTool.UserImgPathBase + userinfo.Path, thumbnails))
                    {
                        result = RedisHelper.RemoveItemFromSortedSet(key, userinfo, FunctionTypeEnum.UserHeadImage, 0);
                    }

                }
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "DeleteUserPicture--删除用户图片" + ex + " ：" + DateTime.Now);
                result = false;
            }
            return result;
        }


        /// <summary>
        /// 删除图片以及缩略图
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <param name="thumbnails">缩略图尺寸</param>
        /// <returns></returns>
        public static bool DeleteImages(string path, string[] thumbnails)
        {
            try
            {
                var file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
                file = new FileInfo(path + ".136_136.jpg");
                if (file.Exists)
                {
                    file.Delete();
                }
                if (thumbnails != null)
                {
                    for (int i = 0; i < thumbnails.Length; i++)
                    {
                        string nextpath = path + "." + thumbnails[i] + ".jpg";
                        file = new FileInfo(nextpath);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "DeleteImages--删除图片以及缩略图：" + ex + "：" + DateTime.Now);
                return false;
            }
        }


        #endregion

        #region 品牌图片
        /// <summary>
        /// 保存商品图片到文件目录
        /// </summary>
        /// <param name="file"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string SaveBrandPicture(HttpPostedFile file, string guid)
        {
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetPictureBasePath(guid);

                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();

                string fullpath = savepath + filename;
                file.SaveAs(fullpath);
                return SaveBrandImage(guid, logicpath, filename, fullpath);
            }
            catch (Exception ex)
            {

                PicLog.Log("Error：", "Error", "SaveUserPapersPicture--商品图片：" + ex + "：" + DateTime.Now);
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存商品图片到Redis并切图
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="logicpath"></param>
        /// <param name="filename"></param>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string SaveBrandImage(string guid, string logicpath, string filename, string fullpath)
        {
            if (File.Exists(fullpath))
            {
                Thumbnail.MakeThumbnail(fullpath, fullpath + ".160_160.jpg", 160, 160, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(fullpath, fullpath + ".98_34.jpg", 160, 160, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);

                BrandPictuteInfo info = new BrandPictuteInfo();
                info.Id = RedisHelper.GetNextSequence<BrandPictuteInfo>(FunctionTypeEnum.Identity, 0);
                info.Path = logicpath + filename;

                string key = KeyManager.GetBrandPictureKey(guid);

                double score = 1;

                IDictionary<BrandPictuteInfo, double> highestitem =
                    RedisHelper.GetRangeWithScoresFromSortedSetByHighestScore<BrandPictuteInfo>(key,
                        FunctionType.ZuShouBrandPicture, 0);
                if (highestitem != null && highestitem.Count > 0)
                {
                    score = highestitem.Values.First();
                    score = Math.Ceiling(score) == score ? score + 1 : Math.Ceiling(score);
                }

                RedisHelper.AddItemToSortedSet(info, key, score, null,
                    FunctionType.ZuShouBrandPicture, 0);

                var num = RedisHelper.GetSortedSetCount<BrandPictuteInfo>(key, FunctionType.ZuShouBrandPicture, 0);
                return GetPicture.GetPictureWebPath(guid) + filename + ".160_160.jpg," + info.Id;

            }
            return string.Empty;
        }

        /// <summary>
        /// 删除品牌图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteBrandPicture(string guid, int id)
        {
            var result = false;
            List<BrandPictuteInfo> list = GetPicture.GetBrandPictureListNoFilter(guid, false);
            if (list != null && list.Count > 0)
            {
                foreach (BrandPictuteInfo info in list)
                {
                    if (info.Id == id)
                    {
                    string key = KeyManager.GetBrandPictureKey(guid);
                    if (DeleteImages(RedisTool.ImgpathBase + info.Path, Thumbnail.GetBrandPictureThumbnail()))
                    {
                    result = RedisHelper.RemoveItemFromSortedSet(key, info,
                        FunctionType.ZuShouBrandPicture, 0);
                    }
                    break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region 广告图片

        public static string SaveAdvertisementPicture(HttpPostedFile file, string guid)
        {
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetPictureBasePath(guid);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                string fullpath = savepath + filename;
                file.SaveAs(fullpath);
                //保存图片到Redis
                string key = KeyManager.GetAdvertisementKey(guid);
                if (File.Exists(fullpath))
                {
                    RedisHelper.RemoveAllFromList(key, FunctionType.ZuShouAdvertisement, 0);
                    var info = new AdvertisementPictureInfo
                    {
                        Id = RedisHelper.GetNextSequence<AdvertisementPictureInfo>(FunctionTypeEnum.Identity, 0),
                        Path = logicpath + filename
                    };
                    double score = 1;

                    IDictionary<AdvertisementPictureInfo, double> highestitem =
                        RedisHelper.GetRangeWithScoresFromSortedSetByHighestScore<AdvertisementPictureInfo>(key,
                            FunctionType.ZuShouAdvertisement, 0);
                    if (highestitem != null && highestitem.Count > 0)
                    {
                        score = highestitem.Values.First();
                        score = Math.Ceiling(score) == score ? score + 1 : Math.Ceiling(score);
                    }
                    RedisHelper.AddItemToSortedSet(info, key, score, null,
                    FunctionType.ZuShouAdvertisement, 0);
                    return GetPicture.GetPictureWebPath(guid) + filename + "," + info.Id;
                }
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "SaveGoodsPicture--广告图片：" + ex + "：" + DateTime.Now);
            }
            return string.Empty;
        }

        /// <summary>
        ///删除广告图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteAdvertisementPicture(string guid, int id)
        {
            var result = false;
            List<AdvertisementPictureInfo> list = GetPicture.GetAdvertisementPictureListNoFilter(guid, false);
            if (list != null && list.Count > 0)
            {
                foreach (AdvertisementPictureInfo info in list)
                {
                    if (info.Id == id)
                    {
                        string key = KeyManager.GetAdvertisementKey(guid);
                        if (DeleteImages(RedisTool.ImgpathBase + info.Path, null))
                        {
                            result = RedisHelper.RemoveItemFromSortedSet(key, info,
                                FunctionType.ZuShouAdvertisement, 0);
                        }
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region 商品图片
        public static string SaveGoodsPicture(HttpPostedFile file, string guid, string picturetype)
        {
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetPictureBasePath(guid);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                string fullpath = savepath + filename;
                file.SaveAs(fullpath);
                return SaveGoodsImage(guid, picturetype, logicpath, filename, fullpath);
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "SaveUserPapersPicture--商品图片：" + ex + "：" + DateTime.Now);
            }
            return string.Empty;
        }

        /// <summary>
        /// 通过文件路径保存商品图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="logicpath"></param>
        /// <param name="filename"></param>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string SaveGoodsImage(string guid, string picturetype, string logicpath, string filename, string fullpath,int size = 0)
        {
            string key = KeyManager.GetGoodsPictureKey(guid, picturetype);
            if (File.Exists(fullpath))
            {
                //切商品展示列表图
                if (GoodsType.SHOW.ToString().Equals(picturetype))
                    Thumbnail.MakeThumbnail(fullpath, fullpath + ".136_136.jpg", 136, 136, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                ////切商品详细信息图
                //if(GoodsType.INFO.ToString().Equals(picturetype))
                //    Thumbnail.MakeThumbnail(fullpath, fullpath + ".136_136.jpg", 136, 136, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
                //闲置商品图
                if (GoodsType.FREE.ToString().Equals(picturetype))
                {
                    Thumbnail.MakeThumbnail(fullpath, fullpath + ".136_136.jpg", 136, 136, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
                    RedisHelper.RemoveAllFromList(key, FunctionType.ZuShouGoodsPicture, 0);
                }

                //发送切图消息
                MsgQueue.SendMessage.SendPictureMessage(guid, picturetype, string.Empty, fullpath);

                GoodsInfo info = new GoodsInfo();
                info.Id = RedisHelper.GetNextSequence<GoodsInfo>(FunctionTypeEnum.Identity, 0);
                info.Path = logicpath + filename;
                info.PictureType = picturetype;
                //if (UserPapersPictureType.XXX资料1.ToString().Equals(picturetype))
                //    info.IsNew = 0;
                //else
                //    info.IsNew = 1;
                ////是否是小区图片
                //info.IsVillage = 0;
                //info.Vid = 0;
                

                double score = 1;

                IDictionary<GoodsInfo, double> highestitem =
                    RedisHelper.GetRangeWithScoresFromSortedSetByHighestScore<GoodsInfo>(key,
                        FunctionType.ZuShouGoodsPicture, 0);
                if (highestitem != null && highestitem.Count > 0)
                {
                    score = highestitem.Values.First();
                    score = Math.Ceiling(score) == score ? score + 1 : Math.Ceiling(score);
                }

                RedisHelper.AddItemToSortedSet(info, key, score, null,
                    FunctionType.ZuShouGoodsPicture, 0);

                var num = RedisHelper.GetSortedSetCount<GoodsInfo>(key, FunctionType.ZuShouGoodsPicture, 0);
                var type = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                if (GoodsType.INFO.ToString().Equals(picturetype))
                    return GetPicture.GetPictureWebPath(guid) + filename + "," + info.Id + "," + filename + "," + type +
                           "," + size;
                return GetPicture.GetPictureWebPath(guid) + filename + ".136_136.jpg," + info.Id ;
            }
            return string.Empty;
        }


        /// <summary>
        ///删除商品图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="picturetype"></param>
        /// <returns></returns>
        public static bool DeleteGoodsPicture(string guid, int id, string picturetype)
        {
            var result = false;
            List<GoodsInfo> list = GetPicture.GetGoodsPictureListNoFilter(guid, false, picturetype);
            if (list != null && list.Count > 0)
            {
                foreach (GoodsInfo info in list)
                {
                    if (info.Id == id)
                    {
                        string key = KeyManager.GetGoodsPictureKey(guid, picturetype);
                        if (DeleteImages(RedisTool.ImgpathBase + info.Path, Thumbnail.GetGoodsPictureThumbnail(picturetype)))
                        {
                            result = RedisHelper.RemoveItemFromSortedSet(key, info,
                                FunctionType.ZuShouGoodsPicture, 0);
                        }
                        break;
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 删除商品列表图
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <returns></returns>
        public static bool DeleteGoodsListPicture(string guid, string picturetype)
        {
            try
            {

                string key = KeyManager.GetGoodsPictureKey(guid, picturetype);
                List<GoodsInfo> list = GetPicture.GetGoodsPictureList(guid, false, picturetype);
                if (list != null && list.Count > 0)
                {
                    foreach (var info in list)
                    {
                        if (DeleteGoodsImagesList(RedisTool.ImgpathBase + info.Path, Thumbnail.GetGoodsPictureThumbnail(picturetype)))
                        {
                            RedisHelper.RemoveItemFromSortedSet(key, info,
                                FunctionTypeEnum.NewHouseBasicDataPicture, 0);
                        }

                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return true;
        }

        /// <summary>
        /// 删除列表图
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumbnails"></param>
        /// <returns></returns>
        public static bool DeleteGoodsImagesList(string path, string[] thumbnails)
        {
            try
            {
                if (thumbnails != null)
                {
                    for (int i = 0; i < thumbnails.Length; i++)
                    {
                        string nextpath = path + "." + thumbnails[i] + ".jpg";
                        FileInfo file = new FileInfo(nextpath);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region 用户材料图片
        public static string SaveUserPapersPicture(HttpPostedFile file, string guid, string picturetype)
        {
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetPictureBasePath(guid);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                string fullpath = savepath + filename;
                file.SaveAs(fullpath);
                return SaveImage(guid, picturetype, logicpath, filename, fullpath);
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "SaveUserPapersPicture--用户资料图片：" + ex + "：" + DateTime.Now);
            }
            return string.Empty;
        }


        /// <summary>
        /// 通过文件路径保存图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="logicpath"></param>
        /// <param name="filename"></param>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string SaveImage(string guid, string picturetype, string logicpath, string filename, string fullpath)
        {
            if (File.Exists(fullpath))
            {
                Thumbnail.MakeThumbnail(fullpath, fullpath + ".180_123.jpg", 180, 123, "jiji", System.Drawing.Imaging.ImageFormat.Jpeg);

                UserPapersPictureInfo info = new UserPapersPictureInfo();
                info.Id = RedisHelper.GetNextSequence<UserPapersPictureInfo>(FunctionTypeEnum.Identity, 0);
                info.Path = logicpath + filename;
                info.PictureType = picturetype;
                //if (UserPapersPictureType.XXX资料1.ToString().Equals(picturetype))
                //    info.IsNew = 0;
                //else
                //    info.IsNew = 1;
                ////是否是小区图片
                //info.IsVillage = 0;
                //info.Vid = 0;
                string key = KeyManager.GetUserPapersPictureKey(guid, picturetype);

                double score = 1;

                IDictionary<UserPapersPictureInfo, double> highestitem =
                    RedisHelper.GetRangeWithScoresFromSortedSetByHighestScore<UserPapersPictureInfo>(key,
                        FunctionType.ZuShouPapersPicture, 0);
                if (highestitem != null && highestitem.Count > 0)
                {
                    score = highestitem.Values.First();
                    score = Math.Ceiling(score) == score ? score + 1 : Math.Ceiling(score);
                }

                RedisHelper.AddItemToSortedSet(info, key, score, null,
                    FunctionType.ZuShouPapersPicture, 0);

                var num = RedisHelper.GetSortedSetCount<UserPapersPictureInfo>(key, FunctionType.ZuShouPapersPicture, 0);

                return GetPicture.GetPictureWebPath(guid) + filename + ".180_123.jpg" + "," + info.Id + "," + num;
            }
            return string.Empty;
        }


        /// <summary>
        /// 删除用户资料图片
        /// </summary>
        /// <param name="guid">房源guid</param>
        /// <param name="id">图片id</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns>是否删除</returns>
        public static bool DeleteUserPapersPicture(string guid, int id, string picturetype)
        {

            bool result;
            try
            {
                //删除类型数据
                result = DeletePicture(guid, id, picturetype);
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "DeleteRentHousePicture--删除用户资料图片" + ex + " ：" + DateTime.Now);
                result = false;
            }
            return result;
        }

        /// <summary>
        ///删除用户资料图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="picturetype"></param>
        /// <returns></returns>
        private static bool DeletePicture(string guid, int id, string picturetype)
        {
            var result = false;
            List<UserPapersPictureInfo> list = GetPicture.GetUserPapersPictureListNoFilter(guid, false, picturetype);
            if (list != null && list.Count > 0)
            {
                foreach (UserPapersPictureInfo info in list)
                {
                    if (info.Id == id)
                    {
                        string key = KeyManager.GetUserPapersPictureKey(guid, picturetype);
                        if (DeleteImages(RedisTool.ImgpathBase + info.Path, Thumbnail.GetUserPapersPictureThumbnail(picturetype)))
                        {
                            result = RedisHelper.RemoveItemFromSortedSet(key, info,
                                FunctionType.ZuShouPapersPicture, 0);
                        }
                        break;
                    }
                }
            }
            return result;
        }
        #endregion





    }
}
