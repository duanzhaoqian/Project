using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    public class Message
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 发送者ID（系统默认为0）
        /// </summary>
        public int SendId { get; set; }
        /// <summary>
        /// 接受者ID
        /// </summary>
        public int ReceiveId { get; set; }
        /// <summary>
        /// 发送类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 站内信标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 站内信内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        #endregion Model
    }
}
