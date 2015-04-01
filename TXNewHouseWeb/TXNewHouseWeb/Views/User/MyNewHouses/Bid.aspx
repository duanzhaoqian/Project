<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/User.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXModel.User.CT_Bid>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
    我的新房-我参与的竞购
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearFix">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="p_wrap">
            <div class="bidtitle mt20" style="padding-left: 0px;">
                <span class="fl mr30">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>user/images/wdxf_tit.gif" /></span>
                <h6 class="fl">
                    <a href="javascript:void(0)" class="cur">我参与的竞购</a> <%--<a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/yaohao">
                        我参与的网络摇号</a>--%> <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/collectionsPremise">我收藏的楼盘</a>
                </h6>
            </div>
            <div class="select_title">
                <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=0" class="bold col666">全部</a>
                <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=7" id="7">秒杀</a> 
                <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=5"id="5">竞价</a> 
                <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=8" id="8">一口价</a>
                <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=6" id="6">砍价</a><%-- <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=2"
                    id="2">限时折扣</a> <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=4" id="4">限时团购</a>
                <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/user/bid?type=3" id="3">排号购房</a>--%>
            </div>
            <div class="bidCont" id="divAjaxData">
                <%=Html.Partial("MyNewHouses/BidTable", Model)%>
            </div>
            <div class="shadowHR mb20">
            </div>
        </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/dialog_new.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var type = parseInt('<%=ViewData["type"] %>');
            if (type > 0) {
                $(".select_title").find("a").each(function() {
                    if ($(this).attr("id") == type) {
                        $(this).addClass("bg_ff6600");
                    }
                });
            }
        })
    </script>
</asp:Content>
