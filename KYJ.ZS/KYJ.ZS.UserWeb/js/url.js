/**
* public get request url function based on jQuery.  1.3
**/
var exp1 = /[p]\d+/g;
var exp2 = /[p](\d,){1,}/g;
var KYJ = {}
KYJ.CORE = {}
KYJ.CORE.URL = {
    stype: "",
    pathname: location.pathname.substr(1, location.pathname.length),
    qArray: location.pathname.substr(1, location.pathname.length).split("/"),
    query: new Array(),
    init: function () {
        if (this.qArray == undefined || this.qArray == "") { return; }
        this.qArray = this.pathname.split('/');
        var lastArr = this.qArray[this.qArray.length - 1];
        this.query = lastArr.split('-');
    },
    set: function (a, b, c, d, e) {
        var temp = null, q = this.query.join('-').split('-');

        //        if (a == null) { a = this.qArray[0]; }
        if (b == null) { b = this.qArray[1]; }
        if (c != null && c != "") {

            switch (d) {
                case 1: var str = q.join('-');
                    var reg = RegExp(e + "(\\d{1,}[_]\\d{1,})");
                    var hasattr = false;
                    var tempattr;
                    //如果URL没有该条件分类
                    if (str.indexOf(e + c) > 0) {

                    }
                    else {
                        var attr = e + "(" + c.split('_')[0] + ")[_]\\d{1,}";
                        if (str.match(RegExp(attr)) == null)
                            str += "-" + e + c;
                        else
                            str = str.replace(RegExp(attr), e + c);

                    }
                    q = str.split('-');
                    break;
                case 2: var str = q.join('-');
                    var reg = RegExp(e + "(\\d{1,}[_]\\d{1,}[,]{0,1})+", "g");
                    if (str.match(reg) == null) {
                        str = this.append(str, e + c); //不存在
                        q = str.split('-');
                    } else {
                        str = this.replace(reg, str, e + c)
                        q = str.split('-');
                    }
                    break;
                case 3: var str = q.join('-');
                    var reg = RegExp(e + "(\\d{1,})+", "g");
                    if (str.match(reg) == null) {
                        str = this.append(str, e + c); //不存在
                        q = str.split('-');
                    } else {
                        str = this.replace(reg, str, e + c)
                        q = str.split('-');
                    }
                    break;
                case 4: var str = q.join('-');
                    str = e + c;
                    q = str.split('-');
                    break;
                case 5: var str = q.join('-');
                    var reg = RegExp("^[a-z]{2,}");
                    if (reg.test(this.query[0]) && this.query[0] != undefined) {
                        str = this.query[0] + "-" + e + c;
                        q = str.split('-');
                    }
                    else {
                        str = e + c;
                        q = str.split('-');
                    }
                    break;
            }
        } else {

        }

        var page = q.join('-').match(/-i\d{1,}/g); //分页
        if (a != null) {
            return a + "/" + q.join('-').replace(page, "");
        }
        else {
            return "/" + this.qArray[0] + "/" + q.join('-').replace(page, "");
        }

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

