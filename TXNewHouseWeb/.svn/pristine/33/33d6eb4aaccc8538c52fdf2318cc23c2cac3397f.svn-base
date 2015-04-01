<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="allmap" style="width: 520px; height: 360px">
</div>
<script language="javascript" type="text/javascript">

    var BaiduOp = {
        newlng: null,
        newlat: null,
        bmarker: null,
        map: null,

        // 初始化
        initMap: function() {
            BaiduOp.map = new BMap.Map("allmap");
            BaiduOp.map.enableScrollWheelZoom(true);

            if (map_baidu.lng != "" && map_baidu.lat != ""
                && map_baidu.lng > 0 && map_baidu.lat > 0) {
                BaiduOp.map.centerAndZoom(new BMap.Point(map_baidu.lng, map_baidu.lat), 11);
                BaiduOp.addMarker(map_baidu.lng, map_baidu.lat);
            } else {
                BaiduOp.map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
                BaiduOp.addMarker(null, null);
            }

            BaiduOp.map.addEventListener("click", function(e) {
                BaiduOp.addMarker(e.point.lng, e.point.lat);
            });
        },

        // 添加标记
        addMarker: function(lng, lat) {
            if (null == lng || null == lat) {
                return;
            }
            BaiduOp.map.clearOverlays();
            BaiduOp.bmarker = new BMap.Marker(new BMap.Point(lng, lat)); // 创建标注
            BaiduOp.map.addOverlay(BaiduOp.bmarker); // 将标注添加到地图中
            BaiduOp.newlng = lng;
            BaiduOp.newlat = lat;
        },
        
        // 获取最新标点
        getNewPoint: function() {
            if (null == BaiduOp.newlat || null == BaiduOp.newlng) {
                alert("请单击地图设置经纬度");
                return false;
            }

            map_baidu.updateLngLat(BaiduOp.newlng, BaiduOp.newlat);
            return true;
        }
    };

    $(function () {
        BaiduOp.initMap();
        //==============
          if (map_baidu.lng != "" && map_baidu.lat != ""
                && map_baidu.lng > 0 && map_baidu.lat > 0) {
            } else {
             BaiduOp.map.centerAndZoom(new BMap.Point(<%=ViewData["Lng"]%>, <%=ViewData["Lat"]%>), 11);
            }
        //===============
    });
</script>