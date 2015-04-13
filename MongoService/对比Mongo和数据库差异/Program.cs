using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoProcess;
using ServiceStack.Mongo;

namespace 对比Mongo和数据库差异
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                MongoProcess.MongoDBProcess mongoDbProcess = new MongoDBProcess();
                StringBuilder stringBuilder = new StringBuilder();
                List<IMongoQuery> listQueries = new List<IMongoQuery>();
                string[] strings = File.ReadAllLines("MongoQuery.txt");
                Regex regex = new Regex(@"(\w+)(\W+)(\w+)");
                foreach (string s in strings)
                {
                    Match match = regex.Match(s);
                    string str1 = match.Groups[1].Value;
                    string str2 = match.Groups[2].Value;
                    string str3 = match.Groups[3].Value;
                    bool b3;
                    int i3;
                    switch (str2)
                    {
                        case "=":
                            if (str3.ToLower() == "true" || str3.ToLower() == "false")
                            {
                                listQueries.Add(Query.EQ(str1, BsonValue.Create(Convert.ToBoolean(str3))));
                            }
                            else if (int.TryParse(str3, out i3))
                            {
                                listQueries.Add(Query.EQ(str1, BsonValue.Create(i3)));

                            }
                            break;
                        case "!=":
                        case "<>":
                            if (str3.ToLower() == "true" || str3.ToLower() == "false")
                            {
                                listQueries.Add(Query.Not(Query.EQ(str1, BsonValue.Create(Convert.ToBoolean(str3)))));
                            }
                            else if (int.TryParse(str3, out i3))
                            {
                                listQueries.Add(Query.Not(Query.EQ(str1, BsonValue.Create(i3))));

                            }
                            break;
                    }

                }
                List<BsonDocument> listMongoHouseModels = mongoDbProcess.MongoHelper.GetAll<BsonDocument>("HouseCollection",
                   Query.And(listQueries), "_id");
                List<int> listMongoIds = listMongoHouseModels.Select(c => Convert.ToInt32(c.GetValue("_id").AsString)).ToList();//.Select(c => Convert.ToInt32(c._id)).Distinct().ToList();
                listMongoIds.Sort();
                stopwatch.Stop();
                Console.WriteLine("MongoId数量:{0},消耗时间:{1}", listMongoIds.Count, stopwatch.Elapsed);
                stringBuilder.AppendLine(string.Format("MongoId数量:{0},消耗时间:{1}", listMongoIds.Count, stopwatch.Elapsed));
                stopwatch.Restart();
                string sql = File.ReadAllText("SelectSqlId.txt");

                DataTable dataTable = mongoDbProcess.HouseDal.GetDataTableBySql(sql);
                List<int> listSqlIds = new List<int>();
                foreach (DataRow row in dataTable.Rows)
                {
                    listSqlIds.Add(Convert.ToInt32(row["id"]));
                }
                listSqlIds = listSqlIds.Distinct().ToList();
                listSqlIds.Sort();
                stopwatch.Stop();
                Console.WriteLine("SqlId数量:{0},消耗时间:{1}", listSqlIds.Count, stopwatch.Elapsed);
                stringBuilder.AppendLine(string.Format("SqlId数量:{0},消耗时间:{1}", listSqlIds.Count, stopwatch.Elapsed));
                Console.WriteLine("MonID条数{0},SQLID条数{1}", listMongoIds.Count, listSqlIds.Count);
                stringBuilder.AppendLine(string.Format("MonID条数{0},SQLID条数{1}", listMongoIds.Count, listSqlIds.Count));
                List<int> listExcept = listSqlIds.Except(listMongoIds).ToList();
                Console.WriteLine("差异条数:{0}", listExcept.Count);
                stringBuilder.AppendLine(string.Format("差异条数:{0}", listExcept.Count));
                if (listExcept.Count > 0)
                {
                    File.WriteAllText("ExceptIds.txt", string.Join("\r\n", listExcept));
                }
                stringBuilder.AppendLine("======" + DateTime.Now.ToString() + "======");
                File.AppendAllText("Record.txt", stringBuilder.ToString());
                Console.ReadKey();
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message + Environment.NewLine + exception.StackTrace);
                Console.ReadLine();
            }
        }
    }
}
