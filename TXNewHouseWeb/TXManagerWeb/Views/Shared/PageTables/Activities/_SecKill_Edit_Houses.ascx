<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_SecKill_Edit_Houses>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="5%">
                房号
            </th>
            <th width="5%">
                楼层
            </th>
            <th width="10%">
                单元
            </th>
            <th width="10%">
                楼号
            </th>
            <th width="10%">
                户型
            </th>
            <th width="10%">
                建筑面积
            </th>
            <th width="10%">
                单价
            </th>
            <th width="10%">
                总价
            </th>
            <th width="10%">
                销售状态
            </th>
            <th width="10%">
                发布日期
            </th>
            <th width="10%" class="lasted">
                操作
            </th>
        </tr>
    </thead>
    <tbody class="test" id="tbody_seckill">
        <% if (Model != null && Model.Count > 0)
           {
               foreach (var item in Model)
               { %>
        <tr id="tr_<%= item.HouseId %>">
            <td>
                <%= item.HouseName %>
            </td>
            <td>
                <%= item.FloorString %>
            </td>
            <td>
                <%= item.UnitName %>
            </td>
            <td>
                <%= item.BuildingName %>
            </td>
            <td>
                <%= item.RoomTypeString %>
            </td>
            <td>
                <%= item.BuildingAreaString %>平方米
            </td>
            <td>
                <%= item.SinglePriceString %>元/平方米
            </td>
            <td>
                <%= item.TotalPriceString %>万元/套
            </td>
            <td>
                <%= item.SalesStatusString %>
            </td>
            <td>
                <%= item.CreateTimeString %>
            </td>
            <td>
                <a href="javascript:void(0);" id="join_<%= item.HouseId %>" value="<%= item.HouseId %>"
                    onclick="seckill_edit_step2.join_activity('<%= item.HouseId %>')">参加</a>
            </td>
        </tr>
        <% }
           }
           else
           { %>
        <tr>
            <td colspan="11">
                暂无数据！
            </td>
        </tr>
        <% } %>
    </tbody>
</table>