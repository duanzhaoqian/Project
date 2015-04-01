<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/global.css")%>s" rel="stylesheet" type="text/css" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/newhouse.css")%>" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="<%=TXCommons.GetConfig.GetFileUrl("js/UtilityVar.js")%>"></script>
        <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery-1.6.4.min.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/golbal.js")%>" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/pageTestDemo.js")%>" language="javascript"
        type="text/javascript">
        </script>
    <!--[if IE 6]>
<script src="../js/DD_belatedPNG_0.0.8a.js"  language="javascript" type="text/javascript">
</script>
<script type="text/javascript">
DD_belatedPNG.fix('*');
</script>
<![endif]-->
    <title>新房-地图找房</title>
</head>
<body>
    <div class="top_nav">
        <div class="clearFix">
            <span class="fl ml20">您好！请</span>
            <div class="down_box">
                <a href="" class="tb_down">登录</a>
                <div class="tc_box w70">
                    <ul>
                        <li><a href="">个人</a></li>
                        <li><a href="">经纪人</a></li>
                        <li><a href="">开发商</a></li>
                    </ul>
                </div>
            </div>
            <i class="fl">|</i>
            <div class="down_box">
                <a href="" class="tb_down">注册</a>
                <div class="tc_box w70">
                    <ul>
                        <li><a href="">个人</a></li>
                        <li><a href="">经纪人</a></li>
                        <li><a href="">开发商</a></li>
                    </ul>
                </div>
            </div>
            <div class="fr">
                <div class="down_box">
                    <a href="" class="tb_down">我的快有家</a>
                    <div class="tc_box w125">
                        <p>
                            您好，请<a href="">登录</a></p>
                        <ul>
                            <li><a href="">我的二手房/租房</a></li>
                            <li><a href="">我的新房</a></li>
                            <li><a href="">我的消息</a></li>
                        </ul>
                    </div>
                </div>
                <div class="fl">
                    <i class="mr10">|</i>服务热线：<span class="font12">400-999-3535<i class="colffd200">9:00-20:00</i></span><i>|</i><a
                        href="" class="link_help">帮助中心</a></div>
            </div>
        </div>
    </div>
    <div class="pub_nav">
        <div class="pub_nav_map clearFix">
            <div class="logo">
                <a href="#">
                    <img src="../images/blank.gif" width="200" height="70" /></a></div>
            <div class="city" style="margin-top: 60px;">
                <div class="pos_r" style="z-index: 2;">
                    <strong>北京</strong>[<a href="">切换城市</a>]
                    <div style="display: none;" class="pos_a">
                        <span class="tc_bj"></span><a href="">北京</a><a href="">上海</a><a href="">广州</a><a
                            href="">深圳</a><a href="">天津</a><a href="">成都</a><a href="">南京</a><a href="">大连</a><a
                                href="">杭州</a><a href="">青岛</a>
                    </div>
                </div>
            </div>
            <div class="search_box clearFix">
                <input type="text" id="txtPremiseskey" name="txtPremiseskey" class="input" value="请输入房源特征，地点或小区名..." />
                <input onclick="search()" type="button" class="btn_search" value="找楼盘" />
            </div>
        </div>
    </div>
    <div class="house_map">
        <div class="house_map_l">
            <div class="house_map_js tar">
                <span class="bg_green"></span>新盘<span class="bg_blue ml10"></span>在售<span class="bg_red ml10"></span><span
                    class="mr15">优惠</span></div>
            <%--<div class="house_map_box">
        	<img src="../images/map1.jpg" />
        </div>--%>
            <div class="house_map_box" id="divMap">
            </div>
           <%-- <span class="mapts_green" style="left: 100px; top: 100px;"><span class="pos_rel"><i
                class="ts_abc">A</i>金科廊桥水岸 | 均价：5万/平方米<i class="green_r"></i></span></span>
            <span class="mapts_blue" style="left: 400px; top: 100px;"><span class="pos_rel"><i
                class="ts_abc">B</i>远洋未来广场购物中心 | 均价：5万/平方米<i class="blue_r"></i></span></span>
            <span class="mapts_yellow" style="left: 200px; top: 200px;"><span class="pos_rel"><i
                class="ts_abc">C</i>北岸1292 | 均价：3.5万/平方米<i class="yellow_r"></i></span></span>--%>
            <div class="map_layer" style="left: 430px; top: 200px;">
               <%-- <div class="map_layer_title">
                    <strong class="fl">远洋未来广场购物中心</strong><i class="zt-i-1 fl"></i><span class="right"><a
                        href="" class="red">点击详情</a><a href="" class="close ml10"></a></span></div>--%>
             <%--   <div class="map_layer_box">
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
                </div>--%>
            </div>
            <div class="map_an">
            </div>
        </div>
        <div class="house_map_r">
    	<div class="map_r_title pos_rel" style="z-index:2;">
            <ul class="title_ul">
                <li><a href="">区域</a></li>
                <li><a href="">价格区间</a></li>
                <li><a href="">户型</a></li>
                <li><a href="">地铁</a></li>
            </ul>
            <div class="pos_abs border_e4 padd15" style="left:0px; top:32px; z-index:1;">
            	<div class="border_b mb10"><span class="colff6600">选择区域：</span></div>
                <ul class="map_qy_list clearFix">
                	<li><a href="">不限</a></li>
                    <li><a href="">朝阳</a></li>
                    <li><a href="">东城</a></li>
                    <li><a href="">海淀</a></li>
                    <li><a href="">西城</a></li>
                    <li><a href="">崇文</a></li>
                    <li><a href="">丰台</a></li>
                    <li><a href="">石景山</a></li>
                    <li><a href="">宣武</a></li>
                    <li><a href="">通州</a></li>
                    <li><a href="">不限</a></li>
                    <li><a href="">朝阳</a></li>
                    <li><a href="">东城</a></li>
                    <li><a href="">海淀</a></li>
                    <li><a href="">西城</a></li>
                    <li><a href="">崇文</a></li>
                    <li><a href="">丰台</a></li>
                    <li><a href="">石景山</a></li>
                    <li><a href="">北京周边</a></li>
                    <li><a href="">通州</a></li>
                </ul>
                <div class="border_b mb10"><span class="colff6600">热门板块</span><span class="ml10 hot"></span></div>
                <ul class="map_qy_list clearFix">
                	<li><a href="">不限</a></li>
                    <li><a href="">朝阳</a></li>
                    <li><a href="">东城</a></li>
                    <li><a href="">海淀</a></li>
                    <li><a href="">西城</a></li>
                    <li><a href="">崇文</a></li>
                    <li><a href="">丰台</a></li>
                    <li><a href="">石景山</a></li>
                    <li><a href="">宣武</a></li>
                    <li><a href="">通州</a></li>
                    <li><a href="">不限</a></li>
                    <li><a href="">朝阳</a></li>
                    <li><a href="">东城</a></li>
                    <li><a href="">海淀</a></li>
                    <li><a href="">西城</a></li>
                    <li><a href="">崇文</a></li>
                    <li><a href="">丰台</a></li>
                    <li><a href="">石景山</a></li>
                    <li><a href="">北京周边</a></li>
                    <li><a href="">通州</a></li>
                </ul>
            </div>
            <div class="pos_abs border_e4 padd15" style="left:0px; top:32px; z-index:1;">
            	<div class="clearFix mb10"><span class="fr">单价(元/㎡)</span></div>
                <ul class="map_qy_list clearFix map_qy_list1">
                	<li><a href="">不限</a></li>
                    <li><a href="">10000以下</a></li>
                    <li><a href="">10000-15000</a></li>
                    <li><a href="">15000-20000</a></li>
                    <li><a href="">20000-30000</a></li>
                    <li><a href="">30000-40000</a></li>
                    <li><a href="">40000以上</a></li>
                </ul>
            </div>
            <div class="pos_abs border_e4 padd15" style="left:0px; top:32px; z-index:1;">
                <ul class="map_qy_list clearFix">
                	<li><a href="">不限</a></li>
                    <li><a href="">一居</a></li>
                    <li><a href="">二居</a></li>
                    <li><a href="">三居</a></li>
                    <li><a href="">四居</a></li>
                    <li><a href="">五居</a></li>
                    <li><a href="">六居</a></li>
                    <li><a href="">复式</a></li>
                    <li><a href="">跃层</a></li>
                    <li><a href="">别墅户</a></li>
                </ul>
            </div>
            <div class="pos_abs border_e4 padd15" style="left:0px; top:32px; z-index:1;">
                <ul class="map_qy_list clearFix">
                	<li><a href="">不限</a></li>
                    <li><a href="">1号线</a></li>
                    <li><a href="">2号线</a></li>
                    <li><a href="">4号线</a></li>
                    <li><a href="">5号线</a></li>
                    <li><a href="">9号线</a></li>
                    <li><a href="">10号线</a></li>
                    <li><a href="">13号线</a></li>
                    <li><a href="">八通线</a></li>
                    <li><a href="">6号线</a></li>
                    <li><a href="">7号线</a></li>
                    <li><a href="">8号线</a></li>
                    <li><a href="">11号线</a></li>
                    <li><a href="">12号线</a></li>
                    <li><a href="">14号线</a></li>
                    <li><a href="">15号线</a></li>
                    <li><a href="">16号线</a></li>
                    <li><a href="">机场线</a></li>
                    <li><a href="">亦庄线</a></li>
                    <li><a href="">大兴线</a></li>
                    <li><a href="">房山线</a></li>
                    <li><a href="">昌平线</a></li>
                    <li><a href="">3号线</a></li>
                </ul>
            </div>
        </div>
        <div class="map_r_box">
        	<div class="overflow_box">
                <div class="pad10 clearFix">
                    <div class="fl line_h24"><span class="colff6600 mr5">北京</span>找到<span class="colff6600 ml5 mr5">60</span>个符合要求的楼盘</div>
                    <div class="fr"><a href="" class="jgpx jgpx1">价格排序</a></div>
                </div>
                <dl class="map_dl">
                    <dt><img src="../images/img_w135_h100.jpg" width="105" height="80" /></dt>
                    <dd class="ml120">
                        <p class="mb5"><a href="" class="blue font14">北京城建·世华</a></p>
                        <p class="col666">均价<strong class="font20 colff6600 fontYahei">80000</strong>元/平方米</p>
                        <p class="mt10"><a href="" class="btn_w95_yellow">查看楼盘详情</a></p>
                    </dd>
                    <dd class="map_px">A</dd>
                    <dd class="checkbox"><input type="checkbox" name="a" /></dd>
                </dl>
                <dl class="map_dl">
                    <dt><img src="../images/img_w135_h100.jpg" width="105" height="80" /></dt>
                    <dd class="ml120">
                        <p class="mb5"><a href="" class="blue font14">北京城建·世华</a></p>
                        <p class="col666">均价<strong class="font20 colff6600 fontYahei">80000</strong>元/平方米</p>
                        <p class="mt10"><a href="" class="btn_w95_yellow">查看楼盘详情</a></p>
                    </dd>
                    <dd class="map_px">B</dd>
                    <dd class="checkbox"><input type="checkbox" name="a" /></dd>
                </dl>
                <dl class="map_dl">
                    <dt><img src="../images/img_w135_h100.jpg" width="105" height="80" /></dt>
                    <dd class="ml120">
                        <p class="mb5"><a href="" class="blue font14">北京城建·世华</a></p>
                        <p class="col666">均价<strong class="font20 colff6600 fontYahei">80000</strong>元/平方米</p>
                        <p class="mt10"><a href="" class="btn_w95_yellow">查看楼盘详情</a></p>
                    </dd>
                    <dd class="map_px">C</dd>
                    <dd class="checkbox"><input type="checkbox" name="a" /></dd>
                </dl>
                <dl class="map_dl">
                    <dt><img src="../images/img_w135_h100.jpg" width="105" height="80" /></dt>
                    <dd class="ml120">
                        <p class="mb5"><a href="" class="blue font14">北京城建·世华</a></p>
                        <p class="col666">均价<strong class="font20 colff6600 fontYahei">80000</strong>元/平方米</p>
                        <p class="mt10"><a href="" class="btn_w95_yellow">查看楼盘详情</a></p>
                    </dd>
                    <dd class="map_px">D</dd>
                    <dd class="checkbox"><input type="checkbox" name="a" /></dd>
                </dl>
            </div>
            <div class="page tac border_top">
                <a class="no">&lt;&lt;</a><a href="" class="hover">1</a><a href="">2</a><a href="">3</a><a href="">4</a><a href="">5</a><a href="">6</a><a href="">&gt;&gt;</a><a href="" class="btn_w66_gray">开始对比</a>
            </div>
        </div>
    </div>
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery.lazyload.min.js")%>" type="text/javascript"></script>
    <!--弹出层相关-->
    <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/skins/nxdgreen/css/dialog.css")) %>"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/core/base.js")) %>"></script>
    <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/plugins/freeDialog.js")) %>"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.5&ak=1d0c8b02751d95a0ea07d72c58d3b2c6"></script>
    <script type="text/javascript">
        function MapPremises(premisesid) {
            $.ajax({
                type: "get",
                url: "/xinfang/MapSearchPremises/MapPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //               alert(result);
                    $("#divMap").html(result);
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function MapPremisessearch(premiseskey) {
            $.ajax({
                type: "get",
                url: "/xinfang/MapSearchPremises/MapPremisessearch",
                data: { premiseskey: premiseskey, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //               alert(result);
                    $("#divMap").html(result);
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function search() {
            var premiseskey = $("#txtPremiseskey").val();
            MapPremisessearch(premiseskey);
        }
        $(function () {
            //加载地图
            MapPremises('60');

            //搜索框
            $("#txtPremiseskey").focus(function () {
                var key = $("#txtPremiseskey").val();
                if (key == "请输入房源特征，地点或小区名...") $("#txtPremiseskey").val("");
            });
            //搜索框
            $("#txtPremiseskey").blur(function () {
                var key = $("#txtPremiseskey").val();
                if (key == "") $("#txtPremiseskey").val("请输入房源特征，地点或小区名...");
            });

        });
    </script>
</body>
</html>
