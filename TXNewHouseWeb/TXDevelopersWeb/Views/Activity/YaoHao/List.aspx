<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXModel.NHouseActivies.YaoHao.CT_YaoHao>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-摇号活动列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                摇号活动列表</div>
            <div class="font14 fr mr35">
                <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/YaoHaoAdd">创建摇号活动</a></div>
        </div>
        <div id="divAjaxData" class="mt15 clearFix" style="position: relative;">
            <%=Html.Partial("YaoHao/YaoHaoTable", Model)%>
        </div>
    </div>
</asp:Content>
