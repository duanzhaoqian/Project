<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Developer>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="15%">
                帐号
            </th>
            <th width="10%">
                公司名称
            </th>
            <th width="10%">
                公司类型
            </th>
            <th width="12%">
                所在城市
            </th>
            <th width="8%">
                认证状态
            </th>
            <th width="7%">
                帐号状态
            </th>
            <th width="15%">
                注册时间
            </th>
            <th width="23%" class="lasted">
                操作
            </th>
        </tr>
    </thead>
    <tbody class="test">
        <%if (Model != null && Model.Count > 0)
          { %>
        <% foreach (var item in Model)
           {%>
        <tr>
            <td>
                <%=item.LoginName %>
            </td>
            <td>
                <%=item.Name %>
            </td>
            <td>
                <%=item.TypeStr %>
            </td>
            <td>
                <%=item.ProvinceName %>&nbsp;&nbsp;<%=item.CityName %>
            </td>
            <td>
                <%=item.StateStr %>
            </td>
            <td>
                <%=item.LockStateStr %>
            </td>
            <td>
                <%=item.CreateTime %>
            </td>
            <td>
                <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().DevelopersAccount_Handle(item.Id) %>')">
                    修改</a>&nbsp;&nbsp;
                <%
               if (item.LockState == 0)
               {%>
                <a href="javascript:void(0);" data-url="<%=Url.SiteUrl().SetLockState(item.Id,1) %>"
                    class="col16 accepted_button">锁定</a>&nbsp;&nbsp;
                <%}
               else
               {%>
                <a href="javascript:void(0);" data-url="<%=Url.SiteUrl().SetLockState(item.Id,0) %>"
                    class="col16 accepted_button">解锁</a>&nbsp;&nbsp;
                <%} 
                %>
                <% if (!item.IsRecommend)
                   { %>
                <a href="javascript:void(0);" data-url="<%=Url.SiteUrl().SetRecommend(item.Id,1) %>"
                    class="col16 recommend_button">推荐</a>&nbsp;&nbsp;
                <% }
                   else if (item.IsRecommend)
                   { %>
                <a href="javascript:void(0);" data-url="<%=Url.SiteUrl().SetRecommend(item.Id,0) %>"
                    class="col16 recommend_button">取消推荐</a>&nbsp;&nbsp;
                <% } %>
            </td>
        </tr>
        <%}%>
        <%}
          else
          {%>
        <tr>
            <td colspan="10">
                暂无数据！
            </td>
        </tr>
        <%}%>
    </tbody>
</table>
<script type="text/javascript">
    $(document).ready(function () {
        $(".accepted_button").bind("click", function () {
            var url = $(this).attr("data-url");
            if (url) {
                if (confirm("确定执行当前操作？")) {
                    $.post(url, {}, function (response) {
                        if (response.result) {
                            GoToNewPageForSend();
                        } else {
                            alert(response.message);
                        }
                    });
                }
            }
        });
        $(".recommend_button").bind("click", function () {
            var url = $(this).attr("data-url");
            if (url) {
                var text = $(this).html();
                var tipmsg = text.indexOf('取消') == -1 ? "确定设置为推荐开发商？" : "确定取消设置推荐开发商？";
                if (confirm(tipmsg)) {
                    $.post(url, {}, function (response) {
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
</script>
<script type="text/javascript">
    // 刷新本页
    function GoToNewPageForSend() {
        var path = UrlPathHelper.trimEndSharp(window.location.href);
        path = UrlPathHelper.appendParams(path, "pageindex=" + opts.current_page);
        window.location.href = path;
    }
    // 处理backUrl
    function GoToNewPage(path) {
        path = UrlPathHelper.filterUrlParamsRepeat(path);
        var cpath = UrlPathHelper.trimEndSharp(window.location.href);

        if (opts.current_page != undefined) {
            cpath = UrlPathHelper.appendParams(cpath, "pageindex=" + opts.current_page);
        } else {
            cpath = UrlPathHelper.appendParams(cpath, "pageindex=0");
        }
        window.location.href = UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath));
    }
</script>
