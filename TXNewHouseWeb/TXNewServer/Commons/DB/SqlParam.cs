/*----------------------------------------------------------------
// Copyright (C) 2010 ʢ�ش�ý 
// �ļ�����SqlParam
// �ļ��������������ݲ���
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Commons.DB
{
    /// <summary>
    /// ����������
    /// </summary>
    public class SqlParam
    {
        private List<SqlParameter> _paramters = new List<SqlParameter>();
        /// <summary>
        /// ���һ������
        /// ���ǵ���ѯ���ܸ÷���ֻ������ֵ�Ͳ�����
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="val"></param>
        /// <param name="type"></param>
        public void AddParam(string pname,object val) 
        {
            _paramters.Add(new SqlParameter(pname, val));
        }
        /// <summary>
        /// ���һ������
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
        /// ���ز�������
        /// </summary>
        public List<SqlParameter> Paramters
        {
            get
            {
                return _paramters;
            }
        }
        /// <summary>
        /// �����Լ�����������
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
