using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using IService;

namespace PicCutImpl
{
    public class CutPicWrapper : ICutPic
    {
        public BaseCutPic BaseCutPic { get; set; }
        public CutPicWrapper(BaseCutPic baseCutPic)
        {
            BaseCutPic = baseCutPic;
        }

        public IDiskDal DiskDal { get; set; }
        public string CutPic(string path)
        {
            Console.WriteLine(this.ToString());
            return BaseCutPic.CutPic(path);
        }
    }
}
