using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDBHelper.Query
{
    public interface IQuery<T> 
    {
        T FindOne(int houseid);
        List<T> FindAll(params int[] houseIds);
    }
}
