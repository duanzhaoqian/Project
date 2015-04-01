<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList<TXModel.Web.PremisesBuilding>>" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXModel.Web" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
    <tr>
        <th width="9%">
            楼栋名称
        </th>
        <th width="17%">
            建筑类别
        </th>
        <th width="17%">
            装修情况
        </th>
        <th width="17%">
            物业类型
        </th>
        <th width="16%">
            销售状态
        </th>
        <th width="24%">
            操作
        </th>
    </tr>
    <%
        foreach (var p in Model)
        {
    %>
    <tr>
        <td>
            <%=p.Name %><%=p.NameType%>
        </td>
        <td>
            <%string BuildingType = string.Empty;
              switch (p.BuildingType)
              {
                  case 1: BuildingType = "板楼"; break;
                  case 2: BuildingType = "塔楼"; break;
                  case 3: BuildingType = "砖楼"; break;
                  case 4: BuildingType = "砖混"; break;
                  case 5: BuildingType = "平房"; break;
                  case 6: BuildingType = "钢混"; break;
              } %>
            <%=BuildingType %>
        </td>
        <td>
            <%string Renovation = string.Empty;
              switch (p.Renovation)
              {
                  case 1: Renovation = "毛坯"; break;
                  case 2: Renovation = "简装修"; break;
                  case 3: Renovation = "精装修"; break;
                  case 4: Renovation = "菜单式装修"; break;
                  case 5: Renovation = "公共部分精装修"; break;
                  case 6: Renovation = "全装修"; break;
              } %>
            <%=Renovation%>
        </td>
        <td>
            <%
              var pType = "无";
              if (p.PropertyType.Length > 0)
              {
                  StringBuilder sb = new StringBuilder();
                  string[] PropertyType = p.PropertyType.Split(',');
                  for (int i = 0; i < PropertyType.Length; i++)
                  {
                      switch (PropertyType[i])
                      {
                          case "1": sb.Append("住宅,"); break;
                          case "2": sb.Append("写字楼,"); break;
                          case "3": sb.Append("别墅,"); break;
                          case "4": sb.Append("商业,"); break;
                      }
                  }
                  pType = sb.Remove(sb.ToString().LastIndexOf(","), 1).ToString();
              }
            %>
            <%=pType%>
        </td>
        <td>
            <%string SalesStatus = string.Empty;
              switch (p.State)
              {
                  case 1: SalesStatus = "待售"; break;
                  case 2: SalesStatus = "在售"; break;
                  case 3: SalesStatus = "售罄"; break;
                  case 4: SalesStatus = "建设中"; break;
                  case 5: SalesStatus = "规划中"; break;
              } %>
            <%=SalesStatus%>
        </td>
        <td>
            <a href="<%=TXCommons.GetConfig.BaseUrl%>NhBuilding/Edit?id=<%=p.Id %>" class="mr10">
                编辑</a> <a href="<%=TXCommons.GetConfig.BaseUrl%>NHouse/Create?pid=<%=p.PremisesId %>&bid=<%=p.Id %>"
                    class="mr10">发布房源</a> <a href="<%=TXCommons.GetConfig.GetSiteUrl%><%=TXCommons.NHWebUrl.BuildingDetailUrl(p.PremisesId.ToString(), p.Id.ToString())%>"
                        class="mr10" target="_blank">详情</a>
        </td>
    </tr>
    <%  }%>
    <%if (Model.Count <= 0)
      { %>
    <tr>
        <td colspan="6">
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
