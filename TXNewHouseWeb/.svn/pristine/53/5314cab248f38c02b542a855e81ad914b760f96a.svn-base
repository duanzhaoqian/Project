<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    在线支付
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        //线上支付
        //http://beijing.cjkjb.com/Landlord/ShowPayMents
        window.location.href = '<%=TXCommons.GetConfig.BaseUrl %>' + 'Landlord/ShowPayMents' + '<%=ViewData["url"] %>';
    </script>
</asp:Content>
