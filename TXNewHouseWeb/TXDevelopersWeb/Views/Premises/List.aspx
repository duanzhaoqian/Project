<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXOrm.Premis>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    楼盘管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <!--显示分页数据-->
        <div id="divAjaxData">
            <%=Html.Partial("PremisesTable", Model)%>
        </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/dialog_new.js")%>" type="text/javascript"></script>
</asp:Content>
