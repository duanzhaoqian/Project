
var admincoms = {

    // 等待提示
    WaittingBar: {
        show: function (msg) {
            $.blockUI({
                message: (msg == null ? "处理中，请稍后……" : msg),
                css: { padding: '15px' }
            });
        },

        close: function () {
            $.unblockUI();
        }
    },

    // 字符串处理
    StringHelper: {
        // 获取字节长度
        getByteCount: function (str) {
            return str.replace(/[^\x00-\xFF]/g, '**').length;
        },

        // 获取可输入剩余字节数
        getSurplusByteCount: function (totalByteCount, str) {
            var count = admincoms.getByteCount(str);
            return totalByteCount - count;
        },

        // 获取可输入的汉字数量
        getSurplusCount: function (totalStrCount, str) {
            var count = admincoms.getByteCount(str);
            return Math.round((totalStrCount * 2 - count) / 2);
        },

        // 按指定长度截取字符串
        cutByteString: function (totalByteCount, str) {
            var count = admincoms.StringHelper.getByteCount(str);
            if (count <= totalByteCount) {
                return str;
            }

            var newStr = "";
            for (var i = 0; i < str.length; i++) {
                var strByteCount = admincoms.StringHelper.getByteCount(str.charAt(i));
                if (admincoms.StringHelper.getByteCount(newStr) + strByteCount <= totalByteCount) {
                    newStr += str.charAt(i);
                }
            }

            return newStr;
        }

    },

    // Url地址处理
    UrlHelper: {
        // 清除url后的#锚点
        trimEndSharp: function (path) {
            if (path.lastIndexOf('#') > -1) {
                return path.substr(0, path.lastIndexOf('#'));
            }
            return path;
        },

        // 向主url后追加添加参数条件
        appendParams: function (path1, path2) {
            var url;
            if (path1.indexOf('?') > -1) {
                url = path1 + "&" + path2;
                url = admincoms.UrlHelper.filterUrlParamsRepeat(url);
                return url;
            } else {
                url = path1 + "?" + path2;
                url = admincoms.UrlHelper.filterUrlParamsRepeat(url);
                return url;
            }
        },

        // 过滤url中重复的参数
        filterUrlParamsRepeat: function (url) {
            var arrUrlParams = new Array();
            if (url.indexOf('?') > -1) {
                arrUrlParams = url.substring(url.indexOf('?') + 1, url.length).split('&');
            }

            var urlParamsJson = new Array();
            for (var i = 0; i < arrUrlParams.length; i++) {
                // 将参数按倒序排列
                urlParamsJson.splice(0, 0, { name: arrUrlParams[i].split('=')[0].toLowerCase(), value: arrUrlParams[i].split('=')[1] });
            }

            // 过滤重复参数，删掉原位置靠前的重复参数
            for (i = 0; i < urlParamsJson.length; i++) {
                for (var j = i + 1; j < urlParamsJson.length; ) {
                    if (urlParamsJson[i].name == urlParamsJson[j].name) {
                        urlParamsJson.splice(j, 1);
                    } else {
                        j++;
                    }
                }
            }

            var newUrl;
            if (url.indexOf('?') > -1) {
                newUrl = url.substring(0, url.indexOf('?'));
            } else {
                newUrl = url;
            }
            for (i = 0; i < urlParamsJson.length; i++) {
                newUrl += ((i == 0 ? "?" : "&") + urlParamsJson[i].name + "=" + urlParamsJson[i].value);
            }

            return newUrl;
        }
    }
};