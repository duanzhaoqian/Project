/**
* public get request url function based on jQuery.  1.3
**/
var exp1 = /[p]\d+/g;
var exp2 = /[p](\d,){1,}/g;
var KYJ = {}
KYJ.CORE = {}
KYJ.CORE.URL = {
    stype: "",
    city: location.host.split('.')[0], //beijing
    pathname: location.pathname.substr(1, location.pathname.length), //xf/sub/xf/quyu
    qArray: location.pathname.substr(1, location.pathname.length).split("/"), //xf,sub
    query: new Array(),
    init: function () {
        if (this.qArray == undefined || this.qArray == "") { return; }
        this.qArray = this.pathname.split('/'); //xf,sub
        var lastArr = this.qArray[this.qArray.length - 1]; //qy
        this.query = lastArr.split('-'); //qy

        //上线的时候需要根据线上的URL规则修改
        this.city = this.city == "www" || this.city == "nhwww" ? "beijing" : this.city;
    },
    set: function (a, b, c, d, e) {
        //a:qArray数组第一个参数zufang/esf 默认无变化时：null
        //b:qArray数组第二个参数house/flats/villa/shop/office 
        //c:qArray最后一个参数传入需要新的参数 例如:q234 /quyu 等
        //d:用来区分c传入的值是搜索类型、区域/地铁线路还是商圈/站点 0:quyu/sub  1:区域/地铁值2:商圈/站点
        //e:区分字母 t100-u188,v12
        var temp = null, q = this.query.join('-').split('-');

        if (a == null) { a = this.qArray[0]; } //alert(a); //zufang/esf
        if (b == null) { b = this.qArray[1]; } //alert(b); //all
        if (c != null && c != "") {

            //alert(c);
            switch (d) {
                case 0: if (this.query[0] == "quyu" || this.query[0] == "sub") { q[0] = c; } else { q[0] = c + "-" + this.query[0]; } break;
                case 1:
                case 2:
                    var reg = RegExp(this.query[0] == "quyu" ? '^[a-z]{3,}' : '^[x|b]\\d{1,}');
                    if (d == 1) { if (reg.test(this.query[2])) q.del(this.query[2]); }
                    //                    var val = this.query[d];
                    //                    if (this.query[d] == null) { val = ""; }
                    if (!reg.test(this.query[d]) && reg.test(c) || this.query[d] == "") {
                        //alert(1);
                        q[d] = c + (this.query[d] == undefined || reg.test(this.query[d]) ? "" : ("-" + this.query[d])); //区域/地铁[不]存在 --- 商圈/站点[不]存在
                    } else { q[d] = c; }
                    break;
                case 3: var str = q.join('-');
                    var rz = '[0-9]{1,}';
                    //if (c == "") 
                    e = "-" + e;
                    //else e = e.replace("-", "");
                    var reg = RegExp(e + rz);
                    if (str.match(reg) == null) {
                        str = this.append(str, c); //不存在
                        q = str.split('-');
                    } else {
                        str = this.replace(reg, str, "-"+c); //存在
                        q = str.split('-');
                    } //alert(q);
                    break;
                case 4: var str = q.join('-');
                    var _c = e.split(",");
                    if (_c.length == 2) {
                        var one = _c[0], two = _c[1];
                        var reg = RegExp(one + '[0-9]{1,}-' + two + '[0-9]{1,}');
                        if (str.match(reg) == null) {
                            str = this.append(str, c); //dose not exist
                            q = str.split('-');
                        } else {
                            str = this.replace(reg, str, c); //exist
                            q = str.split('-');
                        }
                    }
                    break;
                case 5: var str = q.join('-');
                    var reg = RegExp(e + "(\\d?\\,?\\d)+", "g");
                    if (str.match(reg) == null) {
                        str = this.append(str, e + c); //dose not exist
                        q = str.split('-');
                    } else {
                        var arrs = str.match(reg)[0].match(/(\d?\,?\d)+/g)[0].split(',');
                        var flat = this.compare(arrs, c);
                        if (!flat) {
                            c = str.match(reg)[0] + "," + c;
                        } else {
                            reg = arrs.length == 1 ? (/\-p(\d?\,?\d)+/g) : reg; //only one item and the item need to remove
                            c = arrs.length == 1 ? "" : e + arrs.del(c).join(',');
                        }
                        str = this.replace(reg, str, c); //exist
                        q = str.split('-');
                    }
                    break; //checkbox method
            }
        } else {
            var reg = RegExp(this.query[0] == "quyu" ? '^[a-z]{4,}' : '^[x|b]\\d{1,}');
            if (d == 1) { if (reg.test(this.query[2])) q.del(this.query[2]); }
        }
        //var ky = q.join('-').match(/-k[%a-zA-Z0-9]+/g); //关键字参数移到最后

        var page = q.join('-').match(/-i\d{1,}/g); //分页
        return "/" + a + "/" + q.join('-').replace(page, "");  // + (ky == null ? "" : ky);

    },
    get: function (a, b) {
        var value = "";
        if (a == 0) { value = this.qArray[b]; }
        if (a == 1) { value = this.query[b]; }
        return value;
    },
    replace: function (regexp, str, value) {
        return str.replace(regexp, value); //正则替换
    },
    search: function (str, value) {
        return str.search("-" + value) == -1 ? false : true; //判断字符串里是否包含某个字符
    },
    append: function (defaultStr, value) {
        return defaultStr + "-" + value; //不存在时，追加参数
    },
    compare: function (arr, value) {
        var flat = false;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == value) { flat = true; break; }
        }
        return flat;
    }
}
KYJ.CORE.URL.init();



//租房 二手房 平台服务 切换
$(function () {
    switch (KYJ.CORE.URL.qArray[0]) {
        case "xinfang": $('.RPart ul li').eq(0).addClass("current").siblings().removeClass("current"); break;
        //case "esf": $('.RPart ul li').eq(1).addClass("current").siblings().removeClass("current"); break; 
        default: $('.RPart ul li').eq(2).addClass("current").siblings().removeClass("current");
    }
});