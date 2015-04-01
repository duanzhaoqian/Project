using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorClient.AppUtility
{
    //class CmdType
    //{
    //}

    #region KPS服务指令
    public class KOMCmd
    {
        public const short Succeed = 0;
        /// <summary>
        /// 密码验证
        /// </summary>
        public const short skServer2 = 5801;
        public const short skServer2_backup = 5802;
        /// <summary>
        /// 操作服务器
        /// </summary>
        public const short OperateServer = 5805;
        public const short OperateServerbackup = 5806;
        public const short CheckPwd = 5807;
        public const short CheckPwd_back = 5808;
        public const short AddServertype = 5809;
        public const short delServertype = 5810;
        public const short updateServertype = 5811;
        public const short skServertype = 5812;
        public const short skServertype2 = 5827;
        /// <summary>
        /// 添加设备
        /// </summary>
        public const short AddServer = 5813;
        public const short DELServer = 5814;
        public const short UpdateServer = 5815;
        public const short skServer = 5816;
        /// <summary>
        /// 添加用户
        /// </summary>
        public const short AddUser = 5817;

        public const short delUser = 5819;
        public const short updateUser = 5820;
        public const short skUser = 5821;
        /// <summary>
        /// 更新密码
        /// </summary>
        public const short UpdatePwd = 5822;

    
       
       
       


    }
    #endregion
}
