﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box w690">
    <a style="cursor:pointer" opertype="dialog_close"  class="close"></a>
    <div class="layer_title" opertype="dialog_title">
        楼盘地图标注</div>
    <div class="layer_content" style="height: 380px;">
        <div id="allmap" style="width: 690px; height: 370px">
        </div>
       
    </div>
    <div class="layer_btn">
        <input type="button" onclick="SetHidPoint()" value="确定" class="btn_yel_w70 mr20" opertype="dialog_close"><input type="button" value="取消"
            class="btn_grey_w70" opertype="dialog_close"></div>
</div>
<script language="javascript" type="text/javascript">
//地图初始化
var Lng = $("#Lng").val();
var Lat = $("#Lat").val();
var addlng = null;
var addlat = null;
var marker1 = "";
var map = new BMap.Map("allmap");
map.enableScrollWheelZoom(true);

if (Lng != "" && Lat != "" && Lng > 0 && Lat > 0) {
    map.centerAndZoom(new BMap.Point(Lng, Lat), 11);
    addmarker(Lng, Lat);
} else {
    map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);
    addmarker(null, null);
}

function showInfo(e) {
    //        alert(e.point.lng + ", " + e.point.lat);
    addmarker(e.point.lng, e.point.lat);
}
map.addEventListener("click", showInfo);

//创建标注
function addmarker(Lng, Lat) {
    map.clearOverlays();
    marker1 = new BMap.Marker(new BMap.Point(Lng, Lat));  // 创建标注
    map.addOverlay(marker1);              // 将标注添加到地图中
    addlng = Lng;
    addlat = Lat;
}
function SetHidPoint() {
    if (addlng == null) {
        //            $.freedialog.warn("请单击地图设置经纬度");
        alert("请单击地图设置经纬度");
    }
    else {
        $("#Lng").val(addlng);
        $("#Lat").val(addlat);
        //            $.freedialog.success("地图经纬度设置成功");
        alert("地图经纬度设置成功");
        $("#spanMap").attr("class", "span");
        $("#spanMap").html("");
    }
}
function SetPoint(centerPoint, level) {

    map.setCenter(centerPoint);
    map.setZoom(level);
}
function SetMapPoint(type, lng, lat) {

    var level = GetLevelByID(type);

    SetPoint(new BMap.Point(lng, lat), level);
}
function GetLevelByID(id) {

    var level = 8;
    switch (id) {
        case '#CountryId':
            level = 4;
            break;
        case '#ProvinceId':
            level = 9;
            break;
        case '#CityId':
            level = 11;
            break;
        case '#DId':
            level = 12;
            break;

        case '#BId':
            level = 13;
            break;
        default:
            level = 9;
            break;
    }
    return level;

}
    $(document).ready(function () {
        $("#div_dia .close").bind("click", function () {
            $("#div_dia").hide();
        });
        $("#div_dia .btn_grey_w70").bind("click", function () {
            $("#div_dia").hide();
        });
        var Lng = $("#Lng").val();
        var Lat = $("#Lat").val();
        if (Lng != "" && Lat != "" && Lng > 0 && Lat > 0) {
       
        } else {
            map.centerAndZoom(new BMap.Point(<%=ViewData["Lng"]%>, <%=ViewData["Lat"]%>), 11);
        }
    });
</script>
