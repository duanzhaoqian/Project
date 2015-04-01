<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box w695">
    <a style="cursor: pointer" class="close"></a>
    <div class="layer_title">
        沙盘地图标注</div>
    <div class="layer_content" style="height: 450px;">
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
            width="695" height="450" id="Sandpainting" title="沙盘地图" align="middle">
            <param name="allowScriptAccess" value="always" />
            <param name="movie" value="<%=TXCommons.GetConfig.GetFileUrl("js/flash/Sandbox.swf")%>" />
            <param name="quality" value="high" />
            <param name="wmode" value="transparent" />
            <embed src="<%=TXCommons.GetConfig.GetFileUrl("js/flash/Sandbox.swf")%>" name="沙盘地图"
                quality="high" allowscriptaccess="always" swliveconnect="true" pluginspage="http://www.macromedia.com/go/getflashplayer"
                type="application/x-shockwave-flash" width="695" height="450"></embed>
        </object>
    </div>
    <div class="layer_btn">
        <input type="button" onclick="jswinsandbox.saveData();" value="确定" class="btn3" />
        <input type="button" onclick="jswinsandbox.closeWin();" value="取消" class="btn3" /></div>
</div>
<script type="text/javascript">

    // Flash调用方法 ----- START

    //加载flash图片
    function Callurl() {
        return jsnewsandbox.getPic();
    }

    //加载楼盘坐标
    function Call0() {
        jswinsandbox.data = jsnewsandbox.getSandBoxData();//
        return jsnewsandbox.getSandBoxDataString();
    }
    
    //沙盘标记
    function jsReturnValue(id, bid, x, y) {
        jswinsandbox.addData(id.replace("box", "biao"), bid, x, y);
    }

    //删除标记
    function jsDeleteValue(id) {
        jswinsandbox.delData(id.replace("box", "biao"));
    }

    // Flash调用方法 ----- END

    var jswinsandbox = {
        data: [],

        // 保存数据
        saveData: function () {
            $("#hid_isupdatesandbox").val(1);
            jsnewsandbox.setSandBoxData(jswinsandbox.data);
            jswinsandbox.closeWin();
        },
        
        // 添加数据
        addData: function (id, bid, x, y) {
            if (!jswinsandbox.isjsonErepeat(id)) {
                jswinsandbox.data.push({ "SandBox": id, "Number": bid, "CoordX": x, "CoordY": y });
            } else {
                jswinsandbox.updateData(id, bid, x, y);
            }
        },

        // 关闭窗口
        closeWin: function () {
            $("#my_dialog").dialog("close");
        },
        
        // 更新json中的数据
        updateData: function(id, bid, x, y) {
            var e = jswinsandbox.getJsonEById(id);
            if (null != e) {
                e.Number = bid;
                e.CoordX = x;
                e.CoordY = y;
            }
        },
        
        // 删除json中的数据
        delData: function(id) {
            var index = jswinsandbox.getJsonIndexById(id);
            if (index > -1) {
                jswinsandbox.data.splice(index, 1);
            }
        },
        
        // 根据id获取json元素
        getJsonEById: function (id) {
            var json = jswinsandbox.data;
            for (var i = 0; i < json.length; i++) {
                if (json[i].SandBox == id) {
                    return json[i];
                }
            }
            return null;
        },
        
        // 根据id获取json列所在位置
        getJsonIndexById: function(id) {
            var json = jswinsandbox.data;
            for (var i = 0; i < json.length; i++) {
                if (json[i].SandBox == id) {
                    return i;
                }
            }
            return -1;
        },

        // 检测json列中是否有元素id重复
        isjsonErepeat: function (id) {
            var json = jswinsandbox.data;
            for (var i = 0; i < json.length; i++) {
                if (json[i].SandBox == id) {
                    return true;
                }
            }
            return false;
        }
    };
</script>