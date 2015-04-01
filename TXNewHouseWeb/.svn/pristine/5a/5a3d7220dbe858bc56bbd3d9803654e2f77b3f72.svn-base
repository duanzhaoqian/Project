using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;

namespace TXCommons.PictureModular
{
    public class GetPicture
    {
        private static object lockobject = new object();


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
        /// 图片绝对路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetPictureBasePath(string guid)
        {
            string result = Redis.IMGPATH_BASE + GetPicturePath(guid);
            return result;
        }

        /// <summary>
        /// 用户图片绝对路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetUserPictureBasePath(string guid)
        {
            string result = Redis.USER_IMGPATH_BASE + GetPicturePath(guid);
            return result;
        }

        /// <summary>
        /// 图片web路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetPictureWebPath(string guid)
        {
            string result = Redis.IMGWEB_BASE + GetPicturePath(guid);
            return result;
        }

        /// <summary>
        /// 用户图片web路径
        /// </summary>
        /// <param name="guid">guid</param>
        /// <returns></returns>
        public static string GetUserPictureWebPath(string guid)
        {
            string result = Redis.USER_IMGWEB_BASE + GetPicturePath(guid);
            return result;
        }
        /// <summary>
        /// 文档绝对路径
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GetDocumentBasePath(string guid)
        {
            string result = Redis.DOCUMENT_BASE + GetPicturePath(guid);
            return result;
        }

        /// <summary>
        /// 文档web路径
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string GetDocumentWebPath(string guid)
        {
            string result = Redis.DOCUMENT_WEB_BASE + GetPicturePath(guid);
            return result;
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
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                int index = random.Next(0, chars.Length);
                result += chars[index].ToString();
            }
            return result;
        }



        public static string GetPictureUserWebPath(string guid)
        {
            string result = Redis.USER_IMGWEB_BASE + GetPicturePath(guid);
            return result;
        }


        public static string GetPictureUserPath(string guid)
        {
            string result = Redis.USER_IMGPATH_BASE + GetPicturePath(guid);
            return result;

        }
        #region 文档
        /// <summary>
        /// 获取文档
        /// </summary>
        /// <param name="guid">guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="documenttype">文档类型</param>
        /// <returns></returns>
        public static DocumentInfo GetDocumentInfo(string guid, bool fullpath, string documenttype)
        {
            string key = KeyManager.GetDocumentKey(guid, documenttype);

            DocumentInfo result = new DocumentInfo();

            List<DocumentInfo> list = Redis.GetAllItemsFromSortedSet<List<DocumentInfo>>(key, FunctionTypeEnum.DocumentInfo, 0);

            if (list != null && list.Count > 0)
            {
                result = list[0];
                if (fullpath)
                {
                    result.Path = Redis.DOCUMENT_WEB_BASE + result.Path;
                }
            }

            return result;
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
            string key = KeyManager.GetUserPictureKey(guid, picturetype);

            UserPictureInfo result = new UserPictureInfo();

            List<UserPictureInfo> list = Redis.GetAllItemsFromSortedSet<List<UserPictureInfo>>(key, FunctionTypeEnum.NewHouseUserInfo, 0);

            if (list != null && list.Count > 0)
            {
                result = list[0];
                if (fullpath)
                {
                    result.Path = Redis.USER_IMGWEB_BASE + result.Path;
                }
            }
            //else
            //{
            //    result.Path = "default_userlogo.gif";
            //    if (fullpath)
            //    {
            //        result.Path = Redis.USER_IMGWEB_BASE + result.Path;
            //    }
            //}
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

            List<UserPictureInfo> list = Redis.GetAllItemsFromSortedSet<List<UserPictureInfo>>(key, FunctionTypeEnum.NewHouseUserInfo, 0);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (UserPictureInfo info in list)
                    {
                        if (!string.IsNullOrEmpty(info.Path))
                        {
                            info.Path = Redis.USER_IMGWEB_BASE + info.Path;
                        }
                        else
                        {
                            info.Path = string.Empty;
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 删除Redis新房图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        public static void RemoveRedisKey(string guid, string picturetype, int cityId)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);
            TXCommons.Redis.RemoveAllFromList(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
        }

        /// <summary>
        /// 添加Redis新房图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <param name="info"></param>
        public static void AddRedisKey(string guid, string picturetype, int cityId, List<TXCommons.PictureModular.PremisesPictureInfo> list)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);
            double score = 1;
            foreach (TXCommons.PictureModular.PremisesPictureInfo info in list)
            {
                Redis.AddItemToSortedSet<PremisesPictureInfo>(info, key, score, null, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
            }
        }
        /// <summary>
        /// 获取租房所有图片不过滤
        /// </summary>
        /// <param name="guid">租房guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns></returns>
        public static List<PremisesPictureInfo> GePremisesPictureListByThumbnail(string guid, bool fullpath, string picturetype, int cityId)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            List<PremisesPictureInfo> list = PremisesPictureFilter(Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId), 1);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (PremisesPictureInfo info in list)
                    {

                        if (!string.IsNullOrEmpty(info.Path))
                        {
                            info.Path = Redis.IMGWEB_BASE + info.Path;
                        }
                        else
                        {
                            info.Path = string.Empty;
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 返回所有图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="fullpath"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static List<PremisesPictureInfo> GetPremisesPictureList(string guid, bool fullpath, string picturetype, int cityId)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            List<PremisesPictureInfo> list = PremisesPictureFilter(Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId));
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (PremisesPictureInfo info in list)
                    {

                        if (!string.IsNullOrEmpty(info.Path))
                        {
                            info.Path = Redis.IMGWEB_BASE + info.Path;
                        }
                        else
                        {
                            info.Path = string.Empty;
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 返回所有图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="fullpath"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static PremisesPictureInfo GetPremisesPictureByID(string guid, bool fullpath, string picturetype, int cityId, int id)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            List<PremisesPictureInfo> list = PremisesPictureFilter(Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId));
            PremisesPictureInfo result = new PremisesPictureInfo();
            if (list != null && list.Count > 0)
            {
                list = list.Where(it => it.ID == id).ToList();
                if (list.Count > 0)
                {
                    result = list[0];
                    if (fullpath)
                    {
                        if (!string.IsNullOrEmpty(result.Path))
                        {
                            result.Path = Redis.IMGWEB_BASE + result.Path;
                        }
                        else
                        {
                            result.Path = string.Empty;
                        }

                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 返回指定图片类型的第一张图片
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="fullpath">是否全路径返回</param>
        /// <param name="picturetype">图片类型</param>
        /// <param name="cityId">城市ID</param>
        /// <returns></returns>
        public static PremisesPictureInfo GetPremisesPictureInfo(string guid, bool fullpath, string picturetype, int cityId)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            List<PremisesPictureInfo> list = PremisesPictureFilter(Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId));
            PremisesPictureInfo result = new PremisesPictureInfo();
            if (list != null && list.Count > 0)
            {
                result = list[0];
                if (fullpath)
                {
                    if (!string.IsNullOrEmpty(result.Path))
                    {
                        result.Path = Redis.IMGWEB_BASE + result.Path;
                    }
                    else
                    {
                        result.Path = string.Empty;
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 过滤图片列表，将新图片(没有缩略图的)去掉
        /// </summary>
        /// <param name="list">图片列表</param>
        /// <returns>过滤后的图片列表</returns>
        private static List<PremisesPictureInfo> PremisesPictureFilter(List<PremisesPictureInfo> list)
        {
            List<PremisesPictureInfo> result = new List<PremisesPictureInfo>();
            if (list != null && list.Count > 0)
            {
                var linqresult = from item in list
                                 where item.IsNew == 0
                                 orderby item.ID ascending
                                 select item;
                foreach (var item in linqresult)
                {
                    result.Add(item as PremisesPictureInfo);
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 过滤图片列表，将新图片(没有缩略图的)去掉
        /// </summary>
        /// <param name="list">图片列表</param>
        /// <returns>过滤后的图片列表</returns>
        public static List<PremisesPictureInfo> PremisesPictureFilter(List<PremisesPictureInfo> list, int isNew)
        {
            List<PremisesPictureInfo> result = new List<PremisesPictureInfo>();
            if (list != null && list.Count > 0)
            {
                var linqresult = from item in list
                                 where item.IsNew == isNew
                                 select item;
                foreach (var item in linqresult)
                {
                    result.Add(item as PremisesPictureInfo);
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取租房所有图片不过滤
        /// </summary>
        /// <param name="guid">租房guid</param>
        /// <param name="fullpath">是否获取全路径</param>
        /// <param name="picturetype">图片类型</param>
        /// <returns></returns>
        public static List<PremisesPictureInfo> GetPremisesPictureListNoFilter(string guid, bool fullpath, string picturetype, int cityId)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            List<PremisesPictureInfo> list = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
            if (list != null && list.Count > 0)
            {
                if (fullpath)
                {
                    foreach (PremisesPictureInfo info in list)
                    {

                        if (!string.IsNullOrEmpty(info.Path))
                        {
                            info.Path = Redis.IMGWEB_BASE + info.Path;
                        }
                        else
                        {
                            info.Path = string.Empty;
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 返回未保存图片
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="fullpath"></param>
        /// <param name="picturetype"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static List<PremisesPictureInfo> GetPremisesNoSavePiceureList(string guid, bool fullpath, string picturetype, int cityId)
        {
            string key = KeyManager.GetPremisesPictureKey(guid, picturetype);

            List<PremisesPictureInfo> list = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(key, FunctionTypeEnum.NewHouseBasicDataPicture, cityId);
            if (list != null && list.Count > 0)
            {
                list = list.Where(r => r.IsNew == 1).ToList();
                if (fullpath)
                {
                    foreach (PremisesPictureInfo info in list)
                    {

                        if (!string.IsNullOrEmpty(info.Path))
                        {
                            info.Path = Redis.IMGWEB_BASE + info.Path;
                        }
                        else
                        {
                            info.Path = string.Empty;
                        }
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 返回楼盘图片排序
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="fullpath"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static List<PremisesPictureInfo> GetPremisesPictureOrder(string guid, bool fullpath, int cityId)
        {


            List<PremisesPictureInfo> listROOMTYPE = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.ROOMTYPE.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> listPlan = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.Plan.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> listPlane = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.Plane.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> listTRAFFIC = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.TRAFFIC.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> listLocation = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.Location.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> listScene = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.Scene.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> listEffect = Redis.GetAllItemsFromSortedSet<List<PremisesPictureInfo>>(KeyManager.GetPremisesPictureKey(guid, PremisesPictureType.Effect.ToString()), FunctionTypeEnum.HouseImage, cityId);

            List<PremisesPictureInfo> list = new List<PremisesPictureInfo>();
            if (listROOMTYPE != null)
                list.AddRange(listROOMTYPE);
            if (listPlan != null)
                list.AddRange(listPlan);
            if (listPlane != null)
                list.AddRange(listPlane);
            if (listTRAFFIC != null)
                list.AddRange(listTRAFFIC);
            if (listLocation != null)
                list.AddRange(listLocation);
            if (listScene != null)
                list.AddRange(listScene);
            if (listEffect != null)
                list.AddRange(listEffect);

            if (list.Count >= 6)
                list = list.OrderByDescending(r => r.CreateTime).Take(6).ToList<PremisesPictureInfo>();
            else
                list = list.OrderByDescending(r => r.CreateTime).ToList<PremisesPictureInfo>();
            if (fullpath)
            {
                foreach (PremisesPictureInfo info in list)
                {

                    if (!string.IsNullOrEmpty(info.Path))
                    {
                        info.Path = Redis.IMGWEB_BASE + info.Path;
                    }
                    else
                    {
                        info.Path = string.Empty;
                    }
                }
            }

            return list;

        }

        public static List<PremisesPictureInfo> GetPremisesPictureByCity(int cityid)
        {
            return new List<PremisesPictureInfo>();
        }


    }
}
