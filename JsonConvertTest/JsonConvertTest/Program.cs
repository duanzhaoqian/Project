using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace JsonConvertTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> list = new List<Student>();
            list.Add(new Student() { Age = 18, Name = "18" });
            list.Add(new Student() { Age = 20, Name = "20" });
            string str = JsonConvert.SerializeObject(list);


            string s= JsonConvert.DeserializeObject<object>(str).ToString();
            Console.ReadKey();
        }
      
    }
}
