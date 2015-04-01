<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXModel.NHouseActivies.YaoHao.CT_YaoHao>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
    <tr>
        <th width="20%">
            摇号活动名称
        </th>
        <th width="21%">
            楼盘
        </th>
        <th width="21%">
            楼栋
        </th>
        <th width="12%">
            申请时间
        </th>
        <th width="12%">
            活动状态
        </th>
        <th width="14%">
            操作
        </th>
    </tr>
    <%if (Model != null && Model.Count > 0)
      {
          foreach (TXModel.NHouseActivies.YaoHao.CT_YaoHao item in Model)
          {  
    %>
    <tr>
        <td>
            <%=item.Name %>
        </td>
        <td>
            <%=item.PremisesName%>
        </td>
        <td>
            <%=item.ActivitieExtend%>
        </td>
        <td>
            <%= string.Format("{0:yyyy-MM-dd}",item.CreateTime)%>
        </td>
        <td>
            <%switch (item.ActivitieState)
              {
                  case 5: %>
            已开始
            <%break;
                  case 6: %>
            已结束
            <%break;
                  default: %>未开始
            <%
              break;
              }%>
        </td>
        <td>
            <%if (item.ActivitieState == 6)
              {
                  if (item.ActivitieExtend == "全部")
                  {%>
            <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/YaoHaoUserList?id=<%=item.ActivitiesId %>&title=<%=item.PremisesName%>"
                class="mr10">查看报名用户</a>
            <%}
                  else
                  {%>
            <a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/YaoHaoUserList?id=<%=item.ActivitiesId %>&title=<%=item.PremisesName+" "+item.ActivitieExtend%>"
                class="mr10">查看报名用户</a>
            <%}
              }%>
        </td>
    </tr>
    <%}
      }
      else
      {%>
    <tr>
        <td colspan="6">
            <center>
                对不起，暂无记录！</center>
        </td>
    </tr>
    <%} %>
</table>
<div class="fen">
    <div class="tar">
        <span class="col333 font12 mr10">共<em class="col666">
            <%=ViewData["TotalItemCount"]%></em> 条记录</span>
        <%=Html.AjaxPager(Model, new PagerOptions
{
    ContainerTagName = "span",
    CssClass = "",
    PageIndexParameterName = "page",
    NumericPagerItemCount = 10,
    ShowFirstLast = false,
    SeparatorHtml = "",
    PrevPageText = "<<",
    NextPageText = ">>",
    AlwaysShowFirstLastPageNumber = false,
    CurrentPagerItemWrapperFormatString = "<a class=\"hover\">{0}</a>",
}, new AjaxOptions { UpdateTargetId = "divAjaxData" })%>
    </div>
</div>
