using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Commons.Collections;
using NVelocity;
using NVelocity.App;

namespace NVelocityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NVelocity.App.VelocityEngine velocityEngine = new VelocityEngine();
            velocityEngine.SetProperty(NVelocity.Runtime.RuntimeConstants.INPUT_ENCODING,"utf-8");
            velocityEngine.SetProperty(NVelocity.Runtime.RuntimeConstants.RESOURCE_LOADER,"file");
            velocityEngine.SetProperty(NVelocity.Runtime.RuntimeConstants.OUTPUT_ENCODING,"utf-8");
            velocityEngine.Init();
            Student student=new Student(){Age = 20,Name = "AAA"};
           
            NVelocity.VelocityContext velocityContext=new VelocityContext();
            StringWriter textWriter=new StringWriter();
            velocityContext.Put("String", new StringHelp());
            velocityContext.Put("Student", student);
            Template template = velocityEngine.GetTemplate("template.vm");
            //velocityEngine.MergeTemplate("template.vm", Encoding.UTF8.EncodingName, velocityContext, textWriter);
            template.Merge(velocityContext,textWriter);
            string result = textWriter.GetStringBuilder().ToString();
            Console.ReadKey();
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string Say()
        {
            return Name;
        }
    }

    public class StringHelp
    {
        public bool IsNullOrEmpty(string str)
        {
            return String.IsNullOrEmpty(str);
        }
    }
}
