<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div class="layer_box w300">
    <%TXOrm.PremisStatisticsInfo info = ViewData["Info"] as TXOrm.PremisStatisticsInfo;
      string SubscribedCount = "0";//认购数
      string BargainCount = "0";//成交数
      var PremisesId = ViewData["premisesId"];
      if (info != null)
      {
          SubscribedCount = info.SubscribedCount.ToString();
          BargainCount = info.BargainCount.ToString();
      }
    %>
    <a href="javascript:void(0)" class="close" opertype="dialog_close"></a>
    <div class="layer_title" opertype="dialog_title">
        今日成交信息</div>
    <div class="layer_content">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
            <tr>
                <td align="right">
                    认购套数：
                </td>
                <td>
                    <input type="text" class="input_wauto w180" id="Subscribed" value="<%=SubscribedCount=="0"?"0":SubscribedCount %>" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    成交套数：
                </td>
                <td>
                    <input type="text" class="input_wauto w180" id="Bargain" value="<%=BargainCount=="0"?"0":BargainCount %>" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <input type="button" value="确定" class="btn_w97_green" onclick="addBargain()" />
                </td>
            </tr>
        </table>
    </div>
</div>
<script type="text/javascript">    
    function addBargain() {
        var subscribed = $("#Subscribed").val(); //认购套数
        var bargain = $("#Bargain").val(); //成交套数
        var premisesId='<%=PremisesId %>';
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";
        var reg = /^\d+$/;
        if (subscribed=="") {
            alert("认购套数不能为空");
            return;
        } 
        if (!reg.test(subscribed)) {
            alert("认购套数格式不正确");
            return;
        }
        if (bargain=="") {
            alert("成交套数不能为空");
            return;
        }
        if (!reg.test(bargain)) {
            alert("成交套数格式不正确");
            return;
        }
            $.ajax({
                url: mainUrl + "Premises/SubBargainInfo",
                type: "post",
                cache: false,
                data: { PremisesId: premisesId, SubscribedCount: subscribed, BargainCount: bargain },
                success: function (data) {
                    //alert(data);
                    Dialog.closeDialog();
                }
            });
    }
</script>
