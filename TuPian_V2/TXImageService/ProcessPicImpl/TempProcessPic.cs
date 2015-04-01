using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;
using System.IO;
namespace ProcessPicImpl
{
    public class TempProcessPic : BaseProcessPic
    {
        public override string ProcessPic(string guid, string picturetype, byte[] dataBytes,string fileName)
        {
            string path=base.DiskDal.SavePic(Path.Combine(guid,fileName),dataBytes);
            return CutPic.First().CutPic(path);
        }
    }
}
