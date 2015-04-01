using System.Collections.Generic;
using System.IO;

namespace TXCommons.PictureModular
{
    public class UploadThumbnail
    {
        /// <summary>
        /// 楼盘沙盘缩略图
        /// </summary>
        private static readonly string[] PremisesListThumbnail =
        {
            "180_130", "692_450"
        };

        /// <summary>
        /// logo图
        /// </summary>
        private static readonly string[] LogoThumbnail =
        {
            "170_100", "140_140"
        };

        /// <summary>
        /// 广告图
        /// </summary>
        private static readonly string[] AdvertThumbnail =
        {
            "550_185", "1190_200"
        };

        /// <summary>
        /// 楼栋平面图
        /// </summary>
        private static readonly string[] PlaneThumbnail =
        {
            "710_383"
        };

        /// <summary>
        /// 效果图
        /// </summary>
        private static readonly string[] EffectThumbnail =
        {
            "186_125","175_125"
        };

        /// <summary>
        /// 公共尺寸
        /// </summary>
        private static readonly string[] CommonThumbnail =
        {
            "211_150","71_53","168_129","500_350","180_130","85_65", "695_457"
        };




        public static void ThumbnailPremisesPicture(string path, string picturetype)
        {
            //通过消息生成缩略图
            if (string.IsNullOrEmpty(picturetype))
            {
                return;
            }
            picturetype = picturetype.ToUpper();
            if (picturetype == PremisesPictureType.PremisesLIST.ToString().ToUpper())
            {
                ThumbnailPic(PremisesListThumbnail, path);
            }
            else if (picturetype == PremisesPictureType.Logo.ToString().ToUpper())
            {
                ThumbnailPic(LogoThumbnail, path);
            }
            else if (picturetype == PremisesPictureType.Advert.ToString().ToUpper())
            {
                ThumbnailPic(AdvertThumbnail, path);
            }
            else if (picturetype == PremisesPictureType.Plane.ToString().ToUpper())
            {
                ThumbnailPic(PlaneThumbnail, path);
            }
                else if (picturetype == PremisesPictureType.Effect.ToString().ToUpper())
            {
                ThumbnailPic(EffectThumbnail, path);
            }
            //切公共大小
            if (picturetype != PremisesPictureType.Logo.ToString().ToUpper())
            {
                ThumbnailPic(CommonThumbnail, path);
            }
        }

        private static void ThumbnailPic(IEnumerable<string> size,string path)
        {
            foreach (var item in size)
            {
                if (item == "168_129" || item == "71_53")
                    Thumbnail.MakeThumbnail(path, path + "." + item + ".jpg", int.Parse(item.Split(new[] { '_' })[0]), int.Parse(item.Split(new[] { '_' })[1]), "zoom", System.Drawing.Imaging.ImageFormat.Jpeg);
                else
                    Thumbnail.MakeThumbnail(path, path + "." + item + ".jpg", int.Parse(item.Split(new[] { '_' })[0]), int.Parse(item.Split(new [] { '_' })[1]), "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public static void ThumbnailPremisesCommonPhoto(string path, string picturetype)
        {
            foreach (var item in CommonThumbnail)
            {
                var file = path + "." + item + ".jpg";
                if (!File.Exists(file))
                {
                    Thumbnail.MakeThumbnail(path, file, int.Parse(item.Split(new[] { '_' })[0]), int.Parse(item.Split(new[] { '_' })[1]), "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }

        /// <summary>
        /// 为楼盘相册切出211_150大小图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="picturetype"></param>
        public static void ThumbnailPremisesPhoto211_150(string path, string picturetype)
        {
            var file = path + ".211_150.jpg";
            if (!File.Exists(file))
            {
                Thumbnail.MakeThumbnail(path, file, 211, 150, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// 为楼盘相册切出211_150大小图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="picturetype"></param>
        public static void ThumbnailAdvertPic1190_200(string path, string picturetype)
        {
            var file = path + ".1190_200.jpg";
            if (!File.Exists(file))
            {
                Thumbnail.MakeThumbnail(path, file, 1109, 200, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }


        /// <summary>
        /// 为楼盘相册切出140_140大小图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="picturetype"></param>
        public static void ThumbnailLogoPhoto140_140(string path, string picturetype)
        {
            var file = path + ".140_140.jpg";
            if (!File.Exists(file))
            {
                Thumbnail.MakeThumbnail(path, file, 140, 140, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }


        /// <summary>
        /// 为楼盘相册切出211_150大小图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="picturetype"></param>
        public static void ThumbnailPremisesPhoto175_125(string path, string picturetype)
        {
            var file = path + ".175_125.jpg";
            if (!File.Exists(file))
            {
                Thumbnail.MakeThumbnail(path, file, 175, 125, "leguan2", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

    }
}
