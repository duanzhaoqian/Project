using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        private String str = "good";
        private char[] cha = { 'a', 'b', 'c' };
        private int i = 10;
        static void Main(string[] args)
        {
            Program program = new Program();
            program.change(program.str, program.cha,program.i);
            Console.WriteLine(program.str + " and " + program.cha);
            Console.ReadKey();
        }

        void change(String str, char[] ch,int i)
        {
            string.Intern(str);
            str = "test ok";
            ch[0] = 'g';
            i = 100;
        }
    }
}
