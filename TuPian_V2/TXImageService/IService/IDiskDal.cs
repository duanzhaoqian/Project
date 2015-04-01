using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IService
{
    public interface IDiskDal
    {
        string SavePic(string path,byte[] bytes);
        void DelPic();
    }
}
