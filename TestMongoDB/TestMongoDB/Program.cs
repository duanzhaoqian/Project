using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MongoDBHelper.Factory;
//using MongoDBHelper.NoQuery;
//using MongoDBHelper.Query;
using TXCommons.Mongo;

namespace TestMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //INoQuery<Student> noQuery = HouseDBFactory<Student>.GetNoQuery("mongodb://192.168.3.153", "test", "test");
            ////object b = noQuery.Update(new List<Student>() { new Student() { _id = 10 }, new Student() { _id = 11 }, new Student() { _id = 13 } });
            //object b = noQuery.Update(new Student() { _id = 10, Name = "bb" });



            //IQuery<Student> query = HouseDBFactory<Student>.GetQuery("mongodb://192.168.3.153", "test", "test");
            //Student student = query.FindOne(10);
            //List<Student> lists = query.FindAll(11, 12, 13, 14);


            //Mongo_HouseModel mongoHouseModel = new Mongo_HouseModel() { _id = "122", Age = 122 };
            //Mongo_NonQuery.Upsert(EnumMongoDBCollectionNames.HouseCollection, mongoHouseModel);
            //List<Mongo_HouseModel> list = Mongo_Query.FindAll(EnumMongoDBCollectionNames.HouseCollection, "122","111");
            Mongo_HouseModel mongoHouseModel = Mongo_Query.FindOne(EnumMongoDBCollectionNames.HouseCollection, "111");
             Console.ReadKey();
        }
    }

    
    class Student
    {
        public int  _id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        //public string Address { get; set; }
    }
}
