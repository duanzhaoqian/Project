function city_nav(n) {
    var a, t = [],
		i = location.host.split(".")[0],
		u = "\u5317\u4eac",
		h = "",
		s = "";
    i = "www" == i ? "beijing" : i, s = location.pathname.substr(1, location.pathname.length), a = s.match(/(zufang)+/g), h = "/xinfang/quyu";
    for (var y = 0; y < city.text.length; y++) city.value[y] == i && (u = city.text[y]), t.push('<li><a  href="http://' + city.value[y] + globalvar.domain + h + '">' + city.text[y] + n + "</a></li>");
    return $(".city .pos_r strong").html(u), t.join("")
}
function citylist(n) {
    var a, t = [],
		i = location.host.split(".")[0],
		u = "",
		h = "",
		s = "",
		y = "";
    i = "www" == i ? "beijing" : i, s = location.pathname.substr(1, location.pathname.length), a = s.match(/(xinfang|esf)+/g), h = "/xinfang/quyu";
    for (var g = 0; g < n.length; g++) {
        t.push("<dl>"), t.push(" <dt>" + n[g].sz + "</dt>"), t.push(" <dd>");
        for (var e = 0; e < n[g].py.length; e++) n[g].py[e] == i && (u = n[g].text[e]), y = u == n[g].text[e] ? 'class="colf30"' : "", t.push('    <a href="http://' + n[g].py[e] + globalvar.domain + h + '" target="_blank" ' + y + " >" + n[g].text[e] + "</a>");
        t.push(" </dd>"), t.push("</dl>")
    }
    return $(".city .pos_r strong").html(u), t.join("")
}
var city = {
    text: ["\u5317\u4eac", "\u4e0a\u6d77", "\u5e7f\u5dde", "\u6df1\u5733", "\u5929\u6d25", "\u676d\u5dde", "\u5357\u4eac", "\u6210\u90fd", "\u5927\u8fde", "\u9752\u5c9b"],
    value: ["beijing", "shanghai", "guangzhou", "shenzhen", "tianjin", "hangzhou", "nanjing", "chengdu", "dalian", "qingdao"],
    subway: [1, 1, 1, 1, 1, 1, 1, 0, 1, 0]
},
	city1 = [{
	    sz: "A",
	    py: ["anyang"],
	    text: ["\u5b89\u9633"],
	    subway: []
	}, {
	    sz: "B",
	    py: ["beijing", "beihai", "baotou", "baoding", "bengbu", "binzhou"],
	    text: ["\u5317\u4eac", "\u5317\u6d77", "\u5305\u5934", "\u4fdd\u5b9a", "\u868c\u57e0", "\u6ee8\u5dde"],
	    subway: []
	}, {
	    sz: "C",
	    py: ["chongqing", "chengdu", "changchun", "changsha", "changzhou", "changshu"],
	    text: ["\u91cd\u5e86", "\u6210\u90fd", "\u957f\u6625", "\u957f\u6c99", "\u5e38\u5dde", "\u5e38\u719f"],
	    subway: []
	}, {
	    sz: "D",
	    py: ["dalian", "dongguan", "datong", "dezhou"],
	    text: ["\u5927\u8fde", "\u4e1c\u839e", "\u5927\u540c", "\u5fb7\u5dde"],
	    subway: []
	}, {
	    sz: "F",
	    py: ["fuzhou", "foshan"],
	    text: ["\u798f\u5dde", "\u4f5b\u5c71"],
	    subway: []
	}, {
	    sz: "G",
	    py: ["guangzhou", "guiyang", "guilin"],
	    text: ["\u5e7f\u5dde", "\u8d35\u9633", "\u6842\u6797"],
	    subway: []
	}, {
	    sz: "H",
	    py: ["haerbin", "hangzhou", "huhehaote", "hefei", "haikou", "huizhou", "huzhou", "hengyang"],
	    text: ["\u54c8\u5c14\u6ee8", "\u676d\u5dde", "\u547c\u548c\u6d69\u7279", "\u5408\u80a5", "\u6d77\u53e3", "\u60e0\u5dde", "\u6e56\u5dde", "\u8861\u9633"],
	    subway: []
	}, {
	    sz: "J",
	    py: ["jinan", "jiaxing", "jinhua", "jiangmen", "jilinshi", "jining"],
	    text: ["\u6d4e\u5357", "\u5609\u5174", "\u91d1\u534e", "\u6c5f\u95e8", "\u5409\u6797\u5e02", "\u6d4e\u5b81"],
	    subway: []
	}, {
	    sz: "K",
	    py: ["kunming", "kunshan"],
	    text: ["\u6606\u660e", "\u6606\u5c71"],
	    subway: []
	}, {
	    sz: "L",
	    py: ["lanzhou", "luoyang", "liuzhou", "lijiang", "linyi"],
	    text: ["\u5170\u5dde", "\u6d1b\u9633", "\u67f3\u5dde", "\u4e3d\u6c5f", "\u4e34\u6c82"],
	    subway: []
	}, {
	    sz: "M",
	    py: ["mianyang"],
	    text: ["\u7ef5\u9633"],
	    subway: []
	}, {
	    sz: "N",
	    py: ["nanjing", "ningbo", "nanchang", "nanning", "nantong"],
	    text: ["\u5357\u4eac", "\u5b81\u6ce2", "\u5357\u660c", "\u5357\u5b81", "\u5357\u901a"],
	    subway: []
	}, {
	    sz: "Q",
	    py: ["qingdao", "qinhuangdao", "quanzhou", "qingyuan"],
	    text: ["\u9752\u5c9b", "\u79e6\u7687\u5c9b", "\u6cc9\u5dde", "\u6e05\u8fdc"],
	    subway: []
	}, {
	    sz: "S",
	    py: ["shenyang", "shanghai", "shenzhen", "shijiazhuang", "suzhou", "shantou", "sanya", "shaoxing"],
	    text: ["\u6c88\u9633", "\u4e0a\u6d77", "\u6df1\u5733", "\u77f3\u5bb6\u5e84", "\u82cf\u5dde", "\u6c55\u5934", "\u4e09\u4e9a", "\u7ecd\u5174"],
	    subway: []
	}, {
	    sz: "T",
	    py: ["tianjin", "taiyuan", "tangshan", "taian", "taizhou"],
	    text: ["\u5929\u6d25", "\u592a\u539f", "\u5510\u5c71", "\u6cf0\u5b89", "\u6cf0\u5dde"],
	    subway: []
	}, {
	    sz: "W",
	    py: ["wuhan", "wuxi", "wulumuqi", "weihai", "wenzhou", "weifang", "wuhu"],
	    text: ["\u6b66\u6c49", "\u65e0\u9521", "\u4e4c\u9c81\u6728\u9f50", "\u5a01\u6d77", "\u6e29\u5dde", "\u6f4d\u574a", "\u829c\u6e56"],
	    subway: []
	}, {
	    sz: "X",
	    py: ["xian", "xiamen", "xining", "xvzhou", "xianyang", "xiangtan", "xiangyang", "xinxiang"],
	    text: ["\u897f\u5b89", "\u53a6\u95e8", "\u897f\u5b81", "\u5f90\u5dde", "\u54b8\u9633", "\u6e58\u6f6d", "\u8944\u9633", "\u65b0\u4e61"],
	    subway: []
	}, {
	    sz: "Y",
	    py: ["yinchuan", "yantai", "yangzhou", "yichang", "yangjiang"],
	    text: ["\u94f6\u5ddd", "\u70df\u53f0", "\u626c\u5dde", "\u5b9c\u660c", "\u9633\u6c5f"],
	    subway: []
	}, {
	    sz: "Z",
	    py: ["zhengzhou", "zhuhai", "zibo", "zhenjiang", "zhongshan", "zhaoqing", "zhangjiakou"],
	    text: ["\u90d1\u5dde", "\u73e0\u6d77", "\u6dc4\u535a", "\u9547\u6c5f", "\u4e2d\u5c71", "\u8087\u5e86", "\u5f20\u5bb6\u53e3"],
	    subway: []
	}],
	city2 = [{
	    sz: "\u76f4\u8f96\u5e02",
	    text: ["\u5317\u4eac", "\u4e0a\u6d77", "\u5929\u6d25", "\u91cd\u5e86"],
	    py: ["beijing", "shanghai", "tianjin", "chongqing"],
	    subway: []
	}, {
	    sz: "\u5e7f\u4e1c",
	    text: ["\u5e7f\u5dde", "\u6df1\u5733", "\u4e1c\u839e", "\u73e0\u6d77", "\u4e2d\u5c71", "\u4f5b\u5c71", "\u60e0\u5dde", "\u6c5f\u95e8", "\u6c55\u5934", "\u8087\u5e86", "\u6e05\u8fdc", "\u9633\u6c5f"],
	    py: ["guangzhou", "shenzhen", "dongguan", "zhuhai", "zhongshan", "foshan", "huizhou", "jiangmen", "shantou", "zhaoqing", "qingyuan", "yangjiang"],
	    subway: []
	}, {
	    sz: "\u798f\u5efa",
	    text: ["\u798f\u5dde", "\u6cc9\u5dde"],
	    py: ["fuzhou", "quanzhou"],
	    subway: []
	}, {
	    sz: "\u6d59\u6c5f",
	    text: ["\u676d\u5dde", "\u5b81\u6ce2", "\u5609\u5174", "\u7ecd\u5174", "\u6e56\u5dde", "\u91d1\u534e", "\u6e29\u5dde"],
	    py: ["hangzhou", "ningbo", "jiaxing", "shaoxing", "huzhou", "jinhua", "wenzhou"],
	    subway: []
	}, {
	    sz: "\u6cb3\u5317",
	    text: ["\u77f3\u5bb6\u5e84", "\u4fdd\u5b9a", "\u79e6\u7687\u5c9b", "\u5510\u5c71", "\u5f20\u5bb6\u53e3"],
	    py: ["shijiazhuang", "baoding", "qinhuangdao", "tangshan", "zhangjiakou"],
	    subway: []
	}, {
	    sz: "\u6c5f\u82cf",
	    text: ["\u5357\u4eac", "\u82cf\u5dde", "\u65e0\u9521", "\u5f90\u5dde", "\u5e38\u5dde", "\u626c\u5dde", "\u5357\u901a", "\u6cf0\u5dde", "\u9547\u6c5f", " \u5e38\u719f", "\u6606\u5c71"],
	    py: ["nanjing", "suzhou", "wuxi", "xvzhou", "changzhou", "yangzhou", "nantong", "taizhou", "zhenjiang", "changshu", "kunshan"],
	    subway: []
	}, {
	    sz: "\u8fbd\u5b81",
	    text: ["\u6c88\u9633", "\u5927\u8fde"],
	    py: ["shenyang", "dalian"],
	    subway: []
	}, {
	    sz: "\u5c71\u4e1c",
	    text: ["\u6d4e\u5357", "\u9752\u5c9b", "\u6f4d\u574a", "\u70df\u53f0", "\u5a01\u6d77", "\u6dc4\u535a", "\u6cf0\u5b89", "\u6ee8\u5dde", "\u5fb7\u5dde", "\u6d4e\u5b81", "\u4e34\u6c82"],
	    py: ["jinan", "qingdao", "weifang", "yantai", "weihai", "zibo", "taian", "binzhou", "dezhou", "jining", "linyi"],
	    subway: []
	}, {
	    sz: "\u5185\u8499\u53e4",
	    text: ["\u547c\u548c\u6d69\u7279", "\u5305\u5934"],
	    py: ["huhehaote", "baotou"],
	    subway: []
	}, {
	    sz: "\u6cb3\u5357",
	    text: ["\u90d1\u5dde", "\u6d1b\u9633", "\u5b89\u9633", "\u65b0\u4e61"],
	    py: ["zhengzhou", "luoyang", "anyang", "xinxiang"],
	    subway: []
	}, {
	    sz: "\u5409\u6797",
	    text: ["\u957f\u6625", "\u5409\u6797"],
	    py: ["changchun", "jilin"],
	    subway: []
	}, {
	    sz: "\u9ed1\u9f99\u6c5f",
	    text: ["\u54c8\u5c14\u6ee8"],
	    py: ["haerbin"],
	    subway: []
	}, {
	    sz: "\u5b89\u5fbd",
	    text: ["\u5408\u80a5", "\u868c\u57e0", "\u829c\u6e56"],
	    py: ["hefei", "bengbu", "wuhu"],
	    subway: []
	}, {
	    sz: "\u6c5f\u897f",
	    text: ["\u5357\u660c"],
	    py: ["nanchang"],
	    subway: []
	}, {
	    sz: "\u5c71\u897f",
	    text: ["\u592a\u539f", "\u5927\u540c"],
	    py: ["taiyuan", "datong"],
	    subway: []
	}, {
	    sz: "\u6e56\u5317",
	    text: ["\u6b66\u6c49", "\u5b9c\u660c", "\u8944\u9633"],
	    py: ["wuhan", "yichang", "xiangyang"],
	    subway: []
	}, {
	    sz: "\u6e56\u5357",
	    text: ["\u957f\u6c99", "\u8861\u9633", "\u6e58\u6f6d"],
	    py: ["changsha", "hengyang", "xiangtan"],
	    subway: []
	}, {
	    sz: "\u5e7f\u897f",
	    text: ["\u5357\u5b81", "\u6842\u6797", "\u5317\u6d77", "\u67f3\u5dde", "\u8d35\u6e2f"],
	    py: ["nanning", "guilin", "beihai", "liuzhou", "guigang"],
	    subway: []
	}, {
	    sz: "\u56db\u5ddd",
	    text: ["\u6210\u90fd", "\u7ef5\u9633"],
	    py: ["chengdu", "mianyang"],
	    subway: []
	}, {
	    sz: "\u8d35\u5dde",
	    text: ["\u8d35\u9633"],
	    py: ["guiyang"],
	    subway: []
	}, {
	    sz: "\u4e91\u5357",
	    text: ["\u6606\u660e", "\u4e3d\u6c5f"],
	    py: ["kunming", "lijiang"],
	    subway: []
	}, {
	    sz: "\u6d77\u5357",
	    text: ["\u6d77\u53e3", "\u4e09\u4e9a"],
	    py: ["haikou", "sanya"],
	    subway: []
	}, {
	    sz: "\u9655\u897f",
	    text: ["\u897f\u5b89", "\u54b8\u9633"],
	    py: ["xian", "xianyang"],
	    subway: []
	}, {
	    sz: "\u7518\u8083",
	    text: ["\u5170\u5dde"],
	    py: ["lanzhou"],
	    subway: []
	}, {
	    sz: "\u5b81\u590f",
	    text: ["\u94f6\u5ddd"],
	    py: ["yinchuan"],
	    subway: []
	}, {
	    sz: "\u9752\u6d77",
	    text: ["\u897f\u5b81"],
	    py: ["xining"],
	    subway: []
	}, {
	    sz: "\u65b0\u7586",
	    text: ["\u4e4c\u9c81\u6728\u9f50"],
	    py: ["wulumuqi"],
	    subway: []
	}];
$(function() {
    $(".city .pos_r").hover(function() {
        $(this).children(".pos_a").show();
//        列表页面打开以后在切换省市、拼音的时候右侧的下拉条会丢失，所以将以下代码进行屏蔽 updator: liyuzhao
//        $('.scroll-pane').jScrollPane(
//            {
//                verticalDragMinHeight: 84,
//                verticalDragMaxHeight: 84,
//                horizontalDragMinWidth: 11,
//                horizontalDragMaxWidth: 11
//            }
//        );
    }, function() {
        $(this).children(".pos_a").hide();
    });
    $(".tjcity .clearFix").html(city_nav("")), $(".dis_b .scrollContent").html(citylist(city1)), $(".qgcity .clearFix li").each(function(n, a) {
        $(a).hover(function() {
            $(this).addClass("over").siblings().removeClass("over"), $(".dis_b .scrollContent").html(citylist(0 == n ? city1 : city2))
        })
    })
});