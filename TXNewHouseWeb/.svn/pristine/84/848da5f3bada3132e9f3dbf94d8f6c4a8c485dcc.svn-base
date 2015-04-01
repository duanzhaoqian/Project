<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_SecKill>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="5%">
                房号
            </th>
            <th width="5%">
                楼层
            </th>
            <th width="5%">
                单元
            </th>
            <th width="5%">
                楼号
            </th>
            <th width="10%">
                所属开发商
            </th>
            <th width="10%">
                总价
            </th>
            <th width="10%">
                秒杀价格
            </th>
            <th width="10%">
                时间
            </th>
            <th width="10%">
                剩余时间
            </th>
            <th width="10%">
                活动所在城市
            </th>
            <th width="10%">
                状态
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
                <%= string.IsNullOrEmpty(item.Developer) ? item.Agent : item.Developer %>
            </td>
            <td>
                <%= item.TotalPriceString %>万元/套
            </td>
            <td>
                <%= item.BidPriceString %>万元/套
            </td>
            <td>
                <%= item.ActivityTime %>
            </td>
            <td>
                <%= item.ActivitieState != 2 && item.ActivitieState != 0 ? item.RestTimeString : "--"%>
            </td>
            <td>
                <%= item.BelongCity %>
            </td>
            <td>
                <%= item.ActivitieStateString %>
            </td>
            <td>
                <a href="javascript:void(0);" onclick="seckill.edit('<%= item.Id %>')">编辑</a><%--GoToNewPage('<%= Url.SiteUrl().Discount_Search("editdiscount") %>?id=<%= item.Id %>&ram=<%= new Random().Next(1, 999999999) %>')--%>
                &nbsp;&nbsp;<a href="javascript:void(0);" onclick="seckill.del('<%= item.Id %>')">删除</a><%--discount.del('<%= item.Id %>')--%>
                &nbsp;&nbsp;<a href="<%= Url.SiteUrl().SecKill_Search("UserReportList") %>?activeId=<%= item.Id %>" target="_blank">查看用户报名</a>
            </td>
        </tr>
        <% }
           }
           else
           { %>
        <tr>
            <td colspan="12">
                暂无数据！
            </td>
        </tr>
        <% } %>
    </tbody>
</table>