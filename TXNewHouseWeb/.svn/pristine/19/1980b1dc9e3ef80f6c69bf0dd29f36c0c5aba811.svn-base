<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<TXCommons.Index.IndexRecList>>" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<div class="r_title_lp">
    <strong class="title_span">同区域<span class="colff840b">热门</span>楼盘</strong></div>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <th scope="col">
            楼盘名称
        </th>
        <th scope="col">
            区县
        </th>
        <th scope="col">
            均价
        </th>
        <th scope="col">
            点击(次)
        </th>
    </tr>
    <%if (Model != null && Model.Count > 0)
      { %>
    <% foreach (var item in Model)
       {
           %>
    <tr>
        <td>
            <a target="_blank" href="<%=TXCommons.NHWebUrl.PremisesIndexUrl(item.PremisesID.ToString()) %>"
                class="blue">
                <%=Util.getStrLenB(item.PremisesName, 0, 18)%></a>
        </td>
        <td>
            <%=item.DistrictName%>
        </td>
        <td>
            <span class="colff840b">
                <%=Convert.ToDouble(item.ReferencePrice) > 0 ? Convert.ToDouble(item.ReferencePrice) + "元/平方米" : "价格待定"%></span>
        </td>
        <td>
            <%=item.ClickRate%>
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
</table>
