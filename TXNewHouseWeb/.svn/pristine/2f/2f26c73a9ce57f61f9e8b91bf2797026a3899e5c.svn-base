<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<ActivityHouse>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXModel.NHouseActivies.Common" %>
<div id="divpages" class="mt15 clearFix" style="position: relative;">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
        <tr>
            <th width="5%">
                房号
            </th>
            <th width="5%">
                楼层
            </th>
            <th width="5%">
                楼栋名称
            </th>
            <th width="10%">
                参考价格
            </th>
            <th width="10%">
                <%
                    if (Convert.ToString(ViewData["type"]) == "7")
                    { 
                %>秒杀价格<%
                }
                else if (Convert.ToString(ViewData["type"]) == "8")
                {
                %>一口价<%
                } 
                %>
            </th>
            <th width="9%">
                时间
            </th>
            <th width="9%">
                剩余时间
            </th>
            <th width="8%">
                活动保证金
            </th>
            <th width="6%">
                状态
            </th>
            <th width="9%">
                查看用户报名
            </th>
        </tr>
        <%
            if (Model != null && Model.Count > 0)
            {
                for (int i = 0; i < Model.Count; i++)
                {
        %>
        <tr>
            <td>
                <%=Model[i].HouseNo%>
            </td>
            <td>
                <%=Model[i].Floor%>层
            </td>
            <td>
                <%=Model[i].BuildName%><%=Model[i].NameType%>
            </td>
            <td>
                <%=Convert.ToInt32(Model[i].SinglePrice)%>万元/套
            </td>
            <td>
                <%=Convert.ToInt32(Model[i].BidPrice)%>万元/套
            </td>
            <td>
                <%=Model[i].BeginTime.ToString("yyyy-MM-dd")%><br />
                ---<%=Model[i].EndTime.ToString("yyyy-MM-dd")%>
            </td>
            <td>
                <% 
                if (Model[i].ActivitieState == 2)
                {
                %><span>无</span><%
                        }
                        else if (Model[i].ActivitieState == 0)
                        {
                %><span>--</span><%
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
                        }
                %>
            </td>
            <td>
                <%=Model[i].Bond.ToString("f0")%>元
            </td>
            <td>
                <%
                if (Model[i].ActivitieState == 0)
                { 
                %>未开始<%
                        }
                        else if (Model[i].ActivitieState == 1)
                        { 
                %>进行中<%
                        }
                        else if (Model[i].ActivitieState == 2)
                        { 
                %>已结束<%
                        } 
                %>
            </td>
            <td>
                <%
                if (Model[i].ActivitieState == 2)
                {
                    if (Convert.ToString(ViewData["type"]) == "5" || Convert.ToString(ViewData["type"]) == "6")
                    { 
                %><a target="_blank" href='<%=TXCommons.GetConfig.BaseUrl%>Activity/BidingUserList?houseId=<%=Model[i].Id %>'>查看用户报名</a><%
                            }
                            else if (Convert.ToString(ViewData["type"]) == "7")
                            {
                %><a target="_blank" href='<%=TXCommons.GetConfig.BaseUrl%>Activity/SecKillUserList?houseId=<%=Model[i].Id %>'>查看用户报名</a><%
                            }
                            else if (Convert.ToString(ViewData["type"]) == "8")
                            {
                %><a target="_blank" href='<%=TXCommons.GetConfig.BaseUrl%>Activity/APrice?houseId=<%=Model[i].Id %>'>查看用户报名</a><%
                            }
                        }
                        else
                        {
                %>--<%
                        } 
                %>
            </td>
        </tr>
        <%
            }
        }
        else
        { 
        %><tr>
            <td colspan="12">
                对不起，暂无记录！
            </td>
        </tr>
        <%
      } 
        %>
    </table>
</div>
<div class="fen">
    <div class="tar">
        <span class="col333 font12 mr10">共&nbsp;<em class="col666"><%=Model.TotalItemCount%></em>&nbsp;条记录</span>
        <%=Ajax.Pager(Model, new PagerOptions()
            {   
                ContainerTagName = "span",
                PageIndexParameterName = "Id",
                NumericPagerItemCount = 10,
                ShowFirstLast = false,
                SeparatorHtml = "",
                PrevPageText = "<<",
                NextPageText = ">>",
                AlwaysShowFirstLastPageNumber = false,
                CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>"
            }, new AjaxOptions { UpdateTargetId = "divpages01" })%>
    </div>
</div>
