using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Mongo;

namespace MongoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertMongo();
        }
        //static void Main(string[] args)
        //{
        //}
        public static bool InsertMongo()
        {
            string table = "LuckUserAwardInfoTbl";
            string Conn = System.Configuration.ConfigurationSettings.AppSettings["MongoWebDB"].ToString();
            string serverconnstr = Conn.Split(';')[0];
            string serverconnDB = Conn.Split(';')[1];

            TestClass test = new TestClass()
            {
                Name = "zhangsan",
                Pwd = "123"
            };

            MongoHelper mongo = new MongoHelper(serverconnstr, serverconnDB);
            mongo.InsertOne<TestClass>(table, test);
            return true;
        }
    }
}
