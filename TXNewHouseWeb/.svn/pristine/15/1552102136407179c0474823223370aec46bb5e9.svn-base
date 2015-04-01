<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    忘记密码
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="p_wrap">
            <p class="font14 tar mt20 mb20 col666">
                <span class="orange mr5 font20 ver5">*</span>为必填项</p>
            <div class="passCt">
                <p class="mb30">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/wj03.png" /></p>
                <p class="mb20 lh25 clearFix">
                    <span class="fl">已发送验证码到您的手机，没收到?</span>
                    <input id="btnSendVerify" type="button" class="btn_yzm fl ml20" />
                    <span id="errSendCode" style="display: none" class="no fl ml20">已发送</span></p>
                <form id="frmForgetPwd" action="/" method="post">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="passTAB">
                    <tr>
                        <td width="20%" align="right">
                            <span class="colff6600 mr5 font20 ver5">*</span>验证码：
                        </td>
                        <td width="80%" align="left">
                            <input id="txtVerifyCode" name="txtVerifyCode" type="text" class="input" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="colff6600 mr5 font20 ver5">*</span>新密码：
                        </td>
                        <td align="left">
                            <input id="txtPwd" name="txtPwd" type="password" class="input" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="colff6600 mr5 font20 ver5">*</span>重复新密码：
                        </td>
                        <td align="left">
                            <input id="txtPwdCopy" name="txtPwdCopy" type="password" class="input" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td align="left">
                            <input type="submit" value="" class="btn_tj cursor mt10" />&nbsp;<span id="errSubmit"
                                style="display: none;" class="ts">修改中...</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            <div class="passWxts">
                                <p class="orange">
                                    温馨提示：</p>
                                <p>
                                    没有收到验证码？请尝试点击“免费获取验证码”按键重新获取。</p>
                                <p>
                                    同一登录名1天最多获取3次验证码，每条验证码30分钟后失效。</p>
                            </div>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <p class="shadowHR mb20">
            </p>
        </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //网站域名
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            var mobile = '<%=ViewData["Mobile"]%>';
            //发送验证码
            $("#btnSendVerify").click(function () {
                $("#btnSendVerify").attr("disabled", true);
                var err = $("#errSendCode");
                err.removeClass();
                err.text("");
                err.show();
                var reg = new RegExp(validateRegExp.mobile);
                if (reg.test(mobile)) {
                    var domain = "<%=TXCommons.GetConfig.Domain%>";
                    var baseKey = "<%=TXCommons.KeyList.CookieForgetPwdSendMsgKey%>";
                    var numKey = baseKey + mobile;
                    $.post(mainUrl + "user/AjaxCheckRePwdValidateCount", { mobile: mobile, t: 1, m: Math.random() }, function (data) {
                        if (data) {
                            err.removeClass();
                            err.addClass("ts fl ml20");
                            err.text("发送中...");
                            $.post(mainUrl + "user/ajaxsendvalidate", { mobile: mobile, t: 1, m: Math.random() }, function (data) {
                                if (data) {
                                    var countdown = setInterval(CountDown, 1000);
                                    err.removeClass();
                                    err.addClass("ok fl ml20");
                                    err.text("已发送");
                                } else {
                                    err.removeClass();
                                    err.addClass("no fl ml20");
                                    err.text("发送失败");
                                }
                            });
                        } else {
                            $("#btnSendVerify").removeAttr("disabled");
                            err.addClass("no fl ml20");
                            err.text("您今日的短信发送数量已到达上限");
                            err.show();
                            return;
                        }
                    });
                }
            });
            var count = 180;
            function CountDown() {
                $("#btnSendVerify").attr("disabled", true);
                $("#btnSendVerify").removeClass(); //btn_yzm_none
                $("#btnSendVerify").addClass("btn_yzm_none fl ml20");
                $("#btnSendVerify").val(count + " 秒");
                if (count == 0) {
                    $("#btnSendVerify").removeAttr("disabled").removeClass().addClass("btn_yzm fl ml20");
                    clearInterval(countdown);
                }
                count--;
            }
            //验证方法
            $("#frmForgetPwd").validate({
                errorClass: "no",       //设置错误提示css
                errorElement: "span",               //设置错误提示容器
                //验证通过事件
                submitHandler: function (form) {
                    var err = $("#errSubmit");
                    err.removeClass();
                    err.addClass("ts");
                    err.text("修改中...");
                    err.show();
                    $.post(mainUrl + "user/doforgetpassword", { code: $("#txtVerifyCode").val(), mobile: mobile, newPwd: $("#txtPwd").val(), m: Math.random() }, function (data) {
                        if (data) {
                            err.removeClass();
                            err.addClass("ok");
                            err.text("修改成功，请用新密码登陆");
                            window.location = mainUrl + "user/login";
                        } else {
                            err.removeClass();
                            err.addClass("no");
                            err.text("修改失败，验证码错误");
                        }
                    });
                },
                //设置验证规则
                rules: {
                    txtVerifyCode: {
                        required: true,
                        remote: {
                            url: mainUrl + "user/ajaxcheckvalidate",
                            type: "post",
                            dataType: "json",
                            data: {
                                mobile: function () {
                                    return mobile;
                                },
                                code: function () {
                                    return $("#txtVerifyCode").val();
                                }
                            }
                        }
                    },
                    txtPwd: {
                        required: true,
                        rangelength: [6, 16],
                        password: true
                    },
                    txtPwdCopy: {
                        required: true,
                        equalTo: "#txtPwd"
                    }
                },
                //设置提示信息
                messages: {
                    txtVerifyCode: {
                        required: "请输入验证码",
                        remote: "请输入正确的验证码"
                    },
                    txtPwd: {
                        required: "请输入密码",
                        rangelength: jQuery.format("限{0}-{1}个字符"),
                        password: "只能由英文、数字及符号组成，字母区分大小写"
                    },
                    txtPwdCopy: {
                        required: "请再次输入密码",
                        equalTo: "两次输入密码不一致"
                    }
                }
            });
        });
    </script>
</asp:Content>
