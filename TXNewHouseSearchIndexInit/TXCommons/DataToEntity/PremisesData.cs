using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TXCommons.Index;
using TXCommons.MsgQueue;

namespace TXCommons.DataToEntity
{
    public class PremisesData
    {
        /// <summary>
        /// 通过索引获取新房列表
        /// 作者：sunlin 
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static List<T> PremisesListResult<T>(string conditions, string url, int cityId = 253) where T : new()
        {
            try
            {
                string premises_xml_url = url + "?condition=" + conditions;//查询生成xml文件
                DataSet ds = new DataSet("Results");
                ds.ReadXml(premises_xml_url);
                if (ds.Tables.Count < 2)
                {
                    return null;
                }
                else
                {
                    DataRow PageRow = ds.Tables[0].Rows[0];
                    DataTable dt = ds.Tables[1];
                    return TableToEntity.GetTableEntity<T>(dt);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        
    }
}
