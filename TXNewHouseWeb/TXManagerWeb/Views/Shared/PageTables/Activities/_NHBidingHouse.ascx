<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_Biding_House>>" %>
<table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%">
                    房号
                </th>
                <th width="5%">
                    楼层
                </th>
                <th width="8%">
                    单元
                </th>
                <th width="10%">
                    楼号
                </th>
                <th width="10%">
                    户型
                </th>
                 <th width="8%">
                    建筑面积
                </th> 
                <th width="10%">
                    单价
                </th>
                <th width="10%">
                    总价
                </th>
                <th width="8%">
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
                    <td>
                    <%=item.Name %>
                    </td>
                    <td>
                    <%=item.Floor %>层
                    </td>
                     <td>
                    <%=item.UnitName %>
                    </td>
                    <td>
                    <%=item.BuildingName %>
                    </td>
                    <td>
                    <%=item.Room %>室<%=item.Hall %>厅<%=item.Toilet %>卫<%=item.Kitchen %>厨
                    </td>
                    <td>
                    <%=item.BuildingArea %>平方米
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
                        <% if (ViewData["HouseId"] != null && Convert.ToInt32(ViewData["HouseId"]) == item.Id)
                           { %>
                     <a href="javascript:;" class="canjiahouse" flag="true" onclick="BidingModify.SetHouse(this,<%=item.Id %>)">不参加</a>
                     <% }
                           else
                           { %>
                    <a href="javascript:;" class="canjiahouse" flag="false" onclick="BidingModify.SetHouse(this,<%=item.Id %>)">参加</a>
                     <% } %>
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

        //alert(BidingModify.IsSearch);
        //列表加载完毕 禁用
        //if (BidingModify.IsSearch!="true")
            //    BidingModify.Disable_Er();
    </script>

