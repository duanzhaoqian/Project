<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Discount_Edit1>>" %>

<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="10%">
                房号
            </th>
            <th width="5%">
                楼层
            </th>
            <th width="5%">
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
    <tbody class="test">
        <% if (Model != null && Model.Count > 0)
           {
               foreach (var item in Model)
               { %>
        <tr>
            <td>
                <input type="checkbox" id="chk_h_<%= item.HouseId %>" value="<%= item.HouseId %>" />
                <%= item.HouseName %>
            </td>
            <td>
                <%= item.FloorName %>
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
                <%= item.BuildingAreaString %>
            </td>
            <td>
                <%= item.SinglePriceString %>
            </td>
            <td>
                <%= item.TotalPriceString %>
            </td>
            <td>
                <%= item.SalesStatusString %>
            </td>
            <td>
                <%= item.CreateTimeString %>
            </td>
            <td>
                <a href="javascript:void(0);" id="join_<%= item.HouseId %>" value="<%= item.HouseId %>" onclick="editdiscount_step3.signupJoin('<%= item.HouseId %>')"></a>
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