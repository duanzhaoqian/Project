using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using TXOrm;

namespace DAL
{
    public class SHouseDal
    {
        private kyj_HouseDBEntities kyjHouseDbEntities = new kyj_HouseDBEntities();
        public List<T> GetAllData<T>() where T : EntityObject
        {
            kyjHouseDbEntities.CommandTimeout = 3600;
            return kyjHouseDbEntities.CreateObjectSet<T>().OrderBy(c=>1).Skip(1000000).Take(500000).ToList();
        }
    }
}
