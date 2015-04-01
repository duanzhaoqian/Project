using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace DrawingPictureHelp
{
    public class DrawPic
    {
        public string DirPath { get; set; }

        public void BeginDrawing(string guid)
        {
            string dpath = Path.Combine(DirPath, guid.Substring(0, 2), guid.Substring(2, 2), guid);
            if (!Directory.Exists(dpath))
            {
                Directory.CreateDirectory(dpath);
            }
            string fName = guid.GetHashCode() + ".jpg";
            string fullName = Path.Combine(dpath, fName);
            using (Image image=new Bitmap(guid.Length*20,50))
            {
                using (System.Drawing.Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.AliceBlue);
                    graphics.DrawString(guid, new Font(new FontFamily("黑体"), 20), new HatchBrush(HatchStyle.Percent90, Color.Black), new PointF(10, 15));
                }
                image.Save(fullName,ImageFormat.Jpeg);
            }
        }
    }
}
