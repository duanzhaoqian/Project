using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.AOP;

namespace ServiceStack.Log
{
    [AOP]
    public abstract class LogClassBase : ContextBoundObject
    {
    }
}
