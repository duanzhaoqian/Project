<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    在线支付
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="w1190 mb15">
            <%if (ViewData["msg"] != null)
              { 
            %>
            <div class="pay_suc brt2 clearFix">
                <div class="right">
                    <h5>
                        <%=ViewData["msg"]%>
                    </h5>
                </div>
            </div>
            <%} %>
            <!-- 秒杀、一口价 -->
            <% 
                TXOrm.Activity activity = ViewData["Activity"] as TXOrm.Activity;
                TXOrm.Order order = ViewData["Order"] as TXOrm.Order;
                TXModel.Web.PremisesHouse house = ViewData["House"] as TXModel.Web.PremisesHouse;

                if (activity != null && order != null && house != null)
                {
            %>
            <%if (activity.Type == 7 || activity.Type == 8)
              { %>
            <div class="pay_suc brt2 clearFix">
                <div class="sucimg">
                    &nbsp;</div>
                <div class="right">
                    <h5>
                        支付成功！</h5>
                    <p class="mt20">
                        恭喜您获得【<%=house.PremisesName%>
                        <%=house.BuildingName%>
                        <%=house.UnitName%>
                        <%=house.Floor%>1层
                        <%=house.HouseName%>号房】购房资格！</p>
                    <p>
                        1-2个工作日内，开发商会打电话与你联系进行线下交易。</p>
                    <p class="mt15">
                        <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(order.HouseId.ToString())%>"
                            class="btn_w90_yellow" style="display: block;">返回房源</a></p>
                </div>
            </div>
            <%}

              if (activity.Type == 5 || activity.Type == 6)
              { %>
            <!-- 竞价砍价 -->
            <div class="pay_suc brt2 clearFix">
                <div class="sucimg">
                    &nbsp;</div>
                <div class="right">
                    <h5>
                        支付成功！</h5>
                    <p class="mt20">
                        恭喜您，已对【<%=house.PremisesName%>
                        <%=house.BuildingName%>
                        <%=house.UnitName%>
                        <%=house.Floor%>1层
                        <%=house.HouseName%>号房】出价成功！</p>
                    <p>
                        您的本次出价金额：<strong class="coleb0000"><%=(double)activity.Bond%>元</strong></p>
                    <p class="col6aa700 font12">
                        温馨提示：</p>
                    <p class="font12">
                        开发商根据您的出价会与您取得电话联系，如果完成交易；</p>
                    <p class="font12">
                        您也可以继续出价，出价次数越多，成交几率越大；</p>
                    <p class="mt15">
                        <a href="<%=TXCommons.NHWebUrl.PremisesHouseInfoUrl(order.HouseId.ToString())%>"
                            class="btn_w90_yellow" style="display: block;">返回房源</a></p>
                </div>
            </div>
            <%}
                } %>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</asp:Content>
