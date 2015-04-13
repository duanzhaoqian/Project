using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KYJ.ServiceStack;
using ServiceStack;
using ServiceStack.TCP;
using System.Threading;
using ServiceStack.Mongo;

namespace ServiceStackRedis
{
    class Program
    {
        static void Main(string[] args)
        {

            string key = "";
           
            bool boolsoincr = RedisHelperv2.SetValue<string>("1aaaaazs", "1", FunctionTypeEnum.HouseImage, 253, 1);

            IList<string> listkeys = RedisHelperv2.Keys("1aaaaazs*", FunctionTypeEnum.HouseImage, 253);
           string strsoincr = RedisHelperv2.GetValue<string>("1aaaaazs",  FunctionTypeEnum.HouseImage, 253);
            
           IList<RentHousePictureInfo> listzzz = RedisHelperv2.GetAllItemsFromSortedSet<RentHousePictureInfo>("renthouse:livingroom:4e3f5501-4b50-4642-8bbf-9decaceaa1f1", FunctionTypeEnum.HouseImage, 253);

           Console.WriteLine("count：" + listzzz.Count);

           Console.WriteLine("完成");
           Console.ReadLine();
           /*
        double soincr = RedisHelperv2.GetNextSequence("1zs", FunctionTypeEnum.HouseImage, 253);
        double soincr1 = RedisHelperv2.GetNextSequence("1zs", FunctionTypeEnum.HouseImage, 253);
        string key = "3";

        SampleClass c = new SampleClass()
        {
            id = 7,
            name = "zhangsan",
            sex = true
        };

        SampleClass c2 = new SampleClass()
        {
            id = 2,
            name = "lisi",
            sex = true
        };
        bool type = true;

        Thread t = new Thread(t1 =>
        {
            for (int i = 0; i < 10000; i++)
            {
                var bbool = RedisHelperv2.SetValue("set123", "123", FunctionTypeEnum.HouseImage, 253, 1);
                Console.WriteLine("完成all1" + bbool.ToString() + i);
            }

        });
        t.Start();


        Thread t2 = new Thread(t1 =>
        {
            for (int i = 0; i < 10000; i++)
            {
                var bbool = RedisHelperv2.SetValue("set123", "123", FunctionTypeEnum.HouseImage, 253, 1);
                Console.WriteLine("完成all2" + bbool.ToString() + i);
            }

        });
        t2.Start();
        Console.WriteLine("all2");
        Console.ReadLine();
        string ssszs = RedisHelperv2.GetValue<string>("set123", FunctionTypeEnum.HouseImage, 253);




        IDictionary<SampleClass, double> sdichight = RedisHelperv2.GetRangeWithScoresFromSortedSetByHighestScore<SampleClass>(key, FunctionTypeEnum.HouseImage, 253);

        IDictionary<SampleClass, double> sdiclower = RedisHelperv2.GetRangeWithScoresFromSortedSetByLowestScore<SampleClass>(key, FunctionTypeEnum.HouseImage, 253);

        //RedisHelperv2.AddItemToSortedSet<SampleClass>(c, "3", 7, FunctionTypeEnum.HouseImage, 253);
        int cccount = RedisHelperv2.GetSortedSetCount("3", FunctionTypeEnum.HouseImage, 253);

        double d = RedisHelperv2.GetItemScoreInSortedSet<SampleClass>("3", c2, FunctionTypeEnum.HouseImage, 253);
        IDictionary<SampleClass, double> sdic = RedisHelperv2.GetAllWithScoresFromSortedSet<SampleClass>("3", FunctionTypeEnum.HouseImage, 253);

        RedisHelperv2.AddItemToSortedSet<SampleClass>(c, "3", 3, FunctionTypeEnum.HouseImage, 253);



        IList<SampleClass> zlist = RedisHelperv2.GetAllItemsFromSortedSet<SampleClass>("3", FunctionTypeEnum.HouseImage, 253);

        RedisHelperv2.RemoveItemFromSortedSet("3", c2, FunctionTypeEnum.HouseImage, 253);
        //type = RedisHelperv2.SetValue<SampleClass>(key, c1, FunctionTypeEnum.HouseImage, 253);
        //RedisHelperv2.RemoveAll(key,FunctionTypeEnum.HouseImage,253);
        RedisHelperv2.RemoveItemFromList<SampleClass>("2", c2, FunctionTypeEnum.HouseImage, 253);
        type = RedisHelperv2.AddItemToList<SampleClass>("2", c2, FunctionTypeEnum.HouseImage, 253);
        type = RedisHelperv2.AddItemToList<SampleClass>("2", c, FunctionTypeEnum.HouseImage, 253);



        //RedisHelperv2.RemoveAll(new[] { "1", "2" }, FunctionTypeEnum.HouseImage, 253);
        List<SampleClass> sssslis = new List<SampleClass>();
        sssslis.Add(c);
        sssslis.Add(c2);
        RedisHelperv2.AddItemToList<List<SampleClass>>(key, sssslis, FunctionTypeEnum.HouseImage, 253);
        RedisHelperv2.Remove(key, FunctionTypeEnum.HouseImage, 253);


        RedisHelperv2.SetValue<string>(key, "1", FunctionTypeEnum.HouseImage, 253);


        RedisHelperv2.SetValue("2", "2", FunctionTypeEnum.HouseImage, 253);
        List<string> lli = new List<string>();
        lli.Add("1");
        lli.Add("2");
        IList<object> slist = RedisHelperv2.GetValues<string>(lli, FunctionTypeEnum.HouseImage, 253);

        key.RedisJson().Set(FunctionTypeEnum.HouseImage, CityEnum.BeiJing, c, 1);
        SampleClass z = key.RedisJson().Get<SampleClass>(FunctionTypeEnum.HouseImage, CityEnum.BeiJing);
        //key.RedisString().Set(FunctionTypeEnum.HouseImage, CityEnum.BeiJing, "2", 1);
        //string v = key.RedisJson().Get<string>(FunctionTypeEnum.HouseImage, CityEnum.BeiJing);

        //key.RedisString().Set(FunctionTypeEnum.HouseImage, CityEnum.Other, "2", 1);
        //string v1 = key.RedisJson().Get<string>(FunctionTypeEnum.HouseImage, CityEnum.Other);

        //key.RedisString().Set(FunctionTypeEnum.HouseImage, CityEnum.TianJin, "2", 1);
        //string v3 = key.RedisJson().Get<string>(FunctionTypeEnum.HouseImage, CityEnum.TianJin);

        //new RedisClient().Get("1");
        new RedisClient(FunctionTypeEnum.WebDBData, CityEnum.BeiJing).Set("1", "1", DataType.String);

        Redis.Set<string>("AdvertCount", "[{\"Date\":\"\\/Date(1399737600000+0800)\\/\",\"count\":15},{\"Date\":\"\\/Date(1399824000000+0800)\\/\",\"count\":15},{\"Date\":\"\\/Date(1399910400000+0800)\\/\",\"count\":13},{\"Date\":\"\\/Date(1399996800000+0800)\\/\",\"count\":14},{\"Date\":\"\\/Date(1399046400000+0800)\\/\",\"count\":81},{\"Date\":\"\\/Date(1399132800000+0800)\\/\",\"count\":79},{\"Date\":\"\\/Date(1399219200000+0800)\\/\",\"count\":56},{\"Date\":\"\\/Date(1399305600000+0800)\\/\",\"count\":43},{\"Date\":\"\\/Date(1399392000000+0800)\\/\",\"count\":29},{\"Date\":\"\\/Date(1399478400000+0800)\\/\",\"count\":21},{\"Date\":\"\\/Date(1399564800000+0800)\\/\",\"count\":18},{\"Date\":\"\\/Date(1399651200000+0800)\\/\",\"count\":14},{\"Date\":\"\\/Date(1400256000000+0800)\\/\",\"count\":16},{\"Date\":\"\\/Date(1400083200000+0800)\\/\",\"count\":12},{\"Date\":\"\\/Date(1400169600000+0800)\\/\",\"count\":14},{\"Date\":\"\\/Date(1400342400000+0800)\\/\",\"count\":12},{\"Date\":\"\\/Date(1400428800000+0800)\\/\",\"count\":10},{\"Date\":\"\\/Date(1400515200000+0800)\\/\",\"count\":4},{\"Date\":\"\\/Date(1400601600000+0800)\\/\",\"count\":1}]", null, FunctionTypeEnum.ADPosition, null);

        bool ssss = Redis.Set<int>("NewHouseRank_53", 1, null, FunctionTypeEnum.NewHousePropertyRank, null);
        List<string> s = Redis.GetValues<List<string>>(FunctionTypeEnum.NewHouseViewCount, null, "h_262,h_248,h_245,h_244,h_243,h_242,h_241,h_240,h_238,h_237,h_236,h_235,h_234,h_233,h_232,h_231,h_230,h_229,h_228,h_227");
        UserPictureInfo iu = new UserPictureInfo();
        iu.FileName = "s7pd2w.jpg";
        iu.ID = 31258;
        iu.Path = "85/7a/62/857a6209-986b-44a5-b1d4-66ee4b5ad06b/s7pd2w.jpg";
        iu.PictureType = "LOGO";

        Redis.AddItemToSortedSet<UserPictureInfo>("User:LOGO:857a6209-986b-44a5-b1d4-66ee4b5ad06b", iu, 0, null, FunctionTypeEnum.UserHeadImage, CityEnum.BeiJing);

        //List<string> sss = KYJ.ServiceStack.Redis.GetAllItemsFromList<List<string>>("13681667695");

        //string key = "RentHouse001";
        #region GetSet测试
        string str = "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1+B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee++";
        //KYJ.ServiceStack.Redis.Set<string>("uuiu", str, null, FunctionTypeEnum.BidPrice, CityEnum.BeiJing);
        //string html = KYJ.ServiceStack.Redis.Get<string>("uuiu", FunctionTypeEnum.BidPrice, CityEnum.BeiJing);
        //string s = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(str);

        #endregion

        //string str = "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1+B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee++";

        //KYJ.ServiceStack.Redis.GetNextSequence<SampleClass>();//自增 

        //bool istrue= KYJ.ServiceStack.Redis.Remove("seq:SampleClass");
        //string key = "123";
        //string testHtml = Tool.GetPostHtml(Tool.ConfigKey("offerPrice"), "opt=zadd&n=3&cmd=3&key=123321&val=123");
        List<RentHousePictureInfo> lists = new List<RentHousePictureInfo>();
        RentHousePictureInfo en1 = new RentHousePictureInfo();

        //List<RentHousePictureInfo> sli = KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<List<RentHousePictureInfo>>("RentHouse:LONGLIST:2410b04b-68b2-4aff-af4c-c9fcb16e847b");

        en1.Desc = null;
        en1.ID = 3624;
        en1.IsNew = 1;
        en1.IsVillage = 0;
        en1.Path = "62/99/0d/62990d13-c39d-4163-8024-67801694617c/n0m1x7.jpg";
        en1.PictureType = "BEDROOM";
        en1.Vid = 0;

        //KYJ.ServiceStack.Redis.Set<RentHousePictureInfo>(key + "001", en1, null,"");

        //bool istrue = KYJ.ServiceStack.Redis.AddItemToSortedSet<RentHousePictureInfo>("RentHouse:BEDROOM:62990d13-c39d-4163-8024-67801694617c", en1, 3, DateTime.Now.AddHours(10));
        //List<RentHousePictureInfo> ss_ = KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<List<RentHousePictureInfo>>("RentHouse:BEDROOM:62990d13-c39d-4163-8024-67801694617c");//
        List<RentHousePictureInfo> li = new List<RentHousePictureInfo>();
        for (int i = 0; i < 3; i++)
        {
            RentHousePictureInfo info = new RentHousePictureInfo();
            info.Desc = "";
            info.ID = i;
            info.IsNew = 0;
            info.IsVillage = 0;
            info.Path = "cb/32/ad/cb32ada8-7096-4c04-a934-586ead8dbea8/vi4kak.jpg";
            info.PictureType = "LONGLIST";
            info.Vid = 0;
            li.Add(info);
        }

        //KYJ.ServiceStack.Redis.AddItemToSortedSet<List<RentHousePictureInfo>>(key + "002", li, 3, null, FunctionTypeEnum.HouseImage, CityEnum.ShangHai);

        //KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<List<RentHousePictureInfo>>(key + "002", FunctionTypeEnum.HouseImage, CityEnum.BeiJing);


        KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<List<RentHousePictureInfo>>(key + "002", FunctionTypeEnum.HouseImage, CityEnum.ShangHai);

        KYJ.ServiceStack.Redis.AddItemToList<List<RentHousePictureInfo>>(key + "002", li, null, null, CityEnum.BeiJing);
        RentHousePictureInfo info_ = new RentHousePictureInfo();
        info_.Desc = "";
        info_.ID = Redis.GetNextSequence<RentHousePictureInfo>(null, CityEnum.BeiJing);
        info_.IsNew = 0;
        info_.IsVillage = 0;
        info_.Path = "RentHouse:BEDROOM:2f87951c-ae70-4618-915f-3521a1fb8a082f/87/95/2f87951c-ae70-4618-915f-3521a1fb8a08/9o2bwp.png";
        info_.PictureType = "BEDROOM";
        info_.Vid = 0;
        IDictionary<RentHousePictureInfo, double> dicdou = Redis.GetRangeWithScoresFromSortedSetByHighestScore<RentHousePictureInfo>(key + "110", null, CityEnum.BeiJing);

        bool ss = KYJ.ServiceStack.Redis.AddItemToSortedSet<RentHousePictureInfo>(key + "110", info_, 3, DateTime.Now.AddMinutes(20), null, CityEnum.BeiJing);//DateTime.Now.AddMinutes(50)
        IDictionary<RentHousePictureInfo, double> dz = KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByLowestScore<RentHousePictureInfo>(key + "4", null, CityEnum.BeiJing);


        //double dd = KYJ.ServiceStack.Redis.GetItemScoreInSortedSet(key + "3", ss);


        //IDictionary<RentHousePictureInfo, double> d = KYJ.ServiceStack.Redis.GetAllWithScoresFromSortedSet<RentHousePictureInfo>(key + "2", null, CityEnum.BeiJing);


        //bool ll = KYJ.ServiceStack.Redis.RemoveItemFromSortedSet(key + "2", li);


        //KYJ.ServiceStack.Redis.AddItemToList<List<RentHousePictureInfo>>(key + "11", li, DateTime.Now.AddMinutes(10));
        bool stat = KYJ.ServiceStack.Redis.RemoveItemFromList<List<RentHousePictureInfo>>(key + "11", li, null, CityEnum.BeiJing);


        //KYJ.ServiceStack.Redis.AddItemToList<List<RentHousePictureInfo>>(key + "11", li, DateTime.Now.AddMonths(20));
        //List<RentHousePictureInfo> sss = KYJ.ServiceStack.Redis.GetAllItemsFromList<List<RentHousePictureInfo>>(key + "11");

        List<RentHousePictureInfo> asd = KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<List<RentHousePictureInfo>>(key + "2", null, CityEnum.BeiJing);


        IDictionary<RentHousePictureInfo, double> dicByHighestScorett = KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByHighestScore<RentHousePictureInfo>(key + "2", null, CityEnum.BeiJing);

        //RentHousePictureInfo llRentHousePictureInfo = KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<RentHousePictureInfo>(key + "2");//DateTime.Now.AddMinutes(50)




        var Getstat = KYJ.ServiceStack.Redis.GetAllItemsFromList<List<string>>(key + "11", null, CityEnum.BeiJing);


        KYJ.ServiceStack.Redis.GetNextSequence<SampleClass>(null, CityEnum.BeiJing);//自增 
        //ServiceStack.Redis.Set<string>("t00", str, DateTime.Now.AddDays(1));
        /*

        */

            List<SampleClass> listSampleClass = new List<SampleClass>();

            for (int i = 1; i < 3; i++)
            {
                SampleClass e = new SampleClass();
                e.id = i;
                e.name = "李四";
                e.sex = true;
                listSampleClass.Add(e);
            }
            KYJ.ServiceStack.Redis.AddItemToList<List<SampleClass>>(key + "11", listSampleClass, DateTime.Now.AddMinutes(10), null, CityEnum.BeiJing);
            List<string> listSample = new List<string>();
            for (int i = 1; i < 3; i++)
            {
                listSample.Add(i.ToString());
            }

            #region SortedSet（有序集合） 测试



            List<string> l = KYJ.ServiceStack.Redis.GetAllItemsFromSortedSet<List<string>>(key + "2", null, CityEnum.BeiJing);


            IDictionary<string, double> dicByHighestScore = KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByHighestScore<string>(key + "2", null, CityEnum.BeiJing);

            IDictionary<string, double> dicByLowestScore = KYJ.ServiceStack.Redis.GetRangeWithScoresFromSortedSetByLowestScore<string>(key + "2", null, CityEnum.BeiJing);

            IDictionary<string, double> dicFromSortedSet = KYJ.ServiceStack.Redis.GetAllWithScoresFromSortedSet<string>(key + "2", null, CityEnum.BeiJing);

            KYJ.ServiceStack.Redis.RemoveItemFromSortedSet<List<string>>(key + "2", listSample, null, CityEnum.BeiJing);

            #endregion
            #region List（列表）测试

            //KYJ.ServiceStack.Redis.AddItemToList<List<string>>(key + "1", listSample, DateTime.Now.AddMinutes(10));

            KYJ.ServiceStack.Redis.AddItemToList<List<SampleClass>>(key + "11", listSampleClass, DateTime.Now.AddMinutes(10), null, CityEnum.BeiJing);

            //List<string> GetAllItemsFromListstring = KYJ.ServiceStack.Redis.GetAllItemsFromList<List<string>>(key + "1");

            List<SampleClass> GetAllItemsFromList = KYJ.ServiceStack.Redis.GetAllItemsFromList<List<SampleClass>>(key + "11", null, CityEnum.BeiJing);

            #endregion

            #region GetSet测试
            //string str = "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1+B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee++";
            //KYJ.ServiceStack.Redis.Set<string>("uuiu", str, null);
            //string html = KYJ.ServiceStack.Redis.Get<string>("uuiu");
            //string s = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(str);

            #endregion
        }
    }
}
