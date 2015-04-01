<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Biding>>" %>
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
            <th width="6%">
                所属楼盘
            </th>
            <th width="5%">
                开发商
            </th>
            <th width="5%">
                总价
            </th>
            <th width="6%">
                起拍价格
            </th>
            <th width="7%">
                加价幅度
            </th>
            <th width="7%">
                最大幅度
            </th>
            <th width="12%">
                时间
            </th>
            <th width="7%">
                剩余时间
            </th>
            <th width="5%">
                活动所在城市
            </th>
            <th width="5%">
                状态
            </th>
            <th width="15%" class="lasted">
                操作
            </th>
        </tr>
    </thead>
    <tbody class="test">
        <%if (Model != null && Model.Count > 0)
          { %>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= item.HouseName %>
            </td>
            <td>
                <%= item.Floor %>层
            </td>
            <td>
                <%= item.UnitName %>
            </td>
            <td>
                <%= item.BuildingName %>
            </td>
            <td>
                <%= item.PremisesName %>
            </td>
            <td>
                <!--<%= item.DeveloperId %>-->
                <%= (string.IsNullOrEmpty(item.Developer) ? item.Agent : item.Developer) %>
            </td>
            <td>
                <%= Convert.ToDouble(item.TotalPrice) %>万元/套
            </td>
            <td>
                <%= Convert.ToDouble(item.BidPrice) %>万元/套
            </td>
            <td>
                必须为<%= Convert.ToDouble(item.AddPrice) %>万元的整数倍
            </td>
            <td>
                单次加价不高于<%= Convert.ToDouble(item.MaxPrice) %>万元
            </td>
            <td>
                <%= item.BeginTimeStr %>---<%= item.EndTimeStr %>
            </td>
            <td>
                <% if (item.TimeFlag == 0)
                   { %>
                --
                <% }
                   else if (item.TimeFlag == 1)
                   { %>
                <%= item.SurplusTimeString %>
                <% }
                   else if (item.TimeFlag == 2)
                   { %>
                --
                <% } %>
            </td>
            <td>
                <%=item.CityName %>
            </td>
            <td>
                <%=item.ActivitieStateStr%>
            </td>
            <td>
                <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().NhBidingModify(item.ActivitiesId) %>')">
                    编辑</a>&nbsp;&nbsp; <a href="javascript:;" class="accepted_button" data-url="<%=Url.SiteUrl().DelNhBiding(item.ActivitiesId) %>">
                        删除</a>&nbsp;&nbsp; <a href="javascript:;" onclick="GoToNewPage('<%=Url.SiteUrl().NhBidingUsersIndex(item.ActivitiesId,item.PremisesId,item.HouseId) %>')">
                            查看用户报名</a>
            </td>
        </tr>
        <%}%>
        <%}
          else
          {%>
        <tr>
            <td colspan="15">
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
        // window.location.href = UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath));
        window.open(UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath)));
    }
</script>