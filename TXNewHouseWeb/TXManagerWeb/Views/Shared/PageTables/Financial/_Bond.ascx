<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Bond>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="20%">
                预计退还时间
            </th>
            <th width="10%">
                省份
            </th>
            <th width="10%">
                城市
            </th>
            <th width="10%">
                房源编号
            </th>
            <th width="10%">
                会员类别
            </th>
            <th width="10%">
                登录名
            </th>
            <th width="10%">
                手机号码
            </th>
            <th width="10%">
                保证金额
            </th>
            <th width="10%" class="lasted">
                状态
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
                <%= item.EndTimeString %>
            </td>
            <td>
                <%= item.ProvinceName %>
            </td>
            <td>
                <%= item.CityName %>
            </td>
            <td>
                <%= item.HouseId %>
            </td>
            <td>
                个人
            </td>
            <td>
                <%= item.BidUserName %>
            </td>
            <td>
                <%= item.BidUserMobile %>
            </td>
            <td>
                <%= item.BondString %>
            </td>
            <td class="lasted">
                <%= item.BondStatusString %>
            </td>
        </tr>
        <%
               }
           }
           else
           {
        %>
        <tr>
            <td colspan="9">
                暂无数据！
            </td>
        </tr>
        <%
           } %>
    </tbody>
</table>