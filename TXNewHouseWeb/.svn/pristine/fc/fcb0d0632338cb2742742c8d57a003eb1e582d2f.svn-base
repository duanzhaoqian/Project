<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TXOrm.Premis>" %>
<div class="map_left">
    <div id="mapContainer" style="width: 830px; height: 390px;">
    </div>
</div>
<div class="map_right">
    <ul class="r_qh_title clearFix">
        <li><a href="javascript:showMapSearch(1);">生活</a></li>
        <li class="on"><a href="javascript:showMapSearch(2);">交通</a></li>
        <li style="padding: 0 18px;"><a href="javascript:showMapSearch(3);">周边楼盘</a></li>
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
                var content = '<p class="map_title">' + decodeURI('<%=Server.UrlEncode(Model.Name)%>') + '</p><p class="map_content">' + decodeURI('<%= Server.UrlEncode(Model.PremisesAddress)%>') + '</p>';
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
            onSearchComplete: function (results) {
                //alert(results.getNumPois());
                PremisesMap.drawCenter();
                switch (search_type) {
                    case 1:
                        {
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
                pre = map_pageIndex - 1;
                startcount = (map_pageIndex + 3) > allpage ? allpage - 5 : map_pageIndex - 2; //中间页起始序号
                //中间页终止序号
                endcount = map_pageIndex < 3 ? 6 : map_pageIndex + 3;
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
    $(function () {
        PremisesMap.initMap();
        showMapSearch(2);
    });

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
