using System;

namespace TXModel.AdminPVM
{
    /// <summary>
    /// 开发商实体
    /// </summary>
    public class PVM_NH_Developer
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公司类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 省ID
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 城市Id
        /// </summary>
        public int CityId { get; set; }

        public int DistrictId { set; get; }

        /// <summary>
        /// 认证状态 0未认证 1已通过，2未通过
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 锁定状态 0正常 1锁定
        /// </summary>
        public int LockState { get; set; }
        /// <summary>
        /// 是否推荐 0未推荐 1 推荐
        /// </summary>
        public bool IsRecommend { get; set; }

        /// <summary>
        /// 省名
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        public string DistrictName { set; get; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 备用电话
        /// </summary>
        public string SpareTelephone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登陆名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public string InnerCode { get; set; }

        /// <summary>
        /// 本次登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime OldLoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 认证状态 0未认证 1已通过，2未通过
        /// </summary>
        public string StateStr { get; set; }

        /// <summary>
        /// 公司类型 0开发商
        /// </summary>
        public string TypeStr { get; set; }

        /// <summary>
        ///  锁定状态 0正常 1锁定
        /// </summary>
        public string LockStateStr { get; set; }

        /// <summary>
        /// 拒绝理由
        /// </summary>
        public string Refuse { get; set; }

        public N_Developer NHDeveloper { set; get; }

        public N_DeveloperIdentity NHDeveloperIdentity { set; get; }

        public class N_Developer
        {
            public int Id { set; get; }

            public int ProvinceId { set; get; }

            public string ProvinceName { set; get; }

            public int CityId { set; get; }

            public string CityName { set; get; }

            public string Mobile { set; get; }

            public string Email { set; get; }

            public string Telephone { set; get; }

            public string SpareTelephone { set; get; }

            public string LoginName { set; get; }

            public string Pwd { set; get; }

            public string RealName { set; get; }

            public int State { set; get; }

            public int LockState { set; get; }

            public string InnerCode { set; get; }

            public DateTime LoginTime { set; get; }

            public DateTime OldLoginTime { set; get; }

            public DateTime CreateTime { set; get; }

            public bool IsDel { set; get; }
        }

        public class N_DeveloperIdentity
        {
            public int Id { set; get; }

            public int DeveloperId { set; get; }

            public string UserName { set; get; }

            public string UserMobile { set; get; }

            public string UserEmail { set; get; }

            public int Type { set; get; }

            public int ProvinceId { set; get; }

            public string ProvinceName { set; get; }

            public int CityId { set; get; }

            public string CityName { set; get; }

            public int DId { set; get; }

            public string DName { set; get; }

            public string CompanyName { set; get; }

            public string CompanyAddress { set; get; }

            public string Refuse { set; get; }

            public DateTime CreateTime { set; get; }

            public bool IsDel { set; get; }
        }
    }
}