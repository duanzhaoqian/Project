using System;

namespace TXCommons.PictureModular
{
    public class PremisesPictureInfo
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

        private string prcturetype;
        public string PictureType
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(prcturetype)) return prcturetype.ToUpper();
                return "";
            }
            set { prcturetype = value; }
        }

        public string PictureTypeName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PictureType)) return "";
                switch (PictureType.ToUpper())
                {
                    case "PREMISESLIST":
                        return "楼盘沙盘图";

                    case "ROOMTYPE":
                        return "户型图";

                    case "PLAN":
                        return "楼栋规划图";

                    case "PLANE":
                        return "楼栋平面图";

                    case "TRAFFIC":
                        return "交通图";

                    case "LOCATION":
                        return "位置图";

                    case "SCENE":
                        return "实景图";

                    case "EFFECT":
                        return "效果图";

                    case "LIST":
                        return "列表图";

                    case "LOGO":
                        return "Logo";
                    case "ADVERT":
                        return "广告图";
                    case "ADVERTLIST":
                        return "当前广告图";
                }
                return "";
            }
        }

        public string Title
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
        /// 是否广告图
        /// 0:否 1:是
        /// </summary>
        public int IsForce
        {
            get;
            set;
        }
        /// <summary>
        /// 是否新图
        /// </summary>
        public int IsNew
        {
            get;
            set;
        }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 点击量
        /// </summary>
        public string Hits
        {
            get;
            set;
        }

    }
    public enum PremisesPictureType
    {


        /// <summary>
        /// 楼盘沙盘图
        /// </summary>
        PremisesLIST = 0,
        /// <summary>
        /// 户型图
        /// </summary>
        ROOMTYPE = 1,
        /// <summary>
        /// 楼栋规划图
        /// </summary>
        Plan = 2,
        /// <summary>
        /// 楼栋平面图
        /// </summary>
        Plane = 3,
        /// <summary>
        /// 交通图
        /// </summary>
        TRAFFIC = 4,
        /// <summary>
        /// 位置图
        /// </summary>
        Location = 5,
        /// <summary>
        /// 实景图
        /// </summary>
        Scene = 6,
        /// <summary>
        /// 效果图
        /// </summary>
        Effect = 7,
        /// <summary>
        /// 列表图
        /// </summary>
        List = 8,
        /// <summary>
        /// logo
        /// </summary>
        Logo = 9,
        /// <summary>
        /// 广告图
        /// </summary>
        Advert = 10,

        /// <summary>
        /// 当前广告图
        /// </summary>
        AdvertList = 11
    }
}
