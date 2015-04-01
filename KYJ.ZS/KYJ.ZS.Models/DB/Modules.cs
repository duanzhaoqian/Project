using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-19
    /// 描述：管理模块实体类
    /// </summary>
   public class Modules
    {
       /// <summary>
       /// 模块实体类
       /// </summary>
        public int ModuleId { get; set; }
       /// <summary>
       /// 模块名称
       /// </summary>
        public string ModuleName { get; set; }
       /// <summary>
       /// 模块说明
       /// </summary>
        public string  ModuleRemark { get; set; }
       /// <summary>
       /// 模块权限
       /// </summary>
        public string Permission { get; set; }
    }
}
