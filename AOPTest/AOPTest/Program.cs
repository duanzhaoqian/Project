using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person();
            p.Age = 8;
            //p.Name = "aaa";
          int a=  p.Say();



          Person p1 = new Person();
          p1.Age = 18;
          //p.Name = "aaa";
          int a1 = p1.Say();
            Console.ReadKey();
        }
    }
}
