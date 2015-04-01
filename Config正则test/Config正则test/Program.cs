using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Config正则test
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = ConfigurationManager.AppSettings["CheckHtmlContentRegex"];
            string str1 = File.ReadAllText("TextFile1.txt");
            if (Regex.IsMatch(str1,str,RegexOptions.IgnoreCase))
            {
                int i = 0;
            }
              Console.ReadKey();
        }
    }
}
