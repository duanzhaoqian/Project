using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FilterIPAddress.Dal
{
    class FilterIPAddress_Dal
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FilterIPAddressConnection"].ConnectionString;
        internal List<string> GetAllIPAddress()
        {
            List<string> list=new List<string>();
            string sql = "select IPAddress from AllowIPAddress";
            DataSet dataSet = SqlHelper.ExecuteDataset(connectionString, CommandType.Text, sql);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                list.Add(row["IPAddress"].ToString().Trim());
            }
            return list.Where(c => !string.IsNullOrEmpty(c)).Distinct().ToList();
        }

        internal int Insert(string IPAddress)
        {
            string sql = "insert AllowIPAddress(IPAddress) values (@IPAddress);";
            SqlParameter sqlParameter=new SqlParameter("@IPAddress",IPAddress);
            return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, sqlParameter);
        }

        internal int Delete(string IPAddress)
        {
            string sql = "delete from AllowIPAddress where IPAddress=@IPAddress;";
            SqlParameter sqlParameter = new SqlParameter("@IPAddress", IPAddress);
            return SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, sqlParameter);
        }
    }
}
