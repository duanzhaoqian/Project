using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;
using MongoDal;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Wrappers;
using MongoProcess;

namespace MongoDBDataInit
{
    class Program
    {
        private static void ShowLog(string str)
        {
            Console.WriteLine(str);
        }
        static void Main(string[] args)
        {
            //int a3 = int.MaxValue;
            //a3 = a3 + 1;


            //Console.WriteLine(a3);
            //unsafe
            //{
            //  //  int a5 = 1;
            //    int a1 = 10;
            //    int* a2 = &a1;
            //    int* arr = stackalloc int[10];
                
            //    for (int i = 0; i < 10; i++)
            //    {
            //        arr[i] = i;
            //    }
            //    Console.WriteLine(*(arr+2));
            //    arr = arr + 1;
            //    Console.WriteLine(*(arr+2));
            //    Console.WriteLine();
            //    Console.WriteLine(*a2);
            //    *a2 = 20;
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine(arr[i]);
            //    }
            //    Console.WriteLine(a1);
            //}
            MongoDBProcess mongoDbProcess = new MongoDBProcess();
            if (args.Length==2)
            {
                int startId = Convert.ToInt32(args[0]);
                int endId = Convert.ToInt32(args[1]);
                DataTable dt = mongoDbProcess.HouseDal.GetDataTableBySql("Select Max(id) As MaxId from S_LongHouseBase(NOLOCK)");
                int maxid = Convert.ToInt32(dt.Rows[0]["MaxId"]);
                int buclkcount = 10000;
                endId = Math.Min(maxid, endId);
                int count = (int)Math.Ceiling((double)(endId - startId) / buclkcount);
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine("生成第{0}批，共{1}批", i + 1, count);

                    mongoDbProcess.Process(startId + i * buclkcount, startId + (i + 1) * buclkcount, ShowLog);
                }
                
            }
            else
            {
                Console.WriteLine("正在生成");
                Console.WriteLine("*****选择操作******");
                Console.WriteLine("*****1、生成所有******");
                Console.WriteLine("*****2、执行sql******");
                Console.WriteLine("*****3、分批生成所有******");
                char readkey = Console.ReadKey().KeyChar;
                Console.WriteLine();
                Console.WriteLine("正在生成");

                switch (readkey)
                {
                    case '1':
                        Console.WriteLine("生成所有数据，按任意键继续");
                        Console.WriteLine("不支持操作");
                        break;
                    case '2':
                        try
                        {
                            string sql = File.ReadAllText("SelectID.sql");
                            Console.WriteLine("读取到的SQL为{0}", sql);
                            Console.WriteLine("按任意键继续执行");
                            Console.ReadKey();
                            DataTable dataTable = mongoDbProcess.HouseDal.GetDataTableBySql(sql);
                            List<int> listids = new List<int>();
                            int arrcount = 10000;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                listids.Add(Convert.ToInt32(row["Id"]));
                            }
                            listids.Sort(new Comparison<int>((c, d) => d.CompareTo(c)));
                            Console.WriteLine("获得ID数量：{0}", listids.Count);
                            if (listids.Count > 0)
                            {
                                List<int> listidsNew = new List<int>();

                                int n = (int)Math.Ceiling(listids.Count * 1.0 / arrcount);
                                int l = listids.Count % arrcount;
                                int length;
                                for (int i = 0; i < n; i++)
                                {
                                    if (i == n - 1)
                                    {
                                        length = l;
                                    }
                                    else
                                    {
                                        length = arrcount;
                                    }
                                    Console.WriteLine("生成第{0}批，共{1}批", i + 1, n);
                                    listidsNew.AddRange(listids.GetRange(i * arrcount, length));
                                    mongoDbProcess.Process(listidsNew, ShowLog);
                                    listidsNew.Clear();
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        break;
                    case '3':
                        Console.WriteLine("输入起始ID");
                        int startId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("输入结束ID");
                        int endId = Convert.ToInt32(Console.ReadLine());
                        DataTable dt = mongoDbProcess.HouseDal.GetDataTableBySql("Select Max(id) As MaxId from S_LongHouseBase(NOLOCK)");
                        int maxid = Convert.ToInt32(dt.Rows[0]["MaxId"]);
                        int buclkcount = 10000;
                        endId = Math.Min(maxid, endId);
                        int count = (int)Math.Ceiling((double)(endId - startId) / buclkcount);
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine("生成第{0}批，共{1}批", i + 1, count);

                            mongoDbProcess.Process(startId + i * buclkcount, startId + (i + 1) * buclkcount, ShowLog);
                        }

                        //List<Mongo_HouseModel> lists = mongoHouseDal.GetHouseModels(0,100000);
                        //foreach (Mongo_HouseModel model in lists)
                        //    {
                        //        Mongo_NonQuery.Upsert(EnumMongoDBCollectionNames.HouseCollection, model);
                        //        Console.WriteLine("添加成功：房源ID：" + model._id);
                        //    }
                        break;
                }


            }
          

            //List<Mongo_HouseModel> list1s = Mongo_Query.FindAll(EnumMongoDBCollectionNames.HouseCollection);
            //Mongo_NonQuery.Upsert(EnumMongoDBCollectionNames.HouseCollection, Query.EQ("_id", BsonValue.Create("1")),
            //    Update.Unset("abc"));

            //  BsonElement[] bsonElements = new BsonElement[2];
            // BsonElement bson1 = new BsonElement("$set", BsonValue.Create(new {OldPrice1=DateTime.Now,UpdatePrice=DateTime.Now}));
            //BsonElement bson2 = new BsonElement("$set", DateTime.Now.ToString());
            //   bsonElements[0] = bson1;
            //var obj = new {OldPrice1 = DateTime.Now, UpdatePrice1 = "44"};
            //Mongo_NonQuery.Upsert(EnumMongoDBCollectionNames.HouseCollection,
            //    Query.EQ("_id", "1"), new UpdateDocument("$set",obj.ToBsonDocument()));
            //UpdateBuilder updateBuilder=   Update.Set("aa", "bb");
            //   UpdateDocument updateDocument = new UpdateDocument("$set",
            //       new {aaa="bbbb",bbb="cccc"}.ToBsonDocument());
            //   Mongo_NonQuery.Upsert(EnumMongoDBCollectionNames.HouseCollection,
            //    Query.EQ("_id", "1"),updateDocument);
            Console.WriteLine("生成完成");
            Console.ReadKey();
        }
    }
}
