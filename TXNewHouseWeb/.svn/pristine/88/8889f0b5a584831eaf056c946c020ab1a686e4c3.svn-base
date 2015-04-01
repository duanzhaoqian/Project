<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新房后台-登录
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="bg_login">
            <div class="login_title">开发商登录</div>
            <form id="frmTest" action="/" method="post">
                <table width="300" border="0" cellpadding="0" cellspacing="0" class="login_table">
                    <tr>
                        <th scope="row">登录名：</th>
                        <td><input id="txtLoginName" type="text" placeholder="登录名/手机号" class="input_w200" maxlength="20"/></td>
                    </tr>
                    <tr>
                        <th scope="row">密码：</th>
                        <td><input id="txtPwd" type="password" class="input_w200" maxlength="20"/></td>
                    </tr>
                    <tr>
                        <th scope="row">&nbsp;</th>
                        <td><input id="btnLogin" type="button" value="登 录" class="btn_w97_green" /><a href="<%=TXCommons.GetConfig.BaseUrl%>user/forgetpassword1" class="font12 ml20 ie_valign_5">找回密码</a></td>
                    </tr>
                    <tr>
                        <th scope="row">&nbsp;</th>
                        <td><span id="errShow" style="display:none;" class="no"></span></td>
                    </tr>
                </table>
            </form>
            <div class="border_top tac">
                <p class="font12 col666"><span class="ie_valign_5">还没有快有家账号？</span><input type="button" value="注 册" class="btn_w98_green ml15" onclick="javascript:window.location='<%=TXCommons.GetConfig.BaseUrl%>user/register'" /></p>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtPwd").bind("keyup", function (event) {
                if (event.keyCode == 13) {
                    submitForm();
                }
            });
            $("#btnLogin").click(function () {
                submitForm();
            });
            //提交表单
            function submitForm() {
                var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
                var backUrl = '<%=ViewData["BackUrl"]%>';
                var login = $("#txtLoginName");
                var pwd = $("#txtPwd");
                var err = $("#errShow");
                var errMsg = "";
                err.hide();
                if ($.trim(login.val()) == "") {
                    login.focus();
                    errMsg = "登录名不能为空！";
                } else if ($.trim(pwd.val()) == "") {
                    pwd.focus();
                    errMsg = "密码不能为空！";
                }
                if (errMsg.length > 0) {
                    err.removeClass();
                    err.addClass("no");
                    err.text(errMsg);
                    err.fadeIn();
                    return;
                } else {
                    err.removeClass();
                    err.addClass("ts");
                    err.html("登录中...<img src='<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif' height='14px' width='14px' />");
                    err.fadeIn();
                    $.post(mainUrl + "user/ajaxdologin", { loginName: login.val(), pwd: pwd.val(), m: Math.random() }, function (data) {
                        var result = parseInt(data);
                        if (result > 0) {
                            err.removeClass();
                            err.addClass("ok");
                            err.text("登录成功，跳转中...");
                            if (backUrl == "") {
                                window.location = mainUrl + "home/index";
                            }
                            else {
                                window.location = backUrl;
                            }
                        }
                        else {
                            switch (result) {
                                case -1: err.text("用户名或密码错误！"); break;
                                case -2: err.text("您的帐号已锁定，请联系我平台客服！"); break;
                                case -3: err.text("您的帐号已删除，请联系我平台客服！"); break;
                                default: err.text("登陆错误，请稍后重试！"); break;
                            }
                        }
                    });
                }
            }
        });
    </script>
</asp:Content>
