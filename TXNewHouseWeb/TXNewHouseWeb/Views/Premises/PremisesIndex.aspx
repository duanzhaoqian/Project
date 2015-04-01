<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Premis>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXNewHouseWeb.Common" %>
<%@ Import Namespace="TXCommons" %>
<%--<%@ Import Namespace="TXCommons.PubConstant" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--新房-楼盘--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bg-comparepop-none1
        {
            width: auto;
            overflow: hidden;
            background: none;
            color: #8d978a;
            padding-top: 0;
        }
    </style>
    <style type="text/css">
        .map_title
        {
            color: #0068b7;
            font-weight: bold;
        }
        .map_content
        {
            margin-top: 10px;
        }
        .ul_list li
        {
            height: 29px;
            overflow: hidden;
        }
        .page_traffic
        {
            height: 24px;
            line-height: 22px;
            color: #969696;
            text-align: center;
        }
        .page_traffic a
        {
            height: 22px;
            line-height: 22px;
            display: inline-block;
            margin: 0px 2px;
            color: #969696;
            padding: 0px 8px;
            border: 1px solid #d7d7d7;
        }
        .page_traffic a:hover
        {
            color: #FFFFFF;
            background: #71b30e;
            border: 1px solid #71b30e;
        }
        .page_traffic a.hover
        {
            color: #FFFFFF;
            background: #71b30e;
            border: 1px solid #71b30e;
        }
        .page_traffic a.no, .page_traffic a.no:hover
        {
            color: #999;
            background: #f8f8f8;
            cursor: auto;
            border: 1px solid #d7d7d7;
        }
    </style>
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">
                    <%=ViewData["cityName"]%>新房</a> >
                <%=Model.Name%><%--楼盘主页--%>
            </div>
        </div>
        <%{
              if (("" != ViewData["ADPictureUrl"]) && ("" != ViewData["DeveloperLOGOUrl"]))
              {
        %>
        <div class="r_title r_title1">
            <span class="img">
                <img width="140" height="140" alt="" src="<%=ViewData["DeveloperLOGOUrl"]%>.140_140.jpg" />
            </span><span class="font24 fontYahei">
                <%=Model.Name%></span><span class="r_bjtb1"><%=TXCommons.NewHouseWeb.PremisesType.GetSalesStatus(Convert.ToInt32(Model.SalesStatus))%></span>
            <%----推荐 Begin----%>
            <%if ("1".Equals(Convert.ToString(ViewData["IsRecommend"])))
              { %>
            <i class="tuj_i"></i>
            <%} %>
            <%----推荐 End----%>
            <span class="font14 col999">[<%=Model.DName%>] 排名<%=ViewData["Rank"] %>
                浏览<%=ViewData["ViewCount"]??"0"%>次</span><%--<span class="right"><a href=""><img src="<%=TXCommons.GetConfig.ImgUrl%>images/yaohao.gif" /></a></span>--%><!--摇号-->
        </div>
        <div class="ggt">
            <%-- <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>">--%>
            <img width="1190" height="200" alt="" src="<%=ViewData["ADPictureUrl"]%>.1190_200.jpg" />
        </div>
        <%
              }
              else
              { %>
        <div class="r_title">
            <span class="font24 fontYahei">
                <%=Model.Name%></span><span class="r_bjtb1"><%=TXCommons.NewHouseWeb.PremisesType.GetSalesStatus(Convert.ToInt32(Model.SalesStatus))%></span>
            <%----推荐 Begin----%>
            <%if ("1".Equals(Convert.ToString(ViewData["IsRecommend"])))
              { %>
            <i class="tuj_i"></i>
            <%} %>
            <%----推荐 End----%>
            <span class="font14 col999">[<%=Model.DName%>] 排名<%=ViewData["Rank"] %>
                浏览<%=ViewData["ViewCount"]??"0"%>次</span><%--<span class="right"><a href=""><img src="<%=TXCommons.GetConfig.ImgUrl%>images/yaohao.gif" /></a></span>--%><!--摇号-->
        </div>
        <% }
          }%>
        <div class="user_nav">
            <div class="w1190">
                <a class="current" href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"><i
                    class="r1"></i></a><a href="<%=NHWebUrl.PremisesDetailUrl(Model.Id.ToString()) %>"><i
                        class="r2"></i></a><a href="<%=NHWebUrl.PremisesHouseUrl(Model.Id.ToString(),"","","") %>">
                            <i class="r3"></i></a><a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"") %>">
                                <i class="r4"></i></a><a href="<%=NHWebUrl.AlbumUrl("",Model.Id.ToString(),"") %>"><i
                                    class="r5"></i></a><a href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>">
                                        <i class="r6"></i></a>
            </div>
        </div>
        <div class="bg_f8f8f8">
            <dl class="loupan_dl clearFix">
                <dt id="divCarousel"></dt>
                <dd>
                    <ul>
                        <% var tmpPriceStr = (Model.ReferencePrice.Equals(0) ?
                                                   string.Format("<strong class=\"colf60\">{0}</strong>", "价格待定")
                                                   : string.Format("<strong class=\"colf60\">{0}</strong><span class=\"font14\">元/平方米</span>", Convert.ToDouble(Model.ReferencePrice)));  %>
                        <li><strong>参考均价：</strong><%= tmpPriceStr %>
                            <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-1.html" target="_Blank" class="blue">
                                <span class="r_tb_fd ml15">房贷计算器</span></a></li>
                        <li><strong>项目地址：</strong><%=TXCommons.Util.getStrLenB(Model.PremisesAddress,0,60)%><a
                            target="_blank" href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>"
                            class="blue ml15">查看地图</a></li>
                        <li><strong>售楼地址：</strong><%=TXCommons.Util.getStrLenB(Model.salesAddress,0,60)%><a
                            target="_blank" href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>"
                            class="blue ml15">查看地图</a></li>
                        <li><strong>户型图：</strong><span id="sp_zlhxt"></span><a style="display: none" target="_blank"
                            href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"")%>" class="blue ml15">全部户型</a></li>
                        <li><strong>开盘时间：</strong><span id="sp_kpsj"></span></li>
                        <li><strong>交房时间：</strong><span id="sp_jfsj"></span></li>
                    </ul>
                    <ul class="loupan_ul1 clearFix">
                        <li><strong>建筑类别：</strong><%=TXCommons.NewHouseWeb.HouseType.GetBuildingType(Convert.ToInt32(Model.BuildingType))%></li>
                        <li><strong>物业类型：</strong><span id="sp_wylx"></span></li>
                        <li><strong>装修情况：</strong><span id="sp_zxqk"></span></li>
                        <li><strong>产权年限：</strong><%=Model.PropertyRight%>年</li>
                        <li><strong>得 房 率：</strong><%=Model.RoomRate == 0 ? "暂无资料" : "约" + Model.RoomRate + "%"%></li>
                        <li><strong>容 积 率：</strong><%=Model.AreaRatio == 0 ? "暂无资料" : Model.AreaRatio.ToString()%></li>
                        <li><strong>车位信息：</strong><%=Model.ParkingLot == "" ? "暂无资料" : Model.ParkingLot + "个"%></li>
                        <li><strong>物 业 费：</strong><%=Convert.ToDouble(Model.PropertyPrice) == 0 ? "价格待定" : Convert.ToDouble(Model.PropertyPrice) + "元/平米/月"%></li>
                        <li><strong>开 发 商：</strong><%=Model.Developer.Length > 15 ? Model.Developer.Substring(0, 15) + "..." : Model.Developer %></li>
                        <li><strong>预 售 证：</strong><span id="sp_ysxkz"></span></li>
                        <li class="pos_a"><a href="<%=NHWebUrl.PremisesDetailUrl(Model.Id.ToString()) %>"
                            class="blue" target="_blank">查看更多详情</a></li>
                    </ul>
                    <div class="yellow_box">
                        <p class="tel_bj">
                            <strong>客服电话:
                                <%=Model.IsShow400 ? (string.IsNullOrEmpty(Model.Phone400)?(string.IsNullOrEmpty(Model.TelePhone) ? "暂无" : Model.TelePhone):Model.Phone400) : (string.IsNullOrEmpty(Model.TelePhone) ? "暂无" : Model.TelePhone)%></strong>免费咨询电话（工作时间：9:00-20:00）</p>
                    </div>
                </dd>
            </dl>
        </div>
        <div class="bg_ea mb10">
            <div class="w1190">
                <div class="bg_ea_title">
                    <div id="divts">
                    </div>
                    <%--  <strong class="font16 col666 fontYahei">项目特色：</strong>
                    <%=ViewData["PremisesCharacteristic"]%>--%>
                    <span class="right"><a href="javascript:void(0);" onclick="AddUserFavorite('<%=Model.Id %>','<%=ViewData["UserId"]%>')"
                        class="fav blue">收藏楼盘</a><a href="javascript:void(0);" id="plus_compare" data="<%=Model.Name %>"
                            lang="<%=Model.Id%>" url="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"
                            class="plus blue ml30">加入对比</a></span>
                </div>
            </div>
        </div>
        <div class="w1190">
            <div id="divsp">
            </div>
            <div class="shadow">
            </div>
            <%--  <div class="r_title_lp mt10">
                <strong class="title_span">
                    <%=Util.getStrLenB(Model.Name, 0, 24)%>房源列表</strong><span class="col666 font12"></span><span
                        class="right"><a href="<%=NHWebUrl.PremisesHouseUrl(Model.Id.ToString(),"","","") %>"
                            class="blue font12">更多>> </a></span>
            </div>--%>
            <div id="divActivities">
            </div>
            <%{
                  List<TXCommons.Index.IndexRoom> dtlistROOMTYPE = ViewData["sizeChartListViewData"] as List<TXCommons.Index.IndexRoom>;
                  if (null != dtlistROOMTYPE && dtlistROOMTYPE.Count > 0)
                  { %>
            <div class="r_title_lp">
                <strong class="title_span">
                    <%=Util.getStrLenB(Model.Name, 0, 24)%>户型图</strong><span class="col666 font12">(共<%=ViewData["ViewDatalistROOMTYPECount"]%>张)</span><span
                        class="right">
                        <%foreach (var item in ViewData["RoomType"] as IEnumerable<int>)
                          {%>
                        <a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"r"+item.ToString()) %>"
                            class="blue font12 mr20">
                            <%=item %>居</a>
                        <%} %>
                        <a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"")%>" class="blue font12">更多>>
                        </a></span>
            </div>
            <%}
              }
            %>
            <div class="housetype clearFix mt10">
                <%{
                      List<TXCommons.Index.IndexRoom> dtlistROOMTYPE = ViewData["sizeChartListViewData"] as List<TXCommons.Index.IndexRoom>;
                      if (null != dtlistROOMTYPE && dtlistROOMTYPE.Count > 0)
                      {
                          for (int i = 0; i < dtlistROOMTYPE.Count; i++)
                          {
                              if (i < 5)
                              {
                 
                %>
                <dl <%=(i==4?" class=\"last\"":"") %>>
                    <dt><a href="<%=NHWebUrl.ImageDetailsUrl("hxt",Model.Id.ToString(),dtlistROOMTYPE[i].RID, "",dtlistROOMTYPE[i].HouseID)%>"
                        class="blue">
                        <img class="imgload lazy" data-original="<%=dtlistROOMTYPE[i].PicUrl%>.175_125.jpg"
                            src="" />
                    </a></dt>
                    <dd>
                        <a href="<%=NHWebUrl.ImageDetailsUrl("hxt",Model.Id.ToString(),dtlistROOMTYPE[i].RID, "",dtlistROOMTYPE[i].HouseID)%>"
                            class="blue">
                            <%=Util.getStrLenB(dtlistROOMTYPE[i].Title,0,24)%>
                            <%--     <%=dtlistROOMTYPE[i].Desc%>--%>
                        </a>
                    </dd>
                </dl>
                <%}
                          }
                      }
                      else
                      {%>
                <%--  <div class="noResult_box tac">
                    <span class="icons01">暂无数据</span>
                </div>--%>
                <%}
                  } %>
            </div>
            <div class="r_title_lp">
                <strong class="title_span">
                    <%=Util.getStrLenB(Model.Name, 0, 24)%>周边交通配套</strong><span class="right"><a target="_blank"
                        href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>" class="blue font12 mr20 r_tb_map">查看地图</a><a
                            target="_blank" href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>"
                            class="blue font12">更多>> </a></span>
            </div>
            <div class="map_box clearFix" id="divMap">
            </div>
            <div class="shadow">
            </div>
            <div id="divAlbum">
            </div>
            <div id="divInterest">
            </div>
            <div class="lr_box clearFix">
                <div id="divhot" class="fl">
                </div>
                <div id="divnew" class="fr">
                </div>
            </div>
            <div class="mt10 mb10">
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/r_ywlc.gif" />
            </div>
            <a class="comparepop-btn roof" href="javascript:scroll(0,0)">
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/roof.gif" /></a> <a class="comparepop-btn comparepop-btn1"
                    id="comparepop-btn" href="javascript:void(0);">楼盘<br />
                    对比</a> <a class="llgdlp-btn llgdlp-btn1" href="javascript:void(0);" style="display: inline;">
                        购房<br />
                        指南</a>
            <div class="comparepop right45" style="display: none;">
                <div class="this-title">
                    楼盘对比<a href="javascript:void(0);" class="on" id="lpdbclose"></a>
                </div>
                <div class="this-conx" id="this-conx">
                    <div>
                        <div class="bg-comparepop-none">
                            <ul>
                            </ul>
                        </div>
                        <div class="clearFix tac pt10 pb10">
                            <a href="javascript:void(0);" class="s-link-a font12" id="com_start">开始对比</a> <a
                                href="javascript:void(0);" class="s-link-a font12" id="com_clear">清空</a>
                        </div>
                    </div>
                    <div style="display: none">
                        <div class="clearFix tac">
                            <img src="<%=TXCommons.GetConfig.ImgUrl%>images/bg-comparepop-none.gif" />
                        </div>
                        <div class="clearFix tac pt10 pb10">
                            <a href="javascript:void(0);" class="s-link-a font12">开始对比</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="xf_llgdlp " style="display: none;">
                <div class="this-title">
                    购房指南<a href="javascript:void(0);" class="on" id="gfznclose"></a>
                </div>
                <div class="this-conx this-conx1">
                    <p class="this-conx_p1">
                        <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-gfsc.html" target="_blank">购房手册</a></p>
                    <div class="gj_box">
                        <ul>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-1.html" class="gj_1" target="_blank">
                                贷款计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-3.html" class="gj_2" target="_blank">
                                公积金贷款计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-4.html" class="gj_3" target="_blank">
                                提前还贷计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-2.html" class="gj_4" target="_blank">
                                购房能力评估计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-5.html" class="gj_5" target="_blank">
                                税费计算器</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery.lazyload.min.js")%>" type="text/javascript"></script>
    <!--弹出层相关-->
    <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/skins/nxdgreen/css/dialog.css")) %>"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/core/base.js")) %>"></script>
    <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/plugins/freeDialog.js")) %>"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.5&ak=1d0c8b02751d95a0ea07d72c58d3b2c6"></script>
    <script type="text/javascript">
        $(function () {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Ajax/SetRecentlyViewed",
                data: { id: "<%=Model.Id%>", m: Math.random() },
                cache: false,
                async: false,
                dataType: "json",
                success: function (result) {
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });

            //异步获取楼盘相关信息
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/GetAsyPremisesInfo",
                data: { id: "<%=Model.Id%>", m: Math.random() },
                cache: false,
                async: false,
                dataType: "json",
                success: function (result) {
                    if (result) {
                        $("#sp_kpsj").text(result.kpsj == "" ? "暂无资料" : result.kpsj);
                        $("#sp_jfsj").text(result.jfsj == "" ? "暂无资料" : result.jfsj);
                        $("#sp_wylx").text(result.wylx == "" ? "暂无资料" : result.wylx);
                        $("#sp_zxqk").text(result.zxqk == "" ? "暂无资料" : result.zxqk);
                        $("#sp_ysxkz").text(result.ysxkz == "" ? "暂无资料" : result.ysxkz);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });
        });
    </script>
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
            $("#plus_compare").click(function () {
                $('.bg-comparepop-none').addClass('bg-comparepop-none1');
                var $this = this;
                var cookie_compare_items = $.cookie("compare_items");
                var ifPlus = false;
                var ifFour = false;
                if (!isNullOrEmptyOrUndefined(cookie_compare_items)) {
                    $("#this-conx").find("ul").empty();
                    var arr = cookie_compare_items.split(",");
                    for (var i = arr.length - 1; i >= 0; i--) {
                        var result = arr[i].split("|");
                        $("#this-conx").find("ul").prepend("<li id='" + result[1] + "' data='" + result[0] + "' url='" + result[2] + "'><label class='this-repair'><input type='checkbox' name='gr_compare' /></label>&nbsp;<a href='" + result[2] + "' target='_blank' class='linkB-666 font12'>" + result[0] + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                    }
                    $("#this-conx").children("div:eq(0)").show();
                    $("#this-conx").children("div:eq(1)").hide();
                    $(".comparepop").show();
                    if (arr.length <= 3) {
                        for (var i = 0; i < arr.length; i++) {
                            if ($($this).attr("lang") == arr[i].split("|")[1]) {
                                ifPlus = true;
                                break;
                            }
                        }
                        if (ifPlus == false) {
                            $("#this-conx").find("ul").prepend("<li id='" + $($this).attr("lang") + "' data='" + $($this).attr("data") + "' url='" + $($this).attr("url") + "'><label class='this-repair'><input type='checkbox' name='gr_compare'/></label>&nbsp;<a href='" + $($this).attr("url") + "' target='_blank' class='linkB-666 font12'>" + $($this).attr("data") + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                        }
                    } else {
                        ifFour = true;
                    }
                } else {
                    $("#this-conx").children("div:eq(0)").show();
                    $("#this-conx").children("div:eq(1)").hide();
                    $("#this-conx").find("ul").empty();
                    $("#this-conx").find("ul").prepend("<li id='" + $($this).attr("lang") + "' data='" + $($this).attr("data") + "' url='" + $($this).attr("url") + "'><label class='this-repair'><input type='checkbox' name='gr_compare'/></label>&nbsp;<a href='" + $($this).attr("url") + "' target='_blank' class='linkB-666 font12'>" + $($this).attr("data") + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
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
                var li = $('#this-conx').find(".bg-comparepop-none").children("ul").children("li");
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
                var liLength = $("#this-conx").find(".bg-comparepop-none").children("ul").children("li").length;
                if (liLength <= 0) {
                    $("#this-conx").children("div:eq(0)").hide();
                    $("#this-conx").children("div:eq(1)").show();
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
                    var div = $("#this-conx");
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
            $("#this-conx").find("ul li").find("input[name=gr_compare]").each(function () {
                if (!$(this).attr("checked")) {
                    $(this).trigger("click");
                }
            });
        }

        //将对比项加入cookie
        function SetCompareItemsToCookie() {
            var arr = new Array();
            var liList = $("#this-conx").find("ul li");
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
                $("#this-conx").find("ul").empty();
                for (var i = arr.length - 1; i >= 0; i--) {
                    var result = arr[i].split("|");
                    $("#this-conx").find("ul").prepend("<li id='" + result[1] + "' data='" + result[0] + "' url='" + result[2] + "'><label class='this-repair'><input type='checkbox' name='gr_compare' /></label>&nbsp;<a href='" + result[2] + "' target='_blank' class='linkB-666 font12'>" + result[0] + "</a><a href='javascript:void(0);' class='this-del'></a></li>");
                }
            } else {
                $("#this-conx").find("ul").empty();
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
        function InterestPremises(premisesid) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/InterestPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    $("#divInterest").html(result);
                    $("#divInterest .imgload").lazyload();
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        //异步获取楼盘特色
        function TSPremises(premisesid) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/TSPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                    alert(result);
                    $("#divts").html(result);
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function SPPremises(premisesid) {
            //  alert(premisesid);
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/SPPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                alert(result);
                    $("#divsp").html(result);
                    //=============
                    //楼盘
                    $(".hometype_ul li .li_box").eq(0).show();
                    $(".hometype_ul li").click(function () {
                        $(".hometype_ul li p").removeClass("jian");
                        $(this).children("p").addClass("jian");
                        $(".hometype_ul li .li_box").hide();
                        $(this).children(".li_box").show();
                    })

                    $("#divsp .imgload").lazyload();
                    //=============
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function ActivitiesHousePremises(premisesid) {
            //  alert(premisesid);
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/ActivitiesHousePremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                alert(result);
                    $("#divActivities").html(result);
                    $("#divActivities .imgload").lazyload();
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function CarouselPremises(premisesid) {
            //  alert(premisesid);
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/CarouselPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                alert(result);
                    $("#divCarousel").html(result);
                    $("#divCarousel .imgload").lazyload();
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
        function AlbumPremises(premisesid) {
            //  alert(premisesid);
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/AlbumPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                alert(result);
                    $("#divAlbum").html(result);
                    $("#divAlbum .imgload").lazyload();
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }

        function MapPremises(premisesid) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/MapPremises",
                data: { premisesid: premisesid, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                alert(result);
                    $("#divMap").html(result);
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }

        function Buildinginfo(premisesid, Id, PNum) {
            //            for(var i=0;i<PNum+1;i++){
            //             $("#span_" + i).removeClass("lp_bg_red");
            //             $("#span_" + i).addClass("lp_bg_blue");
            //            }
            //             $("#span_" + PNum).removeClass("lp_bg_blue");
            //             $("#span_" + PNum).addClass("lp_bg_red");
            //        alert("楼盘id："+premisesid+"  楼栋沙盘编号："+PNum);
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/HXPremisesList",
                data: { premisesid: premisesid, id: Id, pnum: PNum, m: Math.random() },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                alert(result);
                    $("#divhxlist").html(result);
                    //=============
                    //楼盘
                    $(".hometype_ul li .li_box").eq(0).show();
                    $(".hometype_ul li").click(function () {
                        $(".hometype_ul li p").removeClass("jian");
                        $(this).children("p").addClass("jian");
                        $(".hometype_ul li .li_box").hide();
                        $(this).children(".li_box").show();
                    })
                    //=============
                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
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
        function changeimg(path, id) {
            $("#IDCARD2").attr("src", path);
            $("#Carouselli1").removeClass("on");
            $("#Carouselli2").removeClass("on");
            $("#Carouselli3").removeClass("on");
            $("#Carouselli4").removeClass("on");
            $("#Carouselli5").removeClass("on");
            $("#Carouselli" + id).addClass("on");
        }
        ///跳转到楼盘pk对比
        function premisesppk() {
            var premisesid1 = parseInt("<%=Model.Id%>");
            var premisesid2 = $("#hidpremisesid").val();
            if (premisesid2 == undefined || premisesid2 == '') {
                var path = "<%=TXCommons.GetConfig.GetSiteRoot%>/gj-lppk-" + premisesid1;
                window.open(path);
            } else {
                var path = "<%=TXCommons.GetConfig.GetSiteRoot%>/gj-lppk-" + premisesid1 + "," + premisesid2;
                window.open(path);
            }
        }

      
        ///楼盘进行切换对比
        function pkpremiseschange(id, name, referenceprice, innercode, sender) {

            $("#pkpremiseimg").attr("src", $(sender).attr('src'));
            $("#hidpremisesid").val(id);
            $("#pkpremisea").text(name);
            $('#pkpremisea').attr('href', '<%=TXCommons.GetConfig.GetSiteRoot%>/lp-' + id + '.html');
            $("#pkpremisestrong").text(referenceprice + '元/平方米');
        }
        function getHotPremisesList(id) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/HotPremisesList",
                data: { id: id },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {

                    $("#divhot").html(result);

                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
       
        function getNewPremisesList(id) {
            $.ajax({
                type: "get",
                url: globalvar.siteRoot + "/Premises/NewPremisesList",
                data: { id: id },
                cache: false,
                async: false,
                dataType: "html",
                success: function (result) {
                    //                    alert(result);
                    $("#divnew").html(result);

                },
                error: function (msg) {
                    //alert(msg.responseText);
                }
            });
        }
       
        $(function () {
            //联播图
            CarouselPremises('<%=Model.Id%>');
            //参加营销活动房源
            ActivitiesHousePremises('<%=Model.Id%>');
            //楼盘特色
            TSPremises('<%=Model.Id%>');
            //沙盘图
            SPPremises('<%=Model.Id%>');
            //加载楼盘相册
            AlbumPremises('<%=Model.Id%>');
            //可能感兴趣的楼盘
            InterestPremises('<%=Model.Id%>');
            //加载地图
            MapPremises('<%=Model.Id%>');

            getHotPremisesList('<%=Model.Id%>');
            getNewPremisesList('<%=Model.Id%>');
        });
       
        //延迟加载
        $(function () { $(".imgload").lazyload(); }); 
    </script>
</asp:Content>
