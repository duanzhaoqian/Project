using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;
using System.IO;

namespace DiskDalImpl
{
    public abstract class BaseDiskDal : IDiskDal
    {
        public string BaseSource { get; set; }
        public BaseDiskDal(string baseSource)
        {
            BaseSource = baseSource;
        }
        public abstract string SavePic(string path,byte[] bytes);

        public abstract void DelPic();
    }
}
