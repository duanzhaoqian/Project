// JavaScript Document
//*发布楼盘-楼盘相册切换
$(function () {
    $(function () {
        var $span_a = $(".choose span a");
        $span_a.click(function () {
            $(this).addClass("cur")
				   .siblings().removeClass("cur");
            var index = $span_a.index(this);
            $("#ddivd > div")
					.eq(index).show()
					.siblings().hide();
        })
    })
    divshow();

    $(".big_img a.close").click(function () {
        $("div.big_img").hide();
        return false;
    });

})

function divshow() {
    var t = getQueryString("t")
    if (t&&t>=1&&t<=7) {
        $("#a_" + t).addClass("cur").siblings().removeClass("cur");
        $("#d_" + t).show().siblings().hide();
    }
    else {
        $("#a_1").addClass("cur").siblings().removeClass("cur");
        $("#d_1").show().siblings().hide();
    }
}

function GetPictureType(t) {
    var x = "ROOMTYPE";
    switch (t) {
        case 0:
            x = "PremisesLIST";
            break;
        case 1:
            x = "ROOMTYPE";
            break;
        case 2:
            x = "Plan";
            break;
        case 3:
            x = "Plane";
            break;
        case 4:
            x = "TRAFFIC";
            break;
        case 5:
            x = "Location";
            break;
        case 6:
            x = "Scene";
            break;
        case 7:
            x = "Effect";
            break;
        case 8:
            x = "List";
            break;
    }

    return x;
}

function getQueryString(key) {
    var value = "";
    //获取当前文档的URL,为后面分析它做准备
    var sURL = window.document.URL;
    //URL中是否包含查询字符串
    if (sURL.indexOf("?") > 0) {
        //分解URL,第二的元素为完整的查询字符串
        //即arrayParams[1]的值为【first=1&second=2】
        var arrayParams = sURL.split("?");
        //分解查询字符串
        //arrayURLParams[0]的值为【first=1 】
        //arrayURLParams[2]的值为【second=2】
        var arrayURLParams = arrayParams[1].split("&");
        //遍历分解后的键值对
        for (var i = 0; i < arrayURLParams.length; i++) {
            //分解一个键值对
            var sParam = arrayURLParams[i].split("=");
            if ((sParam[0] == key) && (sParam[1] != "")) {
                //找到匹配的的键,且值不为空
                value = sParam[1];
                break;
            }
        }
    }
    return value;
}