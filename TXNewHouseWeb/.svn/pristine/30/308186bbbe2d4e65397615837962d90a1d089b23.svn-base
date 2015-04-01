$(function() {
//演示demo
$(".log_list dl").hover(
  function(){
    $(this).addClass("bg_f8f8f8");
  },
  function(){
    $(this).removeClass("bg_f8f8f8");
  }
)

//v1.3	开始
	//找住宅
	$('.findwhat .btn').hover( function(){ $(this).parent().children(".thisconx").addClass('adblock');$(this).parent(".findwhat").addClass('adborder'); });
	$('.findwhat .thisconx a').click( function(){ $(this).parent().parent().children(".thisconx").removeClass('adblock');$(this).parent().parent(".findwhat").removeClass('adborder'); })

//v1.3	结束
	
	$('.pub_listGroupA  .ttxxccA .wycj i.thisIcn').hover( function(){ $(this).toggleClass('hover') });
	
	//$('.this-show').hover( function(){ $(this).parent().parent().children('.whats-1').toggleClass('hover') });
	
	//滑过变色
	$(".dataTAB table tbody tr").hover(function(){$(this).addClass("mousHover")}, function(){$(this).removeClass("mousHover")});
	$('.bidContent .bidPos').hover( function(){ $(this).toggleClass('divhover') });
	$('.pub_listGroupA li').hover( function(){ $(this).toggleClass('hoverColor') });
	$('input.btn_chujia').hover( function(){ $(this).toggleClass('hover') });
	$('.pub_nav .RPart ul li').hover( function(){ $(this).toggleClass('hover') });
	$('.Evt2013 .wycj a i.thisIcn').hover( function(){ $(this).toggleClass('hover') });
	$('input.btn_wt01').hover( function(){ $(this).toggleClass('wtColor') });
	//鼠标移动改变背景色
	
	//demo	
	$(".search_web .state input").click(function(){
		$(this).parent().children(".pub_checkListC").show();
	});
	$(".search_web .state a").click(function(){
		$(this).parent().parent().parent().children(".pub_checkListC").hide();
	});
	$(".search_web .quyu a").click(function(){
		$(this).parent().parent().parent().parent().children(".pub_checkListC").hide();
	});
	
	
	$(".helpbox ul li").hover( function(){ $(this).toggleClass('hoverColor') });
	
	
	//单选切换
	$(".lb label").click(function(){
		var num=$(".lb label").index(this); 
		$(".ct").hide();
		$(".ct").eq(num).show();	
	})
	//鼠标移动改变背景色
	$(".hover_col tr").hover( function(){ $(this).toggleClass('bg_fefff2') });
	
	
	//彩蛋浮动层、在线客服
	$(window).scroll(function(){
	if($.browser.msie && $.browser.version=="6.0")$(".caidan").css("top",$(window).height()-$(".caidan").height()+$(document).scrollTop());
	});	
	$(".caidanA").hide();
	$(".caidanB").hover(function () {
		$(".caidanA").toggle();
	});
	$(".caidanA").hover(function () {
		$(".caidanA").toggle();
	})
	
	//在线客服
	$(window).scroll(function(){
		if($.browser.msie && $.browser.version=="6.0"){
			var hei=$(window).height()-$(".zaixiankf").height()+$(document).scrollTop();
			$(".zaixiankf").css("top",hei);
		}
	});	
	
	//返回顶部
	$(window).scroll(function(){
		if($.browser.msie && $.browser.version=="6.0"){
			var hei=$(window).height()-$(".fanhuisy").height()+$(document).scrollTop();
			hei=hei-70;
			$(".fanhuisy").css("top",hei);
		}
	});	
	//微信
	$(window).scroll(function(){
		if($.browser.msie && $.browser.version=="6.0"){
			var hei=$(window).height()-$(".sp-weixin").height()+$(document).scrollTop();
			hei=hei-125;
			$(".sp-weixin").css("top",hei);
		}
	});	
	
	$(".zxkf2").hide();
	$(".zxkf1").click(function(){
		$(".zxkf1").hide();
		$(".zxkf2").show();
	})
	$(".zxkf2").click(function(){
		$(".zxkf2").hide();
		$(".zxkf1").show();

	})
		
	
	
	
	
	
$(".tel_js").hover(
  function(){
    $(this).children(".btn").addClass("tel_bg_hover");
	$(this).children(".adblock").show();
  },
  function(){
    $(this).children(".btn").removeClass("tel_bg_hover");
	$(this).children(".adblock").hide();
  }
)
$(".sp-weixin-box").hover(
  function(){
	$(this).children(".ps_a").show();
  },
  function(){
	$(this).children(".ps_a").hide();
  }
)

	/*v1.8巨划算房源*/
	$(".bg_7cba0f").hover(
	  function(){
		$(this).children(".green_border").show();
	  },
	  function(){
		$(this).children(".green_border").hide();
	  }
	)
	$(".bg_ff6600").hover(
	  function(){
		$(this).children(".yellow_border").show();
	  },
	  function(){
		$(this).children(".yellow_border").hide();
	  }
	)
	$(".ul_box ul li").hover(
	  function(){
		$(this).children(".jhs_layer_box").show();
	  },
	  function(){
		$(this).children(".jhs_layer_box").hide();
	  }
	)
	$(".ul_box ul li").hover(
	  function(){
		$(this).addClass("hover");
		$(this).children(".jhs_layer_box").show();
	  },
	  function(){
		$(this).removeClass("hover");
		$(this).children(".jhs_layer_box").hide();
	  }
	)
	
	
	/*委托-20131204房东发房*/
	$(function(){
		 var len  = $(".num > span").length;
		 var index = 0;
		 var adTimer;
		 $(".num span").mouseover(function(){
			index  =   $(".num span").index(this);
			showImg(index);
		 }).eq(0).mouseover();	
		 $('.wt_ad').hover(function(){
				 clearInterval(adTimer);
			 },function(){
				 adTimer = setInterval(function(){
					showImg(index)
					index++;
					if(index==len){index=0;}
				  } , 3000);
		 }).trigger("mouseleave");
	})
	function showImg(index){
			var adWidth = $(".wt_ad").width();
			$(".slider").stop(true,false).animate({left : -adWidth*index},500);
			$(".num span").removeClass("on")
				.eq(index).addClass("on");
	}	
	
	$(".wt_box1_l dl").hover(
	  function(){
		$(this).addClass("bg_f5");
	  },
	  function(){
		$(this).removeClass("bg_f5");
	  }
	)	
	$(".wt_box1_l dl dt").hover(
	  function(){
		$(this).addClass("border_f90");
	  },
	  function(){
		$(this).removeClass("border_f90");
	  }
	)	
	//*	新房前台demo
	$(".list-groups-k ul li").hover( function(){ $(this).toggleClass('hover');});
	$(".show-ylbox").hover( 
		function(){ 
			$(this).children(".ylbox").show();
			$(this).css("z-index","9999");
		},
		function(){ 
			$(this).children(".ylbox").hide();
			$(this).css("z-index","0");
		}
	);
	$(".ylbox").click( function(){ 
		$(this).hide();
	});$(document).click( function(){ $(".ylbox").hide();});
	//头部我的快有家hover效果
    $(".down_box").hover( 
		  function(){ 
		  	  $(this).children(".tb_down").addClass("tb_down_hover");
			  $(this).children(".tc_box").show();
		  },
		  function(){ 
		  	  $(this).children(".tb_down").removeClass("tb_down_hover");
			  $(this).children(".tc_box").hide();
		  }
	 );
	 $(".pub_nav ul li:has(ul)").hover(function(){
			$(this).children("ul").stop(true,true).show();
        },function(){
		    $(this).children("ul").stop(true,true).hide();
		}); 
	 //全国分站hover效果
     $(".city_bottom").hover( 
		  function(){ 
			  $(this).children(".pos_a").show();
		  },
		  function(){ 
			  $(this).children(".pos_a").hide();
		  }
	 );
	 //首页vip
   $(".vipcon").hover(
       function(){
		$(this).children(".vipbox").show();  
	   },
	   function(){
		$(this).children(".vipbox").hide();    
	   }
    )
 //首页—头部—vip房东/房客切换
	 $(".vipp_nr").eq(0).show();
	 $(".vipp_nav ul li").hover(function(){
		var num = $(this).index();
		$(".vipp_nav ul li").removeClass("on");
		$(this).addClass("on");
		$(".vipp_nr").hide();
		$(".vipp_nr").eq(num).show();
		return false;
	 });	
  //首页—头部—vip灰色房东/房客切换
	 $(".vaa").eq(0).show();
	 $(".vipp_nav ul li").hover(function(){
		var num = $(this).index();
		$(".vipp_nav ul li").removeClass("on");
		$(this).addClass("on");
		$(".vaa").hide();
		$(".vaa").eq(num).show();
		return false;
	 });	
	//头部vip
   $(".vipcon").hover(
       function(){
		$(this).children(".vipp_box").show();  
	   },
	   function(){
		$(this).children(".vipp_box").hide();    
	   }
    );	
	
	
	
});