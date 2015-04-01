using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Bll;

namespace Module
{
    public class SyncDataModule
    {
        public void SyncSQLData()
        {
            //获取LongHouseBase数据
            Source_S_LongHouseService sourceSLongHouseService = new Source_S_LongHouseService();
            string strTimeHour = "02:00:00";
            DateTime beginTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyy-MM-dd " + strTimeHour));
            DateTime endTime = Convert.ToDateTime(DateTime.Now.ToString("yyy-MM-dd " + strTimeHour));
            List<Model.S_LongHouseCache> list = sourceSLongHouseService.GetList<Model.S_LongHouseCache>(beginTime, endTime);

            //删除s_LongHouse数据并添加
            List<int> listDeleteIds = list.Select(c => c.Id).ToList();
            S_LongHouseService sLongHouseService = new S_LongHouseService();
            sLongHouseService.RelDeleteData(listDeleteIds);
            sLongHouseService.InsertData(Command.DataConvert.ListToDataTable(list));
        }
    }
}
