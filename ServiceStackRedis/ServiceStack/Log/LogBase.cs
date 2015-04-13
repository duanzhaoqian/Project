using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.Log
{
    public class LogBase
    {
        protected LogBase(string name)
        {
            Name = name;
        }
        protected string Name { get; set; }
    }
}
