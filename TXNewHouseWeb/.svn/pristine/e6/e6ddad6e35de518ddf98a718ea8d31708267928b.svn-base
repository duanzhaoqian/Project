<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Bargain>>" %>
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
            <th width="8%">
                所属开发商
            </th>
            <th width="7%">
                总价
            </th>
            <th width="8%">
                保底价
            </th>
            <th width="8%">
                加价幅度
            </th>
            <th width="8%">
                最大幅度
            </th>
            <th width="8%">
                时间
            </th>
            <th width="8%">
                剩余时间
            </th>
            <th width="9%">
                活动所在城市
            </th>
            <th width="7%">
                状态
            </th>
            <th width="9%" class="lasted">
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
                <%=item.HouseNum %>
            </td>
            <td>
                <%=item.Floor %>层
            </td>
            <td>
                <%=item.UnitName %>
            </td>
            <td>
                <%=item.FloorNum %><%=item.NameType %>
            </td>
            <td>
                <%=item.DeveloperName %>
            </td>
            <td>
                <%=Convert.ToDouble(item.TotalPrice) %>万元/套
            </td>
            <td>
                <%=Convert.ToDouble(item.BidPrice) %>万元/套
            </td>
            <td>
                必须为<%=Convert.ToDouble(item.AddPrice) %>万元的整数倍
            </td>
            <td>
                单次加价不高于<%=Convert.ToDouble(item.MaxPrice) %>万元
            </td>
            <td>
                <%=item.BeginTimeString %>--<%=item.EndTimeString %>
            </td>
            <td>
            <%if (item.ActivitieState != 0)
              {%>
                <%=item.SurplusTimeString.Equals("0") ? "--" : item.SurplusTimeString%>
                <%}
              else
              { %>
                --
              <%} %>
            </td>
            <td>
                <%=item.CityName %>
            </td>
            <td>
                <%=item.ActivitieStateString %>
            </td>
            <td>
                <a href="<%=Url.SiteUrl().UpdateBargain(item.Id) %>">编辑</a>&nbsp;&nbsp; 
                <a href="javascript:;" class="accepted_button" data-url="<%=Url.SiteUrl().ActionUrl("Delete","BargainActive") %>?id=<%=item.Id %>">删除</a>&nbsp;&nbsp;&nbsp;&nbsp;
                <a href="javascript:void(0);" onclick="GoToNewPage('<%=Url.SiteUrl().BargainUserIndex(item.Id,item.PremisesId,item.HouseId) %>')">查看用户报名</a>
            </td>
        </tr>
        <%}%>
        <%}
          else
          {%>
        <tr>
            <td colspan="14">
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
        window.open(UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath)));
    }
   </script>