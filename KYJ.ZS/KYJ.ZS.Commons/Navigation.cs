using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KYJ.ZS.Commons
{
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-04-28
    /// Desc:商户后台导航枚举
    /// </summary>
    public enum MerchantNavigation
    {
        /// <summary>
        /// 出租的订单
        /// </summary>
        CHUZUDEDINGDAN,
        /// <summary>
        /// 代售的订单
        /// </summary>
        DAISHOUDEDINGDAN,
        /// <summary>
        /// 评价管理
        /// </summary>
        PINGJIAGUANLI,
        /// <summary>
        ///  发货管理
        /// </summary>
        FAHUOGUANLI,
        /// <summary>
        /// 运费模板设置
        /// </summary>
        YUNFEIMOBANSHEZHI,
        /// <summary>
        /// 商品管理
        /// </summary>
        SHANGPINGUANLI,
        /// <summary>
        /// 发布商品
        /// </summary>
        FABUSHANGPIN,
        /// <summary>
        /// 租期模板设置
        /// </summary>
        ZUQIMOBANSHEZHI,
        /// <summary>
        /// 图片管理
        /// </summary>
        TUPIANGUANLI,
        /// <summary>
        /// 消费者保障服务
        /// </summary>
        XIAOFEIZHEBAOZHANGFUWU,
        /// <summary>
        /// 站内通知
        /// </summary>
        ZHANNEITONGZHI,
        /// <summary>
        /// 提现管理
        /// </summary>
        TIXIANGUANLI,
        /// <summary>
        /// 修改登录密码
        /// </summary>
        XIUGAIDENGLUMIMA
    }
    /// <summary>
    /// Author:zhuzh
    /// Date:2014-04-28
    /// Desc:用户后台导航枚举
    /// </summary>
    public enum UserNavigation
    {
        /// <summary>
        /// 发布信息
        /// </summary>
        FABUXINXI,
        /// <summary>
        /// 信息管理
        /// </summary>
        XINXIGUANLI,
        /// <summary>
        /// 租用中心
        /// </summary>
        ZUYONGZHONGXIN,
        /// <summary>
        ///  收藏夹
        /// </summary>
        SHOUCANGJIA,
        /// <summary>
        /// 基本资料
        /// </summary>
        JIBENZILIAO,
        /// <summary>
        /// 收货地址
        /// </summary>
        SHOUHUODIZHI,
        /// <summary>
        /// 个人认证
        /// </summary>
        GERENRENZHENG
    }
    /// <summary>
    /// 作者：孟国栋
    /// 时间：2014-05-26
    /// 描述：网站后台导航枚举
    /// </summary>
    public enum ManagerNavigation
    {
        /// <summary>
        /// 管理商品排序
        /// </summary>
        GUANLISHANGPINPAIXU = 1,
        /// <summary>
        /// 核审商品排序
        /// </summary>
        SHENHESHANGPINPAIXU = 4,
        /// <summary>
        /// 添加商品排序
        /// </summary>
        TIANJIASHANGPINPAIXU = 5,
        /// <summary>
        /// 管理广告
        /// </summary>
        GUANLIGUANGGAO = 6,
        /// <summary>
        /// 核审广告
        /// </summary>
        HESHENGUANGGAO = 7,
        /// <summary>
        /// 发布广告
        /// </summary>
        FABUGUANGGAO = 8,
        /// <summary>
        /// 管理商户
        /// </summary>
        GUANLISHANGHU = 9,
        /// <summary>
        /// 添加商户
        /// </summary>
        TIANJIASHANGHU = 10,
        /// <summary>
        /// 商品列表
        /// </summary>
        SHANGPINLIEBIAO = 11,
        /// <summary>
        /// 违规商品
        /// </summary>
        WEIGUISHANGPIN = 13,
        /// <summary>
        /// 管理订单
        /// </summary>
        GUANLIDINGDAN = 14,
        /// <summary>
        /// 管理商户提现
        /// </summary>
        GUANLISHANGHUTIXIAN = 15,
        /// <summary>
        /// 管理普通用户
        /// </summary>
        GUANLIPUTONGYONGHU = 16,
        /// <summary>
        /// 管理信息
        /// </summary>
        GUANLIXINXI = 17,
        /// <summary>
        /// 违规信息
        /// </summary>
        WEIGUIXINXI = 18,
        /// <summary>
        /// 管理职员权限
        /// </summary>
        GUANLIZHIYUANQUANXIAN = 19,
        /// <summary>
        /// 添加职员账号
        /// </summary>
        TIANJIAZHIYUANZHANGHAO = 20,
        /// <summary>
        ///  商品分类管理
        /// </summary>
        SHANGPINFENLEIGUANLI = 21,
        /// <summary>
        ///  属性规格管理
        /// </summary>
        SHUXINGGUIGEGUANLI = 22,
        /// <summary>
        /// 信息标签管理
        /// </summary>
        XINXIBIAOQIANGUANLI = 23,
        /// <summary>
        /// 管理权限角色
        /// </summary>
        GUANLIQUANXIANJUESE = 24,
        /// <summary>
        /// 日志查阅
        /// </summary>
        RIZHICHAYUE = 26,
        /// <summary>
        /// 认证审核
        /// </summary>
        RENZHENGSHENHE = 27
    }
    /// <summary>
    /// Auther:zhuzh
    /// Date  :2014-05-28
    /// Desc  :登录注册页导航枚举
    /// </summary>
    public enum CommonNavigation
    {
        /// <summary>
        /// 登录
        /// </summary>
        LOGIN,
        /// <summary>
        /// 注册
        /// </summary>
        REGISTER,
        /// <summary>
        /// 找回密码
        /// </summary>
        RETURNPWD
    }
}
