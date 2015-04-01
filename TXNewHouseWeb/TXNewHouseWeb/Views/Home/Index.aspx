<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    快有家新房平台
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearfix">
        <div class="new pos_r">
            <div class="w1190">
                <div class="joinus">
                    <a href="<%=TXCommons.GetConfig.DevelopersUrl%>user/register">立即申请入驻</a></div>
                <div class="flash">
                    <embed src="<%=TXCommons.GetConfig.ImgUrl%>flash/flash_1190_607.swf" quality="high"
                        width="1190" height="607" wmode="transparent" type='application/x-shockwave-flash'></embed></div>
            </div>
        </div>
    </div>
</asp:Content>
