<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<CT_Activity>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXModel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-限时折扣活动列表
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                限时折扣活动列表</div>
            <div class="font14 fr mr35">
                <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/Discount">创建限时折扣</a>
            </div>
        </div>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
                <tr>
                    <th width="15%">
                        活动名称
                    </th>
                    <th width="15%">
                        活动时间
                    </th>
                    <th width="15%">
                        剩余时间
                    </th>
                    <th width="8%">
                        房源数量
                    </th>
                    <th width="8%">
                        折扣
                    </th>
                    <th width="10%">
                        活动保证金
                    </th>
                    <th width="8%">
                        状态
                    </th>
                    <th width="10%">
                        操作
                    </th>
                </tr>
                <%for (int i = 0; i < Model.Count; i++)
                  {%>
                <tr>
                    <td>
                        <%=Model[i].Name %>
                    </td>
                    <td>
                        <%=string.Format("{0:yyyy-MM-dd}",Model[i].BeginTime)%>——<%=string.Format("{0:yyyy-MM-dd}", Model[i].EndTime)%>
                    </td>
                    <td>
                        <% 
                      if (Model[i].ActivitieState == 2)
                      {
                        %><span>无</span><%
                      }
                      else
                      {
                          TimeSpan span = Model[i].EndTime - DateTime.Now;
                          if (span.Days >= 1)
                          { 
                        %><span><%=span.Days%>天</span><span><%=span.Hours%>小时</span><%
                          }
                          else if (span.Days == 0)
                          {
                              if (span.Hours > 0 || (span.Hours == 0 && span.Minutes > 0))
                              {
                        %><span><%=span.Hours%>小时</span><span><%=span.Minutes%>分</span><%
                              }
                          }
                          else
                          {
                        %><span>无</span><%
                          }
                      }
                        %>
                    </td>
                    <td>
                        <%=Model[i].HouseCount %>
                    </td>
                    <td>
                        <%
                      {
                          int maxDis = Model[i].ListDiscount.Max();
                          int minDis = Model[i].ListDiscount.Min();

                          if (maxDis != minDis)
                          {
                        %>
                        <%=string.Format("{0}-{1}折",minDis,maxDis) %>
                        <%}
                          else
                          {
                        %>
                        <%=string.Format("{0}折",minDis) %>
                        <%}

                      } %>
                    </td>
                    <td>
                        <%=Model[i].Bond.ToString("#.##") %>元
                    </td>
                    <td>
                        <%if (Model[i].ActivitieState == 0)
                          { %>
                        未开始
                        <%}
                          else if (Model[i].ActivitieState == 1)
                          { %>
                        进行中
                        <%}
                          else if (Model[i].ActivitieState == 2)
                          { %>
                        已结束
                        <%} %>
                    </td>
                    <td>
                        <a target="_blank" href='<%=TXCommons.GetConfig.BaseUrl%>Activity/DiscountDetail?activityId=<%=Model[i].Id %>'>
                            详情</a>
                        <%if (Model[i].ActivitieState == 2)
                          {
                        %>
                        <a target="_blank" href='<%=TXCommons.GetConfig.BaseUrl%>Activity/DiscountUserList?activityId=<%=Model[i].Id %>'>
                            查看用户报名</a>
                        <%
                          }
                        %>
                    </td>
                </tr>
                <%}%>
                <% if (Model.Count == 0)
                   {
                %>
                <tr>
                    <td colspan="8">
                        暂无数据！
                    </td>
                </tr>
                <%
                   } %>
            </table>
        </div>
        <div class="fen">
            <div class="tar">
                <span class="col333 font12 mr10">共<em class="col666"><%=Model.TotalItemCount%></em>
                    条记录</span>
                <%=Html.Pager(Model, new PagerOptions
                     {
                            ContainerTagName = "span",
                            CssClass = "",
                            PageIndexParameterName = "id",
                            NumericPagerItemCount = 10,
                            ShowFirstLast = false,
                            SeparatorHtml = "",
                            PrevPageText = "<<",
                            NextPageText = ">>",
                            AlwaysShowFirstLastPageNumber = false,
                            CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>",
                     })%>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
