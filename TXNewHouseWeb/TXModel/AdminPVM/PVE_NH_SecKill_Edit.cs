using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVE_NH_SecKill_Edit
    {
        public int ActivityId { set; get; }

        public int HouseId { set; get; }

        public int PremisesId { set; get; }

        public List<SelectListItem> Premisess { set; get; }

        public int BuildingId { set; get; }

        public List<SelectListItem> Buildings { set; get; }

        public int UnitNum { set; get; }

        public int Floor { set; get; }

        public int SalesStatus { set; get; }

        public List<SelectListItem> SalesStatusList { set; get; }

        public decimal BidPrice { set; get; }

        public string BidPriceString { set; get; }

        public decimal Bond { set; get; }

        public string BondString { set; get; }

        public DateTime BeginTime { set; get; }

        public string BeginTimeString { set; get; }

        public DateTime EndTime { set; get; }

        public string EndTimeString { set; get; }

        public int ActivitieState { set; get; }
    }
}