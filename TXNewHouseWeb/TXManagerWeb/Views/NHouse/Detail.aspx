<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVE_NH_House_Detail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>-详情
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%= AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%= AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%= AdminPageInfo.ItemName %></a><i>&gt;</i> <a href="javascript:void(0);">详情</a></span>
    </div>
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            楼盘名称：
                        </th>
                        <td width="86%">
                            <%= Model.PremisesName %>
                        </td>
                    </tr>
                    <tr>
                        <th width="14%">
                            楼栋名称：
                        </th>
                        <td width="86%">
                            <%= Model.BuildingName %>
                        </td>
                    </tr>
                </table>
                <div class="dispose">
                    <h5>
                        房源信息</h5>
                </div>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <tr>
                        <th width="14%">
                            房号：
                        </th>
                        <td width="86%">
                            <%= Model.HouseName %>
                        </td>
                    </tr>
                    <tr>
                        <th width="14%">
                            单元：
                        </th>
                        <td width="86%">
                            <%= Model.UnitName %>
                        </td>
                    </tr>
                    <tr>
                        <th width="14%">
                            所在楼层：
                        </th>
                        <td width="86%">
                            <%= Model.FloorName %>
                        </td>
                    </tr>
                    <tr>
                        <th width="14%">
                            户型：
                        </th>
                        <td width="86%">
                            <%= Model.HouseTypeStructure %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            建筑形式：
                        </th>
                        <td>
                            <%= Model.BuildingTypeName %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            采光情况：
                        </th>
                        <td>
                            <%= Model.OrientationName %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <%= Model.PriceTypeName %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            总价：
                        </th>
                        <td>
                            <%= Model.TotalPriceString %>万元/套
                        </td>
                    </tr>
                    <tr>
                        <th>
                            单价：
                        </th>
                        <td>
                            <%= Model.SinglePriceString %>万元/平方米
                        </td>
                    </tr>
                    <tr>
                        <th>
                            首期付款：
                        </th>
                        <td>
                            <%= Model.DownPaymentString %>万元
                        </td>
                    </tr>
                    <tr>
                        <th>
                            销售状态：
                        </th>
                        <td>
                            <%= Model.SalesStatusName %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            建筑面积：
                        </th>
                        <td>
                            <%= Model.BuildingAreaString %>平方米
                        </td>
                    </tr>
                    <tr>
                        <th>
                            使用面积：
                        </th>
                        <td>
                            <%= Model.AreaString %>平方米
                        </td>
                    </tr>
                    <tr>
                        <th>
                            户型图：
                        </th>
                        <td>
                            已选编号：<%= Model.RId %>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            房间位置标注：
                        </th>
                        <td>
                            坐标：<%= Model.CoordX %>,
                            <%= Model.CoordY %>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <!--//data-->
</asp:Content>