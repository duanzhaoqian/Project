<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_WithdrawCash>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="12%">
                提现申请时间
            </th>
            <th width="14%">
                登录名
            </th>
            <th width="8%">
                手机号码
            </th>
            <th width="14%">
                本次提现金额
            </th>
            <th width="36%">
                收款账户信息
            </th>
            <th width="16%" class="lasted">
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
                <%= item.CreateTimeString %>
            </td>
            <td>
                <%= item.LoginName %>
            </td>
            <td>
                <%= item.Mobile %>
            </td>
            <td>
                <%= string.Format("{0:N2}", item.Price) %>
            </td>
            <td>
                <% if (item.ReceiveType == 0)
                   { %>
                <span>[支付宝]</span>&nbsp; <span>真实姓名：<%= item.RealName %></span>&nbsp; <span>支付宝账户：<%= item.ALiPayAccount %></span>
                <% }
                   else if (item.ReceiveType == 1)
                   { %>
                <span>[银行卡]</span>&nbsp; <span>开户城市：<%= item.CityName %></span>&nbsp; <span>开户银行：<%= item.BankName %></span>&nbsp;
                <span>开户支行：<%= item.PubBankName %></span>&nbsp; <span>真实姓名：<%= item.RealName %></span>&nbsp;
                <span>银行账户：<%= item.BankAccount %></span>
                <% } %>
            </td>
            <td>
                <% if (item.Status == 0)
                   { %>
                <a href="javascript:void(0);" data-url="<%= Url.SiteUrl().HandleWithdrawCash(item.Id, 1, item.UserId, item.Price, item.Status, item.UserType) %>"
                    class="accepted_button">审批通过</a> <a href="javascript:void(0);" data-url="<%= Url.SiteUrl().HandleWithdrawCash(item.Id, 2, item.UserId, item.Price, item.Status, item.UserType) %>"
                        class="accepted_button">审批未通过</a>
                <% }
                   else if (item.Status == 1)
                   { %>
                <a href="javascript:void(0);" data-url="<%= Url.SiteUrl().HandleWithdrawCash(item.Id, 3, item.UserId, item.Price, item.Status, item.UserType) %>"
                    class="accepted_button">打款成功</a> <a href="javascript:void(0);" data-url="<%= Url.SiteUrl().HandleWithdrawCash(item.Id, 4, item.UserId, item.Price, item.Status, item.UserType) %>"
                        class="accepted_button">打款失败</a>
                <% }
                   else if (item.Status == 4)
                   { %>
                <a href="javascript:void(0);" data-url="<%= Url.SiteUrl().HandleWithdrawCash(item.Id, 3, item.UserId, item.Price, item.Status, item.UserType) %>"
                    class="accepted_button">打款成功</a>
                <% } %>
            </td>
        </tr>
        <% }
           }
           else
           { %>
        <tr>
            <td colspan="7">
                暂无数据！
            </td>
        </tr>
        <% } %>
    </tbody>
</table>
<script type="text/javascript">
    $(document).ready(function () {
        $(".accepted_button").bind("click", function () {
            var url = $(this).attr("data-url");
            if (url) {
                if (confirm("确定执行当前操作？")) {
                    $.post(url, { adminID: '', adminName: "" }, function (response) {
                        if (response.result) {
                            $("#searchForm").submit();
                            //window.location.reload();
                        } else {
                            alert(response.message);
                        }
                    });
                }
            }
        });
    });
</script>
