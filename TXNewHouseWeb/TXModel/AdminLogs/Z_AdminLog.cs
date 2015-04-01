
namespace TXModel.AdminLogs
{
    public class Z_AdminLog
    {
        /// <summary>
        /// 操作管理员名称
        /// </summary>
        public string OperAdminName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string OperTime { get; set; }

        /// <summary>
        /// 操作项目
        /// </summary>
        public string OperDes { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string OperIP { get; set; }
    }
}