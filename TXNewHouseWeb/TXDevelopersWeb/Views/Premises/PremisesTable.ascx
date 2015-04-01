<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXOrm.Premis>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
    <tr>
        <th width="31%">
            楼盘名称
        </th>
        <th width="14%">
            物业类型
        </th>
        <th width="10%">
            销售状态
        </th>
        <th width="45%">
            操作
        </th>
    </tr>
    <%
        foreach (var p in Model)
        {
    %>
    <tr>
        <td>
            <%=((TXOrm.Premis)p).Name %>
        </td>
        <td>
            <%StringBuilder sb = new StringBuilder();
              string[] PropertyType = p.PropertyType.Split(',');
              for (int i = 0; i < PropertyType.Length; i++)
              {
                  switch (PropertyType[i])
                  {
                      case "1": sb.Append("住宅,"); break;
                      case "2": sb.Append("写字楼,"); break;
                      case "3": sb.Append("别墅,"); break;
                      case "4": sb.Append("商业,"); break;
                      default:
                          sb.Append(","); break;//下面会异常，值有0
                  }
              } %>
            <%= sb.Remove(sb.ToString().LastIndexOf(","), 1)%>
        </td>
        <td>
            <%string SalesStatus = string.Empty; switch (p.SalesStatus)
              {
                  case 0: SalesStatus = "待售"; break;
                  case 1: SalesStatus = "在售"; break;
                  case 2: SalesStatus = "售罄"; break;
              } %>
            <%=SalesStatus%>
        </td>
        <td>
            <a href="<%=TXCommons.GetConfig.BaseUrl%>Premises/Edit?id=<%=p.Id %>" class="mr10">编辑</a>
            <a href="<%=TXCommons.GetConfig.BaseUrl%>NhBuilding/Create?pid=<%=p.Id %>" class="mr10">
                发布楼栋</a> <a href="<%=TXCommons.GetConfig.GetSiteUrl%><%=TXCommons.NHWebUrl.PremisesIndexUrl(p.Id.ToString())%>"
                    class="mr10" target="_blank">详情</a> <a href="<%=TXCommons.GetConfig.BaseUrl%>Premises/PremisesImage/<%=p.Id %>"
                        class="mr10" target="_blank">编辑楼盘相册</a> <a href="javascript:void(0);" class="mr10"
                            onclick="javascript:Dialog.showDialog('url', '<%=TXCommons.GetConfig.BaseUrl%>premises/bargaininfo?premisesId=<%=p.Id %>', '<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif');">
                            今日成交信息</a>
        </td>
    </tr>
    <%  }%>
    <%if (Model.Count <= 0)
      { %>
    <tr>
        <td colspan="4">
            对不起，暂无记录！
        </td>
    </tr>
    <%} %>
</table>
<div class="fen">
    <div class="tar">
        <span class="col333 font12 mr10">共<em class="col666">
            <%=ViewData["totalCount"].ToString()%></em>条记录</span>
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
