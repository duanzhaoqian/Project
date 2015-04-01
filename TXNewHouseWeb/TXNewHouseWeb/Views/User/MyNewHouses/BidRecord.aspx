<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/User.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXOrm.BidPrice>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    我的新房-我的出价记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearFix">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="p_wrap ddxiangqing">
            <div class="bidtitle">
                <h6>
                    <span class="tex">我的出价记录</span></h6>
            </div>
            <div class="dd_part3 clearFix" style="margin-top: 15px">
                <div class="thistitle">
                    <strong class="colff8200"><%=ViewData["Title"] %></strong>
                    <!--<div class="thisext">
					<a href="#" class="linkA">返回 &gt;&gt;</a>
				</div>-->
                </div>
                <div id="divAjaxData" class="dataTAB bgS1 clearFix">
                     <%=Html.Partial("MyNewHouses/BidRecordTable", Model)%>
                </div>
                <div class="thisShadow">
                </div>
            </div> 
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
