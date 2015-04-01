<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box" style="width: 710px;">
    <a style="cursor: pointer" class="close"></a>
    <div class="layer_title">
        房间位置标注</div>
    <div class="layer_content" style="height: 383px;">
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
            width="710" height="383" id="Sandpainting" title="房间位置" align="middle">
            <param name="allowScriptAccess" value="always" />
            <param name="movie" value="<%=TXCommons.GetConfig.GetFileUrl("js/flash/Room.swf")%>">
            <param name="quality" value="high">
            <param name="wmode" value="transparent" />
            <embed src="<%=TXCommons.GetConfig.GetFileUrl("js/flash/Room.swf")%>" name="房间位置"
                quality="high" allowscriptaccess="always" swliveconnect="true" pluginspage="http://www.macromedia.com/go/getflashplayer"
                type="application/x-shockwave-flash" width="710" height="383"></embed>
        </object>
    </div>
    <div>
        <input type="button" onclick="btn();" value="确定" class="btn3 mr10"><input type="button"
            onclick="No();" value="取消" class="btn3"></div>
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $("#div_dia .close").bind("click", function () {
            $("#div_dia").hide();

        });
        $("#div_dia .btn_grey_w70").bind("click", function () {
            $("#div_dia").hide();
        });

        //        var data = $("#SandboxCoordinate").val();
        //        $.each(eval(data), function (i, item) {
        //            alert(i)
        //            Callx(item.x);
        //            Cally(item.y);
        //            Callid(item.bid);
        //        });
    });
    var markedResultJson;
    function btn() {
        if (!markedResultJson)
            alert("请标记！");
        else {
            alert("标记完成");
            var json = JSON.parse(markedResultJson);
            $("#my_dialog").dialog("close");
            $("#HouseCoordinate").val(markedResultJson);
            $("#CoordX").val(json[json.length - 1].CoordX);
            $("#CoordY").val(json[json.length - 1].CoordY);
        }
    }
    function No() {
        $("#my_dialog").dialog("close");
    }
    var JSONObject = {
        "employees": []
    }
    function jsReturnValue(x, y) {

        //插入一条
        JSONObject.employees.push({ "CoordX": x, "CoordY": y });
        markedResultJson = JSON.stringify(JSONObject.employees);

    }

    //发送图片地址
    function Callurl() {
        var pic = $("#PicSrc").val();
        return pic;

    }

    //X轴地址
    function Callx() {
        var x = $("#CoordX").val();
        return x;
    }

    //Y轴地址
    function Cally() {
        var y = $("#CoordY").val();
        return y;
    }

    //位置ID
    function Callid() {
        return "biao1_mc";
    }

    function Call0() {
        return all;
    }


</script>
