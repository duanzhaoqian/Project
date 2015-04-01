//验证正则表达式
//Time:2014-2-26
//Author:tyh
var validateRegExp = {
    decmal: "^([+-]?)\\d*\\.\\d+$", //浮点数
    decmal1: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*$", //正浮点数
    decmal2: "^-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*)$", //负浮点数
    decmal3: "^-?([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0)$", //浮点数
    decmal4: "^[1-9]\\d*.\\d*|0.\\d*[1-9]\\d*|0?.0+|0$", //非负浮点数（正浮点数 + 0）
    decmal5: "^(-([1-9]\\d*.\\d*|0.\\d*[1-9]\\d*))|0?.0+|0$", //非正浮点数（负浮点数 + 0）
    intege: "^-?[1-9]\\d*$", //整数
    intege1: "^[1-9]\\d*$", //正整数
    intege2: "^-[1-9]\\d*$", //负整数
    num: "^([+-]?)\\d*\\.?\\d+$", //数字
    num1: "^[1-9]\\d*|0$", //正数（正整数 + 0）
    num2: "^-[1-9]\\d*|0$", //负数（负整数 + 0）
    ascii: "^[\\x00-\\xFF]+$", //仅ACSII字符
    chinese: "^[\\u4e00-\\u9fa5]+$", //仅中文
    color: "^[a-fA-F0-9]{6}$", //颜色
    date: "^\\d{4}(\\-|\\/|\.)\\d{1,2}\\1\\d{1,2}$", //日期
    email: "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$", //邮件
    idcard: "^[1-9]([0-9]{14}|[0-9]{17})$", //身份证
    ip4: "^(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)\\.(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)$", //ip地址
    letter: "^[A-Za-z]+$", //字母
    letter_l: "^[a-z]+$", //小写字母
    letter_u: "^[A-Z]+$", //大写字母
    mobile: "^0?(13|15|18|14)[0-9]{9}$", //手机
    notempty: "^\\S+$", //非空
    password: "^.*[A-Za-z0-9\\w_-]+.*$", //密码
    fullNumber: "^[0-9]+$", //数字
    picture: "(.*)\\.(jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$", //图片
    qq: "^[1-9]*[1-9][0-9]*$", //QQ号码
    rar: "(.*)\\.(rar|zip|7zip|tgz)$", //压缩文件
    tel: "^[0-9\-()（）]{7,18}$", //电话号码的函数(包括验证国内区号,国际区号,分机号)
    url: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&=]*)?$", //url
    username: "^[A-Za-z0-9_\\-\\u4e00-\\u9fa5]+$", //户名
    deptname: "^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$", //单位名
    zipcode: "^\\d{6}$", //邮编
    realname: "^[A-Za-z\\u4e00-\\u9fa5]+$", // 真实姓名
    companyname: "^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$",
    companyaddr: "^[A-Za-z0-9_()（）\\#\\-\\u4e00-\\u9fa5]+$",
    companysite: "^http[s]?:\\/\\/([\\w-]+\\.)+[\\w-]+([\\w-./?%&#=]*)?$"
};
//重写jQuery.validate默认验证提示
jQuery.extend(jQuery.validator.messages, {
    required: "<span class=\"ie_valign_5 no\">此项必填</span>",
    remote: "<span class=\"ie_valign_5 no\">此项错误，请重新输入</span>",
    email: "<span class=\"ie_valign_5 no\">请输入正确格式的电子邮件</span>",
    url: "<span class=\"ie_valign_5 no\">请输入合法的网址</span>",
    date: "<span class=\"ie_valign_5 no\">请输入合法的日期</span>",
    dateISO: "<span class=\"ie_valign_5 no\">请输入合法的日期(ISO)</span>",
    number: "<span class=\"ie_valign_5 no\">请输入合法的数字</span>",
    digits: "<span class=\"ie_valign_5 no\">只能输入整数</span>",
    creditcard: "<span class=\"ie_valign_5 no\">请输入合法的信用卡号</span>",
    equalTo: "<span class=\"ie_valign_5 no\">请再次输入相同的文字</span>",
    accept: "<span class=\"ie_valign_5 no\">请输入拥有合法后缀名的文字</span>",
    maxlength: jQuery.validator.format("<span class=\"ie_valign_5 no\">请输入一个长度最多是{0}的文字</span>"),
    minlength: jQuery.validator.format("<span class=\"ie_valign_5 no\">请输入一个长度最少是{0}的文字</span>"),
    rangelength: jQuery.validator.format("<span class=\"ie_valign_5 no\">请输入一个长度介于{0}和{1}之间的文字</span>"),
    range: jQuery.validator.format("<span class=\"ie_valign_5 no\">请输入一个介于{0}和{1}之间的值</span>"),
    max: jQuery.validator.format("<span class=\"ie_valign_5 no\">请输入一个最大为{0}的值</span>"),
    min: jQuery.validator.format("<span class=\"ie_valign_5 no\">请输入一个最小为{0}的值</span>")
});
//jQuery.validate自定义验证
//验证电话或手机
jQuery.validator.addMethod("mobileortel", function (value) {
    if (value == "") {
        return true;
    } else {
        var regM = new RegExp(validateRegExp.mobile);
        var regT = new RegExp(validateRegExp.tel);
        if (regM.test(value) || regT.test(value))
            return true;
        else
            return false;
    }
}, "请输入正确的电话或手机格式");
//验证电话号码格式
jQuery.validator.addMethod("telephone", function (value) {
    if (value == "") {
        return true;
    } else {
        var reg = new RegExp(validateRegExp.tel);
        return reg.test(value);
    }
}, "请输入正确的电话格式");
//验证手机号码格式
jQuery.validator.addMethod("mobile", function (value) {
    if (value == "") {
        return true;
    } else {
        var reg = new RegExp(validateRegExp.mobile);
        return reg.test(value);
    }
}, "请输入正确的手机格式");
//验证用户名规则
jQuery.validator.addMethod("username", function (value) {
    if (value == "") {
        return true;
    } else {
        var reg = new RegExp(validateRegExp.username);
        return reg.test(value);
    }
}, "只能由中文、英文、数字及'-'、'_'组成");
//验证真实姓名规则
jQuery.validator.addMethod("realname", function (value) {
    if (value == "") {
        return true;
    } else {
        var reg = new RegExp(validateRegExp.realname);
        return reg.test(value);
    }
}, "只能由全中文或全英文组成");
//验证密码规则
jQuery.validator.addMethod("password", function (value) {
    if (value == "") {
        return true;
    } else {
        var reg = new RegExp(validateRegExp.password);
        return reg.test(value);
    }
}, "只能由英文、数字及符号组成");
//验证名称或地址规则
jQuery.validator.addMethod("nameaddress", function (value) {
    if (value == "") {
        return true;
    } else {
        var reg = new RegExp(validateRegExp.companyaddr);
        return reg.test(value);
    }
}, "只能由中文、英文、数字及'_'、'-'、()、（）、#组成");