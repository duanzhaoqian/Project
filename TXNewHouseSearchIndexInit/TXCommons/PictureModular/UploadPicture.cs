using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Diagnostics;
using ServiceStack;

namespace TXCommons.PictureModular
{
    public class UploadPicture
    {
        #region 租房图片
        /// <summary>
        /// 保存楼盘图片
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="guid">房源guid</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns>上传图片地址</returns>
        public static string SavePremisesPicture(HttpPostedFile file, string guid, string picturetype, int cityId)
        {
            string result = string.Empty;
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetPictureBasePath(guid);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                string fullpath = savepath + filename;
                file.SaveAs(fullpath);

                if (File.Exists(fullpath))
                {
                    //发送切图消息 ，原有是根据redis获取生成，现在改成fullpath来切图
                    MsgQueue.SendMessage.SendPictureMessage(guid, "Premises", picturetype, string.Empty, cityId, fullpath);
                    PremisesPictureInfo info = SavePictureToRedis(guid, picturetype, cityId, logicpath, filename, string.Empty, string.Empty);
                    result = GetPicture.GetPictureWebPath(guid) + filename + "," + info.ID;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return result;
        }

        /// <summary>
        /// 保存图片到redis
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="picturetype">图片类型</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="logicpath">保存路径</param>
        /// <param name="filename">图片名称</param>
        /// <param name="title">图片标题</param>
        /// <param name="desc">图片描述</param>
        /// <returns></returns>
        public static PremisesPictureInfo SavePictureToRedis(string guid, string picturetype, int cityId, string logicpath, string filename, string title, string desc)
        {
            var id = Redis.GetNextSequence<PremisesPictureInfo>(FunctionTypeEnum.Identity, 0);
            PremisesPictureInfo info = new PremisesPictureInfo();
            info.ID = id;
            info.Path = logicpath + filename;
            info.PictureType = picturetype;
            info.IsForce = 0;
            info.IsNew = 0;
            info.Title = title;
            info.Desc = desc;
            info.Hits = "0";
            if (picturetype == PremisesPictureType.PremisesLIST.ToString() || picturetype == PremisesPictureType.Logo.ToString())
            {
                info.IsNew = 0;
            }
            info.CreateTime = DateTime.Now;
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            double score = 1;

            Redis.AddItemToSortedSet<PremisesPictureInfo>(info, key, score, null, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
            return info;
        }

        /// <summary>
        /// 保存用户图片
        /// </summary>
        /// <param name="file"></param>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static string SaveUserPicture(HttpPostedFile file, string guid, string picturetype, int cityId)
        {
            string result = string.Empty;
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetPictureUserPath(guid);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                string fullpath = savepath + filename;
                file.SaveAs(fullpath);
                sw.Stop();

                if (File.Exists(fullpath))
                {
                    if (picturetype == UserPictureType.LOGO.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".50_50.jpg", 50, 50, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".80_80.jpg", 80, 80, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".180_180.jpg", 180, 180, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".300_300.jpg", 300, 300, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".1000_800.jpg", 1000, 800, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    if (picturetype == UserPictureType.Identification.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".180_130.jpg", 130, 100, "leguan", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }


                    UserPictureInfo info = new UserPictureInfo();
                    info.ID = Redis.GetNextSequence<UserPictureInfo>(FunctionTypeEnum.Identity, 0);
                    info.Path = logicpath + filename;
                    info.PictureType = picturetype;
                    info.FileName = filename;

                    string key = KeyManager.GetUserPictureKey(guid, picturetype);

                    double score = 1;

                    sw.Restart();

                    Redis.AddItemToSortedSet<UserPictureInfo>(info, key, score, null, FunctionTypeEnum.NewHouseUserInfo, cityId);
                    sw.Stop();

                    result = GetPicture.GetPictureUserWebPath(guid) + filename + "," + info.ID;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                FileInfo file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Delete();
                }
                file = new FileInfo(path + ".180_130.jpg");
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


                throw ex;
            }
        }
        /// <summary>
        /// 删除列表图
        /// </summary>
        /// <param name="path"></param>
        /// <param name="thumbnails"></param>
        /// <returns></returns>
        public static bool DeleteImagesList(string path, string[] thumbnails)
        {
            try
            {
                FileInfo file = new FileInfo(path);

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


                throw ex;
            }
        }
        #endregion

        #region 文档相关

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="guid">guid</param>
        /// <param name="documenttype">文档类型</param>
        /// <returns>文档地址</returns>
        public static string SaveDocument(System.Web.HttpPostedFile file, string guid, string documenttype)
        {
            string result = string.Empty;
            try
            {
                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetDocumentBasePath(guid);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                string filename = file.FileName;//GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                string fullpath = savepath + filename;
                file.SaveAs(fullpath);

                if (File.Exists(fullpath))
                {
                    string key = KeyManager.GetDocumentKey(guid, documenttype);

                    DocumentInfo info = new DocumentInfo();

                    var list = Redis.GetAllItemsFromSortedSet<List<DocumentInfo>>(key, FunctionTypeEnum.DocumentInfo, 0);

                    if (list != null && list.Count > 0)
                        info.ID = list[0].ID;
                    else
                        info.ID = Redis.GetNextSequence<DocumentInfo>(FunctionTypeEnum.Identity, 0);

                    info.Path = logicpath + filename;

                    info.DocumentType = documenttype;

                    info.FileName = filename;//.Split(new char[] { '.' })[0];

                    Redis.RemoveAllFromList(key, FunctionTypeEnum.DocumentInfo, 0);

                    Redis.AddItemToSortedSet<DocumentInfo>(info, key, 0, null, FunctionTypeEnum.DocumentInfo, 0);

                    result = GetPicture.GetDocumentWebPath(guid) + filename + "," + filename + "," + info.ID;
                }
            }
            catch (Exception ex)
            {
                //RecordLog.RecordException("祝志华", "SaveDocument--保存文档", ex);
                throw ex;
            }
            return result;
        }

        #endregion


        /// <summary>
        /// 删除租房图片
        /// </summary>
        /// <param name="keyguid">房源guid</param>
        /// <param name="id">图片id</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns>是否删除</returns>
        public static bool DeleteRentHousePicture(string guid, int id, string picturetype, int cityId)
        {
            bool result = false;
            try
            {
                List<PremisesPictureInfo> list = GetPicture.GetPremisesPictureListNoFilter(guid, false, picturetype, cityId);
                if (list != null && list.Count > 0)
                {
                    foreach (PremisesPictureInfo info in list)
                    {
                        if (info.ID == id)
                        {
                            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);
                            if (DeleteImages(Redis.IMGPATH_BASE + info.Path, Thumbnail.GetRentHousePictureThumbnail(picturetype)))
                            {
                                result = Redis.RemoveItemFromSortedSet<PremisesPictureInfo>(key, info, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 删除租房未保存图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static bool DeleteNoSavePicture(string guid, string picturetype, int cityId)
        {
            bool result = false;
            try
            {
                List<PremisesPictureInfo> list = GetPicture.GetPremisesNoSavePiceureList(guid, false, picturetype, cityId);
                if (list != null && list.Count > 0)
                {
                    foreach (PremisesPictureInfo info in list)
                    {
                        if (info.IsNew == 1)
                        {
                            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);
                            if (DeleteImages(Redis.IMGPATH_BASE + info.Path, Thumbnail.GetRentHousePictureThumbnail(picturetype)))
                            {
                                result = Redis.RemoveItemFromSortedSet<PremisesPictureInfo>(key, info, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 删除列表图
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static bool DeleteRentHousePicture(string guid, string picturetype, int cityId)
        {
            bool result = false;
            try
            {

                string key = KeyManager.GetPremisesPictureKey(guid, picturetype);
                List<PremisesPictureInfo> list = TXCommons.PictureModular.GetPicture.GetPremisesPictureList(guid, false, picturetype, cityId);
                if (list != null && list.Count > 0)
                {
                    foreach (PremisesPictureInfo info in list)
                    {
                        if (DeleteImagesList(Redis.IMGPATH_BASE + info.Path, Thumbnail.GetRentHousePictureThumbnail(picturetype)))
                        {

                            Redis.RemoveItemFromSortedSet<PremisesPictureInfo>(key, info, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
                        }

                    }
                    result = true;
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="id"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static bool DeleteUserPicture(string guid, int id, string picturetype, int cityId)
        {
            bool result = false;
            try
            {
                List<UserPictureInfo> list = GetPicture.GetUserPictureInfos(guid, false, picturetype);
                if (list != null && list.Count > 0)
                {
                    foreach (UserPictureInfo info in list)
                    {
                        if (info.ID == id)
                        {
                            string key = KeyManager.GetUserPictureKey(guid, picturetype);

                            string[] thumbnails = null;

                            if (picturetype == TXCommons.PictureModular.UserPictureType.LOGO.ToString())
                            {
                                thumbnails = new string[] { "50_50", "80_80", "180_180", "300_300", "1000_800" };
                            }
                            else if (picturetype == UserPictureType.Identification.ToString())
                            {
                                thumbnails = Thumbnail.GetRentHousePictureThumbnail(picturetype);
                            }
                            if (DeleteImages(Redis.USER_IMGPATH_BASE + info.Path, thumbnails))
                            {
                                result = Redis.RemoveItemFromSortedSet<UserPictureInfo>(key, info, FunctionTypeEnum.NewHouseUserInfo, cityId);
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public static string SaveUserPortraitRdies(string guid, string filename, string picturetype)
        {
            string result = string.Empty;
            try
            {
                //删除同类型的图片
                DeleteUserPicture(guid, picturetype);

                string logicpath = GetPicture.GetPicturePath(guid);

                UserPictureInfo info = new UserPictureInfo();
                //文件存储ID
                info.ID = Redis.GetNextSequence<UserPictureInfo>(FunctionTypeEnum.Identity, 0);
                //文件路径
                info.Path = logicpath + filename;
                //类型
                info.PictureType = picturetype;
                //文件名
                info.FileName = filename;

                string key = KeyManager.GetUserPictureKey(guid, picturetype);

                Redis.RemoveAllFromList(key, FunctionTypeEnum.NewHouseUserInfo, 0);

                Redis.AddItemToSortedSet<UserPictureInfo>(info, key, 0, null, FunctionTypeEnum.NewHouseUserInfo, 0);

                result = "result:\"true\"";
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return result;
        }

        /// <summary>
        /// 删除用户图片
        /// </summary>
        /// <param name="keyguid">用户guid</param>
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
                    string key = KeyManager.GetUserPictureKey(guid, picturetype);
                    string[] thumbnails = null;
                    if (picturetype == TXCommons.PictureModular.UserPictureType.LOGO.ToString())
                    {
                        thumbnails = new string[] { "50_50", "80_80", "180_180", "300_300", "1000_800" };
                    }
                    if (DeleteImages(Redis.USER_IMGPATH_BASE + userinfo.Path, thumbnails))
                    {
                        result = Redis.RemoveItemFromSortedSet<UserPictureInfo>(key, userinfo, FunctionTypeEnum.NewHouseUserInfo, 0);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



        public static string SaveUserPortraitPicture(System.Web.HttpPostedFile file, string guid, string picturetype)
        {
            string result = string.Empty;
            try
            {
                //删除同类型的图片
                //DeleteUserPicture(guid, picturetype);

                string logicpath = GetPicture.GetPicturePath(guid);
                string savepath = GetPicture.GetUserPictureBasePath(guid);

                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }

                string filename = GetPicture.GetRandomFileName(6) + file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                string fullpath = savepath + filename;

                file.SaveAs(fullpath);

                if (File.Exists(fullpath))
                {
                    //UserPictureInfo info = new UserPictureInfo();
                    ////文件存储ID
                    //info.ID = Redis.GetNextSequence<UserPictureInfo>();
                    ////文件路径
                    //info.Path = logicpath + filename;
                    ////类型
                    //info.PictureType = picturetype;
                    ////文件名
                    //info.FileName = filename;

                    if (picturetype == UserPictureType.LOGO.ToString())
                    {
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".50_50.jpg", 50, 50, "logo", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".80_80.jpg", 80, 80, "logo", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".180_180.jpg", 180, 180, "logo", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".300_300.jpg", 300, 300, "logo", System.Drawing.Imaging.ImageFormat.Jpeg);
                        Thumbnail.MakeThumbnail(fullpath, fullpath + ".1000_800.jpg", 1000, 800, "logo", System.Drawing.Imaging.ImageFormat.Jpeg);
                    }


                    //string key = KeyManager.GetUserPictureKey(guid, picturetype);
                    //Redis.RemoveAllFromList(key);
                    //Redis.AddItemToSortedSet<UserPictureInfo>(info, key, 0, null);
                    result = GetPicture.GetUserPictureWebPath(guid) + filename + "," + filename;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 保存用户图片缩略图
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="ow"></param>
        /// <param name="oh"></param>
        public static void SaveUserCutPicture(string guid, string filename, int w, int h, int x, int y, int ow, int oh)
        {
            //获取临时图片
            //UserPictureInfo tempinfo = GetPicture.GetUserPictureInfo(guid, false, TXCommons.PictureModular.UserPictureType.LOGOTEMP.ToString());
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

                string backPath = Redis.USER_IMGPATH_BASE + logicpath + filename;
                Thumbnail.SaveCutPic(backPath, newpath, 0, 0, w, h, x, y, ow, oh);


                Thumbnail.MakeThumbnail(newpath, newpath + ".50_50.jpg", 50, 50, "logo",
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(newpath, newpath + ".80_80.jpg", 80, 80, "logo",
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(newpath, newpath + ".180_180.jpg", 180, 180, "logo",
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(newpath, newpath + ".300_300.jpg", 300, 300, "logo",
                    System.Drawing.Imaging.ImageFormat.Jpeg);
                Thumbnail.MakeThumbnail(newpath, newpath + ".1000_800.jpg", 1000, 800, "logo",
                    System.Drawing.Imaging.ImageFormat.Jpeg);


                //string tempkey = KeyManager.GetUserPictureKey(guid, UserPictureType.LOGO.ToString());
                //Redis.RemoveAllFromList(tempkey);
                if (File.Exists(newpath))
                {
                    UserPictureInfo info = new UserPictureInfo();
                    info.ID = Redis.GetNextSequence<UserPictureInfo>(FunctionTypeEnum.Identity, 0);
                    info.Path = logicpath + newfilename;// filename;
                    info.PictureType = TXCommons.PictureModular.UserPictureType.LOGO.ToString();
                    info.FileName = newfilename;//filename

                    string key = KeyManager.GetUserPictureKey(guid,
                        TXCommons.PictureModular.UserPictureType.LOGO.ToString());
                    Redis.RemoveAllFromList(key, FunctionTypeEnum.NewHouseUserInfo, 0);
                    Redis.AddItemToSortedSet<UserPictureInfo>(info, key, 0, null, FunctionTypeEnum.NewHouseUserInfo, 0);
                }

                //删除原图和原图的缩略图
                if (File.Exists(backPath))
                {
                    try
                    {
                        var thumbnailFileName5050 = backPath + ".50_50.jpg";
                        var thumbnailFileName8080 = backPath + ".80_80.jpg";
                        var thumbnailFileName180180 = backPath + ".180_180.jpg";
                        var thumbnailFileName300300 = backPath + ".300_300.jpg";
                        var thumbnailFileName1000800 = backPath + ".1000_800.jpg";
                        File.Delete(backPath);
                        File.Delete(thumbnailFileName5050);
                        File.Delete(thumbnailFileName8080);
                        File.Delete(thumbnailFileName180180);
                        File.Delete(thumbnailFileName300300);
                        File.Delete(thumbnailFileName1000800);
                    }
                    catch
                    {
                    }
                }



            }
        }



    }
}
