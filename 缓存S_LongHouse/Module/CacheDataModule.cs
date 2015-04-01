using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Bll;
using ListenRedis;


namespace Module
{
    public class CacheDataModule
    {

        public void ProcessMQData<T>(string str) where T : new()
        {
            List<int> listDeleteIds = new List<int>();
            DataTable dataTable = new DataTable();
            //接收到MQ消息
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            Dictionary<int, string> dictionary = javaScriptSerializer.Deserialize<Dictionary<int, string>>(str);
            //数据类型转换
            List<string> listReidsKeys = dictionary.Select(c => c.Value).ToList();
            //请求Redis,封装数据类型
            GetDataFromRedis getDataFromRedis = new GetDataFromRedis();
            foreach (string key in listReidsKeys)
            {
                DataRow t = getDataFromRedis.Get<DataRow>(key);
                if (t != null)
                {
                    dataTable.Rows.Add(t);
                }
                else
                {
                    listDeleteIds.AddRange(dictionary.Where(c => c.Value == key).Select(c => c.Key));
                }
            }

            //s_LongHouse_Cache插入数据
            S_LongHouseCacheService sLongHouseCacheService = new S_LongHouseCacheService();
            sLongHouseCacheService.InsertData(dataTable);
            //获取临时表数据
            S_LongHouse_TempService sLongHouseTempService = new S_LongHouse_TempService();
            DataTable dataTableTemp = sLongHouseTempService.SelectData();

            List<Model.S_LongHouseTemp> listTemp =
                Command.DataConvert.DataTableToList<Model.S_LongHouseTemp>(dataTableTemp);
            //提取SId为0的数据（要插入的数据）
            List<Model.S_LongHouseTemp> listPreInsertSLongHouse = listTemp.Where(c => c.SId == 0).ToList();
            //插入数据
            S_LongHouseService sLongHouseService = new S_LongHouseService();
            DataTable dataTablePreInsertSLongHouse = Command.DataConvert.ListToDataTable(listPreInsertSLongHouse);
            dataTablePreInsertSLongHouse.Columns.Remove("SId");//移除SId列
            sLongHouseService.InsertData(dataTablePreInsertSLongHouse);//插入数据

            //提取SId不为0的数据（更新数据）
            List<Model.S_LongHouseTemp> listPreUpdateSLongHouse = listTemp.Where(c => c.SId != 0).ToList();
            DataTable dataTablePreUpdateSLongHouse = Command.DataConvert.ListToDataTable(listPreUpdateSLongHouse);
            dataTablePreUpdateSLongHouse.Columns.Remove("SId");//移除SId列
            sLongHouseService.UpdateData(dataTablePreUpdateSLongHouse);//更新数据

            //删除数据
            sLongHouseService.LogicallyDeleteData(listDeleteIds);
        }
    }
}
