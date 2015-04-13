using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Mongo
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PagerInfo
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

 

}
