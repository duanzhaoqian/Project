<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXOrm.BidPrice>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="table-layout: fixed;
    word-wrap: break-word; word-break: break-all" class="tabBorder">
    <thead>
        <tr>
            <th width="50">
                <div class="col666 pl20">
                    序号</div>
            </th>
            <th width="120">
                <span class="col666 font12">出价金额</span>
            </th>
            <th width="100">
                <span class="col666 font12">出价时间</span>
            </th>
            <th width="100">
                <span class="col666 font12">目前排名</span>
            </th>
            <th width="120">
                <span class="col666 font12">中标状态</span>
            </th>
        </tr>
    </thead>
    <tbody>
        <%if (Model != null && Model.Count > 0)
          {
              int i = 1;
              foreach (var item in Model)
              {%>
        <tr>
            <td>
                <div class="pl20">
                    <%=i %></div>
            </td>
            <td>
                <%=(double)item.BidUserPrice %>万元
            </td>
            <td>
                <%=string.Format("{0:yyyy-MM-dd HH:mm:ss}",item.CreateTime)%>
            </td>
            <td>
                第<%=item.Id %>名
            </td>
            <td>
                <%if (item.Status == 1)
                  { %>
                <span class="col6aa700">已中标</span>
                <%}
                  if (item.Status == 2)
                  { %>
                <span class="col6aa700">已成交</span>
                <%}
                  else
                  { %>
                <span>未中标</span>
                <%} %>
            </td>
        </tr>
        <% i++;
              }
          }
          else
          { %>
        <tr>
            <td colspan="5" align="center">
                对不起，暂无记录！
            </td>
        </tr>
        <%} %>
    </tbody>
</table>
<div class="page mb20">
    <div class="tar">
        <span class="col666 font12 mr10">共
            <%=ViewData["TotalItemCount"]%>
            条记录</span>
        <%=Html.AjaxPager(Model, new PagerOptions
{
    ContainerTagName = "span",
    CssClass = "",
    PageIndexParameterName = "page",
    ShowFirstLast = false,
    SeparatorHtml = "",
    PrevPageText = "<<",
    NextPageText = ">>",
    AlwaysShowFirstLastPageNumber = false,
    CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>"
}, new AjaxOptions { UpdateTargetId = "divAjaxData" })%>
    </div>
</div>
