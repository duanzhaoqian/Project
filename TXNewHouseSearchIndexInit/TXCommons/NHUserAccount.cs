using TXCommons.user.cjkjb.webservice;

namespace TXCommons
{
    /// <summary>
    /// 新房前台用户账户相关操作
    /// </summary>
    public class NHUserAccount
    {
        private OperaUserService ouService = new OperaUserService();

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="xmlUser"></param>
        /// <returns></returns>
        public bool UpdateUser(string xmlUser)
        {
            return ouService.UpdateUser("binpath", "classpath", xmlUser);
        }

        /// <summary>
        /// 获得用户账户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserAccountInfo GetUserAccountInfo(int userID)
        {
            return ouService.GetUserAccountInfo("binpath", "classpath", userID);
        }

        /// <summary>
        /// 支付出价保证金
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="price">支付金额</param>
        /// <returns></returns>
        public bool RecordAccountForBid(int userID, decimal price, string msg)
        {
            return ouService.RecordAccount("binpath", "classpath", userID, price, 0, 6, msg, 0, string.Empty);
        }


        /// <summary>
        /// 账户流水记录
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="price">金额</param>
        /// <param name="msg">描述</param>
        /// <param name="atype">记录类型（0出账1进账）</param>
        /// <param name="longtype">日志类型（2、账户充值 6、保证金）</param>
        /// <returns></returns>
        public bool AddUserAccountInfo(int userID, decimal price, string msg, int atype, int longtype)
        {
            return ouService.AddUserAccountInfo("binpath", "classpath", userID, System.Convert.ToByte(atype), 0, longtype, price, msg, string.Empty);
        }
    }
}
