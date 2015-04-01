<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="xf_layer_box w430 mb10">
    <a href="" class="close"></a>
    <div class="layer_title">
        支付保证金</div>
    <div class="layer_box">
        <p class="tac font14 mb20">
            本次应支付保证金：<strong class="yellow" id="bond"><%=ViewData["Bond"].ToString().Split('.')[0]%>元</strong></p>
        <p class="tac mb20">
            <input type="button" value="支付保证金" class="btn_w80_gray" onclick="zhifu()" />
            <input type="button" value="取消" onclick="cancel()" class="btn_w80_gray ml10" /></p>
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
<input type="hidden" id="ActivitiesId" value="<%=ViewData["ActivitieId"] %>" />
<input type="hidden" id="HouseId" value="<%=ViewData["HouseId"] %>" />
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/dialog_new.js")%>" type="text/javascript"></script>
<script type="text/javascript">
    //支付
    function zhifu() {
        $.get("<%=TXCommons.GetConfig.BaseUrl%>user/CheckMoneyAndpayfor", { bond: $("#bond").html(), aid: $("#ActivitiesId").val(), hid: $("#HouseId").val() }, function (data) {
            if (data == 1) {
                alert("支付成功！");
                window.location.reload();
            }
            else {
                window.location = "<%=TXCommons.GetConfig.BaseUrl%>Premises/UpdateOrder";
            }
        })
    }
    //取消
    function cancel() {
        $(".xf_layer_box").hide();
    }
</script>
