<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_YaoHao>>" %>


 <table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%">
                    活动名称
                </th>
                <th width="10%">
                摇号的楼盘
                </th>
                <th width="10%">
                    摇号的楼栋
                </th>
                <th width="8%">
                    摇号公正
                </th>
                <th width="12%">
                    报名时间
                </th> 
                 <th width="12%">
                    活动时间
                </th> 
                <th width="7%">
                    选房时间
                </th>
                <th width="8%">
                活动所在城市
                </th> 
                <th width="6%">
                    活动状态
                </th>
                <th width="20%" class="lasted">
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
                    <%=item.PremisesName %>
                    </td>
                    <td>
                    <%=item.BuildingIds%>
                    </td>
                     <td>
                    <%=item.NotarialOffice%>
                    </td>
                    <td>
                    <%=item.SignupBeginTime%><br /><%=item.SignupEndTime%>
                    </td>
                    <td>
                    <%=item.BeginTime %><br /><%=item.EndTime %>
                    </td>
                     <td>
                    <%=item.ChooseHouseTime%>
                    </td>
                    <td>
                    <%=item.CityName %>
                    </td>
                    <td>
                    <%=item.ActivitieStateStr %>
                    </td>
                    <td>
                        <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().ModifyYaoHao(item.Id) %>')">修改</a>
                        <a href="javascript:;" class="accepted_button" data-url="<%=Url.SiteUrl().DelYaoHao(item.Id) %>" >删除</a>
                        <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().YaoHaoUsersIndex(item.Id,item.PremisesId) %>')">查看报名用户</a>
                        <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().InfoYaoHao(item.Id) %>')">详情</a>
                  <%--  <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().Developers_Handle(item.Id) %>')">审核</a>
--%>                    
                    </td>
                </tr>
                <%}%>
            <%}else
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
                        $.post(url, { adminId: "<%=CurrentUser.Id %>", adminName: "<%=CurrentUser.Name %>" }, function (response) {
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