<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TXOrm.Developer>" %>

<form id="frmUpdateMobile" action="/" method="post">
    <div class="layer_box w500">
        <a href="javascript:void(0);" class="close" opertype="dialog_close"></a>
        <div class="layer_content">
            <div class="pt15 pl10 pb10 pr10">
               <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab_3">
                <tr>
                  <th width="33%">您当前手机号：</th>
                  <td width="67%"><%=Model.Mobile%></td>
                </tr>
                <tr>
                  <th><span class="red mr5">*</span>新手机号：</th>
                  <td><input id="txtMobileNew" name="txtMobileNew" type="text" class="input_wauto w150" maxlength="20"/></td>
                </tr>
                <tr>
                  <th><span class="red mr5">*</span>输入登录密码：</th>
                  <td><input id="txtPwd" name="txtPwd" type="password" class="input_wauto w150" maxlength="20"/></td>
                </tr>
                <tr>
                  <th><span class="red mr5"></span></th>
                  <td><input id="btnSendVerify" type="button" class="btn_yel_w70" value="获取验证码" />&nbsp;<span id="errSendCode" class="ie_valign_5 ts" style="display:none;"></span></td>
                </tr>
                <tr>
                  <th><span class="red mr5">*</span>验证码：</th>
                  <td><input id="txtVerifyCode" name="txtVerifyCode" type="text" class="input_wauto w150" maxlength="10"/></td>
                </tr>
                <tr>
                  <th>&nbsp;</th>
                  <td><span id="errUpdateMobile" class="ie_valign_5 ts" style="display:none;"></span></td>
                </tr>
              </table>
            </div>
            <div class="layer_btn"><input type="submit" class="btn_yel_w70 mr20" value="提交修改" /><input type="button" class="btn_grey_w70" value="取消" opertype="dialog_close" /></div>
        </div>
    </div>
</form>
<script type="text/javascript">
    $(document).ready(function () {
        //网站域名
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
        //发送验证码
        $("#btnSendVerify").click(function () {
            var col = $("#errSendCode");
            $(col).show();
            var mobile = $("#txtMobileNew").val();
            var reg = /^[1][358]\d{9}$/;
            if (reg.test(mobile)) {
                $(col).removeClass();
                $(col).addClass("ie_valign_5 ts");
                $(col).text("发送中...");
                $.post(mainUrl + "user/ajaxsendvalidate", { mobile: mobile, m: Math.random() }, function (data) {
                    if (data) {
                        $(col).removeClass();
                        $(col).addClass("ie_valign_5 ok");
                        $(col).text("已发送");
                    }
                    else {
                        $(col).removeClass();
                        $(col).addClass("ie_valign_5 no");
                        $(col).text("发送失败");
                    }
                });
            }
            else {
                $(col).removeClass();
                $(col).addClass("ie_valign_5 ts");
                $(col).text("手机号填写有误");
            }
        });
        //验证方法
        $("#frmUpdateMobile").validate({
            errorClass: "ie_valign_5 no",       //设置错误提示css
            errorElement: "span",               //设置错误提示容器

            //验证通过事件
            submitHandler: function (form) {
                var code = $("#txtVerifyCode").val();
                var pwd = $("#txtPwd").val();
                var mobile = $("#txtMobileNew").val();
                var err = $("#errUpdateMobile");
                err.show();
                $(err).removeClass();
                $(err).addClass("ie_valign_5 ts");
                $(err).text("修改中...");
                $.post(mainUrl + "home/doupdatemobile", { code: code, pwd: pwd, mobile: mobile, m: Math.random() }, function (data) {
                    var result = parseInt(data);
                    if (result > 0) {
                        $("#showMobile").text(mobile);
                        Dialog.closeDialog();//关闭弹出层
                    }
                    else {
                        $(err).removeClass();
                        $(err).addClass("ie_valign_5 no");
                        $(err).text("密码错误");
                    }
                });
            },
            //设置验证规则
            rules: {
                txtMobileNew: {
                    required: true,
                    mobile: true,
                    remote: {
                        url: mainUrl + "user/ajaxcheckmobile",
                        type: "post",
                        dataType: "json",
                        data: {
                            mobile: function () {
                                return $("#txtMobileNew").val();
                            }
                        }
                    }
                },
                txtVerifyCode: {
                    required: true,
                    remote: {
                        url: mainUrl + "user/ajaxcheckvalidate",
                        type: "post",
                        dataType: "json",
                        data: {
                            mobile: function () {
                                return $("#txtMobileNew").val();
                            },
                            code: function () {
                                return $("#txtVerifyCode").val();
                            }
                        }
                    }
                },
                txtPwd: {
                    required: true
                }
            },
            //设置提示信息
            messages: {
                txtMobileNew: {
                    required: "请输入手机号码",
                    mobile: "请输入正确的手机格式",
                    remote: "对不起，该手机号已被注册"
                },
                txtVerifyCode: {
                    required: "请输入验证码",
                    remote: "请输入正确的验证码"
                },
                txtPwd: {
                    required: "请输入登录密码"
                }
            }
        });
    });
</script>