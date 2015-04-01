<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXCommons.Index.IndexRecList>>" %>
<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<div class="r_title_lp">
    <span class="title_span">同区域最热楼盘排行</span></div>
<table width="100%" cellspacing="0" cellpadding="0" border="0">
    <tbody>
        <tr>
            <th width="48%" scope="col">
                楼盘名称
            </th>
            <th width="37%" scope="col">
                均价(元/㎡)
            </th>
            <th width="15%" align="right" scope="col" style="text-align: right">
                对比
            </th>
        </tr>
        <%if (Model != null && Model.Count > 0)
          { %>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <a target="_blank" class="linkB" href="<%=TXCommons.NHWebUrl.PremisesIndexUrl(item.PremisesID.ToString()) %>">
                    <%=Util.getStrLenB(item.PremisesName,0,16)%></a>
            </td>
            <td>
                <span class="colff840b">
                    <%=Convert.ToDouble(item.ReferencePrice).Equals(0) ? "价格待定" : Convert.ToString(Convert.ToDouble(item.ReferencePrice))%></span>
            </td>
            <td align="right">
                <a href="javascript:void(0);" data="<%=Util.getStrLenB(item.PremisesName,0,24)%>"
                    lang="<%=item.PremisesID %>" url="<%=NHWebUrl.PremisesIndexUrl(item.PremisesID.ToString()) %>"
                    class="blue plus_compare">对比</a>
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
