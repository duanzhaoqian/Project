using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ConsoleApplication1.kyj_HouseDB_5_4DataSetTableAdapters;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            kyj_HouseDB_5_4DataSetTableAdapters.LNHouseTableAdapter lnHouseTableAdapter = new LNHouseTableAdapter();
            kyj_HouseDB_5_4DataSet.LNHouseDataTable dataTable = lnHouseTableAdapter.GetData();
            Console.WriteLine(lnHouseTableAdapter.Connection.ConnectionString+Environment.NewLine);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(kyj_HouseDB_5_4DataSet.LNHouseDataTable));
           
            using (MemoryStream memory=new MemoryStream())
            {
                XmlWriter xmlWriter=new XmlTextWriter(memory,Encoding.UTF8);
               xmlSerializer.Serialize(xmlWriter,dataTable);
                xmlWriter.Flush();
                xmlWriter.Close();
                string str = Encoding.UTF8.GetString(memory.ToArray());
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }
    }
}
