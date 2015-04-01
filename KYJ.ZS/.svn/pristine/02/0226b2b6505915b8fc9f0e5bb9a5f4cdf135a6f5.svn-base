using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

//using Lucene.Net.Store;
using KYJ.ZS.Commons.PictureModular;

namespace KYJ.ZS.Commons.Common
{
    /// <summary>
    /// UpdataImageRAR 的摘要说明
    /// </summary>
    public class UpdataImageRAR
    {
        public UpdataImageRAR()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //

        }

        /// 上传图片 集成cmyk→rgb
        /// </summary>
        /// <param name="postedFile">上载文件的访问</param>
        /// <param name="guid"></param>
        /// <param name="pictureType"></param>
        /// <returns>上传后的文件名</returns>
        public static string SavePostedImage(HttpPostedFile postedFile,string guid, string pictureType)
        {
            return SavePostedImage(postedFile, 0, 0, 0.5, guid, pictureType);
        }
        ///// <summary>
        ///// 上传图片 集成cmyk→rgb
        ///// </summary>
        ///// <param name="postedFile">上载文件的访问</param>
        ///// <param name="filePaht">保存目录</param>
        ///// <param name="imgFormat">图片格式要求</param>
        ///// <returns>上传后的文件名</returns>
        //public string SavePostedImage1(HttpPostedFile postedFile, string filePaht, ImageFormat imgFormat, string guid, string pictureType)
        //{
        //    return SavePostedImage(postedFile, filePaht, 0, 0, imgFormat, 0.8,guid,pictureType);
        //}
        ///// <summary>
        ///// 上传图片 集成cmyk→rgb
        ///// </summary>
        ///// <param name="postedFile">上载文件的访问</param>
        ///// <param name="filePaht">保存目录</param>
        ///// <param name="fileName">新名字</param>
        ///// <param name="imgFormat">图片格式要求</param>
        ///// <returns>上传后的文件名</returns>
        //public string SavePostedImage2(HttpPostedFile postedFile, string filePaht, ImageFormat imgFormat, string guid, string pictureType)
        //{
        //    return SavePostedImage(postedFile, filePaht, 0, 0, imgFormat, 0.5, guid, pictureType);
        //}

        /// <summary>
        /// 上传图片 集成cmyk→rgb
        /// </summary>
        /// <param name="postedFile">上载文件的访问</param>
        /// <param name="filePaht">保存目录</param>
        /// <param name="fileName">新名字</param>
        /// <param name="maxHeight">限高</param>
        /// <param name="maxWidth">限宽</param>
        /// <param name="imgFormat">图片格式要求</param>
        /// <param name="rate">压缩比(double:0--1)</param>
        /// <param name="guid"></param>
        /// <param name="pictureType"></param>
        /// <returns>上传后的文件名</returns>
        private static string SavePostedImage(HttpPostedFile postedFile, int maxHeight, int maxWidth, double rate,string guid,string pictureType)
        {

            try
            {
                ImageFormat imgFormat = null;
                
                    if (postedFile.FileName.ToLower().EndsWith("jpg"))
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }
                    else if (postedFile.FileName.ToLower().EndsWith("gif"))
                    {
                        imgFormat = ImageFormat.Gif;
                    }
                    else if (postedFile.FileName.ToLower().EndsWith("bmp"))
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (postedFile.FileName.ToLower().EndsWith("png"))
                    {
                        imgFormat = ImageFormat.Png;
                    }
                    else if (postedFile.FileName.ToLower().EndsWith("tiff"))
                    {
                        imgFormat = ImageFormat.Tiff;
                    }
                    else if (postedFile.FileName.ToLower().EndsWith("icon"))
                    {
                        imgFormat = ImageFormat.Icon;
                    }
                


                Bitmap bmp = new Bitmap(postedFile.InputStream);
                bmp = IsCMYK(bmp) ? ConvertCMYK(bmp, rate) : bmp;
                if ((bmp.HorizontalResolution > 72) || (bmp.VerticalResolution > 72))
                {
                    bmp.SetResolution(72, 72);
                }
                Bitmap saveBmp;

                if ((bmp.Height > maxHeight) || (bmp.Width > maxWidth))
                {
                    if ((maxHeight == 0) || (maxWidth == 0))
                    {
                        saveBmp = new Bitmap(bmp);
                    }
                    else
                    {
                        Double heightRatio = Convert.ToDouble(maxHeight) / Convert.ToDouble(bmp.Height);
                        Double widthRatio = Convert.ToDouble(maxWidth) / Convert.ToDouble(bmp.Width);
                        Double scaleRatio;
                        scaleRatio = heightRatio > widthRatio ? widthRatio : heightRatio;
                        saveBmp = new Bitmap(bmp, Convert.ToInt32(bmp.Width * scaleRatio), Convert.ToInt32(bmp.Height * scaleRatio));
                    }
                }
                else
                {
                    saveBmp = new Bitmap(bmp);
                }

                //DateTime DateFile = DateTime.Now;
                //string NewFileName = DateFile.Year.ToString() + DateFile.Month.ToString() + DateFile.Day.ToString() + DateFile.Hour.ToString() + DateFile.Minute.ToString();
                string NewFileName = DateTime.Now.ToString("yyyyMMddHHmmss");

                string savepath = GetPicture.GetPictureBasePath(guid);
                string logicpath = GetPicture.GetPicturePath(guid);
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                string filename = GetPicture.GetRandomFileName(6) + postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(".", StringComparison.Ordinal)).ToLower();
                string fullpath = savepath + filename;
                saveBmp.Save(fullpath);
                bmp.Dispose();
                saveBmp.Dispose();
                postedFile.InputStream.Close();
                //MemoryStream ms = new MemoryStream(); 
                //byte[] imagedata = null;
                //if (imgFormat != null) saveBmp.Save(ms, imgFormat);
                //imagedata = ms.GetBuffer();
                var size = 0;//imagedata.Length;
                //保存到Redis
                return UploadPicture.SaveGoodsImage(guid, pictureType, logicpath, filename, fullpath, size);
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }

        #region 获取ImageFlags
        private static string GetImageFlags(Image img)
        {
            ImageFlags FlagVals = (ImageFlags)Enum.Parse(typeof(ImageFlags), img.Flags.ToString());
            return FlagVals.ToString();
        }
        #endregion

        #region 判断CMYK
        private static bool IsCMYK(Image img)
        {
            string FlagVals = GetImageFlags(img);
            if ((FlagVals.IndexOf("Ycck") > -1) || (FlagVals.IndexOf("Cmyk") > -1))
            { return true; }
            else
            { return false; }
        }
        #endregion

        #region 压缩图片
        private static Bitmap ConvertCMYK(Bitmap bmp, double rate)
        {
            rate = rate > 1.0 ? 1.0 : rate;
            rate = rate < 0.5 ? 0.5 : rate;
            Bitmap tmpBmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(tmpBmp);
            if (rate == 0.5)//底质量
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
            }
            if (rate == 1.0)//高质量
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
            else
            {//一般质量
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            }

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            // 将CMYK图片重绘一遍,此时GDI+自动将CMYK格式转换为RGB了
            g.DrawImage(bmp, rect);

            Bitmap returnBmp = new Bitmap(tmpBmp);

            g.Dispose();
            tmpBmp.Dispose();
            bmp.Dispose();

            return returnBmp;
        }
        #endregion
    }
}