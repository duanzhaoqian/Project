<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_BidingUsers>>" %>

  <table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="5%">
                
            </th>
            <th width="5%">
                序号
            </th>
            <th width="8%">
                出价人 
            </th>
            <th width="8%">
                真实姓名 
            </th>
            <th width="10%">
                身份证号 
            </th>
            <th width="8%">
                手机号  
            </th>
            <th width="8%">
                QQ号码 
            </th>
            <th width="8%">
                E-mail 
            </th>
            <th width="12%">
                最后出价时间 
            </th>
            <th width="8%">
                出价次数
            </th>
            <th width="10%">
                最后出价金额
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
                 <%--<% if (item.Status == 2)
                   {
                %>
                <span class="yellow_bjtb">成交</span>
                <% } %>--%>
                 <%--<% var models =int.Parse(ViewData["models"].ToString());  %>
                 <% if (models == item.Id)
                    { %>
                        <span class="yellow_bjtb">中标</span>
                     <% } %>--%>
                     <% if (2 == item.Status)
                        {
                     %>
                        <span class="yellow_bjtb">成交</span>
                    <%
                        }
                        else if (1 == item.Status && 0 == item.HaveWinCount)
                        {
                    %>
                    <span class="yellow_bjtb">中标</span>
                    <% } %>
            </td>
            <td>
                <%= item.Id %>
            </td>
            <td>
                <%= item.BidUserName %>
            </td>
            <td>
                <%= item.BidRealName %>
            </td>
            <td>
                <%= item.IDCard %> 
            </td>
            <td>
                <%= item.BidUserMobile %>
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
            <td>
                <%= item.BidCount %>
            </td>
            <td>
                <%= item.BidUserPrice %>
            </td>
            <td>
                 <% if (item.HaveWinCount > 0)
                    { %>
                <% if (item.Status == 2)
                   { %>
                     
                <a href="javascript:;" data-url="<%= Url.SiteUrl().Common("UpdateStatus", "BargainActive") %>?id=<%= item.Id %>&status=0"  class="accepted_button">取消成交</a>
                    <%--<em style="color:Grey;">已中标</em>--%>
                <% }
                   else
                   { %>
                --
                <% } %>
                <% }
                    else
                    { %>
                <a href="javascript:;" data-url="<%= Url.SiteUrl().Common("UpdateStatus", "BargainActive") %>?id=<%= item.Id %>&status=2"
                    class="accepted_button">成交</a>
                <% } %>
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
<script type="text/javascript">
    $(document).ready(function () {
        $(".accepted_button").bind("click", function () {
            if (IsActivityClose != "True") {
                alert("活动尚未结束，不能标记成交");
                return;
            }

            var url = $(this).attr("data-url");
            if (url) {
                if (confirm("确定执行当前操作？")) {
                    $.post(url, { adminId: "<%=CurrentUser.Id %>", adminName: "<%=CurrentUser.Name %>", r: Math.random() }, function (response) {
                        if (response.result) {
                            GoToNewPageForSend();
                        } else {
                            alert(response.message);
                        }
                    });
                }
            }
        });
    });

    // 刷新本页
    function GoToNewPageForSend() {
        var path = UrlPathHelper.trimEndSharp(window.location.href);
        path = UrlPathHelper.appendParams(path, "pageindex=" + opts.current_page);
        window.location.href = path;
    }
</script>
