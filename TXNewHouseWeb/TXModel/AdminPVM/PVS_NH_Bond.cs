using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVS_NH_Bond
    {
        public int CityId { set; get; }

        public List<SelectListItem> Cities { set; get; }

        public int ProvinceId { set; get; }

        public List<SelectListItem> Provinces { set; get; }

        public string BeginTime { set; get; }

        public string EndTime { set; get; }

        public int UserType { set; get; }

        public List<SelectListItem> UserTypes { set; get; }

        public string KeyWord { set; get; }
    }
}