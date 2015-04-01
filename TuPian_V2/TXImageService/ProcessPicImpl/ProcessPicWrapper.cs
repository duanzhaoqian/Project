using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;

namespace ProcessPicImpl
{
    public class ProcessPicWrapper : IProcessPic
    {
        public BaseProcessPic BaseProcessPic { get; set; }
        public ProcessPicWrapper(BaseProcessPic baseProcessPic)
        {
            BaseProcessPic = baseProcessPic;
        }
        public string ProcessPic(string guid, string picturetype, byte[] dataBytes,string fileName)
        {
            Console.WriteLine(this.ToString());
            return BaseProcessPic.ProcessPic(guid, picturetype, dataBytes,fileName);
        }
    }
}
