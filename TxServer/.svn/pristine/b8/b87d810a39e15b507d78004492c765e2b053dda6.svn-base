using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace AdvertConfigure
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string str = Profile.IniReadValue("public", "BaseDataConnectionString");
                SearchServiceDal dal = new SearchServiceDal(str);

                DataTable areatable = dal.GetArea();

                DataTable districttable = dal.GetDistrict();

                DataTable businesstable = dal.GetBusiness();

                foreach (DataRow row in areatable.Rows)
                {
                    dal.AddCityData(row);
                }

                foreach (DataRow row in districttable.Rows)
                {
                    dal.AddDistrictData(row);
                }

                foreach (DataRow row in businesstable.Rows)
                {
                    dal.AddBusinessData(row);
                }
                Console.WriteLine("ok");
                Console.Read();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }
}
