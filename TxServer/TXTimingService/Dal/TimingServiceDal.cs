using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TXTimingService.Dal
{
    public  class TimingServiceDal
    {
        string ConnectionString = "";
        public TimingServiceDal(string Connstring)
        {
            ConnectionString = Connstring;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetHouseBid()
        {
            string sql = "DECLARE @HouseData table   " +
                               "  (   " +
                                  "  Id int NOT NULL, " +
                                   "  HouseId int NOT NULL, " +
                                   "  HouseBidStatus int NOT NULL " +
                              "  )   " +
                                "  IF EXISTS ( select Id from HouseTiming where getdate()>=BeginTime and getDate()< EndTime and HouseBidStatus=0 and DealStatus=0  )  " +
                                    "  begin  " +
                                         "  DECLARE @News table  " +
                                           "  (  " +
                                           "  Id int NOT NULL, " +
                                            "  HouseId int NOT NULL, " +
                                            "  HouseBidStatus int NOT NULL, " +
                                            "  BeginTime datetime NOT NULL, " +
                                            "  EndTime datetime NOT NULL, " +
                                            "  DealStatus int NOT NULL, " +
                                            "  DealTime	datetime NOT NULL " +
                                            "  ) " +

                                          "  INSERT INTO @News (Id,HouseId,HouseBidStatus,BeginTime,EndTime,DealStatus,DealTime) " +
                                          "  SELECT Id,HouseId,HouseBidStatus,BeginTime,EndTime,DealStatus,DealTime " +
                                          "  FROM HouseTiming  " +
                                          "  where getdate()>=BeginTime and getDate()< EndTime and HouseBidStatus=0 and DealStatus=0 " +

                                          "  Update  S_LongHouse set BidStatus=1 where Id in (select HouseId from @News) " +


                                          "  INSERT INTO @HouseData (Id, HouseId,HouseBidStatus) " +
                                          "  SELECT Id, HouseId,1 " +
                                          "  FROM @News " +
                                "  end " +

                                 "  IF EXISTS ( select Id from HouseTiming where  getDate()>= EndTime and (HouseBidStatus=1 or HouseBidStatus=0) and DealStatus=0  ) " +
                                    "  begin " +
                                              "  DECLARE @News2 table " +
                                              "  (  " +
                                                   "  Id int NOT NULL, " +
                                                   "  HouseId int NOT NULL, " +
                                                   "  HouseBidStatus int NOT NULL, " +
                                                   "  BeginTime datetime NOT NULL, " +
                                                   "  EndTime datetime NOT NULL, " +
                                                   "  DealStatus int NOT NULL, " +
                                                   "  DealTime	datetime NOT NULL " +
                                              "  ) " +

                                            "  INSERT INTO @News2 (Id,HouseId,HouseBidStatus,BeginTime,EndTime,DealStatus,DealTime) " +
                                             "  SELECT Id,HouseId,HouseBidStatus,BeginTime,EndTime,DealStatus,DealTime " +
                                             "  FROM HouseTiming " +
                                             "  where  getDate()>= EndTime and (HouseBidStatus=1 or HouseBidStatus=0)  and DealStatus=0 " +


                                            "  Update  S_LongHouse set BidStatus=3 where Id in (select HouseId from @News2) " +
                                             "  Update  Investment set Status =3 where HouseId in (select HouseId from @News2) " +
                                            "  INSERT INTO @HouseData (Id,HouseId,HouseBidStatus) " +
                                            "  SELECT Id, HouseId,3 " +
                                            "  FROM @News2 " +

                                   "  end " +

                                 "  select * from @HouseData ";
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql);
        }
    }
}
