<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXOrm.DeveloperMessage>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	新房后台-站内信
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="search_agent mt10 clearFix">
            <input id="txtKey" type="text" value="<%=ViewData["Keyword"]%>" class="input_h30 w300 fl mr10" maxlength="50" />
            <input id="btnSearch" type="button" value="" class="bnt_serch fl" />
        </div>
        <div id="divAjaxData" class="landlord_box mt10">
            <%=Html.Partial("MessageTable", Model)%>
        </div>
    </div>
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftAjax.js")%>" type="text/javascript"></script>
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSearch").click(function () {
                $.post("<%=TXCommons.GetConfig.BaseUrl%>home/message?key=" + $("#txtKey").val() + "&m=" + Math.random(), function (data) {
                    $("#divAjaxData").html(data);
                });
            });
        });
    </script>
</asp:Content>
