<%@ Control Language="C#" Inherits="WebViewUserControl<List<TXModel.AdminPVM.PVM_NH_YaoHaoApply>>" %>


 <table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th width="10%">
                    公司类型
                </th>
                <th width="10%">
                    公司名称
                </th>
                <th width="10%">
                    联系人
                </th>
                <th width="10%">
                    联系电话
                </th>
                 <th width="10%">
                     楼盘
                </th> 
                <th width="10%">
                    楼栋
                </th>
                <th width="10%">
                    标记状态
                </th> 
                <th width="12%">
                    申请时间
                </th>
                <th width="18%" class="lasted">
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
                    <%=item.CompanyTypeStr %>
                    </td>
                    <td>
                    <%=item.CompanyName %>
                    </td>
                    <td>
                    <%=item.UserName %>
                    </td>
                    <td>
                    <%=item.UserMobile %>
                    </td>
                    <td>
                    <%=item.PremisesName %>
                    </td>
                    <td>
                        <% if (item.IsAllPremises)
                           { %>
                           全部
                        <% }
                           else
                           { %>
                                <%= item.BuildingIds %>
                            <% } %>
                    </td>
                    <td>
                    <%=item.StateStr %>
                    </td>
                    <td>
                    <%=item.CreateTime %>
                    </td>
                    <td>
                        <% var state = item.State; %>
                        <% if (state == 1)
                           { %>
                        <a href="<%=Url.SiteUrl().AddYaoHao(item.Id)%>" >发布摇号活动</a>
                        <% }
                           else if(state!=5)
                           { %>
                           <a href="javascript:;" onclick="yaohaomanagerjs.ModifyState('<%=item.Id %>')" >标记状态</a>
                        <% } %>
                    </td>
                </tr>
                <% }%>
            <%}else
            {%>
            <tr>
                <td colspan="9">
                    暂无数据！
                </td>
            </tr>
            <%}%>
        </tbody>
    </table>
    <script type="text/javascript">
        var yaohaomanagerjs = {
            ModifyState: function(id) {
                var htmls = [];
                htmls.push('<table width="100%" border="0" cellspacing="0" cellpadding="0"><tbody>');
                htmls.push('<tr class=""><td width="100%" align="center"><p id="textMsg" style="display:none; height:22px;padding-left:30px;" ><span style="display:block; height:22px; text-align:left;" class="lose ml10">请选择状态！</span></p></td></tr>');
                htmls.push('<tr class=""><td width="100%" align="center"><select class = "w100" id="modifstate" >' + $("#State").html() + '</select></td></tr>');
                htmls.push('</tbody></table>');
                $.freeDialog.open({
                    width: 205,
                    content: htmls.join(''),
                    isHidden: false,
                    buttons: [{
                            text: '确定',
                            onclick: function(item, dialog) {
                                var state = $("#modifstate").val();
                                if (state == -1) {
                                    $("#textMsg").show();
                                } else {
                                    $.post("<%=Url.SiteUrl().Common("UpdateYaoHaoState","YaoHao")%>", { id: id, state: state, adminId: "<%=CurrentUser.Id %>", adminName: "<%=CurrentUser.Name %>",r:Math.random() }, function(response) {
                                        if (response.result) {
                                            GoToNewPageForSend();
                                        } else {
                                            alert(response.message);
                                        }
                                    });
                                }
                            }
                        }],
                    Title: "标记状态",
                    showTitle: false
                });
                $("#modifstate").find("option:last-child").remove();
            },
            GoToNewPage:function(path) {// 处理backUrl
                path = UrlPathHelper.filterUrlParamsRepeat(path);
                var cpath = UrlPathHelper.trimEndSharp(window.location.href);
                if (opts.current_page != undefined) {
                    cpath = UrlPathHelper.appendParams(cpath, "pageindex=" + opts.current_page);
                } else {
                    cpath = UrlPathHelper.appendParams(cpath, "pageindex=0");
                }
                window.location.href = UrlPathHelper.appendParams(path, "backurl=" + encodeURIComponent(cpath));
            }
        };
    </script>