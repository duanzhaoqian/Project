<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<List<TXModel.AdminPVM.PVM_NH_Discount_UserReport>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a></span>
    </div>
    <div class="data">
        <div class="filterBar">
            <% var premis = ViewData["Premis"] as TXOrm.Premis;  %>
            筛选：<select disabled="disabled"><option value="<%= premis == null ? "0" : "" + premis.Id %>">
                <%= premis == null ? "" : premis.Name %></option>
            </select>
            <select id="buildingId">
                <option value="0">选择楼栋</option>
                <%
                    var buildings = ViewData["Buildings"] as List<SelectListItem>;
                    if (null != buildings)
                    {
                        foreach (var item in ViewData["Buildings"] as List<SelectListItem>)
                        { %>
                        <option value="<%= item.Value %>" <%= (Convert.ToInt32(item.Value) == Convert.ToInt32(Request.QueryString["buildingid"]) ? "selected=\"selected\"" : "") %>>
                    <%= item.Text %></option>
                        <%
                        }
                    }
                        %>
            </select>
            <select id="unitId">
                <option value="0">选择单元</option>
            </select>
            <input type="button" id="btn_search" value="搜索" />
            <div class="icon_buttonA dingwei">
                <a style="color: Blue;" href="<%= Url.SiteUrl().Discount_Search("ExportExcel") %>?id=<%= Request.Params["id"] %>&buildingid=<%= Request.Params["buildingid"] %>&unitid=<%=Request.Params["unitid"] %>">
                    导出此列表</a>
            </div>
        </div>
        <div class="clearFix">
            <table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th width="7%">
                            序号
                        </th>
                        <th width="8%">
                            出价人
                        </th>
                        <th width="10%">
                            真实姓名
                        </th>
                        <th width="15%">
                            身份证号
                        </th>
                        <th width="10%">
                            手机号
                        </th>
                        <th width="10%">
                            QQ号码
                        </th>
                        <th width="10%">
                            E-mail
                        </th>
                        <th width="15%">
                            出价的房源
                        </th>
                        <th width="15%" class="lasted">
                            出价时间
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
                            <%= item.HouseInfo %>
                        </td>
                        <td>
                            <%= item.BidTimeString %>
                        </td>
                    </tr>
                    <%
                           }
                       }
                       else
                       { %>
                    <tr>
                        <td colspan="9">
                            暂无数据！
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>
    <script type="text/javascript">

        $(function() {
            $("#btn_search").bind("click", userreport.search);
            $("#buildingId").bind("change", userreport.evt_changed_unit);
            userreport.evt_changed_unit();
        });

        var userreport = {
            search: function() {
                var url = '<%= Url.SiteUrl().Discount_Search("userindex") %>';
                url += '?id=<%= Request.Params["id"] %>';
                url += "&buildingid=" + $("#buildingId").val();
                url += "&unitid=" + $("#unitId").val();
                url += "&ram=" + Math.random();
                window.location.href = url;
            },

            evt_changed_unit: function () {
                clearListItems($("#unitId"));
                if (0 == $("#buildingId").val()) {
                    return;
                }
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("GetUnitsByBuildingId") %>?buildingId=' + $("#buildingId").val(), $("#unitId"), function() {
                    var selId = '<%= Request.Params["unitid"] %>';
                    $("#unitId").val(selId);
                });
            }
        };

    </script>
</asp:Content>