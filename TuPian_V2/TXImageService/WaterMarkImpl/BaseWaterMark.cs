using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IService;

namespace WaterMarkImpl
{
    public abstract class BaseWaterMark : IWaterMarkPic
    {
        public IDiskDal DiskDal { get; set; }
        public abstract void WaterMarkPic();
    }
}
