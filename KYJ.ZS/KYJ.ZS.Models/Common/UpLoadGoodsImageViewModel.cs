using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.Common
{
    public class UpLoadGoodsImageViewModel
    {
        /// <summary>
        /// guid
        /// </summary>
        public string _Guid { get; set; }

        private bool _FullPath = true;
        /// <summary>
        /// 是否获取全路径
        /// </summary>
        public bool FullPath
        {
            get { return _FullPath; }
            set { _FullPath = value; }
        }
        /// <summary>
        /// 图片类型
        /// </summary>
        public string PictureType { get; set; }

        private int _Minnum = 0;
        /// <summary>
        /// 最小上传数
        /// </summary>
        public int Minnum
        {
            get { return _Minnum; }
            set { _Minnum = value; }
        }

        private int _Maxnum = 5;
        /// <summary>
        /// 最大上传数
        /// </summary>
        public int Maxnum
        {
            get { return _Maxnum; }
            set { _Maxnum = value; }
        }

        /// <summary>
        /// 商品图片编号
        /// </summary>
        public string PictureNo { get; set; }

        private string _UrlPath = "";
        /// <summary>
        /// 上传路径
        /// </summary>
        public string UrlPath
        {
            get { return _UrlPath; }
            set { _UrlPath = value; }
        }
    }
}
