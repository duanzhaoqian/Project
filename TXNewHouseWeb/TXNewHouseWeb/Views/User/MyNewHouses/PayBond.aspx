<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div class="xf_layer_box w450 mb10">
    <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid"
        class="close"></a>
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
<input type="hidden" id="bond" value="<%=ViewData["Bond"] %>" />
<input type="hidden" id="orderid" value="<%=ViewData["OrderID"] %>" />
<link href="<%=TXCommons.GetConfig.GetFileUrl("css/newhouse.css")%>" rel="stylesheet"
    type="text/css" />
<script type="text/javascript">
    //支付
    function zhifu() {
        $.get("<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/CheckMoneyAndpayfor", { bond: $("#bond").val(), orderid: $("#orderid").val() }, function (data) {
            if (data == 1) {
                alert("订单费用已从账户中扣除，订单处理成功！");
                window.location.reload();
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
