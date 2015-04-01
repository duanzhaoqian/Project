<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_RegistrationUser>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="10%">
                序号
            </th>
            <th width="10%">
                出价人
            </th>
            <th width="10%">
                真实姓名
            </th>
            <th width="10%">
                身份证号
            </th>
            <th width="10%">
                手机号
            </th>
            <th width="10%">
                QQ号码
            </th>
            <th width="10%">
                E-mail
            </th>
            <th width="15%">
                团购房源
            </th>
            <th width="15%" class="lasted">
                报名时间
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
                <%= item.Id %>
            </td>
            <td>
                <%= item.BidUserName%>
            </td>
            <td>
                <%= item.RealName %>
            </td>
            <td>
                <%= item.IDCard %>
            </td>
            <td>
                <%= item.Mobile %>
            </td>
            <td>
                <%= item.BidUserQQ %>
            </td>
            <td>
                <%= item.BidUserMail %>
            </td>
            <td>
                <%= item.PremiseName %><br />
                <%= item.BuildingName %>&nbsp;&nbsp;
                <%= item.UnitName %>单元&nbsp;&nbsp;
                <%= item.Floor %>层&nbsp;&nbsp;
                <%= item.HouseNum %>&nbsp;&nbsp;
            </td>
            <td>
                <%= string.Format("{0:yyyy-MM-dd HH:mm}", item.CreateTime) %>
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