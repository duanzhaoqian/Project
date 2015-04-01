<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_BidingUsers>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="5%">
            </th>
            <th width="7%">
                序号
            </th>
            <th width="8%">
                出价人
            </th>
            <th width="10%">
                真实姓名
            </th>
            <th width="15%">
                身份证号
            </th>
            <th width="10%">
                手机号
            </th>
            <th width="10%">
                QQ号码
            </th>
            <th width="15%">
                E-mail
            </th>
            <th width="20%">
                出价时间
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
                <span class="yellow_bjtb">成交</span>
            </td>
            <td>
                <%= item.Id %>
            </td>
            <td>
                <%= item.BidUserName%>
            </td>
            <td>
                <%= item.BidRealName%>
            </td>
            <td>
                <%= item.IDCard %>
            </td>
            <td>
                <%= item.BidUserMobile%>
            </td>
            <td>
                <%= item.BidUserQQ %>
            </td>
            <td>
                <%= item.BidUserMail %>
            </td>
            <td>
                <%= item.CreateTime %>
            </td>
        </tr>
        <% }
           }
           else
           { %>
        <tr>
            <td colspan="10">
                暂无数据！
            </td>
        </tr>
        <% } %>
    </tbody>
</table>