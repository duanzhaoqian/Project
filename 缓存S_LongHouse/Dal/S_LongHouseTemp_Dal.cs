using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Command;

namespace Dal
{
    public class S_LongHouseTemp_Dal : DalBase
    {
        private string connectionString = Config.S_LongHouse_Cache_ConnectionString;
        public DataTable SelectData()
        {
            string sql = @"with #temp as
(select 
a.Id, a.Type, a.PropertyType, a.HouseType, a.PType, a.IsSVip, a.CountryID, a.CountryName, a.ProvinceID, a.ProvinceName, a.CityId, a.CityName, a.DId, a.DName, a.BId, a.BName, a.VId, a.UId, a.UserType, a.UName, a.CompanyName, a.VName, a.Address, a.Mobile, a.InnerCode, a.QuotedMinPrice, a.RentType, a.PayType, a.Room, a.Hall, a.Toilet, a.Kitchen, a.Balcony, a.Area, a.BuildingArea, a.Orientation, a.Renovation, a.AuthenticationState, a.AuthenticationTime, a.State, a.Title, a.CreateTime, a.UpdateTime, a.IsDel, a.IsRecommend, a.IsReal, a.AdminId, a.AdminName, a.SLHId, a.OfficeType, a.PropertyRight, a.Lng, a.Lat, a.BuildingStructure, a.BuildingType, a.BuildingYear, a.theFloar, a.allFloar, a.SupportingFacilities, a.IndoorFacilities, a.MaxPrice, a.MinPrice, a.SearchTime, a.CheckInTime, a.VideoUrl, a.ParticipateNum, a.BidCount, a.BidUserNum, a.BidStartTime, a.BidEndTime, a.IsPlat, a.IsTxfree, a.IsFullYears, a.IsUnique, a.IsMorePic, a.fromurl, a.IsSheng, a.sentdate, a.SendState, a.RecTime, a.IsDaiK, a.Expr1, a.PrivateNum, a.PTypeState, a.IsOnePrice, a.IsBargaining, a.IsAgency, a.LoginName, a.QuotedPrice, a.QuotedMaxPrice, a.IsCut, a.BidStatus, a.ViewCount, a.IsDK, a.IsGuarantee, a.AgentId, a.SalesManId, a.HouseScore, a.IsReturn, a.IsWorry, a.OldPrice, a.UpdatePriceDate, a.IsKjw, a.IsFDVip
,b.id as SId
from s_longhouseCache a left join s_longhouse b on a.id=b.id)
select * from #temp";
            return this.SelectData(sql, connectionString);
        }
    }
}
