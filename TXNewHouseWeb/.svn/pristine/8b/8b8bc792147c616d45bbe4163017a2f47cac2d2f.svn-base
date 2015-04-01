<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_House>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%">
                    房号
                </th>
                <th width="8%">
                    楼层
                </th>
                <th width="10%">
                    楼号
                </th>
                <th width="10%">
                    户型
                </th>
                 <th width="10%">
                    建筑面积
                </th> 
                <th width="10%">
                    单价
                </th>
                <th width="10%">
                    总价
                </th>
                <th width="10%">
                    销售状态
                </th>
                 <th width="12%">
                    发布日期
                </th>
                <th width="10%" class="lasted">
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
                    <td><input value="<%=item.Id %>" cityid_data="<%=item.CityId %>" type="checkbox" name="houseIds" />
                    <%=item.Name %>
                    </td>
                    <td>
                    <%=item.Floor %>层
                    </td>
                    <td>
                    <%=item.BuildingName %>
                    </td>
                    <td>
                    <%=item.Room %>室<%=item.Hall %>厅<%=item.Toilet %>卫<%=item.Kitchen %>厨
                    </td>
                    <td>
                    <%=item.BuildingArea %>
                    </td>
                    <td>
                    <%=Convert.ToDouble(item.SinglePrice) %>元/平方米
                    </td>
                    <td>
                    <%=Convert.ToDouble(item.TotalPrice) %>万元/套
                    </td>
                    <td>
                    <%=item.SalesStatusStr %>
                    </td>
                    <td>
                    <%=item.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") %>
                    </td>
                    <td>
                    <a href="javascript:;"onclick="GoToNewPage('<%=Url.SiteUrl().NHouseModify(item.Id) %>')" >编辑</a>&nbsp;&nbsp;
                    <a href="javascript:;" class="accepted_button" data-url="<%=Url.SiteUrl().DeleteHouseById(item.Id) %>">删除</a>&nbsp;&nbsp;
                    <a href="<%=Auxiliary.Instance.NhManagerUrl %>nhouse/detail.html?houseid=<%=item.Id %>&ran=<%=new Random().Next(9999,999999) %>" target="_blank" >详情</a>
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
                        $.post(url, { adminId: "<%=CurrentUser.Id %>",adminName: "<%=CurrentUser.Name %>" }, function (response) {
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
            window.location.href = UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath));
        }
    </script>

