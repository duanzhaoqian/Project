using System;

namespace TXModel.Dev
{
    /// <summary>
    /// 开发商及身份认证信息
    /// </summary>
    [Serializable]
    public class CT_DeveAndIdenInfo
    {
        #region Developers
        /// <summary>
        /// Id
        /// </summary>		
        private int _id;
        public int Id
        {
            get { return _id_iden; }
            set { _id_iden = value; }
        }
        /// <summary>
        /// ProvinceID
        /// </summary>		
        private int _provinceid;
        public int ProvinceID
        {
            get { return _provinceid_iden; }
            set { _provinceid_iden = value; }
        }
        /// <summary>
        /// ProvinceName
        /// </summary>		
        private string _provincename;
        public string ProvinceName
        {
            get { return _provincename_iden; }
            set { _provincename_iden = value; }
        }
        /// <summary>
        /// CityId
        /// </summary>		
        private int _cityid;
        public int CityId
        {
            get { return _cityid_iden; }
            set { _cityid_iden = value; }
        }
        /// <summary>
        /// CityName
        /// </summary>		
        private string _cityname;
        public string CityName
        {
            get { return _cityname_iden; }
            set { _cityname_iden = value; }
        }
        /// <summary>
        /// Mobile
        /// </summary>		
        private string _mobile;
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        /// <summary>
        /// Email
        /// </summary>		
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        /// <summary>
        /// Telephone
        /// </summary>		
        private string _telephone;
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; }
        }
        /// <summary>
        /// SpareTelephone
        /// </summary>		
        private string _sparetelephone;
        public string SpareTelephone
        {
            get { return _sparetelephone; }
            set { _sparetelephone = value; }
        }
        /// <summary>
        /// LoginName
        /// </summary>		
        private string _loginname;
        public string LoginName
        {
            get { return _loginname; }
            set { _loginname = value; }
        }
        /// <summary>
        /// Pwd
        /// </summary>		
        private string _pwd;
        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        /// <summary>
        /// RealName
        /// </summary>		
        private string _realname;
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }
        /// <summary>
        /// State
        /// </summary>		
        private int _state;
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }
        /// <summary>
        /// LockState
        /// </summary>		
        private int _lockstate;
        public int LockState
        {
            get { return _lockstate; }
            set { _lockstate = value; }
        }
        /// <summary>
        /// InnerCode
        /// </summary>		
        private string _innercode;
        public string InnerCode
        {
            get { return _innercode; }
            set { _innercode = value; }
        }
        /// <summary>
        /// LoginTime
        /// </summary>		
        private DateTime _logintime;
        public DateTime LoginTime
        {
            get { return _logintime; }
            set { _logintime = value; }
        }
        /// <summary>
        /// OldLoginTime
        /// </summary>		
        private DateTime _oldlogintime;
        public DateTime OldLoginTime
        {
            get { return _oldlogintime; }
            set { _oldlogintime = value; }
        }
        /// <summary>
        /// CreateTime
        /// </summary>		
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// IsDel
        /// </summary>		
        private bool _isdel;
        public bool IsDel
        {
            get { return _isdel; }
            set { _isdel = value; }
        }
        #endregion

        #region Developers_Identity（含与数据库不同名字段）
        /// <summary>
        /// Id（与数据库字段不同名）
        /// </summary>		
        private int _id_iden;
        public int Id_Iden
        {
            get { return _id_iden; }
            set { _id_iden = value; }
        }
        /// <summary>
        /// DeveloperId
        /// </summary>		
        private int _developerid;
        public int DeveloperId
        {
            get { return _developerid; }
            set { _developerid = value; }
        }
        /// <summary>
        /// UserName
        /// </summary>		
        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }
        /// <summary>
        /// UserMobile
        /// </summary>		
        private string _usermobile;
        public string UserMobile
        {
            get { return _usermobile; }
            set { _usermobile = value; }
        }
        /// <summary>
        /// UserEmail
        /// </summary>		
        private string _useremail;
        public string UserEmail
        {
            get { return _useremail; }
            set { _useremail = value; }
        }
        /// <summary>
        /// Type
        /// </summary>		
        private int _type;
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// ProvinceId（与数据库字段不同名）
        /// </summary>		
        private int _provinceid_iden;
        public int ProvinceId_Iden
        {
            get { return _provinceid_iden; }
            set { _provinceid_iden = value; }
        }
        /// <summary>
        /// ProvinceName（与数据库字段不同名）
        /// </summary>		
        private string _provincename_iden;
        public string ProvinceName_Iden
        {
            get { return _provincename_iden; }
            set { _provincename_iden = value; }
        }
        /// <summary>
        /// CityId（与数据库字段不同名）
        /// </summary>		
        private int _cityid_iden;
        public int CityId_Iden
        {
            get { return _cityid_iden; }
            set { _cityid_iden = value; }
        }
        /// <summary>
        /// CityName（与数据库字段不同名）
        /// </summary>		
        private string _cityname_iden;
        public string CityName_Iden
        {
            get { return _cityname_iden; }
            set { _cityname_iden = value; }
        }
        /// <summary>
        /// DId
        /// </summary>		
        private int _did;
        public int DId
        {
            get { return _did; }
            set { _did = value; }
        }
        /// <summary>
        /// DName
        /// </summary>		
        private string _dname;
        public string DName
        {
            get { return _dname; }
            set { _dname = value; }
        }
        /// <summary>
        /// CompanyName
        /// </summary>		
        private string _companyname;
        public string CompanyName
        {
            get { return _companyname; }
            set { _companyname = value; }
        }
        /// <summary>
        /// CompanyAddress
        /// </summary>		
        private string _companyaddress;
        public string CompanyAddress
        {
            get { return _companyaddress; }
            set { _companyaddress = value; }
        }
        /// <summary>
        /// Refuse
        /// </summary>		
        private string _refuse;
        public string Refuse
        {
            get { return _refuse; }
            set { _refuse = value; }
        }
        /// <summary>
        /// CreateTime（与数据库字段不同名）
        /// </summary>		
        private DateTime _createtime_iden;
        public DateTime CreateTime_Iden
        {
            get { return _createtime_iden; }
            set { _createtime_iden = value; }
        }
        /// <summary>
        /// IsDel（与数据库字段不同名）
        /// </summary>		
        private bool _isdel_iden;
        public bool IsDel_Iden
        {
            get { return _isdel_iden; }
            set { _isdel_iden = value; }
        }    
        #endregion
    }
}
