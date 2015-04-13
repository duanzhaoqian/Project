using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.Log
{
    public interface ILog
    {
        void Log(string content,string name);
        void LogError(string content, string name,Exception exception);
    }
}
