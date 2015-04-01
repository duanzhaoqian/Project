using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Model;
using SQLData;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<S_LongHouseBase> list = new List<S_LongHouseBase>();
            Random random = new Random();
            SQLWrite sqlWrite = new SQLWrite();
            Stopwatch stopwatch1=new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i <100*10000+3; i++)
            {
                S_LongHouseBase sLongHouse = new S_LongHouseBase() { Address = random.Next().GetHashCode().ToString(), AdminId = random.Next(1, 100), AdminName = random.Next().GetHashCode().ToString(), Area = random.NextDouble(), AuthenticationState = random.Next(), AuthenticationTime = DateTime.Now.AddMonths(random.Next(-36, 0)), BId = random.Next(), BName = random.Next().GetHashCode().ToString(), Balcony = random.Next(), BuildingArea = random.NextDouble(), CityId = random.Next(), CityName = random.Next().GetHashCode().ToString(), CompanyName = random.Next().GetHashCode().ToString(), CountryID = random.Next(), CountryName = random.Next().GetHashCode().ToString(), CreateTime = DateTime.Now.AddMonths(random.Next(-36, 0)), DId = random.Next(), DName = random.Next().GetHashCode().ToString(), Hall = random.Next(), HouseType = random.Next(), InnerCode = Guid.NewGuid().ToString(), IsDel = Convert.ToBoolean(random.Next(0, 2)), IsFDVip = Convert.ToBoolean(random.Next(0, 2)), IsHaveP = random.Next(0, 2), IsKjw = Convert.ToBoolean(random.Next(0, 2)), IsReal = Convert.ToBoolean(random.Next(0, 2)), IsRecommend = Convert.ToBoolean(random.Next(0, 2)), IsSVip = Convert.ToBoolean(random.Next(0, 2)), Kitchen = random.Next(), Mobile = random.Next().GetHashCode().ToString(), OldPrice = random.Next(), Orientation = random.Next(), PType = random.Next(), PayType = random.Next(), PropertyType = random.Next(), ProvinceID = random.Next(), ProvinceName = random.Next().GetHashCode().ToString(), QuotedMinPrice = random.Next(), Renovation = random.Next(), RentType = random.Next(), Room = random.Next(), State = random.Next(), Title = random.Next().GetHashCode().ToString(), Toilet = random.Next(), Type = random.Next(), UId = random.Next(), UName = random.Next().GetHashCode().ToString(), UpdatePriceDate = DateTime.Now.AddMonths(random.Next(-36, 0)), UpdateTime = DateTime.Now.AddMonths(random.Next(-36, 0)), UserType = random.Next(), VId = random.Next(), VName = random.Next().GetHashCode().ToString() };
                list.Add(sLongHouse);
            }
            stopwatch1.Stop();
            Console.WriteLine(string.Format("有{0}条数据",list.Count));
            Console.WriteLine(string.Format("消耗时间：{0}",stopwatch1.Elapsed));


            Stopwatch stopwatch2=new Stopwatch();
            stopwatch2.Start();
            sqlWrite.InserDataByInsertNum(list);
            stopwatch2.Stop();
            Console.WriteLine("插入{0}条数据",list.Count);
            Console.WriteLine("插入数据库消耗时间：{0}",stopwatch2.Elapsed);

            Console.ReadKey();
        }
    }
}
