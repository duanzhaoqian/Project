using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;

namespace ProcessPicImpl
{
    public abstract class BaseProcessPic : IProcessPic
    {
        public List<ICutPic> CutPic { get; set; }

        public IDiskDal DiskDal { get; set; }

        public IWaterMarkPic WaterMarkPic { get; set; }

        public abstract string ProcessPic(string guid, string picturetype, byte[] dataBytes, string fileName);
    }
}
