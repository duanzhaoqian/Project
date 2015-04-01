<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_Purchase_Detail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%= AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%= AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%= AdminPageInfo.ItemName %></a> <i>&gt;</i><a href="javascript:void(0);"><%= Model.Name %>限时折扣活动</a></span><span
                    style="float: right;"> <a href="<%= ViewData["backurl"] %>">返回排号购房活动列表</a>
        </span>
    </div>
    <div class="data">
        <div class="outer">
            <div class="dispose">
                <h5>
                    基本信息</h5>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="100">
                            活动名称：
                        </th>
                        <td>
                            <%= Model.Name %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            楼盘：
                        </th>
                        <td>
                            <%= Model.PremiseName %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            活动保证金金额：
                        </th>
                        <td>
                            <%= Model.BondString %>元
                        </td>
                    </tr>
                    <tr>
                        <th>
                            取前：
                        </th>
                        <td>
                            <%= Model.UserCount %> 名，为有效报名。
                        </td>
                    </tr>
                    <tr>
                        <th>
                            活动时间：
                        </th>
                        <td>
                            <%= Model.BeginTimeString %>
                            -
                            <%= Model.EndTimeString %>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="dispose">
                <h5>
                    房源</h5>
            </div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="DataTableA">
                    <thead>
                        <tr>
                            <th width="10%">
                                房号
                            </th>
                            <th width="10%">
                                楼层
                            </th>
                            <th width="10%">
                                单元
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
                            <th width="10%">
                                发布日期
                            </th>
                        </tr>
                    </thead>
                    <tbody class="test">
                        <% for (int i = 0; i < Model.Houses.Count; i++)
                           {
                               var house = Model.Houses[i];
                        %>
                        <tr>
                            <td>
                                <%= house.HouseName %>
                            </td>
                            <td>
                                <%= 0<house.Floor? string.Format("{0}层",house.Floor) : string.Format("地下{0}层",Math.Abs(house.Floor)) %>
                            </td>
                            <td>
                                <%= house.UnitName %>
                            </td>
                            <td>
                                <%= house.BuildingName %>
                            </td>
                            <td>
                                <%= string.Format("{0}室{1}厅{2}卫{3}厨",house.Room,house.Hall,house.Toilet,house.Kitchen) %>
                            </td>
                            <td>
                                <%= house.BuildingArea %>平方米
                            </td>
                            <td>
                                <%= string.Format("{0:F0}",house.SinglePriceString) %>元/平方米
                            </td>
                            <td>
                                <%= string.Format("{0:F0}",house.TotalPrice) %>万元/套
                            </td>
                            <td>
                                <%= house.SalesStatusString %>
                            </td>
                            <td>
                                <%= house.CreateTimeString %>
                            </td>
                        </tr>
                        <% } %>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>