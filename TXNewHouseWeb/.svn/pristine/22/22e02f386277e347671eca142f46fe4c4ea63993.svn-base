<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/YaoHao.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<ActivityList>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="TXModel.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    网络摇号-摇号活动列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wlyhbj">
        <div class="wlyh_main clearFix">
            <div class="wlyh_l">
                <%if (ViewData["activity"] != null)
                  {
                      ActivityList activity = ViewData["activity"] as ActivityList; %>
                <div class="wlyh_title1">
                    <span class="whyh_t_s"><i class="font24">
                        <%=activity.Periods %></i>期</span><%=activity.Name %></div>
                <div class="wlyh_box">
                    <dl class="clearFix">
                        <dt><a href="">
                            <img src="<%=TXCommons.GetConfig.ImgUrl%>images/img_w168_h129.jpg" /></a></dt>
                        <dd>
                            <p>
                                <span class="col333">摇号时间：</span><%=activity.SignupBeginTime.ToString("yyyy年MM月dd日 HH:mm")%></p>
                            <p>
                                <span class="col333">摇号地点：</span><%=activity.ActivitieLocation%></p>
                            <p>
                                <span class="col333">公证处：</span><%=activity.NotarialOffice %></p>
                            <p>
                                <span class="col333">备选数量：</span><%=activity.HouseCount %></p>
                            <p>
                                <span class="col333">参与人数：</span><%=activity.UserCount %></p>
                            <p class="mt10">
                                <a href="<%=TXCommons.NHWebUrl.PremisesIndexUrl(activity.PremisesId.ToString()) %>"
                                    class="btn_w96_gray">楼盘详情</a><a href="" class="btn_w96_gray ml5">摇号直播现场</a></p>
                        </dd>
                    </dl>
                    <p>
                        <%=activity.Introduction %></p>
                </div>
                <%} %>
                <h3 class="wlyh_h3 mb10">
                    往期回顾</h3>
                <%if (Model != null)
                  {
                      foreach (ActivityList al in Model)
                      {
                %>
                <div class="wlyh_title2">
                    <span class="whyh_t_s"><i class="font24">
                        <%=al.Periods %></i>期</span><%=al.Name %></div>
                <div class="wlyh_box">
                    <dl class="clearFix">
                        <dt><a href="">
                            <img src="<%=TXCommons.GetConfig.ImgUrl%>images/img_w168_h129.jpg" /></a></dt>
                        <dd>
                            <p>
                                <span class="col333">摇号时间：</span><%=al.SignupBeginTime.ToString("yyyy年MM月dd日 HH:mm")%></p>
                            <p>
                                <span class="col333">摇号地点：</span><%=al.ActivitieLocation %></p>
                            <p>
                                <span class="col333">公证处：</span><%=al.NotarialOffice %></p>
                            <p>
                                <span class="col333">备选数量：</span><%=al.HouseCount %></p>
                            <p>
                                <span class="col333">参与人数：</span><%=al.UserCount %></p>
                            <p class="mt10">
                                <a href="<%=TXCommons.NHWebUrl.PremisesDetailUrl(al.PremisesId.ToString()) %>" class="btn_w96_gray">楼盘详情</a><a href=""
                                    class="btn_w96_gray ml5">摇号直播现场</a></p>
                        </dd>
                    </dl>
                    <p>
                        <%=al.Introduction %></p>
                </div>
                <%}
                  }%>
            </div>
            <div class="wlyh_r">
                <div class="green_line">
                </div>
                <h3 class="wlyh_h3_1">
                    快有家网络摇号介绍</h3>
                <div class="text mt15 mb20">
                    快有家开发的网络摇号系统，可随机抽取、分组摇号、排序，并提供网络直播查询和短信自动发送。全部过程保证客户资料不外泄。三重错误规避逻辑避免了错误的摇号的出现，保证了摇号的公正和安全。</div>
                <div class="green_line">
                </div>
                <h3 class="wlyh_h3_1">
                    快有家网络摇号流程</h3>
                <ul class="green_ul">
                    <li><i>1</i>参加摇号报名</li>
                    <li><i>2</i>网络直播摇号</li>
                    <li class="mb42"><i>3</i>公证员现场公正</li>
                    <li><i>4</i>公布结果</li>
                </ul>
            </div>
        </div>
        <!--end 分页-->
        <div class="w1190">
            <!--begin 分页-->
            <div class="bidFenyeBar clearFix mt35">
                <%=Model != null ? Html.Pager(Model.TotalItemCount, Model.PageSize, Model.CurrentPageIndex, null, null, new PagerOptions { SeparatorHtml = " ", ShowFirstLast = true, ShowPageIndexBox = true, FirstPageText = "", LastPageText = "", GoButtonText = "", CssClass = "pub_fenye1", SelfDefineUrl = ViewData["conPage"].ToString(), CurrentPagerItemWrapperFormatString = "<a class=\"current\">" + Model.CurrentPageIndex + "</a>", SelfParameterLenght = -1, PrevCss = "getL", NextCss = "getR", FirstCss = "getL1", LastCss = "getR1", SpanCSS = "col666 font14 fytjy", ShowGoButtonCss = "fytz" }, null, null, null).ToHtmlString() : ""%>
            </div>
        </div>
    </div>
</asp:Content>
