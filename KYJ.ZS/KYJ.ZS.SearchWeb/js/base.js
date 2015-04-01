//图片延迟加载
$(function () {
    $("img").lazyload({ effect: "fadeIn", event: "sporty" });
    $("img").trigger("sporty");

    //$("img").lazyload({ effect: "fadeIn", effectspeed: 5000, skip_invisible: false, threshold: 200 });
})
