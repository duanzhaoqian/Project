using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;

namespace PicCutImpl
{
    public class PreviewCutPic : BaseCutPic
    {
        public PreviewCutPic(int hight, int width)
            : base(hight, width)
        {
        }

        public override string CutPic(string path)
        {
            using (Bitmap bitmap = new Bitmap(path))
            {
                Stream stream = new MemoryStream();

                using (Bitmap image = new Bitmap(Width, Hight))
                {
                    Graphics graphics = Graphics.FromImage(image);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.Clear(Color.Transparent);
                    graphics.DrawImage(bitmap, new Rectangle(0, 0, Width, Hight), new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                    image.Save(stream, bitmap.RawFormat);
                }
                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Close();
                return base.DiskDal.SavePic(path + ".100_100." + new ImageFormatConverter().ConvertToString(bitmap.RawFormat), bytes);
            }

        }
    }
}
