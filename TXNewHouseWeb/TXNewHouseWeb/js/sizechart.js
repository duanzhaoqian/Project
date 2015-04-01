var sizechart = {
    ld: { prefix: 'd', text: [], value: [] },
    mj: { prefix: 'm', text: ['50平方米以下', '51-70平方米', '71-90平方米', '91-120平方米', '121-150平方米', '151平方米以上'], value: ['1', '2', '3', '4', '5', '6'] },
    ts: { prefix: 'r', text: [], value: [] },
    mianji: location.href.match(/m[0-9]+/g), //取面积
    loudong: location.href.match(/d[0-9]+/g), //取楼栋
    tingshi: location.href.match(/r[0-9]+/g), //取厅室
    xianshi: location.href.match(/l[0-9]+/g), //取显示方式（图片，列表）
    hostname: location.hostname, //beijing.kyjob.com
    pathname: location.pathname, // /xinfang/lp_hxt_12_d3_i1
    param: location.pathname.substr(1).split('/')[1], //lp_hxt_12_d3
    show: "l1",
    pid: 0, //楼盘Id
    //取楼盘ID
    getpid: function () {
        var p = sizechart.param.split('-');
        var reg = RegExp(/^\d{1,}$/);
        for (var i = 0; i < p.length; i++) {
            if (p[i].match(reg) != null && p[i].match(reg) != "") sizechart.pid = p[i];
        }
    },
    //初始化页面
    init: function () {
        sizechart.getpid(); //取楼盘Id
        sizechart.showType(); //初始化显示方式
        staticBase(sizechart.pid, "d"); //动态加载楼栋
        staticBase(sizechart.pid, "r"); //动态加载楼栋
        $(".r_qh_title li").removeClass();
        sizechart.initExt(1, 'd');
        sizechart.initExt(2, 'r');
        sizechart.initExt(3, 'm');
        sizechart.initExt(0, '');
        if (sizechart.loudong != "" && sizechart.loudong != null) {//楼栋选中
            $(".r_qh_title li").eq(1).addClass("on");
            //sizechart.initType('d', sizechart.ld);
        }
        else if (sizechart.tingshi != "" && sizechart.tingshi != null) {//厅室选中
            $(".r_qh_title li").eq(2).addClass("on");
            //sizechart.initType('r', sizechart.ts);
        }
        else if (sizechart.mianji != "" && sizechart.mianji != null) {//面积选中
            $(".r_qh_title li").eq(3).addClass("on");
            sizechart.initType('m', sizechart.mj);
        }
        else {
            $(".r_qh_title li").eq(0).addClass("on"); //默认选中
            sizechart.initType('', sizechart.ld);
        }

    },
    //初始化地址 n:位置，c:标识符
    initExt: function (n, c) {
        var arr = sizechart.param.split('-');
        var url = "";
        for (var i = 0; i < 3; i++) {
            if (i < 3) url += arr[i] + (c == "" && i == 2 ? "" : "-");
        }
        if (c != "") {
            if (c == "d") { c += "0"; }
            if (c == "r") { c += "0"; }
            if (c == "m") { c += "0"; }
            url = url + c;
        }
        $(".r_qh_title li a").eq(n).attr("href", url + "-" + sizechart.show);
    },
    //初始化类型分类地址
    initType: function (c, b) {
        var rz = '[0-9]{1,}';
        var reg = RegExp(c + rz);
        var arr = sizechart.param.split('-');
        var str = sizechart.param;
        var data = b;
        var html = [];
        if (c != "") {
            if (str.match(reg) != null) {
                for (var i = 0; i < data.text.length; i++) {
                    var url = "";
                    for (var j = 0; j < 3; j++) {
                        if (j < 3) url += arr[j] + (c == "" && j == 2 ? "" : "-");
                    }
                    if (c != "")
                        url = url + c;
                    if (str.match(reg) != c + data.value[i]) {
                        html.push("<a href=\"" + url + data.value[i] + "-" + sizechart.show + "\">" + data.text[i] + "</a>");
                    } else {
                        if (c != "m")
                            html.push("<strong class=\"green\">" + data.text[i] + "</strong>");
                    }
                }
            }
        } else {
            html.push("<strong class=\"green\">&nbsp;</strong>");
        }
        $("#sizecharttype").append(html.join(''));
    },
    //初始化显示方式
    showType: function () {
        var rz = 'l[0-9]{1,}';
        var reg = RegExp(rz);
        var str = sizechart.param;
        if (str.match(reg) != null) {
            sizechart.show = str.match(reg);
            if (sizechart.show == "l1") {//图片
                $("#show").html("<a href=\"" + str.replace("l1", "l2") + "\" class=\"r_lbms_gray\" title=\"列表模式\"></a><i class=\"r_tpms\" title=\"图片模式\"></i>");
            } else {//列表 l2
                $("#show").html("<i class=\"r_lbms\" title=\"列表模式\"></i><a href=\"" + str.replace("l2", "l1") + "\" class=\"r_tpms_gray\" title=\"图片模式\"></a>");
            }
        } else {//默认图片
            $("#show").html("<a href=\"" + str + "_l1" + "\" class=\"r_lbms_gray\" title=\"列表模式\"></a><i class=\"r_tpms\" title=\"图片模式\"></i>");
        }
    }
}

$(function () { sizechart.init(); });
//取楼栋
function staticBase(pid, type) {
//    $.ajax({
//        type: "get",
//        url: "/Ajax/GetStaticData",
//        data: { id: pid, type: type },
//        cache: false,
//        async: false,
//        dataType: "json",
//        success: function (result) {
//            if (result.baseData[0] != null) {
//                for (var i = 0; i < result.baseData.length; i++) {
//                    if (type == "d") {
//                        sizechart.ld.text[i] = result.baseData[i].Name + result.baseData[i].NameType;
//                        sizechart.ld.value[i] = result.baseData[i].Id;
//                    } else if (type = "r") {
//                        sizechart.ts.text[i] = result.baseData[i].Room + "室";
//                        sizechart.ts.value[i] = result.baseData[i].Room;
//                    }
//                }
//            } else {
                if (type == "d") {
                    sizechart.ld.text[0] = "&nbsp;";
                    sizechart.ld.value[0] = 0;
                }
                if (type == "r") {
                    sizechart.ts.text[0] = "&nbsp;";
                    sizechart.ts.value[0] = 0;
                }
//            }
//        },
//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//            //alert(textStatus);
//        }
//    });
}