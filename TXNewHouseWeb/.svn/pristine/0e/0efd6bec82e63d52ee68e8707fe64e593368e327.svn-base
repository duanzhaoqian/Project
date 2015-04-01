/**
* public search function based on jQuery.  1.3
**/

//搜索筛选条件生成的js插件
(function ($) {
    $.fn.KYJSearch = function (settings) {
        var $$ = KYJ.CORE.URL;
        settings = $.extend({ kyj: $$ }, $.fn.KYJSearch.defaultSettings, settings || {});
        jQuery.extend({ kyj: $$, hx: $$.qArray[0], wy: $$.qArray[1], ss: $$.query[0] });
        try {
            _search = new search(settings);
            $.fn.KYJSearch.load.base($$); //加载Base
            $.fn.KYJSearch.load.xinfang(); //加载新房条件
            $.fn.KYJSearch.load.xuanzetiaojian(); //加载搜索条件显示框
        } catch (e) { }
    }
    $.fn.KYJSearch.defaultSettings = {
        housetype: "xinfang" //新房
    }
    $.fn.KYJSearch.load = {
        base: function ($$) {
            //_search.init({ data: wylx, sort: 0, remove: [1, 4], cssname: ".pub_search .findwhat .thisconx", istitle: false });
            _search.init({ data: sousou[0], sort: 1, current: "active1", cssname: ".classify_tab", istitle: false }); //加载【区域，地铁标签】
            //_search.init({ data: sousou[1], sort: 1, current: "current quyu", cssname: ".fangshi", istitle: false }); //加载【地图搜索标签】
            if ($.ss == "quyu") {//按【区域】查询输出条件
                getdatas(quyu, KYJ.CORE.URL.city, 0, 0); //取【区域】
                _search.init({ title: "区域", data: quyu, sort: 6, current: "current thisbbb", cssname: $(".filterBar dl").eq(0) });
                if ($.kyj.query[1] != undefined && (/^[a-z]{4,}$/).test($.kyj.query[1])) {//$.kyj.query=quyu-xicheng-xisi
                    //alert((/^[a-z]{4,}$/).test($.kyj.query[1]) + "-" + $.kyj.query[1]);
                    getdatas(sangquan, KYJ.CORE.URL.city, 0, $.kyj.query[1]); //取【商圈】
                    _search.init({ data: sangquan, sort: 6, current: "current", cssname: $(".filterBar dl:first dd div"), istitle: false, k: 2 }).addClass("dddd");
                }
            } else {//按【地铁】查询输出条件

                var cityPy = KYJ.CORE.URL.city;
                if (cityPy == "beijing" || cityPy == "shanghai" || cityPy == "guangzhou" || cityPy == "shenzhen" || cityPy == "tianjin" || cityPy == "qingdao" || cityPy == "hangzhou" || cityPy == "chengdu" || cityPy == "nanjing" || cityPy == "dalian" || cityPy == "www") {
                    getdatas(sub, KYJ.CORE.URL.city, 1, 0); //取【地铁线】
                    _search.init({ title: "地铁", data: sub, model: 1, sort: 6, current: "current thisbbb", cssname: $(".filterBar dl").eq(0) });
                    if ((/^x{1,}/g).test($.kyj.query[1])) {
                        getdatas(station, KYJ.CORE.URL.city, 1, $.kyj.query[1].match(/\d+/g)[0]); //取【地铁站】
                        _search.init({ data: station, model: 1, sort: 6, current: "current", cssname: $(".filterBar dl:first dd div"), istitle: false, k: 2 }).addClass("dddd");
                    }
                }
            }

            getFeature();
            pxinit(); //初始化排序图标
            _search.init({ data: sx, sort: 3, current: "cur", cssname: ".bq-box", istitle: false /*, deletes:/-i\d{1,}/g*/ }); //输出导航
            var cityPy = KYJ.CORE.URL.city;
            if (cityPy == "beijing" || cityPy == "shanghai" || cityPy == "guangzhou" || cityPy == "shenzhen" || cityPy == "tianjin" || cityPy == "qingdao" || cityPy == "hangzhou" || cityPy == "chengdu" || cityPy == "nanjing" || cityPy == "dalian" || cityPy == "www") {

            } else {
                $(".title2").hide();
            }
        },
        xinfang: function () {
            var cityPy = KYJ.CORE.URL.city;
            _search.init({ title: "类型", data: leix, sort: 3, current: "current", cssname: $(".filterBar dl").eq(1) });
            _search.init({ title: "价格", data: price, sort: 3, current: "nolimit current", cssname: $(".filterBar dl").eq(2) });
            _search.init({ title: "户型", data: room, sort: 3, current: "current", cssname: $(".filterBar dl").eq(3) });
            _search.init({ title: "面积", data: area, sort: 3, current: "current", cssname: $(".filterBar dl").eq(4) });
            if (cityPy == "beijing" || cityPy == "www")
                _search.init({ title: "环线", data: loop_bj, sort: 3, current: "current", cssname: $(".filterBar dl").eq(5) });
            if (cityPy == "shanghai")
                _search.init({ title: "环线", data: loop_sh, sort: 3, current: "current", cssname: $(".filterBar dl").eq(5) });
            if (cityPy == "tianjin")
                _search.init({ title: "环线", data: loop_tj, sort: 3, current: "current", cssname: $(".filterBar dl").eq(5) });
            if (cityPy == "chengdu")
                _search.init({ title: "环线", data: loop_cd, sort: 3, current: "current", cssname: $(".filterBar dl").eq(5) });
            if (cityPy == "wuhan")
                _search.init({ title: "环线", data: loop_wh, sort: 3, current: "current", cssname: $(".filterBar dl").eq(5) });
            _search.init({ title: "建筑类别", data: jjlb, sort: 3, current: "current", cssname: $(".filterBar dl").eq(6) });
            _search.init({ title: "特色楼盘", data: tslp, sort: 3, current: "current", cssname: $(".filterBar dl").eq(7) });
            _search.init({ title: "开盘时间", data: kpsj, sort: 3, current: "current", cssname: $(".filterBar dl").eq(8) });
            _search.init({ title: "装修状况", data: jxzk, sort: 3, current: "current", cssname: $(".filterBar dl").eq(9) });
        },
        xuanzetiaojian: function () {
            var src = "http://" + location.hostname + location.pathname.split('-')[0];
            if (xztj.length > 0) {
                $("#tj").addClass("selected m3");
                $("#tj").html("<div class=\"floatl\">已选条件：</div><ul id=\"_tj\"></ul><div class=\"floatl\"><a href=\"" + src + "\" class=\"blue\">清空全部条件</a></div>");
            } else {
                $("#tj").removeClass();
            }
            $("#_tj").append(xztj.length == 0 ? "<div style=\"float:left;margin-bottom:10px;margin-right:15px;padding:3px 30px 3px 9px;position:relative;\">&nbsp;</div> " : xztj.join(""));
            //alert(location.href.substring(0, location.href.length));
            //alert(src);
            //$(".right").append(xztj.length == 0 ? "<div class=\"colsebox\">不限<a href=\"\"></a></div> " : xztj.join(""));
        }
    }
    function search(settings) {
        this.search = null;
        this.settings = settings;
    }
    search.prototype = {
        //加载搜索类型数据
        init: function (settings) {
            var defaultSettings = {
                title: null, //标题
                data: null, //数据
                model: 0, //0表示区域搜索 1标示地铁搜索
                sort: 0, //区分模块分类
                current: null, //当前选中样式
                remove: null, //[0,1]data中index 不包含项
                cssname: null, //父级标签样式，用来填充数据的父级标签
                defaultval: null,
                isbx: true, //是否存在不限
                istitle: true, //是否有标题
                deletes: null, //移除某个参数 比如 -i2 分页
                k: 1 //1=区域/地铁 2：商圈/站点 set query array with index
            };
            settings = $.extend({}, defaultSettings, settings || {});
            _$ = settings;
            if (_$.data == null) { return; }
            var html = [], value = null, param = null, m = "", n = "", flat = true, paramStr = $.kyj.query.join("-");
            var nav_href = "/" + $.kyj.pathname, nar_href_arrs = nav_href.substring(1, nav_href.length).split('/'); //nav  href ;arrs

            for (var i = 0; i < _$.data.text.length; i++) {
                flat = true;
                if (_$.remove != null) {
                    for (var j = 0; j < _$.remove.length; j++) {
                        if (i == _$.remove[j]) { flat = false; } //排除项
                    }
                }
                if (flat) {
                    switch (_$.sort) {
                        case 0: m = "id=\"" + _$.data.value[i] + "\"";
                            nar_href_arrs[1] = _$.data.value[i]; //change current housetype=[wylx]
                            html.push("<a " + m + " href=\"/" + nar_href_arrs.join('/') + "\"" + n + ">找" + _$.data.text[i] + "</a>");
                            break;
                        case 1:
                            if (i == 0) {
                                n = $.ss == _$.data.value[i] ? " class=\"active1\"" : " class=\"title1\"";
                            }
                            if (i == 1) {

                                n = $.ss == _$.data.value[i] ? " class=\"active2\"" : " class=\"title2\"";

                            }
                            m = $.kyj.pathname.match($.hx == "xinfang");
                            param = $.kyj.pathname.split("-")[0].split("/");
                            param[1] = _$.data.value[i];
                            html.push("<li " + n + "><a  href=\"/" + param.join('/') + "\" >" + _$.data.text[i] + "</a></li>");

                            break; //区域搜索/地铁搜索切换
                        case 2: m = _$.data.prefix.split(',');
                            param = this.params(m, _$.data.value[i].split(',')); //拼接字符
                            reg = new RegExp(m[0] + "\\d{1,}-" + m[1] + "\\d{1,}");
                            value = paramStr.match(reg);
                            n = param == value ? " class=\"" + _$.current + "\"" : ""; //选中项
                            param = param == null ? "" : param;
                            var bx = _$.data.text[i] == "不限";
                            var let, text
                            if (m.length == 2) {
                                let = bx ? false : RegExp('[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+').test(_$.data.text[i]), lv = null; //check china
                                if (let) lv = RegExp('[\\u4E00-\\u9FA5\\uF900-\\uFA2D]+').exec(_$.data.text[i])[0];
                                text = let ? RegExp('^[0-9]+').exec(_$.data.text[i])[0] + _$.data.unit + lv : _$.data.text[i] + (bx ? "" : _$.data.unit);
                            } else { text = _$.data.text[i]; }
                            var _href = $.kyj.set(null, null, param, 4, _$.data.prefix);
                            html.push("<a href=\"" + (i == 0 && _$.isbx ? _href.replace('-' + value, '') : _href) + "\"" + n + ">" + text + "</a>");
                            break; //tow letters
                        case 3:
                            var prefix = _$.data.prefix;
                            param = this.params(prefix, _$.data.value[i]); //拼接字符
                            var reg = "[0-9]{1,}";
                            value = RegExp("[-]" + prefix + reg).exec(paramStr);
                            //var v = RegExp(prefix + reg).exec(paramStr);
                            //设置显示选择的搜索条件-（类型 到 装修状况）部分
                            if (param == RegExp(prefix + reg).exec(value) && _$.data.text[i] != "不限" && _$.data.text[i] != "全部楼盘") {
                                n = " class=\"" + _$.current + "\"";
                                if (_$.data.text[i] != "在售楼盘" && _$.data.text[i] != "待售楼盘" && _$.data.text[i] != "售罄楼盘") {
                                    xztj.push("<li><a href=\"" + $.kyj.set(null, null, param, 3, prefix).replace((value), '') + "\" class=\"off\">" + _$.data.text[i] + "</a></li>");
                                }
                            }
                            //alert(param);
                            n = param == RegExp(prefix + reg).exec(value) ? " class=\"" + _$.current + "\"" : ""; //选中项
                            param = param == null ? "" : param;
                            var _href = $.kyj.set(null, null, param, 3, prefix); //生成url
                            if (_$.deletes != null) _href = _href.replace(_href.match(_$.deletes), ""); //排除分页

                            html.push("<a href=\"" + (i == 0 && _$.data.value[i] == "" ? _href.replace((value), '') : _href) + "\"" + n + ">" + _$.data.text[i] + "</a>");
                            if (prefix == 'p')
                                if (_$.data.value.length == i + 1)
                                    html.push("<span class=\"\">单价(元/㎡)</span>");
                            break; //single letter
                        case 4: //多选框  
                            var reg = new RegExp(_$.data.prefix + "(\\d?\\,?\\d)+", "g");
                            value = paramStr.match(reg), checked = "";
                            if (i == 0 && _$.data.value[i] == "") { //值=空 
                                if (value == null) {
                                    html.push("<a href=\"" + $.kyj.pathname + "\"" + n + ">" + _$.data.text[i] + "</a>");
                                } else {
                                    html.push("<a href=\"/" + $.kyj.pathname.replace("-" + value[0], "") + "\"" + n + ">" + _$.data.text[i] + "</a>");
                                }
                            } else {
                                value = value != null ? value[0] : null;
                                value = value != null ? value.match(/(\d?\,?\d)+/g)[0].split(',') : null;
                                checked = value != null ? this.compare(value, _$.data.value[i]) ? "checked" : "" : "";
                                html.push("<input type=\"checkbox\" onclick=\"location.href='" + $.kyj.set(null, null, _$.data.value[i], 5, _$.data.prefix) + "'\" name=\"zhengzu\" " + checked + " value=\"" + _$.data.text[i] + "\"/><label>" + _$.data.text[i] + "</label>");
                            }
                            break;
                        case 5: //select 排序 装修
                            var param = _$.data.prefix + _$.data.value[i];
                            var cur_value = this.selected(_$.data); cur_value = cur_value == null ? _$.defaultval : cur_value;
                            var val = $.kyj.set(null, null, param, 3, _$.data.prefix)
                            if (i == 0) html.push("选择排序: <a href=\"#\" class=\"cur nopr\">默认排序</a>");
                            val = i == 0 ? val.replace(('-' + param), '') : val;
                            html.push("<a  href=\"" + val + "\">" + _$.data.text[i] + "</a>");
                            if (i == _$.data.text.length - 1) html.push("</div>");
                            break;
                        case 6: //区域商圈 地铁站点
                            m = _$.data.prefix != "" ? _$.data.prefix : "";

                            //设置显示选择的搜索条件-区域、商圈部分
                            if ($.kyj.query[_$.k] == m + _$.data.value[i]) {
                                xztj.push("<li><a href=\"" + $.kyj.set(null, null, null, _$.k, m).replace("-" + $.kyj.query[_$.k], "") + "\" class=\"off\">" + _$.data.text[i] + "</a></li>");
                            }

                            n = $.kyj.query[_$.k] == m + _$.data.value[i] ? " class=\"" + _$.current + "\"" : "";
                            if ($.kyj.query.length - 1 >= _$.k) {
                                //0表示区域搜索 1标示地铁搜索 $.kyj.query[0]= sub/quyu  [1]=xicheng [2]=xisi
                                var val = $.kyj.query[_$.k];
                                value = $.kyj.query[_$.k].match(_$.model == 0 ? /^[a-z]{3,}$/ : /[x|b]\d{1,}/g);
                                //model:0表示区域搜索 1标示地铁搜索
                            }
                            n = value == null && i == 0 ? "class=\"current\"" : n;
                            cur_value = _$.data.value[i] == "" ? null : m + _$.data.value[i];
                            var url = $.kyj.set(null, null, cur_value, _$.k, m);
                            //a:qArray数组第一个参数zufang/esf 默认无变化时：null
                            //b:qArray数组第二个参数house/flats/villa/shop/office 
                            //c:qArray最后一个参数传入需要新的参数 例如:q234 /quyu 等
                            //d:用来区分c传入的值是搜索类型、区域/地铁线路还是商圈/站点 0:quyu/sub  1:区域/地铁值2:商圈/站点
                            //e:区分字母 t100-u188,v12
                            html.push("<a " + m + " href=\"" + (i == 0 && _$.data.value[i] == "" && value != null ? url.replace("-" + $.kyj.query[_$.k], "") : url) + "\"" + n + ">" + _$.data.text[i] + "</a>");
                            //alert("<a " + m + " href=\"" + (i == 0 && _$.data.value[i] == "" && value != null ? url.replace("-" + $.kyj.query[_$.k], "") : url) + "\"" + n + ">" + _$.data.text[i] + "</a>");
                            break;

                    }
                }
            }
            if (_$.istitle) {
                flat = (_$.title == "区域" || _$.title == "地铁");
                var css = flat ? " class=\"autoKeepAll\"" : "";
                html = "<dt>" + _$.title + "：</dt><dd" + css + ">" + html.join('') + (flat ? "<div class=\"\"></div>" : "") + "</dd>"
            } else {
                html = html.join('');
            }

            $(_$.cssname).append(html);

            //if (_$.sort == 0) { $(".findwhat > .btn").html($("#" + KYJ.CORE.URL.get(0, 1)).html()); /*seleted物业类型*/ }
            return $(_$.cssname);
        },
        //拼接字符，prefix:标识，value:值
        params: function (prefix, value) {
            if (value == "") {
                return null;
            } else {
                if (prefix.length == 1) {
                    return prefix + value;
                }
                if (prefix.length == 2) {
                    return prefix[0] + value[0] + "-" + prefix[1] + value[1];
                }
            }
        },
        compare: function (arr, value) {
            var flat = false;
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] == value) { flat = true; break; }
            }
            return flat;
        },
        selected: function (arrs) {
            var val = null, value = null;
            for (var i = 0; i < arrs.text.length; i++) {
                value = arrs.value[i] == null || arrs.value[i] == "" ? null : arrs.prefix + arrs.value[i];
                if ($.kyj.search($.kyj.qArray[0], value)) {//判断字符串里是否包含某个字符
                    val = arrs.text[i]; break;
                }
            }
            return val; //get selected text
        }
    }
})(jQuery);

$.fn.KYJSearch({ housetype: KYJ.CORE.URL.qArray[0] });

//nav change selected
function nav_css(i, o) {
    $('.user_nav .w1190 a').eq(i).addClass("current").siblings().removeClass("current");
}

//默认排序
$("#mr").click(function () {
    n = "class=\"cur nopr\"";
    var m = $.kyj.qArray[1];
    var s = m.split("-");
    var p = "";
    for (var i = 0; i < s.length; i++) {
        var value = s[i].match(/[y]\d{1,}/g);
        if (!value) {
            if (i < s.length) {
                i == 0 ? p += s[i] : p += "-" + s[i];
            }
        }
    }
    window.location.href = p;
});

//售价排序：z1高到低（降），z2低到高（升）
$("#sj").click(function () {
    px('sj', 'y2', 'y1');
});

//开盘排序：z3高到低（降），z4低到高（升）
$("#kp").click(function () {
    px('kp', 'y4', 'y3');
});

//开盘排序：z5高到低（降），z6低到高（升）
$("#rz").click(function () {
    px('rz', 'y6', 'y5');
});

//排序：a:函数名，b:升序标识，c：降序标识
function px(a, b, c) {
    var m = $.kyj.pathname.split('/')[1];
    var s = m.split("-");
    var p = "";
    for (var i = 0; i < s.length; i++) {
        var value = s[i].match(/[y]\d{1,}/g);
        if (value) {
            if (s[i] == c) {
                i == 0 ? p += b : p += "-" + b;
            } else {
                i == 0 ? p += c : p += "-" + c;
            }
        } else {
            i == 0 ? p += s[i] : p += "-" + s[i];
            if (m.indexOf(c) < 0 && m.indexOf(b) < 0) {
                if (i == s.length - 1) { p += "-" + c; }
            }
        }
    }
    window.location.href = p;
}

//初始化
function pxinit() {
    var m = $.kyj.pathname.split('/')[1];
    var s = m.split("-");
    var p = "";
    $("#_sj").attr("class", "zt2"); $("#_sj").attr("title", "升序");
    $("#_kp").attr("class", "zt2"); $("#_kp").attr("title", "升序");
    $("#_rz").attr("class", "zt2"); $("#_rz").attr("title", "升序");
    for (var i = 0; i < s.length; i++) {
        var value = s[i].match(/[y]\d{1,}/g);
        if (value) {
            if (s[i] == 'y1') { $("#_sj").attr("class", "zt1"); $("#_sj").attr("title", "降序"); }
            if (s[i] == 'y2') { $("#_sj").attr("class", "zt2"); $("#_sj").attr("title", "升序"); }
            if (s[i] == 'y3') { $("#_kp").attr("class", "zt1"); $("#_kp").attr("title", "降序"); }
            if (s[i] == 'y4') { $("#_kp").attr("class", "zt2"); $("#_kp").attr("title", "升序"); }
            if (s[i] == 'y5') { $("#_rz").attr("class", "zt1"); $("#_rz").attr("title", "降序"); }
            if (s[i] == 'y6') { $("#_rz").attr("class", "zt2"); $("#_rz").attr("title", "升序"); }
        }
    }
}
//搜索函数
function SearchList() {
    var k = location.href.match(/-w_[%a-zA-Z0-9]+/g);
    var i = location.href.match(/-i[0-9]+/g);
    var u = $.trim(FilterSpecialCharacter($("#search_key").val()).replaceAll("·", "я").replaceAll("•", "ю"));
    if (u != "请输入楼盘名或具体位置") {
        if (k != null && k != "") {

            location.href = location.href.replace("#list", "").replace(i, "").replace(k, (u == "" ? "" : "-w_" + encodeURI(u)));
        }
        else {
            location.href = location.href.replace("#list", "").replace(i, "") + (u == "" ? "" : "-w_" + encodeURI(u));
        }
    }
    else {
        //я
        location.href = location.href.replace("#list", "").replace(k, "").replace(i, "");
    }
}
//搜索框
$("#search_key").focus(function () {
    var key = $("#search_key").val();
    if (key == "请输入楼盘名/或具体位置...") $("#search_key").val("");
});
//搜索框
$("#search_key").blur(function () {
    var key = $("#search_key").val();
    if (key == "") $("#search_key").val("请输入楼盘名/或具体位置...");
});
//按钮搜索
$("#btn_search").click(function () {
    SearchList();
});

//回车搜索
$(document).keydown(function (e) {
    if (e.keyCode == 13 && $(e.target).is("#search_key")) {
        SearchList();
    }
});

//重新给搜索框赋值
var key = location.href.match(/-w_[%a-zA-Z0-9]+/g);
key = key == undefined || key == null ? "" : key.toString().replace("-w_", "");
if (key != null && key != "") $("#search_key").val(decodeURI(key).replaceAll("я", "·").replaceAll("ю", "•"));

//自动填充=======================================
(function ($) {
    $.fn.AutoComplete = function (settings) {
        settings = $.extend({}, $.fn.AutoComplete.defautSetting, settings || {});
        settings.dataUrl = settings.dataUrl;
        var elem = $(this);
        $(this).autocomplete(settings.dataUrl, {
            //width: elem.parent()[0].scrollWidth,
            width: 214,
            max: 10, //显示数据条数
            minChars: 1, //在触发autoComplete前用户至少需要输入的字符数 默认为0 在输入框内双击或者删除输入框内内容时显示列表
            matchCase: false,
            matchContains: "word", //只要包含输入字符就会显示提示
            autoFill: false, //自动填充输入框  
            scroll: false, //不显示滚动条
            dataType: "jsonp",
            extraParams: {
                keyword: function () { return elem.val(); },
                cid: 1,
                //cityid: function () { return "" },
                pagesize: 10,
                returntype: 'json',
                typeid: function () { return 0 }
            },
            parse: function (data) {
                var rows = [];
                for (var i = 0; i < data.length; i++) {
                    rows[rows.length] = {
                        data: data[i], //每条数据对象 
                        value: $.trim(data[i].Name), //与输入的值比较的数据
                        result: $.trim(data[i].Name)  //选中的实际数据
                    };
                }
                return rows;
            },
            formatItem: function (row, i, max) {
                $(".pub_checkListC").hide();
                return "<span class='ac_name'>" + row.Name + "</span>";
            },
            formatMatch: function (row, i, max) {
                return row.Name + " " + row.Id;
            },
            formatResult: function (row) {
                return $.trim(row.Id);
            }
        }).result(function (event, data, formatted) {
            if (settings.isAddAtribute) {
                elem.attr("lang", settings.type == "city" ? data.Id : data.Name);
                elem.attr("data", settings.type == "city" ? data.pyin : data.Name);
            }
        });
    }
    $.fn.AutoComplete.defautSetting = {
        dataUrl: "",
        type: "city",
        isAddAtribute: false
    }
})(jQuery);

//物业类型显示
//$(".findwhat").mouseover(function () { $(this).addClass("adborder").children(".thisconx").addClass("adblock"); }).mouseout(function () { $(this).removeClass("adborder").children(".thisconx").removeClass("adblock"); });
//排序、装修
$(".groupsthis").mouseover(function () { $(this).find(".thisconx").show(); }).mouseout(function () { $(this).find(".thisconx").hide(); });

//浮动分页隐藏
var currentp = location.href.match(/-i[1-9]+/g);
if (currentp == null || currentp == "-i1") { $("#prev > a").hide(); } //第一页隐藏上一页
if ($(".getR1").attr("href") == undefined) { $("#next > a").hide(); } //当最后一页隐藏下一页


