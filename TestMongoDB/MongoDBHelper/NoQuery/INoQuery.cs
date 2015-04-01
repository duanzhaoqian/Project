using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDBHelper.NoQuery
{
    public interface INoQuery<T>
    {
        object Update<T>(T t);
        object Update<T>(List<T> list);
    }
}
