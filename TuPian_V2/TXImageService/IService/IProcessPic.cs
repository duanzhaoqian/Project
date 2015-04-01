using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IService
{
    /// <summary>
    /// 加工图片基接口
    /// </summary>
    public interface IProcessPic
    {
     //   ICutPic CutPic { get; set; }
      //  IDiskDal DiskDal { get; set; }
       // IWaterMarkPic WaterMarkPic { get; set; }
        string ProcessPic(string guid, string picturetype, byte[] dataBytes, string fileName);
    }
}
