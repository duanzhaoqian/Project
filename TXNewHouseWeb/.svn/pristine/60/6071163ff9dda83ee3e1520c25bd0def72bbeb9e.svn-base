<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<TXOrm.Premis>" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="TXModel.Web" %>
<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--楼盘-详情信息--%>
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
    <div class="clearfix">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="bgcolor">
            <div class="w1190 line_h50 font14 col666">
                当前位置：<a href="<%=NHWebUrl.KYJIndexUrl() %>" class="blue">快有家首页</a> > <a href="<%=NHWebUrl.NewHouseUrl() %>"
                    class="blue">
                    <%=ViewData["cityName"]%>新房</a>
                <%-- <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>" class="blue">楼盘主页</a>--%>
                >
                <%=Model.Name%>
            </div>
        </div>
        <%{
              if (("" != ViewData["ADPictureUrl"]) && ("" != ViewData["DeveloperLOGOUrl"]))
              {
        %>
        <div class="r_title r_title1">
            <span class="img">
                <%--<a href="">--%>
                <img width="140" height="140" alt="" src="<%=ViewData["DeveloperLOGOUrl"]%>.140_140.jpg" /></span>
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
        <div class="ggt">
            <%--  <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>">--%>
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
                <a href="<%=NHWebUrl.PremisesIndexUrl(Model.Id.ToString()) %>"><i class="r1"></i>
                </a><a class="current" href="<%=NHWebUrl.PremisesDetailUrl(Model.Id.ToString()) %>">
                    <i class="r2"></i></a><a href="<%=NHWebUrl.PremisesHouseUrl(Model.Id.ToString(),"","","") %>">
                        <i class="r3"></i></a><a href="<%=NHWebUrl.AlbumUrl("hxt",Model.Id.ToString(),"") %>">
                            <i class="r4"></i></a><a href="<%=NHWebUrl.AlbumUrl("",Model.Id.ToString(),"") %>"><i
                                class="r5"></i></a><a href="<%=NHWebUrl.TrafficFacilitiesUrl(Model.Id.ToString()) %>">
                                    <i class="r6"></i></a>
            </div>
        </div>
        <div class="w1190 mt5">
            <div class="box_l731_r245 clearFix">
                <div class="l_box">
                    <div class="r_title_lp">
                        <span class="title_span pad_0_18">基本信息</span></div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box2 mb10">
                        <tr>
                            <th>
                                参考均价
                            </th>
                            <td>
                                均价<%= Model.ReferencePrice.Equals(0) ? "价格待定" : string.Format("{0}元/㎡", Convert.ToDouble(Model.ReferencePrice))%>
                            </td>
                            <th>
                                咨询电话
                            </th>
                            <td>
                                <%=Model.IsShow400 ? (string.IsNullOrEmpty(Model.Phone400)?(string.IsNullOrEmpty(Model.TelePhone) ? "暂无资料" : Model.TelePhone):Model.Phone400) : (string.IsNullOrEmpty(Model.TelePhone) ? "暂无" : Model.TelePhone)%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                项目地址
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.PremisesAddress) ? "暂无资料" : Model.PremisesAddress%>
                            </td>
                            <th>
                                售楼地址
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.salesAddress) ? "暂无资料" : Model.salesAddress%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                区域/板块
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.DName) ? "暂无资料" : Model.DName %>
                            </td>
                            <th>
                                开发商
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.Developer) ? "暂无资料" : Model.Developer%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                所属商圈
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.BName) ? "暂无资料" : Model.BName%>
                            </td>
                            <th>
                                环线位置
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(TXCommons.NewHouseWeb.PremisesType.GeRingType(Convert.ToInt32(Model.Ring))) ? "暂无资料" : TXCommons.NewHouseWeb.PremisesType.GeRingType(Convert.ToInt32(Model.Ring))%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                销售许可证
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Convert.ToString(ViewData["strPermitPresaleTemp"])) ? "暂无资料" : ViewData["strPermitPresaleTemp"]%>
                            </td>
                            <th>
                                代理商
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.Agent) ? "暂无资料" : Model.Agent%>
                            </td>
                        </tr>
                    </table>
                    <div class="r_title_lp">
                        <span class="title_span pad_0_18">建筑信息</span></div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box2 mb10">
                        <tr>
                            <th>
                                产权
                            </th>
                            <td>
                                <%=Model.PropertyRight%>年
                            </td>
                            <th>
                                建筑面积
                            </th>
                            <td>
                                <%=Model.BuildingArea.Equals(0) ? "暂无资料" : string.Format("{0}平方米", Model.BuildingArea) %>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                占地面积
                            </th>
                            <td>
                                <%=Model.Area.Equals(0) ? "暂无资料" : string.Format("{0}平方米", Model.Area)%>
                            </td>
                            <th>
                                总户数
                            </th>
                            <td>
                                <%=Model.UserCount.Equals(0) ? "暂无资料" : Convert.ToString(Model.UserCount)%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                项目特色
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Convert.ToString(ViewData["PremisesCharacteristic"])) ? "暂无资料" : ViewData["PremisesCharacteristic"]%>
                            </td>
                            <th>
                                建筑类别
                            </th>
                            <td>
                                <%=TXCommons.NewHouseWeb.PremisesType.GetBuildingType(Convert.ToInt32(Model.BuildingType))%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                楼层状态
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Convert.ToString(ViewData["strUnderloorTemp"])) ? "暂无资料" : ViewData["strUnderloorTemp"] %>
                            </td>
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <div class="r_title_lp">
                        <span class="title_span pad_0_18">物业类型</span></div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box2 mb10">
                        <tr>
                            <th>
                                物业类型
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Convert.ToString(ViewData["strPropertyTypedesc"])) ? "暂无资料" : ViewData["strPropertyTypedesc"]%>
                            </td>
                            <th>
                                容积率
                            </th>
                            <td>
                                <%=Model.AreaRatio.Equals(0) ? "暂无资料" : Util.ToString(Model.AreaRatio)%>
                                <%--<span class="r_wen_green ml20"><i class="r_layer_box">总建筑面积与用地面积的比值。一般情况，容积率越低的项目相对舒适度越高。
                                    <span class="sj"></span></i></span>--%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                得房率
                            </th>
                            <td>
                                <%=Model.RoomRate.Equals(0) ? "暂无资料" : Util.ToString(Model.RoomRate) + "%"%>
                                <%--<span class="r_wen_green ml20"><i class="r_layer_box">总建筑面积与用地面积的比值。一般情况，容积率越低的项目相对舒适度越高。
                                    <span class="sj"></span></i></span>--%>
                            </td>
                            <th>
                                物业费
                            </th>
                            <td>
                                <%= Model.PropertyPrice.Equals(0) ? "价格待定" : Util.ToString((double)Model.PropertyPrice) + "元/平/月"%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                车位信息
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.ParkingLot) ? "暂无资料" : Model.ParkingLot%>
                            </td>
                            <th>
                                物业公司
                            </th>
                            <td>
                                <%=string.IsNullOrEmpty(Model.PropertyCompany) ? "暂无资料" : Model.PropertyCompany%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                绿化率
                            </th>
                            <td>
                                <%=Model.GreeningRate.Equals(0) ? "暂无资料" : Util.ToString(Model.GreeningRate) + "%"%>
                                <%--<span class="r_wen_green ml20"><i class="r_layer_box">总建筑面积与用地面积的比值。一般情况，容积率越低的项目相对舒适度越高。
                                    <span class="sj"></span></i></span>--%>
                            </td>
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <div class="r_title_lp">
                        <span class="title_span pad_0_18">楼盘介绍</span></div>
                    <div class="padd15">
                        <p class="text mb5">
                            <%=string.IsNullOrEmpty(Model.PremisesIntroduction) ? "暂无资料" : Model.PremisesIntroduction %></p>
                        <%--     <p class="text">本项目总占地11.2万方，容积率3.2，绿地率35%，未来将建成25栋33层或27层的高层精装住宅，装修人性化，充分考虑业主生活需求。户型面积段为70-135㎡，所有户型均是得到市场认可的精选户型，都有较大附赠面积和改造空间。</p>
                        --%>
                    </div>
                    <div class="r_title_lp">
                        <span class="title_span pad_0_18">楼栋开盘信息</span></div>
                    <%{
                          List<TXModel.Web.PremisesBuilding> dtlsBuilding = ViewData["lsBuildingNew"] as List<TXModel.Web.PremisesBuilding>;

                          if (null == dtlsBuilding || dtlsBuilding.Count == 0)
                          {
                    %><p class="text mb5">
                        暂无资料</p>
                    <%
                          }

                          if (null != dtlsBuilding && dtlsBuilding.Count > 0)
                          {

                              for (int i = 0; i < dtlsBuilding.Count; i++)
                              {
                 
                    %>
                    <p class="padd15">
                        <strong class="font12 col333">
                            <%=dtlsBuilding[i].Periods%>期
                            <%=dtlsBuilding[i].Name%><%=dtlsBuilding[i].NameType%>
                            当前均价<%=Convert.ToDouble(dtlsBuilding[i].houseSinglePrice)%>元/㎡</strong></p>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table_box2">
                        <tr>
                            <th>
                                开盘时间
                            </th>
                            <td>
                                <%=Convert.ToDateTime(dtlsBuilding[i].OpeningTime).ToString("yyyy-MM-dd")%>
                            </td>
                            <th>
                                交付时间
                            </th>
                            <td>
                                <%=Convert.ToDateTime(dtlsBuilding[i].OthersTime).ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                本期户数
                            </th>
                            <td>
                                <%=dtlsBuilding[i].FamilyNum.Equals(0) ? "暂无资料" : Convert.ToString(dtlsBuilding[i].FamilyNum)%>
                            </td>
                            <th>
                                交付标准
                            </th>
                            <td>
                                <%=TXCommons.NewHouseWeb.BuildingType.GetRenovation(Convert.ToInt32(dtlsBuilding[i].Renovation))%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                物业类别
                            </th>
                            <td>
                                <%=TXCommons.NewHouseWeb.BuildingType.GetPropertyType(dtlsBuilding[i].PropertyType.ToString())%>
                            </td>
                            <th>
                                建筑类别
                            </th>
                            <td>
                                <%=TXCommons.NewHouseWeb.PremisesType.GetBuildingType(Convert.ToInt32(Model.BuildingType))%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                楼层状态
                            </th>
                            <td>
                                <%=dtlsBuilding[i].TheFloor%>层
                            </td>
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <%}
                          }
                      } %>
                </div>
                <div class="r_box">
                    <div id="divTQY" class="style-gro-a">
                    </div>
                    <div id="divTJW" class="style-gro-a">
                    </div>
                </div>
            </div>
            <div class="mt15 mb15">
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
            <div class="xf_llgdlp" style="display: none;">
                <div class="this-title">
                    购房指南<a href="javascript:void(0);" class="on" id="gfznclose"></a>
                </div>
                <div class="this-conx this-conx1">
                    <p class="this-conx_p1">
                        <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-gfsc.html" target="_blank">购房手册</a></p>
                  <div class="gj_box">
                         <ul>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-1.html" class="gj_1" target="_blank">贷款计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-3.html" class="gj_2" target="_blank">公积金贷款计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-4.html" class="gj_3" target="_blank">提前还贷计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-2.html" class="gj_4" target="_blank">购房能力评估计算器</a></li>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/gj-dk-5.html" class="gj_5" target="_blank">税费计算器</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
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
                $(".plus_compare").click(function () {
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
            function getTQYHotPremisesList(id) {
                $.ajax({
                    type: "get",
                    url: globalvar.siteRoot + "/Premises/TQYHotPremisesList",
                    data: { id: id },
                    cache: false,
                    async: false,
                    dataType: "html",
                    success: function (result) {
                        $("#divTQY").html(result);

                    },
                    error: function (msg) {
                        //alert(msg.responseText);
                    }
                });
            }
            function getTJWHotPremisesList(id) {
                $.ajax({
                    type: "get",
                    url: globalvar.siteRoot + "/Premises/TJWHotPremisesList",
                    data: { id: id },
                    cache: false,
                    async: false,
                    dataType: "html",
                    success: function (result) {
                        $("#divTJW").html(result);

                    },
                    error: function (msg) {
                        //alert(msg.responseText);
                    }
                });
            }
            getTQYHotPremisesList('<%=Model.Id%>');
            getTJWHotPremisesList('<%=Model.Id%>');

        </script>
    </div>
</asp:Content>
