using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using TXCommons.Index;
using System.ComponentModel;
using ServiceStack;

namespace Commons.Sources
{
    public class PremisesDal
    {
        private static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        public PremisesDal()
        {
        }

        #region 楼盘信息
        /// <summary>
        /// 获取楼盘索引信息
        /// </summary>
        /// <returns></returns>
        public List<IndexPremises> GetPremises(string cityId)
        {
            if (string.IsNullOrEmpty(cityId))
            {
                return new List<IndexPremises>();
            }
            var sql = @"SELECT  P.[Id] ,
                                P.[InnerCode] ,
                                P.[ProvinceID] ,
                                P.[ProvinceName] ,
                                P.[CityId] ,
                                P.[CityName] ,
                                P.[DId] ,
                                P.[DName] ,
                                P.[BId] ,
                                P.[BName] ,
                                P.[TId] ,
                                P.[Name] ,
                                P.[ReferencePrice] ,
                                P.[TelePhone] ,
                                P.[SalesStatus] ,
                                P.[Ring] ,
                                P.[PremisesAddress] ,
                                P.[salesAddress] ,
                                P.[Developer] ,
                                P.[BuildingArea] ,
                                P.Phone400,
                                P.IsShow400,
                                --P.TopSort,
                                P.PropertyRight,
                                IsNull(dev.[IsRecommend],0) as IsRecommend,
                                [RoomAreas] = (SELECT STUFF(( SELECT  DISTINCT
                                                                        ','
                                                                        + CONVERT(NVARCHAR(100), BuildingArea)
                                                                FROM    House(NOLOCK) AS k
                                                                WHERE   P.Id = k.PremisesId AND IsDel=0
                                                              FOR
                                                                XML PATH('')
                                                              ), 1, 1, '') ),
                                P.[Area] ,
                                P.[Characteristic] ,
                                P.[BuildingType] ,
                                P.[PropertyType] ,
                                [Renovation] = ( SELECT STUFF(( SELECT  DISTINCT
                                                                        ','
                                                                        + CONVERT(NVARCHAR(10), Renovation)
                                                                FROM    Building AS B
                                                                WHERE   P.Id = B.PremisesId
                                                              FOR
                                                                XML PATH('')
                                                              ), 1, 1, '')
                                               ) ,
                                P.[Lng] ,
                                P.[Lat] ,
                                P.[CreateTime] ,
                                [OpeningTime] = ( SELECT TOP 1
                                                            B.OpeningTime
                                                  FROM      dbo.Building AS B ( NOLOCK )
                                                  WHERE     B.PremisesId = P.ID
                                                            AND B.IsDel = 0
                                                  ORDER BY  B.OpeningTime ASC
                                                ) ,
                                [CheckInTime] = ( SELECT TOP 1
                                                            B.OthersTime
                                                  FROM      dbo.Building AS B ( NOLOCK )
                                                  WHERE     B.PremisesId = P.ID
                                                            AND B.IsDel = 0
                                                  ORDER BY  B.OpeningTime DESC
                                                ) ,
                                P.[UpdateTime]
                        FROM    [dbo].[Premises] AS P ( NOLOCK )
                        Left join Developer as dev (nolock)
                        on p.DeveloperId = dev.Id
                        WHERE   P.IsDel = 0 and p.pageurl is null 
                                AND P.CityId {0}";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, cityId == "-1" ? "not in (253,239,344,355, 205,263,307,155, 243,296)" : " in (" + cityId + ")"));
            var list = new List<IndexPremises>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectPremisesToParticipates(item));
                }
                catch (Exception ex)
                {

                    OptLog.Log("Error：", "Error", "读取楼盘信息：" + ex.Message, false);
                }
            }

            return list;
        }
        /// <summary>
        /// 获取指定楼盘索引信息
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public List<IndexPremises> GetPremisesByIds(string ids)
        {
            var sql = @"SELECT  P.[Id] ,
                                P.[InnerCode] ,
                                P.[ProvinceID] ,
                                P.[ProvinceName] ,
                                P.[CityId] ,
                                P.[CityName] ,
                                P.[DId] ,
                                P.[DName] ,
                                P.[BId] ,
                                P.[BName] ,
                                P.[TId] ,
                                P.[Name] ,
                                P.[ReferencePrice] ,
                                P.[TelePhone] ,
                                P.[SalesStatus] ,
                                P.[Ring] ,
                                P.[PremisesAddress] ,
                                P.[salesAddress] ,
                                P.[Developer] ,
                                P.[BuildingArea] ,
                                P.[Area] ,
                                P.[Characteristic] ,
                                P.[BuildingType] ,
                                P.TelePhone,
                                P.Phone400,
                                P.IsShow400,
                                --P.TopSort,
                                P.PropertyRight,
                                IsNull(dev.[IsRecommend],0) as IsRecommend,
                                [RoomAreas] = (SELECT STUFF(( SELECT  DISTINCT
                                                                        ','
                                                                        + CONVERT(NVARCHAR(100), BuildingArea)
                                                                FROM    House(NOLOCK) AS k
                                                                WHERE   P.Id = k.PremisesId AND IsDel=0
                                                              FOR
                                                                XML PATH('')
                                                              ), 1, 1, '') ),
                                P.[PropertyType] ,
                                --P.[Renovation] ,
                                Renovation = ( SELECT STUFF(( SELECT  DISTINCT
					                    ','
					                    + CONVERT(NVARCHAR(10), Renovation)
				                    FROM    Building AS B
				                    WHERE   P.Id = B.PremisesId
			                          FOR
				                    XML PATH('')
			                          ), 1, 1, '')
	                           ) ,
                                P.[Lng] ,
                                P.[Lat] ,
                                P.[CreateTime] ,
                                [OpeningTime] = ( SELECT TOP 1
                                                            B.OpeningTime
                                                  FROM      dbo.Building AS B ( NOLOCK )
                                                  WHERE     B.PremisesId = P.ID
                                                            AND B.IsDel = 0
                                                  ORDER BY  B.OpeningTime ASC
                                                ) ,
                                [CheckInTime] = ( SELECT TOP 1
                                                            B.OthersTime
                                                  FROM      dbo.Building AS B ( NOLOCK )
                                                  WHERE     B.PremisesId = P.ID
                                                            AND B.IsDel = 0
                                                  ORDER BY  B.OpeningTime DESC
                                                ) ,
                                P.[UpdateTime]
                        FROM    [dbo].[Premises] AS P ( NOLOCK )
                        Left join Developer as dev (nolock)
                        on p.DeveloperId = dev.Id 
                        WHERE   P.IsDel = 0 and p.pageurl is null AND P.Id in ({0})";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, ids));


            var list = new List<IndexPremises>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectPremisesToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取楼盘信息：" + ex.Message, false);
                }
            }

            return list;
        }

        private IndexPremises SelectPremisesToParticipates(DataRow row)
        {
            IndexPremises getPremises = new IndexPremises();

            getPremises.PremisesID = row["Id"] == DBNull.Value ? string.Empty : Convert.ToString(row["Id"]);

            getPremises.InnerCode = row["InnerCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["InnerCode"]);

            getPremises.ProvinceID = row["ProvinceID"] == DBNull.Value ? string.Empty : Convert.ToString(row["ProvinceID"]);

            getPremises.ProvinceName = row["ProvinceName"] == DBNull.Value ? string.Empty : Convert.ToString(row["ProvinceName"]);

            getPremises.CityID = row["CityId"] == DBNull.Value ? string.Empty : Convert.ToString(row["CityId"]);

            getPremises.CityName = row["CityName"] == DBNull.Value ? string.Empty : Convert.ToString(row["CityName"]);

            getPremises.DistrictID = row["DId"] == DBNull.Value ? string.Empty : Convert.ToString(row["DId"]);

            getPremises.DistrictName = row["DName"] == DBNull.Value ? string.Empty : Convert.ToString(row["DName"]);

            getPremises.BusinessID = row["BId"] == DBNull.Value ? string.Empty : Convert.ToString(row["BId"]);

            getPremises.BusinessName = row["BName"] == DBNull.Value ? string.Empty : Convert.ToString(row["BName"]);

            getPremises.Traffics = row["TId"] == DBNull.Value ? string.Empty : Convert.ToString(row["TId"]);

            getPremises.PremisesName = row["Name"] == DBNull.Value ? string.Empty : Convert.ToString(row["Name"]);

            getPremises.PremisesAddress = row["PremisesAddress"] == DBNull.Value ? string.Empty : Convert.ToString(row["PremisesAddress"]);

            getPremises.SalesAddress = row["salesAddress"] == DBNull.Value ? string.Empty : Convert.ToString(row["salesAddress"]);

            getPremises.Developer = row["Developer"] == DBNull.Value ? string.Empty : Convert.ToString(row["Developer"]);

            getPremises.Lng = row["Lng"] == DBNull.Value ? string.Empty : Convert.ToString(row["Lng"]);

            getPremises.Lat = row["Lat"] == DBNull.Value ? string.Empty : Convert.ToString(row["Lat"]);

            getPremises.ReferencePrice = row["ReferencePrice"] == DBNull.Value ? string.Empty : Convert.ToString(row["ReferencePrice"]);
            #region 获取400电话
            string telephone = string.Empty;
            if (row["TelePhone"] != DBNull.Value)
            {
                telephone = Convert.ToString(row["TelePhone"]);
                if (row["IsShow400"] != DBNull.Value && row["Phone400"] != DBNull.Value && Convert.ToBoolean(row["IsShow400"]) && !string.IsNullOrEmpty(Convert.ToString(row["Phone400"])))
                {
                    telephone = Convert.ToString(row["Phone400"]);
                }
            }
            #endregion
            getPremises.TelePhone = telephone;

            getPremises.SalesStatus = row["SalesStatus"] == DBNull.Value ? string.Empty : Convert.ToString(row["SalesStatus"]);

            getPremises.Ring = row["Ring"] == DBNull.Value ? string.Empty : Convert.ToString(row["Ring"]);

            getPremises.BuildingArea = row["BuildingArea"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingArea"]);

            getPremises.Area = row["Area"] == DBNull.Value ? string.Empty : Convert.ToString(row["Area"]);

            getPremises.BuildingType = row["BuildingType"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingType"]);

            getPremises.PropertyType = row["PropertyType"] == DBNull.Value ? string.Empty : Convert.ToString(row["PropertyType"]);

            getPremises.Renovation = row["Renovation"] == DBNull.Value ? string.Empty : Convert.ToString(row["Renovation"]);

            var info = TXCommons.PictureModular.GetPicture.GetPremisesPictureInfo(getPremises.InnerCode, true, "AdvertList", int.Parse(getPremises.CityID));

            //列表（广告图）
            getPremises.ListPictureUrl = info != null && !string.IsNullOrEmpty(info.Path) ? info.Path : string.Empty;

            //开发商LOGO图
            var developerinfo = TXCommons.PictureModular.GetPicture.GetPremisesPictureInfo(getPremises.InnerCode, true, "logo", 0);

            getPremises.DeveloperLOGOUrl = developerinfo.Path ?? string.Empty;

            getPremises.PictureCount = "10";

            getPremises.Characteristic = row["Characteristic"] == DBNull.Value ? string.Empty : Convert.ToString(row["Characteristic"]);

            //getPremises.BrowseCount = 100;

            getPremises.OpeningTime = row["OpeningTime"] == DBNull.Value ? null : Convert.ToString(row["OpeningTime"]);

            getPremises.CheckInTime = row["CheckInTime"] == DBNull.Value ? null : Convert.ToString(row["CheckInTime"]);

            getPremises.UpdateTime = row["UpdateTime"] == DBNull.Value ? DateTime.Now.ToString() : Convert.ToString(row["UpdateTime"]);
              getPremises.PropertyRight = row["PropertyRight"] == DBNull.Value ? string.Empty : Convert.ToString(row["PropertyRight"]);

            getPremises.IsRecommend = row["IsRecommend"] == DBNull.Value ? "0" : Convert.ToString(row["IsRecommend"]);
            //if (row["id"] == "257")
            //{
            //    string str = string.Empty;
            //}
            //getPremises.HouseMinArea = row["HouseMinArea"] == DBNull.Value ? "-1" : Convert.ToString(row["HouseMinArea"]);
            //getPremises.HouseMaxArea = row["HouseMaxArea"] == DBNull.Value ? "-1" : Convert.ToString(row["HouseMaxArea"]);

            getPremises.HouseAreas = GetHouseAreas(row["RoomAreas"] as string);
            return getPremises;
        }


        private string GetHouseAreas(string areas)
        {
            var arealist = string.Empty;
            var alist = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(areas))
                {
                    string[] list = areas.Split(",".ToCharArray());
                    foreach (var area in list)
                    {
                        var areanum = Convert.ToInt32(area);
                        if (areanum < 50)
                            alist.Add("1");
                        else if (areanum == 50)
                        {
                            alist.Add("1");
                            alist.Add("2");
                        }
                        else if (areanum > 50 && areanum < 70)
                            alist.Add("2");
                        else if (areanum == 70)
                        {
                            alist.Add("2");
                            alist.Add("3");
                        }
                        else if (areanum > 70 && areanum < 90)
                            alist.Add("3");
                        else if (areanum == 90)
                        {
                            alist.Add("3");
                            alist.Add("4");
                        }
                        else if (areanum > 90 && areanum < 110)
                            alist.Add("4");
                        else if (areanum == 110)
                        {
                            alist.Add("4");
                            alist.Add("5");
                        }
                        else if (areanum > 110 && areanum < 130)
                            alist.Add("5");
                        else if (areanum == 130)
                        {
                            alist.Add("5");
                            alist.Add("6");
                        }
                        else if (areanum > 130 && areanum < 150)
                            alist.Add("6");
                        else if (areanum == 150)
                        {
                            alist.Add("6");
                            alist.Add("7");
                        }
                        else if (areanum > 150 && areanum < 200)
                            alist.Add("7");
                        else if (areanum == 200)
                        {
                            alist.Add("7");
                            alist.Add("8");
                        }
                        else if (areanum > 200 & areanum < 300)
                            alist.Add("8");
                        else if (areanum == 300)
                        {
                            alist.Add("8");
                            alist.Add("9");
                        }
                        else if (areanum > 300)
                            alist.Add("9");

                    }
                    if (alist.Count > 0)
                    {
                        alist = alist.Distinct<string>().ToList();
                        arealist = alist.Aggregate(arealist, (current, item) => current + "," + item);
                        arealist = arealist.Substring(1);
                    }
                }
            }
            catch
            {
            }
            return arealist;
        }

        /// <summary>
        /// 根据项目特色ID 获取特色名称集合
        /// </summary>
        /// <param name="characteristicIds"></param>
        /// <returns></returns>
        public List<OperNumber> GetCharacteristicByID(string characteristicIds)
        {
            var sql = @"SELECT  Id,Name
                    FROM  PremisesFeature
                    WHERE   Id IN ( {0} ) 
                            AND IsDel = 0";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, string.IsNullOrEmpty(characteristicIds) ? "-1" : characteristicIds));

            List<OperNumber> list = new List<OperNumber>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    var oper = new OperNumber();
                    oper.ID = item["Id"] == DBNull.Value ? 0 : Convert.ToInt32(item["Id"]);
                    oper.Name = item["Name"] == DBNull.Value ? string.Empty : Convert.ToString(item["Name"]);
                    list.Add(oper);
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取地铁信息：" + ex.Message, false);
                }
            }

            return list;
        }



        #endregion

        #region 房源信息

        public List<IndexHouse> GetHouseByNewActivities(string premisesId, string salesStatus, int state, int top)
        {
            var sql = @"SELECT  H.Id AS HouseID ,
                                P.Name AS PremisesName ,
                                B.Name AS BuildingName ,
                                B.NameType,
                                U.Name AS UnitName ,
                                H.[Floor] AS TheFloor ,
                                H.Name AS HouseNumber ,
                                B.TheFloor AS AllFloor ,
                                H.Room ,
                                H.Hall ,
                                H.BuildingArea ,
                                A.BidPrice ,
                                A.[Type] AS ActivityType ,
                                H.TotalPrice ,
                                H.InnerCode
                        FROM    dbo.ActivitiesHouse AS AH
                                JOIN dbo.Activities AS A ON AH.ActivitiesId = A.Id
                                JOIN dbo.House AS H ON AH.HouseId = H.Id
                                JOIN dbo.Unit AS U ON H.UnitId = U.Num  
                                AND ah.PremisesId=u.PremisesId AND AH.BuildingId=u.BuildingId
                                JOIN dbo.Building AS B ON AH.BuildingId = B.Id
                                JOIN dbo.Premises AS P ON AH.PremisesId = P.Id
                        WHERE   AH.IsDel = 0
                                AND H.IsDel = 0
                                AND H.SalesStatus in ({1})
                                AND P.IsDel = 0
                                AND B.IsDel = 0
                                AND AH.PremisesId = {0}
                                AND A.ActivitieState = {2}
                        ORDER BY AH.CreateTime DESC";

            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, premisesId, salesStatus, state));

            var list = new List<IndexHouse>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectHouseToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取楼盘信息：" + ex.Message, false);
                }
            }

            return list;
        }

        private IndexHouse SelectHouseToParticipates(DataRow row)
        {
            IndexHouse getHouse = new IndexHouse();

            getHouse.HouseID = row["HouseID"] == DBNull.Value ? 0 : Convert.ToInt32(row["HouseID"]);

            getHouse.InnerCode = row["InnerCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["InnerCode"]);

            getHouse.PremisesName = row["PremisesName"] == DBNull.Value ? string.Empty : Convert.ToString(row["PremisesName"]);

            getHouse.BuildingName = row["BuildingName"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingName"]);

            getHouse.BuildingType = row["NameType"] == DBNull.Value ? string.Empty : Convert.ToString(row["NameType"]);

            getHouse.UnitName = row["UnitName"] == DBNull.Value ? string.Empty : Convert.ToString(row["UnitName"]);

            getHouse.HouseNumber = row["HouseNumber"] == DBNull.Value ? string.Empty : Convert.ToString(row["HouseNumber"]);

            getHouse.BuildingArea = row["BuildingArea"] == DBNull.Value ? 0 : Convert.ToDouble(row["BuildingArea"]);

            getHouse.Room = row["Room"] == DBNull.Value ? 0 : Convert.ToInt32(row["Room"]);

            getHouse.Hall = row["Hall"] == DBNull.Value ? 0 : Convert.ToInt32(row["Hall"]);

            getHouse.TheFloor = row["TheFloor"] == DBNull.Value ? 0 : Convert.ToInt32(row["TheFloor"]);

            getHouse.AllFloor = row["AllFloor"] == DBNull.Value ? 0 : Convert.ToInt32(row["AllFloor"]);

            getHouse.BidPrice = row["BidPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BidPrice"]);

            getHouse.ActivityType = row["ActivityType"] == DBNull.Value ? 0 : Convert.ToInt32(row["ActivityType"]);

            getHouse.TotalPrice = row["TotalPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TotalPrice"]);

            getHouse.BrowseCount = 100;

            getHouse.PictureUrl = string.Empty;//"http://static.cjkjb.com:8080/static_1.3/kyj/images/examplespicture/sp_img.png";

            return getHouse;
        }

        #endregion

        #region 楼栋开盘时间

        /// <summary>
        /// 获取指定楼盘所有楼栋的开盘时间
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <returns></returns>
        public long GetOpeningTimesByPremisesID(string id)
        {
            var sql = @"SELECT TOP 1
                                OpeningTime
                        FROM    dbo.Building (NOLOCK)
                        WHERE   PremisesId = {0}
                                AND IsDel = 0
                        ORDER BY OpeningTime DESC";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, id));

            long optime = 0;

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    optime = Constant.UNIX_TIMESTAMP(Convert.ToDateTime(item["OpeningTime"]));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取楼栋开盘时间：" + ex.Message, false);
                }
            }

            return optime;
        }

        #endregion

        #region 获取房源居室统计

        public List<Room> GetRoomsByPremisesID(string id)
        {
            var sql = @"SELECT  Room AS RoomNumber ,
                                COUNT(1) AS RoomCount
                        FROM    dbo.House (NOLOCK)
                        WHERE   IsDel = 0
                                AND PremisesId = {0}
                        GROUP BY Room
                        ORDER BY Room ASC
                        ";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, id));

            var list = new List<Room>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    var getRoom = new Room();

                    getRoom.RoomNumber = item["RoomNumber"] == DBNull.Value ? 0 : Convert.ToInt32(item["RoomNumber"]);

                    getRoom.RoomCount = item["RoomCount"] == DBNull.Value ? 0 : Convert.ToInt32(item["RoomCount"]);

                    list.Add(getRoom);

                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "获取房源居室统计：" + ex.Message, false);
                }
            }

            return list;
        }

        #endregion

        #region 地铁

        public List<int> GetTrafficsByPremisesID(string id)
        {
            var sql = @"SELECT  Tid,PId
                        FROM    dbo.PremisesSubway
                        WHERE   VId = {0}
                                AND IsDel = 0";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, id));

            List<int> list = new List<int>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    var tId = item["Tid"] == DBNull.Value ? 0 : Convert.ToInt32(item["Tid"]);
                    var pId = item["PId"] == DBNull.Value ? 0 : Convert.ToInt32(item["PId"]);
                    list.Add(tId);
                    list.Add(pId);
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取地铁信息：" + ex.Message, false);
                }
            }



            return list.Distinct<int>().ToList();
        }

        #endregion

        #region 楼盘排行

        public List<IndexRanking> GetPremisesByRanking(string cityIds)
        {
            var sql = @"SELECT  [Id] ,
                                [ProvinceID] ,
                                [CityId] ,
                                [DId] ,
                                [DName] ,
                                [BId] ,
                                [BName] ,
                                [Name] ,
                                [ReferencePrice] ,
                                [CreateTime],
                                [OpeningTime] = ( SELECT TOP 1
                                                            B.OpeningTime
                                                  FROM      dbo.Building AS B ( NOLOCK )
                                                  WHERE     B.PremisesId = P.ID
                                                            AND B.IsDel = 0
                                                  ORDER BY  B.OpeningTime ASC
                                                )
                        FROM    [dbo].[Premises] AS P ( NOLOCK )
                        WHERE   SalesStatus = 1
                                AND IsDel = 0 AND CityId IN ({0})";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, cityIds));

            var list = new List<IndexRanking>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectRankingToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取排行信息：" + ex.Message, false);
                }
            }

            return list;
        }

        private IndexRanking SelectRankingToParticipates(DataRow row)
        {
            IndexRanking getRanking = new IndexRanking();

            getRanking.PremisesID = row["Id"] == DBNull.Value ? string.Empty : Convert.ToString(row["Id"]);

            getRanking.ProvinceID = row["ProvinceID"] == DBNull.Value ? string.Empty : Convert.ToString(row["ProvinceID"]);

            getRanking.CityID = row["CityId"] == DBNull.Value ? string.Empty : Convert.ToString(row["CityId"]);

            getRanking.DistrictID = row["DId"] == DBNull.Value ? string.Empty : Convert.ToString(row["DId"]);

            getRanking.DistrictName = row["DName"] == DBNull.Value ? string.Empty : Convert.ToString(row["DName"]);

            getRanking.BusinessID = row["BId"] == DBNull.Value ? string.Empty : Convert.ToString(row["BId"]);

            getRanking.BusinessName = row["BName"] == DBNull.Value ? string.Empty : Convert.ToString(row["BName"]);

            getRanking.PremisesName = row["Name"] == DBNull.Value ? string.Empty : Convert.ToString(row["Name"]);

            getRanking.ReferencePrice = row["ReferencePrice"] == DBNull.Value ? string.Empty : Convert.ToString(row["ReferencePrice"]);

            getRanking.ClickRate = TXCommons.Redis.GetOneViewCountValue(string.Format("NewHouseView-{0}", getRanking.PremisesID), FunctionTypeEnum.NewHouseViewCount, Convert.ToInt32(getRanking.CityID)).ToString("#0");

            getRanking.CreateTime = row["CreateTime"] == DBNull.Value ? DateTime.Now.ToString() : Convert.ToString(row["CreateTime"]);

            getRanking.OpeningTime = row["OpeningTime"] == DBNull.Value ? DateTime.Now.ToString() : Convert.ToString(row["OpeningTime"]);

            return getRanking;
        }

        #endregion

        #region 小区图

        #endregion

        #region 户型图

        public List<IndexRoom> GetRooms(string cityId)
        {
            var sql = @"SELECT  H.Id AS HouseID ,
                                H.UnitId ,
                                H.BuildingId ,
		                        B.Name AS BuildingName,
		                        B.NameType AS BuildingNameType,
                                H.RId ,
                                H.Room ,
                                H.Hall ,
                                H.Toilet ,
                                H.BuildingArea ,
                                H.Area ,
                                H.CreateTime,
                                P.Id AS PremisesID ,
                                P.InnerCode ,
                                P.CityId
                        FROM    dbo.House (NOLOCK) AS H
                                JOIN dbo.Premises (NOLOCK) AS P ON p.Id = H.PremisesId
                                LEFT JOIN dbo.Building AS B ON B.Id = H.BuildingId
                        WHERE   H.Id IN ( SELECT    Id
                                          FROM      ( SELECT    MAX(Id) AS Id
                                                      FROM      dbo.House
                                                      WHERE     IsDel = 0
                                                      GROUP BY  PremisesId ,
                                                                UnitId ,
                                                                BuildingId ,
                                                                Room ,
                                                                RId ,
                                                                Area
                                                    ) AS Room )
                                AND P.CityId IN ({0}) AND P.IsDel = 0 AND H.IsDel = 0";

            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, string.IsNullOrEmpty(cityId) ? "-1" : cityId));

            var list = new List<IndexRoom>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectRoomToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取房源户型信息：" + ex.Message, false);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取指定户型索引信息
        /// </summary>
        /// <param name="id">房源ID</param>
        /// <returns></returns>
        public IndexRoom GetRoomByHouseID(string premisesid, string houseid)
        {
            var sql = @"SELECT  H.Id AS HouseID ,
                                H.UnitId ,
                                H.BuildingId ,
		                        B.Name AS BuildingName,
		                        B.NameType AS BuildingNameType,
                                H.RId ,
                                H.Room ,
                                H.Hall ,
                                H.Toilet ,
                                H.BuildingArea ,
                                H.Area ,
                                H.CreateTime,
                                P.Id AS PremisesID ,
                                P.InnerCode ,
                                P.CityId
                        FROM    dbo.House (NOLOCK) AS H
                                JOIN dbo.Premises (NOLOCK) AS P ON p.Id = H.PremisesId
                                LEFT JOIN dbo.Building AS B ON B.Id = H.BuildingId
                        WHERE   H.Id IN ( SELECT    Id
                                          FROM      ( SELECT    MAX(Id) AS Id
                                                      FROM      dbo.House (NOLOCK)
                                                      WHERE     PremisesId = {0}
                                                                AND IsDel = 0
                                                      GROUP BY  UnitId ,
                                                                BuildingId ,
                                                                Room ,
                                                                RId ,
                                                                Area
                                                    ) AS Room
                                          WHERE     Room.Id = {1} )
                                AND H.IsDel = 0";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, premisesid, houseid));

            IndexRoom room = null;

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                room = SelectRoomToParticipates(item);
            }

            return room;
        }

        /// <summary>
        /// 获取指定户型索引信息
        /// </summary>
        /// <param name="id">楼盘Ids，以逗号隔开</param>
        /// <returns></returns>
        public List<IndexRoom> GetRoomByPremisesIds(string premisesids)
        {
            var sql = @"SELECT  H.Id AS HouseID ,
                                H.UnitId ,
                                H.BuildingId ,
		                        B.Name AS BuildingName,
		                        B.NameType AS BuildingNameType,
                                H.RId ,
                                H.Room ,
                                H.Hall ,
                                H.Toilet ,
                                H.BuildingArea ,
                                H.Area ,
                                H.CreateTime,
                                P.Id AS PremisesID ,
                                P.InnerCode ,
                                P.CityId
                        FROM    dbo.House (NOLOCK) AS H
                                JOIN dbo.Premises (NOLOCK) AS P ON P.Id = H.PremisesId
                                LEFT JOIN dbo.Building AS B ON B.Id = H.BuildingId
                        WHERE   H.Id IN ( SELECT    Id
                                          FROM      ( SELECT    MAX(Id) AS Id
                                                      FROM      dbo.House (NOLOCK)
                                                      WHERE     PremisesId IN ({0})
                                                                AND IsDel = 0
                                                      GROUP BY  UnitId ,
                                                                BuildingId ,
                                                                Room ,
                                                                RId ,
                                                                Area
                                                    ) AS Room )
                                AND H.IsDel = 0";
            var dataset = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, string.Format(sql, premisesids));

            var list = new List<IndexRoom>();

            foreach (DataRow item in dataset.Tables[0].Rows)
            {
                try
                {
                    list.Add(SelectRoomToParticipates(item));
                }
                catch (Exception ex)
                {
                    OptLog.Log("Error：", "Error", "读取房源户型信息：" + ex.Message, false);
                }
            }

            return list;
        }

        private IndexRoom SelectRoomToParticipates(DataRow row)
        {
            IndexRoom getRoom = new IndexRoom();

            getRoom.PremisesID = row["PremisesID"] == DBNull.Value ? string.Empty : Convert.ToString(row["PremisesID"]);

            getRoom.HouseID = row["HouseID"] == DBNull.Value ? string.Empty : Convert.ToString(row["HouseID"]);

            getRoom.UnitId = row["UnitId"] == DBNull.Value ? string.Empty : Convert.ToString(row["UnitId"]);

            getRoom.BuildingId = row["BuildingId"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingId"]);

            getRoom.BuildingName = row["BuildingName"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingName"]);

            getRoom.BuildingNameType = row["BuildingNameType"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingNameType"]);

            getRoom.RID = row["RId"] == DBNull.Value ? string.Empty : Convert.ToString(row["RId"]);

            getRoom.Room = row["Room"] == DBNull.Value ? string.Empty : Convert.ToString(row["Room"]);

            getRoom.Hall = row["Hall"] == DBNull.Value ? string.Empty : Convert.ToString(row["Hall"]);

            getRoom.Toilet = row["Toilet"] == DBNull.Value ? string.Empty : Convert.ToString(row["Toilet"]);

            getRoom.BuildingArea = row["BuildingArea"] == DBNull.Value ? string.Empty : Convert.ToString(row["BuildingArea"]);

            getRoom.Area = row["Area"] == DBNull.Value ? string.Empty : Convert.ToString(row["Area"]);

            getRoom.CreateTime = row["CreateTime"] == DBNull.Value ? string.Empty : Convert.ToString(row["CreateTime"]);

            var innercode = row["InnerCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["InnerCode"]);

            var cityId = row["CityId"] == DBNull.Value ? 0 : Convert.ToInt32(row["CityId"]);

            var rid = string.IsNullOrEmpty(getRoom.RID) ? -1 : int.Parse(getRoom.RID);

            var info = TXCommons.PictureModular.GetPicture.GetPremisesPictureByID(innercode, true, "ROOMTYPE", cityId, rid);

            getRoom.PicUrl = string.IsNullOrEmpty(info.Path) ? string.Empty : info.Path;
            getRoom.Title = string.IsNullOrEmpty(info.Title) ? string.Empty : info.Title;
            getRoom.PicDesc = string.IsNullOrEmpty(info.Desc) ? string.Empty : info.Desc;

            return getRoom;
        }

        #endregion

    }

    public class Room
    {
        public int RoomNumber { get; set; }

        public int RoomCount { get; set; }
    }

    public class OperNumber
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

}
