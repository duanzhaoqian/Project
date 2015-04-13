using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Log;

namespace LogTest
{

    class Person : LogClassBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        [LogFileAttribute("dzq", "test", new Type[] { typeof(FileLogger), typeof(MailLogger) }, new object[]{null,new object[]{"subject","duanchaoqian@kuaiyoujia.com",""}})]
        public bool Say(Person p, int i, out int j)
        {
            throw new Exception("hello aop");
            //Console.WriteLine("Name:{0},Age{1}", Name, Age);
            return true;
        }
    }
}
