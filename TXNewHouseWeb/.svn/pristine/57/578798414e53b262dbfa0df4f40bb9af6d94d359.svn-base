<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Premis>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--楼盘相册--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/newhouse.css")%>" rel="stylesheet"
        type="text/css" />
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">
                    <%=ViewData["cityName"]%>新房</a>
                <%--> <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"
                        class="blue">楼盘主页</a>--%>
                >
                <%=Model.Name%>
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
            <img width="1190" height="200" alt="" src="<%=ViewData["ADPictureUrl"]%>.1190_200.jpg" /></div>
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
        <%}
          }%>
        <div class="user_nav">
            <div class="w1190">
                <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"><i class="r1"></i>
                </a><a href="<%=NHWebUrl.PremisesDetailUrl(Model.Id.ToString()) %>"><i class="r2"></i>
                </a><a href="<%=NHWebUrl.PremisesHouseUrl(Model.Id.ToString(),"","","") %>"><i class="r3">
                </i></a><a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"") %>"><i class="r4">
                </i></a><a href="<%=NHWebUrl.AlbumUrl("",Model.Id.ToString(),"") %>" class="current">
                    <i class="r5"></i></a><a href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>">
                        <i class="r6"></i></a>
            </div>
        </div>
        <div class="w1190 hometype_img mt15 mb15">
            <ul class="r_qh_title clearFix pl20">
                <li onclick="album.fun(<%=Model.Id %>,'xgt',0)"><a href="javascript:void(0)">效果图</a></li>
                <li onclick="album.fun(<%=Model.Id %>,'hxt',1)"><a href="javascript:void(0)">户型图</a></li>
                <li onclick="album.fun(<%=Model.Id %>,'ght',2)"><a href="javascript:void(0)">规划图</a></li>
                <li onclick="album.fun(<%=Model.Id %>,'wzt',3)"><a href="javascript:void(0)">位置图</a></li>
                <li onclick="album.fun(<%=Model.Id %>,'ldt',4)"><a href="javascript:void(0)">楼栋平面图</a></li>
                <li onclick="album.fun(<%=Model.Id %>,'sjt',5)"><a href="javascript:void(0)">实景图</a></li>
                <li onclick="album.fun(<%=Model.Id %>,'jtt',6)"><a href="javascript:void(0)">交通图</a></li>
            </ul>
            <div class="hometype_pho_box clearFix">
                <div class="mt10" id="sel" style="display: none">
                    筛选：
                    <select class="w100 mr10" id="sel1">
                        <option value="r">厅室查询</option>
                        <option value="m">面积查询</option>
                        <option value="d">楼栋查询</option>
                    </select>
                    <select class="w100" id="sel2">
                    </select></div>
                <div id="albumDiv">
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script type="text/javascript">
        var album = {
            id: '<%=Model.Id %>', //楼盘Id
            type: '<%=ViewData["albumType"]%>', //相册类型
            ts: '<%=ViewData["TS"] %>', //厅室
            noInfo: '<div class="hometype_pho_box clearFix"><div class="clearFix list-groups-k"><div class="noResult_box tac mt35"><span class="icons01">暂无数据。</span></div></div></div>', //暂无数据
            albumDiv: $("#albumDiv"), //数据集合DIV
            selDiv: $("#sel"),
            selOne: $("#sel1"),
            selTwo: $("#sel2"),
            hxtHtml: function (detailurl, picurl, room, title, hall, toilet, buildingarea, hits) {//户型图输出
                return '<dl><dt><a href="' + detailurl + '" target="_blank"><img src="' + picurl + '.180_130.jpg" width="180" height="130" /></a></dt><dd><p><a href="' + detailurl + '" class="blue" target="_blank">' + title + '</a></p><p><span class="col666">户型：</span>' + (room == "" ? "" : room + "室" + hall + "厅" + toilet + "卫") + '<span class="col666 ml20">面积：</span>' + (buildingarea == "" ? "" : buildingarea + "㎡") + '</p><p><span class="col666">浏览：</span>' + hits + '次</p></dd></dl>';
            },
            fun: function (id, type, tab) {//切换标签
                if (type != "hxt") {
                    album.selDiv.hide(); //隐藏分类标签
                    album.selTwo.empty(); //清空子类
                    album.selOne.val('r'); //选中厅室
                    album.picList(id, type, true, "", "", "");
                }
                else {
                    album.initHxt();
                }
                $(".r_qh_title li").removeClass(); //清空样式
                $(".r_qh_title li").eq(tab).addClass("on"); //样式选中
            },
            picList: function (premisesId, pictype, isallflag, mj, ts, ld) {//异步取相册数据
                $.post(globalvar.siteRoot + "/Premises/AlbumList", { m: Math.random(), premisesId: premisesId, picturetype: pictype, isallflag: isallflag, mj: "", ts: "", ld: "" }, function (data) {
                    if (data != null && data != "") {
                        var info = '';
                        if (pictype != "hxt") {

                            for (var i = 0; i < data.length; i++) {
                                var dataI = data[i];
                                info += '<dl><dt><a href="' + dataI.DetailUrl + '" class="blue"><img src="' + dataI.PicUrl + '.211_150.jpg" /></a></dt><dd><p><a href="' + dataI.DetailUrl + '" class="blue">' + dataI.Title + '</a></p><p><span class="col666">浏览：</span>' + dataI.Hits + '次</p></dd></dl>';
                            }
                        } else {
                            album.tsTab(0, 0); //初始化户型图
                        }
                        album.albumDiv.html(info);
                    } else {

                        album.albumDiv.html(album.noInfo);
                    }
                });
            },
            tsTab: function (ts, from) {
                $.post(globalvar.siteRoot + "/Premises/TsTab", { m: Math.random(), ts: ts, premisesId: album.id }, function (d) {
                    var a = d.str.split(',');
                    var sizechart = d.sizeChartListAll;
                    var info = "";
                    if (a != "") {
                        album.selDiv.show();
                        album.selOne.val('r');
                        if (from == 0) {
                            album.selTwo.empty();
                            for (var i = 0; i < a.length; i++) {
                                album.selTwo.append("<option value='" + a[i] + "'>" + a[i] + "室</option>");
                            }
                        } else {
                            album.selTwo.empty();
                            for (var i = 0; i < a.length; i++) {
                                album.selTwo.append("<option value='" + a[i] + "'>" + a[i] + "室</option>");
                            }
                            if (ts != 0) {
                                album.selTwo.val(ts);
                            }
                        }
                    } else {//暂无数据（无房源关联户型图）
                        album.selDiv.hide();
                    }
                    if (sizechart != null && sizechart.length > 0) {
                        for (var j = 0; j < sizechart.length; j++) {
                            info += album.hxtHtml(sizechart[j].DetailUrl, sizechart[j].PicUrl, sizechart[j].Room, sizechart[j].Title, sizechart[j].Hall, sizechart[j].Toilet, sizechart[j].BuildingArea, sizechart[j].Hits);
                        }
                        album.albumDiv.html(info);
                    } else {//无数据
                        $.post(globalvar.siteRoot + "/Premises/RedisSizeChart", { m: Math.random(), premisesId: album.id, guid: album.guid }, function (data) {
                            if (data != "") {
                                for (var i = 0; i < data.length; i++) {
                                    var dataI = data[i];
                                    info += '<dl><dt><a href="' + dataI.DetailUrl + '" class="blue"><img src="' + dataI.PicUrl + '.211_150.jpg" /></a></dt><dd><p><a href="' + dataI.DetailUrl + '" class="blue">' + dataI.Title + '</a></p><p><span class="col666">浏览：</span>' + dataI.Hits + '次</p></dd></dl>';
                                }
                                album.albumDiv.html(info);
                            } else {
                                album.albumDiv.html(album.noInfo);
                            }
                        });
                    }
                });
            },
            ldTab: function (ld, from) {
                $.post(globalvar.siteRoot + "/Premises/LdTab", { m: Math.random(), ld: ld, premisesId: album.id }, function (d) {
                    var a = d.str.split(',');
                    var sizechart = d.sizeChartListAll;
                    var info = "";

                    if (from == 0) {
                        for (var i = 0; i < a.length; i++) {
                            var b = a[i].split(':');
                            if (b[1] != "" && b[2] != "") {
                                album.selTwo.append("<option value='" + b[0] + "'>" + b[1] + b[2] + "</option>");
                            }
                        }
                    }
                    if (sizechart.length > 0) {
                        for (var j = 0; j < sizechart.length; j++) {
                            info += album.hxtHtml(sizechart[j].DetailUrl, sizechart[j].PicUrl, sizechart[j].Room, sizechart[j].Title, sizechart[j].Hall, sizechart[j].Toilet, sizechart[j].BuildingArea, sizechart[j].Hits);
                        }
                        album.albumDiv.html(info);
                    } else {
                        album.albumDiv.html(album.noInfo);
                    }
                });
            },
            mjTab: function (mj, from) {
                $.post(globalvar.siteRoot + "/Premises/MjTab", { m: Math.random(), mj: mj, premisesId: album.id }, function (d) {
                    var a = d.str.split(',');
                    var sizechart = d.sizeChartListAll;
                    var info = "";
                    if (from == 0) {
                        for (var i = 0; i < a.length; i++) {
                            var b = a[i].split(':');
                            if (b[0] != "" && b[1] != "") {
                                album.selTwo.append("<option value='" + b[1] + "'>" + b[0] + "</option>");
                            }
                        }
                    }
                    if (sizechart.length > 0) {
                        for (var j = 0; j < sizechart.length; j++) {
                            info += album.hxtHtml(sizechart[j].DetailUrl, sizechart[j].PicUrl, sizechart[j].Room, sizechart[j].Title, sizechart[j].Hall, sizechart[j].Toilet, sizechart[j].BuildingArea, sizechart[j].Hits);
                        }
                        album.albumDiv.html(info);
                    } else {
                        album.albumDiv.html(album.noInfo);
                    }
                });
            },
            initHxt: function () {
                //album.selDiv.show();
                album.selTwo.empty();
                album.selOne.val('r');
                album.tsTab(0, 0);
            }
        }

        $(function () {
            //条件1变更事件
            album.selOne.change(function () {
                album.selTwo.empty();
                if (album.selOne.val() == "r")
                    album.tsTab(0, 0); //加载厅室（默认选中第一个）
                if (album.selOne.val() == "d")
                    album.ldTab(0, 0); //加载楼栋（默认选中第一个）
                if (album.selOne.val() == "m")
                    album.mjTab(0, 0); //加载面积（默认选中第一个）
            });
            //条件2变更事件
            album.selTwo.change(function (e) {
                var val = album.selTwo.val();
                var mj = "", ts = "", ld = "";
                if (album.selOne.val() == "r")
                    album.tsTab(val, 1); //加载厅室（默认选中第一个）
                if (album.selOne.val() == "d")
                    album.ldTab(val, 1); //加载楼栋（默认选中第一个）
                if (album.selOne.val() == "m")
                    album.mjTab(val, 1);
            });
            $(".r_qh_title li").removeClass();
            if (album.type != "" && album.type != null) {//类型导航选中
                if (album.type == "xgt")
                    album.fun(album.id, album.type, 0);
                if (album.type == "hxt") {
                    if (album.ts != "") {
                        $(".r_qh_title li").removeClass();
                        $(".r_qh_title li").eq(1).addClass("on");
                        album.tsTab(album.ts, 1);
                    } else {
                        album.fun(album.id, album.type, 1);
                    }
                }
                if (album.type == "ght")
                    album.fun(album.id, album.type, 2);
                if (album.type == "wzt")
                    album.fun(album.id, album.type, 3);
                if (album.type == "ldt")
                    album.fun(album.id, album.type, 4);
                if (album.type == "sjt")
                    album.fun(album.id, album.type, 5);
                if (album.type == "jtt")
                    album.fun(album.id, album.type, 6);
            }
            else {//默认选中效果图
                album.fun(album.id, "xgt", 0);
            }
        });
    </script>
</asp:Content>
