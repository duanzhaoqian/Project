
namespace TXDal
{
    /// <summary>
    /// 网站管理平台专用基础类，非本组成员慎重继承
    /// </summary>
    public class BaseDal_Admin
    {
        #region SQL Result

        /// <summary>
        /// SQL执行返回首行首列(整型)
        /// Author: 李雨钊
        /// </summary>
        protected class ESqlResult_ScalarInt
        {
            public int? Result { set; get; }
        }

        /// <summary>
        /// SQL执行返回首行首列(字符串)
        /// Author: 李雨钊
        /// </summary>
        protected class ESqlResult_ScalarString
        {
            public string Result { set; get; }
        }

        #endregion
    }
}
