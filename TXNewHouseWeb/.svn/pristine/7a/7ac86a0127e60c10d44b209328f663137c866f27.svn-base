$(function() {
    //demo 详细页评论区域 查找除第一个之外的li 删除topborder
    $('.detailContent .pjMsgList ul li:lt(1)').addClass('addTopborder');

    //demo
    $(".w_layer .close").click(function() {
        $(".w_layer ").hide();
    });

    //demo
    $('.webMainA .left .list-content .listView:nth-child(3n)').addClass('none');

    //demo
    $('.pub_listA .listView:nth-child(3n)').addClass('none');

    //demo
    $('.indexUse .pub_listC li:lt(4)').addClass('top4');

    //demo	
    $(".search_web .state").click(function() {
        $(".pub_checkListC").hide();
        $(this).addClass("cur");
        $(this).children(".pub_checkListC").show();
    });
    $(".pub_checkListC .close").click(function() {
        $(this).parent().parent().hide();
    });

    //accordion手风琴
    $('.accordion-content').css('display', 'none');
    $('.accordion-header').addClass('common_accordion');
    $('.common_accordion').click(function() {
        var sibling = $(this).siblings('.accordion-content');
        var flag = (sibling.css('display') == 'block') ? true : false;
        if (flag) {
            //alert("叠起")
            $(this).removeClass('accordion-header-active').addClass('accordion-header');
            sibling.slideUp();
        } else {
            //alert("展开")
            $(this).removeClass('accordion-header').addClass('accordion-header-active');
            sibling.slideDown();
        }
        //event.stopPropagation();
    });
    //查找并显示当前手风琴
    $(".accordion_box").find(".current").parent('.accordion-content').css('display', 'block');
    //查找并改变当前header样式
    $(".accordion_box").find(".current").parent().parent().find(".accordion-header").addClass('accordion-header-active');

    //滑过变色
    $("tbody tr").hover(function() { $(this).addClass("eqc") }, function() { $(this).removeClass("eqc"); }); //鼠标移动改变背景色

    //房源管理修改鼠标移动改变背景色
    $(".ac_results li").mouseover(function() {
        $(this).css("background-color", "#F7F7F7");
    });
    $(".ac_results li").mouseout(function() {
        $(this).css("background-color", "#FFFFFF");
    });
});