using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace KYJ.ZS.Commons.PictureModular
{
    public class Thumbnail
    {
        /// <summary>
        /// 用户资料图片列表
        /// </summary>
        private static readonly string[] UserPapersType1 = { "180_123" };

        /// <summary>
        /// 商品展示列表
        /// </summary>
        private static readonly string[] GoodsShow = { "360_360","130_130","80_80","60_60","26_26","370_370","50_50","110_110" };

        /// <summary>
        /// 商品详细信息列表
        /// </summary>
        private static readonly string[] GoodsInfo = { "50_50" };

        /// <summary>
        /// 闲置商品列表
        /// </summary>
        private static readonly string[] FreeGoodsInfo ={ "360_360", "130_130", "80_80", "60_60", "26_26", "370_370", "50_50","110_110" };

        /// <summary>
        /// 品牌图片尺寸
        /// </summary>
        private static readonly string[] BrandInfo = {"160_160", "98_34"};

        /// <summary>
        /// 广告图片尺寸
        /// </summary>
        private static readonly string[] AdvertisementInfo = { "698_278", "248_92", "218_234", "220_110", "220_312","246_126", "246_136" };

        #region 商品图片切割

        public static void ThumbnailGoodsPicture(string path, string picturetype)
        {
            if (GoodsType.SHOW.ToString().Equals(picturetype))
            {
                foreach (var item in GoodsShow)
                {
                    MakeThumbnail(path, path + "." + item + ".jpg", int.Parse(item.Split(new[] {'_'})[0]),
                        int.Parse(item.Split(new[] {'_'})[1]), "leguan2", ImageFormat.Jpeg);
                }
            }

            if (GoodsType.INFO.ToString().Equals(picturetype))
            {
                foreach (var item in GoodsInfo)
                {
                    MakeThumbnail(path, path + "." + item + ".jpg", int.Parse(item.Split(new[] {'_'})[0]),
                        int.Parse(item.Split(new[] {'_'})[1]), "leguan2", ImageFormat.Jpeg);
                }
            }

            if (GoodsType.FREE.ToString().Equals(picturetype))
            {
                foreach (var item in FreeGoodsInfo)
                {
                    MakeThumbnail(path, path + "." + item + ".jpg", int.Parse(item.Split(new[] { '_' })[0]),
                        int.Parse(item.Split(new[] { '_' })[1]), "leguan2", ImageFormat.Jpeg);
                }
            }

        }
        #endregion


        #region 将文本写成任意扩展名的文件
        public static void WriteTextToFile(string text, string fileFullPathAndName)
        {
            WriteTextToFile(text, fileFullPathAndName, false, "gb2312");
        }
        public static void WriteTextToFile(string text, string fileFullPathAndName, bool isAppend)
        {
            WriteTextToFile(text, fileFullPathAndName, isAppend, "gb2312");
        }

        public static void WriteTextToFile(string text, string fileFullPathAndName, bool isAppend, string encoding)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            string dirName = GetDirByFileName(fileFullPathAndName);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            if (!File.Exists(fileFullPathAndName))
            {
                using (File.Create(fileFullPathAndName)) { }
            }
            Encoding gbEncode = Encoding.GetEncoding(encoding);
            using (var swFromFile = new StreamWriter(fileFullPathAndName, isAppend, gbEncode))
            {
                swFromFile.Write(text);
                swFromFile.Close();
            }
        }
        #endregion


        #region 根据文件名,取得目录名
        public static string GetDirByFileName(string fileName)
        {
            int position = fileName.LastIndexOf(@"\", StringComparison.Ordinal);
            if (position > 0)
            {
                return fileName.Substring(0, position);
            }
            return "";
        }

        #endregion



        #region 工具

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式"HW"://指定高宽缩放（可能变形）"W"://指定宽，高按比例 "H"://指定高，宽按比例"Cut"://指定高宽裁减（不变形）</param>
        /// <param name="iformat"></param>   
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, ImageFormat iformat)
        {
            try
            {
                // System.Threading.Thread.Sleep(200);
                var f = new FileInfo(originalImagePath);
                if (File.Exists(originalImagePath) && f.Length > 0)
                {
                    var originalImage = Image.FromFile(originalImagePath, true);

                    int towidth = width;
                    int toheight = height;

                    int x = 0;
                    int y = 0;
                    int ow = originalImage.Width;
                    int oh = originalImage.Height;
                    int j = 0;
                    int k = 0;

                    switch (mode)
                    {
                        case "HW"://指定高宽缩放（可能变形）           
                            break;
                        case "W"://指定宽，高按比例             
                            toheight = originalImage.Height * width / originalImage.Width;
                            break;
                        case "H"://指定高，宽按比例
                            towidth = originalImage.Width * height / originalImage.Height;
                            break;
                        case "Cut"://指定高宽裁减（不变形）           
                            if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Height * towidth / toheight;
                                y = 0;
                                x = (originalImage.Width - ow) / 2;
                            }
                            else
                            {
                                ow = originalImage.Width;
                                oh = originalImage.Width * height / towidth;
                                x = 0;
                                y = (originalImage.Height - oh) / 4;
                            }
                            break;
                        case "leguan":
                            if (oh > ow)
                            {
                                toheight = height;
                                towidth = Convert.ToInt32(ow * (double)height / oh);
                            }

                            else
                            {
                                towidth = width;
                                toheight = Convert.ToInt32(oh * (double)width / ow);
                            }

                            break;
                        case "white":

                            if (oh > ow)
                            {

                                toheight = height;

                                towidth = Convert.ToInt32(ow * (double)height / oh);

                                j = (width - towidth) / 2;

                            }
                            else if (oh == ow)
                            {
                                if (height > width)
                                {
                                    towidth = width;
                                    toheight = Convert.ToInt32(oh * (double)width / ow);
                                    k = (height - toheight) / 2;

                                }
                                else if (height == width)
                                {
                                    towidth = width;
                                    toheight = height;
                                    j = 0; k = 0;
                                }
                                else
                                {
                                    toheight = height;
                                    towidth = Convert.ToInt32(ow * (double)height / oh);
                                    j = (width - towidth) / 2;
                                }
                            }
                            else
                            {
                                towidth = width;
                                toheight = Convert.ToInt32(oh * (double)width / ow);
                                k = (height - toheight) / 2;

                            }

                            break;
                        case "big":  
                            if (ow / (double)oh > width / (double)height)
                            {
                                if (ow >= (double)width)
                                {
                                    towidth = width;
                                    toheight = Convert.ToInt32(oh * (double)width / ow);
                                }
                                else
                                {
                                    toheight = oh;
                                    towidth = ow;
                                }
                            }
                            else
                            {
                                if (oh >= (double)height)
                                {
                                    toheight = height;
                                    towidth = Convert.ToInt32(ow * (double)height / oh);
                                }
                                else
                                {
                                    toheight = oh;
                                    towidth = ow;

                                }

                            }

                            break;


                        case "leguan2":
                            if (oh / (double)ow > (double)toheight / towidth)
                            {
                                toheight = height;
                                towidth = Convert.ToInt32(ow * (double)height / oh);
                            }
                            else
                            {
                                towidth = width;
                                toheight = Convert.ToInt32(oh * (double)width / ow);
                            }
                            break;
                        case "jiji":
                            if ((double)originalImage.Width / originalImage.Height > towidth / (double)toheight)
                            {

                                oh = originalImage.Height;
                                towidth = width;
                                ow = Convert.ToInt32((double)originalImage.Height * towidth / toheight);

                                y = (originalImage.Height - oh) / 4;
                                x = (originalImage.Width - ow) / 2;
                            }
                            else
                            {

                                ow = originalImage.Width;
                                toheight = height;
                                oh = Convert.ToInt32(originalImage.Width * (double)toheight / towidth);

                                x = (originalImage.Width - ow) / 2;
                                y = (originalImage.Height - oh) / 4;
                            }
                            break;
                        case "logo":
                            if (ow < towidth && oh < toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Width;

                                towidth = ow;
                                toheight = oh;

                                y = 0;
                                x = 0;

                            }
                            else
                            {
                                if (originalImage.Width / (double)originalImage.Height > towidth / (double)toheight)
                                {

                                    oh = originalImage.Height;
                                    towidth = width;
                                    ow = Convert.ToInt32(originalImage.Height * (double)towidth / toheight);

                                    y = (originalImage.Height - oh) / 4;
                                    x = (originalImage.Width - ow) / 2;
                                }
                                else
                                {

                                    ow = originalImage.Width;
                                    toheight = height;
                                    oh = Convert.ToInt32(originalImage.Width * (double)toheight / towidth);

                                    x = (originalImage.Width - ow) / 2;
                                    y = (originalImage.Height - oh) / 4;
                                }
                            }
                            break;
                        case "zoom":
                            if (ow < towidth && oh < toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Width;

                                towidth = ow;

                                toheight = oh;

                                y = 0;
                                x = 0;

                                j = (width - towidth) / 2;

                                k = (height - toheight) / 2;

                            }
                            else
                            {
                                if (oh > ow)
                                {

                                    toheight = height;

                                    towidth = Convert.ToInt32((double)ow * height / oh);

                                    j = (width - towidth) / 2;


                                }
                                else if (oh == ow)
                                {
                                    if (height > width)
                                    {
                                        towidth = width;
                                        toheight = Convert.ToInt32(oh * (double)width / ow);
                                        k = (height - toheight) / 2;

                                    }
                                    else if (height == width)
                                    {
                                        towidth = width;
                                        toheight = height;
                                        j = 0; k = 0;
                                    }
                                    else
                                    {
                                        toheight = height;
                                        towidth = Convert.ToInt32(ow * (double)height / oh);
                                        j = (width - towidth) / 2;
                                    }
                                }
                                else
                                {
                                    towidth = width;
                                    toheight = Convert.ToInt32(oh * (double)width / ow);
                                    k = (height - toheight) / 2;

                                }
                            }
                            break;
                    }
                    Image bitmap;
                    switch (mode)
                    {
                        case "white":
                            //新建一个bmp图片 200 135
                            bitmap = new Bitmap(width, height);
                            break;
                        case "zoom":
                            //新建一个bmp图片 200 135
                            bitmap = new Bitmap(width, height);
                            break;
                        default:
                            bitmap = new Bitmap(towidth, toheight);
                            break;
                    }
                    //新建一个画板
                    Graphics g = Graphics.FromImage(bitmap);

                    g.CompositingQuality = CompositingQuality.HighQuality;
                    //设置高质量插值法
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    //清空画布并以透明背景色填充
                    g.Clear(Color.White);
                    switch (mode)
                    {
                        case "white":
                            //新建一个bmp图片 200 135
                            g.DrawImage(originalImage, j, k, towidth, toheight);
                            break;
                        case "zoom":
                            //新建一个bmp图片 200 135
                            g.DrawImage(originalImage, j, k, towidth, toheight);
                            break;
                        default:
                            g.DrawImage(originalImage, new Rectangle(j, k, towidth, toheight),
                            new Rectangle(x, y, ow, oh),
                            GraphicsUnit.Pixel);
                            break;
                    }
                    try
                    {
                        //以jpg格式保存缩略图
                        bitmap.Save(thumbnailPath, iformat);
                    }
                    catch (Exception ex)
                    {
                        PicLog.Log("Error：", "Drawing", "Save Pic:" + ex.Message + " ：" + DateTime.Now);
                        throw;
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        g.Dispose();
                    }
                }
                else
                    PicLog.Log("Info", "Drawing", "读取文件失败");

            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Drawing", "Thumbail:" + ex.Message + " ：" + DateTime.Now);
                throw;
            }

        }



        /// <summary>
        /// 获取图片缩略图格式
        /// </summary>
        /// <param name="picturetype">图片类型</param>
        /// <returns>格式数组</returns>
        public static string[] GetUserPapersPictureThumbnail(string picturetype)
        {
            string[] result = null;
            if (picturetype == UserPapersPictureType.XXX资料1.ToString())
            {
                result = UserPapersType1;
            }
            return result;
        }

        /// <summary>
        /// 获取商品图片缩略图格式
        /// </summary>
        /// <param name="picturetype">图片类型</param>
        /// <returns>格式数组</returns>
        public static string[] GetGoodsPictureThumbnail(string picturetype)
        {
            string[] result = null;
            if (picturetype == GoodsType.SHOW.ToString())
            {
                result = GoodsShow;
            }
            else if (picturetype == GoodsType.INFO.ToString())
            {
                result = GoodsInfo;
            }
            else if (picturetype == GoodsType.FREE.ToString())
            {
                result = FreeGoodsInfo;
            }
            return result;
        }

        /// <summary>
        /// 获取品牌图片所录取图格式
        /// </summary>
        /// <returns></returns>
        public static string[] GetBrandPictureThumbnail()
        {
            return BrandInfo;
        }





        #endregion



        #region 用户图片切割
        public static string SaveCutPic(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            try
            {
                using (Image originalImg = Image.FromFile(pPath))
                {
                    string filePath = pSavedPath;
                    Bitmap thumimg;
                    if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                    {
                        thumimg = new Bitmap(originalImg);
                    }
                    else
                    {
                        thumimg = MakeThumbnail(originalImg, imageWidth, imageHeight);
                    }

                    var partImg = new Bitmap(pPartWidth, pPartHeight);
                    Graphics graphics = Graphics.FromImage(partImg);
                    var destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                    var origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                    Graphics g = Graphics.FromImage(partImg);
                    g.Clear(Color.White);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    graphics.DrawImage(thumimg, destRect, origRect, GraphicsUnit.Pixel);
                    g.Dispose();
                    originalImg.Dispose();
                    if (File.Exists(filePath))
                    {
                        File.SetAttributes(filePath, FileAttributes.Normal);
                        File.Delete(filePath);
                    }
                    partImg.Save(filePath, ImageFormat.Jpeg);

                    partImg.Dispose();
                    thumimg.Dispose();
                }
            }
            catch (Exception ex)
            {
                PicLog.Log("Error：", "Error", "SaveCutPic1" + ex.Message + " ：" + DateTime.Now);
                return "error";
            }
            return "";
        }


        public static Bitmap MakeThumbnail(Image fromImg, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            int ow = fromImg.Width;
            int oh = fromImg.Height;
            //新建一个画板
            Graphics g = Graphics.FromImage(bmp);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            g.DrawImage(fromImg, new Rectangle(0, 0, width, height),
                new Rectangle(0, 0, ow, oh),
                GraphicsUnit.Pixel);
            return bmp;

        }

        #endregion



    }
}
