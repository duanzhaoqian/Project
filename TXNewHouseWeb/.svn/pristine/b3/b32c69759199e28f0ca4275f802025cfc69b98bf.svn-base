<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="System.Activities.Expressions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-限时折扣详情
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        TXOrm.Activity activity = ViewData["Activity"] as TXOrm.Activity;
        List<TXModel.CT_ActivitiesHouse> actHouses = ViewData["actHouses"] as List<TXModel.CT_ActivitiesHouse>;
    %>
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <h4 class="title_h4 mb10">
            <span>活动名称-限时折扣活动详情</span><em class="ts_right"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/DiscountList">返回限时折扣活动列表</a></em></h4>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                基本信息</div>
        </div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box1">
            <tbody>
                <tr>
                    <th scope="row" width="100" align="right">
                        活动名称：
                    </th>
                    <td class="col333">
                        <%=activity.Name %>
                    </td>
                </tr>
                <tr>
                    <th scope="row" width="130" align="right">
                        楼盘：
                    </th>
                    <td class="col333">
                        <%=actHouses[0].Premis %>
                    </td>
                </tr>
                <tr>
                    <th scope="row" width="100" align="right">
                        活动保证金金额：
                    </th>
                    <td class="col333">
                        <%=activity.Bond.ToString("#.##") %>元
                    </td>
                </tr>
                <tr>
                    <th scope="row" width="100" align="right">
                        时间：
                    </th>
                    <td class="col333">
                        <%=activity.BeginTime.ToString("yyyy-MM-dd") %>
                        -
                        <%=activity.EndTime.ToString("yyyy-MM-dd") %>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="mt15 btit clearFix">
            <div class="tit_font fl">
                限时折扣</div>
        </div>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
                <tbody>
                    <tr>
                        <th width="6%">
                            房号
                        </th>
                        <th width="6%">
                            楼层
                        </th>
                        <th width="6%">
                            单元
                        </th>
                        <th width="15%">
                            楼号
                        </th>
                        <th width="10%">
                            户型
                        </th>
                        <th width="10%">
                            建筑面积
                        </th>
                        <th width="10%">
                            总价
                        </th>
                        <th width="8%">
                            限时折扣
                        </th>
                        <th width="10%">
                            折后价格
                        </th>
                    </tr>
                    <% foreach (var item in actHouses)
                       {
                    %>
                    <tr>
                        <td>
                            <%=item.Name %>
                        </td>
                        <td>
                            <%=item.Floor%>
                        </td>
                        <td>
                            <%=item.Unit %>
                        </td>
                        <td>
                            <%=item.Building %>
                        </td>
                        <td>
                            <%=item.Room %>房<%=item.Hall %>厅<%=item.Toilet %>卫<%=item.Kitchen %>厨
                        </td>
                        <td>
                            <%=item.BuildingArea.ToString("#.##")%>平方米
                        </td>
                        <td>
                            <%=item.TotalPrice.ToString("#.##") %>万元/套
                        </td>
                        <td>
                            <input type="text" value="<%=item.Discount %>" class="input_bgf5" disabled="disabled">折
                        </td>
                        <td>
                            <%=((decimal)(item.TotalPrice*item.Discount/10)).ToString("#.##") %>万元/套
                        </td>
                    </tr>
                    <%
                       }
                        
                    %>
                </tbody>
            </table>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
