//新房JS全局变量
var globalvar = {
    //siteRoot: ""//本地使用
    siteRoot: "/xinfang", //测试使用
    dialog: function (result, fun) {
        $.freeDialog.open({ content: '<div style="text-align:center">' + result + '</div>', title: '提示', height: 120, width: 400, allowClose: true, isResize: true, buttonSite: 230, buttons: [{ text: '关闭', onclick: function (item, dialog) { if (fun) { fun(); } else { dialog.close(); } } }] });
    },
    domain:".kyjob.com"
};
