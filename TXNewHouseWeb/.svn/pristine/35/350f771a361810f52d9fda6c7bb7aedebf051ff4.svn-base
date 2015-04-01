//下拉列表级联效果
var loadGeographyItems = function (url, container, onComplete) {
    $.getJSON(url, function (response) {
        if (response && response.success && container) {
            fillListItems(container, response.items);
            if (onComplete) {
                onComplete(container);
            }
        }
    });
};
//下拉列表级联效果,换线数据为空的话就隐藏
var loadGeographyHideEmptyItems = function (url, container, onComplete) {
    $.getJSON(url, function (response) {
        if (response && response.success && container) {
            if (response.items && response.items.length > 0) {
                container.closest("tr").show();
                fillListItemsValue(container, response.items);
                if (onComplete) {
                    onComplete(container);
                }
            } else
                container.closest("tr").hide();
        }
    });
};

var clearListItems = function (container) {
    $(container).find(":first").nextAll().remove();
};

var fillListItems = function (container, dataItems) {
    if (dataItems) {
        $.each(dataItems, function () {
            $("<option value='" + this.GeographyCode + "' lat='" + this.GeographyLat + "' lng='" + this.GeographyLng + "'  data='" + this.GeographyName + "' >" + this.GeographyName + "</option>")
                                .appendTo(container);
        });
    }
};
var fillListItemsValue = function (container, dataItems) {
    if (dataItems) {
        $.each(dataItems, function () {

            $("<option value='" + this.Key + "' data='" + this.Value + "' >" + this.Value + "</option>")
                                .appendTo(container);
        });
    }
};
//分页插件通用部分导入

//下拉列表级联效果
var loadSelectedGeographyItems = function (url, container, selected, onComplete) {
    $.getJSON(url, function (response) {
        if (response && response.success && container) {
            fillSelectedListItems(container, selected, response.items);
            if (onComplete) {
                onComplete(container);
            }
        }
    });
};

var fillSelectedListItems = function (container, selected, dataItems) {
    if (dataItems) {
        $.each(dataItems, function () {
            var select = (selected == this.GeographyCode) ? "selected='selected'" : "";
            $("<option " + select + " value='" + this.GeographyCode + "' lat='" + this.GeographyLat + "' lng='" + this.GeographyLng + "'  data='" + this.GeographyName + "' >" + this.GeographyName + "</option>")
                                .appendTo(container);
        });
    }
};

