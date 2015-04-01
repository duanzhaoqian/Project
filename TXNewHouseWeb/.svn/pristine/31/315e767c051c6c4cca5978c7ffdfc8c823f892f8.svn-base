using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ImageTestDao.Base.Impl;

namespace ImageTestDao
{
    public class GetPremises : BaseDao
    {
        public DataTable GetPremisesInnrtCodeAndCityId()
        {
            const string sql = @"Select Id,InnerCode,CityId from Premises where IsDel = 0";
            return ExecuteQuery(sql);
        }
    }
}
