using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly[] assemblys=AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblys)
            {
                Console.WriteLine(assembly.FullName);
            }
            Console.ReadKey();
        }
    }
}
