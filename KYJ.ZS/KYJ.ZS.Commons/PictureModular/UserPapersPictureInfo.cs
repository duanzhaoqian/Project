namespace KYJ.ZS.Commons.PictureModular
{
    /// <summary>
    /// 用户资料实体图片
    /// </summary>
    public class UserPapersPictureInfo
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

        public string PictureType
        {
            get;
            set;
        }

        public string Desc
        {
            get;
            set;
        }

        /// <summary>
        /// 是否为新上传，是为1，否为0
        /// </summary>
        public int IsNew
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 用户资料类型
    /// </summary>
    public enum UserPapersPictureType
    {
        /// <summary>
        /// 资料类型预留
        /// </summary>
        XXX资料1 = 1

    }
}
