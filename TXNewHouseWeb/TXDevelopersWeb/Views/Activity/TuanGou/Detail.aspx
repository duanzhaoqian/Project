<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="TXModel" %>
<%@ Import Namespace="TXOrm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    营销中心-阶梯团购详情
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        TXOrm.Activity activity = ViewData["Activity"] as TXOrm.Activity;
        List<TXModel.CT_ActivitiesHouse> actHouses = ViewData["actHouses"] as List<TXModel.CT_ActivitiesHouse>;
        List<TXOrm.Social> socials = ViewData["social"] as List<TXOrm.Social>;

        activity = activity ?? new Activity();
        actHouses = actHouses ?? new List<CT_ActivitiesHouse>();
        socials = socials ?? new List<Social>();
    %>
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <h4 class="title_h4 mb10">
            <span>活动名称-阶梯团购</span><em class="ts_right"><a href="<%=TXCommons.GetConfig.BaseUrl%>Activity/TuanGouList">返回阶梯团购列表</a></em></h4>
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
                        选择楼盘：
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
                    <th scope="row" width="100" align="right" valign="top">
                        阶梯团购条件：
                    </th>
                    <td class="col333">
                        <%
                            foreach (var item in socials)
                            {
                        %>
                        <div class="mb10">
                            当<input type="text" class="input_bgef" value="<%=item.UserCount %>" disabled="disabled">人，团购价格为<input
                                type="text" class="input_bgef" value="<%=item.Discount %>" disabled="disabled">折</div>
                        <%
                            }
                        %>
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
                房源</div>
        </div>
        <div class="mt15 clearFix" style="position: relative;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab1">
                <tbody>
                    <tr>
                        <th width="6%">
                            序号
                        </th>
                        <th width="5%">
                            楼层
                        </th>
                        <th width="7%">
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
                        <th width="12%">
                            单价
                        </th>
                        <th width="10%">
                            总价
                        </th>
                        <th width="6%">
                            销售状态
                        </th>
                        <th width="13%">
                            发布日期
                        </th>
                    </tr>
                    <%
                        foreach (var item in actHouses)
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
                            <%=item.SinglePrice.ToString("#.##") %>元/平方米
                        </td>
                        <td>
                            <%=item.TotalPrice.ToString("#.##") %>万元/套
                        </td>
                        <td>
                            <%=TXCommons.NewHouseWeb.HouseType.GetSalesStatus(item.SalesStatus) %>
                        </td>
                        <td>
                            <%=activity.CreateTime %>
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
