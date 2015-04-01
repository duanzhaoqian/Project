﻿<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_LadderBuy>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
    <thead>
        <tr>
            <th width="10%">
                活动名称
            </th>
            <th width="10%">
                楼盘
            </th>
            <th width="10%">
                开发商
            </th>
            <th width="15%">
                活动时间
            </th>
            <th width="10%">
                剩余时间
            </th>
            <th width="8%">
                房源数量
            </th>
            <th width="10%">
                活动所在城市
            </th>
            <th width="7%">
                状态
            </th>
            <th width="20%" class="lasted">
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
                <%= item.Name %>
            </td>
            <td>
                <%= item.PremisesName%>
            </td>
            <td>
                <%= item.DeveloperName %>
            </td>
            <td>
                <%= item.BeginTimeString %>—<%= item.EndTimeString %>
            </td>
            <td>
                <%= item.RestTimeString.Equals("0") ? "--" : item.RestTimeString %>
            </td>
            <td>
                <%= item.HouseCount %>
            </td>
            <td>
                <%= item.CityName %>
            </td>
            <td>
                <%= item.ActivitieStateString %>
            </td>
            <td>
                <a href="<%= Url.SiteUrl().LadderGroup_SearchResult("edittuangou") %>?id=<%= item.Id %>"
                    target="_blank">编辑</a>&nbsp;&nbsp; <a href="javascript:void(0);" onclick="tuangou.del('<%= item.Id %>')">删除</a>&nbsp;&nbsp;
                <a href="<%= Url.SiteUrl().LadderGroup_SearchResult("DetailMessage") %>?id=<%= item.Id %>">详情</a>&nbsp;&nbsp; <a href="<%=Url.SiteUrl().RegistrationUserIndex(item.Id,item.Type) %>">
                    查看用户报名</a>
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