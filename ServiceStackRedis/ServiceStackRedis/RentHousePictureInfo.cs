using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStackRedis
{

    /// <summary>
    /// 租房图片实体类
    /// </summary>
    public class RentHousePictureInfo
    {
        public int ID
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

        /// <summary>
        /// 图片小类别，适用于室内图和家具图
        /// </summary>
        public string PictureShowType { get; set; }

        /// <summary>
        /// 当是列表图时，生成此列表图的老图的Id
        /// </summary>
        public int LongListOldPicId { get; set; }


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
        /// <summary>
        /// 小区图片ID
        /// </summary>
        public int Vid
        {
            get;
            set;
        }
        /// <summary>
        /// 是否是小区图片 1 是 0 否
        /// </summary>
        public int IsVillage
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 用户图片实体类
    /// </summary>
    public class UserPictureInfo
    {
        public int ID
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
}
