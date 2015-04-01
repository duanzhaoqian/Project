<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Building>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="10%">
                楼栋名称
            </th>
            <th width="10%">
                建筑类别
            </th>
            <th width="10%">
                装修情况
            </th>
            <th width="10%">
                物业类型
            </th>
            <th width="10%">
                销售状态
            </th>
            <th width="50%" class="lasted">
                操作
            </th>
        </tr>
    </thead>
    <tbody class="test">
        <% if (Model != null && Model.Count > 0)
           {
               foreach (var item in Model)
               {
        %>
        <tr>
            <td>
                <%= item.Name %><%= item.NameType %>
            </td>
            <td>
                <%= item.BuildingTypeString %>
            </td>
            <td>
                <%= item.RenovationString %>
            </td>
            <td>
                <%= item.PropertyTypeString %>
            </td>
            <td>
                <%= item.StateString %>
            </td>
            <td class="lasted">
                <a href="javascript:void(0);" onclick="GoToNewPage('<%= Url.SiteUrl().Building_Search("editbuilding") + "?buildingId=" + item.Id %>')">编辑</a>
                &nbsp;&nbsp;<a href="javascript:void(0);" onclick="building.del('<%= item.Id %>')">删除</a>
                <% if (0 < item.HouseCount)
                   {
                %>
                &nbsp;&nbsp;<a href="<%= Url.SiteUrl().NewHouseIndex_FromBuilding(item.Id) %>">房源管理</a>
                <% } %>
                <% if (!(item.State == 4 || item.State == 5))
                   {
                %>
                &nbsp;&nbsp;<a href="<%= Url.SiteUrl().AddNewHouse(item.Id, 1) %>">发布房源</a>   
               <% } %>
                
            </td>
        </tr>
        <%
               }
           }
           else
           {
        %>
        <tr>
            <td colspan="6">
                暂无数据！
            </td>
        </tr>
        <%
           } %>
    </tbody>
</table>