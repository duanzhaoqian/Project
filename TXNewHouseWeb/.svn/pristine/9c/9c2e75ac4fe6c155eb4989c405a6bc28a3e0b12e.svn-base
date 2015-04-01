<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AlipayOrderSuc
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        function delayURL(url) {
            var delay = document.getElementById("time").innerHTML;
            //最后的innerHTML不能丢，否则delay为一个对象  
            if (delay > 0) {
                delay--;
                document.getElementById("time").innerHTML = delay;
            } else {
                window.top.location.href = url;
            }
            setTimeout("delayURL('" + url + "')", 1000);
            //此处1000毫秒即每一秒跳转一次  
        }
        $(document).ready(function () {
            delayURL("<%=TXCommons.GetConfig.BaseUrl%>Account/Index?second=62");  
        });
    </script>
    <div class="p_wrap clearFix">
        <div class="payWin">
            <p class="payP">
                支付成功！</p>
            <p class="font12">
                <a class="blue" href="<%=TXCommons.GetConfig.BaseUrl%>Account/Index?second=62"><span id="time">3</span> 秒后自动跳转...点击直接跳转</a></p>
        </div>
    </div>
</asp:Content>

