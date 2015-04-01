using System;
using System.Collections.Generic;
using System.Text;

namespace Model //ÐÞ¸ÄÃû×Ö¿Õ¼ä
{
	public class S_LongHouseBase
	{
		private int id;
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
	
		private int type;
		public int Type
		{
			get { return type; }
			set { type = value; }
		}
	
		private int propertyType;
		public int PropertyType
		{
			get { return propertyType; }
			set { propertyType = value; }
		}
	
		private int houseType;
		public int HouseType
		{
			get { return houseType; }
			set { houseType = value; }
		}
	
		private int pType;
		public int PType
		{
			get { return pType; }
			set { pType = value; }
		}
	
		private bool isSVip;
		public bool IsSVip
		{
			get { return isSVip; }
			set { isSVip = value; }
		}
	
		private int countryID;
		public int CountryID
		{
			get { return countryID; }
			set { countryID = value; }
		}
	
		private string countryName;
		public string CountryName
		{
			get { return countryName; }
			set { countryName = value; }
		}
	
		private int provinceID;
		public int ProvinceID
		{
			get { return provinceID; }
			set { provinceID = value; }
		}
	
		private string provinceName;
		public string ProvinceName
		{
			get { return provinceName; }
			set { provinceName = value; }
		}
	
		private int cityId;
		public int CityId
		{
			get { return cityId; }
			set { cityId = value; }
		}
	
		private string cityName;
		public string CityName
		{
			get { return cityName; }
			set { cityName = value; }
		}
	
		private int dId;
		public int DId
		{
			get { return dId; }
			set { dId = value; }
		}
	
		private string dName;
		public string DName
		{
			get { return dName; }
			set { dName = value; }
		}
	
		private int bId;
		public int BId
		{
			get { return bId; }
			set { bId = value; }
		}
	
		private string bName;
		public string BName
		{
			get { return bName; }
			set { bName = value; }
		}
	
		private int vId;
		public int VId
		{
			get { return vId; }
			set { vId = value; }
		}
	
		private int uId;
		public int UId
		{
			get { return uId; }
			set { uId = value; }
		}
	
		private int userType;
		public int UserType
		{
			get { return userType; }
			set { userType = value; }
		}
	
		private string uName;
		public string UName
		{
			get { return uName; }
			set { uName = value; }
		}
	
		private string companyName;
		public string CompanyName
		{
			get { return companyName; }
			set { companyName = value; }
		}
	
		private string vName;
		public string VName
		{
			get { return vName; }
			set { vName = value; }
		}
	
		private string address;
		public string Address
		{
			get { return address; }
			set { address = value; }
		}
	
		private string mobile;
		public string Mobile
		{
			get { return mobile; }
			set { mobile = value; }
		}
	
		private string innerCode;
		public string InnerCode
		{
			get { return innerCode; }
			set { innerCode = value; }
		}
	
		private decimal quotedMinPrice;
		public decimal QuotedMinPrice
		{
			get { return quotedMinPrice; }
			set { quotedMinPrice = value; }
		}
	
		private int rentType;
		public int RentType
		{
			get { return rentType; }
			set { rentType = value; }
		}
	
		private int payType;
		public int PayType
		{
			get { return payType; }
			set { payType = value; }
		}
	
		private int room;
		public int Room
		{
			get { return room; }
			set { room = value; }
		}
	
		private int hall;
		public int Hall
		{
			get { return hall; }
			set { hall = value; }
		}
	
		private int toilet;
		public int Toilet
		{
			get { return toilet; }
			set { toilet = value; }
		}
	
		private int kitchen;
		public int Kitchen
		{
			get { return kitchen; }
			set { kitchen = value; }
		}
	
		private int balcony;
		public int Balcony
		{
			get { return balcony; }
			set { balcony = value; }
		}
	
		private double area;
		public double Area
		{
			get { return area; }
			set { area = value; }
		}
	
		private double buildingArea;
		public double BuildingArea
		{
			get { return buildingArea; }
			set { buildingArea = value; }
		}
	
		private int orientation;
		public int Orientation
		{
			get { return orientation; }
			set { orientation = value; }
		}
	
		private int renovation;
		public int Renovation
		{
			get { return renovation; }
			set { renovation = value; }
		}
	
		private int authenticationState;
		public int AuthenticationState
		{
			get { return authenticationState; }
			set { authenticationState = value; }
		}
	
		private DateTime authenticationTime;
		public DateTime AuthenticationTime
		{
			get { return authenticationTime; }
			set { authenticationTime = value; }
		}
	
		private int state;
		public int State
		{
			get { return state; }
			set { state = value; }
		}
	
		private string title;
		public string Title
		{
			get { return title; }
			set { title = value; }
		}
	
		private DateTime createTime;
		public DateTime CreateTime
		{
			get { return createTime; }
			set { createTime = value; }
		}
	
		private DateTime updateTime;
		public DateTime UpdateTime
		{
			get { return updateTime; }
			set { updateTime = value; }
		}
	
		private bool isDel;
		public bool IsDel
		{
			get { return isDel; }
			set { isDel = value; }
		}
	
		private bool isRecommend;
		public bool IsRecommend
		{
			get { return isRecommend; }
			set { isRecommend = value; }
		}
	
		private bool isReal;
		public bool IsReal
		{
			get { return isReal; }
			set { isReal = value; }
		}
	
		private int adminId;
		public int AdminId
		{
			get { return adminId; }
			set { adminId = value; }
		}
	
		private string adminName;
		public string AdminName
		{
			get { return adminName; }
			set { adminName = value; }
		}
	
		private decimal oldPrice;
		public decimal OldPrice
		{
			get { return oldPrice; }
			set { oldPrice = value; }
		}
	
		private DateTime updatePriceDate;
		public DateTime UpdatePriceDate
		{
			get { return updatePriceDate; }
			set { updatePriceDate = value; }
		}
	
		private bool isKjw;
		public bool IsKjw
		{
			get { return isKjw; }
			set { isKjw = value; }
		}
	
		private bool isFDVip;
		public bool IsFDVip
		{
			get { return isFDVip; }
			set { isFDVip = value; }
		}
	
		private int isHaveP;
		public int IsHaveP
		{
			get { return isHaveP; }
			set { isHaveP = value; }
		}
	}
}