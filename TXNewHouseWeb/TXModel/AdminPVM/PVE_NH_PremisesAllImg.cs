using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TXModel.AdminPVM
{
    public class PVE_NH_PremisesAllImg<T>
    {
        public int PremisesID { get; set; }
        public string PremisesName { get; set; }
        public string InnerCode { get; set; }
        public int CityID { get; set; }
        public List<T> ImgList { get; set; }
    }
}
