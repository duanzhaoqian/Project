using TXCommons.user.cjkjb.webservice;



namespace TXCommons
{
    public class NHWebFavorite
    {
        private OperaUserService ouService = new OperaUserService();

        /// <summary>
        /// 添加用户收藏
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Platform"></param>
        /// <param name="Type"></param>
        /// <param name="BusinessId"></param>
        /// <param name="BusinessName"></param>
        /// <param name="PicUrl"></param>
        /// <param name="Expand"></param>
        /// <returns></returns>
        public bool AddUserFavourite(int UserId, string Type, string BusinessId, string BusinessName, string PicUrl, string Expand)
        {
            //NH  新房
            // return ouService.AddUserFavorite(UrlSet.binName, UrlSet.className, UserId, UrlSet.Platform, Type, BusinessId, BusinessName, PicUrl, Expand);
            return ouService.AddUserFavorite("binpath", "classpath", UserId, "NH", Type, BusinessId, BusinessName, PicUrl, Expand);

        }

        /// <summary>
        /// 判断该用户是否收藏过
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="BusinessId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int IsCountFavorite(int UserId, int BusinessId, string Type)
        {

            UserFavoritePage list = ouService.SearchUserFavoriteList("binpath", "classpath", 1, 1, UserId, "NH", Type, BusinessId, "");
            return list.Count;
        }

        /// <summary>
        /// 获取用户收藏
        /// author:汪敏
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Type"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="BusinessId"></param>
        /// <param name="BusinessName"></param>
        /// <returns></returns>
        public UserFavoritePage GetMyPremisesFavorite(int userid, string Type, int pageindex, int pagesize, int BusinessId, string BusinessName)
        {

            return ouService.SearchUserFavoriteList("binpath", "classpath", pageindex, pagesize, userid, "NH", Type, BusinessId, BusinessName);
        }
        /// <summary>
        /// 删除用户收藏
        /// author:汪敏
        /// </summary>
        /// <param name="id">收藏Id</param>
        /// <returns></returns>
        public bool DeletePremisesFavorite(int id)
        {
            return ouService.DeleteUserFavorite("binpath", "classpath", id);
        }
    }
}
