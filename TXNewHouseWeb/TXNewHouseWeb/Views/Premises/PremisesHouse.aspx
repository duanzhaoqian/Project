<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--新房-楼盘-房源--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .fang_left .list ul li.dd
        {
            padding: 0;
        }
    </style>
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">
                    <%=ViewData["cityName"]%>新房</a> >
                <%-- <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"
                    class="blue">楼盘主页</a>>--%>
                <%=Model.Name%>
            </div>
        </div>
        <%{
              if (("" != ViewData["ADPictureUrl"]) && ("" != ViewData["DeveloperLOGOUrl"]))
              {
        %>
        <div class="r_title r_title1">
            <span class="img">
                <img width="140" height="140" alt="" src="<%=ViewData["DeveloperLOGOUrl"]%>.140_140.jpg" />
            </span><span class="font24 fontYahei">
                <%=Model.Name%></span><span class="r_bjtb1"><%=TXCommons.NewHouseWeb.PremisesType.GetSalesStatus(Convert.ToInt32(Model.SalesStatus))%></span>
            <%----推荐 Begin----%>
            <%if ("1".Equals(Convert.ToString(ViewData["IsRecommend"])))
              { %>
            <i class="tuj_i"></i>
            <%} %>
            <%----推荐 End----%>
            <span class="font14 col999">[<%=Model.DName%>] 排名<%=ViewData["Rank"] %>
                浏览<%=ViewData["ViewCount"]??"0"%>次</span><%--<span class="right"><a href=""><img src="<%=TXCommons.GetConfig.ImgUrl%>images/yaohao.gif" /></a></span>--%><!--摇号-->
        </div>
        <div class="ggt">
            <%--  <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>">--%>
            <img width="1190" height="200" alt="" src="<%=ViewData["ADPictureUrl"]%>.1190_200.jpg" />
        </div>
        <%
              }
              else
              { %>
        <div class="r_title">
            <span class="font24 fontYahei">
                <%=Model.Name%></span><span class="r_bjtb1"><%=TXCommons.NewHouseWeb.PremisesType.GetSalesStatus(Convert.ToInt32(Model.SalesStatus))%></span>
            <%----推荐 Begin----%>
            <%if ("1".Equals(Convert.ToString(ViewData["IsRecommend"])))
              { %>
            <i class="tuj_i"></i>
            <%} %>
            <%----推荐 End----%>
            <span class="font14 col999">[<%=Model.DName%>] 排名<%=ViewData["Rank"] %>
                浏览<%=ViewData["ViewCount"]??"0"%>次</span><%--<span class="right"><a href=""><img src="<%=TXCommons.GetConfig.ImgUrl%>images/yaohao.gif" /></a></span>--%><!--摇号-->
        </div>
        <% }
          }%>
        <div class="user_nav">
            <div class="w1190">
                <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"><i class="r1"></i>
                </a><a href="<%=NHWebUrl.PremisesDetailUrl(Model.Id.ToString()) %>"><i class="r2"></i>
                </a><a class="current" href="<%=NHWebUrl.PremisesHouseUrl(Model.Id.ToString(),"","","") %>">
                    <i class="r3"></i></a><a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"") %>">
                        <i class="r4"></i></a><a href="<%=NHWebUrl.AlbumUrl("",Model.Id.ToString(),"") %>"><i
                            class="r5"></i></a><a href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>">
                                <i class="r6"></i></a>
            </div>
        </div>
        <div class="conmain clearFix">
            <%if (Util.ToInt(ViewData["ldCount"]) == 0)
              { %>
            <div class="noResult_box tac mt35">
                <span class="icons01">暂无数据</span>
            </div>
            <%}
              else
              { %>
            <div class="fang_left">
                <div class="r_title_lp">
                    <strong class="title_span">楼座列表</strong></div>
                <div class="list">
                    <ul class="mt15">
                        <%=ViewData["html"]%>
                        <%--<li id="li12" onclick="toggleVendor('12')" class="down"><a href="#">01栋（100户）</a></li>
                        <li id="ul12" class="dd" style="display: none;">
                            <ul>
                                <li id="lisub120001" onclick="toggleVendor('sub120001')" class="down"><a href="#">1单元（40户）</a></li>
                                <li id="ulsub120001" class="dd" style="display: none;">
                                    <ul>
                                        <li><a href="javascript:void(0);">1层（8户）</a></li>
                                        <li><a href="javascript:void(0);">2层（8户）</a></li>
                                        <li class="cur"><a href="javascript:void(0);">3层（8户）</a></li>
                                        <li><a href="javascript:void(0);">4层（8户）</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="down"><a href="#">02栋（100户）</a></li>
                        <li style="display: none;">
                            <ul class="dd">
                                <li class="down"><a href="#">1单元（40户）</a></li>
                                <li  style="display: none;">
                                    <ul class="dd">
                                        <li><a href="javascript:void(0);">1层（8户）</a></li>
                                        <li><a href="javascript:void(0);">2层（8户）</a></li>
                                        <li class="cur"><a href="javascript:void(0);">3层（8户）</a></li>
                                        <li><a href="javascript:void(0);">4层（8户）</a></li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li class="down"><a href="#">03栋（100户）</a></li>
                        <li class="down"><a href="#">04栋（100户）</a></li>--%>
                    </ul>
                </div>
            </div>
            <!-- end left -->
            <div class="fang_right">
                <div id="divtop">
                </div>
                <div id="divright">
                </div>
            </div>
            <%} %>
            <div class="clear">
            </div>
            <div class="mt10 mb10">
                <%--  <img src="../images/icons/newhouse/r_ywlc.gif">--%>
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/r_ywlc.gif" />
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        function toggleVendor(id) {
            if ($("#li" + id).is('.down')) {
                if ($("#ul" + id).find('li').length > 0)
                    $("#ul" + id).show();
                $("#li" + id).addClass("up");
                $("#li" + id).removeClass("down");
            }
            else {
                $("#ul" + id).hide();
                $("#li" + id).removeClass("up");
                $("#li" + id).addClass("down");
            }
        }
        $(function () {
            //        alert("toggleVendorid:" + toggleVendorid);
            //        alert("toggleVendorsubid:" + toggleVendorsubid);
            //        alert("toggleVendorfloor:" + toggleVendorfloor);
            var toggleVendorid = '<%=ViewData["toggleVendorid"] %>';
            var toggleVendorsubid = '<%=ViewData["toggleVendorsubid"] %>';
            var toggleVendorfloor = '<%=ViewData["toggleVendorfloor"] %>';
            if ($.trim(toggleVendorid) == '') {

            } else {
                toggleVendor(toggleVendorid);
            }
            //        alert(toggleVendorsubid);
            if ($.trim(toggleVendorsubid) == '') {

            } else {
                toggleVendor(toggleVendorsubid);
            }
            //楼层
            if ($.trim(toggleVendorfloor) == '') {

            } else {
                $("#" + toggleVendorfloor).addClass("cur");
            }
            $(".list ul ul ul li").click(function () {
                $(this).siblings("li").removeClass("cur");
                $(this).addClass("cur");
            });
        })
        function toggleVendorhref(premisesId, buildingId, unitId, floor) {
            alert("楼盘ID：" + premisesId + " 楼栋id： " + buildingId + " 单元id：" + unitId + " 楼层：" + floor);
        }
        function PremisesHouseTopInfo(id, buildingid, unitId, floor) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/PremisesHouseTopInfo",
                data: { id: id, buildingid: buildingid, unitId: unitId, floor: floor, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                    alert(result);
                    $("#divtop").html(result);

                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function getPremisesHouseRightInfo(id, buildingid, unitId, floor) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/PremisesHouseRightInfo",
                data: { id: id, buildingid: buildingid, unitId: unitId, floor: floor, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                    alert(result);
                    $("#divright").html(result);

                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        //异步加载右侧顶部信息
        PremisesHouseTopInfo('<%=Model.Id%>', '<%=ViewData["ibuildingidViewDatatemp"]%>', '<%=ViewData["iunitIdViewDatatemp"]%>', '<%=ViewData["ifloorViewDatatemp"]%>');
        //异步加载右侧底部信息
        getPremisesHouseRightInfo('<%=Model.Id%>', '<%=ViewData["ibuildingidViewDatatemp"]%>', '<%=ViewData["iunitIdViewDatatemp"]%>', '<%=ViewData["ifloorViewDatatemp"]%>');
    </script>
</asp:Content>
