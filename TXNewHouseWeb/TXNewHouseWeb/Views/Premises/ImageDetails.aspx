<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<TXOrm.Premis>" %>

<%@ Import Namespace="TXCommons" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%
            TXNewHouseWeb.Common.SEOModel seo = ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel;
        %>
        <%=seo==null?string.Empty:seo.Title %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="<%=(ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel)==null?string.Empty:(ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel).Keywords %>" />
    <meta name="description" content="<%=(ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel)==null?string.Empty:(ViewData["Seo"] as TXNewHouseWeb.Common.SEOModel).Description %>" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/global.css")%>" rel="stylesheet"
        type="text/css" />
    <link href="<%=TXCommons.GetConfig.GetFileUrl("css/newhouse.css")%>" rel="stylesheet"
        type="text/css" />
</head>
<body>
    <div class="pub_nav spe_head">
        <div class="w1190">
            <div class="logo">
                <a href="#">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/blank.gif" width="200" height="70" /></a></div>
            <div class="city">
                <div class="pos_r">
                    <div class="qh_city">
                        <strong>北京</strong>[<a target="_blank" href="">切换城市</a>]</div>
                    <div class="pos_a w430">
                        <!--<a href="" class="close"></a>-->
                        <span class="tc_bj"></span>
                        <div class="citybox">
                            <div class="tjcity">
                                <div class="hd">
                                    <h2>
                                        推荐城市</h2>
                                </div>
                                <div class="bd">
                                    <ul class="clearFix">
                                        <!--推荐-->
                                    </ul>
                                </div>
                            </div>
                            <div class="qgcity">
                                <div class="hd" id="list_title">
                                    <h2>
                                        全国城市列表</h2>
                                    <ul class="clearFix">
                                        <li class="out over">按拼音</li>
                                        <li class="out">按省份</li>
                                    </ul>
                                </div>
                                <div id="list_cont" class="clearFix">
                                    <span class="dis_b">
                                        <div class="scrollContent scroll-pane">
                                            <!--首字母分类-->
                                        </div>
                                    </span><span class="dis_none">
                                        <div class="scrollContent scroll-pane">
                                            <!--省市分类-->
                                        </div>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    快有家新房平台</p>
            </div>
            <div class="mbnav">
                <%
                    var type = ViewData["type"] as string;
                    var tName = "";
                    var tUrl = "";
                    if (type == "hxt")
                    {
                        tName = "户型图";
                        tUrl = NHWebUrl.AlbumUrl(type, Model.Id.ToString(), "");
                    }
                    else
                    {
                        //1.户型图2.规划图3.位置图4.楼栋平面图5.效果图6.实景图7.交通图
                        tName = TXCommons.NewHouseWeb.AlbumType.GetAlbumType(type);
                        tUrl = NHWebUrl.AlbumUrl(type, Model.Id.ToString(), "");
                    }
              
              
                %>
                快有家首页 &gt; <a href="<%=NHWebUrl.NewHouseUrl() %>">
                    <%=ViewData["cityName"]%>新房</a> &gt; <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>">
                        <%=Model.Name %></a> &gt; <a href="<%=tUrl %>">
                            <%=tName %></a> &gt; <a>当前图片</a></div>
        </div>
    </div>
    <%--    <div class="spe_head">
        <div class="w1000">
            <div class="logo">
                <a href="#">
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/blank.gif" width="133" height="45" /></a></div>
            <div class="citylinkc">
                <span><strong>北京</strong></span><a href="#" class="noborder">[切换]</a>
            </div>
            <div class="mbnav">
                <%
                    var type = ViewData["type"] as string;
                    var tName = "";
                    var tUrl = "";
                    if (type == "hxt")
                    {
                        tName = "户型图";
                        tUrl = NHWebUrl.SizeChartUrl(Model.Id.ToString(), 0, 0, 0, 1);
                    }
                    else
                    {
                        //1.户型图2.规划图3.位置图4.楼栋平面图5.效果图6.实景图7.交通图
                        tName = TXCommons.NewHouseWeb.AlbumType.GetAlbumType(type);
                        tUrl = NHWebUrl.AlbumUrl(TXCommons.NewHouseWeb.AlbumType.GetAlbumIntType(type), Model.Id.ToString());
                    }
              
              
                %>
                快有家首页 &gt; <a href="<%=NHWebUrl.NewHouseUrl() %>">
                    <%=ViewData["cityName"]%>新房</a> &gt; <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>">
                        <%=Model.Name %></a> &gt; <a href="<%=tUrl %>">
                            <%=tName %></a> &gt; <a>当前图片</a></div>
        </div>
    </div>
    --%>
    <!--//spe_head	end-->
    <div class="clearfix">
        <div class="con_box">
            <div class="clearFix pad_20_15 pos_rel">
                <span class="font14 col666 fl mr20 mt5">
                    <%=Model.Name %></span>
                <div class="phonav">
                    <ul>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("xgt", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">效果图</a></li>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("hxt", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">户型图</a></li>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("ght", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">规划图</a></li>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("wzt", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">位置图</a></li>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("ldt", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">楼栋平面图</a></li>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("sjt", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">实景图</a></li>
                        <li><a href="<%=NHWebUrl.ImageDetailsUrl("jtt", Model.Id.ToString(), "0", "", "") %>"
                            class="linkD">交通图</a></li>
                    </ul>
                </div>
                <div class="fr">
                    <%--<a href="<%=tUrl %>" class="linkD">返回<%=tName %></a>--%></div>
            </div>
            <!--end -->
        </div>
        <%if (ViewData["detail"] != null)
          {
              IndexPremisesPic detail = ViewData["detail"] as IndexPremisesPic;
        %>
        <div class="phobox">
            <div class="w1190 pos_rel">
                <div class="fl" style="width: 840px;">
                    <div class="font14 col666 pad10 blod">
                        <label id="picname">
                            <%=detail.Title%></label>
                    </div>
                    <div class="pos_rel">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td valign="middle" align="center" width="840" height="457" style="position: relative;">
                                    <img src="<%=detail.PicUrl+".695_457.jpg"%>"  id="imgdetail" />
                                    <a href="<%=detail.PicUrl+".695_457.jpg" %>" target="_blank" id="bigpic">
                                        <div class="fdimg">
                                            查看大图</div>
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="imginf fr">
                    <h5>
                        图片信息</h5>
                    <div class="imginf_box">
                        <%if (type == "hxt")
                          {
                              if (detail.HouseID != "0")
                              {
                        %>
                        <p>
                            户型：<%=detail.Room + "室" + detail.Hall + "厅" + detail.Toilet + "卫"%></p>
                        <p>
                            面积：<%=detail.BuildingArea == "" ? "" : detail.BuildingArea + "㎡"%></p>
                        <%}
                          }%>
                        <p>
                            图片描述：<%=detail.PicDesc%></p>
                    </div>
                    <div class="imginf_bb">
                        &nbsp;</div>
                </div>
                <!--图片信息-->
                <div class="clear">
                </div>
                <div class="scrbox clearFix">
                    <div id="prev" class="scr_l fl">
                    </div>
                    <div class="scrbody">
                        <div class="scrbd">
                            <ul>
                                <%if (ViewData["SizeChartList"] != null)
                                  {
                                      List<IndexPremisesPic> room = ViewData["SizeChartList"] as List<IndexPremisesPic>;
                                      for (var i = 0; i < room.Count; i++)
                                      {
                                %>
                                <li>
                                    <div name="pp" id="p_<%=room[i].PicID %>_<%=room[i].BuildingArea %>_<%=room[i].HouseID %>"
                                        class="<%=((ViewData["detail"] as IndexPremisesPic).PicID==room[i].PicID&&room[i].BuildingArea==(ViewData["detail"] as IndexPremisesPic).BuildingArea&&room[i].HouseID==(ViewData["detail"] as IndexPremisesPic).HouseID)?"bdimg hover":"bdimg" %>">
                                        <a href="javascript:void(0)" onclick="picDetail(<%=room[i].PicID %>,<%=room[i].HouseID==null?"0": room[i].HouseID%>,<%=room[i].BuildingArea %>)">
                                            <img src="<%=room[i].PicUrl+".695_457.jpg"%>" width="176" height="120" id="pic_<%=room[i].PicID %>_<%=room[i].HouseID==null?"0": room[i].HouseID%>_<%=room[i].BuildingArea %>" /></a></div>
                                    <div class="imgtxt">
                                        <a class="linkD" href="javascript:void(0)" onclick="picDetail(<%=room[i].PicID %>,<%=room[i].HouseID==null?"0": room[i].HouseID%>,<%=room[i].BuildingArea %>)">
                                            <label id="title_<%=room[i].PicID %>">
                                                <%=room[i].Title%></label>
                                        </a>
                                    </div>
                                    <input type="hidden" value="<%=room[i].Room + "室" + room[i].Hall + "厅" + room[i].Toilet + "卫" %>"
                                        id="ts_<%=room[i].PicID %>_<%=room[i].HouseID==null?"0": room[i].HouseID%>_<%=room[i].BuildingArea %>" />
                                    <input type="hidden" value="<%=room[i].BuildingArea == "" ? "" : room[i].BuildingArea + "㎡" %>"
                                        id="mj_<%=room[i].PicID %>_<%=room[i].HouseID==null?"0": room[i].HouseID%>_<%=room[i].BuildingArea %>" />
                                    <input type="hidden" value="<%=room[i].PicDesc %>" id="desc_<%=room[i].PicID %>_<%=room[i].HouseID==null?"0": room[i].HouseID%>_<%=room[i].BuildingArea %>" />
                                </li>
                                <%}
                                  } %>
                            </ul>
                        </div>
                    </div>
                    <div id="next" class="scr_r fr">
                    </div>
                </div>
            </div>
        </div>
        <%}
          else
          { %>
        <div class="noResult_box tac mt35">
            <span class="icons01">很抱歉，没有找到符合条件的信息。</span>
        </div>
        <%} %>
        <!--end 2-->
    </div>
    <div class="footer">
        <p>
            <a href="http://www.kuaiyoujia.com/about/cert" target="_blank">
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/f_cx.gif" /></a>&nbsp;&nbsp;<a href="http://www.itrust.org.cn/yz/pjwx.asp?wm=1689221523"
                    target="_blank"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/f_wx.gif" /></a>&nbsp;&nbsp;<a
                        target="_blank" href="http://www.bj.cyberpolice.cn/index.do"><img src="<%=TXCommons.GetConfig.ImgUrl%>images/f_bj.gif"></a></p>
        <div class="footer_link_box">
            <a target="_blank" href="http://www.kuaiyoujia.com/About/AboutUs">关于快有家</a>|<a target="_blank"
                href="http://www.kuaiyoujia.com/About/WebRreaty">网站协议</a>|<a target="_blank" href="http://www.kuaiyoujia.com/comment/UserRreaty">用户协议</a>|
            <span class="city_bottom"><a href="">全国分站</a> <span class="pos_a" style="display: none;">
                <span class="tc_bj"></span><a href="http://<%=TXCommons.PinyinHelper.GetPinyin("北京") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">
                    北京</a><a href="http://<%=TXCommons.PinyinHelper.GetPinyin("上海") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">上海</a><a
                        href="http://<%=TXCommons.PinyinHelper.GetPinyin("广州") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">广州</a><a
                            href="http://<%=TXCommons.PinyinHelper.GetPinyin("深圳") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">深圳</a><a
                                href="http://<%=TXCommons.PinyinHelper.GetPinyin("天津") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">天津</a><a
                                    href="http://<%=TXCommons.PinyinHelper.GetPinyin("成都") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">成都</a><a
                                        href="http://<%=TXCommons.PinyinHelper.GetPinyin("南京") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">南京</a><a
                                            href="http://<%=TXCommons.PinyinHelper.GetPinyin("大连") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">大连</a><a
                                                href="http://<%=TXCommons.PinyinHelper.GetPinyin("杭州") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">杭州</a><a
                                                    href="http://<%=TXCommons.PinyinHelper.GetPinyin("青岛") %><%=TXCommons.GetConfig.Domain %><%=TXCommons.GetConfig.GetSiteRoot%>/quyu">青岛</a>
            </span></span>|<a target="_blank" href="http://www.kuaiyoujia.com/Comment/Message">金点子赢大奖</a>|<a
                target="_blank" href="<%=TXCommons.GetConfig.HelpCenterUrl %>">帮助中心</a>
        </div>
        <p>
            Copyright &copy; 2014 <a href="<%=TXCommons.NHWebUrl.KYJIndexUrl() %>">www.kuaiyoujia.com</a>
            All Rights Reserved</p>
        <p>
            京公网安备 110105010112 <span class="colfff">京ICP备11027796号-2</span> Email:<a href="mail:service@kuaiyoujia.com">service@kuaiyoujia.com</a><span
                class="colf90">客服热线：400-999-3535</span> 9:00-20:00</p>
        <script src="<%=TXCommons.GetConfig.GetFileUrl("js/jquery-1.4.2.min.js")%>" language="javascript"
            type="text/javascript">
        </script>
        <script type="text/javascript" src="<%=TXCommons.GetConfig.GetFileUrl("js/UtilityVar.js")%>"></script>
        <script src="<%=TXCommons.GetConfig.GetFileUrl("js/pageTestDemo.js")%>" language="javascript"
            type="text/javascript">
        </script>
        <script src="<%=TXCommons.GetConfig.GetFileUrl("js/city.js")%>" language="javascript"
            type="text/javascript"></script>
        <!--[if IE 6]>
<script src="<%=TXCommons.GetConfig.GetFileUrl("js/DD_belatedPNG_0.0.8a.js")%>"  language="javascript" type="text/javascript">
</script>
<script type="text/javascript">
DD_belatedPNG.fix('*');
</script>
<![endif]-->
        <script type="text/javascript">

            function picDetail(id, hid, m) {
                var src = $("#pic_" + id + "_" + hid + "_" + m).attr("src");
                var ts = $("#ts_" + id + "_" + hid + "_" + m).val();
                var mj = $("#mj_" + id + "_" + hid + "_" + m).val();
                var desc = $("#desc_" + id + "_" + hid + "_" + m).val();
                var type = "<%=type %>";
                $("div[name='pp']").removeClass("hover");
                $("#p_" + id + "_" + m + "_" + hid).addClass("hover")
                $("#imgdetail").attr("src", src);
                $("#bigpic").attr("href", src);
                if (type == "hxt") {
                    $("#picname").html($("#title_" + id).html());
                    if (hid != "0" && m != "0")
                        $(".imginf_box").html("<p>户型：" + ts + "</p><p>面积：" + mj + "</p><p>图片描述：" + desc + "</p>");
                    else
                        $(".imginf_box").html("<p>图片描述：" + desc + "</p>");

                } else {
                    $("#picname").html($("#title_" + id).html());
                    $(".imginf_box").html("<p>图片描述：" + desc + "</p>");
                }
            }
        </script>
    </div>
    <!--//pub_footer	end-->
</body>
</html>
