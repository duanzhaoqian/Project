<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXCommons.Index.IndexRecList>>" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<div class="r_title_lp">
    <strong class="title_span"><%=ViewData["cityName"]%>最新楼盘</strong></div>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tbody>
        <tr>
            <th width="48%" scope="col">
                楼盘名称
            </th>
            <th width="19%" scope="col">
                区县
            </th>
            <th width="23%" align="" style="text-align: " scope="col">
                点击（次）
            </th>
        </tr>
<%if (Model != null && Model.Count > 0)
  { %>
<% foreach (var item in Model)
   {%>
        <tr>
            <td>
                <a href="<%=BaseUrl.PremisesIndexUrl() %>-<%=item.PremisesID.ToString() %>.html" class="linkB" target="_blank" title="<%= item.PremisesName %>">
                    <%= item.PremisesName.Length>11?item.PremisesName.Substring(0,9)+"...":item.PremisesName%></a>
            </td>
            <td>
                <%= item.DistrictName%>
            </td>
            <td align="">
                <span class="colff840b mr10">
                    <%=item.ClickRate%></span>
            </td>
        </tr>
<%}%>
<%}
  else
  {%>
        <tr>
            <td colspan="3">
                暂无数据！
            </td>
        </tr>
<%}%>
    </tbody>
</table>
        
