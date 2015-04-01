<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--新房-楼盘-交通配套--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bg-comparepop-none1
        {
            width: auto;
            overflow: hidden;
            background: none;
            color: #8d978a;
            padding-top: 0;
        }
    </style>
    <style type="text/css">
        .map_title
        {
            color: #0068b7;
            font-weight: bold;
        }
        .map_content
        {
            margin-top: 10px;
        }
        .ul_list li
        {
            height: 29px;
            overflow: hidden;
        }
        
        .page_traffic
        {
            height: 24px;
            line-height: 22px;
            color: #969696;
            text-align: center;
        }
        .page_traffic a
        {
            height: 22px;
            line-height: 22px;
            display: inline-block;
            margin: 0px 2px;
            color: #969696;
            padding: 0px 8px;
            border: 1px solid #d7d7d7;
        }
        .page_traffic a:hover
        {
            color: #FFFFFF;
            background: #71b30e;
            border: 1px solid #71b30e;
        }
        .page_traffic a.hover
        {
            color: #FFFFFF;
            background: #71b30e;
            border: 1px solid #71b30e;
        }
        .page_traffic a.no, .page_traffic a.no:hover
        {
            color: #999;
            background: #f8f8f8;
            cursor: auto;
            border: 1px solid #d7d7d7;
        }
    </style>
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">北京新房</a>
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
        <% }
          }%>
        <div class="user_nav">
            <div class="w1190">
                <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"><i class="r1"></i>
                </a><a href="<%=NHWebUrl.PremisesDetailUrl(Model.Id.ToString()) %>"><i class="r2"></i>
                </a><a href="<%=NHWebUrl.PremisesHouseUrl(Model.Id.ToString(),"","","") %>"><i class="r3">
                </i></a><a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"") %>"><i class="r4">
                </i></a><a href="<%=NHWebUrl.AlbumUrl("",Model.Id.ToString(),"") %>"><i class="r5"></i>
                </a><a class="current" href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>">
                    <i class="r6"></i></a>
            </div>
        </div>
        <div class="mt15 w1190 clearFix">
            <div class="r_title_lp">
                <strong class="title_span">
                    <%=Model.Name%>周边交通配套</strong><%--<span class="right"><a class="blue font12 mr20 r_tb_map"
                        href="">查看地图</a></span>--%></div>
            <div class="transpor_box clearFix">
                <%--  <div class="transpor_left">
      <img src="<%=TXCommons.GetConfig.ImgUrl%>images/img_w745_h391.jpg" width="745" height="391" /></div>--%>
                <div class="transpor_left">
                    <div id="mapContainer" style="width: 935px; height: 391px;">
                    </div>
                </div>
                <div class="transpor_right">
                    <ul class="r_qh_title transtiti clearFix">
                        <li class="on tanspd15"><a href="javascript:showMapSearch(1);">生活</a></li>
                        <li class="tanspd15"><a href="javascript:showMapSearch(2);">交通</a></li>
                        <li class="tanspd15"><a href="javascript:showMapSearch(3);">周边楼盘</a></li>
                    </ul>
                    <div class="map_right_box">
                        <h5 class="title_cy">
                            <a href="javascript:" id="cy_fanhui" style="float: right; display: none;" class="blue font12 mr5">
                                &lt;&lt; 返回</a> 餐饮</h5>
                        <ul class="ul_list clearFix">
                            <li>暂无数据</li>
                        </ul>
                        <p class="tar">
                            <a href="javascript:" id="cy_more" class="blue font12 mr5">更多&gt;&gt; </a>
                        </p>
                        <h5 class="title_gw">
                            <a href="javascript:" id="gw_fanhui" style="float: right; display: none;" class="blue font12 mr5">
                                &lt;&lt; 返回</a> 购物</h5>
                        <ul class="ul_list clearFix">
                            <li>暂无数据</li>
                        </ul>
                        <p class="tar">
                            <a href="javascript:" id="gw_more" class="blue font12 mr5">更多&gt;&gt; </a>
                        </p>
                        <ul id="ul_jt" class="ul_list clearFix">
                            <li>暂无数据</li>
                        </ul>
                    </div>
                    <div class="map_right_box" style="display: none; overflow: auto; height: 360px;">
                        <ul class="ul_list clearFix">
                            <%if (ViewData["NearbyPremises"] != null)
                              {%>
                            <%foreach (var item in (ViewData["NearbyPremises"] as List<TXOrm.Premis>))
                              { 
                            %>
                            <li><a href="javascript:;" map_lng="<%=item.Lng %>" map_lat="<%=item.Lat %>" map_address="<%=item.PremisesAddress %>">
                                <%=item.Name%></a></li>
                            <% 
                              }

                              }
                              else
                              { %>
                            <li>暂无数据</li>
                            <% }
                            %>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="shadow">
            </div>
            <div class="mt15 clearFix mb15">
                <div class="periphery clearFix fl">
                    <h5>
                        周边交通</h5>
                    <p>
                        <%=Model.TrafficIntroduction%></p>
                    <div id='zbjt'>
                    </div>
                    <%-- 紧邻新华西街。新华大街是长安街东沿线，是通州连接市中心的最核心的交通主干道。
            临近地铁八通线 地铁八通线通州北苑站下车即到； 紧邻通州长途客运站与机场巴士站，
            40分钟通达首都国际机场周边公车808、435、通12、804、647、312、322、667、668、669、
            728等，乘公交在通州北苑路口西站下车即到。</p>--%>
                    <%-- <dl>
                <dt class="icon_gj1">公交：</dt>
                <dd>北京西站(55路、77路、99路)</dd>
            </dl>
            <dl>
                <dt class="icon_gj2">地铁：</dt>
                <dd>4号线、9号线</dd>
            </dl>--%>
                    <%=ViewData["html"]%>
                    <div class="bgxu1 mt20">
                    </div>
                    <div class="clearFix">
                    </div>
                    <h5 class="mt20">
                        周边配套</h5>
                    <p>
                        <%=Model.SupportingIntroduction%></p>
                    <%--   周边景观：西侧紧邻市政规划苗圃，同时亦新郊野公园、南海子公园、
            国际企业文化园、27洞高尔夫球场、老君堂公园、海棠公园公园、鸿博公园八大公园环绕--%>
                    <%--     <p>周边医院：同仁医院、多伦多国际医院、亦庄医院</p>
            <p>周边学校：小大人艺术幼儿园、新世纪幼儿园、三羊小学、张家店小学、亦庄中心小学、亦庄第二中心小学、北京八中、亦庄中学</p>
                    --%>
                    <%-- <dl>
                        <dt class="icon_gj3">学校：</dt>
                        <dd>
                            小大人艺术幼儿园<span class="col999"><span class="col999">（100米）</span></span>、小大人艺术幼儿园<span
                                class="col999">（100米）</span>、小大人艺术幼儿园<span class="col999">（100米）</span>、小大人艺术幼儿园<span
                                    class="col999">（100米）</span></dd>
                    </dl>--%>
                    <div id="zbpt">
                        <%--<dl>
                            <dt class="icon_gj5">购物：</dt>
                            <dd>
                                物美超市<span class="col999">（100米）</span>、物美超市<span class="col999">（100米）</span>、物美超市<span
                                    class="col999">（100米）</span>、物美超市<span class="col999">（100米）</span>、物美超市<span class="col999">（100米）</span></dd>
                        </dl>--%>
                        <%--<dl>
                            <dt class="icon_gj6">医院：</dt>
                            <dd>
                                同仁医院<span class="col999">（100米）</span>、同仁医院<span class="col999">（100米）</span>、同仁医院<span
                                    class="col999">（100米）</span>、同仁医院<span class="col999">（100米）</span>、同仁医院<span class="col999">（100米）</span></dd>
                        </dl>--%>
                        <%--<dl>
                            <dt class="icon_gj7">生活：</dt>
                            <dd>
                                北京银行<span class="col999">（1000米）</span>、北京银行<span class="col999">（1000米）</span>、北京银行<span
                                    class="col999">（1000米）</span>、北京银行<span class="col999">（1000米）</span>、北京银行<span class="col999">（1000米）</span></dd>
                        </dl>--%>
                        <%--<dl>
                            <dt class="icon_gj8">娱乐：</dt>
                            <dd>
                                人民公园<span class="col999">（800米）</span>、人民公园<span class="col999">（800米）</span>、人民公园<span
                                    class="col999">（800米）</span>、人民公园<span class="col999">（800米）</span>、人民公园<span class="col999">（800米）</span>、人民公园<span
                                        class="col999">（800米）</span></dd>
                        </dl>
                        <dl>
                            <dt class="icon_gj8">餐饮：</dt>
                            <dd>
                                北京烤鸭店<span class="col999">（1000米）</span>、北京烤鸭店<span class="col999">（1000米）</span>、北京烤鸭店<span
                                    class="col999">（1000米）</span>、北京烤鸭店<span class="col999">（1000米）</span>、北京烤鸭店<span
                                        class="col999">（1000米）</span></dd>
                        </dl>--%>
                    </div>
                </div>
                <div class="lp_hotp">
                    <div id="divTJW" class="mb10 style-gro-a">
                    </div>
                    <div id="divTQY" class="mb15 style-gro-a">
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="mt10 mb10">
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/r_ywlc.gif" />
            </div>
            <a class="comparepop-btn roof" href="javascript:scroll(0,0)">
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/roof.gif" /></a> <a class="comparepop-btn comparepop-btn1"
                    id="comparepop-btn" href="javascript:void(0);">楼盘<br />
                    对比</a> <a class="llgdlp-btn llgdlp-btn1" href="javascript:void(0);" style="display: inline;">
                        购房<br />
                        指南</a>
            <div class="comparepop right45" style="display: none;">
                <div class="this-title">
                    楼盘对比<a href="javascript:void(0);" class="on" id="lpdbclose"></a>
                </div>
                <div class="this-conx" id="this-conx">
                    <div>
                        <div class="bg-comparepop-none">
                            <ul>
                            </ul>
                        </div>
                        <div class="clearFix tac pt10 pb10">
                            <a href="javascript:void(0);" class="s-link-a font12" id="com_start">开始对比</a> <a
                                href="javascript:void(0);" class="s-link-a font12" id="com_clear">清空</a>
                        </div>
                    </div>
                    <div style="display: none">
                        <div class="clearFix tac">
                            <img src="<%=TXCommons.GetConfig.ImgUrl%>images/bg-comparepop-none.gif" />
                        </div>
                        <div class="clearFix tac pt10 pb10">
                            <a href="javascript:void(0);" class="s-link-a font12">开始对比</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="xf_llgdlp" style="display: none;">
                <div class="this-title">
                    购房指南<a href="javascript:void(0);" class="on" id="gfznclose"></a>
                </div>
                <div class="this-conx this-conx1">
                    <p class="this-conx_p1">
                        <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-gfsc.html" target="_blank">购房手册</a></p>
                    <%-- <p class="this-conx_p2">
                        <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk.html" target="_blank">贷款计算器</a></p>
                    <p class="this-conx_p3">
                        <span><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-gfzc.html" target="_blank">购房资格查询</a></span></p>--%>
                    <div class="gj_box">
                         <ul>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-1.html" class="gj_1" target="_blank">贷款计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-3.html" class="gj_2" target="_blank">公积金贷款计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-4.html" class="gj_3" target="_blank">提前还贷计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-2.html" class="gj_4" target="_blank">购房能力评估计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-5.html" class="gj_5" target="_blank">税费计算器</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.5&ak=1d0c8b02751d95a0ea07d72c58d3b2c6"></script>
            <script type="text/javascript">

                //显示地图搜索 type：1 生活 2 交通 3 楼盘
                function showMapSearch(type) {
                    $('.r_qh_title li').removeClass('on');
                    if (type == 1) {
                        $('.r_qh_title li').eq(0).addClass('on');
                        search_type = 1;
                        PremisesMap.searechMap("餐饮");
                        PremisesMap.searechMap("商场");
                        $('.map_right_box').eq(1).hide().end().eq(0).show().children().show();
                        $('#ul_jt').hide();
                    }
                    else if (type == 2) {
                        $('.r_qh_title li').eq(1).addClass('on');
                        search_type = 3;
                        PremisesMap.searechMap("公交站点");
                        $('.map_right_box').eq(1).hide().end().eq(0).show().children().hide();
                        $('#ul_jt').show();
                    }
                    else {
                        $('.r_qh_title li').eq(2).addClass('on');
                        $('.map_right_box').hide().eq(1).show();
                    }
                }

                //注册餐饮购物更多和返回事件
                $(function () {
                    $('#cy_more').bind('click', function () {
                        $('.title_gw').hide().next('.ul_list').hide().next('.tar').hide();
                        $(this).parent('.tar').hide();
                        search_type = 2;
                        PremisesMap.searechMap("餐饮");
                        $('#cy_fanhui').show();
                    });

                    $('#gw_more').bind('click', function () {
                        $('.title_cy').hide().next('.ul_list').hide().next('.tar').hide();
                        $(this).parent('.tar').hide();
                        search_type = 2;
                        PremisesMap.searechMap("商场");
                        $('#gw_fanhui').show();
                    });

                    $('#cy_fanhui').bind('click', function () {
                        $(this).hide();
                        search_type = 1;
                        PremisesMap.searechMap("餐饮");
                        PremisesMap.searechMap("商场");
                        $('.title_cy').show().next('.ul_list').show().next('.tar').show();
                        $('.title_gw').show().next('.ul_list').show().next('.tar').show();
                    });

                    $('#gw_fanhui').bind('click', function () {
                        $(this).hide();
                        search_type = 1;
                        PremisesMap.searechMap("餐饮");
                        PremisesMap.searechMap("商场");
                        $('.title_cy').show().next('.ul_list').show().next('.tar').show();
                        $('.title_gw').show().next('.ul_list').show().next('.tar').show();
                    });

                    //附近楼盘注册点击事件
                    $('.map_right_box').eq(1).find('a').bind('click', function () {
                        var map_lng = $(this).attr('map_lng');
                        var map_lat = $(this).attr('map_lat');
                        var map_address = $(this).attr('map_address');
                        var map_title = $(this).text();
                        var map_point = new BMap.Point(map_lng, map_lat);
                        PremisesMap.addMarker(map_point, { title: map_title, address: map_address });
                    });

                });
            </script>
            <script type="text/javascript">
                //地图
                var map = null; //地图实例对象
                var point = null; //地图中心坐标（当前楼盘坐标）
                var radius = 1000; //搜索半径
                var isSearch = true; //是否启用地图搜索，楼盘坐标不存在不起用

                var map_pageIndex = 1;
                var map_pageSize = 9;
                var map_dataCount = 0;

                //搜索类型 1餐饮|购物  2餐饮更多|购物更多  3交通
                var search_type = 1;

                //楼盘地图
                var PremisesMap =
        {
            initMap: function () //初始化
            {
                PremisesMap.createMap(); //创建地图
            },
            drawCenter: function () {
                map.clearOverlays(); //清空覆盖物
                if ('<%=Model.Lng%>' == '' && '<%=Model.Lat%>' == '') {
                    point = new BMap.Point(116.232929, 39.542415); //设置中心点坐标
                    map.centerAndZoom(point, 5); //设定地图的中心点并将地图显示在地图容器中
                    isSearch = false;
                    return;
                }
                point = new BMap.Point(parseFloat('<%=Model.Lng%>'), parseFloat('<%=Model.Lat%>')); //设置中心点坐标
                map.centerAndZoom(point, 14); //设定地图的中心点并将地图显示在地图容器中

                //附加中心点标志物和信息
                var marker = new BMap.Marker(point);
                map.addOverlay(marker);
                var content = '<p class="map_title"><%=Model.Name%></p><p class="map_content"><%=Model.PremisesAddress%></p>';
                var infoWindow = new BMap.InfoWindow(content, { enableMessage: false });
                marker.openInfoWindow(infoWindow);
                //给中心点标志物添加单击监听事件
                marker.addEventListener('click', function () {
                    this.openInfoWindow(infoWindow);
                });
            },
            createMap: function () //创建地图
            {
                if (map == undefined) {
                    var map = new BMap.Map("mapContainer"); //在百度地图容器中创建一个地图
                }
                map.clearOverlays(); //清空覆盖物
                window.map = map; //将map变量存储在全局
                PremisesMap.setMapEvent(); //设置地图事件
                PremisesMap.addMapControl(); //向地图添加控件	
                PremisesMap.drawCenter();
            },
            addMapControl: function () {
                //向地图中添加缩放控件
                map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE }));
                //向地图中添加缩略图控件
                map.addControl(new BMap.OverviewMapControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, isOpen: 1 }));
                //向地图中添加比例尺控件
                map.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT }));
            },
            setMapEvent: function ()  //设置鼠标事件
            {
                map.enableDragging(); //启用地图拖拽事件，默认启用
                map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
                map.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用
                map.enableKeyboard(); //启用键盘上下左右键移动地图
                map.enableContinuousZoom();
            },
            addMarker: function (mark_poit, obj) {
                PremisesMap.drawCenter();
                var marker = new BMap.Marker(mark_poit);

                var content = '<p class="map_title">' + obj.title + '</p><p class="map_content">' + obj.address + '</p>';
                var infoWindow = new BMap.InfoWindow(content, { enableMessage: false });
                map.addOverlay(marker);
                marker.openInfoWindow(infoWindow);
                //给中心点标志物添加单击监听事件
                marker.addEventListener('click', function () {
                    this.openInfoWindow(infoWindow);
                });
                //marker.addEventListener("mouseover", function () { set_v(txt); });
                //marker.addEventListener("mouseout", function () { set_u(txt); });


            },
            searechMap: function (Key) {
                if (!isSearch)
                { return; }
                local = new BMap.LocalSearch(map, { renderOptions: { autoViewport: true }, onSearchComplete: PremisesMap.onSearchComplete });
                local.setPageCapacity(map_pageSize);
                local.searchNearby(Key, point, radius);
            },
            searechMapPT: function (Key) {
                local = new BMap.LocalSearch(map, { renderOptions: { autoViewport: true }, onSearchComplete: PremisesMap.onSearchPT });
                local.setPageCapacity(10);
                local.searchNearby(Key, point, radius);
            },
            onSearchPT: function (results) {
                if (results.getCurrentNumPois() > 0) {
                    var str = "";
                    var key = results.keyword;
                    switch (key) {
                        case "学校":
                            str = "<dl width='880px'><dt class=\"icon_gj3\">学校：</dt><dd style='width:768px'>";
                            break;
                        case "购物":
                            str = "<dl><dt class=\"icon_gj5\">购物：</dt><dd style='width:768px'>";
                            break;
                        case "医院":
                            str = "<dl><dt class=\"icon_gj6\">医院：</dt><dd style='width:768px'>";
                            break;
                        case "生活":
                            str = "<dl><dt class=\"icon_gj7\">生活：</dt><dd style='width:768px'>";
                            break;
                        case "娱乐":
                            str = "<dl><dt class=\"icon_gj8\">娱乐：</dt><dd style='width:768px'>";
                            break;
                        case "餐饮":
                            str = "<dl><dt class=\"icon_gj8\">餐饮：</dt><dd style='width:768px'>";
                            break;

                    }
                    for (var i = 0; i < results.getCurrentNumPois(); i++) {
                        var juli = map.getDistance(point, results.getPoi(i).point).toString();
                        str += results.getPoi(i).title + "<span class=\"col999\"><span class=\"col999\">（" + juli.substring(0, juli.indexOf('.')) + "米）</span></span>";
                        if (i < results.getCurrentNumPois() - 1)
                            str += "、";
                    }
                    //周边配套填充
                    $("#zbpt").append(str + "</dd></dl>");
                }
            },
            searechMapJT: function (Key) {
                local = new BMap.LocalSearch(map, { renderOptions: { autoViewport: true }, onSearchComplete: PremisesMap.onSearchJT });
                local.setPageCapacity(10);
                local.searchNearby(Key, point, radius);
            },
            onSearchJT: function (results) {
                if (results.getCurrentNumPois() > 0) {
                    var str = "";
                    var key = results.keyword;
                    switch (key) {
                        case "公交":
                            str = "<dl width='880px'><dt class=\"icon_gj1\">公交：</dt><dd style='width:768px'>";
                            break;
                        case "地铁":
                            str = "<dl width='880px'><dt class=\"icon_gj2\">地铁：</dt><dd style='width:768px'>";
                            break;
                    }
                    for (var i = 0; i < results.getCurrentNumPois(); i++) {
                        var juli = map.getDistance(point, results.getPoi(i).point).toString();
                        str += results.getPoi(i).title;
                        if (i < results.getCurrentNumPois() - 1)
                            str += "、";
                    }
                    //周边交通填充
                    $("#zbjt").append(str + '</dd></dl>');
                }
            },
            onSearchComplete: function (results) {
                PremisesMap.drawCenter();
                switch (search_type) {
                    case 1:
                        {
                            if (results.getCurrentNumPois() == 0) {
                                return;
                            }

                            var pointThis = null;
                            if (results.keyword == "餐饮") {
                                $('.title_cy').next('.ul_list').html('');
                            } else if (results.keyword == "商场") {
                                $('.title_gw').next('.ul_list').html('');
                            }

                            for (var i = 0; i < results.getCurrentNumPois(); i++) {
                                if (i > 3) break;
                                var juli = map.getDistance(point, results.getPoi(i).point).toString();
                                var li = document.createElement('li');
                                var a = document.createElement('a');
                                $(a).attr('href', 'javascript:;').text((isNullOrEmptyOrUndefined(results.getPoi(i).title) ? "名字不明确" : results.getPoi(i).title));
                                $(li).append('<span class="fr">' + juli.substring(0, juli.indexOf('.')) + '米</span>').append(a);
                                //防止闭包
                                function bindA(_poit, _obj) {
                                    $(a).bind('click', function () {
                                        PremisesMap.addMarker(_poit, _obj);
                                    });
                                }
                                bindA(results.getPoi(i).point, results.getPoi(i));
                                if (results.keyword == "餐饮") {
                                    $('.title_cy').next('.ul_list').append(li);
                                } else if (results.keyword == "商场") {
                                    $('.title_gw').next('.ul_list').append(li);
                                }
                            }
                        }; break;
                    case 2:
                        {
                            if (results.getCurrentNumPois() == 0) {
                                return;
                            }

                            map_dataCount = results.getNumPois(); //设置总记录数
                            map_pageIndex = results.getPageIndex() + 1;
                            var pointThis = null;
                            if (results.keyword == "餐饮") {
                                $('.title_cy').next('.ul_list').html('');
                            } else if (results.keyword == "商场") {
                                $('.title_gw').next('.ul_list').html('');
                            }

                            for (var i = 0; i < results.getCurrentNumPois(); i++) {
                                var juli = map.getDistance(point, results.getPoi(i).point).toString();
                                var li = document.createElement('li');
                                var a = document.createElement('a');
                                $(a).attr('href', 'javascript:;').text((isNullOrEmptyOrUndefined(results.getPoi(i).title) ? "名字不明确" : results.getPoi(i).title));
                                $(li).append('<span class="fr">' + juli.substring(0, juli.indexOf('.')) + '米</span>').append(a);
                                //防止闭包
                                function bindA(_poit, _obj) {
                                    $(a).bind('click', function () {
                                        PremisesMap.addMarker(_poit, _obj);
                                    });
                                }
                                bindA(results.getPoi(i).point, results.getPoi(i));

                                if (results.keyword == "餐饮") {
                                    $('.title_cy').next('.ul_list').append(li);
                                } else if (results.keyword == "商场") {
                                    $('.title_gw').next('.ul_list').append(li);
                                }

                            }
                            if (results.keyword == "餐饮") {
                                $('.title_cy').next('.ul_list').append('<li>' + PremisesMap.pageCreate() + '</li>');
                            } else if (results.keyword == "商场") {
                                $('.title_gw').next('.ul_list').append('<li>' + PremisesMap.pageCreate() + '</li>');
                            }
                        }; break;
                    case 3:
                        {
                            if (results.getCurrentNumPois() == 0) {
                                return;
                            }

                            $('#ul_jt').html('');
                            map_dataCount = results.getNumPois(); //设置总记录数
                            map_pageIndex = results.getPageIndex() + 1;
                            var pointThis = null;

                            for (var i = 0; i < results.getCurrentNumPois(); i++) {
                                if (results.getPoi(i).title != "站点Station") {
                                    var juli = map.getDistance(point, results.getPoi(i).point).toString();
                                    var li = document.createElement('li');
                                    var a = document.createElement('a');
                                    $(a).attr('href', 'javascript:;').text((isNullOrEmptyOrUndefined(results.getPoi(i).title) ? "名字不明确" : results.getPoi(i).title));
                                    $(li).append('<span class="fr">' + juli.substring(0, juli.indexOf('.')) + '米</span>').append(a);
                                    //防止闭包
                                    function bindA(_poit, _obj) {
                                        $(a).bind('click', function () {
                                            PremisesMap.addMarker(_poit, _obj);
                                        });
                                    }
                                    bindA(results.getPoi(i).point, results.getPoi(i));

                                    $('#ul_jt').append(li);
                                }
                            }
                            $('#ul_jt').append('<li>' + PremisesMap.pageCreate() + '</li>');

                        }; break;
                    default: break;
                }
            },
            //分页
            pageCreate: function () {
                var allpage = 0;
                var next = 0;
                var pre = 0;
                var startcount = 0;
                var endcount = 0;
                var pagestr = " <div class=\"page_traffic\">";
                if (map_pageIndex < 1) { map_pageIndex = 1; }
                //计算总页数
                if (map_pageSize != 0) {
                    allpage = parseInt((map_dataCount / map_pageSize));
                    allpage = ((map_dataCount % map_pageSize) != 0 ? allpage + 1 : allpage);
                    allpage = (allpage == 0 ? 1 : allpage);
                }
                next = map_pageIndex + 1;
                pre = map_pageIndex - 1; //123
                startcount = (map_pageIndex + 2) > allpage ? (map_pageIndex < 3 ? 1 : (map_pageIndex - 1)) : map_pageIndex - 1; //中间页起始序号
                //中间页终止序号
                endcount = map_pageIndex < 2 ? 3 : map_pageIndex + 1;
                if (startcount < 1) { startcount = 1; }
                if (allpage < endcount) { endcount = allpage; }
                pagestr += map_pageIndex > 1 ? "<a href=\"javascript:local.gotoPage(" + (pre - 1) + ");\">&lt;&lt;</a>" : "<a class=\"no\">&lt;&lt;</a>";
                for (i = startcount; i <= endcount; i++) {
                    pagestr += map_pageIndex == i ? "<a href=\"javascript:;\" class=\"hover\">" + i + "</a>" : "<a href=\"javascript:local.gotoPage(" + (i - 1) + ");\">" + i + "</a>";
                }
                pagestr += map_pageIndex != allpage ? "<a href=\"javascript:local.gotoPage(" + (next - 1) + ");\">&gt;&gt;</a>" : "<a class=\"no\">&gt;&gt;</a>";
                pagestr += "</div>";
                return pagestr;
            }
        }
        
            </script>
            <script type="text/javascript">
                function getTQYHotPremisesList(id) {
                    $.ajax({
                        type: "get",
                        url: globalvar.siteRoot + "/Premises/TQYHotPremisesList",
                        data: { id: id },
                        cache: false,
                        async: false,
                        dataType: "html",
                        success: function (result) {

                            $("#divTQY").html(result);

                        },
                        error: function (msg) {
                            //alert(msg.responseText);
                        }
                    });
                }
                function getTJWHotPremisesList(id) {
                    $.ajax({
                        type: "get",
                        url: globalvar.siteRoot + "/Premises/TJWHotPremisesList",
                        data: { id: id },
                        cache: false,
                        async: false,
                        dataType: "html",
                        success: function (result) {
                            //                    alert(result);
                            $("#divTJW").html(result);

                        },
                        error: function (msg) {
                            //alert(msg.responseText);
                        }
                    });
                }
                getTQYHotPremisesList('<%=Model.Id%>');
                getTJWHotPremisesList('<%=Model.Id%>');
            </script>
            <script type="text/javascript">
                $(function () {
                    PremisesMap.initMap();
                    showMapSearch(1);
                    PremisesMap.searechMapPT("学校");
                    PremisesMap.searechMapPT("购物");
                    PremisesMap.searechMapPT("医院");
                    PremisesMap.searechMapPT("生活");
                    PremisesMap.searechMapPT("娱乐");
                    PremisesMap.searechMapPT("餐饮");
                    PremisesMap.searechMapJT("公交");
                    PremisesMap.searechMapJT("地铁");
                });
            </script>
            <!-- InstanceEndEditable -->
        </div>
        <script type="text/javascript">
            $(function () {
                //购房指南
                $(".llgdlp-btn").bind('click', function () {
                    //getLastPremisesList();
                    if ($(".xf_llgdlp").is(":hidden")) {
                        $(".comparepop").hide();
                        $(".xf_llgdlp").show();
                    } else {
                        $(".xf_llgdlp").hide();
                    }
                });

                $("#gfznclose").bind('click', function () {
                    $(".xf_llgdlp").hide();
                });

                //添加楼盘对比项
                $(".plus_compare").click(function () {
                    $('.bg-comparepop-none').addClass('bg-comparepop-none1');
                    var $this = this;
                    var cookie_compare_items = $.cookie("compare_items");
                    var ifPlus = false;
                    var ifFour = false;
                    if (!isNullOrEmptyOrUndefined(cookie_compare_items)) {
                        $("#this-conx").find("ul").empty();
                        var arr = cookie_compare_items.split(",");
                        for (var i = arr.length - 1; i >= 0; i--) {
                            var result = arr[i].split("|");
                            $("#this-conx").find("ul").prepend("<li id='" + result[1] + "' data='" + result[0] + "' url='" + result[2] + "'><label class='this-repair'><input type='checkbox' name='gr_compare' /></label>&nbsp;<a href='" + result[2] + "' target='_blank' class='linkB-666 font12'>" + result[0] + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                        }
                        $("#this-conx").children("div:eq(0)").show();
                        $("#this-conx").children("div:eq(1)").hide();
                        $(".comparepop").show();
                        if (arr.length <= 3) {
                            for (var i = 0; i < arr.length; i++) {
                                if ($($this).attr("lang") == arr[i].split("|")[1]) {
                                    ifPlus = true;
                                    break;
                                }
                            }
                            if (ifPlus == false) {
                                $("#this-conx").find("ul").prepend("<li id='" + $($this).attr("lang") + "' data='" + $($this).attr("data") + "' url='" + $($this).attr("url") + "'><label class='this-repair'><input type='checkbox' name='gr_compare'/></label>&nbsp;<a href='" + $($this).attr("url") + "' target='_blank' class='linkB-666 font12'>" + $($this).attr("data") + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                            }
                        } else {
                            ifFour = true;
                        }
                    } else {
                        $("#this-conx").children("div:eq(0)").show();
                        $("#this-conx").children("div:eq(1)").hide();
                        $("#this-conx").find("ul").empty();
                        $("#this-conx").find("ul").prepend("<li id='" + $($this).attr("lang") + "' data='" + $($this).attr("data") + "' url='" + $($this).attr("url") + "'><label class='this-repair'><input type='checkbox' name='gr_compare'/></label>&nbsp;<a href='" + $($this).attr("url") + "' target='_blank' class='linkB-666 font12'>" + $($this).attr("data") + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                    }
                    //全部选中
                    Cb_SelectAll();
                    $(".comparepop").show();
                    //存入cookie
                    SetCompareItemsToCookie();
                    if (ifPlus == true) {
                        alert("您已添加该楼盘");
                    } else if (ifFour == true) {
                        alert("最多添加4个楼盘");
                    }
                });

                //开始对比
                $("#com_start").click(function () {
                    var li = $('#this-conx').find(".bg-comparepop-none").children("ul").children("li");
                    var arr = new Array();
                    for (var i = 0; i < li.length; i++) {
                        if ($(li[i]).find("input[name='gr_compare']").attr("checked")) {
                            arr.push($(li[i]).attr("id"));
                        }
                    }
                    if (arr.length > 0) {
                        window.open("<%=TXCommons.GetConfig.GetSiteRoot%>/gj-lppk-" + arr.join(","));
                    } else {
                        alert("请勾选对比项");
                    }
                });

                //删除对比项
                $(".this-del").live("click", function () {
                    $(this).closest("li").remove();
                    var liLength = $("#this-conx").find(".bg-comparepop-none").children("ul").children("li").length;
                    if (liLength <= 0) {
                        $("#this-conx").children("div:eq(0)").hide();
                        $("#this-conx").children("div:eq(1)").show();
                    }
                    //更新cookie
                    SetCompareItemsToCookie();
                });

                //清空对比项
                $("#com_clear").click(function () {
                    var a_clear = this;
                    $(a_clear).parent(".clearFix").prev("ul").children("li").remove();
                    $(a_clear).parent(".clearFix").parent("div").hide();
                    $(a_clear).parent(".clearFix").parent("div").next("div").show();
                    //清空cookie
                    $.cookie("compare_items", null, { domain: "<%=TXCommons.GetConfig.Domain%>" });
                });

                //楼盘对比显示隐藏
                $("#comparepop-btn").live("click", function () {
                    if ($(".comparepop").is(":hidden")) {
                        //从cookie中加载对比项
                        GetCompareItemsFromCookie();
                        //全部选中
                        Cb_SelectAll();
                        $(".xf_llgdlp").hide();
                        $(".comparepop").show();
                        var div = $("#this-conx");
                        var liLength = div.find(".bg-comparepop-none").children("ul").children("li").length;
                        if (liLength <= 0) {
                            $('.bg-comparepop-none').removeClass('bg-comparepop-none1');
                            div.children("div:eq(0)").hide();
                            div.children("div:eq(1)").show();
                        } else {
                            $('.bg-comparepop-none').addClass('bg-comparepop-none1');
                            div.children("div:eq(0)").show();
                            div.children("div:eq(1)").hide();
                        }
                    } else {
                        $(".comparepop").hide();
                    }
                });
                $("#lpdbclose").live("click", function () {
                    $(".comparepop").hide();
                });

                //购房指南显示隐藏
                $(".goufangzhinan-btn").bind('click', function () {
                    if ($(".goufang_pop").is(":hidden")) {
                        $(".goufang_pop").show();
                    } else {
                        $(".goufang_pop").hide();
                    }
                }).bind('mouseenter', function () {
                    $(".goufang_pop").show();
                });

                $(".goufang_pop").bind('mouseleave', function () {
                    $(this).hide();
                });

                $(".goufang_pop").find(".shouqi").click(function () {
                    $(this).closest(".goufang_pop").hide();
                });
            });

            //全部选中
            function Cb_SelectAll() {
                $("#this-conx").find("ul li").find("input[name=gr_compare]").each(function () {
                    if (!$(this).attr("checked")) {
                        $(this).trigger("click");
                    }
                });
            }

            //将对比项加入cookie
            function SetCompareItemsToCookie() {
                var arr = new Array();
                var liList = $("#this-conx").find("ul li");
                for (var i = 0; i < liList.length; i++) {
                    arr.push($(liList[i]).attr("data") + "|" + $(liList[i]).attr("id") + "|" + $(liList[i]).attr("url"));
                }
                //存入cookie，有效期7天
                $.cookie("compare_items", arr.join(","), { expires: 7, domain: "<%=TXCommons.GetConfig.Domain%>" });
            }

            //从cookie中加载对比项
            function GetCompareItemsFromCookie() {
                var cookie_compare_items = $.cookie("compare_items");
                if (!isNullOrEmptyOrUndefined(cookie_compare_items)) {
                    var arr = cookie_compare_items.split(",");
                    $("#this-conx").find("ul").empty();
                    for (var i = arr.length - 1; i >= 0; i--) {
                        var result = arr[i].split("|");
                        $("#this-conx").find("ul").prepend("<li id='" + result[1] + "' data='" + result[0] + "' url='" + result[2] + "'><label class='this-repair'><input type='checkbox' name='gr_compare' /></label>&nbsp;<a href='" + result[2] + "' target='_blank' class='linkB-666 font12'>" + result[0] + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                    }
                } else {
                    $("#this-conx").find("ul").empty();
                }
            }

            function AddUserFavorite(premisesid, userid) {
                $.ajax({
                    type: "get",
                    url: globalvar.siteRoot + "/Search/AddUserFavorite",
                    data: { premisesid: premisesid, userid: userid, m: Math.random() },
                    cache: false,
                    async: false,
                    dataType: "html",
                    success: function (result) {
                        globalvar.dialog(result);
                    },
                    error: function (msg) {
                        //alert(msg.responseText);
                    }
                });
            }
        </script>
</asp:Content>
