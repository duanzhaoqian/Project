using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Models.DB
{
    /// <summary>
    /// 作者：maq
    /// 时间：2014年4月24日
    /// 描述：商户用户实体
    /// </summary>
    public partial class Merchant
    {
        private int _id;
        /// <summary>
        /// PK
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _loginName;
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        private string _email;
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _mobile;
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        private string _pwd;
        /// <summary>
        /// 商户登陆密码
        /// </summary>
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        private string _payPwd;
        /// <summary>
        /// 支付密码
        /// </summary>
        public string PayPwd
        {
            get { return _payPwd; }
            set { _payPwd = value; }
        }

        private string _name;
        /// <summary>
        /// 商户名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _realName;
        /// <summary>
        /// 商户法人
        /// </summary>
        public string RealName
        {
            get { return _realName; }
            set { _realName = value; }
        }

        private string _card;
        /// <summary>
        /// 商户证书编号
        /// </summary>
        public string Card
        {
            get { return _card; }
            set { _card = value; }
        }

        private int _state;
        /// <summary>
        /// 商户状态
        /// </summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _rank;
        /// <summary>
        /// 商户等级
        /// </summary>
        public int Rank
        {
            get { return _rank; }
            set { _rank = value; }
        }

        private int _status;
        /// <summary>
        /// 商户处理状态
        /// </summary>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _guid;
        /// <summary>
        /// guid唯一标识
        /// </summary>
        public string Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        private DateTime _createTime;
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        private DateTime _updateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        private DateTime _lastLoginTime;
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLoginTime
        {
            get { return _lastLoginTime; }
            set { _lastLoginTime = value; }
        }

        private Boolean _isDel;
        /// <summary>
        /// 是否删除
        /// </summary>
        public Boolean IsDel
        {
            get { return _isDel; }
            set { _isDel = value; }
        }

        private int _adminId;
        /// <summary>
        /// 管理员ID
        /// </summary>
        public int AdminId
        {
            get { return _adminId; }
            set { _adminId = value; }
        }

        private string _adminName;
        /// <summary>
        /// 管理员名称
        /// </summary>
        public string AdminName
        {
            get { return _adminName; }
            set { _adminName = value; }
        }
    }
    #region 枚举
    /// <summary>
    /// 商户状态
    /// </summary>
    public enum MerchantState
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 锁定
        /// </summary>
        Lock = 1
    }
    /// <summary>
    /// 商户等级
    /// </summary>
    public enum MerchantRank
    {
        /// <summary>
        /// 普通
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// 高级
        /// </summary>
        Senior = 1
    }
    /// <summary>
    /// 商户处理状态
    /// </summary>
    public enum MerchantStatus
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// 认证待审核
        /// </summary>
        Pending = 1,
        /// <summary>
        /// 认证通过
        /// </summary>
        Through = 2,
        /// <summary>
        /// 未通过
        /// </summary>
        NotThrough = 3
    }
    #endregion
}
