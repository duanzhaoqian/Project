<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    忘记密码
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="clearFix">
            <div class="p_wrap">
                <p class="font14 tar mt20 mb20 col666">
                    <span class="orange mr5 font20">*</span>为必填项</p>
                <div class="passCt">
                    <p class="mb20">
                        <img src="<%=TXCommons.GetConfig.ImgUrl%>images/wj02.png" /></p>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="passTAB">
                        <tr>
                            <td width="20%" align="right">
                                <span class="colff6600 mr5 font20">*</span>手机号码：
                            </td>
                            <td width="38%">
                                <input id="txtMobile" type="text" class="input col999" placeholder="请输入手机号码" maxlength="20" />
                            </td>
                            <td width="42%">
                                <span id="err" style="display: none;" class="no">手机号不能为空</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                            </td>
                            <td>
                                <input id="btnNext" type="button" class="btn_xyb mt20" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <p class="shadowHR mb20">
                </p>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnNext").click(function () {
                var mobile = $("#txtMobile").val();
                var err = $("#err");
                if ($.trim(mobile) == "") {
                    err.text("手机号不能为空");
                    err.show();
                    return;
                }
                var reg = /^0?(13|15|18|14)[0-9]{9}$/;
                if (!reg.test(mobile)) {
                    err.text("请输入正确的手机号码");
                    err.show();
                    return;
                }
                var domain = "<%=TXCommons.GetConfig.Domain%>";
                var baseKey = "<%=TXCommons.KeyList.CookieForgetPwdSendMsgKey%>";
                var numKey = baseKey + mobile;
                $.post("<%=TXCommons.GetConfig.BaseUrl%>user/AjaxCheckRePwdValidateCount", { mobile: mobile, t: 1, m: Math.random() }, function (data) {
                    if (data) {
                        err.hide();
                        $.post("<%=TXCommons.GetConfig.BaseUrl%>user/ajaxcheckmobile", { mobile: mobile, m: Math.random() }, function (data) {
                            if (!data) {
                                err.removeClass();
                                err.addClass("ts");
                                err.text("发送中...");
                                err.show();
                                $.post("<%=TXCommons.GetConfig.BaseUrl%>user/ajaxsendvalidate", { mobile: mobile, t: 1, m: Math.random() }, function (data1) {
                                    if (data1) {
                                        var num = 1;
                                        if ($.cookie(numKey) != undefined) {
                                            num = num + parseInt($.cookie(numKey));
                                        }
                                        $.cookie(numKey, num, { expires: 1, path: '/', domain: domain });
                                        //发送验证码后跳转页面
                                        window.location = "<%=TXCommons.GetConfig.BaseUrl%>user/forgetpassword2?k=" + mobile;
                                    } else {
                                        err.removeClass();
                                        err.addClass("no");
                                        err.text("发送失败");
                                    }
                                });
                            } else {
                                err.text("您输入的手机号码不存在");
                                err.show();
                            }
                        });
                    } else {
                        err.text("您今日的短信发送数量已到达上限");
                        err.show();
                        return;
                    }
                });
            });
        });
    </script>
</asp:Content>
