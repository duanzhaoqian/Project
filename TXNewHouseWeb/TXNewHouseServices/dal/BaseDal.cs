using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace TXNewHouseServices.dal
{
    public class BaseDal
    {
        protected string ConnStr_WebDB
        {
            get { return ConfigurationManager.ConnectionStrings["kyj_WebDBEntities"].ConnectionString; }
        }

        protected string ConnStr_UserDB
        {
            get { return ConfigurationManager.ConnectionStrings["kyj_UserDBEntities"].ConnectionString; }
        }

        protected string ConnStr_NewHouseDB
        {
            get { return ConfigurationManager.ConnectionStrings["kyj_NewHouseDBEntities"].ConnectionString; }
        }

        /// <summary>
        /// 将DataTable转化为实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        protected IList<T> GetEntities<T>(DataTable table) where T : new()
        {
            IList<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (table.Columns.Contains(item.Name))
                    {
                        item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

        protected T GetEntity<T>(DataTable table) where T : new()
        {
            T entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }
                    }
                }
            }

            return entity;
        }
    }
}