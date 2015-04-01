<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/TXNewHouseManager.Master"
    Inherits="WebRentViewPage<TXModel.AdminPVM.PVM_NH_Premises>" %>

<%@ Import Namespace="TXOrm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=AdminPageInfo.PageTitle %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        a
        {
            /*color: #0068b7;*/
            text-decoration: none;
        }
    </style>
    <script src="http://api.map.baidu.com/api?v=1.5&ak=1d0c8b02751d95a0ea07d72c58d3b2c6"
        type="text/javascript"></script>
    <div class="current">
        <div class="root">
            <a href="javascript:void(0);">当前位置</a></div>
        <span><a href="javascript:void(0);">
            <%=AdminPageInfo.CardName %></a> <i>&gt;</i><a href="javascript:void(0);"><%=AdminPageInfo.FatherItemName %></a>
            <i>&gt;</i> <a href="javascript:void(0);">
                <%=AdminPageInfo.ItemName %></a><i>&gt;</i><a href="javascript:void(0);">编辑楼盘</a></span>
    </div>
    <!--//current-->
    <div class="data">
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="outer">
            <div class="dispose">
                <h5>
                    基本资料</h5>
                <span><em class="red">*</em> 为必填项</span></div>
            <div class="tab1">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                    <% if (!string.IsNullOrEmpty(Model.PageUrl))
                       {
                           %>
                    <tr>
                        <th width="11%">
                            信息来源页：
                        </th>
                        <td width="89%">
                            <a href="<%= Model.PageUrl %>" target="_blank"><%=Model.PageUrl %></a>
                        </td>
                    </tr>
                           <%
                       } %>
                    <tr>
                        <th width="11%">
                            <span class="mr2 red">*</span>楼盘名称：
                        </th>
                        <td width="89%">
                            <input id="name" type="text" maxlength="50" value="<%=Model.Name %>" /><span id="len_name"
                                style="color: #999999; padding-left: 10px;"></span>&nbsp;&nbsp;<span id="err_name"></span>
                        </td>
                    </tr>
                    <tr>
                        <th width="11%">
                            <span class="mr2 red">*</span>楼盘LOGO：
                        </th>
                        <td width="89%">
                            <%Html.RenderAction("UploadPremisesPhotoLogo", "Premises", new { guid = Model.InnerCode, picturetype = TXCommons.PictureModular.PremisesPictureType.Logo.ToString(), cityId = 0 }); %><span
                                id="err_logo"></span>
                            <input type="hidden" id="hid_logo" value="1" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>参考均价：
                        </th>
                        <td>
                            <input id="referenceprice" type="text" maxlength="15" value="<%=string.Format("{0:F0}", Model.ReferencePrice) %>" />&nbsp;&nbsp;元&nbsp;&nbsp;<span
                                id="err_referenceprice"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            咨询电话：
                        </th>
                        <td>
                            <input id="telephone" type="text" value="<%=Model.TelePhone %>" maxlength="20" />&nbsp;&nbsp;<span
                                id="err_telephone"></span>
                            <input id="chk400" type="checkbox" checked="checked" style="display: none;" <%--<%=Model.IsShow400 ? "checked=\"checked\"" : "" %>--%> />&nbsp;&nbsp;<%--使用400转接电话。--%>所填写的咨询电话会自动转换成我平台提供的400电话在前端显示
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>楼盘状态：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.SalesStatus, Model.PvsPremises.SalesState, new {@readonly = "readonly"}) %><span
                                id="err_SalesStatus"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>区域/板块：
                        </th>
                        <td>
                            <em>
                                <%= Html.DropDownListFor(model => model.ProvinceId, Model.PvsPremises.Provinces, new {@class = "w100"}) %></em>
                            <em>
                                <%= Html.DropDownListFor(model => model.CityId, Model.PvsPremises.Cities, new {@class = "w100"}) %></em>
                            <em>
                                <%= Html.DropDownListFor(model => model.DId, Model.PvsPremises.Districts, new {@class = "w100"}) %></em>&nbsp;&nbsp;<span
                                    id="err_DId"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>所属商圈：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.BId, Model.PvsPremises.Businesses, new {@class = "w100"}) %>&nbsp;&nbsp;<span
                                id="err_BId"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>环线位置：
                        </th>
                        <td>
                            <%= Html.DropDownListFor(model => model.Ring, Model.PvsPremises.RingList, new {@readonly = "readonly"}) %>&nbsp;&nbsp;<span
                                id="err_Ring"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>项目地址：
                        </th>
                        <td>
                            <input id="premisesaddress" type="text" class="txt22 w150" maxlength="200" value="<%=Model.PremisesAddress %>" />&nbsp;&nbsp;<span
                                id="err_premisesaddress"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>售楼地址：
                        </th>
                        <td>
                            <input id="salesaddress" type="text" class="txt22 w150" maxlength="200" value="<%=Model.SalesAddress %>" />&nbsp;&nbsp;<span
                                id="err_salesaddress"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>开发商：
                        </th>
                        <td>
                            <input id="developer" type="text" class="txt22 w150" maxlength="60" value="<%=Model.Developer %>" />&nbsp;&nbsp;<span
                                id="err_developer"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            代理商：
                        </th>
                        <td>
                            <input id="agent" type="text" class="txt22 w150" maxlength="60" value="<%=Model.Agent %>" />&nbsp;&nbsp;<em
                                class="ml10 col999">不填视为自销</em>&nbsp;&nbsp;<span id="err_agent"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="mr2 red">*</span>预售许可证：
                            <br />
                            <span id="err_salePermit"></span>
                        </th>
                        <td>
                            <% if (null == Model.SalePermitList || 0 == Model.SalePermitList.Count)
                               {
                            %>
                            <p>
                                <input type="text" id="salePermit_0" class="txt22 w150" maxlength="50" isdel="0"
                                    oriid="0" /><input type="button" id="btnSalePermitAdd_0" value="添加" class="btn1 mr"
                                        style="top: -1px;" onclick="premises_salepermit.add(0)" /><input type="button" id="btnSalePermitDel_0"
                                            value="删除" class="btn1 mr" style="display: none; top: -1px;" onclick="premises_salepermit.del('0')" /></p>
                            <% }
                               else
                               {
                                   var i = 0;
                                   for (; i < Model.SalePermitList.Count; i++)
                                   {
                            %>
                            <p style="padding-top: 6px;">
                                <input type="text" id="salePermit_<%= i %>" class="txt22 w150" maxlength="50" isdel="0"
                                    oriid="<%= Model.SalePermitList[i].Id %>" value="<%= Model.SalePermitList[i].Name %>" /><input
                                        type="button" id="btnSalePermitAdd_<%= i %>" value="添加" class="btn1 mr" style="display: none;
                                        top: -1px;" onclick="premises_salepermit.add('<%= i %>')" /><input type="button"
                                            id="btnSalePermitDel_<%= i %>" value="删除" class="btn1 mr" style="top: -1px;"
                                            onclick="premises_salepermit.del('<%= i %>')" /></p>
                            <%
                                   }
                            %>
                            <p style="padding-top: 6px;">
                                <input type="text" id="salePermit_<%= i %>" class="txt22 w150" maxlength="50" isdel="0"
                                    oriid="0" /><input type="button" id="btnSalePermitAdd_<%= i %>" value="添加" class="btn1 mr"
                                        style="top: -1px;" onclick="premises_salepermit.add('<%= i %>')" /><input type="button"
                                            id="btnSalePermitDel_<%= i %>" value="删除" class="btn1 mr" style="display: none;
                                            top: -1px;" onclick="premises_salepermit.del('<%= i %>')" /></p>
                            <% } %>
                        </td>
                    </tr>
                </table>
                <div class="dispose">
                    <h5>
                        建筑信息</h5>
                </div>
                <div class="tab1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                        <tr>
                            <th width="11%">
                                <span class="mr2 red">*</span>产权：
                            </th>
                            <td width="89%">
                                <input id="propertyright" type="text" maxlength="2" value="<%=Model.PropertyRight %>" />&nbsp;&nbsp;年<span
                                    id="err_propertyright"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span class="mr2 red">*</span>建筑面积：
                            </th>
                            <td>
                                <input id="buildingarea" type="text" value="<%=string.Format("{0:F0}", Model.BuildingArea) %>"
                                    maxlength="9" />&nbsp;&nbsp;平方米<span id="err_buildingarea"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span class="mr2 red">*</span>占地面积：
                            </th>
                            <td>
                                <input id="area" type="text" value="<%=string.Format("{0:F0}", Model.Area) %>" maxlength="9" />&nbsp;&nbsp;平方米<span
                                    id="err_area"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                总户数：
                            </th>
                            <td>
                                <input id="usercount" type="text" value="<%=Model.UserCount.Equals(0) ? "" : Convert.ToString(Model.UserCount) %>"
                                    maxlength="9" />&nbsp;&nbsp;户<span id="err_usercount"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                项目特色：
                            </th>
                            <td>
                                <ul id="ul_tag_info">
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <th>
                            </th>
                            <td>
                                <div class="bbtt_dd">
                                    <div>
                                        <% for (int i = 0; i < Model.PremisesFeatures.Count; i++)
                                           {
                                        %>
                                        <input type="button" class="btn3" value="<%= ((PremisesFeature) Model.PremisesFeatures[i]).Name %>"
                                            onclick="premises_characteristic.add('<%= ((PremisesFeature) Model.PremisesFeatures[i]).Id %>','<%= ((PremisesFeature) Model.PremisesFeatures[i]).Name %>')" />
                                        <% } %>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span class="mr2 red">*</span>建筑类别：
                            </th>
                            <td>
                                <%= Html.DropDownListFor(model => model.BuildingType, Model.PvsPremises.BuildingTypes, new {@readonly = "readonly"}) %>&nbsp;&nbsp;<span
                                    id="err_BuildingType"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="dispose">
                    <h5>
                        物业类型</h5>
                </div>
                <div class="tab1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                        <tr>
                            <th width="11%">
                                <span class="mr2 red">*</span>物业类型：
                            </th>
                            <td width="89%">
                                <% var tmpPropertytype = "," + Model.PropertyType + ","; %>
                                <input id="propertytype_1" type="checkbox" value="1" class="btn_checkbox" <%=tmpPropertytype.IndexOf(",1,", StringComparison.Ordinal) > -1 ? "checked=\"checked\"" : "" %> />住宅&nbsp;&nbsp;
                                <input id="propertytype_2" type="checkbox" value="2" class="btn_checkbox" <%=tmpPropertytype.IndexOf(",2,", StringComparison.Ordinal) > -1 ? "checked=\"checked\"" : "" %> />写字楼&nbsp;&nbsp;
                                <input id="propertytype_3" type="checkbox" value="3" class="btn_checkbox" <%=tmpPropertytype.IndexOf(",3,", StringComparison.Ordinal) > -1 ? "checked=\"checked\"" : "" %> />别墅&nbsp;&nbsp;
                                <input id="propertytype_4" type="checkbox" value="4" class="btn_checkbox" <%=tmpPropertytype.IndexOf(",4,", StringComparison.Ordinal) > -1 ? "checked=\"checked\"" : "" %> />商业&nbsp;&nbsp;<span
                                    id="err_propertytype"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                容积率：
                            </th>
                            <td>
                                <input id="arearatio" type="text" value="<%=Model.AreaRatio.Equals(0) ? "" : string.Format("{0:0.00}", Model.AreaRatio) %>"
                                    maxlength="9" />&nbsp;&nbsp;<span id="err_arearatio"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                得房率：
                            </th>
                            <td>
                                <input id="roomrate" type="text" value="<%=Model.RoomRate.Equals(0) ? "" : string.Format("{0:0.00}", Model.RoomRate) %>"
                                    maxlength="9" />&nbsp;&nbsp;%&nbsp;&nbsp;<span id="err_roomrate"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                物业费：
                            </th>
                            <td>
                                <input id="propertyprice" type="text" value="<%=Model.PropertyPrice.Equals(0) ? "" : string.Format("{0:0.00}", ((double)Model.PropertyPrice)) %>"
                                    maxlength="9" />&nbsp;&nbsp;/月/平方米&nbsp;&nbsp;<span id="err_propertyprice"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                车位信息：
                            </th>
                            <td>
                                <input id="parkinglot" type="text" value="<%=Model.ParkingLot %>" maxlength="9" />&nbsp;&nbsp;位&nbsp;&nbsp;<span
                                    id="err_parkinglot"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                物业公司：
                            </th>
                            <td>
                                <input id="propertycompany" type="text" class="txt22 w150" maxlength="60" value="<%=Model.PropertyCompany%>" />&nbsp;&nbsp;<span
                                    id="err_propertycompany"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                绿化率：
                            </th>
                            <td>
                                <input id="greeningrate" type="text" value="<%=Model.GreeningRate.Equals(0) ? "" : string.Format("{0:0.00}", Model.GreeningRate) %>" />&nbsp;&nbsp;%&nbsp;&nbsp;<span
                                    id="err_greeningrate"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="dispose">
                    <h5>
                        交通配套</h5>
                </div>
                <div class="tab1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                        <tr>
                            <th width="11%">
                                楼盘地图标注：
                            </th>
                            <td width="89%">
                                <input id="btn_coordinate" type="button" class="btn4" value="点击进行地图标记" />&nbsp;&nbsp;<span
                                    id="err_coordinate"></span>
                                <input type="hidden" id="hid_lng" value="<%=Model.Lng %>" />
                                <input type="hidden" id="hid_lat" value="<%=Model.Lat %>" />
                            </td>
                        </tr>
                        <tr>
                            <th width="11%">
                                周边地铁：
                            </th>
                            <td width="89%">
                                <input id="btn_subway" type="button" class="btn4" value="点击添加" />&nbsp;&nbsp;<span
                                    id="err_subway"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                &nbsp;
                            </th>
                            <td>
                                <div id="show_subway">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                交通介绍：
                            </th>
                            <td>
                                <textarea id="trafficintroduction" cols="" rows="" class="txtare_w500" style="width: 500px;
                                    height: 100px;"><%=Model.TrafficIntroduction %></textarea>
                                <span id="err_trafficintroduction"></span>
                            </td>
                        </tr>
                        <tr>
                            <th valign="top">
                                配套介绍：
                            </th>
                            <td>
                                <textarea id="supportingintroduction" cols="" rows="" class="txtare_w500" style="width: 500px;
                                    height: 100px;"><%=Model.SupportingIntroduction %></textarea>
                                <span id="err_supportingintroduction"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="dispose">
                    <h5>
                        楼盘介绍</h5>
                </div>
                <div class="tab1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                        <tr>
                            <th width="11%" valign="top">
                                楼盘介绍：
                            </th>
                            <td>
                                <textarea id="premisesintroduction" cols="" rows="" class="txtare_w500" style="width: 500px;
                                    height: 100px;"><%=Model.PremisesIntroduction %></textarea>
                                <span id="err_premisesintroduction"></span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="dispose">
                    <h5>
                        楼盘沙盘图</h5>
                </div>
                <div class="tab1">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tb1">
                        <tr>
                            <th width="11%" valign="top">
                            </th>
                            <td width="89%" align="left">
                                <input id="UpdateSandboxCoordinate" type="hidden" value="<%=Model.LoadingSank %>" />
                                <input id="SandboxCoordinate" name="SandboxCoordinate" type="hidden" />
                                <%Html.RenderAction("UploadPremisesPhotoSp", "Premises", new { guid = Model.InnerCode, picturetype = TXCommons.PictureModular.PremisesPictureType.PremisesLIST.ToString(), cityId = 0 }); %>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clearFix w900">
                    <input id="btn_submit" type="button" value="发布楼盘" class="btn2 mr10" />
                </div>
            </div>
            <!-- InstanceEndEditable -->
        </div>
    </div>
    <div id="div_traffic_subway" style="padding: 0;">
    </div>
    <div id="my_dialog" style="display: none;">
        <%Html.RenderAction("Sandbox", "Premises"); %>
    </div>
    <input type="hidden" id="hid_guid" value="<%= Model.InnerCode %>" />
    <input type="hidden" id="DeveloperId" value="<%= Model.DeveloperId %>" />
    <input type="hidden" id="PremisesId" value="<%= Model.Id %>" />
    <input type="hidden" id="hid_isupdatesandbox" value="0" />
    <script type="text/javascript">
        var SalePermitMaxId = <%=Model.SalePermitList == null ? 0 : (Model.SalePermitList.Count == 0 ? 0 : Model.SalePermitList.Count) %>; // 预售许可证编号

        $(function () {
            if ($("#Ring option").length < 1) $("#Ring").closest("tr").hide();
            $("#name").bind("keyup", premises.evt_name_keyup);
            $("#btn_coordinate").bind("click", map_baidu.show);
            $("#btn_subway").bind("click", traffic_subway.show_dialog);
            $("#btn_submit").bind("click", premises.submit);

            premises.dis_len_name();

            var selProvince = $("#ProvinceId");
            var selCity = $("#CityId");
            var selDistricts = $("#DId");
            var selBusinesses = $("#BId");
              var selRing = $("#Ring");
            $(selProvince).change(function () {
                clearListItems(selCity);
                clearListItems(selDistricts);
                clearListItems(selBusinesses);
                 clearListItems(selRing);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("cities") %>?geographyCode=' + this.value, selCity);
            });

            $(selCity).change(function () {
                clearListItems(selDistricts);
                clearListItems(selBusinesses);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("districts") %>?geographyCode=' + this.value, selDistricts);
                loadGeographyHideEmptyItems('<%=Url.SiteUrl().GeographyLocation("Ring") %>?geographyCode=' + this.value, selRing);

            });

            $(selDistricts).change(function () {
                clearListItems(selBusinesses);
                loadGeographyItems('<%=Url.SiteUrl().GeographyLocation("businesses") %>?geographyCode=' + this.value, selBusinesses);
            });
            
            // 加载 特色
            premises_characteristic.init([<%=Model.Characteristic_JsonString %>]);
            
            // 加载 地铁
            traffic_subway.new_stations = [<%=Model.Tid_JsonString %>];
            traffic_subway.selected_stations = [<%=Model.Tid_JsonString %>];
            traffic_subway.displaySelectedStation_InMainPage();
        });

        var premises = {
            icon_suc: "win ml10",
            icon_err: "lose ml10",
            err_content: "请填写",


            // 验证 名称
            valid_name: function () {
                var eerr = $("#err_name");
                var value = $.trim($("#name").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html(premises.err_content);
                    return false;
                }

                if (25 - Math.round(premises.getStringLengthForByte(value) / 2) < 0) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("楼盘名称限定25个汉字以内");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 LOGO
            valid_logo: function() {
                var eerr = $("#err_logo");
                var value = $.trim($("#hid_logo").val());
                if ("0" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html(premises.err_content);
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 参考价格
            valid_referenceprice: function () {
                var eerr = $("#err_referenceprice");
                var value = $.trim($("#referenceprice").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html(premises.err_content);
                    return false;
                }

//                var pattern = /^(([1-9]{1}\d+)(\.\d{2})?)$/;
                var pattern = /^(\d+)$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("价格只能输入数字");
                    return false;
                }

//                if (!(1000 < parseFloat(value)
//                    && parseFloat(value) < 1000000000)) {
//                    eerr.attr("class", premises.icon_err);
//                    eerr.html("价格范围为大于1千元小于10亿元");
//                    return false;
//                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 咨询电话
            valid_telephone: function () {
                var eerr = $("#err_telephone");
                var value = $.trim($("#telephone").val());
                if ("" != value) {
                    var pattern = /^[0-9\-()（）]{7,18}$/; //^\d{1,15}$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("请输入正确的电话");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                } else {
                    // 不输入，则不判断
                    premises.clearErrStyle('err_telephone');
                    return true;
                }
            },

            // 验证 楼盘状态
            valid_SalesStatus: function () {
                var eerr = $("#err_SalesStatus");
                var value = $.trim($("#SalesStatus").val());
                if ("-1" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 区域
            valid_DId: function () {
                var eerr = $("#err_DId");
                var value = $.trim($("#DId").val());
                if ("0" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 商圈
            valid_BId: function () {
                var eerr = $("#err_BId");
                var value = $.trim($("#BId").val());
                if ("0" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 环线
            valid_Ring: function () {
                if ($("#Ring").is(":visible")) {
                    var eerr = $("#err_Ring");
                    var value = $.trim($("#Ring").val());
                    if (value<=0) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("请填写");
                        return false;
                    }

                    eerr.attr("class", premises.icon_suc);
                    eerr.html("");
                    return true;
                }
                return true;
            },

            // 验证 项目地址
            valid_premisesaddress: function () {
                var eerr = $("#err_premisesaddress");
                var value = $.trim($("#premisesaddress").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                var count = premises.getStringLengthForByte(value);
                if (300 < count) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("最多可输入150个汉字");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 售楼地址
            valid_salesaddress: function () {
                var eerr = $("#err_salesaddress");
                var value = $.trim($("#salesaddress").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                var count = premises.getStringLengthForByte(value);
                if (300 < count) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("最多可输入150个汉字");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 开发商
            valid_developer: function () {
                var eerr = $("#err_developer");
                var value = $.trim($("#developer").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                var count = premises.getStringLengthForByte(value);
                if (60 < count) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("最多可输入30个汉字");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 代理商
            valid_agent: function () {
                var eerr = $("#err_agent");
                var value = $.trim($("#agent").val());
                if ("" != value) {
                    var count = premises.getStringLengthForByte(value);
                    if (60 < count) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多可输入30个汉字");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_agent");
                return true;
            },

            // 验证 预售许可证
            valid_salepermit: function () {
                var eerr = $("#err_salePermit");
                var count = premises_salepermit.get_count();
                if (count < 1) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }
                
                if (10 < count) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("超出最大数量10个");
                    return false;
                }

                premises.clearErrStyle("err_salePermit");
                return true;
            },

            // 验证 产权
            valid_propertyright: function () {
                var eerr = $("#err_propertyright");
                var value = $.trim($("#propertyright").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                var pattern = /^[1-9]{1}\d{0,1}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请输入正确的产权");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 建筑面积
            valid_buildingarea: function () {
                var eerr = $("#err_buildingarea");
                var value = $.trim($("#buildingarea").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                if (9 < value.length) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("最多可输入9位正数");
                    return false;
                }

                var pattern = /^[1-9]{1}\d{0,8}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请输入正确的面积数");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 占地面积
            valid_area: function () {
                var eerr = $("#err_area");
                var value = $.trim($("#area").val());
                if ("" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                if (9 < value.length) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("最多可输入9位正数");
                    return false;
                }

                var pattern = /^[1-9]{1}\d{0,8}$/;
                if (!pattern.test(value)) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请输入正确的面积数");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 总户数
            valid_usercount: function () {
                var eerr = $("#err_usercount");
                var value = $.trim($("#usercount").val());
                if ("" != value) {
                    var pattern = /^[1-9]{1}\d{0,8}$/;
                    if (9 < value.length) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多可输入9位正数");
                        return false;
                    } else if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("请输入正确的总户数");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle();
                return true;
            },

            // 验证 建筑类别
            valid_BuildingType: function () {
                var eerr = $("#err_BuildingType");
                var value = $.trim($("#BuildingType").val());
                if ("-1" == value) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },

            // 验证 物业类型
            valid_propertytype: function () {
                var eerr = $("#err_propertytype");
                var count = $("input[id^='propertytype_']:checked").length;
                if (0 == count) {
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请填写");
                    return false;
                }

                eerr.attr("class", premises.icon_suc);
                eerr.html("");
                return true;
            },
            
            // 获取 物业类型
            get_propertytype: function() {
                var infos = "";
                $("input[id^='propertytype_']:checked").each(function() {
                    infos += $(this).val() + ",";
                });
                return infos.substr(0, infos.length - 1);
            },

            // 验证 容积率
            valid_arearatio: function () {
                var eerr = $("#err_arearatio");
                var value = $.trim($("#arearatio").val());
                if ("" != value) {
                    //var pattern = /^[1-9]{1}\d{0,}$/;
                    var pattern = /^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多保留两位小数的正数");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_arearatio");
                return true;
            },

            // 验证 得房率
            valid_roomrate: function () {
                var eerr = $("#err_roomrate");
                var value = $.trim($("#roomrate").val());
                if ("" != value) {
                    //var pattern = /^[1-9]{1}\d{0,}$/;
                    var pattern = /^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多保留两位小数的正数");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_roomrate");
                return true;
            },

            // 验证 物业费
            valid_propertyprice: function () {
                var eerr = $("#err_propertyprice");
                var value = $.trim($("#propertyprice").val());
                if ("" != value) {
                    //var pattern = /^[1-9]{1}\d{0,}$/;
                    var pattern = /^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多保留两位小数的正数");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_propertyprice");
                return true;
            },

            // 验证 车位信息
            valid_parkinglot: function () {
                var eerr = $("#err_parkinglot");
                var value = $.trim($("#parkinglot").val());
                if ("" != value) {
                    var pattern = /^[1-9]{1}\d{0,}$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("请输入正数");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_parkinglot");
                return true;
            },

            // 验证 物业公司
            valid_propertycompany: function () {
                var eerr = $("#err_propertycompany");
                var value = $.trim($("#propertycompany").val());
                if ("" != value) {
                    var count = premises.getStringLengthForByte(value);
                    if (60 < count) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多可输入30个汉字");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_propertycompany");
                return true;
            },

            // 验证 绿化率
            valid_greeningrate: function () {
                var eerr = $("#err_greeningrate");
                var value = $.trim($("#greeningrate").val());
                if ("" != value) {
                    //var pattern = /^[1-9]{1}\d{0,}$/;
                    var pattern = /^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$/;
                    if (!pattern.test(value)) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多保留两位小数的正数");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_greeningrate");
                return true;
            },

            // 验证 交通介绍
            valid_trafficintroduction: function () {
                var eerr = $("#err_trafficintroduction");
                var value = $.trim($("#trafficintroduction").val());
                if ("" != value) {
                    var count = premises.getStringLengthForByte(value);
                    if (1000 < count) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多可输入500个汉字");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_trafficintroduction");
                return true;
            },

            // 验证 配套介绍
            valid_supportingintroduction: function () {
                var eerr = $("#err_supportingintroduction");
                var value = $.trim($("#supportingintroduction").val());
                if ("" != value) {
                    var count = premises.getStringLengthForByte(value);
                    if (1000 < count) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多可输入500个汉字");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_supportingintroduction");
                return true;
            },

            // 验证 楼盘介绍
            valid_premisesintroduction: function () {
                var eerr = $("#err_premisesintroduction");
                var value = $.trim($("#premisesintroduction").val());
                if ("" != value) {
                    var count = premises.getStringLengthForByte(value);
                    if (1200 < count) {
                        eerr.attr("class", premises.icon_err);
                        eerr.html("最多可输入600个汉字");
                        return false;
                    } else {
                        eerr.attr("class", premises.icon_suc);
                        eerr.html("");
                        return true;
                    }
                }

                premises.clearErrStyle("err_premisesintroduction");
                return true;
            },

            // 验证沙盘图及标记
            valid_sandbox: function() {
                var eerr = $("#err_sandbox");
                if ("" == $("#PicSrc").val()) {
                    // 上传沙盘图
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请上传沙盘图");
                    return false;
                }

                if ("" == $("#SandboxCoordinate").val()) {
                    // 标记
                    eerr.attr("class", premises.icon_err);
                    eerr.html("请在沙盘图上标记楼栋位置");
                    return false;
                }

                premises.clearErrStyle("err_sandbox");
                return true;
            },









            // 输入楼盘名称事件
            evt_name_keyup: function () {
                premises.clearErrStyle("err_name");
                premises.dis_len_name();
            },

            // 显示还可输入字符数量
            dis_len_name: function () {
                var length = Math.round(premises.getStringLengthForByte($("#name").val()) / 2);
                $("#len_name").html("还可输入" + ((25 - length) > 0 ? (25 - length) : 0) + "字");
            },

            //  获取字符串的字节长度
            getStringLengthForByte: function (str) {
                return str.replace(/[^\x00-\xFF]/g, '**').length;
            },

            // 清空错误提示
            clearErrStyle: function (name) {
                $("#" + name).removeAttr("class");
                $("#" + name).html("");
            },

            // 提交
            submit: function() {
                if (!(premises.valid_name()
                    & premises.valid_logo()
                    & premises.valid_referenceprice()
                    & premises.valid_telephone()
                    & premises.valid_SalesStatus()
                    & premises.valid_DId()
                    & premises.valid_BId()
                    & premises.valid_Ring()
                    & premises.valid_premisesaddress()
                    & premises.valid_salesaddress()
                    & premises.valid_developer()
                    & premises.valid_agent()
                    // & premises.valid_salepermit()
                    // -- 建筑信息
                    & premises.valid_propertyright()
                    & premises.valid_buildingarea()
                    & premises.valid_area()
                    & premises.valid_usercount()
                    & premises.valid_BuildingType()
                    // -- 物业类型
                    & premises.valid_propertytype()
                    & premises.valid_arearatio()
                    & premises.valid_roomrate()
                    & premises.valid_propertyprice()
                    & premises.valid_parkinglot()
                    & premises.valid_propertycompany()
                    & premises.valid_greeningrate()
                    // -- 交通配套
                    & premises.valid_trafficintroduction()
                    & premises.valid_supportingintroduction()
                    // -- 楼盘介绍
                    & premises.valid_premisesintroduction()
                    // -- 沙盘图
                    & premises.valid_sandbox())) {
                    
                    return;
                }
                

                var url = '<%= Url.SiteUrl().Premises_action("SubmitHandle") %>';
                var data = {
                    // ----- ----- 基本资料
                    Id: '<%=Model.Id %>',
                    // 楼盘
                    Name: $("#name").val(),
                    // 参考均价
                    ReferencePrice: $("#referenceprice").val(),
                    // 咨询电话
                    TelePhone: $("#telephone").val(),
                    // 接入400电话
                    IsShow400: $("#chk400").prop("checked") ? 1 : 0,
                    // 楼盘状态
                    SalesStatus: $("#SalesStatus").val(),
                    // 省市
                    ProvinceId: $("#ProvinceId").val(),
                    ProvinceName: $("#ProvinceId option:selected").text(),
                    // 城市
                    CityId: $("#CityId").val(),
                    CityName: $("#CityId option:selected").text(),
                    // 区域
                    DId: $("#DId").val(),
                    DName: $("#DId option:selected").text(),
                    // 商圈
                    BId: $("#BId").val(),
                    BName: $("#BId option:selected").text(),
                    // 环线位置
                    Ring:$("#Ring").is(":visible")? $("#Ring").val():"0",
                    // 项目地址
                    PremisesAddress: $("#premisesaddress").val(),
                    // 售楼地址
                    SalesAddress: $("#salesaddress").val(),
                    // 开发商
                    Developer: $("#developer").val(),
                    // 代理商
                    Agent: $("#agent").val(),
                    // 预售许可证列表
                    PermitPresales_Json: premises_salepermit.get_info(),
                    
                    // ----- ----- 建筑信息
                    // 产权
                    PropertyRight: $("#propertyright").val(),
                    // 建筑面积
                    BuildingArea: $("#buildingarea").val(),
                    // 占地面积
                    Area: $("#area").val(),
                    // 总户数
                    UserCount: $("#usercount").val(),
                    // 项目特色
                    Characteristic: premises_characteristic.get_ids(),
                    // 建筑类别
                    BuildingType: $("#BuildingType").val(),

                    // ----- ----- 物业类型
                    // 物业类型
                    PropertyType: premises.get_propertytype(),
                    // 容积率
                    AreaRatio: $("#arearatio").val(),
                    // 得房率
                    RoomRate: $("#roomrate").val(),
                    // 物业费
                    PropertyPrice: $("#propertyprice").val(),
                    // 车位信息
                    ParkingLot: $("#parkinglot").val(),
                    // 物业公司
                    PropertyCompany: $("#propertycompany").val(),
                    // 绿化率
                    GreeningRate: $("#greeningrate").val(),

                    // ----- ----- 交通配套
                    // 经度
                    Lng: $("#hid_lng").val(),
                    // 纬度
                    Lat: $("#hid_lat").val(),
                    // 周边地铁
                    TId: traffic_subway.getSubwayIds(),
                    // 交通介绍
                    TrafficIntroduction: $("#trafficintroduction").val(),
                    // 配套介绍
                    SupportingIntroduction: $("#supportingintroduction").val(),
                    
                    // ----- ----- 楼盘介绍
                    // 楼盘介绍
                    PremisesIntroduction: $("#premisesintroduction").val(),

                    SandboxCoordinate: $("#SandboxCoordinate").val(),
                    InnerCode: $("#hid_guid").val(),

                    // 是否修改沙盘图
                    IsUpdateSandbox: $("#hid_isupdatesandbox").val(),

                    ram: Math.random()
                };

                $("#btn_submit").attr("disabled", "disabled");
                admincoms.WaittingBar.show();
                $.post(url, data, function(msg) {
                    $("#btn_submit").removeAttr("disabled");
                    admincoms.WaittingBar.close();
                    if (msg.suc) {
                        alert("保存成功");
                        window.location.href = "<%=ViewData["backurl"] %>";
                        return;
                    }
                    
                    if (-1 == msg.state) {
                        var eerr = $("#err_name");

                        eerr.attr("class", premises.icon_err);
                        eerr.html("楼盘名称已存在");
                    }
                    
                    alert(msg.msg);
                });
            }
        };

        // 预售许可证
        var premises_salepermit = {
            // 添加
            add: function (curId) {

                var p = $("#btnSalePermitAdd_" + curId).parent();

                $("#btnSalePermitAdd_" + SalePermitMaxId).attr("style", "display: none;");
                $("#btnSalePermitDel_" + SalePermitMaxId).removeAttr("style");

                SalePermitMaxId += 1;

                var tml = "<p style=\" padding-top:6px;\">";
                tml += "<input type=\"text\" id=\"salePermit_" + SalePermitMaxId + "\" class=\"txt22 w150\" maxlength=\"20\" isdel=\"0\" oriid=\"0\" />";
                tml += "<input type=\"button\" id=\"btnSalePermitAdd_" + SalePermitMaxId + "\" value=\"添加\" class=\"btn1 mr\" style=\"top:1px;\" onclick=\"premises_salepermit.add('" + SalePermitMaxId + "')\" />";
                tml += "<input type=\"button\" id=\"btnSalePermitDel_" + SalePermitMaxId + "\" value=\"删除\" class=\"btn1 mr\" style=\"display: none; top:-1px;\"  onclick=\"premises_salepermit.del('" + SalePermitMaxId + "')\" />";
                tml += "</p>";
                $(tml).insertAfter(p);
            },

            // 删除
            del: function (curId) {
                if (!confirm("您确定要执行该操作?")) {
                    return;
                }
                var p = $("#btnSalePermitDel_" + curId).parent();
                p.attr("style", "display: none;");
                $("#salePermit_" + curId).attr("isdel", "1");
            },

            // 获取数量
            get_count: function () {
                var count = 0;
                var p = $("#btnSalePermitAdd_0").parent();
                var ttr = p.parent();
                ttr.find("p").each(function () {
                    var tmp = $(this).find("input[type='text']");
                    if (tmp.attr("isdel") == "0"
                && $.trim(tmp.val()) != "") {
                        count++;
                    }
                });
                return count;
            },

            // 获取信息
            get_info: function () {
                // 没有任何可用数据
                if (0 == premises_salepermit.get_count()) {
                    return "[]";
                }

                // 有可用数据
                var list = "[";
                $("input[id^='salePermit_']").each(function () {

                    if ($(this).attr("oriid") != "0") {
                        // 源数据

                        if ($(this).attr("isdel") == "1") {
                            // 标记删除

                            list += "{\"Id\":\"" + $(this).attr("oriid") + "\", \"Name\":\"" + $(this).val() + "\", \"IsDel\":\"1\"},";
                        } else {
                            if ($.trim($(this).val()) == "") {
                                // 清空数据信息(删除)

                                list += "{\"Id\":\"" + $(this).attr("oriid") + "\", \"Name\":\"" + $(this).val() + "\", \"IsDel\":\"1\"},";
                            } else {
                                // 保存数据

                                list += "{\"Id\":\"" + $(this).attr("oriid") + "\", \"Name\":\"" + $(this).val() + "\", \"IsDel\":\"0\"},";
                            }
                        }
                    } else {
                        // 新数据

                        if ($(this).attr("isdel") == "0") {

                            if ("" != $.trim($(this).val())) {
                                // 数据判空

                                list += "{\"Id\":\"0\", \"Name\":\"" + $(this).val() + "\", \"IsDel\":\"0\"},";
                            }
                        }
                    }
                });
                list += "]";

                return list;
            }
        };

        // 项目特色
        var premises_characteristic = {

            // 已选项
            selected: [],

            // 添加
            add: function(id, name) {
                if (8 <= premises_characteristic.get_count()) {
                    alert("最多选择8个特色");
                    return;
                }
                if (!premises_characteristic.isExist(id)) {
                    premises_characteristic.selected.push({ id: id, name: name });
                    premises_characteristic.display();
                }
            },

            // 删除
            del: function (id) {
                if (!premises_characteristic.isExist(id)) {
                    premises_characteristic.display();
                    return;
                }

                for (var i = 0; i < premises_characteristic.selected.length; i++) {
                    if (id == premises_characteristic.selected[i].id) {
                        premises_characteristic.selected.splice(i, 1);
                        break;
                    }
                }
                premises_characteristic.display();
            },

            // 验证 是否已选
            isExist: function (id) {
                if (0 == premises_characteristic.selected.length) {
                    return false;
                }

                var flag = false;
                for (var i = 0; i < premises_characteristic.selected.length; i++) {
                    if (id == premises_characteristic.selected[i].id) {
                        flag = true;
                        break;
                    }
                }

                return flag;
            },

            // 显示
            display: function () {
                if (0 == premises_characteristic.selected.length) {
                    $("#ul_tag_info").html("");
                    return;
                }

                var html = "";
                for (var i = 0; i < premises_characteristic.selected.length; i++) {
                    html += "<div class=\"colsebox_1 mr10\" data='" + premises_characteristic.selected[i].id + "'>";
                    html += premises_characteristic.selected[i].name;
                    html += "<a href=\"javascript:void(0);\" class=\"del_li cha\" onclick=\"premises_characteristic.del('" + premises_characteristic.selected[i].id + "')\">&nbsp;</a>";
                    html += "</div>";
                }

                $("#ul_tag_info").html(html);
            },

            // 获取已选择的数量
            get_count: function () {
                return premises_characteristic.selected.length;
            },

            // 获取已选Id
            get_ids: function () {
                if (0 == premises_characteristic.selected.length) {
                    return "";
                }

                var ids = "";
                for (var i = 0; i < premises_characteristic.selected.length; i++) {
                    ids += premises_characteristic.selected[i].id;
                    if (i < premises_characteristic.selected.length - 1) {
                        ids += ",";
                    }
                }

                return ids;
            },
            
            // 初始化 已选项目特色
            init: function(json) {
                if (0 == json.length) {
                    return;
                }

                premises_characteristic.selected = json;
                premises_characteristic.display();
            }
        };

        // 百度地图
        var map_baidu = {
            id: 1,

            lng: $("#hid_lng").val(),
            lat: $("#hid_lat").val(),

            //  显示
            show: function() {
                var name = "map_" + map_baidu.id;
                $("<div/>").attr("id", name).appendTo("body");
                $("#" + name).load('<%=Url.SiteUrl().ActionUrl("Coordinate","Premises") %>', {cityId:$("#CityId").val(), ram: Math.random() }, function() {
                    $("#" + name).dialog({
                        modal: true,
                        title: "楼盘地图标注",
                        draggable: false,
                        width: 560,
                        height: 510,
                        resizable: false,
                        buttons: {
                            "确定": function() {
                                BaiduOp.getNewPoint(); //设置经纬度
                                alert("标记成功");
                                $(this).dialog("close");
                            },
                            "取消": function() {
                                $(this).dialog("close");
                            }
                        },

                        close: function() {
                            $("#" + name).html("");
                            map_baidu.id += 1;
                        }
                    });
                });
            },

            // 更新坐标
            updateLngLat: function(lng, lat) {
                map_baidu.lng = lng;
                map_baidu.lat = lat;
                $("#hid_lng").val(lng);
                $("#hid_lat").val(lat);
            }
        };


        // 地铁站
        var traffic_subway = {
            // 原数据库地铁站点
            new_stations: [], // {"id":id, "name":name}

            // 选中的地铁站点json
            selected_stations: [], // {"id":id, "name":name}

            // 当前地铁线地铁站json
            line_stations: [],

            // 显示弹窗
            show_dialog: function() {

                var cityId = $("#CityId").val();
                if (0 == cityId || "" == cityId) {
                    alert("请先选择城市后再设置地铁站。");
                    return;
                }

                $("#div_traffic_subway").load('<%=Url.SiteUrl().ActionUrl("Traffic","Premises") %>', { cityId: cityId, ram: Math.random() }, function() {
                    $("#div_traffic_subway").dialog({
                        modal: true,
                        title: "请设置周边地铁",
                        draggable: false,
                        width: 560,
                        buttons: {
                            "确定": function() {
                                traffic_subway.btn_Enter();
                                $(this).dialog("close");
                            },
                            "取消": function() {
                                traffic_subway.btn_Cancel();
                                $(this).dialog("close");
                            }
                        },
                        open: function() {
                            traffic_subway.selected_stations = traffic_subway.cloneData(traffic_subway.new_stations);
                            traffic_subway.displaySelectedStation();
                        }
                    });
                });
            },

            // 显示站点列表
            displayStations: function(id) {
                var url = '<%=Url.SiteUrl().ActionUrl("TrafficStation","Premises") %>';
                $("#traffic_lines a").removeClass("hover");
                $("#line_" + id).addClass("hover");
                traffic_subway.line_stations = [];
                $.getJSON(url, { geographyCode: id }, function(response) {
                    if (!(response || response.success)) {
                        return;
                    }

                    if (response.items) {
                        var strHtml = "";
                        $.each(response.items, function() {
                            strHtml += "<a href=\"javascript:void(0);\" id=\"station_" + this.GeographyCode + "\" onclick=\"traffic_subway.selectedStation('" + this.GeographyCode + "','" + this.GeographyName + "')\">" + this.GeographyName + "</a>";
                            traffic_subway.line_stations.push({ "id": this.geographyCode, "name": this.GeographyName });
                        });
                        $("#traffic_liner").html(strHtml);

                        traffic_subway.initSelectedStation();
                    }
                });
            },

            // 选中地铁站点
            selectedStation: function(id, name) {
                if (!traffic_subway.isExist_InSelectedStation(id)) {
                    if (10 <= traffic_subway.selected_stations.length) {
                        alert("请注意，同一楼盘最多可以添加10个站名。");
                        return;
                    }
                    traffic_subway.selected_stations.push({ "id": id, "name": name });
                    $("#station_" + id).addClass("hover");

                    traffic_subway.displaySelectedStation();
                }
            },

            // 初始化 已选择的地铁站
            initSelectedStation: function() {
                $("a[id^=station_]").each(function() {
                    for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                        if ($(this).attr("id") == ("station_" + traffic_subway.selected_stations[i].id)) {
                            $(this).addClass("hover");
                        }
                    }
                });
            },

            // 删除已选择的地铁站
            delSelectedStation: function(id) {
                for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                    if (id == traffic_subway.selected_stations[i].id) {
                        traffic_subway.selected_stations.splice(i, 1);
                        $("#station_" + id).removeClass("hover");
                        break;
                    }
                }

                traffic_subway.displaySelectedStation();
            },

            // 当前id是否已经被选中
            isExist_InSelectedStation: function(id) {
                if (0 == traffic_subway.selected_stations.length) {
                    return false;
                }

                for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                    if (id == traffic_subway.selected_stations[i].id) {
                        return true;
                    }
                }

                return false;
            },

            // 显示已选中的站点
            displaySelectedStation: function() {
                $("#ul_traffic_info").html("");

                if (0 == traffic_subway.selected_stations.length) {
                    return;
                }

                var html = "";
                for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                    html += "<li id=\"li_selected_station_" + traffic_subway.selected_stations[i].id + "\"><span>" + traffic_subway.selected_stations[i].name + "</span><em class=\"del_li cha\" onclick=\"traffic_subway.delSelectedStation('" + traffic_subway.selected_stations[i].id + "')\"> </em></li>";
                }
                $("#ul_traffic_info").html(html);
            },

            // 将选中的地铁站点显示在主页面上(点击确定)
            btn_Enter: function() {
                traffic_subway.new_stations = traffic_subway.cloneData(traffic_subway.selected_stations);
                traffic_subway.displaySelectedStation_InMainPage();
            },

            // 将选中的地铁站点显示在主页面上(点击取消)
            btn_Cancel: function() {
                traffic_subway.selected_stations = traffic_subway.cloneData(traffic_subway.new_stations);
                traffic_subway.displaySelectedStation_InMainPage();
            },

            // 将选中的地铁站点显示在主页面上
            displaySelectedStation_InMainPage: function() {
                if (0 == traffic_subway.new_stations.length) {
                    $("#show_subway").html("");
                    return;
                }

                var html = "<ul class=\"tbUl\">";
                for (var i = 0; i < traffic_subway.new_stations.length; i++) {
                    html += "<li data=\"" + traffic_subway.new_stations[i].id + "\">";
                    html += "<span>" + traffic_subway.new_stations[i].name + "</span>";
                    html += "<em class=\"show_del_li cha\" onclick=\"traffic_subway.delSelectedStation_InMainPage('" + traffic_subway.new_stations[i].id + "')\"> </em>";
                    html += "</li>";
                }
                html += "</ul>";

                $("#show_subway").html(html);
            },

            // 克隆数据
            cloneData: function(json) {
                var newArray = new Array();
                for (var i = 0; i < json.length; i++) {
                    newArray.push({ "id": json[i].id, "name": json[i].name });
                }
                return newArray;
            },

            // 从主页面上删除地铁站
            delSelectedStation_InMainPage: function(id) {
                traffic_subway.delSelectedStation(id);
                traffic_subway.new_stations = traffic_subway.cloneData(traffic_subway.selected_stations);
                traffic_subway.displaySelectedStation_InMainPage();
            },

            // 获取地铁站编号集合(逗号分隔)
            getSubwayIds: function() {
                if (0 == traffic_subway.new_stations.length) {
                    return "";
                }

                var idArray = [];
                for (var i = 0; i < traffic_subway.new_stations.length; i++) {
                    idArray.push(traffic_subway.new_stations[i].id);
                }

                return idArray.join(",");
            }
        };
        $(function() {
          jsnewsandbox.initSandBoxData("<%=Model.Id %>");
         /*沙盘标注*/
            $("#Sandbox").bind("click", function () {
                //alert("A");
                Show_dialog.Sandbox();
              
                
            });

        });
       
        /*标注房间位置*/
        var Show_dialog = {
            Sandbox: function () {
                $("#my_dialog").dialog({
                    modal: true,
                    title: "沙盘标注",
                    draggable: false,
                    width: 730

                });
            }
        };

        // 沙盘图
        var jsnewsandbox = {

            data: [],

            dataArray: [],

            // 获取沙盘图片
            getPic: function () {
                return $("#PicSrc").val();
            },

            // 设置沙盘图标记
            setSandBoxData: function (t) {
                jsnewsandbox.data = t;

                jsnewsandbox.saveDataToArray(t);

                $("#SandboxCoordinate").val(JSON.stringify(jsnewsandbox.data));
            },

            // 初始化沙盘图标记(修改楼盘时使用)
            initSandBoxData: function (PremisesId) {
                //var json = eval($("#SandboxCoordinate").val());
                var t = jsnewsandbox.getSandBoxList(PremisesId);
                jsnewsandbox.setSandBoxData(t);
            },

            // 将沙盘标记坐标保存为数组形式
            saveDataToArray: function (t) {
                jsnewsandbox.dataArray = [];
                for (var i = 0; i < t.length; i++) {
                    jsnewsandbox.dataArray.push(t[i].SandBox);
                    jsnewsandbox.dataArray.push(t[i].Number);
                    jsnewsandbox.dataArray.push(t[i].CoordX);
                    jsnewsandbox.dataArray.push(t[i].CoordY);
                }
            },

            // 获取沙盘图标记
            getSandBoxData: function () {
                return jsnewsandbox.data;
            },

            // 获取沙盘图
            getSandBoxDataString: function () {
                return jsnewsandbox.dataArray;
            },

            //加载沙盘坐标
            getSandBoxList: function (PremisesId) {
                var t = [];
//                $.getJSON('<%=Url.SiteUrl().Common("GetSandList","NhBuilding") %>', { r: Math.random(), id: PremisesId }, function(response) {
//                    if (response) {
//                        $.each(response, function(i, item) {
//                            t.push({ "SandBox": item.SandBox, "Number": item.Number, "CoordX": item.CoordX, "CoordY": item.CoordY });
//                        });
//                  
//                    }
//                });
               $.ajax({
                    type: "POST",
                    async: false,
                    url:'<%=Url.SiteUrl().Common("GetSandList","NhBuilding") %>',
                    data: "id=" + PremisesId+"&r="+ Math.random(),
                    success: function (msg) {
                        $.each(msg, function (i, item) {
                            t.push({ "SandBox": item.SandBox, "Number": item.Number, "CoordX": item.CoordX, "CoordY": item.CoordY });
                        });
                        //$("#SandboxCoordinate").val(JSON.stringify(AddLoadingSank));
                    }
                });
                return t;
            }

        };
    </script>
</asp:Content>
