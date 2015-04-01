using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Command;
using Dal;

namespace Bll
{
    public class S_LongHouseService
    {
        private S_LongHouse_Dal sLongHouseDal = new S_LongHouse_Dal();
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="dataTable"></param>
        public void UpdateData(DataTable dataTable)
        {
            dataTable.TableName = Config.S_LongHouse_TableName;
            sLongHouseDal.UpdateData(dataTable);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="dataTable"></param>
        public void InsertData(DataTable dataTable)
        {
            dataTable.TableName = Config.S_LongHouse_TableName;
            sLongHouseDal.InsertData(dataTable);
        }
        /// <summary>
        /// 真实删除数据
        /// </summary>
        /// <param name="listDeleteIds"></param>
        public void RelDeleteData(List<int> listDeleteIds)
        {
            sLongHouseDal.RelDeleteData(listDeleteIds);
        }
        /// <summary>
        /// 逻辑删除数据，实际更新数据
        /// </summary>
        /// <param name="listDeleteIds"></param>
        public void LogicallyDeleteData(List<int> listDeleteIds)
        {
            sLongHouseDal.LogcallyDeleteData(listDeleteIds);
        }
    }
}
