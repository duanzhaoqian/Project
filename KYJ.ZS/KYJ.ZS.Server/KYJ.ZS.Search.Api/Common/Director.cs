using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Search.Api.Common
{
    /// <summary>
    /// 作者：wangyu
    /// 时间：2014/6/23 9:20:23
    /// 描述：
    /// </summary>
    public class Director
    {
        public object Exec(Builder builder)
        {
            if (builder == null)
                return null;
            try
            {
                return builder.GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                builder.Close();
            }

        }
    }
}
