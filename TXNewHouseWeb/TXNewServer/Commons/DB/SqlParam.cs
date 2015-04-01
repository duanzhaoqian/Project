/*----------------------------------------------------------------
// Copyright (C) 2010 盛拓传媒 
// 文件名：SqlParam
// 文件功能描述：数据参数
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Commons.DB
{
    /// <summary>
    /// 参数集合类
    /// </summary>
    public class SqlParam
    {
        private List<SqlParameter> _paramters = new List<SqlParameter>();
        /// <summary>
        /// 添加一个参数
        /// 考虑到查询性能该方法只用于数值型参数。
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="val"></param>
        /// <param name="type"></param>
        public void AddParam(string pname,object val) 
        {
            _paramters.Add(new SqlParameter(pname, val));
        }
        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="val"></param>
        /// <param name="type"></param>
        /// <param name="size"></param>
        public void AddParam(string pname, object val, SqlDbType type, int size) 
        {
            
            SqlParameter p = new SqlParameter(pname,type,size);
            p.Value = val;
            _paramters.Add(p);
        }
        /// <summary>
        /// 返回参数集合
        /// </summary>
        public List<SqlParameter> Paramters
        {
            get
            {
                return _paramters;
            }
        }
        /// <summary>
        /// 复制自己完整副本。
        /// </summary>
        /// <returns></returns>
        public SqlParam Copy() 
        {
            SqlParam sp = new SqlParam();
            foreach (SqlParameter p in _paramters)
                sp.AddParam(p.ParameterName, p.Value);
            return sp;
        }
    }
}
