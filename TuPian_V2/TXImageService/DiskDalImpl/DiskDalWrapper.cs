using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IService;

namespace DiskDalImpl
{
    public class DiskDalWrapper:IDiskDal
    {
        public BaseDiskDal BaseDiskDal { get; set; }
        public DiskDalWrapper(BaseDiskDal baseDiskDal)
        {
            BaseDiskDal = baseDiskDal;
        }
        public string SavePic(string path,byte[] bytes)
        {
            Console.WriteLine(this.ToString());
            return BaseDiskDal.SavePic(path,bytes);
        }

        public void DelPic()
        {
            throw new NotImplementedException();
        }
    }
}
