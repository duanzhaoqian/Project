using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.AOP
{
    public abstract class AOPFilterAttribute : Attribute, IAOPFilter
    {
        public virtual void Execute(IProcess processer)
        {
            processer.Process();
        }
        public abstract void ExecuteBefore(IProcess process);
        public abstract void ExecuteAfter(IProcess process);
    }
}
