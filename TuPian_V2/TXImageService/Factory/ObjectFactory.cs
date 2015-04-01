using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DiskDalImpl;
using IService;
using PicCutImpl;
using ProcessPicImpl;

namespace Factory
{
    public class ObjectFactory
    {
        public static IProcessPic GetTempProcessPic()
        {
            TempProcessPic tempProcessPic = new TempProcessPic();
            DiskDal diskDal = new DiskDal(ConfigurationManager.AppSettings["BaseSource"]);
            PreviewCutPic previewCutPic = new PreviewCutPic(123, 180);
            previewCutPic.DiskDal = new DiskDalWrapper(diskDal);
            tempProcessPic.CutPic =new List<ICutPic>();
            tempProcessPic.CutPic.Add(new CutPicWrapper(previewCutPic));
            tempProcessPic.DiskDal = new DiskDalWrapper(diskDal);
            IProcessPic processPic = new ProcessPicWrapper(tempProcessPic);


            return processPic;
        }
    }
}
