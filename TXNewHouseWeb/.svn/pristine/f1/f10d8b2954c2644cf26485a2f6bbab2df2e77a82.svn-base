$(function () {
    $('.pub_nav .RPart ul li').hover(function () { $(this).toggleClass('hover') });
    //找住宅
    $('.findwhat .btn').hover(function () { $(this).parent().children(".thisconx").addClass('adblock'); $(this).parent(".findwhat").addClass('adborder'); });
    $('.findwhat .thisconx a').click(function () { $(this).parent().parent().children(".thisconx").removeClass('adblock'); $(this).parent().parent(".findwhat").removeClass('adborder'); })
    /* 房源户型收缩 */
    $(".zhankai").click(function () {
        $(".zhankai_box").show();
    });
    $(".shousuo").click(function () {
        $(".zhankai_box").hide();
    });
    //*	新房前台demo
    $(".list-groups-k ul li").hover(function () {
        $(this).toggleClass('hover');
        $(this).children(".msg-box").children(".contro-box").toggle()
    });
    $(".list-groups-k ul li").last().css("border-bottom", "none");

    $(".show-ylbox").hover(
		function () {
		    $(this).children(".ylbox").show();
		    $(this).css("z-index", "9999");
		},
		function () {
		    $(this).children(".ylbox").hide();
		    $(this).css("z-index", "0");
		}
	);
    $(".ylbox").click(function () {
        $(this).hide();
    }); $(document).click(function () { $(".ylbox").hide(); });

    $("#filterBar-more").click(function () {
        if (this.className == "") {
            $(this).addClass("filterBar-more-open");
        }
        else { $(this).removeClass("filterBar-more-open"); };

        $(".filterBar .more-box").toggle();
    });
    /*
    $(".filterBar .filterBar-more-open").click(function(){
    $(this).removeClass('filterBar-more-open');
    $(".filterBar .more-box").hide();

    });
    */

    //楼盘
    $(".hometype_ul li .li_box").eq(0).show();
    $(".hometype_ul li").click(function () {
        $(".hometype_ul li p").removeClass("jian");
        $(this).children("p").addClass("jian");
        $(".hometype_ul li .li_box").hide();
        $(this).children(".li_box").show();
    })
    $(".hover_xg dl dt").hover(
	  function () {
	      $(this).children(".hover_bj").show();
	  },
	  function () {
	      $(this).children(".hover_bj").hide();
	  }
	);
    //工具-楼盘pk
    $(".pkbox tr").hover(
	  function () {
	      $(this).addClass("bgcolf7fbf6");
	  },
	  function () {
	      $(this).removeClass("bgcolf7fbf6");
	  }
	);

    $(".r_wen_green").hover(
		function () {
		    $(this).addClass("r_wen_yellow");
		    $(this).children(".r_layer_box").show();
		},
		function () {
		    $(this).removeClass("r_wen_yellow");
		    $(this).children(".r_layer_box").hide();
		}
	);

    //户型图详细信息
    $(function () {
        var $content = $(".scrbd");
        var i = 6; //已知显示的<a>元素的个数
        var m = 6; //用于计算的变量
        var count = $(".scrbd ul li:visible").length; //总共的<li>元素的个数
        if (count <= 6) {
            $('#prev').addClass('scr_la');
            $('#next').addClass('scr_ra');
        }
        $("#next").click(function () {
            if (!$content.is(":animated")) { //判断元素是否正处于动画，如果不处于动画状态，则追加动画。
                if (m < count) { //判断 i 是否小于总的个数
                    m++;
                    $content.animate({ left: "-=187px" }, 600);
                    $(this).removeClass('scr_r');
                    $(this).addClass('scr_ra');
                }
                if (m == count) {
                    $(this).removeClass('scr_ra');
                    $(this).addClass('scr_r');
                }
            }
            $('#prev').removeClass('scr_la');
            $('#prev').addClass('scr_l');
        });
        $("#prev").click(function () {
            if (!$content.is(":animated")) {
                if (m > i) { //判断 i 是否小于总的个数
                    m--;
                    $content.animate({ left: "+=187px" }, 600);
                    $(this).removeClass('scr_l');
                    $(this).addClass('scr_la');
                }
                if (m == 6) {
                    $(this).removeClass('scr_la');
                    $(this).addClass('scr_l');
                }
            }
            $('#next').removeClass('scr_ra');
            $('#next').addClass('scr_r');
        });
    });

    //新房-地图找房
    $(".map_dl").hover(
	  function () {
	      $(this).addClass("bg_f1");
	  },
	  function () {
	      $(this).removeClass("bg_f1");
	  }
	);

    /*20140211新房搜索列表*/
    $(window).scroll(function () {
        if ($.browser.msie && $.browser.version == "6.0") {
            var top = $(document).scrollTop() + 137;
            $(".comparepop").css("top", top);
        }
    });
    $(window).scroll(function () {
        if ($.browser.msie && $.browser.version == "6.0") {
            var top = $(document).scrollTop() + 210;
            $(".goufang_pop").css("top", top);
        }
    });
    $(window).scroll(function () {
        if ($.browser.msie && $.browser.version == "6.0") {
            var top = $(document).scrollTop() + 350;
            $(".xf_llgdlp").css("top", top);
        }
    });
    $(window).scroll(function () {
        if ($.browser.msie && $.browser.version == "6.0") {
            var top = $(document).scrollTop() + 137;
            $(".comparepop-btn").css("top", top);
        }
    });
    $(window).scroll(function () {
        if ($.browser.msie && $.browser.version == "6.0") {
            var top = $(document).scrollTop() + 210;
            $(".goufangzhinan-btn").css("top", top);
        }
    });
    $(window).scroll(function () {
        if ($.browser.msie && $.browser.version == "6.0") {
            var top = $(document).scrollTop() + 350;
            $(".llgdlp-btn").css("top", top);
        }
    });
    $(".xf_list dl dd.ps_ab").toggle(
			function () {
			    $(this).children(".tb_fylb").addClass("tb_fylb_hover");
			    $(this).siblings(".xf_list_layer").show();
			    var page = 1;
			    var i = 2;
			    var div = $(this).siblings(".xf_list_layer").children(".xf_list_layer_cont");
			    var len = div.find("li").length;
			    var page_count = Math.ceil(len / i);
			    var none_unit_width = div.children(".layer_list").width();
			    var $parent = div.find("ul");
			    div.children(".right").click(function () {
			        if (!$parent.is(":animated")) {
			            if (page == page_count) {
			                $parent.animate({ marginLeft: 0 }, 800);
			                page = 1;
			            } else {
			                $parent.animate({ marginLeft: '-=' + none_unit_width }, 800);
			                page++;
			            }
			        }
			    });
			    div.children(".left").click(function () {
			        if (!$parent.is(":animated")) {
			            if (page == 1) {
			                $parent.animate({ marginLeft: '-=' + none_unit_width * (page_count - 1) }, 800);
			                page = page_count;
			            } else {
			                $parent.animate({ marginLeft: '+=' + none_unit_width }, 800);
			                page--;
			            }
			        }
			    });
			},
			function () {
			    $(this).children(".tb_fylb").removeClass("tb_fylb_hover");
			    $(this).siblings(".xf_list_layer").hide();
			}
		);
    //

    $(".show-ylbox1").toggle(
			function () {
			    $(this).children(".tb_fylb2").addClass("tb_fylb2_hover");
			    $(this).siblings(".xf_list_layer2").show();
			    var page = 1;
			    var i = 2;
			    var div = $(this).siblings(".xf_list_layer2").children(".xf_list_layer_cont2");
			    var len = div.find("li").length;
			    var page_count = Math.ceil(len / i);
			    var none_unit_width = div.children(".layer_list2").width();
			    var $parent = div.find("ul");
			    div.children(".right2").click(function () {
			        if (!$parent.is(":animated")) {
			            if (page == page_count) {
			                $parent.animate({ marginLeft: 0 }, 800);
			                page = 1;
			            } else {
			                $parent.animate({ marginLeft: '-=' + none_unit_width }, 800);
			                page++;
			            }
			        }
			    });
			    div.children(".left2").click(function () {
			        if (!$parent.is(":animated")) {
			            if (page == 1) {
			                $parent.animate({ marginLeft: '-=' + none_unit_width * (page_count - 1) }, 800);
			                page = page_count;
			            } else {
			                $parent.animate({ marginLeft: '+=' + none_unit_width }, 800);
			                page--;
			            }
			        }
			    });
			},
			function () {
			    $(this).children(".tb_fylb2").removeClass("tb_fylb2_hover");
			    $(this).siblings(".xf_list_layer2").hide();
			}
		);





    //切换城市hover效果
    //切换城市效果
    $(".qgcity .hd ul li").mouseover(function () {
        var li_num = $(this).index();
        $(this).removeClass("out").addClass("over");
        $(this).siblings("li").removeClass("over").addClass("out");
        $("#list_cont span").removeClass("dis_b").addClass("dis_none");
        $("#list_cont span").eq(li_num).removeClass("dis_none").addClass("dis_b");
    })
    //	$(".qgcity .hd ul li").mouseover(function(){
    //		var li_num = $(this).index();
    //		$(this).removeClass("out").addClass("over");
    //		$(this).siblings("li").removeClass("over").addClass("out");
    //		$("#list_cont1 span").removeClass("dis_b").addClass("dis_none");
    //		$("#list_cont1 span").eq(li_num).removeClass("dis_none").addClass("dis_b");
    //	})
    $(".city").mouseover(function (event) {
        if (event.target != this) {
            return;
        }
        $(this).find(".pos_a").show();
        //			$('.scroll-pane').jScrollPane(
        //					{
        //						verticalDragMinHeight: 84,
        //						verticalDragMaxHeight: 84,
        //						horizontalDragMinWidth: 11,
        //						horizontalDragMaxWidth: 11
        //					}
        //			);
        return false;
    }
	)
    $(".city").mouseout(function (event) {
        if (event.target != this) {
            return;
        }
        $(this).find(".pos_a").hide();
    }
	)
    $(".city .pos_a .close").click(function () {
        $(".city .pos_a").hide();
        return false;
    })
    //全国分站hover效果
    /*$(".city_bottom").hover( 
    function(){ 
    $(this).children(".pos_a").show();
    },
    function(){ 
    $(this).children(".pos_a").hide();
    }
    );*/
    //头部我的快有家hover效果
    $(".down_box").hover(
		  function () {
		      $(this).children(".tb_down").addClass("tb_down_hover");
		      $(this).children(".tc_box").show();
		  },
		  function () {
		      $(this).children(".tb_down").removeClass("tb_down_hover");
		      $(this).children(".tc_box").hide();
		  }
	 );
    //地图找房
    $(".map_r_title .title_ul li").hover(function () {
        var map_num = $(this).index();
        $(".map_r_title .title_ul li").removeClass("on");
        $(this).addClass("on");
        $(".map_r_title .pos_abs").hide();
        $(".map_r_title .pos_abs").eq(map_num).show();
    })
    $(".map_r_title").mouseleave(function () {
        $(".map_r_title .pos_abs").hide();
    })
    //首页vip
    $(".vipcon").hover(
       function () {
           $(this).children(".vipbox").show();
       },
	   function () {
	       $(this).children(".vipbox").hide();
	   }
    )
    //首页—头部—vip房东/房客切换
    $(".vipp_nr").eq(0).show();
    $(".vipp_nav ul li").hover(function () {
        var num = $(this).index();
        $(".vipp_nav ul li").removeClass("on");
        $(this).addClass("on");
        $(".vipp_nr").hide();
        $(".vipp_nr").eq(num).show();
        return false;
    });
    //首页—头部—vip灰色房东/房客切换
    $(".vaa").eq(0).show();
    $(".vipp_nav ul li").hover(function () {
        var num = $(this).index();
        $(".vipp_nav ul li").removeClass("on");
        $(this).addClass("on");
        $(".vaa").hide();
        $(".vaa").eq(num).show();
        return false;
    });
    //首页vip
    $(".vipcon").hover(
       function () {
           $(this).children(".vipp_box").show();
       },
	   function () {
	       $(this).children(".vipp_box").hide();
	   }
    );





});