<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Login.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-注册
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="reg_box">
            <h4 class="title_h4 mb10">
                <span>开发商注册</span><em class="ts_right"><i class="red">*</i>为必填项</em></h4>
            <div class="table_box">
                <form id="frmRegister" action="<%=TXCommons.GetConfig.BaseUrl%>user/doregister" method="post">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tdpd">
                    <tr>
                        <td align="right">
                            <span class="orange">*</span> 登录名：
                        </td>
                        <td>
                            <input id="txtLoginName" name="txtLoginName" type="text" class="input_reg" maxlength="20" />
                        </td>
                    </tr>
                    <%--<tr>
                    <td align="right"><span class="orange">*</span> 真实姓名：</td>
                    <td><input id="txtRealName" name="txtRealName" type="text" class="input_reg" maxlength="20"/></td>
                </tr>--%>
                    <tr>
                        <td width="150" align="right">
                            <span class="orange">*</span>手机号码：
                        </td>
                        <td>
                            <input id="txtMobile" name="txtMobile" type="text" class="input_reg" maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            <input id="btnSendVerify" type="button" value="免费获取验证码" class="btn_w111_gray" /><span
                                id="errSendCode" style="display: none" class="ie_valign_5 no"></span>
                        </td>
                    </tr>
                    <tr>
                        <td width="150" align="right">
                            <span class="orange">*</span> 验证码：
                        </td>
                        <td>
                            <input id="txtVerifyCode" name="txtVerifyCode" type="text" value="" class="input_reg"
                                maxlength="10" />
                        </td>
                    </tr>
                    <tr>
                        <td width="150" align="right">
                            <span class="orange">*</span> 密码：
                        </td>
                        <td>
                            <input id="txtPwd" name="txtPwd" type="password" value="" class="input_reg" maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td width="150" align="right">
                            <span class="orange">*</span> 重复密码：
                        </td>
                        <td>
                            <input id="txtPwdCopy" name="txtPwdCopy" type="password" value="" class="input_reg"
                                maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <p class="font12 mt10">
                                <input id="cbService" type="checkbox" value="" class="valign2" />
                                <label for="chk2">
                                    我已经看过并同意<a href="<%=TXCommons.GetConfig.GetSiteUrl%>about/webrreaty" class="col0068b7"
                                        target="_blank">《快有家服务条款》</a></label>
                                <span id="errService" style="display: none" class="ie_valign_5 no">请勾选快有家服务</span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <input type="submit" value="完成注册" class="btn_w98_green mt15 mb15" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <div class="yellow_box w550">
                                <p class="ts1">
                                    <strong>温馨提示：</strong>没有得到验证码？请尝试重新点击“免费获取验证码”按键获取，或拨打客服热线：400-999-3535。<br />
                                    同一手机号码1天最多获取3次验证码，每条验证码30分钟后失效。</p>
                            </div>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>"
        type="text/javascript"></script>
    <%--        <script type="text/javascript">
            $(function () {
                $('#btn').click(function () {
                    var count = 3;
                    var countdown = setInterval(CountDown, 1000);

                    function CountDown() {
                        $("#btn").attr("disabled", true);
                        $("#btn").val("Please wait " + count + " seconds!");
                        if (count == 0) {
                            $("#btn").val("Submit").removeAttr("disabled");
                            clearInterval(countdown);
                        }
                        count--;
                    }
                })

            }); 
</script> 
</head> 
<body> 
<input type="button" id="btn" value="Submit" /> 
</body> --%>
    <script type="text/javascript">
        $(document).ready(function () {
            //网站域名
            var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
            //发送验证码
            $("#btnSendVerify").click(function () {
                $("#btnSendVerify").attr("disabled", true);
                var col = $("#errSendCode");
                $(col).removeClass();
                $(col).text("");
                $(col).show();
                var mobile = $("#txtMobile").val();
                var reg = /^[1][358]\d{9}$/;
                if (reg.test(mobile)) {//验证手机号
                    var domain = "<%=TXCommons.GetConfig.Domain%>";
                    var baseKey = "<%=TXCommons.KeyList.CookieRegisterSendMsgKey%>";
                    var numKey = baseKey + mobile;
                    $.post(mainUrl + "user/AjaxCheckRePwdValidateCount", { mobile: mobile, m: Math.random() }, function (data) {
                        if (data) {//发送未达三次
                            $(col).removeClass();
                            $(col).addClass("ie_valign_5 ts");
                            $(col).text("发送中...");
                            $.post(mainUrl + "user/ajaxcheckandsendvalidate", { mobile: mobile, m: Math.random() }, function (data) {
                                if (data) {
                                    var countdown = setInterval(CountDown, 1000);
                                    $(col).removeClass();
                                    $(col).addClass("ie_valign_5 ok");
                                    $(col).text("已发送");
                                } else {
                                    $(col).removeClass();
                                    $(col).addClass("ie_valign_5 no");
                                    $(col).text("发送失败");
                                }
                            });
                        } else {//已达三次
                            $("#btnSendVerify").removeAttr("disabled");
                            $(col).removeClass();
                            $(col).addClass("ie_valign_5 no");
                            $(col).text("您今日的短信发送数量已到达上限");
                            $(col).show();
                            return;
                        }
                    });
                } else {
                    $("#btnSendVerify").removeAttr("disabled");
                    $(col).removeClass();
                    $(col).addClass("ie_valign_5 ts");
                    $(col).text("手机号填写有误");
                }
            });
            var count = 180;
            function CountDown() {
                $("#btnSendVerify").attr("disabled", true);
                $("#btnSendVerify").val(count + " 秒");
                if (count == 0) {
                    $("#btnSendVerify").val("免费获取验证码").removeAttr("disabled");
                    clearInterval(countdown);
                }
                count--;
            }
            //自定义验证
            //验证服务协议选项
            function checkService() {
                var check = $("#cbService").prop("checked");
                if (check) {
                    $("#errService").hide();
                } else {
                    $("#errService").show();
                }
                return check;
            }
            //验证方法
            $("#frmRegister").validate({
                errorClass: "ie_valign_5 no",       //设置错误提示css
                errorElement: "span",               //设置错误提示容器
                //success: "ie_valign_5 ok",        //设置成功提示css(成功后不能添加样式，否则，如果再次失败时，依然会显示成功的样式。)
                //验证通过事件
                submitHandler: function (form) {
                    var result = checkService();
                    if (result)
                        form.submit();
                },
                //设置验证规则
                rules: {
                    txtLoginName: {
                        required: true,
                        rangelength: [5, 20],
                        username: true,
                        remote: {
                            //异步验证必须post提交并且为json格式
                            url: mainUrl + "user/ajaxcheckloginname",   //后台处理程序    
                            type: "post",                               //数据发送方式     
                            dataType: "json",                           //接受数据格式   
                            data: {
                                //要传递的数据   
                                loginName: function () { //参数名
                                    return $("#txtLoginName").val(); //参数值
                                }
                            }
                        }
                    },
                    /*txtRealName: {
                    required: true,
                    realname: true
                    },*/
                    txtMobile: {
                        required: true,
                        mobile: true,
                        remote: {
                            url: mainUrl + "user/ajaxcheckmobile",
                            type: "post",
                            dataType: "json",
                            data: {
                                mobile: function () {
                                    return $("#txtMobile").val();
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
                                    return $("#txtMobile").val();
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
                    txtLoginName: {
                        required: "请输入登录名",
                        rangelength: jQuery.format("登录名长度介于{0}到{1}个字符之间"),
                        username: "只能由中文、英文、数字及'-'、'_'组成",
                        remote: "对不起，该登录名已被注册"
                    },
                    /*txtRealName: {
                    required: "请输入真实姓名",
                    realname: "只能由全中文或全英文组成"
                    },*/
                    txtMobile: {
                        required: "请输入手机号码",
                        mobile: "请输入正确的手机格式",
                        remote: "对不起，该手机号已被注册"
                    },
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
