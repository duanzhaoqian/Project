<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PremisesHouse>" %>

<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--房间信息页--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .r_bjtb1
        {
            background: url(<%=TXCommons.GetConfig.ImgUrl%>images/r_bjtb1.png) no-repeat;
            color: #fff;
            padding-left: 7px;
            font-size: 12px;
            width: 36px;
            height: 18px;
            line-height: 18px;
            display: inline-block;
            vertical-align: 3px;
            text-align: center;
        }
    </style>
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">北京新房</a> > <a href="<%=NHWebUrl.PremisesIndexUrl(Model.PremisesId.ToString()) %>"
                        class="blue">
                        <%=Model.PremisesName%></a> > <a href="<%=NHWebUrl.PremisesHouseUrl(Model.PremisesId.ToString(),"","","") %>"
                            class="blue">房源</a> > 房间信息
            </div>
        </div>
        <%{
              if (("" != ViewData["ADPictureUrl"]) && ("" != ViewData["DeveloperLOGOUrl"]))
              {
        %>
        <div class="r_title r_title1">
            <span class="img">
                <%--<a href="">--%>
                <img width="140" height="140" alt="" src="<%=ViewData["DeveloperLOGOUrl"]%>.140_140.jpg" /></span>
            <span class="font24 fontYahei">
                <%=Model.PremisesName%></span><span class="r_bjtb1"><%=TXCommons.NewHouseWeb.PremisesType.GetSalesStatus(Convert.ToInt32((ViewData["Premis"] as TXOrm.Premis).SalesStatus))%></span>
            <%----推荐 Begin----%>
            <%if ("1".Equals(Convert.ToString(ViewData["IsRecommend"])))
              { %>
            <i class="tuj_i"></i>
            <%} %>
            <%----推荐 End----%>
            <span class="font14 col999">[<%=ViewData["DName"]%>] 排名<%=ViewData["Rank"] %>
                浏览<%=ViewData["ViewCount"]??"0"%>次</span><%--<span class="right"><a href=""><img src="<%=TXCommons.GetConfig.ImgUrl%>images/yaohao.gif" /></a></span>--%><!--摇号-->
        </div>
        <div class="ggt">
            <%--  <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>">--%>
            <img width="1190" height="200" alt="" src="<%=ViewData["ADPictureUrl"]%>.1190_200.jpg" /></div>
        <%
              }
              else
              { %>
        <div class="r_title">
            <span class="font24 fontYahei">
                <%=Model.PremisesName%></span><span class="r_bjtb1"><%=TXCommons.NewHouseWeb.PremisesType.GetSalesStatus(Convert.ToInt32((ViewData["Premis"] as TXOrm.Premis).SalesStatus))%></span>
            <%----推荐 Begin----%>
            <%if ("1".Equals(Convert.ToString(ViewData["IsRecommend"])))
              { %>
            <i class="tuj_i"></i>
            <%} %>
            <%----推荐 End----%>
            <span class="font14 col999">[<%=ViewData["DName"]%>] 排名<%=ViewData["Rank"] %>
                浏览<%=ViewData["ViewCount"]??"0"%>次</span><%--<span class="right"><a href=""><img src="<%=TXCommons.GetConfig.ImgUrl%>images/yaohao.gif" /></a></span>--%><!--摇号-->
        </div>
        <%}
          }%>
        <div class="user_nav">
            <div class="w1190">
                <a href="<%=NHWebUrl.PremisesIndexUrl(Model.PremisesId.ToString()) %>"><i class="r1">
                </i></a><a href="<%=NHWebUrl.PremisesDetailUrl(Model.PremisesId.ToString()) %>"><i
                    class="r2"></i></a><a class="current" href="<%=NHWebUrl.PremisesHouseUrl(Model.PremisesId.ToString(),"","","") %>">
                        <i class="r3"></i></a><a href="<%=NHWebUrl.AlbumUrl("hxt",Model.PremisesId.ToString(),"") %>">
                            <i class="r4"></i></a><a href="<%=NHWebUrl.AlbumUrl("",Model.PremisesId.ToString(),"") %>">
                                <i class="r5"></i></a><a href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.PremisesId.ToString()) %>">
                                    <i class="r6"></i></a>
            </div>
        </div>
        <div class="js-search-gro mb10">
            <div class="w1190">
                <span class="back-gro ml10 mr10"><a class="this-text" href="<%=string.Format("{0}/lp-fy-p{1}-b{2}-u{3}-f{4}", TXCommons.GetConfig.GetSiteRoot, Model.PremisesId, Model.BuildingId,Model.UnitId ,Model.Floor)%>">
                    返回<%=Model.BuildingName %>栋列表</a> </span>
                <%=Html.DropDownListFor(model => model.BuildingId, Model.Buildings, new { @class = "select1" })%>
                <%=Html.DropDownListFor(model => model.UnitId, Model.Units, new { @class = "select1" })%>
                <%=Html.DropDownListFor(model => model.Floor, Model.Floors, new { @class = "select1" })%>
                <a href="javascript:void(0)" class="s-link-c"></a>
            </div>
        </div>
        <div class="js-resultfilter-gro">
            <div class="w1190">
                <span class="col666 font12 this-repair ml10"><strong>序号：</strong></span>
                <% foreach (SelectListItem h in Model.Houses)
                   {
                %>
                <a href="<%=NHWebUrl.PremisesHouseInfoUrl(h.Value)%>" <%=h.Value == Model.HouseId.ToString() ? "class=\"cur\"" : "" %>>
                    <%= h.Text%></a>
                <% } %>
            </div>
        </div>
        <div id="divActivitySign">
        </div>
        <script src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/jquery.validate.min.js")) %>"
            type="text/javascript"></script>
        <!--弹出层相关-->
        <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/skins/nxdgreen/css/dialog.css")) %>"
            rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/core/base.js")) %>"></script>
        <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/plugins/freeDialog.js")) %>"></script>
        <script type="text/javascript">
    function getHouseDetail(houseid) {
        $.ajax({
            type: "get",
            url: globalvar.siteRoot +"/Premises/HouseDetail",
            data: { houseid: houseid },
            cache: false,
            async: false,
            dataType: "html",
            success: function (result) {
                $("#divActivitySign").html(result);
            },
            error: function (msg) {
                alert(msg.responseText);
            }
        });
    }
    getHouseDetail(<%=Model.HouseId %>);
        </script>
        <%--<%Html.RenderAction("HouseDetail", "Premises", Model);%>--%>
        <div class="w1190 clearFix">
            <div class="mt15 mb15">
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/r_ywlc.gif" alt="" /></div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        $(".s-link-c").click(function () {
            var buildingid = $("#BuildingId").val();
            var unitid = $("#UnitId").val();
            var floor = $("#Floor").val();
            var premises = '<%=Model.PremisesId %>';
            $.ajax({
                type: "get",
                url: '<%=Url.Content("~/Premises/GetHouse") %>',
                data: { premisesId: premises, buildingid: buildingid, unitid: unitid, floor: floor, m: Math.random() },
                cache: false,
                async: false,
                dataType: "json",
                success: function (result) {
                    if (result.length > 0) {
                        window.location = "<%=TXCommons.GetConfig.GetSiteRoot %>/lp-fy-house-" + result[0].Id + ".html";
                    }
                    else {
                        globalvar.dialog("没有找到相关房源！");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                }
            });
        });

        function FoorListbyHouseId(houseId) {
            $.ajax({
                type: "get",
                url: '<%=Url.Content("~/Premises/FoorListbyHouseId") %>',
                data: { houseId: houseId, m: Math.random() },
                cache: false,
                async: false,
                dataType: "json",
                success: function (result) {
                    //alert(result.length);
                    if (result.length > 0) {
                        $("#Floor").empty();
                        for (var i = 0; i < result.length; i++) {
                            $("<option value=\"" + result[i].Value + "\">" + result[i].Text + "</option>").appendTo("#Floor");
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                }
            });
        };

        $("#UnitId").change(
            function () {

                FoorListbyHouseId("<%=Model.HouseId %>");
            }
        );

        $("#BuildingId").change(
			function () {
			    $.ajax({
			        type: "get",
			        url: '<%=Url.Content("~/Premises/UnitListbyBuildingId") %>',
			        data: { buildingId: $("#BuildingId").val(), m: Math.random() },
			        cache: false,
			        async: false,
			        dataType: "json",
			        success: function (result) {
			            $("#UnitId").empty();
			            if (result.length > 0) {
			                for (var i = 0; i < result.length; i++) {
			                    $("<option value=\"" + result[i].Num + "\">" + result[i].Name + "</option>").appendTo("#UnitId");
			                    if (i == 0) {
			                        FoorListbyHouseId('<%=Model.HouseId %>');
			                    }
			                }
			            }
			        },
			        error: function (XMLHttpRequest, textStatus, errorThrown) {
			        }
			    });
			});

        //控制 房间信息中楼盘名称的显示与隐藏 活动进行中隐藏，否则显示
        $(function () {
            if ($('.yellow_box1 .part1').length == 0) {
                $('.bg_yellow').show();
            }
        });
    </script>
</asp:Content>
