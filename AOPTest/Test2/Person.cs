using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AOPTest;

namespace Test2
{
    class Person:Base
    {
        static Person()
        {
            Name = "aa";
        }
        public static string Name { get; set; }
        public int Age { get; set; }
        public int Say()
        {

            Console.WriteLine("Name:{0},Age:{1}", Name, Age);
            return Age;
        }
    }
}
