using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;

namespace 反射Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            A aa = new A();
            aa.ListB = new List<B>() { new B() { Id = 1 }, new B() { Id = 2 } };
            aa.ListC = new List<C>() { new C() { Id = 11, Name = "11" }, new C() { Id = 12, Name = "12" } };
            Type type = aa.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo info in propertyInfos)
            {

                if (info.PropertyType.IsGenericType)
                {
                    //Type type1 = info.PropertyType.GetGenericTypeDefinition();

                    //Type[] ttt = info.PropertyType.GetGenericParameterConstraints();

                    object obj = info.GetValue(aa, null);
                    List<B> iCollection = obj as List<B>;
                  
                    Type[] types = info.PropertyType.GetGenericArguments();
                    if (types.Any())
                    {
                        PropertyInfo[] propertyInfosTypes = types[0].GetProperties();
                        foreach (PropertyInfo infosType in propertyInfosTypes)
                        {
                            Attribute attribute = Attribute.GetCustomAttribute(infosType, typeof(NameAttribute));
                            if (attribute != null)
                            {
                               

                                int a = 11;
                            }
                        }
                    }
                }
            }
        }
    }

    public class A
    {
        public int Id { get; set; }
        public List<B> ListB { get; set; }
        public List<C> ListC { get; set; }
    }

    public class B
    {
        public int Id { get; set; }
    }

    public class C
    {
        public int Id { get; set; }
        [Name]
        public string Name { get; set; }
    }

    public class NameAttribute : Attribute
    {

    }
}
