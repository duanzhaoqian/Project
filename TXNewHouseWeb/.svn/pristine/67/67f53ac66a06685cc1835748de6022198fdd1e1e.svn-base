var album = {
    hostname: location.hostname, //beijing.kyjob.com
    pathname: location.pathname, // /xinfang/lp_hxt_12_d3_i1
    param: location.pathname.substr(1).split('/')[1], //lp_hxt_12_d3
    xc: location.href.match(/t[0-9]+/g), //1取户型图 2规划图 3位置图 4楼栋平面图 5效果图 6实景图
    init: function () {
        album.initTypeAddr();
        $(".r_qh_title li").removeClass();
        if (album.xc != "" && album.xc != null) {//类型导航选中
            if (album.xc == "t1")
                $(".r_qh_title li").eq(1).addClass("on");
            if (album.xc == "t2")
                $(".r_qh_title li").eq(2).addClass("on");
            if (album.xc == "t3")
                $(".r_qh_title li").eq(3).addClass("on");
            if (album.xc == "t4")
                $(".r_qh_title li").eq(4).addClass("on");
            if (album.xc == "t5")
                $(".r_qh_title li").eq(5).addClass("on");
            if (album.xc == "t6")
                $(".r_qh_title li").eq(6).addClass("on");
        }
        else {//默认全部
            $(".r_qh_title li").eq(0).addClass("on");
        }
    },
    //初始化地址 n:位置，c:标识符
    initExt: function (n, c) {
        var arr = album.param.split('-');
        var url = "";
        for (var i = 0; i < 3; i++) {
            if (i < 3) url += arr[i] + (c == "" && i == 2 ? "" : "-");
        }
        if (c != "")
            url = url + c;

        $(".r_qh_title li a").eq(n).attr("href", url);
    },
    initTypeAddr: function () {
        album.initExt(0, '');
        album.initExt(1, 't1');
        album.initExt(2, 't2');
        album.initExt(3, 't3');
        album.initExt(4, 't4');
        album.initExt(5, 't5');
        album.initExt(6, 't6');
    }
}

$(function () {
    album.init();
});