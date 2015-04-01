$(function() {
    //验证是否为数字
    $("[rule=num]").live("blur",(function () {
        var value = $(this).val();
        var tt = isNaN(value);
        if (tt == true) {
            JP.Messager.Error("请输入数字！");
            $(this).val("");
        }
    }));
});