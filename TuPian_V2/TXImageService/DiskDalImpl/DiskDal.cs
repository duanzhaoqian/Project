using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DiskDalImpl
{
    public class DiskDal:BaseDiskDal
    {
        public DiskDal(string baseSource) : base(baseSource)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public override string SavePic(string path,byte[] bytes)
        {
            string abstractpath = Path.Combine(BaseSource, path);
            if (!Directory.Exists(Path.GetDirectoryName(abstractpath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(abstractpath));
            }
            FileStream fs = new FileStream(abstractpath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.Write(bytes,0,bytes.Length);
            fs.Close();
            return abstractpath;
        }

        public override void DelPic()
        {
            throw new NotImplementedException();
        }
    }
}
