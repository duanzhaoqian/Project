
var Release = {
    init: function (bargaining, startime, endtime) {
        $(".tag>input:[type=checkbox]").live("click", function () {
            var checked = $(this).attr("checked");
            if (!checked) {
                $(".price").attr("disabled", true);
                $(".price").attr("value", "");
                $(this).attr("checked", false)
                return;
            }
            $(".tag>input:[type=checkbox]").attr("checked", false);
            $(this).attr("checked", true);
            var num = parseInt($(this).val());
            if (num == 2) {
                $("#QuotedMaxPrice").attr("disabled", false);
                $("#QuotedMinPrice").attr("disabled", true);
                $("#QuotedMinPrice").attr("value", "");
            }
            else if (num == 3) {
                $("#QuotedMaxPrice").attr("value", "");
                $("#QuotedMaxPrice").attr("disabled", true);
                $("#QuotedMinPrice").attr("disabled", false);
            } else {
                $(".price").attr("disabled", true);
            }
            $(".price").attr("value", "");
        });

        if (bargaining != undefined && bargaining > 0) {
            //            $("#IsBargaining_" + bargaining).attr("checked", true);
            //            $(".p_bargaining>input:[type=checkbox]").attr("disabled", false);
            if (bargaining == 1) {
                $("#QuotedMaxPrice").attr("disabled", false);
                $("#QuotedMinPrice").attr("disabled", true);
            }
            else if (bargaining == 2) {
                $("#QuotedMaxPrice").attr("disabled", true);
                $("#QuotedMinPrice").attr("disabled", false);
            }
            //            Release.bidtime(bargaining, "tr_price", startime, endtime);
        }
    },
    auto: function (url, sender) {
        $(sender).autocomplete(url, {
            minChars: 0,
            width: 190,
            matchContains: true,
            highlightItem: false,
            cacheLength: 1,
            matchSubset: false,
            extraParams: { cid: function () { return $("#CityID").val(); }, r: function () { return Math.random(); } },
            formatItem: function (row, i, max) {
                row = eval("(" + row + ")"); //转换成js对象 
                if (row.result == "true") {
                    //                    alert("sss"+row.Name);
                    return '<span style ="cursor:pointer;">' + row.Name + '</span>';
                } else {
                    return '<span style ="cursor:pointer;">没有搜索到楼盘</span>';
                }
            },
            formatResult: function (row, i, max) {
                row = eval("(" + row + ")"); //转换成js对象 
                if (row.result == "true") {
                    return row.Name;
                } else {
                    return ' ';
                }
            }
        }).result(function (event, data, formatted) {
            row = eval("(" + data + ")"); //转换成js对象
            if (row.result == "true") {
                //$("#hidtxtacid").val(row.VID);
                var num = $(sender).closest("td");
                num.find("span").remove();
                num.append("<span for=\"VillageName\" class=\"error\"><i class=\"no\"></i></span>");
            } else {
                var num = $(sender).closest("td");
                num.find("span").remove();
                num.append("<span for=\"VillageName\" class=\"error\"><i class=\"no\">请选择楼盘。</i></span>");
            }
        });
    }
}