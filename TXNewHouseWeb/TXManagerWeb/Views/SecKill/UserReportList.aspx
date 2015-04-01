<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<List<TXModel.AdminPVM.PVM_NH_SecKill_UserReport>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="data">
        <div class="filterBar">
            <%= ViewData["HouseInfo"] %>
            <div class="icon_buttonA dingwei">
                <a style="color: Blue;" href="<%= Url.SiteUrl().SecKill_Search("ExportExcel") %>?activeId=<%= ViewData["ActivityId"] %>">
                    导出此列表</a>
            </div>
        </div>
        <div class="clearFix">
            <table class="DataTableA" border="0" cellpadding="0" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th width="5%">
                        </th>
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
                        <th width="20%">
                            E-mail
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
                            <span class="yellow_bjtb">成交</span>
                        </td>
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
                            <%= item.BidTimeString %>
                        </td>
                    </tr>
                    <% }
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
</asp:Content>