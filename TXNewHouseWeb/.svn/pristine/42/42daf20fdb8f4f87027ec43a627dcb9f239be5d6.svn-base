<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<PagedList<IndexPremises>>" %>

<%@ Import Namespace="Webdiyer.WebControls.Mvc" %>
<%@ Import Namespace="TXCommons.Index" %>
<%@ Import Namespace="TXCommons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%--楼盘搜索页--%>
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
        <div class="nh-greenline clearFix">
            <%List<TXModel.AD.Advs> adv = ViewData["adv"] as List<TXModel.AD.Advs>; %>
        </div>
        <div class="p_wrap123">
            <div class="focusbox">
                <script type="text/javascript" src="<%=TXCommons.GetConfig.GetFileUrl("js/myfocus-2.0.4.min.js")%>"></script>
                <script src="<%=TXCommons.GetConfig.GetFileUrl("js/_base.js")%>" type="text/javascript"></script>
                <script src="<%=TXCommons.GetConfig.GetFileUrl("js/url.js")%>" type="text/javascript"></script>
                <!--弹出层相关-->
                <link href="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/skins/nxdgreen/css/dialog.css")) %>"
                    rel="stylesheet" type="text/css" />
                <style>
                    #myFocus_index
                    {
                        width: 1190px;
                        height: 100px;
                        float: right;
                    }
                </style>
                <%--<div id="myFocus_index">
                    <!--焦点图盒子-->
                    <div class="loading">
                    </div>
                    <!--载入画面(可删除)-->
                    <%--<div class="pic">
                        <!--图片列表-->
                        <ul>
                            <li><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/home/index" target="_blank">
                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/ggt_w1190_h100.png" width="1190" height="100"
                                    thumb="" alt=" " text=" " /></a></li>
                            <%--<li><a href="#1">
                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/banner-hei200px-2.jpg" thumb="" alt=" "
                                    text=" " /></a></li>-
                        </ul>
                    </div>
                   
                </div>--%>
                <%--<div class="flash"><a href="<%=TXCommons.GetConfig.GetSiteRoot%>/home/index" target="_blank" class="flash_a"><embed src="<%=TXCommons.GetConfig.ImgUrl%>flash/1190_100.swf" quality="high" width="1190" height="100" wmode="transparent" type='application/x-shockwave-flash'></embed></div>--%>
                <!--//myFocus-->
                <%if (adv != null && adv.Count > 0 && adv[0].Pos == "1" && adv[0].Disurl != "")
                  {
                %>
                <div class="pic">
                    <!--图片列表-->
                    <ul>
                        <li><a href="<%=adv[0].Linkurl%>" target="_blank">
                            <img src="<%=TXCommons.GetConfig.ImgUrl+adv[0].Disurl%>" width="1190" height="100"
                                alt="<%=adv[0].Alt %>" /></a></li>
                    </ul>
                </div>
                <%
                  }
                  else
                  {%>
                <div class="flash pos_r">
                    <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/home/index" target="_blank" class="flash_a">
                    </a>
                    <embed src="<%=TXCommons.GetConfig.ImgUrl%>flash/1190_100.swf" quality="high" width="1190"
                        height="100" wmode="transparent" type='application/x-shockwave-flash'></embed></div>
                <% } %>
            </div>
        </div>
        <div class="p_wrap">
            <div class="w1190">
                <div class="classify">
                    <div class="search_new clearFix">
                        <input type="text" value="请输入楼盘名/或具体位置..." class="input" id="search_key" name="search_key"
                            class="inptxt w470" />
                        <input type="button" value="搜 索" class="search_yel" id="btn_search" />
                        <%--地图找房--%>
                        <%--<a target="_blank" href="<%=TXCommons.GetConfig.GetQTBaseUrl%><%=TXCommons.GetConfig.GetSiteRoot%>/dt-dtzf.html"
                            class="find_map fl ml10">地图找房</a>--%>
                    </div>
                    <ul class="classify_tab">
                        <%--<li class="active1"><a href="javascript:;">按区域找房</a></li>
                        <li class="title2"><a href="javascript:;">按地铁找房</a></li>--%>
                    </ul>
                    <div class="filterBar clearFix">
                        <dl>
                        </dl>
                        <dl class="mt5">
                            <%--<dt>类型：</dt>--%>
                        </dl>
                        <dl>
                            <%--<dt class="CSSHACKRepair">价格：</dt>--%>
                        </dl>
                        <dl>
                            <%--<dt>户型：</dt>--%>
                        </dl>
                        <dl>
                            <%--<dt>面积：</dt>--%>
                        </dl>
                        <dl>
                            <%--<dt>环线：</dt>--%>
                        </dl>
                        <div style="display: none" class="more-box clearFix">
                            <!--//下面隐藏	end-->
                            <dl>
                                <%--<dt>建筑类别：</dt>--%>
                            </dl>
                            <dl>
                                <%--<dt>特色楼盘：</dt>--%>
                            </dl>
                            <dl>
                                <%--<dt>开盘时间：</dt>--%>
                            </dl>
                            <dl>
                                <%--<dt>装修状况：</dt>--%>
                            </dl>
                        </div>
                        <a id="filterBar-more" href="javascript:void(0)">111111</a>
                    </div>
                    <div class="selected mt3" id="tj">
                    </div>
                </div>
            </div>
            <div class="w1190" style="display: inline">
                <%if (adv != null && adv.Count > 0 && adv[1].Pos == "2" && adv[1].Disurl != "")
                  {%><a href="<%=adv[1].Linkurl%>" target="_blank">
                      <img src="<%=TXCommons.GetConfig.ImgUrl+adv[1].Disurl%>" width="1190" height="60" alt="<%=adv[1].Alt%>" /></a>
                <%}
                  else
                  {%>
                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/ggt_w1000_h58.jpg" width="1190" height="60" />
                <%} %>
            </div>
            <div class="clearFix p_wrap4">
                <div class="this-left">
                    <div class="bq-box">
                    </div>
                    <div class="px-box">
                        选择排序: <a href="javascript:void(0)" id="mr">默认排序</a> <a href="javascript:void(0)"
                            id="sj">售价<i class="zt2" id="_sj"></i></a> <a href="javascript:void(0)" id="kp">开盘时间<i
                                class="zt2" id="_kp"></i></a> <a href="javascript:void(0)" id="rz">入住时间<i class="zt1"
                                    id="_rz"></i></a>
                        <div class="result-cout">
                            找到
                            <%=Model==null?0:Model.TotalItemCount%>个符合条件的楼盘
                        </div>
                    </div>
                    <!--推广链接begin-->
                    <div class="newtop clearFix" style="display: none">
                        <div class="fl">
                            <h3>
                                <a class="this-title linkB-333" href="#">住总万科橙 </a><span class="col666 normal">[普通住宅]</span></h3>
                            <p class="col666 mt5">
                                最低单位：<span class="colff6600 blod">56</span> 元</p>
                            <p class="col666 mt5">
                                项目位置：亦庄经济开发区博兴八路与秦河三街交...</p>
                            <p class="col666 mt5">
                                项目电话：<span class="colff6600 blod">400-888-2200</span> 转 <span class="colff6600 blod">
                                    20122</span> <span class=" ml70 col999">推广链接</span></p>
                        </div>
                        <div class="fr linenew">
                            <h3>
                                <a class="this-title linkB-333" href="#">住总万科橙 </a><span class="col666 normal">[普通住宅]</span></h3>
                            <p class="col666 mt5">
                                最低单位：<span class="colff6600 blod">56</span> 元</p>
                            <p class="col666 mt5">
                                项目位置：亦庄经济开发区博兴八路与秦河三街交...</p>
                            <p class="col666 mt5">
                                项目电话：<span class="colff6600 blod">400-888-2200</span> 转 <span class="colff6600 blod">
                                    20122</span> <span class=" ml70 col999">推广链接</span></p>
                        </div>
                    </div>
                    <!--推广链接end-->
                    <%if (Model != null)
                      {
                          
                    %>
                    <div class="clearFix list-groups-k">
                        <ul>
                            <%foreach (IndexPremises item in Model)
                              { %>
                            <li class="clearFix ">
                                <div class="linkright_box">
                                    <div class="pic-gr">
                                        <div class="pic-box">
                                            <a href="<%=NHWebUrl.PremisesIndexUrl(item.PremisesID) %>" target="_blank">
                                                <img src="<%=item.EffectPictureUrl==""?TXCommons.GetConfig.ImgUrl+"images/w186_h125.jpg":item.EffectPictureUrl+".186_125.jpg"%>"
                                                    width="186" height="125" onerror="javascript:this.src='<%=TXCommons.GetConfig.ImgUrl%>images/w186_h125.jpg'" /></a>
                                            <%--<div class="icons-gr-01" style="display: none">
                                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/i-01.gif" />
                                            </div>--%>
                                            <%--<div class="icons-gr-02">
                                                <i class="transparent50">12图</i></div>--%>
                                        </div>
                                    </div>
                                    <div class="zhuti">
                                        <h3>
                                            <a class="this-title linkB-333" href="<%=NHWebUrl.PremisesIndexUrl(item.PremisesID) %>"
                                                target="_blank">
                                                <%=item.PremisesName.Length > 10 ? item.PremisesName.Substring(0, 10) : item.PremisesName%></a><%if (item.SalesStatus == "0")
                                                                                                                                                 { %>
                                            <i class="zt-i-3"></i>
                                            <%}
                                                                                                                                                 else if (item.SalesStatus == "1")
                                                                                                                                                 { %><i class="zt-i-1"></i><%}
                                                                                                                                                 else if (item.SalesStatus == "2")
                                                                                                                                                 {%>
                                            <i class="zt-i-2"></i>
                                            <%} %>
                                            <%----推荐 Begin----%>
                                            <%if (item.IsRecommend == "1")
                                              { %>
                                            <i class="tuj_i"></i>
                                            <%} %>
                                            <%----推荐 End----%>
                                            <span class="col666 normal">
                                                <%var typeName = item.PropertyType.Split(',');
                                                  for (var i = 0; i < typeName.Length; i++)
                                                  {
                                                %>
                                                [<%=PremisesType.GetTypeName(typeName[i].ToString())%>]
                                                <%}%></span></h3>
                                        <p class="col666 ">
                                            产权：<%=item.PropertyRight %>&nbsp;年
                                            <%--户型图：
                                            <%
                                                                                var houseTypes = item.HouseTypes.Split(',');
                                                                                var houseTypesStatistics = item.HouseTypesStatistics.Split(',');
                                                                                if (houseTypes.Length > 0)
                                                                                {
                                                                                    var j = 0;
                                                                                    var f = "";
                                                                                    for (int i = 0; i < houseTypes.Length; i++)
                                                                                    {
                                                                                        if (Util.ToInt(houseTypes[i]) <= 5)
                                                                                        {%>
                                            <a href="<%=NHWebUrl.SizeChartUrl(item.PremisesID, 0, Util.ToInt(houseTypes[i]), 0, 1)%>"
                                                class="linkD mr5" target="_blank">
                                                <%=PremisesType.GetHouseTypes(houseTypes[i]) == "" ? "" : PremisesType.GetHouseTypes(houseTypes[i].ToString())%><%=houseTypesStatistics[i] == "" ? "" : "(" + houseTypesStatistics[i].ToString() + ")"%></a>
                                            <%}
                                                                                        else
                                                                                        {
                                                                                            f = houseTypes[i];
                                                                                            j += Util.ToInt(houseTypesStatistics[i]);
                                                                                        }
                                                                                    }
                                                                                    if (j > 0)
                                                                                    {
                                            %><a href="<%=NHWebUrl.SizeChartUrl(item.PremisesID, 0, Util.ToInt(f), 0, 1)%>" class="linkD mr5"
                                                target="_blank">
                                                <%=PremisesType.GetHouseTypes(f) == "" ? "" : PremisesType.GetHouseTypes(f)%><%="(" + j + ")"%></a><%
                                                                                    }
                                                                                }%>--%>
                                        </p>
                                        <p class="col666 mt10">
                                            开发商：<%--<a class="linkD ">--%><%=item.Developer%><%--</a>--%></p>
                                        <p class="col666 mt10">
                                            地址：<%= string.IsNullOrEmpty(PremisesType.GetRing(item.Ring)) ? "" : "[" + PremisesType.GetRing(item.Ring) + "]"%>
                                            <label title="<%=item.PremisesAddress%>">
                                                <%=item.PremisesAddress.Length > 30 ? item.PremisesAddress.Substring(0, 27) + "..." : item.PremisesAddress%><%--[六环以外]通州亦庄中关村科技园区<span class="ml5"><%=item.BusinessName%></span>--%>
                                            </label>
                                            <a href="#" class="seemap linkD ml10" style="display: none">查看地图</a></p>
                                        <div class="col666 mt5 clearFix">
                                            <div>
                                                <%var characteristic = item.CharacteristicName.Split(',');
                                                  if (characteristic.Length > 0)
                                                  {
                                                      for (int i = 0; i < characteristic.Length; i++)
                                                      {
                                                          if (characteristic[i].ToString() != "")
                                                          {
                                                              if (i == 0)
                                                              {%>
                                                <span class="bg_4d94f5">
                                                    <%=characteristic[i].ToString()%></span>
                                                <% }
                                                              if (i == 1)
                                                              { %><span class="bg_f27c70">
                                                                  <%=characteristic[i].ToString()%></span>
                                                <%}
                                                              if (i == 2)
                                                              { %><span class="bg_9898ff">
                                                                  <%=characteristic[i].ToString()%></span>
                                                <%}
                                                          }
                                                          else
                                                          { 
                                                %>&nbsp;<%
                                                          }
                                                      }
                                                  }
                                                %>
                                                <%--<span class="bg_4d94f5">学区房</span> <span class="bg_f27c70">独家</span> <span class="bg_9898ff">
                                                    高性价比</span>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="msg-box">
                                        <p class="font14 fontYaHei col666 mb10">
                                            <% var tmpPriceStr = (item.ReferencePrice.Equals("0") ?
                                                   string.Format("<span class=\"colff7100 font16\"><strong>{0}</strong></span>", "价格待定")
                                                   : string.Format("<span class=\"colff7100 font16\"><strong>{0}</strong></span>元/平方米", item.ReferencePrice));  %>
                                            均价
                                            <%= tmpPriceStr %>
                                        </p>
                                        <p class="font12 songti col666 mb10">
                                            电话：<span class="blod"><%=item.TelePhone%></span><%-- 转 <span class="blod">4016</span>--%></p>
                                        <p class="h42">
                                            <a href="#" class="s-link-f" style="display: none">网络摇号 > </a>
                                        </p>
                                    </div>
                                </div>
                                <div class="linkright">
                                    <div class="show-ylbox" style="<%=item.Houses!=""?"": " right:0"%>">
                                        <a class="fav" href="javascript:void(0);" onclick="AddUserFavorite('<%=item.PremisesID %>','<%=ViewData["UserId"]%>')">
                                            收藏楼盘</a><a class="plus" href="javascript:void(0);" data="<%=item.PremisesName %>"
                                                lang="<%=item.PremisesID %>" url="<%=NHWebUrl.PremisesIndexUrl(item.PremisesID) %>">对比</a></div>
                                    <%if (item.Houses != "")
                                      {%>
                                    <div class="show-ylbox1">
                                        <a class="tb_fylb2" href="javascript:void(0);">房源列表预览</a>
                                    </div>
                                    <%} %>
                                    <!--开始-->
                                    <%if (item.Houses != "")
                                      {%>
                                    <div class="xf_list_layer2">
                                        <div class="xf_list_layer_title2">
                                            <strong>房源营销活动</strong><a href="<%=NHWebUrl.PremisesHouseUrl(item.PremisesID,"","","") %>"
                                                class="right2">更多房源&gt;&gt;</a></div>
                                        <div class="xf_list_layer_cont2">
                                            <a class="left2"></a><a class="right2"></a>
                                            <div class="layer_list2">
                                                <ul>
                                                    <%if (item.Houses != "")
                                                      {
                                                          List<IndexHouse> foo = PubConstant.JsonTObj<List<IndexHouse>>(item.Houses.ToString());
                                                          for (int i = 0; i < foo.Count; i++)
                                                          {
                                                    %>
                                                    <li>
                                                        <dl>
                                                            <dt><a href="<%=NHWebUrl.PremisesHouseInfoUrl(foo[i].HouseID.ToString())%>" target="_blank">
                                                                <img src="<%=TXCommons.GetConfig.ImgUrl+"images/mrt.jpg"%>" /></a>
                                                                <p class="bj_tm">
                                                                </p>
                                                                <p class="tac">
                                                                    <%if (foo[i].ActivityType > 0)
                                                                      { %>
                                                                    <div class="tac">
                                                                        <%=TXCommons.NewHouseWeb.ActivitiesType.ActivitiesTypeByNo(foo[i].ActivityType)%></div>
                                                                    <%} %></p>
                                                            </dt>
                                                            <dd>
                                                                <h2>
                                                                    <a href="<%=NHWebUrl.PremisesHouseInfoUrl(foo[i].HouseID.ToString())%>">
                                                                        <%--楼栋名称+单元名称+层数+房号（序号）--%>
                                                                        <%=foo[i].BuildingName + foo[i].BuildingType + foo[i].UnitName + foo[i].TheFloor + "层" + foo[i].HouseNumber%>
                                                                    </a>
                                                                </h2>
                                                                <p>
                                                                    <%=foo[i].ActivityType == 2 ? "折后价格" : "起拍价"%>：<strong class="colff840b mr20"><%=foo[i].BidPrice.ToString().Substring(0, foo[i].BidPrice.ToString().IndexOf("."))%>万元</strong>市场价：<strong
                                                                        class="colff840b"><%=foo[i].TotalPrice.ToString().Substring(0, foo[i].TotalPrice.ToString().IndexOf("."))%>万元</strong></p>
                                                                <p>
                                                                    <%=foo[i].BuildingArea%>㎡ |
                                                                    <%=foo[i].Room%>室<%=foo[i].Hall%>厅 |
                                                                    <%=foo[i].TheFloor%>/<%=foo[i].AllFloor%>层</p>
                                                                <p>
                                                                    已有<i class="colff840b"><%=foo[i].BrowseCount%></i>人关注</p>
                                                            </dd>
                                                        </dl>
                                                    </li>
                                                    <%}
                                                      }%>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <%} %>
                                    <!--结束-->
                                </div>
                            </li>
                            <%} %>
                        </ul>
                    </div>
                    <%}
                      else
                      {%>
                    <div class="noResult_box2">
                        <span class="icons01">很抱歉，没有找到符合条件的信息。</span>
                    </div>
                    <div class="green_box clearFix">
                        <div class="fl">
                            <p>
                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/welcome.gif" /></p>
                            <p class="green_text">
                                在这里，您可以轻松发布楼盘，一站式解决开发商<br />
                                在网络营销中遇到所有问题！</p>
                            <p class="green_text1">
                                客服电话：<span class="colff6600 font24">400-999-3535</span></p>
                        </div>
                        <div class="fr">
                            <p>
                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/flp.gif" /></p>
                            <p>
                                <a href="<%=TXCommons.GetConfig.DevelopersUrl%>user/register" class="btn_yellow">立即申请入驻</a></p>
                        </div>
                    </div>
                    <%} %>
                    <%--<div class="page tac ">
                        <a class="no">首页</a><a class="no">上一页</a><a href="" class="hover">1</a><a href="">2</a><a
                            href="">3</a><a href="">4</a><a href="">5</a><a href="">6</a><a href="">下一页</a><a
                                href="">尾页</a><span class="col333 font12 mr10">共 <em class="col666">6</em> 页</span>
                        <span class="col333 font12 mr10">去第
                            <input name="" type="" class="pageInput" />
                            页</span><span class=" mr10">
                                <input type="button" class="pagetz" value="跳转">
                            </span>
                    </div>--%>
                    <!--begin 分页-->
                    <div class="page mt10">
                        <%=Model != null ? Html.Pager(Model.TotalItemCount, Model.PageSize, Model.CurrentPageIndex, null, null, new PagerOptions { SeparatorHtml = " ", ShowFirstLast = true, ShowPageIndexBox = true, FirstPageText = "首页", LastPageText = "尾页", GoButtonText = "跳转", CssClass = "", SelfDefineUrl = ViewData["conPage"].ToString(), CurrentPagerItemWrapperFormatString = "<a class=\"active\">" + Model.CurrentPageIndex + "</a>", SelfParameterLenght = -1, PrevCss = "", NextCss = "", FirstCss = "", LastCss = "", SpanCSS = "", ShowGoButtonCss = "btn" }, null, null, null).ToHtmlString() : ""%>
                    </div>
                    <!--end 分页-->
                </div>
                <div class="this-right">
                    <div class="style-gro-a">
                        <div class="mt38">
                            <%if (adv != null && adv.Count > 0 && adv[2].Pos == "3" && adv[2].Disurl != "")
                              {%>
                            <a href="<%=adv[2].Linkurl%>" target="_blank">
                                <img src="<%=TXCommons.GetConfig.ImgUrl+adv[2].Disurl%>" alt="<%=adv[2].Alt %>" /></a>
                            <%}
                              else
                              {%>
                            <a href="<%=TXCommons.GetConfig.GetSiteRoot%>/home/index" target="_blank">
                                <img src="<%=TXCommons.GetConfig.ImgUrl%>images/ggt_w250_h240.jpg" /></a>
                            <%} %>
                        </div>
                        <div class="r_title_lp">
                            <span class="title_span">最近浏览过的楼盘</span></div>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="premis">
                            <tbody>
                                <tr>
                                    <th width="48%" scope="col">
                                        楼盘名称
                                    </th>
                                    <th width="19%" scope="col">
                                        区县
                                    </th>
                                    <th width="33%" align="right" style="text-align: right" scope="col">
                                        价格（元/㎡）
                                    </th>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <!--热门楼盘-->
                    <div class="style-gro-a" id="divhot">
                    </div>
                    <!--广告-->
                    <div class="mt10" style="display: inline">
                        <%if (adv != null && adv.Count > 0 && adv[3].Pos == "4" && adv[3].Disurl != "")
                          {
                        %><a href="<%=adv[3].Linkurl%>" target="_blank">
                            <img src="<%=TXCommons.GetConfig.ImgUrl+adv[3].Disurl%>" alt="<%=adv[3].Alt %>" /></a>
                        <%
                          }
                          else
                          {%>
                        <img src="<%=TXCommons.GetConfig.ImgUrl%>images/ggt_w250_h70.jpg" />
                        <%} %>
                    </div>
                    <!--最新楼盘-->
                    <div class="style-gro-a" id="divnew">
                    </div>
                    <!--广告-->
                    <div class="mt10" style="display: inline">
                        <%if (adv != null && adv.Count > 0 && adv[4].Pos == "5" && adv[4].Disurl != "")
                          {
                        %><a href="<%=adv[4].Linkurl%>" target="_blank">
                            <img src="<%=TXCommons.GetConfig.ImgUrl+adv[4].Disurl%>" alt="<%=adv[4].Alt %>" /></a>
                        <%
                          }
                          else
                          {%>
                        <img src="<%=TXCommons.GetConfig.ImgUrl%>images/ggt_w250_h240_1.jpg" />
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="w1190" style="display: inline">
                <p class="mt15">
                    <%if (adv != null && adv.Count > 0 && adv[5].Pos == "6" && adv[5].Disurl != "")
                      {
                    %><a href="<%=adv[5].Linkurl%>" target="_blank">
                        <img src="<%=TXCommons.GetConfig.ImgUrl+adv[5].Disurl%>" width="1190" height="60" alt="<%=adv[5].Alt %>" /></a>
                    <%
                      }
                      else
                      {%>
                    <img src="<%=TXCommons.GetConfig.ImgUrl%>images/ggt_w1000_h60.jpg" width="1190" height="60" />
                    <%} %>
                </p>
            </div>
            <!--//bidFenyeBar-->
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
        <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/core/base.js")) %>"></script>
        <script type="text/javascript" src="<%=Url.Content(TXCommons.GetConfig.GetFileUrl("js/freeui/js/plugins/freeDialog.js")) %>"></script>
        <script type="text/javascript">
            //设置
            myFocus.set({
                id: 'myFocus_index', //ID
                pattern: 'mF_YSlider'//风格
            });
        </script>
        <script type="text/javascript">
            $(function () {
                var cla = '<%=ViewData["pxclass"] %>';
                if (cla == "") {
                    $("#mr").addClass("cur nopr");
                } else {
                    if (parseInt(cla) < 3) {
                        $("#sj").addClass("cur");
                    }
                    if (parseInt(cla) > 2 && parseInt(cla) < 5) {
                        $("#kp").addClass("cur");
                    }
                    if (parseInt(cla) > 4) {
                        $("#rz").addClass("cur");
                    }
                }
            });
        </script>
        <script src="<%=TXCommons.GetConfig.GetFileUrl("js/_search.js")%>" type="text/javascript"></script>
        <script type="text/javascript">
            //最近浏览过的楼盘
            function getLastPremisesList() {
                $("#premis").html("<tbody><tr><th scope=\"col\">楼盘名称</th><th scope=\"col\">区县</th><th width=\"33%\" align=\"right\" style=\"text-align:right\" scope=\"col\">价格（元/㎡）</th></tr>");
                $.ajax({
                    type: "get",
                    url: globalvar.siteRoot + "/Ajax/RecentlyViewed",
                    cache: false,
                    async: false,
                    dataType: "json",
                    success: function (result) {
                        if (result != null) {
                            var str = "";
                            for (var i = 0; i < result.length; i++) {
                                str += "<tr><td class=\"linkB\"><a href=\"" + result[i].Url + "\" class=\"linkB-666\" target=\"_blank\">" + result[i].Name + "</a></td><td class=\"linkB\">" + result[i].DName + "</td><td class=\"linkB\"><span class=\"colff840b mr10\">"+result[i].ReferencePrice.substring(0, result[i].ReferencePrice.indexOf(".") + 3)+"</span></td></tr>";
                                //<td align=\"right\"><span class=\"colff840b mr10\">" + result[i].ReferencePrice.substring(0, result[i].ReferencePrice.indexOf(".") + 3) + "</span></td>
                            }
                            // Math.round(result.premis[i].ReferencePrice * 100) / 100
                            $("#premis").append(str);
                        } else {
                            $("#premis").append("<tr><td colspan=\"3\">您还没有浏览过楼盘，快去看看吧</td></tr></tbody>");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                    }
                });
            }

            function getHotPremisesList(condition, d, cityid) {
                $.ajax({
                    type: "get",
                    url: globalvar.siteRoot + "/Search/HotPremisesList",
                    data: { conditions: condition, d: d, cityid: cityid },
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
            function getNewPremisesList(condition, d, cityid) {
                $.ajax({
                    type: "get",
                    url: globalvar.siteRoot + "/Search/NewPremisesList",
                    data: { conditions: condition, d: d, cityid: cityid },
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
            getLastPremisesList();
            getHotPremisesList("----3-1-10", "", <%=ViewData["cityId"]%>);
            getNewPremisesList("----3-1-10", "", <%=ViewData["cityId"]%>);


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

                $("#gfznclose").bind('click',function(){
                    $(".xf_llgdlp").hide();
                });

                //添加楼盘对比项
                $(".plus").click(function () {
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
                $("#lpdbclose").live("click",function(){
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
    </div>
</asp:Content>
