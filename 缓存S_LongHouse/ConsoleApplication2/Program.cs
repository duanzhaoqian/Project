using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bll;
using GetMQMessage;
using Module;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 数据库Test
            //            S_LongHouseCacheService sLongHouseCacheService = new S_LongHouseCacheService();
            //            S_LongHouseService sLongHouseService = new S_LongHouseService();
            //            S_LongHouse_TempService sLongHouseTempService = new S_LongHouse_TempService();
            //            Source_S_LongHouseService sourceSLongHouseService = new Source_S_LongHouseService();
            //            List<Model.S_LongHouseCache> list = sourceSLongHouseService.GetList<Model.S_LongHouseCache>(DateTime.Now.AddYears(-5), DateTime.Now);
            //            DataTable dataTableSource = sourceSLongHouseService.GetDataTable(DateTime.Now.AddYears(-5), DateTime.Now);
            //            //sLongHouseCacheService.InsertData(Command.DataConvert.ListToDataTable(list.Take(10).ToList()));

            //            DataTable dataTableTemp = sLongHouseTempService.SelectData();
            //            List<Model.S_LongHouseTemp> listTemp = Command.DataConvert.DataTableToList<Model.S_LongHouseTemp>
            //(dataTableTemp);

            //            //提取SId为0的数据（要插入的数据）
            //            List<Model.S_LongHouseTemp> listPreInsertSLongHouse = listTemp.Where(c => c.SId == 0).ToList();
            //            //插入数据
            //            DataTable dataTablePreInsertSLongHouse = Command.DataConvert.ListToDataTable(listPreInsertSLongHouse);
            //            dataTablePreInsertSLongHouse.Columns.Remove("SId");//移除SId列
            //            sLongHouseService.InsertData(dataTablePreInsertSLongHouse);//插入数据

            //            //提取SId不为0的数据（更新数据）
            //            List<Model.S_LongHouseTemp> listPreUpdateSLongHouse = listTemp.Where(c => c.SId != 0).ToList();
            //            DataTable dataTablePreUpdateSLongHouse = Command.DataConvert.ListToDataTable(listPreUpdateSLongHouse);
            //            dataTablePreUpdateSLongHouse.Columns.Remove("SId");//移除SId列
            //            sLongHouseService.UpdateData(dataTablePreUpdateSLongHouse);//更新数据

            //Module.SyncDataModule syncDataModule=new SyncDataModule();
            //syncDataModule.SyncSQLData();

            #endregion

            #region MQTest
            Process process = new Process();
            process.ProcessData();
            #endregion
            Console.ReadKey();
        }
    }
}
