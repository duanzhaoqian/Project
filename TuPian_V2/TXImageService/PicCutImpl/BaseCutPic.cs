using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;

namespace PicCutImpl
{
    public abstract class BaseCutPic : ICutPic
    {
        public BaseCutPic(int hight, int width)
        {
            Hight = hight;
            Width = width;
        }
        public int Hight { get; set; }
        public int Width { get; set; }
        //public string PicSource { get; set; }
        public IDiskDal DiskDal { get; set; }
        public abstract string CutPic(string picpath);

    }
}
