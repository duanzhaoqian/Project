using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOPTest
{
    [MyAOPProxyAttribute]
    public class Base : ContextBoundObject
    {
    }
}
