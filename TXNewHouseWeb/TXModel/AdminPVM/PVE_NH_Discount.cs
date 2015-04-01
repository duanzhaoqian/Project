using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVE_NH_Discount
    {
        public int Id { set; get; }

        public int PremisesId { set; get; }

        public List<SelectListItem> Premises { set; get; }

        public string Name { set; get; }

        public decimal Bond { set; get; }

        public string BondString { set; get; }

        public DateTime BeginTime { set; get; }

        public string BeginTimeString { set; get; }

        public DateTime EndTime { set; get; }

        public string EndTimeString { set; get; }



        public int BuildingId { set; get; }

        public List<SelectListItem> Buildings { set; get; }

        public int UnitId { set; get; }

        public List<SelectListItem> Units { set; get; }

        public int SalesStatus { set; get; }

        public List<SelectListItem> SalesStatusList { set; get; }

        public string HousesJsonString { set; get; }

        public List<PVE_NH_Discount_JoinHouse> DiscountHouses { set; get; }
    }
}