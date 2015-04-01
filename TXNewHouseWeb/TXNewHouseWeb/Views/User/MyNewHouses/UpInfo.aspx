<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div class="xf_layer_box w450 mb10" id="div_info">
    <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid"
        class="close"></a>
    <div class="layer_title">
        秒杀成功</div>
    <div class="layer_box">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab_k">
            <tr>
                <td colspan="2">
                    <div class="red">
                        请完善或者核对您的个人信息；并在20分钟之内支付保证金，否则购房资格取消。</div>
                </td>
            </tr>
            <tr>
                <th width="24%">
                    <span class="red mr3">*</span>手机号：
                </th>
                <td>
                    <input type="text" id="mobile" class="inptxt1 w150" value="<%=ViewData["Mobile"] %>" />
                    <span style="color: Red" id="error1"></span>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="red mr3">*</span>真实姓名：
                </th>
                <td>
                    <input type="text" id="rname" class="inptxt1 w150" value="<%=ViewData["RName"] %>" />
                    <span style="color: Red" id="error2"></span>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="red mr3">*</span>身份证号：
                </th>
                <td>
                    <input type="text" id="idcard" class="inptxt1 w150" value="<%=ViewData["IDCard"] %>" />
                    <span style="color: Red" id="error3"></span>
                </td>
            </tr>
            <tr>
                <th>
                    QQ号：
                </th>
                <td>
                    <input type="text" id="qq" class="inptxt1 w150" value="<%=ViewData["QQ"] %>" />
                </td>
            </tr>
            <tr>
                <th>
                    邮箱：
                </th>
                <td>
                    <input type="text" id="email" class="inptxt1 w150" value="<%=ViewData["Email"] %>" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="button" value="确认无误，提交" class="btn_w120_gray" onclick="tijiao()" />
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="xf_layer_box w450 mb10" style="display: none;" id="div_zhifu">
    <a href="" class="close"></a>
    <div class="layer_title">
        支付保证金</div>
    <div class="layer_box">
        <p class="tac font14 mb20">
            本次应支付保证金：<strong class="yellow">
                <%=ViewData["Bond"].ToString().Split('.')[0]%>元</strong></p>
        <p class="tac mb20">
            <input type="button" value="支付保证金" class="btn_w80_gray" onclick="zhifu()" /><input
                type="button" value="取消" opertype="dialog_close" class="btn_w80_gray ml10" /></p>
        <div class="line mb15">
        </div>
        <div class="line_h24">
            <p class="col333">
                温馨提示：</p>
            <p class="col666">
                1.为了确保交易的正常秩序，保护竞购参与者的权益，以及线下正签的顺 利进行。需要支付保证金。</p>
            <p class="col666">
                2.竞购成功后，您随时都可以登录用户中心查看您购得的房屋。您与开发商双方约定时间签署《意向成交确认书》。保证金冲抵相应额度首付款。</p>
            <p class="col666">
                3.竞购不成功，在竞购结束后7天之后自动返还至您的账户中。</p>
        </div>
    </div>
</div>
<div class="xf_layer_box w450" id="suc" style="display: none;">
    <a href="" class="close"></a>
    <div class="layer_title">
        竞购成功</div>
    <div class="layer_box">
        <p class="tal mb20">
            <strong class="col333 font14">恭喜您获得【<%=ViewData["House"] %>】购房资格！</strong></p>
        <p class="tal font12 mb20">
            1-2个工作日内，开发商会打电话与你联系进行线下交易。</p>
        <p class="tac mb20 pt20">
            <input type="button" value="关闭" class="btn_w80_gray" onclick="window.location.reload();" /></p>
    </div>
</div>
<input type="hidden" id="bond" value="<%=ViewData["Bond"] %>" />
<input type="hidden" id="orderid" value="<%=ViewData["OrderId"] %>" />
<link href="<%=TXCommons.GetConfig.GetFileUrl("css/newhouse.css")%>" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        $(":input").focus(function () {
            $("#error1").html("");
            $("#error2").html("");
            $("#error3").html("");
        })
    })
    //提交用户信息
    function tijiao() {
        var bond = $("#bond").val();
        var orderid = $("#orderid").val();
        var mobile = $("#mobile").val();
        var rname = $("#rname").val();
        var idcard = $("#idcard").val();
        var qq = $("#qq").val();
        var email = $("#email").val();
        var phoneyz = new RegExp(/^1[3|5|8][0-9]\d{4,8}$/); //手机验证
        var idcard15 = new RegExp(/^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/); //15位的身份证
        var idcard18 = new RegExp(/^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/); //18位的身份证
        if ($.trim(mobile).length == 0) {
            $("#error1").html("请填写手机号");
            return false;
        }
        if (!phoneyz.test(mobile)) {
            $("#error1").html("手机号格式错误");
            return false;
        }
        if ($.trim(rname).length == 0) {
            $("#error2").html("请填写真实姓名");
            return false;
        }
        if ($.trim(idcard).length == 0) {
            $("#error3").html("请填写身份证号");
            return false;
        }
        if (!idcard15.test($.trim(idcard)) && !idcard18.test($.trim(idcard))) {
            $("#error3").html("身份证号格式错误");
            return false;
        }
        $.get("<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/UpdateUserInfo", { mobile: mobile, rname: rname, idcard: idcard, qq: qq, email: email, obj: Math.random() }, function (data) {
            if (data == "1") {
                $("#div_info").hide();
                $("#div_zhifu").show();
            }
            else {
                alert("提交失败！");
                window.location.reload();
            }
        })
    }

    //支付
    function zhifu() {
        $.get("<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/CheckMoneyAndpayforMs", { bond: $("#bond").val(), orderid: $("#orderid").val() }, function (data) {
            if (data == "1") {
                $("#div_zhifu").hide();
                $("#suc").show();
            }
            else if (data == "2") {
                alert("支付失败！");
                return;
            }
            else {
                window.location.href = "<%=TXCommons.GetConfig.GetSiteRoot%>/PayOrder/OnlinePayDeposit?type=2&id=" + $("#orderid").val();
                //window.location = "<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/Premises/UpdateOrder?orderid=0";
            }
        })
    }
</script>
