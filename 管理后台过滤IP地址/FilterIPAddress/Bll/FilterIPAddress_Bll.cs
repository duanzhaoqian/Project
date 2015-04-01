using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FilterIPAddress.Dal;

namespace FilterIPAddress.Bll
{
    public class FilterIPAddress_Bll
    {
        FilterIPAddress_Dal filterIpAddressDal = new FilterIPAddress_Dal();
        internal bool CheckIPAddress(string ipAddress)
        {
            List<string> list = filterIpAddressDal.GetAllIPAddress();
            return list.Contains(ipAddress);
        }

        public int InsertIPAddress(string IPAddress)
        {
            return filterIpAddressDal.Insert(IPAddress);
        }

        public int DeleteIPAddress(string IPAddress)
        {
            return filterIpAddressDal.Delete(IPAddress);
        }
    }
}
