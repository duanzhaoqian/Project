/*  
    作者：马谦
    联系方式：
    日期：2014年1月21日 2：38-
    说明：JP JiePan 来自   睫畔
*/
(function (window, undefined) {
   


    var JiePan = {
        //作者：马谦
        //时间：2014-03-05 15:19
        Messager: {
            Error: function (errorInfo, backFun) {
                $.messager.alert("错误提示", errorInfo, "error", function () {
                    if (backFun) {
                        backFun();
                    }
                });
            },
            Info: function (msg, backFun) {
                if (backFun) {
                    backFun();
                }
            }
        },
        //作者：马谦
        //时间：2014-03-03 14:30
        BackMessage: function (url, data, successFun, failedFun) {
            $.post(url, data, function (backData) {
                if (backData.Success) {//成功时的操作
                    $.messager.alert("提示信息", backData.Message, "info", function () {
                        if (backData.Action == 1) {//如果action=1则重定向
                            location.href = backData.StrUrl;
                        }
                        if (successFun) {//成功是调用回调函数
                            successFun();
                        }
                    });

                } else {//失败时的操作

                    $.messager.alert("错误提示", backData.Message, "error", function () {
                        if (failedFun) {//失败时调用回调函数
                            failedFun();
                        }
                    });

                }
            }, "json");
        },
        //作者：马谦
        //日期：2014年1月21日 2：38
        //功能：操作字符串的一些方法
        //说明：
        String: {
            //得到字符串的实际长度
            GetStrLength: function (str) {
                var realLength = 0, len = str.length, charCode = -1;
                for (var i = 0; i < len; i++) {
                    charCode = str.charCodeAt(i);
                    if (charCode >= 0 && charCode <= 128) { realLength += 1; }
                    else { realLength += 2; }
                }
                return realLength;
            }
        },
        //
        Convert: {
            //将.net序列化之后的时间转换成本地时间
            UTCToLocalDate: function (datetime) {
                var num = datetime.replace(/\/Date\((\d+)\)\//gi, '$1');
                var n = Number(num);
                var d = new Date(n);
                return d.toLocaleDateString() + ' ' + d.toLocaleTimeString();
            },
            //将json转换成object
            JsonToObj: function (json) {
                return eval("(" + json + ")");
            }
        },
        //作者：蒋坤
        //日期：2013-8-26 02:04:01
        //功能：模拟Csharp中Regex对象的静态方法
        //说明：该对象提供四个方法，isMatch，match，matches，replace
        //      1.isMatch
        //          语法：boolean jkr.isMatch(正则表达式, 字符串);
        //          功能：检查是否匹配，匹配上返回true
        //      2.match
        //          语法：object jkr.match(正则表达式, 字符串);
        //          功能：将字符串中匹配到的第一个字符串提取出来
        //          返回值：
        //              success	：属性，表示是否匹配成功
        //			    index	：属性，匹配字符串在原始字符串中的位置
        //				value	：属性，匹配到的字符串
        //				length	：属性，匹配到的字符串的长度
        //				groups	：方法，根据索引获得匹配到的组
        //				groups.count：属性，匹配到组的个数
        //      3.matches
        //          语法：Array jkr.matches(正则表达式, 字符串);
        //          功能：将循环匹配字符串，并将匹配到的数据以数组的形式返回，数组的每一个成员具有
        //          返回值：
        //              success	:属性，表示是否匹配成功
        //			    index	:属性，匹配字符串在原始字符串中的位置
        //				value	:属性，匹配到的字符串
        //				length	:属性，匹配到的字符串的长度
        //			    groups.count:属性，匹配到组的个数
        //      4.replace
        //          语法：string jkr.replace(原始字符串, 正则表达式, 新字符串);
        //          功能：该方法对原始字符串使用正则表达式进行处理，并将匹配到的数据用新的字符串代替
        Regex: {
            isMatch: function (regex, input) {
                // 匹配的方法，返回boolean类型
                if (!(regex instanceof RegExp) || typeof input !== 'string') /* throw {msg:"参数不正确"}; // 考虑使用异常*/ return false;
                return regex.test(input);
            },
            match: function (regex, input) {
                // 匹配提取，将结果返回
                if (!(regex instanceof RegExp) || typeof input !== 'string') throw { msg: "参数不正确" };
                var m = regex.exec(input);
                // 封装一个match对象，里面有一个groups集合，和value属性以及index属性与length属性等
                var _m = {
                    success: !!m, // 如果没有匹配m为null
                    index: m ? m.index : 0,
                    length: m ? m[0].length : 0,
                    value: m ? m[0] : null,
                    groups: function (i) {
                        if (!m) return [];
                        if (i > m.length - 1) throw { msg: "超出数组索引范围" };
                        return m[i];
                    }
                };
                _m.groups.count = m ? m.length : 0;
                return _m;
            },
            matches: function (regex, input) {
                // 匹配所有内容返回matchCollection
                if (!(regex instanceof RegExp) || typeof input !== 'string') throw { msg: "参数不正确" };
                // 保证支持全局模式
                var r = regex.toString();
                var that = this;
                regex = this.isMatch(/g[^\/]*$/i, r)
                            ? regex
                            : (function () {
                                var m = that.match(/\/([^\/]+)\/(.*)/, r);
                                return new RegExp(m.groups(1), m.groups(2) + "g");
                            })();
                // 准备集合
                var matchCollection = [];
                // 循环匹配装配集合
                do {
                    var temp = this.match(regex, input);
                    matchCollection[matchCollection.length] = temp;
                } while (temp.success);
                return matchCollection;
            },
            replace: function (oldStr, regex, newStr) {
                if (typeof oldStr === 'string' && typeof newStr === 'string' && regex instanceof RegExp) {
                    return oldStr.replace(regex, newStr);
                } else {
                    throw { msg: "参数信息错误" };
                }
            }
        }
    };
    window.JiePan = window.JP = JiePan;
})(window);
