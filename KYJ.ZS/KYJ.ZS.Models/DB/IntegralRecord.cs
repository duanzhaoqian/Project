using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// Author:baiyan
    /// Time:2014-4-17
    /// Desc:积分记录表实体类
    /// </summary>
    public class IntegralRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 本地用户ID
        /// </summary>
        public int User_Id { get; set; }
        /// <summary>
        /// 积分数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 积分状态 0未知 1赚取 2消费
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 积分来源 0未知 1签到
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// 赚取/消费积分时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
