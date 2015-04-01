<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新房后台-修改密码
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
      <div class="reg_box">
        <h4 class="title_h4 mb10"><span>修改密码</span><em class="ts_right"><i class="red">*</i>为必填项</em></h4>
          <form id="frmUpdatePwd" action="<%=TXCommons.GetConfig.BaseUrl%>home/doupdatepassword" method="post">
            <div class="table_box">
              <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tdpd">
                <tr>
                  <td align="right"><span class="orange">*</span> 旧密码：</td>
                  <td><input id="txtPwd" name="txtPwd" type="password" class="input_reg" maxlength="20"/></td>
                </tr>
                <tr>
                  <td align="right"><span class="orange">*</span> 新密码：</td>
                  <td><input id="txtPwdNew" name="txtPwdNew" type="password" class="input_reg" maxlength="20"/></td>
                </tr>
                <tr>
                  <td width="150" align="right"><span class="orange">*</span>重复新密码：</td>
                  <td><input id="txtPwdCopy" name="txtPwdCopy" type="password" class="input_reg" maxlength="20"/></td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                  <td>
                    <input type="submit" value="确定修改" class="btn_w98_green mt15 mb15" />
                    <input id="btnReset" type="button" value="重置" class="btn_w98_green mt15 mb15 ml20" />
                  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                  <td>
                    <span id="err" class="ie_valign_5 ts" style="display:none;"></span>
                  </td>
                </tr>
              </table>
            </div>
          </form>
      </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            var validator = $("#frmUpdatePwd").validate({
                errorClass: "ie_valign_5 no",       //设置错误提示css
                errorElement: "span",               //设置错误提示容器
                //验证通过事件
                submitHandler: function(form) {
                    var err = $("#err");
                    err.removeClass();
                    err.addClass("ts");
                    err.html("修改中...<img src='<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif' height='14px' width='14px' />");
                    err.show();
                    var oPwd = $("#txtPwd").val();
                    var nPwd = $("#txtPwdNew").val();
                    $.post(mainUrl + "home/doupdatepassword", { oldPwd: oPwd, newPwd: nPwd, m: Math.random() }, function(data) {
                        if (data > 0) {
                            err.removeClass();
                            err.addClass("ok");
                            $("#err").text("修改成功，请重新登陆");
                            window.location = mainUrl + "user/logout";
                        } else {
                            $("#err").text("旧密码错误，请重新输入");
                            $("#txtPwd").focus();
                        }
                    });
                },
                rules: {
                    txtPwd: {
                        required: true
                    },
                    txtPwdNew: {
                        required: true,
                        rangelength: [6, 16],
                        password: true
                    },
                    txtPwdCopy: {
                        required: true,
                        equalTo: "#txtPwdNew"
                    }
                },
                messages: {
                    txtPwd: {
                        required: "请输入旧密码"
                    },
                    txtPwdNew: {
                        required: "请输入新密码",
                        rangelength: jQuery.format("限{0}-{1}个字符"),
                        password: "只能由英文、数字及符号组成，字母区分大小写"
                    },
                    txtPwdCopy: {
                        required: "请再次输入密码",
                        equalTo: "两次输入密码不一致"
                    }
                }
            });
            //重置表单
            $("#btnReset").click(function () {
                $("#txtPwd").val("");
                $("#txtPwdNew").val("");
                $("#txtPwdCopy").val("");
                validator.resetForm(); //清除验证信息
            });
        })
    </script>
</asp:Content>
