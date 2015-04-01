﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace TXModel.AdminPVM
{
    public class PVM_NH_Purchase
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public int PremisesId { set; get; }

        public string PremisesName { set; get; }

        public int BuildingId { set; get; }

        public List<SelectListItem> Buildings { set; get; }

        public int UserCount { set; get; }

        public DateTime BeginTime { set; get; }

        public DateTime EndTime { set; get; }

        public decimal Bond { set; get; }

        public string HousesJsonString { set; get; }

        public List<PVE_NH_Purchase_JoinHouse> JoinHouses { set; get; }

        public string BeginTimeString { set; get; }
        public string EndTimeString { set; get; }
        public string BondString { set; get; }

        public int ActivitieState { set; get; }
    }
}