using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IService
{
    public interface ICutPic
    {
       // IDiskDal DiskDal { get; set; }
        string CutPic(string picpath);
    }
}
