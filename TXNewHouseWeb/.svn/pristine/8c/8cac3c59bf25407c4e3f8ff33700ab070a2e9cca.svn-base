<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TXModel.Web.PremisesHouse>" %>
<!--房源基本信息-->
<div class="lr_box1 clearFix w1190 <%= Model.SalesStatus == 2 ? "mt15" : "" %>">
    <div class="bordertop-style clearFix">
        <div class="thispart-left">
            <div class="this-title">
                <strong>房源信息</strong></div>
            <div class="this-conx <%= Model.SalesStatus == 2 ? "" : "line_h20" %>">
                <div class="bg_yellow mb5" style="display: none;">
                    <p class="col333 font18 fontYaHei">
                        <%=Model.PremisesName%></p>
                    <p class="fontYaHei font14" style="font-size:14px;">
                        <%=Model.BuildingName%>
                        <%=Model.UnitName%>
                        <%=Model.Floor.ToString()%>层
                        <%=Model.HouseName%>号房</p>
                </div>
                <p>
                    <%=Model.PriceType==0?"市场":"参考" %>单价：<span class="font16 fontYaHei"><span class="colff6600"><%=(double)Model.SinglePrice%>元</span>/㎡</span></p>
                <p>
                    <%=Model.PriceType==0?"市场":"参考" %>总价：<span class="colff6600"><strong><%=(double)Model.TotalPrice%>万元/套</strong></span></p>
                <p>
                    首期付款：<span class="col333"><%=(double)Model.DownPayment%>元</span></p>
                <p>
                    销售状态：<span class="col333"><%=TXCommons.NewHouseWeb.HouseType.GetSalesStatus(Model.SalesStatus)%></span></p>
                <p>
                    预&nbsp;售&nbsp;证：<span class="col333"><%=Model.PermitPresale%></span></p>
                <div class="thisline">
                </div>
                <p>
                    建筑面积：<span class="col333"><%=Model.BuildingArea%>平方米</span></p>
                <p>
                    使用面积：<span class="col333"><%=Model.Area%>平方米</span></p>
                <p>
                    户&nbsp;&nbsp;&nbsp;&nbsp;型：<span class="col333"><%=Model.Room%>室<%=Model.Hall%>厅<%=Model.Toilet%>卫<%=Model.Kitchen%>厨</span></p>
                <p>
                    楼&nbsp;&nbsp;&nbsp;&nbsp;层：<span class="col333">第<%=Model.Floor%>层（共<%=Model.TheFloor%>层）</span></p>
                <p>
                    采光情况：<span class="col333"><%=TXCommons.NewHouseWeb.HouseType.GetOrientation(Model.Orientation)%></span></p>
                <p class="col999">
                    房源信息更新时间：<%=Model.UpdateTime%></p>
                <%
                    if (Model.SalesStatus > 2) //如果房源是非在售状态
                    {
                %>
                <div class="border_top_gray tac pad10 mt10">
                    <strong class="green font18 fontYaHei">本房源已售出 感谢您的关注</strong></div>
                <% 
                    }
                %>
            </div>
        </div>
        <div class="thispart-right">
            <ul class="r_qh_title clearFix">
                <li id="lipmt" onclick="test('pmt')" class="on"><a href="javascript:void(0)">楼栋平面图</a></li>
                <li id="lihxt" onclick="test('hxt')"><a href="javascript:void(0)">户型图</a></li>
            </ul>
            <div id="divpmt" class="this_img">
                <%if (ViewData["Planeimgsrc"] != null)
                  { %>
                <div <%=ViewData["roomtypeimgsrcstyle"]%> id="point_9" class="mapcsbs">
                    <a><span id="span_5" class="lp_bg_at">
                        <%--<%=ViewData["roomtypeimgsrcName"]%>--%></span></a></div>
                <div class="this-map">
                    <img src="<%=ViewData["Planeimgsrc"]%>" style="width: 830px; height: 365px;">
                </div>
                <% }
                  else
                  { %>
                <div class="this-map">
                    <img src="<%=(TXCommons.GetConfig.ImgUrl + "images/w212_h150.gif")%>" style="width: 830px;
                        height: 365px;">
                </div>
                <%} %>
                <div class="b_layer clearFix">
                    <%--<span class="fl">方向：南<i class="map_l"></i><i class="map_r"></i><i class="map_l1"></i><i
                        class="map_r1"></i>北</span>--%><a class="fr r_tb_map blue" href="<%=ViewData["Planeimgsrc"]??(TXCommons.GetConfig.ImgUrl + "images/w212_h150.gif")%>"
                            target="_blank">点击查看大图</a></div>
            </div>
            <div id="divhxt" class="this_img" style="display: none;">
                <div class="this-map">
                    <img src="<%=ViewData["roomtypeimgsrc"]??(TXCommons.GetConfig.ImgUrl + "images/w212_h150.gif")%>"
                        style="width: 830px; height: 365px;">
                </div>
                <div class="b_layer clearFix">
                    <%--<span class="fl">方向：南<i class="map_l"></i><i class="map_r"></i><i class="map_l1"></i><i
                        class="map_r1"></i>北</span>--%><a class="fr r_tb_map blue" href="<%=ViewData["roomtypeimgsrc"]??(TXCommons.GetConfig.ImgUrl + "images/w212_h150.gif")%>"
                            target="_blank">点击查看大图</a></div>
            </div>
        </div>
    </div>
    <div class="shadow">
    </div>
</div>
<!--分享栏-->
<div class="bg_ea mb10 mt15">
    <div class="w1190">
        <div class="bg_ea_title font12 col666">
            <div class="fl">
                <span class="colff840b mr3">
                    <%=Model.ViewCount %></span>人浏览</div>
            <span class="ml30 fl">分享到： </span>
            <!-- JiaThis Button BEGIN -->
            <div class="jiathis_style fl mt5">
                <a class="jiathis_button_qzone"></a><a class="jiathis_button_tsina"></a><a class="jiathis_button_tqq">
                </a><a class="jiathis_button_weixin"></a><a class="jiathis_button_renren"></a><a
                    href="http://www.jiathis.com/share" class="jiathis jiathis_txt jtico jtico_jiathis"
                    target="_blank"></a>
            </div>
            <script type="text/javascript" src="http://v3.jiathis.com/code/jia.js" charset="utf-8"></script>
            <!-- JiaThis Button END -->
            <!--<span class="right"><input type="button" value="加入选房单" class="btn_w90_yellow" /></span>-->
        </div>
    </div>
    <script type="text/javascript">
        function test(id) {
            $("#divpmt").hide();
            $("#divhxt").hide();
            $("#lipmt").removeClass();
            $("#lihxt").removeClass();
            $("#div" + id).show();
            $("#li" + id).addClass("on");
        }
    </script>
</div>
