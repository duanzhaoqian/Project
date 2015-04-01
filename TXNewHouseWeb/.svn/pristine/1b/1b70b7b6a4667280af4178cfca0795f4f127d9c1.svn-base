<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="layer_box w695">
    <a style="cursor: pointer" class="close" opertype="dialog_close"></a>
    <div class="layer_title" opertype="dialog_title" style="cursor: move">
        沙盘地图标注</div>
    <div class="layer_content" style="height: 450px;">
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
            width="692" height="450" id="Sandpainting" title="沙盘地图" align="middle">
            <param name="allowScriptAccess" value="always" />
            <param name="movie" value="<%=TXCommons.GetConfig.GetFileUrl("flash/Sandbox.swf")%>">
            <param name="quality" value="high">
            <param name="wmode" value="transparent" />
            <embed src="<%=TXCommons.GetConfig.GetFileUrl("flash/Sandbox.swf")%>" name="沙盘地图"
                quality="high" allowscriptaccess="always" swliveconnect="true" pluginspage="http://www.macromedia.com/go/getflashplayer"
                type="application/x-shockwave-flash" width="692" height="450"></embed>
        </object>
    </div>
    <div class="layer_btn">
        <input type="button" onclick="jswinsandbox.saveData();" value="确定" class="btn_yel_w70 mr20" /><input
            type="button" value="取消" class="btn_grey_w70" opertype="dialog_close" /></div>
</div>

<script type="text/javascript">

    // Flash调用方法 ----- START

    //加载flash图片
    function Callurl() {
        return jsnewsandbox.getPic();
    }

    //加载楼盘坐标
    function Call0() {
        jswinsandbox.data = jsnewsandbox.getSandBoxData();
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
            jswinsandbox.updateState();//判断是否修改
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
            //$("#my_dialog").dialog("close");
            $("#div_dia").hide();
        },

        // 更新json中的数据
        updateData: function (id, bid, x, y) {
            var e = jswinsandbox.getJsonEById(id);
            if (null != e) {
                e.Number = bid;
                e.CoordX = x;
                e.CoordY = y;
            }
        },

        // 删除json中的数据
        delData: function (id) {
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
        getJsonIndexById: function (id) {
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
        },
        updateState: function () {
            $("#updateState").val(1);
        }


    };
</script>

<%--<script language="javascript" type="text/javascript">
    var JSONObject = {
        "employees": []
    };

    var all = $("#UpdateSandboxCoordinate").val(); //楼盘坐标数据
    var data = $("#SandboxCoordinate").val(); //编辑楼盘坐标使用

    //加载flash图片
    function Callurl() {
        var pic = $("#PicSrc").val();
        return pic;
    }

    //加载楼盘坐标

    function Call0() {
        return eval(all);
    }


    //编辑加载数据
    if (data != null && data != "") {
        $.each(eval(data), function (i, item) {
            JSONObject.employees.push({ "SandBox": item.SandBox, "Number": item.Number, "CoordX": item.CoordX, "CoordY": item.CoordY });
        });
    }

    //沙盘标记
    function jsReturnValue(id, bid, x, y) {
        var f = 0;
        $.each(eval(JSONObject.employees), function (i, item) {
            if (id.replace("box", "biao") == item.SandBox) {
                //存在楼号 准备修改数据
                f = 1;
                JSONObject.employees[i].Number = bid
                JSONObject.employees[i].CoordX = x;
                JSONObject.employees[i].CoordY = y;
            }
        });
        if (f == 0) {
            //插入一条数据
            JSONObject.employees.push({ "SandBox": id.replace("box", "biao"), "Number": bid, "CoordX": x, "CoordY": y });
        }
        var json = JSON.stringify(JSONObject.employees);
        $("#SandboxCoordinate").val(json);
    }


    //删除标记
    function jsDeleteValue(id) {
        var loading = {
            "newJson": []
        };
        $.each(eval(JSONObject.employees), function (i, item) {
            if (id.replace("box", "biao") == item.SandBox) {
                //要删除的选项 
            }
            else {
                //如果不相等
                loading.newJson.push(item);
            }
        });

        JSONObject.employees = loading.newJson;
        var json = JSON.stringify(loading.newJson);
        $("#SandboxCoordinate").val(json);
    }
</script>
<script language="javascript" type="text/javascript">
    var jsSandBox = {
        // 关闭窗口
        closeWin: function () {
            $("#div_dia").hide();
        },

        // 保存数据
        saveData: function () {

            //将坐标值写入UpdateSandboxCoordinate 文本域中 加载使用
            var LoadingSank = "[";
            var data = $("#SandboxCoordinate").val();
            if (data != null && data != "") {
                $.each(eval(data), function (i, item) {
                    if (i == 0) {
                        LoadingSank += "'" + item.SandBox + "','" + item.Number + "','" + item.CoordX + "','" + item.CoordY + "'";
                    } else {
                        LoadingSank += ",'" + item.SandBox + "','" + item.Number + "','" + item.CoordX + "','" + item.CoordY + "'";
                    }
                });
                LoadingSank += "]";
                $("#UpdateSandboxCoordinate").val(LoadingSank);
            }
            alert("标记完成");
            $("#SandError").hide();
            jsSandBox.closeWin();
        }
    };

</script>
--%>