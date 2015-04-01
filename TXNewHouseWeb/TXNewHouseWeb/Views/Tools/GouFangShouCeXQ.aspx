<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%
        TXNewHouseWeb.Common.SEOModel seo = ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel;
    %>
    <%=seo==null?string.Empty:seo.Title %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="bgcolor">
        <div class="w1190 line_h50 font14 col666">
            当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                class="blue">北京新房</a> > <a href="<%=TXCommons.GetConfig.GetQTBaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-1.html"
                    class="blue">工具</a> > 购房手册
        </div>
    </div>
    <div class="bg_fbfbfb">
        <div class="pt15 pb15 w1190 clearFix bg_fbfbfb">
            <div class="manual_nr_tit pb10">
                新手买房必看系列——买房前期准备</div>
            <div class="tac col666 font12 pad_20_15">
                时间：2013年12月03日 17：55</div>
            <div class="manual_nr_box">
                <p>
                    一、买房目标的确定</p>
                <p class="txt_ind">
                    理性和有规划的消费——购房的前提。根据自己的收入、支出等实际情况来确定适合自己的楼盘。不要一买房就是要买三室两厅，做到一步到位；而是从 自己的实际情况出发，好好规划一下，其实能满足基本的居住需求就好，避免出现不必要的额外负担，而培养有梯度的消费观很重要。</p>
                <p>
                    二、买房首付款的积累</p>
                <p class="txt_ind">
                    买房首付款的积累——购房的关键。首先初定一个目标，比如毕业后5年内支付一个首付款，那么就为了实现自己这个目标，合理的分配一下你的收入。 每月固定的留出一部分资金出来，制定出一个定期定额的计划。因为如果开通了定期定额的强制储蓄计划，那么每个月必须得固定提出一笔资金，从而意味着
                    可用资金必定减少，那么消费一定可以更加理性。点点滴滴的积累就是一笔财富。此外，建议也可先向父母借首付款，日后陆续返还，缩短积累时间和降低潜在 的涨价成本。</p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </div>
        </div>
    </div>
    <!-- InstanceEndEditable -->
</asp:Content>
