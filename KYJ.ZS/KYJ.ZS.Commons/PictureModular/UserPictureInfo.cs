
namespace KYJ.ZS.Commons.PictureModular
{
    public class UserPictureInfo
    {
        public int Id
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string PictureType
        {
            get;
            set;
        }
    }


    /// <summary>
    /// 用户图片类型
    /// </summary>
    public enum UserPictureType
    {
        /// <summary>
        /// 用户Logo
        /// </summary>
        LOGO = 0,
        /// <summary>
        /// 用户临时Logo，用户后台上传时使用
        /// </summary>
        LOGOTEMP = 1,
        /// <summary>
        /// 身份证
        /// </summary>
        IDCARD = 2,
        /// <summary>
        /// 名片
        /// </summary>
        PASSPORT = 3,
        /// <summary>
        /// 商户LOGO
        /// </summary>
        MERCHANTLOGO=4

    }

}
