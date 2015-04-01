<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Developer>>" %>

 <table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="15%">
                    公司名称
                </th>
                <th width="8%">
                    公司类型
                </th>
                <th width="12%">
                    所在城市
                </th>
                <th width="10%">
                    联系人
                </th>
                 <th width="15%">
                    手机号
                </th> 
                <th width="15%">
                    邮箱
                </th>
                <th width="25%" class="lasted">
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
                    <%=item.Name %>
                    </td>
                    <td>
                    <%=item.TypeStr %>
                    </td>
                    <td>
                    <%=item.ProvinceName %>&nbsp;&nbsp;<%=item.CityName %>
                    </td>
                    <td>
                    <%=item.RealName %>
                    </td>
                    <td>
                    <%=item.Mobile %>
                    </td>
                    <td>
                    <%=item.Email %>
                    </td>
                    <td>
                    <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().Developers_Handle(item.Id) %>')">审核</a>
                    </td>
                </tr>
                <%}%>
            <%}else
            {%>
            <tr>
                <td colspan="7">
                    暂无数据！
                </td>
            </tr>
            <%}%>
        </tbody>
    </table>
    <script type="text/javascript">
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