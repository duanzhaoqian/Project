<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $(".notification .close").click(function () {
            var $notification = $(this).closest(".notification")
                    .animate({ opacity: 0 }, { queue: false, duration: 800 })
                    .animate({ height: 0 }, 800, function () { $notification.remove(); });
            return false;
        });
    });

    // Url处理通用方法
    var UrlPathHelper = {
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
                url = UrlPathHelper.filterUrlParamsRepeat(url);
                return url;
            } else {
                url = path1 + "?" + path2;
                url = UrlPathHelper.filterUrlParamsRepeat(url);
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
    };
</script>