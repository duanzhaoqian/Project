<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/User.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXModel.User.CT_Yaohao>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    我的新房-我参与的网络摇号
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearFix">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="p_wrap">
            <div class="bidtitle mt20" style="padding-left: 0px;">
                <span class="fl mr30">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/wdxf_tit.gif" /></span>
                <h6 class="fl">
                    <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid">我参与的竞购</a> <a href="javascript:void(0)"
                        class="cur">我参与的网络摇号</a> <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/collectionsPremise">
                            我收藏的楼盘</a>
                </h6>
            </div>
            <div class="select_title">
                <%--  <a href="" class="bold col666">全部</a> <a href="" class="bg_ff6600">秒杀</a> <a href="">
                    竞价</a> <a href="">一口价</a> <a href="">砍价</a> <a href="">限时折扣</a> <a href="">限时团购</a>
                <a href="">排号购房</a>--%>
            </div>
            <div class="bidCont">
                <%if (Model != null && Model.Count > 0)
                  {
                      foreach (var item in Model)
                      {
                %>
                <div class="bidContent borderB">
                    <div class="clearFix pd20 bidPos">
                        <div class="bidImg fl mr20">
                            <img src="<%=TXCommons.GetConfig.ImgUrl%>images/sp_img.png" />
                        </div>
                        <div class="bidCt fl">
                            <p class="font18 fontYahei bidHeight mb10">
                                <a href="#" class="linkB fl">
                                    <%=item.PremisesName %>
                                    网络摇号</a></p>
                            <ul>
                                <li class="w400">地点：<span class="col333"><%=item.ActivitieLocation %></span></li>
                                <li>备选套数：<span class="col333">367套</span> </li>
                                <li>参与人数：<span class="col333"><%=item.UserCount %>人</span></li>
                                <li>房源：<span class="col333">保利花园B区</span></li>
                                <li>选房时间：<span class="col333"><%=string.Format("{0:yyyy-MM-dd}",item.ChooseHouseTime) %></span></li>
                                <li>摇号公证：<span class="col333"><%=item.NotarialOffice %></span></li>
                                <li class="w200">活动时间：<span class="col333"><%=string.Format("{0:yyyy年MM月dd日}",item.BeginTime) %></span></li>
                                <li class="w200">报名时间：<span class="col333"><%=string.Format("{0:yyyy-MM-dd}",item.SignupBeginTime) %>--<%=string.Format("{0:yyyy-MM-dd}",item.SignupEndTime) %></span></li>
                            </ul>
                        </div>
                        <div class="otherv tar">
                            <a href="#" class="fr linkA">网络摇号现场&gt;&gt;</a>
                            <%if (item.Bond == 0)
                              {%>
                            <span class="fl mr10 col6aa700">保证金已退回</span>
                            <%} %></div>
                        <div class="pos_iico">
                            <%if (item.ActivieStatus == 2)
                              { %>
                            <span class="ico_yjs">已结束</span><%}
                              else
                              { %>
                            <span class="ico_jxz">进行中</span>
                            <% } %></div>
                    </div>
                    <!--//bidPos-->
                </div>
                <%}
                  }
                  else
                  { %>
                <div class="clearFix pd20 bidPos">
                    <center>对不起，暂无记录！</center>
                </div>
                <%} %>
            </div>
            <!--begin 分页-->
            <div class="bidFenyeBar clearFix">
                <%=Model != null ? Html.Pager(Model.TotalItemCount, Model.PageSize, Model.CurrentPageIndex, null, null, new PagerOptions { SeparatorHtml = " ", FirstPageText = "", LastPageText = "", CssClass = "pub_fenye1", CurrentPagerItemWrapperFormatString = "<a class=\"current\">" + Model.CurrentPageIndex + "</a>", SelfParameterLenght = -1, PrevCss = "getL", NextCss = "getR", SpanCSS = "col666 font14 fytjy"}, null, null, null).ToHtmlString() : ""%>
            </div>
            <!--end 分页-->
            <div class="shadowHR mb20">
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
