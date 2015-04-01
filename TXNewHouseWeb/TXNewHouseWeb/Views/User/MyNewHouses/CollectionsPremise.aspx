<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/User.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<TXModel.User.CT_FavoritePrem>>" %>

<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    我的新房-我收藏的楼盘
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clearFix">
        <div class="p_wrap">
            <div class="bidtitle mt20" style="padding-left: 0px;">
                <span class="fl mr30">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>user/images/wdxf_tit.gif" /></span>
                <h6 class="fl">
                    <a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/bid">
                        我参与的竞购</a>
                    <%--<a href="<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/yaohao">
                        我参与的网络摇号</a>--%>
                    <a href="javascript:void(0)" class="cur">我收藏的楼盘</a>
                </h6>
            </div>
            <div id="divAjaxData" class="clearFix list-groups-k">
                <%=Html.Partial("MyNewHouses/CollectionTable", Model)%>
            </div>
            <div class="shadowHR mb20">
            </div>
        </div>
    </div>
    <a class="comparepop-btn roof" href="javascript:scroll(0,0)" style="top: 200px;">
        <img src="<%=TXCommons.GetConfig.ImgUrl%>user/images/roof.gif" />
    </a><a class="comparepop-btn comparepop-btn1" style="top: 250px;" id="comparepop-btn"
        href="javascript:void(0);">楼盘<br />
        对比</a>
    <div class="comparepop right45" style="display: none; top: 250px;">
        <div class="this-title">
            楼盘对比<a href="javascript:void(0);" class="on" id="lpdbclose"></a>
        </div>
        <div class="this-conx">
            <div>
                <div class="bg-comparepop-none">
                    <ul>
                    </ul>
                </div>
                <div class="clearFix tac pt10 pb10">
                    <a href="javascript:void(0);" class="s-link-a font12" id="com_start">开始对比</a> 
                    <a href="javascript:void(0);" class="s-link-a font12" id="com_clear">清空</a>
                </div>
            </div>
            <div style="display: none;">
                <div class="clearFix tac">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>user/images/bg-comparepop-none.gif" />
                </div>
                <div class="clearFix tac pt10 pb10">
                    <a href="javascript:void(0);" class="s-link-a font12">开始对比</a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            //购房指南
            $(".llgdlp-btn").bind('click', function () {
                //getLastPremisesList();
                if ($(".xf_llgdlp").is(":hidden")) {
                    $(".comparepop").hide();
                    $(".xf_llgdlp").show();
                } else {
                    $(".xf_llgdlp").hide();
                }
            });

            $("#gfznclose").bind('click', function () {
                $(".xf_llgdlp").hide();
            });

            //添加楼盘对比项
            $(".duibi").click(function () {
                $('.bg-comparepop-none').addClass('bg-comparepop-none1');
                var $this = this;
                var cookie_compare_items = $.cookie("compare_items");
                var ifPlus = false;
                var ifFour = false;
                if (!isNullOrEmptyOrUndefined(cookie_compare_items)) {
                    $(".this-conx").find("ul").empty();
                    var arr = cookie_compare_items.split(",");
                    for (var i = arr.length - 1; i >= 0; i--) {
                        var result = arr[i].split("|");
                        $(".this-conx").find("ul").prepend("<li id='" + result[1] + "' data='" + result[0] + "' url='" + result[2] + "'><label class='this-repair'><input type='checkbox' name='gr_compare' /></label>&nbsp;<a href='" + result[2] + "' target='_blank' class='linkB-666 font12'>" + result[0] + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                    }
                    $(".this-conx").children("div:eq(0)").show();
                    $(".this-conx").children("div:eq(1)").hide();
                    $(".comparepop").show();
                    if (arr.length <= 3) {
                        for (var i = 0; i < arr.length; i++) {
                            if ($($this).attr("lang") == arr[i].split("|")[1]) {
                                ifPlus = true;
                                break;
                            }
                        }
                        if (ifPlus == false) {
                            $(".this-conx").find("ul").prepend("<li id='" + $($this).attr("lang") + "' data='" + $($this).attr("data") + "' url='" + $($this).attr("url") + "'><label class='this-repair'><input type='checkbox' name='gr_compare'/></label>&nbsp;<a href='" + $($this).attr("url") + "' target='_blank' class='linkB-666 font12'>" + $($this).attr("data") + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                        }
                    } else {
                        ifFour = true;
                    }
                } else {
                    $(".this-conx").children("div:eq(0)").show();
                    $(".this-conx").children("div:eq(1)").hide();
                    $(".this-conx").find("ul").empty();
                    $(".this-conx").find("ul").prepend("<li id='" + $($this).attr("lang") + "' data='" + $($this).attr("data") + "' url='" + $($this).attr("url") + "'><label class='this-repair'><input type='checkbox' name='gr_compare'/></label>&nbsp;<a href='" + $($this).attr("url") + "' target='_blank' class='linkB-666 font12'>" + $($this).attr("data") + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                }
                //全部选中
                Cb_SelectAll();
                $(".comparepop").show();
                //存入cookie
                SetCompareItemsToCookie();
                if (ifPlus == true) {
                    alert("您已添加该楼盘");
                } else if (ifFour == true) {
                    alert("最多添加4个楼盘");
                }
            });

            //开始对比
            $("#com_start").click(function () {
                var li = $('.this-conx').find(".bg-comparepop-none").children("ul").children("li");
                var arr = new Array();
                for (var i = 0; i < li.length; i++) {
                    if ($(li[i]).find("input[name='gr_compare']").attr("checked")) {
                        arr.push($(li[i]).attr("id"));
                    }
                }
                if (arr.length > 0) {
                    window.open("<%=TXCommons.GetConfig.GetSiteRoot%>/gj-lppk-" + arr.join(","));
                } else {
                    alert("请勾选对比项");
                }
            });

            //删除对比项
            $(".this-del").live("click", function () {
                $(this).closest("li").remove();
                var liLength = $(".this-conx").find(".bg-comparepop-none").children("ul").children("li").length;
                if (liLength <= 0) {
                    $(".this-conx").children("div:eq(0)").hide();
                    $(".this-conx").children("div:eq(1)").show();
                }
                //更新cookie
                SetCompareItemsToCookie();
            });

            //清空对比项
            $("#com_clear").click(function () {
                var a_clear = this;
                $(a_clear).parent(".clearFix").prev("ul").children("li").remove();
                $(a_clear).parent(".clearFix").parent("div").hide();
                $(a_clear).parent(".clearFix").parent("div").next("div").show();
                //清空cookie
                $.cookie("compare_items", null, { domain: "<%=TXCommons.GetConfig.Domain%>" });
            });

            //楼盘对比显示隐藏
            $("#comparepop-btn").live("click", function () {
                if ($(".comparepop").is(":hidden")) {
                    //从cookie中加载对比项
                    GetCompareItemsFromCookie();
                    //全部选中
                    Cb_SelectAll();
                    $(".xf_llgdlp").hide();
                    $(".comparepop").show();
                    var div = $(".this-conx");
                    var liLength = div.find(".bg-comparepop-none").children("ul").children("li").length;
                    if (liLength <= 0) {
                        $('.bg-comparepop-none').removeClass('bg-comparepop-none1');
                        div.children("div:eq(0)").hide();
                        div.children("div:eq(1)").show();
                    } else {
                        $('.bg-comparepop-none').addClass('bg-comparepop-none1');
                        div.children("div:eq(0)").show();
                        div.children("div:eq(1)").hide();
                    }
                } else {
                    $(".comparepop").hide();
                }
            });
            $("#lpdbclose").live("click", function () {
                $(".comparepop").hide();
            });

            //购房指南显示隐藏
            $(".goufangzhinan-btn").bind('click', function () {
                if ($(".goufang_pop").is(":hidden")) {
                    $(".goufang_pop").show();
                } else {
                    $(".goufang_pop").hide();
                }
            }).bind('mouseenter', function () {
                $(".goufang_pop").show();
            });

            $(".goufang_pop").bind('mouseleave', function () {
                $(this).hide();
            });

            $(".goufang_pop").find(".shouqi").click(function () {
                $(this).closest(".goufang_pop").hide();
            });
        });

        //全部选中
        function Cb_SelectAll() {
            $(".this-conx").find("ul li").find("input[name=gr_compare]").each(function () {
                if (!$(this).attr("checked")) {
                    $(this).trigger("click");
                }
            });
        }

        //将对比项加入cookie
        function SetCompareItemsToCookie() {
            var arr = new Array();
            var liList = $(".this-conx").find("ul li");
            for (var i = 0; i < liList.length; i++) {
                arr.push($(liList[i]).attr("data") + "|" + $(liList[i]).attr("id") + "|" + $(liList[i]).attr("url"));
            }
            //存入cookie，有效期7天
            $.cookie("compare_items", arr.join(","), { expires: 7, domain: "<%=TXCommons.GetConfig.Domain%>" });
        }

        //从cookie中加载对比项
        function GetCompareItemsFromCookie() {
            var cookie_compare_items = $.cookie("compare_items");
            if (!isNullOrEmptyOrUndefined(cookie_compare_items)) {
                var arr = cookie_compare_items.split(",");
                $(".this-conx").find("ul").empty();
                for (var i = arr.length - 1; i >= 0; i--) {
                    var result = arr[i].split("|");
                    $(".this-conx").find("ul").prepend("<li id='" + result[1] + "' data='" + result[0] + "' url='" + result[2] + "'><label class='this-repair'><input type='checkbox' name='gr_compare' /></label>&nbsp;<a href='" + result[2] + "' target='_blank' class='linkB-666 font12'>" + result[0] + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                }
            } else {
                $(".this-conx").find("ul").empty();
            }
        }

        function AddUserFavorite(premisesid, userid) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Search/AddUserFavorite",
                data: { premisesid: premisesid, userid: userid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    globalvar.dialog(result);
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
    </script>
    <script type="text/javascript">
        function hover(obj) {
            //房源预览
            $(obj).children(".ylbox").html("");
            $(obj).children(".ylbox").html("<div style=\"font-size: 12px;\"><center><br/>&nbsp;<img src='<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/images/loading.gif' height=\"15px\" width=\"15px\" />&nbsp;加载中...<br/></center></div>");
            $.getJSON("<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/GetPreviewHouseByPremisId", { pid: $(obj).attr("id") }, function (data) {
                if (data.length > 0) {
                    var str = new Array();
                    for (var i = 0; i < data.length; i++) {
                        str.push("<table width=\"650px\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        str.push("<tr><td rowspan=\"2\" align=\"left\" width=\"80px\">");
                        str.push("<div class=\"this-pic-wp\"><img src=\"" + data[i].InnerCode + "\" />");
                        // str.push("<div class=\"this-text\">限时折</div>");
                        str.push("</div></td>");
                        str.push("<td><a href=\"<%=TXCommons.GetConfig.GetSiteRoot %>/lp-fy-house-" + data[i].HouseId + ".html\" target=\"_blank\" class=\"linkD\">" + data[i].PremisesName + data[i].BuildingName + data[i].Unit + data[i].Floor + "层" + data[i].Name + "</a></td>");
                        str.push("<td align=\"right\">起 价：</td>");
                        str.push("<td><span class=\"colff7100\"><strong>" + data[i].BidPrice + "万元</strong></span></td>");
                        str.push("<td rowspan=\"2\"><a href=\"<%=TXCommons.GetConfig.GetSiteRoot %>/lp-fy-house-" + data[i].HouseId + ".html\" target=\"_blank\" class=\"s-link-a\">查看详情</a></td></tr>");
                        str.push("<tr><td>" + data[i].BuildingArea + "㎡|" + data[i].Room + "室" + data[i].Hall + "厅|" + data[i].Floor + "/" + data[i].TheFloor + "层 已有<span class=\"colff7100\">0</span>人关注</td>");
                        str.push("<td align=\"right\">市场价：</td>");
                        str.push("<td><span class=\"colff7100\"><strong>0万元</strong></span></td></tr></table>");
                        if (i == 0) {
                            str.push("<div class=\"this-line clearFix\"></div>");
                        }
                    }
                    str.push("<div class=\"this-morebox\"><a href=\"#\" class=\"linkD\">更多房源</a></div>");
                    $(obj).children(".ylbox").attr("class", "ylbox clearFix");
                    $(obj).children(".ylbox").html(str.join(""));
                }
                else {
                    $(obj).children(".ylbox").attr("class", "ylbox ylbox1 clearFix");
                    $(obj).children(".ylbox").html("<span class=\"icons01\">对不起，暂时没有数据！</span><span class=\"sj\"></span>");
                }
            })
            $(obj).children(".ylbox").show();
            $(obj).css("z-index", "9999");
        }
        function out(obj) {
            $(obj).parent().css("z-index", "0");
            $(obj).hide();
        }
        //取消收藏
        function Delete(pid) {
            if (confirm("确定取消收藏？")) {
                $.get("<%=TXCommons.GetConfig.BaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/user/Delete", { pid: pid, obj: Math.random() }, function (data) {
                    if (data = "1") {
                        if ($("#ul_main li").length == 1) {
                            //                            $.post("<%=TXCommons.GetConfig.GetSiteRoot%>/user/collectionsPremise?page=1", function (result) {
                            //                                $('#divAjaxData').html(result);
                            //                            });
                            //                            return false;
                            location.reload();
                        }
                        else {
                            $("#li_" + pid).remove();
                        }
                    }
                    else {
                        alert("操作失败！");
                        return;
                    }
                })
            }
        }
    </script>
</asp:Content>
