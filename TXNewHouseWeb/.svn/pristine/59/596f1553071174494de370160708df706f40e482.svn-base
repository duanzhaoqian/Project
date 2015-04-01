$(function() {

	/* 房源户型收缩 */
	$(".zhankai").click(function(){
		$(".zhankai_box").show();
	});	
	$(".shousuo").click(function(){
		$(".zhankai_box").hide();
	});			
	//*	新房前台demo
	$(".list-groups-k ul li").hover( function(){ $(this).toggleClass('hover');
	$(this).children(".msg-box").children(".contro-box").toggle() });
	$(".list-groups-k ul li").last().css("border-bottom","none");
	
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
	
	$("#filterBar-more").click(function(){
		if(this.className == ""){
			$(this).addClass("filterBar-more-open");
			}
		else{$(this).removeClass("filterBar-more-open");};
		
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
	$(".hometype_ul li").click(function(){
		$(".hometype_ul li p").removeClass("jian");
		$(this).children("p").addClass("jian");
		$(".hometype_ul li .li_box").hide();
		$(this).children(".li_box").show();
	})
	
	$(".list ul li.down").toggle(
		function(){
			$(this).siblings(".list ul ul.dd").show();
			$(this).addClass("up");
			$(this).removeClass("down");
		},
		function(){
			$(this).siblings(".list ul ul.dd").hide();
			$(this).removeClass("up");
			$(this).addClass("down");
		}
	);

	//工具-楼盘pk
    $(".pkbox tr").hover(
	  function(){
		$(this).addClass("bgcolf7fbf6");
	  },
	  function(){
		$(this).removeClass("bgcolf7fbf6");
	  }
	);

	$(".r_wen_green").hover(
		function(){
			$(this).addClass("r_wen_yellow");
			$(this).children(".r_layer_box").show();
		},
		function(){
			$(this).removeClass("r_wen_yellow");
			$(this).children(".r_layer_box").hide();
		}
	);
	
});


