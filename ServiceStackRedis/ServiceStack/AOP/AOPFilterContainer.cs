using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ServiceStack.AOP
{
    internal class AOPFilterContainer : IAOPFilter
    {
        private readonly ReadOnlyCollection<IAOPFilter> _filters;

        public AOPFilterContainer(IEnumerable<IAOPFilter> filters)
        {
            _filters =new ReadOnlyCollection<IAOPFilter>(filters.ToList());
        }

        public void Execute(IProcess processer)
        {
            using (var enumerator = _filters.GetEnumerator())
            {
                var wrapper = new ProcesserWrapper(processer, enumerator);
                wrapper.Process();
            }
        }

    }
}
