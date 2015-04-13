﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using TXCommons.DBUtility;
using TXCommons.Mongo;

namespace MongoProcess
{
    public class Mongo_House_Dal
    {
        public DataTable GetHouseModels(int beginid, int endid)
        {
            string sql = string.Format(@"SELECT     
dbo.S_LongHouseBase.Id, 
dbo.S_LongHouseBase.Type, 
dbo.S_LongHouseBase.PropertyType,  
dbo.S_LongHouseBase.PType, 
dbo.S_LongHouseBase.IsSVip,   
dbo.S_LongHouseBase.CityId, 
dbo.S_LongHouseBase.CityName, 
dbo.S_LongHouseBase.DId, 
dbo.S_LongHouseBase.DName, 
dbo.S_LongHouseBase.BId, 
dbo.S_LongHouseBase.BName, 
dbo.S_LongHouseBase.UId, 
dbo.S_LongHouseBase.UserType, 
dbo.S_LongHouseBase.UName,  
dbo.S_LongHouseBase.VName, 
dbo.S_LongHouseBase.Mobile,  
dbo.S_LongHouseBase.QuotedMinPrice, 
dbo.S_LongHouseBase.RentType,  
dbo.S_LongHouseBase.Room, 
dbo.S_LongHouseBase.Hall, 
dbo.S_LongHouseBase.Toilet, 
dbo.S_LongHouseBase.Kitchen, 
dbo.S_LongHouseBase.Balcony, 
dbo.S_LongHouseBase.Area, 
dbo.S_LongHouseBase.BuildingArea, 
dbo.S_LongHouseBase.AuthenticationState, 
dbo.S_LongHouseBase.AuthenticationTime, 
dbo.S_LongHouseBase.State, 
dbo.S_LongHouseBase.Title, 
dbo.S_LongHouseBase.CreateTime, 
dbo.S_LongHouseBase.UpdateTime, 
dbo.S_LongHouseBase.IsDel, 
dbo.S_LongHouseBase.IsRecommend, 
dbo.S_LongHouseBase.IsReal, 
dbo.S_LongHouseBase.AdminId, 
dbo.S_LongHouseBase.AdminName, 
dbo.S_LongHouseBase.IsKjw,  
dbo.S_LongHouseBase.IsHaveP,  
dbo.S_LongHouseBase.InnerCode,  
dbo.S_LongHouseBase.Renovation,  
dbo.S_LongHouseBase.IsWill, 
dbo.S_LongHouseBase.WillTime, 
dbo.S_LongHouseBase.Ts, 
dbo.S_LongHouseBase.CNum, 
dbo.S_LongHouseBase.IsBid,
dbo.S_LongHouseBase.OldPrice,
dbo.S_LongHouseBase.UpdatePriceDate,
dbo.S_LongHouseBase.VId,
dbo.S_LongHouseBase.Address,
dbo.S_LongHouseBase.Orientation,
dbo.S_LongHouseBase.HouseType,
dbo.S_LongHouseBase.IsFDVip,


dbo.S_LongHouseExtend.MaxPrice, 
dbo.S_LongHouseExtend.IsPlat, 
dbo.S_LongHouseExtend.Fromurl, 
dbo.S_LongHouseExtend.IsSheng, 
dbo.S_LongHouseExtend.Sentdate, 
dbo.S_LongHouseExtend.SendState, 
dbo.S_LongHouseExtend.IsDaiK, 
dbo.S_LongHouseExtend.SupportingFacilities, 
dbo.S_LongHouseExtend.BidUserNum, 
dbo.S_LongHouseExtend.BuildingStructure, 
dbo.S_LongHouseExtend.BuildingType, 
dbo.S_LongHouseExtend.TheFloar, 
dbo.S_LongHouseExtend.AllFloar, 
dbo.S_LongHouseExtend.OfficeType, 
dbo.S_LongHouseExtend.VideoUrl, 
dbo.S_LongHouseExtend.IsMorePic, 
dbo.S_LongHouseExtend.Lng, 
dbo.S_LongHouseExtend.Lat, 

dbo.S_LongHouseLost.PTypeState, 
dbo.S_LongHouseLost.LoginName, 
dbo.S_LongHouseLost.QuotedPrice,  
dbo.S_LongHouseLost.IsDK, 
dbo.S_LongHouseLost.IsGuarantee,  
dbo.S_LongHouseLost.HouseScore, 
dbo.S_LongHouseLost.IsReturn, 						
dbo.S_LongHouseLost.FromName,
dbo.S_LongHouseLost.LiaisonOfficer,
dbo.S_LongHouseLost.IsOnePrice,
dbo.S_LongHouseLost.BidStatus,
dbo.S_LongHouseLost.SalesManId,
dbo.S_LongHouseLost.SalesManMobile,
dbo.S_LongHouseLost.IsAgency,
dbo.S_LongHouseLost.RType,
dbo.S_LongHouseLost.IsWorry,
dbo.S_LongHouseLost.LiaisonName,
dbo.S_LongHouseLost.AgentName,
dbo.S_LongHouseLost.QuotedMaxPrice,
dbo.S_LongHouseLost.IsMarket,
dbo.S_LongHouseLost.MarketStatus,
dbo.S_LongHouseLost.MarketEndTime,
dbo.S_LongHouseLost.MarketSource,
dbo.S_LongHouseLost.IsGood,
dbo.S_LongHouseLost.IsAdv,
dbo.S_LongHouseLost.KyjOffer,
dbo.S_LongHouseLost.MergeBudget
FROM  dbo.S_LongHouseBase(nolock) 
left JOIN
dbo.S_LongHouseExtend(nolock) ON dbo.S_LongHouseBase.Id = dbo.S_LongHouseExtend.SLHId 
left JOIN
dbo.S_LongHouseLost(nolock) ON dbo.S_LongHouseBase.Id = dbo.S_LongHouseLost.SLHId 
where  S_LongHouseBase.Id >= {0} AND dbo.S_LongHouseBase.Id<={1} ", beginid, endid);

            DataTable dataTable = KYJHouseReadDB.GetTable(sql);

            //List<Mongo_HouseModel> list = DataConvert.DataTableToList<Mongo_HouseModel>(dataTable);

            return dataTable;
        }

        public DataTable GetHouseModels(List<int> listids)
        {
            string sql = string.Format(@"SELECT     
dbo.S_LongHouseBase.Id, 
dbo.S_LongHouseBase.Type, 
dbo.S_LongHouseBase.PropertyType,  
dbo.S_LongHouseBase.PType, 
dbo.S_LongHouseBase.IsSVip,   
dbo.S_LongHouseBase.CityId, 
dbo.S_LongHouseBase.CityName, 
dbo.S_LongHouseBase.DId, 
dbo.S_LongHouseBase.DName, 
dbo.S_LongHouseBase.BId, 
dbo.S_LongHouseBase.BName, 
dbo.S_LongHouseBase.UId, 
dbo.S_LongHouseBase.UserType, 
dbo.S_LongHouseBase.UName,  
dbo.S_LongHouseBase.VName, 
dbo.S_LongHouseBase.Mobile,  
dbo.S_LongHouseBase.QuotedMinPrice, 
dbo.S_LongHouseBase.RentType,  
dbo.S_LongHouseBase.Room, 
dbo.S_LongHouseBase.Hall, 
dbo.S_LongHouseBase.Toilet, 
dbo.S_LongHouseBase.Kitchen, 
dbo.S_LongHouseBase.Balcony, 
dbo.S_LongHouseBase.Area, 
dbo.S_LongHouseBase.BuildingArea, 
dbo.S_LongHouseBase.AuthenticationState, 
dbo.S_LongHouseBase.AuthenticationTime, 
dbo.S_LongHouseBase.State, 
dbo.S_LongHouseBase.Title, 
dbo.S_LongHouseBase.CreateTime, 
dbo.S_LongHouseBase.UpdateTime, 
dbo.S_LongHouseBase.IsDel, 
dbo.S_LongHouseBase.IsRecommend, 
dbo.S_LongHouseBase.IsReal, 
dbo.S_LongHouseBase.AdminId, 
dbo.S_LongHouseBase.AdminName, 
dbo.S_LongHouseBase.IsKjw,  
dbo.S_LongHouseBase.IsHaveP,  
dbo.S_LongHouseBase.InnerCode,  
dbo.S_LongHouseBase.Renovation,  
dbo.S_LongHouseBase.IsWill, 
dbo.S_LongHouseBase.WillTime, 
dbo.S_LongHouseBase.Ts, 
dbo.S_LongHouseBase.CNum, 
dbo.S_LongHouseBase.IsBid,
dbo.S_LongHouseBase.OldPrice,
dbo.S_LongHouseBase.UpdatePriceDate,
dbo.S_LongHouseBase.VId,
dbo.S_LongHouseBase.Address,
dbo.S_LongHouseBase.Orientation,
dbo.S_LongHouseBase.HouseType,
dbo.S_LongHouseBase.IsFDVip,


dbo.S_LongHouseExtend.MaxPrice, 
dbo.S_LongHouseExtend.IsPlat, 
dbo.S_LongHouseExtend.Fromurl, 
dbo.S_LongHouseExtend.IsSheng, 
dbo.S_LongHouseExtend.Sentdate, 
dbo.S_LongHouseExtend.SendState, 
dbo.S_LongHouseExtend.IsDaiK, 
dbo.S_LongHouseExtend.SupportingFacilities, 
dbo.S_LongHouseExtend.BidUserNum, 
dbo.S_LongHouseExtend.BuildingStructure, 
dbo.S_LongHouseExtend.BuildingType, 
dbo.S_LongHouseExtend.TheFloar, 
dbo.S_LongHouseExtend.AllFloar, 
dbo.S_LongHouseExtend.OfficeType, 
dbo.S_LongHouseExtend.VideoUrl, 
dbo.S_LongHouseExtend.IsMorePic, 

dbo.S_LongHouseLost.PTypeState, 
dbo.S_LongHouseLost.LoginName, 
dbo.S_LongHouseLost.QuotedPrice,  
dbo.S_LongHouseLost.IsDK, 
dbo.S_LongHouseLost.IsGuarantee,  
dbo.S_LongHouseLost.HouseScore, 
dbo.S_LongHouseLost.IsReturn, 						
dbo.S_LongHouseLost.FromName,
dbo.S_LongHouseLost.LiaisonOfficer,
dbo.S_LongHouseLost.IsOnePrice,
dbo.S_LongHouseLost.BidStatus,
dbo.S_LongHouseLost.SalesManId,
dbo.S_LongHouseLost.SalesManMobile,
dbo.S_LongHouseLost.IsAgency,
dbo.S_LongHouseLost.RType,
dbo.S_LongHouseLost.IsWorry,
dbo.S_LongHouseLost.LiaisonName,
dbo.S_LongHouseLost.AgentName,
dbo.S_LongHouseLost.QuotedMaxPrice,
dbo.S_LongHouseLost.IsMarket,
dbo.S_LongHouseLost.MarketStatus,
dbo.S_LongHouseLost.MarketEndTime,
dbo.S_LongHouseLost.MarketSource,
dbo.S_LongHouseLost.IsGood,
dbo.S_LongHouseLost.IsAdv,
dbo.S_LongHouseLost.KyjOffer,
dbo.S_LongHouseLost.MergeBudget

FROM  dbo.S_LongHouseBase(nolock) 
left JOIN
dbo.S_LongHouseExtend(nolock) ON dbo.S_LongHouseBase.Id = dbo.S_LongHouseExtend.SLHId 
left JOIN
dbo.S_LongHouseLost(nolock) ON dbo.S_LongHouseBase.Id = dbo.S_LongHouseLost.SLHId 
where  S_LongHouseBase.Id in ({0})", string.Join(",", listids));

            DataTable dataTable = KYJHouseReadDB.GetTable(sql);

            //List<Mongo_HouseModel> list = DataConvert.DataTableToList<Mongo_HouseModel>(dataTable);

            return dataTable;
        }
    }

    internal class DataConvert
    {
        internal static List<T> DataTableToList<T>(DataTable dataTable) where T : new()
        {
            List<T> list = new List<T>();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach (DataRow row in dataTable.Rows)
            {
                T t = new T();
                foreach (PropertyInfo info in propertyInfos)
                {
                    if (info.Name == "_id")
                    {
                        info.SetValue(t, row["Id"].ToString(), null);
                    }
                    else
                    {
                        DateTime datetime;
                        double dou;
                        if (dataTable.Columns.Contains(info.Name))
                        {
                        if (Double.TryParse(row[info.Name].ToString(), out dou))
                        {
                            info.SetValue(t, Convert.ChangeType(row[info.Name].ToString(), info.PropertyType), null);
                        }
                        else if (DateTime.TryParse(row[info.Name].ToString(), out datetime))
                        {
                            info.SetValue(t, Convert.ChangeType(datetime.ToString("yyyy-MM-dd HH:mm:ss"), info.PropertyType), null);
                        }
                        else
                        {
                            info.SetValue(t, ChangeType(row[info.Name].ToString(), info.PropertyType), null);
                        }
                        }

                    }


                }
                list.Add(t);
            }
            return list;
        }

        internal static object ChangeType(object obj, Type type)
        {
            object t = type.IsValueType ? Activator.CreateInstance(type) : null;
            try
            {
                t = Convert.ChangeType(obj, type);
            }
            catch
            {

            }
            return t;
        }
    }
}
