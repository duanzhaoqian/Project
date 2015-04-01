<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Discount_Edit2>>" %>

<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="10%">
                房号
            </th>
            <th width="10%">
                楼层
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
                参考总价
            </th>
            <th width="10%">
                限时折扣
            </th>
            <th width="10%">
                折后价格
            </th>
            <th width="10%" class="lasted">
                操作
            </th>
        </tr>
    </thead>
    <tbody class="test" id="tbody_discount">
        <% if (Model != null && Model.Count > 0)
           {
               foreach (var item in Model)
               { %>
        <tr id="tr_<%= item.HouseId %>">
            <td>
                <%= item.HouseName %>
            </td>
            <td>
                <%= item.FloorName %>
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
                <span id="totalprice_<%= item.HouseId %>"><%= item.TotalPriceString %></span>万元/套
            </td>
            <td>
                <input type="text" class="w40" id="discount_<%= item.HouseId %>" houseId="<%= item.HouseId %>" value="<%= item.DiscountString %>" onkeyup="editdiscount_step4.updateDiscount('<%= item.HouseId %>')" />&nbsp;&nbsp;折
            </td>
            <td>
                <span id="aftertotalprice_<%= item.HouseId %>"><%= item.AfterDiscountPriceString %></span>万元/套
            </td>
            <td>
                <a href="javascript:void(0);" id="deljoin_<%= item.HouseId %>" value="<%= item.HouseId %>" onclick="editdiscount_step4.delJoinHouse_Discount('<%= item.HouseId %>')">删除</a>
            </td>
        </tr>
        <% }
           }
           else
           { %>
        <tr>
            <td colspan="9">
                暂无数据！
            </td>
        </tr>
        <% } %>
    </tbody>
</table>