// JavaScript Document
//*发布楼盘-楼盘相册切换
$(function () {
    $(function () {
        var $span_a = $(".choose span a");
        $span_a.click(function () {
            $(this).addClass("cur")
				   .siblings().removeClass("cur");
            var index = $span_a.index(this);
            $("#ddivd > div")
					.eq(index).show()
					.siblings().hide();
        })
    })

   
    $(".big_img a.close").click(function () {
        $("div.big_img").hide();
        return false;
    });

})