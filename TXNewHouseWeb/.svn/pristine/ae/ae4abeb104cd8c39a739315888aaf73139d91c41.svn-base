<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box w710">
    <a style="cursor: pointer" class="close" opertype="dialog_close"></a>
    <div opertype="dialog_title" class="layer_title" style="cursor: move">
        房间位置标注</div>
    <div class="layer_content" style="height: 383px;">
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
            width="710" height="383" id="Sandpainting" title="房间位置" align="middle">
            <param name="allowScriptAccess" value="always" />
            <param name="movie" value="<%=TXCommons.GetConfig.GetFileUrl("flash/Room.swf")%>">
            <param name="quality" value="high">
            <param name="wmode" value="transparent" />
            <embed src="<%=TXCommons.GetConfig.GetFileUrl("flash/Room.swf")%>" name="房间位置" quality="high"
                allowscriptaccess="always" swliveconnect="true" pluginspage="http://www.macromedia.com/go/getflashplayer"
                type="application/x-shockwave-flash" width="710" height="383"></embed>
        </object>
    </div>
    <div class="layer_btn">
        <input type="button" onclick="jsRoomMarked.saveData()" value="确定" class="btn_yel_w70 mr20" opertype="dialog_close" /><input
            type="button" value="取消" class="btn_grey_w70" opertype="dialog_close" /></div>
</div>
<script language="javascript" type="text/javascript">
    var jsRoomMarked = {
        saveData: function () {
            alert("标记完成");
            $("#div_dia").hide();
        }
    };

    var JSONObject = {
        "employees": []
    }

    //保存
    function jsReturnValue(x, y) {
        //插入一条
        JSONObject.employees.push({ "CoordX": x, "CoordY": y });
        var json = JSON.stringify(JSONObject.employees);
        $("#HouseCoordinate").val(json);
        $("#CoordX").val(x);
        $("#CoordY").val(y);
    }

    //删除
    function jsDeleteValue(id) {
        $("#CoordX").val("");
        $("#CoordY").val("");
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
</script>
