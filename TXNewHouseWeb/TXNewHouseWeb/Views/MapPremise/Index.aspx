<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新房-地图找房</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!--弹出层相关-->
    <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/skins/nxdgreen/css/dialog.css")) %>"
        rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("css/global.css")) %>"
        rel="stylesheet" type="text/css" />
    <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("css/newhouse.css")) %>"
        rel="stylesheet" type="text/css" />
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/_base.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="<%=TXCommons.GetConfig.GetFileUrl("js/UtilityVar.js")%>"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery-1.6.4.min.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/golbal.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/pageTestDemo.js")%>" language="javascript"
        type="text/javascript">
    </script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/city.js")%>" language="javascript"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery.jscrollpane.min.js")%>" type="text/javascript"></script>
    <style type="text/css">
        .BMapLib_trans
        {
            display: none;
        }
        .mapts_green, .mapts_blue, .mapts_yellow
        {
            top: -20px;
            left: -12px;
        }
    </style>
    <!--[if IE 6]>
    <script src="../../js/DD_belatedPNG_0.0.8a.js"  language="javascript" type="text/javascript">
    </script>
    <script type="text/javascript">
    DD_belatedPNG.fix('*');
    </script>
    <![endif]-->
</head>
<body>
    <div class="top_nav">
        <div class="w1190 clearFix" id="top_l">
        </div>
    </div>
    <div class="pub_nav">
        <div class="pub_nav_map clearFix">
            <div class="logo">
                <a href="#">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/blank.gif" width="200" height="70" /></a></div>
            <div class="city">
                <div class="pos_r">
                    <div class="qh_city">
                        <strong>北京</strong>[<a target="_blank" href="">切换城市</a>]</div>
                    <div class="pos_a w430">
                        <!--<a href="" class="close"></a>-->
                        <span class="tc_bj"></span>
                        <div class="citybox">
                            <div class="tjcity">
                                <div class="hd">
                                    <h2>
                                        推荐城市</h2>
                                </div>
                                <div class="bd">
                                    <ul class="clearFix">
                                        <!--推荐-->
                                    </ul>
                                </div>
                            </div>
                            <div class="qgcity">
                                <div class="hd" id="list_title">
                                    <h2>
                                        全国城市列表</h2>
                                    <ul class="clearFix">
                                        <li class="out over">按拼音</li>
                                        <li class="out">按省份</li>
                                    </ul>
                                </div>
                                <div id="list_cont" class="clearFix">
                                    <span class="dis_b">
                                        <div class="scrollContent scroll-pane">
                                            <!--首字母分类-->
                                        </div>
                                    </span><span class="dis_none">
                                        <div class="scrollContent scroll-pane">
                                            <!--省市分类-->
                                        </div>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    快有家新房平台</p>
            </div>
            <%--            <div class="city" style="margin-top: 60px;">
                <div class="pos_r" style="z-index: 2;">
                    <strong>
                        <%=ViewData["cityName"] %></strong>[<a href="">切换城市</a>]
                    <div class="pos_a" style="display: none;">
                        <span class="tc_bj"></span><a href="http://<%=TXCommons.PinyinHelper.GetPinyin("北京") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">
                            北京</a><a href="http://<%=TXCommons.PinyinHelper.GetPinyin("上海") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">上海</a><a
                                href="http://<%=TXCommons.PinyinHelper.GetPinyin("广州") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">广州</a><a
                                    href="http://<%=TXCommons.PinyinHelper.GetPinyin("深圳") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">深圳</a><a
                                        href="http://<%=TXCommons.PinyinHelper.GetPinyin("天津") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">天津</a><a
                                            href="http://<%=TXCommons.PinyinHelper.GetPinyin("成都") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">成都</a><a
                                                href="http://<%=TXCommons.PinyinHelper.GetPinyin("南京") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">南京</a><a
                                                    href="http://<%=TXCommons.PinyinHelper.GetPinyin("大连") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">大连</a><a
                                                        href="http://<%=TXCommons.PinyinHelper.GetPinyin("杭州") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">杭州</a><a
                                                            href="http://<%=TXCommons.PinyinHelper.GetPinyin("青岛") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/MapPremise/Index">青岛</a>
                    </div>
                </div>
            </div>
            --%>
            <div class="search_box clearFix">
                <input type="text" class="input" value="请输入房源特征，地点或小区名..." id="txtkey" name="txtkey" />
                <input type="button" class="btn_search" value="找楼盘" />
            </div>
        </div>
    </div>
    <div class="house_map">
        <div class="house_map_l" id="mapContainer">
            <div class="house_map_js tar">
                <span class="bg_green"></span>新盘<span class="bg_blue ml10"></span>在售<span class="bg_red ml10"></span><span
                    class="mr15">优惠</span></div>
            <div class="house_map_box" id="divMap" style="width: 999px; height: 600px">
            </div>
            <%--<span class="mapts_green" style="left: 100px; top: 100px;"><span class="pos_rel"><i
                class="ts_abc">A</i>金科廊桥水岸 | 均价：5万/平方米<i class="green_r"></i></span></span>新盘

            <span class="mapts_blue" style="left: 400px; top: 100px;"><span class="pos_rel"><i
                class="ts_abc">B</i>远洋未来广场购物中心 | 均价：5万/平方米<i class="blue_r"></i></span></span>在售

            <span class="mapts_yellow" style="left: 200px; top: 200px;"><span class="pos_rel"><i
                class="ts_abc">C</i>北岸1292 | 均价：3.5万/平方米<i class="yellow_r"></i></span></span>优惠

            <div class="map_layer" style="left: 430px; top: 200px;">
                <div class="map_layer_title">
                    <strong class="fl">远洋未来广场购物中心</strong><i class="zt-i-1 fl"></i><span class="right"><a
                        href="" class="red">点击详情</a><a href="" class="close ml10"></a></span></div>
                <div class="map_layer_box">
                    <dl class="clearFix">
                        <dt>
                            <img src="../images/img_w135_h100.jpg" /></dt>
                        <dd>
                            <p>
                                <span class="col666">均价：</span>7000元/平方米</p>
                            <p>
                                <span class="col666">物业类型：</span>住宅</p>
                            <p>
                                <span class="col666">开盘时间：</span>2013年9月26日一期加推4号楼</p>
                            <p>
                                <span class="col666">开发商：</span>涿州鸿泰房地产开发有限公司</p>
                            <p>
                                <span class="col666">楼盘销售电话：</span><i class="red">400-890-0000</i> 转 <i class="red">
                                    68914</i></p>
                        </dd>
                    </dl>
                </div>
            </div>--%>
            <div class="map_an">
            </div>
        </div>
        <div class="house_map_r">
            <div class="map_r_title pos_rel" style="z-index: 2;">
                <ul class="title_ul">
                    <li><a href="">区域</a></li>
                    <li><a href="">价格区间</a></li>
                    <li><a href="">户型</a></li>
                    <li><a href="">地铁</a></li>
                </ul>
                <div class="pos_abs border_e4 padd15" style="left: 0px; top: 32px; z-index: 1;">
                    <div class="border_b mb10">
                        <span class="colff6600">选择区域：</span></div>
                    <ul class="map_qy_list clearFix" id="quyu">
                        <%--<li><a href="">不限</a></li>--%>
                    </ul>
                    <div class="border_b mb10">
                        <span class="colff6600">热门板块</span><span class="ml10 hot"></span></div>
                    <ul class="map_qy_list clearFix" id="hot_quyu">
                    </ul>
                </div>
                <div class="pos_abs border_e4 padd15" style="left: 0px; top: 32px; z-index: 1;">
                    <div class="clearFix mb10">
                        <span class="fr">单价(元/㎡)</span></div>
                    <ul class="map_qy_list clearFix map_qy_list1" id="price">
                    </ul>
                </div>
                <div class="pos_abs border_e4 padd15" style="left: 0px; top: 32px; z-index: 1;">
                    <ul class="map_qy_list clearFix" id="mj">
                        <%--<li><a href="">不限</a></li>
                        <li><a href="">一居</a></li>
                        <li><a href="">二居</a></li>
                        <li><a href="">三居</a></li>
                        <li><a href="">四居</a></li>
                        <li><a href="">五居</a></li>
                        <li><a href="">六居</a></li>
                        <li><a href="">复式</a></li>
                        <li><a href="">跃层</a></li>
                        <li><a href="">别墅户</a></li>--%>
                    </ul>
                </div>
                <div class="pos_abs border_e4 padd15" style="left: 0px; top: 32px; z-index: 1;">
                    <ul class="map_qy_list clearFix" id="sub">
                    </ul>
                </div>
            </div>
            <div class="map_r_box">
                <div class="overflow_box">
                    <div class="pad10 clearFix" id="jsh">
                        <%--<div class="fl line_h24"><span class="colff6600 mr5">北京</span>找到<span class="colff6600 ml5 mr5">60</span>个符合要求的楼盘</div><div class="fr"><a href="" class="jgpx jgpx1">价格排序</a></div>--%>
                    </div>
                    <div id="premiseList">
                    </div>
                    <%--<dl class="map_dl"><dt><img src="../images/img_w135_h100.jpg" width="105" height="80" /></dt><dd class="ml120"><p class="mb5"><a href="" class="blue font14">北京城建·世华</a></p><p class="col666">均价<strong class="font20 colff6600 fontYahei">80000</strong>元/平方米</p><p class="mt10"><a href="" class="btn_w95_yellow">查看楼盘详情</a></p></dd><dd class="map_px">D</dd><dd class="checkbox"><input type="checkbox" name="a" /></dd></dl>--%>
                </div>
                <div class="page tac border_top" id="map_page">
                    <a class="no">&lt;&lt;</a><a href="" class="hover">1</a><a href="">2</a><a href="">3</a><a
                        href="">4</a><a href="">5</a><a href="">6</a><a href="">&gt;&gt;</a><a href="" class="btn_w66_gray">开始对比</a>
                </div>
            </div>
        </div>
        <input type="hidden" id="type" name="type" value="" />
        <input type="hidden" id="Id" name="Id" value="" />
        <input type="hidden" />
        <input type="hidden" id="hiddenpremisesid" name="hiddenpremisesid" value="" />
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery.lazyload.min.js")%>" type="text/javascript"></script>
    <!--弹出层相关-->
    <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/skins/nxdgreen/css/dialog.css")) %>"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/core/base.js")) %>"></script>
    <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/plugins/freeDialog.js")) %>"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.5&ak=1d0c8b02751d95a0ea07d72c58d3b2c6"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/library/SearchInfoWindow/1.5/src/SearchInfoWindow_min.js"></script>
    <link rel="stylesheet" href="http://api.map.baidu.com/library/SearchInfoWindow/1.5/src/SearchInfoWindow_min.css" />
    <script type="text/javascript">
        //地图
        var map = null; //地图实例对象
        var point = null; //地图中心坐标（当前楼盘坐标）
        var radius = 1000; //搜索半径
        var isSearch = true; //是否启用地图搜索，楼盘坐标不存在不起用

        var map_pageIndex = 1; //当前页
        var map_pageSize = 9; //页大小
        var map_dataCount = 0; //总条数
        var map_keytemp = ''; //之前的查询参数
        var map_order = 1; //排序

        //搜索类型 1餐饮|购物  2餐饮更多|购物更多  3交通
        var search_type = 1;

        //楼盘地图
        var PremisesMap =
        {
            //初始化
            initMap: function () {
                PremisesMap.createMap(); //创建地图
            },
            //无数据设置中心坐标
            drawCenter: function () {
                map.clearOverlays(); //清空覆盖物
                point = new BMap.Point(116.232929, 39.542415); //设置中心点坐标
                map.centerAndZoom(point, 5); //设定地图的中心点并将地图显示在地图容器中
                isSearch = false;
            },
            //创建地图
            createMap: function () {
                if (map == undefined) {
                    map = new BMap.Map("divMap"); //在百度地图容器中创建一个地图
                }
                map.clearOverlays(); //清空覆盖物
                window.map = map; //将map变量存储在全局
                PremisesMap.setMapEvent(); //设置地图事件
                PremisesMap.addMapControl(); //向地图添加控件	
                PremisesMap.drawCenter(); //无数据设置中心坐标
            },
            //地图中添加控件
            addMapControl: function () {
                //向地图中添加缩放控件
                map.addControl(new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE }));
                //向地图中添加缩略图控件
                map.addControl(new BMap.OverviewMapControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, isOpen: 1 }));
                //向地图中添加比例尺控件
                map.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT }));
            },
            //设置鼠标事件
            setMapEvent: function () {
                map.enableDragging(); //启用地图拖拽事件，默认启用
                map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
                map.enableDoubleClickZoom(); //启用鼠标双击放大，默认启用
                map.enableKeyboard(); //启用键盘上下左右键移动地图
                map.enableContinuousZoom();
            },
            //添加标注
            addMarker: function (mark_poit, obj) {
                var salesstatus = decodeURI(obj.salesstatus);
                var spanvar = '';
                var titlestatus = '售罄';
                if (decodeURI(obj.houses) == undefined || decodeURI(obj.houses) == '') {
                    /// 楼盘状态 0 待售 1 在售 2 售罄
                    if (salesstatus == 0) {
                        spanvar = '<SPAN class="mapts_green">';
                        titlestatus = '待售';
                    }
                    if (salesstatus == 1) {
                        spanvar = '<SPAN class="mapts_blue">';
                        titlestatus = '在售';
                    }
                } else {
                    if (salesstatus == 0) {
                        titlestatus = '待售';
                    }
                    if (salesstatus == 1) {
                        titlestatus = '在售';
                    }
                    spanvar = '<SPAN class="mapts_yellow">';
                }
                var txt = spanvar + '<SPAN class="pos_rel"><I class="ts_abc">' + decodeURI(obj.temp) + '</I>' + decodeURI(obj.title) + ' | 均价：' + decodeURI(obj.referenceprice) + '元/平方米<I class="green_r"></I></SPAN></SPAN>';
                var marker = new BMap.Marker(mark_poit);
                var label = new BMap.Label(txt, { position: mark_poit });
                label.setStyle({ border: 'none', height: '20px' });
                map.addOverlay(label);
                var telephone = decodeURI(obj.telephone);
                var openingtime = decodeURI(obj.openingtime);
                var developer = decodeURI(obj.developer);
                var content = '<div class="map_layer_box"><dl class="clearFix"><dt><img width="135" height="100" src="' + decodeURI(obj.effectpictureurl) + '"></dt><dd><p><span class="col666">均价：</span>' + decodeURI(obj.referenceprice) + '元/平方米</p><p><span class="col666">物业类型：</span>住宅</p><p><span class="col666">开盘时间：</span>' + openingtime + '</p><p><span class="col666">开发商：</span>' + developer + '</p><p><span class="col666">楼盘销售电话：</span><i class="red">' + telephone + '</i></p></dd></dl></div>';
                var titlehref = '/xinfang/lp-' + decodeURI(obj.premisesid) + '.html';
                var titlecontent = '<DIV class=map_layer_title><STRONG class=fl>' + decodeURI(obj.title).substring(0, 20) + '</STRONG><I class=\"zt-i-1 fl\"></I><SPAN class=right><A class=red target=\"_blank\" href=\"' + titlehref + '\">点击详情</A><A class=\"close ml10\" href=\"\"></A></SPAN></DIV>';
                var searchInfoWindow = new BMapLib.SearchInfoWindow(map, content, {
                    title: titlecontent,      //标题
                    width: 400,             //宽度
                    panel: "panel",         //检索结果面板
                    enableAutoPan: true,     //自动平移
                    enableSendToPhone: false,
                    searchTypes: []
                });
                label.addEventListener('click', function () {
                    searchInfoWindow.open(marker);
                });
            },
            searechMap: function (id) {
                if (id != undefined && id != '') {
                    map_keytemp = id;
                }
                var key = $("#txtkey").val().trim() == "请输入房源特征，地点或小区名..." ? "" : $("#txtkey").val();
                $.ajax({
                    type: "get",
                    url: "/xinfang/MapPremise/PremisesInfo",
                    data: { id: map_keytemp + "-y" + map_order, pageIndex: map_pageIndex, key: key, m: Math.random() },
                    cache: false,
                    async: false,
                    dataType: "json",
                    success: function (result) {
                        if (result.searchList != null) {
                            if (result.searchList.length > 0) {
                                PremisesMap.drawCenter();
                                for (var i = 0; i < result.searchList.length; i++) {
                                    var url = result.searchList[i].EffectPictureUrl == "" ? '<%=TXCommons.GetConfig.ImgUrl%>images/w186_h125.jpg' : result.searchList[i].EffectPictureUrl;
                                    var map_salesstatus = result.searchList[i].SalesStatus;
                                    var map_propertytype = result.searchList[i].PropertyType;
                                    //开发商
                                    var map_developer = result.searchList[i].Developer;
                                    //开盘时间
                                    var map_openingtime = result.searchList[i].OpeningTime;
                                    //最新活动列表
                                    var map_houses = result.searchList[i].Houses;
                                    //楼盘销售电话
                                    var map_telephone = result.searchList[i].TelePhone;
                                    //楼盘id
                                    var map_premisesid = result.searchList[i].PremisesID;
                                    var map_ReferencePrice = result.searchList[i].ReferencePrice;
                                    var map_address = result.searchList[i].PremisesAddress;
                                    var map_title = result.searchList[i].PremisesName;
                                    var map_point = new BMap.Point(result.searchList[i].Lng, result.searchList[i].Lat);
                                    map.centerAndZoom(map_point, 14);
                                    PremisesMap.addMarker(map_point, { premisesid: map_premisesid, houses: map_houses, telephone: map_telephone, openingtime: map_openingtime, developer: map_developer, propertytype: map_propertytype, effectpictureurl: url, salesstatus: map_salesstatus, title: map_title, address: map_address, referenceprice: map_ReferencePrice, temp: String.fromCharCode(65 + i) });
                                }
                            } else {
                                PremisesMap.drawCenter();
                            }
                        } else {
                            PremisesMap.drawCenter();
                        }
                        PremisesMap.onSearchComplete(result);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                    }
                });
            },
            //右侧列表
            onSearchComplete: function (results) {
                //PremisesMap.drawCenter(); //无数据设置中心坐标
                $('#premiseList').html('');
                if (results.searchList != null) {
                    map_dataCount = results.TotalItemCount; //设置总记录数
                    map_pageIndex = results.CurrentPageIndex;
                    map_pageSize = results.PageSize;
                    var pointThis = null;
                    var def = 'javascript:this.src=<%=TXCommons.GetConfig.ImgUrl%>images/w186_h125.jpg';
                    var jsh = '<div class="fl line_h24"><span class="colff6600 mr5">' + results.CityName + '</span>找到<span class="colff6600 ml5 mr5">' + map_dataCount + '</span>个符合要求的楼盘</div><div class="fr"><a  id="jgpxa" name="jgpxa"  href="javascript:premisjgpx();" class="' + ("jgpx" + map_order) + '">价格排序</a></div>';
                    $('#jsh').html(jsh);
                    for (var i = 0; i < results.searchList.length; i++) {
                        var name = results.searchList[i].PremisesName.length > 10 ? results.searchList[i].PremisesName.substring(0, 10) + "..." : results.searchList[i].PremisesName;
                        var url = results.searchList[i].EffectPictureUrl == "" ? '<%=TXCommons.GetConfig.ImgUrl%>images/w186_h125.jpg' : results.searchList[i].EffectPictureUrl;
                        var dl = '<dl class="map_dl"><dt><img src="' + url + '" width="105" height="80" onerror="' + def + '"/></dt><dd class="ml120"><p class="mb5"><a href="/xinfang/lp-' + results.searchList[i].PremisesID + '.html" class="blue font14" title="' + results.searchList[i].PremisesName + '" target="_blank">' + name + '</a></p><p class="col666">均价<strong class="font20 colff6600 fontYahei">' + results.searchList[i].ReferencePrice + '</strong>元/平方米</p><p class="mt10"><a href="/xinfang/lp-' + results.searchList[i].PremisesID + '.html" class="btn_w95_yellow" target="_blank">查看楼盘详情</a></p></dd><dd class="map_px">' + String.fromCharCode(65 + i)
+ '</dd><dd class="checkbox"><input id=\"' + results.searchList[i].PremisesID + '\" type="checkbox" name="a" /></dd></dl>';
                        $('#premiseList').append(dl);
                    }
                    $('#map_page').html(PremisesMap.pageCreate());
                } else {
                    map_dataCount = results.TotalItemCount; //设置总记录数
                    map_pageIndex = results.CurrentPageIndex;
                    map_pageSize = results.PageSize;
                    var jsh = '<div class="fl line_h24"><span class="colff6600 mr5">' + results.CityName + '</span>找到<span class="colff6600 ml5 mr5">' + 0 + '</span>个符合要求的楼盘</div><div class="fr"><a href="javascript:premisjgpx();" class="' + ("jgpx" + map_order) + '">价格排序</a></div>';
                    $('#jsh').html(jsh);
                    $('#premiseList').html('<dl>暂无数据</dl>');
                    $('#map_page').html(PremisesMap.pageCreate());
                }
            },
            //添加分页
            pageCreate: function () {
                var allpage = 0;
                var next = 0;
                var pre = 0;
                var startcount = 0;
                var endcount = 0;
                var pagestr = "";
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
                pagestr += map_pageIndex > 1 ? "<a  href=\"javascript:premisesgotoPage(" + (pre) + ");\">&lt;&lt;</a>" : "<a class=\"no\">&lt;&lt;</a>";
                for (i = startcount; i <= endcount; i++) {
                    pagestr += map_pageIndex == i ? "<a href=\"javascript:;\" class=\"hover\">" + i + "</a>" : "<a href=\"javascript:premisesgotoPage(" + (i) + ");\">" + i + "</a>";
                }
                pagestr += map_pageIndex != allpage ? "<a href=\"javascript:premisesgotoPage(" + (next) + ");\">&gt;&gt;</a>" : "<a class=\"no\">&gt;&gt;</a>";
                pagestr += "<a href=\"javascript:premisesppk();\" class=\"btn_w66_gray\">开始对比</a>";
                return pagestr;
            },
            //动态获取区域 商圈 地铁 站点  热点位置 等数据
            getData: function (py, type, parent) {
                $.ajax({
                    type: "post",
                    url: "/xinfang/Ajax/GetBaseData",
                    data: { type: type, city: py, pid: parent },
                    cache: false,
                    async: false,
                    dataType: "json",
                    success: function (result) {
                        for (var i = 0; i < result.text.length; i++) {
                            if (type == 0) {
                                if (i == 0) {
                                    $("#quyu").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('quyu')\">" + result.text[i] + "</a></li>");
                                    $("#hot_quyu").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('quyu')\">" + result.text[i] + "</a></li>");
                                } else {
                                    $("#quyu").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('quyu-" + result.value[i] + "')\">" + result.text[i] + "</a></li>");
                                    $("#hot_quyu").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('quyu-" + result.value[i] + "')\">" + result.text[i] + "</a></li>");
                                }
                            }
                            if (type == 1) {
                                if (i == 0)
                                    $("#sub").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('sub')\">" + result.text[i] + "</a></li>");
                                else
                                    $("#sub").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('sub-x" + result.value[i] + "')\">" + result.text[i] + "</a></li>");
                            }
                        };
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                    }
                });
            },
            initPrice: function () {

                for (var i = 0; i < price.value.length; i++) {
                    if (i == 0)
                        $("#price").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('" + price.value[i] + "')\">" + price.text[i] + "</a></li>");
                    else
                        $("#price").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('-p" + price.value[i] + "')\">" + price.text[i] + "</a></li>");
                }
            },
            initRoom: function () {
                for (var i = 0; i < room.value.length; i++) {
                    if (i == 0)
                        $("#mj").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('" + room.value[i] + "')\">" + room.text[i] + "</a></li>");
                    else
                        $("#mj").append("<li><a href=\"javascript:void(0);\" onclick=\"PremisesMap.searechMap('-r" + room.value[i] + "')\">" + room.text[i] + "</a></li>");
                }
            }
        }
    
    </script>
    <script type="text/javascript">
        $(function () {
            //搜索框
            $("#txtkey").focus(function () {
                var key = $("#txtkey").val();
                if (key == "请输入房源特征，地点或小区名...") $("#txtkey").val("");
            });
            //搜索框
            $("#txtkey").blur(function () {
                var key = $("#txtkey").val();
                if (key == "") $("#txtkey").val("请输入房源特征，地点或小区名...");
            });
            $(".btn_search").click(function () {
                var key = $("#txtkey").val();
                if (key.trim() != "请输入房源特征，地点或小区名..." && key.trim() != "")
                    PremisesMap.searechMap("");
            });

            var cityNameGetPinyin = '<%=TXCommons.PinyinHelper.GetPinyin(ViewData["cityName"].ToString()) %>';
            PremisesMap.getData(cityNameGetPinyin, 0, 0); //取【区域】
            PremisesMap.getData(cityNameGetPinyin, 1, 0); //取【地铁线】
            PremisesMap.initPrice();
            PremisesMap.initRoom();
            PremisesMap.initMap(); //初始化地图
            PremisesMap.searechMap("");
        });
        //分页方法
        function premisesgotoPage(page) {
            map_pageIndex = page;
            PremisesMap.searechMap(map_keytemp);
        }
        //排序事件
        function premisjgpx() {
            // alert($('#jgpxa').hasClass('jgpx1'))
            if ($('#jgpxa').hasClass('jgpx1')) {
                map_order = 2;
                PremisesMap.searechMap('');
                $('#jgpxa').removeClass("jgpx1");
                $('#jgpxa').addClass("jgpx2").hasClass('jgpx1')
            } else {
                map_order = 1;
                PremisesMap.searechMap('');
                $('#jgpxa').removeClass("jgpx2");
                $('#jgpxa').addClass("jgpx1").hasClass('jgpx2')
            }
        }
        ///跳转到楼盘pk对比
        function premisesppk() {
            var li = $('#premiseList .map_dl').find('input:[checked]');
            var arr = new Array();
            for (var i = 0; i < li.length; i++) {
                arr.push($(li[i]).attr("id"));
            }
            if (arr.length > 0) {
                window.open("<%=TXCommons.GetConfig.GetSiteRoot%>/gj-lppk-" + arr.join(","));
            } else {
                alert("请勾选对比项");
            }
        }
        function islogin(a, b) {
            $.ajax({ type: "post", url: a, cache: false, dataType: "json", data: {}, beforeSend: function (c) { },
                success: function (c) {
                    //alert(c.lname);
                    var _left = "<div class=\"w1190 clearFix\"><span class=\"fl\">您好！请</span><div class=\"down_box\"><a href=\"<%=TXCommons.GetConfig.LoginUrl %>\" class=\"tb_down\">登录</a><div class=\"tc_box w70\"><ul><li><a href=\"<%=TXCommons.GetConfig.LoginUrl %>\">个人</a></li><li><a href=\"<%=TXCommons.GetConfig.DevelopersUrl%>user/login\">开发商</a></li></ul></div></div><i class=\"fl\">|</i><div class=\"down_box\"><a href=\"<%=TXCommons.GetConfig.RegisterUrl%>\" class=\"tb_down\">注册</a><div class=\"tc_box w70\"><ul><li><a href=\"<%=TXCommons.GetConfig.RegisterUrl%>\">个人</a></li><li><a href=\"<%=TXCommons.GetConfig.DevelopersUrl%>user/register\">开发商</a></li></ul></div></div>";
                    var _right = "<div class=\"fr\"><div class=\"down_box\"><a href=\"\" class=\"tb_down\">我的快有家</a><div class=\"tc_box w125\"><p id=\"r_login\">您好，请<a href=\"<%=TXCommons.GetConfig.LoginUrl %>\">登录</a></p><ul><li><a href=\"<%=TXCommons.GetConfig.UserCenterUrl%>landlord/mylookhouse\">我的看房单</a></li><li><a href=\"<%=TXCommons.GetConfig.UserCenterUrl%>SRent/FKRentList\">我的活动</a></li><li><a href=\"<%=TXCommons.GetConfig.UserCenterUrl%>Landlord/MyReleaseHouse\">我的房源</a></li><li><a href=\"<%=TXCommons.GetConfig.UserCenterUrl%>landlord/systemmessage\">我的消息</a></li></ul></div></div><div class=\"fl\"><i class=\"mr10\">|</i>服务热线：<span class=\"font12\">400-999-3535<i class=\"colffd200\">9:00-20:00</i></span><i>|</i><a  target=\"_blank\" href=\"<%=TXCommons.GetConfig.HelpCenterUrl %>\" class=\"link_help\">帮助中心</a></div></div></div>";
                    if (c.IsLogin) {
                        if ((c.utype == 11 && b == true) || c.utype == 12) {
                            var d = c.utype == 12 ? "<%=TXCommons.GetConfig.UserCenterUrl %>xinfang/user/bid" : "<%=TXCommons.GetConfig.GetQTBaseUrl %>";
                            $("#top_l").html("<span class=\"fl\">您好！<a href='" + d + "'>" + c.lname + "</a><a href='<%=TXCommons.GetConfig.GetSiteRoot %>/Login/LogOut/'>[退出]</a></span>" + _right);
                            $("#r_login").after("<p>" + c.lname + "<a href='<%=TXCommons.GetConfig.GetSiteRoot %>/Login/LogOut/'>[退出]</a></p>"); $("#r_login").remove(); //$(".pub_head .w1000 .RPart a:first").attr("href", d)
                        } else {
                            $("#top_l").html(_left + _right); //.siblings(".bmww").show()
                        }
                    } else {
                        $("#top_l").html(_left + _right); //.siblings(".bmww").show()
                    }
                    $(".down_box").hover(
		                  function () {
		                      $(this).children(".tb_down").addClass("tb_down_hover");
		                      $(this).children(".tc_box").show();
		                  },
		                  function () {
		                      $(this).children(".tb_down").removeClass("tb_down_hover");
		                      $(this).children(".tc_box").hide();
		                  });
                }, complete: function (d, c) { }, error: function () { }
            })
        }
        islogin("/xinfang/Login/AjaxIsLogin", true);
    </script>
</body>
</html>
