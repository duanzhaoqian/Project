using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using ServiceStack;

namespace TXCommons.PictureModular
{
    public class Thumbnail
    {

        #region 租房图片缩略图尺寸
        private static string[] PremisesListThumbnail = new string[] { "692_450" };
        private static string[] ListThumbnail = new string[] { "695_457" };
        private static string[] EffectThumbnail = new string[] { "695_457" };
        private static string[] List = new string[] { "168_129", "71_53" };
        private static string[] LogoThumbnail = new string[] { "170_100" };
        private static string[] PlaneThumbnail = new string[] { "695_457", "710_383" };
        private static string[] Advert = new string[] { "695_457", "550_185" };
        #endregion

        public static void MakeLongRentHouseThumbnail(string guid, int cityId)
        {
            try
            {

                DealLongRentPicture(guid, PremisesPictureType.PremisesLIST, "692_450", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.ROOMTYPE, "695_457", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.Plan, "695_457", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.Plane, "695_457", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.TRAFFIC, "695_457", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.Location, "695_457", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.Scene, "695_457", null, cityId);
                DealLongRentPicture(guid, PremisesPictureType.Effect, "695_457", null, cityId);
            }
            catch (Exception ex)
            {
                //WriteTextToFile("最后异常" + ex.Message, "c:\\1.txt", true);
                //RecordLog.RecordException("宋德华", "MakeLongRentHouseThumbnail", ex);

                throw ex;

            }
        }


        /// <summary>
        /// 切图业务
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="type">类型</param>
        /// <param name="NoWaterMarkSize">不加水印的大小</param>
        /// <param name="LongRentThumbnail">切图大小数组</param>
        private static void DealLongRentPicture(string guid, PremisesPictureType type, string NoWaterMarkSize, string[] LongRentThumbnail, int cityId)
        {
            //WriteTextToFile("Thumbnail start" + guid + ";\n", "c:\\2.txt", true);
            List<PremisesPictureInfo> list = GetPicture.GePremisesPictureListByThumbnail(guid, false, type.ToString(), cityId);

            if (list != null && list.Count > 0)
            {
                foreach (PremisesPictureInfo info in list)
                {
                    string path = Redis.IMGPATH_BASE + info.Path;

                    if (!string.IsNullOrWhiteSpace(NoWaterMarkSize))
                        MakeThumbnail(path, path + "." + NoWaterMarkSize + ".jpg", int.Parse(NoWaterMarkSize.Split(new char[] { '_' })[0]), int.Parse(NoWaterMarkSize.Split(new char[] { '_' })[1]), "jiji", ImageFormat.Jpeg);

                    //处理isnew字段
                    if (info.IsNew == 1)
                    {
                        //WriteTextToFile("redis update" + guid + ";\n", "c:\\2.txt", true);
                        string key = KeyManager.GetPremisesPictureKey(guid, type.ToString());
                        double score = Redis.GetItemScoreInSortedSet<PremisesPictureInfo>(key, info, FunctionTypeEnum.HouseImage, cityId);
                        Redis.RemoveItemFromSortedSet<PremisesPictureInfo>(key, info, FunctionTypeEnum.HouseImage, cityId);
                        info.IsNew = 0;
                        Redis.AddItemToSortedSet<PremisesPictureInfo>(info, key, score, null, FunctionTypeEnum.HouseImage, cityId);
                        //WriteTextToFile("redis update end" + guid + ";\n", "c:\\2.txt", true);
                    }
                }
            }
            //WriteTextToFile("Thumbnail end" + guid + ";\n", "c:\\2.txt", true);
        }

        /// <summary>
        /// 获取图片缩略图格式
        /// </summary>
        /// <param name="picturetype">图片类型</param>
        /// <returns>格式数组</returns>
        public static string[] GetRentHousePictureThumbnail(string picturetype)
        {
            string[] result = null;
            if (picturetype == PremisesPictureType.PremisesLIST.ToString())
                result = PremisesListThumbnail;
            else if (picturetype == PremisesPictureType.List.ToString())
                result = List;
            else if (picturetype == PremisesPictureType.Advert.ToString())
                result = Advert;
            else if (picturetype == PremisesPictureType.Effect.ToString())
                result = EffectThumbnail;
            else if (picturetype == PremisesPictureType.Logo.ToString())
                result = LogoThumbnail;
            else if (picturetype == PremisesPictureType.Plane.ToString())
                result = PlaneThumbnail;
            else
                result = ListThumbnail;
            return result;
        }

        #region 工具
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式"HW"://指定高宽缩放（可能变形）"W"://指定宽，高按比例 "H"://指定高，宽按比例"Cut"://指定高宽裁减（不变形）</param>   
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode, System.Drawing.Imaging.ImageFormat iformat)
        {
            try
            {
                // System.Threading.Thread.Sleep(200);
                FileInfo f = new FileInfo(originalImagePath);
                if (System.IO.File.Exists(originalImagePath) && f.Length > 0)
                {
                    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath, true);

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
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
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
                                towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);
                            }

                            else
                            {
                                towidth = width;
                                toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
                            }

                            break;
                        case "white":

                            if (oh > ow)
                            {

                                toheight = height;

                                towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);

                                j = (width - towidth) / 2;

                            }
                            else if (oh == ow)
                            {
                                if (height > width)
                                {
                                    towidth = width;
                                    toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
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
                                    towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);
                                    j = (width - towidth) / 2;
                                }
                            }
                            else
                            {
                                towidth = width;
                                toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
                                k = (height - toheight) / 2;

                            }

                            break;
                        case "big":
                            //    672      679     
                            if ((double)ow / (double)oh > (double)width / (double)height)
                            {
                                if ((double)ow >= (double)width)
                                {
                                    towidth = width;
                                    toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
                                }
                                else
                                {
                                    toheight = oh;
                                    towidth = ow;
                                }
                            }
                            else
                            {
                                if ((double)oh >= (double)height)
                                {
                                    toheight = height;
                                    towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);
                                }
                                else
                                {
                                    toheight = oh;
                                    towidth = ow;

                                }

                            }

                            break;


                        case "leguan2":
                            if ((double)oh / (double)ow > (double)toheight / (double)towidth)
                            {
                                toheight = height;
                                towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);
                            }
                            else
                            {
                                towidth = width;
                                toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
                            }
                            break;
                        case "jiji":
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                            {

                                oh = originalImage.Height;
                                towidth = width;
                                ow = Convert.ToInt32((double)originalImage.Height * (double)towidth / (double)toheight);

                                y = (originalImage.Height - oh) / 4;
                                x = (originalImage.Width - ow) / 2;
                            }
                            else
                            {

                                ow = originalImage.Width;
                                toheight = height;
                                oh = Convert.ToInt32((double)originalImage.Width * (double)toheight / (double)towidth);

                                x = (originalImage.Width - ow) / 2;
                                y = (originalImage.Height - oh) / 4;
                            }
                            break;

                        case "logo":
                            if (ow < towidth && oh < toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Width;//Convert.ToInt32((double)originalImage.Height * (double)towidth / (double)toheight);

                                towidth = ow;
                                toheight = oh;

                                y = 0;
                                x = 0;

                            }
                            else
                            {
                                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                                {

                                    oh = originalImage.Height;
                                    towidth = width;
                                    ow = Convert.ToInt32((double)originalImage.Height * (double)towidth / (double)toheight);

                                    y = (originalImage.Height - oh) / 4;
                                    x = (originalImage.Width - ow) / 2;
                                }
                                else
                                {

                                    ow = originalImage.Width;
                                    toheight = height;
                                    oh = Convert.ToInt32((double)originalImage.Width * (double)toheight / (double)towidth);

                                    x = (originalImage.Width - ow) / 2;
                                    y = (originalImage.Height - oh) / 4;
                                }
                            }
                            break;
                        case "zoom":
                            if (ow < towidth && oh < toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Width;//Convert.ToInt32((double)originalImage.Height * (double)towidth / (double)toheight);

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

                                    towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);

                                    j = (width - towidth) / 2;


                                }
                                else if (oh == ow)
                                {
                                    if (height > width)
                                    {
                                        towidth = width;
                                        toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
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
                                        towidth = Convert.ToInt32((double)ow * (double)height / (double)oh);
                                        j = (width - towidth) / 2;
                                    }
                                }
                                else
                                {
                                    towidth = width;
                                    toheight = Convert.ToInt32((double)oh * (double)width / (double)ow);
                                    k = (height - toheight) / 2;

                                }
                            }
                            break;
                        default:
                            break;
                    }
                    System.Drawing.Image bitmap = null;
                    switch (mode)
                    {
                        case "white":
                            //新建一个bmp图片 200 135
                            bitmap = new System.Drawing.Bitmap(width, height);
                            break;
                        case "zoom":
                            //新建一个bmp图片 200 135
                            bitmap = new System.Drawing.Bitmap(width, height);
                            break;
                        default:
                            bitmap = new System.Drawing.Bitmap(towidth, toheight);
                            break;
                    }
                    //if (mode == "white")
                    //{
                    //    //新建一个bmp图片 200 135
                    //    bitmap = new System.Drawing.Bitmap(width, height);
                    //}
                    //else
                    //{
                    //    bitmap = new System.Drawing.Bitmap(towidth, toheight);
                    //}
                    //新建一个画板
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                    //Response.Write(g.DpiX);
                    //  byte[] metaComment = { (byte)'C', (byte)'J', (byte)'K', (byte)'J', (byte)'B' };
                    //  g.AddMetafileComment(metaComment);

                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    //清空画布并以透明背景色填充
                    g.Clear(System.Drawing.Color.White);
                    //在指定位置并且按指定大小绘制原图片的指定部分
                    //                                                        49.5 0       200        101
                    //if (mode == "white")
                    //{
                    //    g.DrawImage(originalImage, j, k, towidth, toheight);
                    //}
                    //else
                    //{
                    //    g.DrawImage(originalImage, new System.Drawing.Rectangle(j, k, towidth, toheight),
                    //        new System.Drawing.Rectangle(x, y, ow, oh),
                    //       System.Drawing.GraphicsUnit.Pixel);
                    //}
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
                            g.DrawImage(originalImage, new System.Drawing.Rectangle(j, k, towidth, toheight),
                            new System.Drawing.Rectangle(x, y, ow, oh),
                           System.Drawing.GraphicsUnit.Pixel);
                            break;
                    }
                    try
                    {
                        //以jpg格式保存缩略图
                        bitmap.Save(thumbnailPath, iformat);
                    }
                    catch (Exception ex)
                    {
                        // Moyu.ExceptionFunc.OutPutException(e.Message);
                        //this.listBox1.Items.Add(e.Message);
                        //RecordLog.RecordException("宋德华", "", ex);

                        throw ex;
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        g.Dispose();
                    }
                }
                //else
                //this.listBox1.Items.Add("读取文件失败");


            }
            catch (Exception ex)
            {
                //this.listBox1.Items.Add(ex.Message);   
                //RecordLog.RecordException("宋德华", "", ex);

                throw ex;
            }

        }


        /// <summary>
        /// 加图片水印
        /// </summary>
        /// <param name="img">要加水印的原图</param>
        /// <param name="filename">文件名</param>
        /// <param name="watermarkFilename">水印文件名</param>
        /// <param name="watermarkStatus">图片水印位置1=左上 2=中上 3=右上 4=左中  5=中中 6=右中 7=左下 8=右中 9=右下</param>
        /// <param name="quality">加水印后的质量0~100,数字越大质量越高</param>
        /// <param name="watermarkTransparency">水印图片的透明度1~10,数字越小越透明,10为不透明</param>
        public static void ImageWaterMarkPic(string originalImagePath, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
        {

            System.Drawing.Image img = System.Drawing.Image.FromFile(originalImagePath, true);
            Graphics g = Graphics.FromImage(img);
            //设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Image watermark = new Bitmap(watermarkFilename);

            ImageAttributes imageAttributes = new ImageAttributes();
            try
            {
                if (watermark.Height >= img.Height || watermark.Width >= img.Width)
                {
                    File.Copy(originalImagePath, filename, true);
                }
                else
                {

                    ColorMap colorMap = new ColorMap();

                    colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                    colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                    ColorMap[] remapTable = { colorMap };

                    imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                    float transparency = 0.5F;
                    if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
                        transparency = (watermarkTransparency / 10.0F);


                    float[][] colorMatrixElements = {
                                             new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                             new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                             new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                             new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
                                             new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                         };

                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                    imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    int xpos = 0;
                    int ypos = 0;

                    switch (watermarkStatus)
                    {
                        case 1:
                            xpos = (int)(img.Width * (float).01);
                            ypos = (int)(img.Height * (float).01);
                            break;
                        case 2:
                            xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                            ypos = (int)(img.Height * (float).01);
                            break;
                        case 3:
                            xpos = (int)((img.Width * (float).99) - (watermark.Width));
                            ypos = (int)(img.Height * (float).01);
                            break;
                        case 4:
                            xpos = (int)(img.Width * (float).01);
                            ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                            break;
                        case 5:
                            xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                            ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                            break;
                        case 6:
                            xpos = (int)((img.Width * (float).99) - (watermark.Width));
                            ypos = (int)((img.Height * (float).50) - (watermark.Height / 2));
                            break;
                        case 7:
                            xpos = (int)(img.Width * (float).01);
                            ypos = (int)((img.Height * (float).99) - watermark.Height);
                            break;
                        case 8:
                            xpos = (int)((img.Width * (float).50) - (watermark.Width / 2));
                            ypos = (int)((img.Height * (float).99) - watermark.Height);
                            break;
                        case 9:
                            xpos = (int)((img.Width * (float).99) - (watermark.Width));
                            ypos = (int)((img.Height * (float).99) - watermark.Height);
                            break;
                    }

                    g.DrawImage(watermark, new Rectangle(xpos, ypos, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);

                    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;
                    foreach (ImageCodecInfo codec in codecs)
                    {
                        if (codec.MimeType.IndexOf("jpeg") > -1)
                            ici = codec;
                    }
                    EncoderParameters encoderParams = new EncoderParameters();
                    long[] qualityParam = new long[1];
                    if (quality < 0 || quality > 100)
                        quality = 80;

                    qualityParam[0] = quality;

                    EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam);
                    encoderParams.Param[0] = encoderParam;

                    if (ici != null)
                        img.Save(filename, ici, encoderParams);
                    else
                        img.Save(filename);
                }

            }
            catch (Exception ex)
            {
                //RecordLog.RecordException("宋德华", "", ex);

                throw ex;
            }
            finally
            {
                g.Dispose();
                img.Dispose();
                watermark.Dispose();
                imageAttributes.Dispose();

            }

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
                    Bitmap thumimg = null;
                    if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                    {
                        thumimg = new Bitmap(originalImg);
                    }
                    else
                    {
                        thumimg = MakeThumbnail(originalImg, imageWidth, imageHeight);
                    }

                    Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                    Graphics graphics = Graphics.FromImage(partImg);
                    Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                    Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                    Graphics G = Graphics.FromImage(partImg);
                    G.Clear(Color.White);
                    G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    G.SmoothingMode = SmoothingMode.HighQuality;

                    graphics.DrawImage(thumimg, destRect, origRect, GraphicsUnit.Pixel);
                    G.Dispose();
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
                //RecordLog.RecordException("宋德华", "SaveCutPic1方法 pPath:" + pPath, ex);
                throw ex;

            }
            return "";
        }

        public static Bitmap MakeThumbnail(Image fromImg, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
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

        #region 将文本写成任意扩展名的文件
        public static void WriteTextToFile(string Text, string FileFullPathAndName)
        {
            WriteTextToFile(Text, FileFullPathAndName, false, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend)
        {
            WriteTextToFile(Text, FileFullPathAndName, IsAppend, "gb2312");
        }
        public static void WriteTextToFile(string Text, string FileFullPathAndName, bool IsAppend, string encoding)
        {
            if (Text == "" || Text == null)
            {
                return;
            }
            string DirName = GetDirByFileName(FileFullPathAndName);
            if (!Directory.Exists(DirName))
            {
                Directory.CreateDirectory(DirName);
            }
            if (!File.Exists(FileFullPathAndName))
            {
                using (File.Create(FileFullPathAndName)) { };
            }
            Encoding gbEncode = Encoding.GetEncoding(encoding);
            using (StreamWriter swFromFile = new StreamWriter(FileFullPathAndName, IsAppend, gbEncode))
            {
                swFromFile.Write(Text);
                swFromFile.Close();
            }
        }
        #endregion

        #region 根据文件名,取得目录名
        public static string GetDirByFileName(string FileName)
        {
            int position = FileName.LastIndexOf(@"\");
            if (position > 0)
            {
                return FileName.Substring(0, position);
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
