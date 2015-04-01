<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" MasterPageFile="~/Views/Shared/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-发布楼盘
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="btit clearFix mt5">
            <div class="tit_font fl">
                基本信息</div>
            <div class="font12 fr mr35">
                <span class="red">*</span>为必填项</div>
        </div>
        <form id="PropertyForm" method="post">
        <div class="mt15 mb15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="11%">
                        <span class="mr2 red">*</span>楼盘名称：
                    </th>
                    <td width="89%">
                        <input type="text" id="PropertyName" name="PropertyName" class="txt22 w200" maxlength="25" />
                        <span class="ie_valign_5 no" id="ErrorRepeat" style="display: none">相同地区楼盘名称重复</span>
                    </td>
                </tr>
                <tr>
                    <th width="11%">
                        <span class="mr2 red">*</span>楼盘LOGO：
                    </th>
                    <td width="89%">
                        <%Html.RenderAction("UploadPremisesPhotoLogo", "Premises", new { guid = ViewData["guid"].ToString(), picturetype = TXCommons.PictureModular.PremisesPictureType.Logo.ToString(), cityId = 0 }); %>
                        <span class="ie_valign_5 no" id="PreLogo" style="display: none">请上传楼盘LOGO</span>
                    </td>
                </tr>
                <input type="hidden" id="guid" name="guid" value="<%=ViewData["guid"].ToString()%>" />
                <tr>
                    <th>
                        <span class="mr2 red">*</span>参考均价：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="ReferencePrice" name="ReferencePrice" maxlength="9" />
                        元 <span class="ml10 col999">填0即为价格待定</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>咨询电话：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="TelePhone" name="TelePhone" maxlength="16" />
                        <input id="chk400" type="checkbox" checked="checked" style="display: none;" /><%--使用400转接电话。--%><span class="ml10 col999">所填写的咨询电话会自动转换成我平台提供的400电话在前端显示</span>
                        <input type="hidden" name="IsShow400" id="IsShow400" value="1" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>楼盘状态：
                    </th>
                    <td>
                        <select class="selcss w100" id="SalesStatus" name="SalesStatus">
                            <option value="-1">选择</option>
                            <option value="0">待售</option>
                            <option value="1">在售</option>
                            <option value="2">售罄</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>区域/板块：
                    </th>
                    <td>
                        <select class="selcss w100" id="ProvinceId" name="ProvinceId">
                        </select>
                        <select class="selcss w100" id="CityId" name="CityId">
                        </select>
                        <select class="selcss w100" id="DId" name="DId">
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>所属商圈：
                    </th>
                    <td>
                        <select class="selcss w100" id="BId" name="BId">
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>环线位置：
                    </th>
                    <td>
                        <select class="selcss w100" id="Ring" name="Ring">
                            <option value="0">环线</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>项目地址：
                    </th>
                    <td>
                        <input type="text" class="txt22 w300" id="PremisesAddress" name="PremisesAddress" maxlength="150" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>售楼地址：
                    </th>
                    <td>
                        <input type="text" class="txt22 w300" id="salesAddress" name="salesAddress" maxlength="150" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>开发商：
                    </th>
                    <td>
                        <input type="text" class="txt22 w300" id="Developer" name="Developer" maxlength="30" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red"></span>代理商：
                    </th>
                    <td>
                        <input type="text" class="txt22 w300" id="Agent" name="Agent" maxlength="30" /><span class="ml10 col999">不填视为自销</span>
                    </td>
                </tr>
                <%--  <tr>
                    <th>
                        <span class="mr2 red">*</span>预售许可证：
                    </th>
                    <td>
                        <%--  <input type="text" class="txt22 w300" id="SalePermit" name="SalePermit" />
                <input type="text" class="txt22 w300" id="SalePermit" name="SalePermit" maxlength="20" /><br />
                <p id="PresaleId">
                </p>
                <a href="javascript:void(0);" onclick="add();">新增许可证</a> </td> </tr>--%>
            </table>
        </div>
        <div class="btit">
            <div class="tit_font">
                建筑信息</div>
        </div>
        <div class="mt15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="11%">
                        <span class="mr2 red">*</span>产权：
                    </th>
                    <td width="89%">
                        <input type="text" class="txt22 w100" id="PropertyRight" name="PropertyRight" maxlength="2" />
                        年
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>建筑面积：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="BuildingArea" name="BuildingArea" maxlength="9" />
                        平方米
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>占地面积：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="Area" name="Area" maxlength="9" />
                        平方米
                    </td>
                </tr>
                <tr>
                    <th>
                        总户数：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="UserCount" name="UserCount" maxlength="9" />
                        户
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
                            <div style="margin-left: -15px;" id="PremisesFeature">
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>建筑类别：
                    </th>
                    <td>
                        <select class="selcss w100" id="BuildingType" name="BuildingType">
                            <option value="-1">选择</option>
                            <option value="1">板楼</option>
                            <option value="2">塔楼</option>
                            <option value="3">砖楼</option>
                            <option value="4">砖混</option>
                            <option value="5">平房</option>
                            <option value="6">钢混</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btit">
            <div class="tit_font">
                物业类型</div>
        </div>
        <div class="mt15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="11%">
                        <span class="mr2 red">*</span>物业类型：
                    </th>
                    <td width="89%">
                        <input type="checkbox" class="bnt_checkbox" value="1" name="PropertyType" />住宅<input type="checkbox" class="bnt_checkbox" name="PropertyType" value="2" />写字楼<input type="checkbox" class="bnt_checkbox" name="PropertyType" value="3" />别墅<input type="checkbox" class="bnt_checkbox" name="PropertyType" value="4" />商业
                    </td>
                </tr>
                <tr>
                    <th>
                        容积率：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="AreaRatio" name="AreaRatio" maxlength="5" />
                    </td>
                </tr>
                <tr>
                    <th>
                        得房率：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="RoomRate" name="RoomRate" maxlength="5" />
                        %
                    </td>
                </tr>
                <tr>
                    <th>
                        物业费：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="PropertyPrice" name="PropertyPrice" maxlength="9" />
                        /月/平方米
                    </td>
                </tr>
                <tr>
                    <th>
                        车位信息：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="ParkingLot" name="ParkingLot" maxlength="9" />位
                    </td>
                </tr>
                <tr>
                    <th>
                        物业公司：
                    </th>
                    <td>
                        <input type="text" class="txt22 w200" id="PropertyCompany" name="PropertyCompany" maxlength="30" />
                    </td>
                </tr>
                <tr>
                    <th>
                        绿化率：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="GreeningRate" name="GreeningRate" maxlength="5" />
                        %
                    </td>
                </tr>
            </table>
        </div>
        <div class="btit mt15">
            <div class="tit_font">
                交通配套</div>
        </div>
        <div class="mt15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="11%">
                        楼盘地图标注：
                    </th>
                    <td width="89%">
                        <input id="Coordinate" type="button" class="bnt_w123" value="点击进行地图标记" />
                        <input type="hidden" id="Lng" name="Lng" />
                        <input type="hidden" id="Lat" name="Lat" />
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        周边地铁：
                    </th>
                    <td>
                        <input type="button" class="btn_w80" value="点击添加" id="set_traffic" />
                        <input id="Traffic" name="Traffic" type="hidden" />
                        <div class="mt10" id="show_traffic">
                        </div>
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        交通介绍：
                    </th>
                    <td>
                        <textarea class="txtare_w500" id="TrafficIntroduction" name="TrafficIntroduction"></textarea>
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        配套介绍：
                    </th>
                    <td>
                        <textarea class="txtare_w500" id="SupportingIntroduction" name="SupportingIntroduction"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btit mt15">
            <div class="tit_font">
                楼盘介绍</div>
        </div>
        <div class="mt15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="11%" valign="top">
                        楼盘介绍：
                    </th>
                    <td>
                        <textarea class="txtare_w500" id="PremisesIntroduction" name="PremisesIntroduction"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div class="btit mt15">
            <div class="tit_font">
                <span class="mr2 red">*</span>楼盘沙盘图</div>
        </div>
        <input id="SandboxCoordinate" name="SandboxCoordinate" type="hidden" />
        <%-- <input id="UpdateSandboxCoordinate" name="UpdateSandboxCoordinate" type="hidden" />--%>
        <%Html.RenderAction("UploadPremisesPhotoSp", "Premises", new { guid = ViewData["guid"].ToString(), picturetype = TXCommons.PictureModular.PremisesPictureType.PremisesLIST.ToString(), cityId = 0 }); %>
        <span class="ie_valign_5 no" id="SandError" style="display: none">请先上传楼盘沙盘图 并且标注</span>
        <div class="loubox_bg">
            &nbsp;</div>
        <div class="boxcent20 mt15">
            <input type="submit" id="Create" class="bnt_w120" value="发布进入下一步" />
        </div>
        <input id="CityName" name="CityName" type="hidden" />
        <input id="ProvinceName" name="ProvinceName" type="hidden" />
        <input id="DistrictName" name="DistrictName" type="hidden" />
        <input id="BusinessName" name="BusinessName" type="hidden" />
        </form>
    </div>
    <script src="http://api.map.baidu.com/api?v=1.5&ak=1d0c8b02751d95a0ea07d72c58d3b2c6" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.js")%>" language="javascript" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/dialog_new.js")%>" language="javascript" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

        $(function () {
            var dialog;
            function show_dialog(url) {
                if (dialog != undefined) {
                    dialog.close();
                    dialog == undefined;
                }
                dialog = new Dialog({ type: 'url', value: url }, { showTitle: false, id: "div_dia", modal: false, fixed: true });
                dialog.show();
            }

            $("#set_traffic").bind("click", function () {
                show_dialog(mainUrl + "Premises/Traffic?CityID=" + $("#CityId").val() + "&ram=" + Math.random());
            });
            $("#Coordinate").bind("click", function () {
                Dialog.showDialog('url', "<%=TXCommons.GetConfig.BaseUrl%>Premises/Coordinate?CityID=" + $("#CityId").val() + "&ram=" + Math.random(), '<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif');
            });

            $("#Sandbox").bind("click", function () {
                show_dialog(mainUrl + "Premises/Sandbox?ram=" + Math.random());
            });
        });

        var arrayObj = new Array(); //创建一个数组

        $(document).on("click", ".del_li", function () {
            var v = $(this).closest("div").attr("data");
            $.each(arrayObj, function (i, item) {
                if (item == v) {
                    arrayObj.splice(i, 1);
                }
            });
            $(this).closest("div").remove();
            return false;
        });
        function addTag(id, text) {
            var f = 0;
            var len = arrayObj.length;
            if (8 <= len) {
                alert("最多添加8个特色");
                return;
            }
            $.each(arrayObj, function (i, item) {
                if (item == id) {
                    f = 1;
                    $(this).closest("div").remove();
                }
            });
            if (f == 0) {
                arrayObj.push(id);
                $("#ul_tag_info").append("<div class=\"colsebox_1 mr10\" data='" + id + "'>" + text + "<a href=\"#\" class=\"del_li cha\">&nbsp;</a><input id=Characteristic" + id + " type=\"hidden\" name=\"Characteristic\" value='" + id + "'/></div>");
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

        var jsPremises = {
            mainUrl: "<%=TXCommons.GetConfig.BaseUrl%>",

            ProvinceId: $("#ProvinceId"), //省份
            CityId: $("#CityId"), //城市
            DId: $("#DId"), //区域
            BId: $("#BId"), //商圈
            Ring: $("#Ring"), //环线
            PropertyName: $("#PropertyName"), //楼盘名称

            //楼盘特色列表
            GetPremisesFeatureList: function () {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/GetPremisesFeatureList",
                    success: function (data) {
                        $.each(data, function (i, item) {
                            $("#PremisesFeature").append("<input type=\"button\" class=\"btn_w80 ml15\" value=\"" + item.Name + "\" onclick=\"javascript:addTag('" + item.Id + "','" + item.Name + "')\" />");
                        });
                    }
                });
            },
            //验证开发商是否认证
            //未认证的开发商不能发布新房数据及营销活动
            Checkdevelopers: function () {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Home/GetCurrentUserVipState",
                    success: function (msg) {
                        if (msg != 1) {
                            alert("您尚未通过身份认证，认证后方可操作!");
                            window.location.href = mainUrl + "Home/identification";
                        }
                    }
                });
            },

            //获取省份列表
            GetProvinceList: function () {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/SearchProvinceList",
                    data: "CountryId=1",
                    success: function (msg) {
                        $.each(msg, function (i, item) {
                            // var stmp = v == item.Id ? "selected=\"selected\"" : "";
                            var stmp = item.Id;
                            $("<option></option>").val(item.Id).text(item.Name).appendTo(jsPremises.ProvinceId);
                        });
                    }
                });
            },
            //获取环线列表
            GetRingList: function (cityId) {
                jsPremises.Ring.empty();
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/Ring",
                    data: "CityId=" + cityId,
                    success: function (msg) {
                        if (msg.success && msg.items && msg.items.length > 0) {
                            jsPremises.Ring.closest("tr").show();
                            $.each(msg.items, function (i, item) {
                                $("<option></option>").val(item.Key).text(item.Value).appendTo(jsPremises.Ring);
                            });
                        } else {
                            jsPremises.Ring.closest("tr").hide();
                            $("<option></option>").val("0").text("环线").appendTo(jsPremises.Ring);
                        }
                    }
                });
            },
            GetCityList: function (ProvinceId) {
                jsPremises.CityId.empty();
                $("<option></option>").val("-1").text("选择城市").appendTo(jsPremises.CityId);

                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/SearchCityList",
                    data: "ProvinceId=" + ProvinceId,
                    success: function (msg) {
                        $.each(msg, function (i, item) {
                            var stmp = item.Id;
                            $("<option></option>").val(item.Id).text(item.Name).appendTo(jsPremises.CityId);
                        });
                    }
                });
            },
            GetDidList: function (CityId) {
                jsPremises.DId.empty();
                $("<option></option>").val("-1").text("选择区域").appendTo(jsPremises.DId);
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/SearchDidList",
                    data: "CityId=" + CityId,
                    success: function (msg) {
                        $.each(msg, function (i, item) {
                            var stmp = item.Id;
                            $("<option></option>").val(item.Id).text(item.Name).appendTo(jsPremises.DId);
                        });
                    }
                });
            },
            //商圈列表
            GetBussinessList: function (districtId) {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/SearchBussiness",
                    data: "districtId=" + districtId,
                    success: function (msg) {
                        $.each(msg, function (i, item) {
                            var stmp = item.Id;
                            $("<option></option>").val(item.Id).text(item.Name).appendTo(jsPremises.BId);
                        });
                    }
                });
            },
            //清空列表
            ClearItems: function () {
                jsPremises.ProvinceId.empty();
                $("<option></option>").val("-1").text("选择省份").appendTo(jsPremises.ProvinceId);
                $("<option></option>").val("-1").text("选择城市").appendTo(jsPremises.CityId);
                $("<option></option>").val("-1").text("选择区域").appendTo(jsPremises.DId);
                $("<option></option>").val("-1").text("选择商圈").appendTo(jsPremises.BId);
            },
            CheckTwo: function (Name, Did) {
                $.ajax({
                    type: "POST",
                    url: mainUrl + "Premises/CheckPremises",
                    data: "Name=" + Name + "&Did=" + Did,
                    success: function (msg) {
                        if (msg != 0) {
                            //alert("相同地区 楼盘名称重复");
                            $("#ErrorRepeat").show();
                            jsPremises.PropertyName.focus();
                            jsPremises.DId.val(-1);
                        }
                        else {
                            $("#ErrorRepeat").hide();
                        }
                    }

                });

            }



        };
        $(document).ready(function () {
            jsPremises.GetPremisesFeatureList(); //楼盘特色列表

            jsPremises.Checkdevelopers(); //未认证的开发商不能发布新房数据及营销活动

            jsPremises.ClearItems(); //默认显示下拉列表

            jsPremises.GetProvinceList(); //显示省份列表


            $("#ProvinceId").change(function () {
                jsPremises.GetCityList(jsPremises.ProvinceId.val());
                jsPremises.DId.empty();
                jsPremises.BId.empty();
                jsPremises.Ring.empty();
                $("<option></option>").val("-1").text("选择区域").appendTo(jsPremises.DId);
                $("<option></option>").val("-1").text("选择商圈").appendTo(jsPremises.BId);
                $("<option></option>").val("0").text("环线").appendTo(jsPremises.Ring);
            });

            $("#CityId").change(function () {
                jsPremises.GetDidList(jsPremises.CityId.val());
                jsPremises.GetRingList(jsPremises.CityId.val());
                jsPremises.BId.empty();
                $("<option></option>").val("-1").text("选择商圈").appendTo(jsPremises.BId);
            });

            //检验 相同地区是楼盘是否重复
            $("#PropertyName").blur(function () {
                jsPremises.CheckTwo(jsPremises.PropertyName.val(), jsPremises.DId.val());
            });

            $("#DId").change(function () {
                //检查是否 重复
                jsPremises.CheckTwo(jsPremises.PropertyName.val(), jsPremises.DId.val());

                jsPremises.BId.empty();
                $("<option></option>").val("-1").text("选择商圈").appendTo(jsPremises.BId);

                //商圈列表
                jsPremises.GetBussinessList(jsPremises.DId.val());
            });


        });


        jQuery.validator.addMethod("stringCheck", function (value, element) {
            return this.optional(element) || /^[\u4e00-\u9fa5a-zA-Z0-9]+$/.test(value);
        }, "<span class=\"ie_valign_5 no\">只能包括中文字、英文字母、数字</span>");


        jQuery.validator.addMethod("IsSalesStatus", function (value, element) {
            return this.optional(element) || value < 0 ? false : true;
        }, "<span class=\"ie_valign_5 no\">请选择楼盘状态！</span>");



        jQuery.validator.addMethod("IsBId", function (value, element) {
            return this.optional(element) || value < 0 ? false : true;
        }, "<span class=\"ie_valign_5 no\">请选择所属商圈！</span>");

        jQuery.validator.addMethod("IsRing", function (value, element) {
            if ($(element).is(":visible")) {
                return this.optional(element) || value <= 0 ? false : true;
            }
            return true;
        }, "<span class=\"ie_valign_5 no\">请选择环线位置！</span>");

        jQuery.validator.addMethod("IsBuildingType", function (value, element) {
            return this.optional(element) || value < 0 ? false : true;
        }, "<span class=\"ie_valign_5 no\">请选择建筑类别！</span>");

        jQuery.validator.addMethod('PropertyTypeChecked', function (value, element) {
            var checkedCount = 0;
            $("input[name='PropertyType']:checkbox:checked").each(function () {
                checkedCount = 1;
            });
            value = checkedCount;
            return this.optional(element) || value <= 0 ? false : true;

        }, "<span class=\"ie_valign_5 no\">请至少选择一项！</span>");

        jQuery.validator.addMethod("IsNumber", function (value, element) {
            return this.optional(element) || (/(^-?[1-9]\d*$)/.test(value));
        }, "<span class='ie_valign_5 no'>限整数。</span>");

        jQuery.validator.addMethod("IsFloat2", function (value, element) {
            return this.optional(element) || (/^(([0-9]+)|([0-9]+\.[0-9]{1,2}))$/.test(value));
        }, "<span class='ie_valign_5 no'>最多保留两位小数的正数。</span>");

        jQuery.validator.addMethod("IsNumber", function (value, element) {
            return this.optional(element) || (/(^-?[1-9]\d*$)/.test(value));
        }, "<span class='ie_valign_5 no'>限整数。</span>");

        jQuery.validator.addMethod("isZhengNumber", function (value, element) {
            return this.optional(element) || (/(^[1-9]\d*$)/.test(value));
        }, "<span class='ie_valign_5 no'>请输入正确的面积数。</span>");

        jQuery.validator.addMethod("IsWhere", function (value, element) {
            return this.optional(element) || value < 0 ? false : true;
        }, "<span class=\"ie_valign_5 no\">请选择区域/板块！</span>");



        $("#PropertyForm").validate({
            rules: {
                PropertyName: {
                    required: true,
                    maxlength: 25,
                    stringCheck: true

                },
                ReferencePrice: {
                    required: true,
                    number: true,
                    digits: true
                    //                    range: [1000, 1000000000]                 
                },
                TelePhone: {
                    required: true
                },
                SalesStatus: {
                    IsSalesStatus: true
                },
                PremisesAddress: {
                    required: true,
                    maxlength: 150
                },
                salesAddress: {
                    required: true,
                    maxlength: 150
                },
                Developer: {
                    required: true,
                    maxlength: 30
                },
                Agent: {
                    maxlength: 30
                },
                PropertyRight: {
                    required: true,
                    range: [10, 99],
                    digits: true
                },
                BuildingArea: {
                    required: true,
                    isZhengNumber: true,
                    max: 999999999
                },
                Area: {
                    required: true,
                    isZhengNumber: true,
                    max: 999999999
                },
                BuildingType: {
                    IsBuildingType: true
                },
                PropertyType: {
                    PropertyTypeChecked: true
                },
                UserCount: {
                    digits: true,
                    max: 999999999
                },
                AreaRatio: {
                    //number: true,
                    IsFloat2: true,
                    min: 0.000000000001
                },
                RoomRate: {
                    //number: true,
                    IsFloat2: true,
                    min: 0.000000000001
                },
                PropertyPrice: {
                    //number: true,
                    IsFloat2: true,
                    min: 0.000000000001
                },
                ParkingLot: {
                    digits: true,
                    min: 0
                },
                GreeningRate: {
                    //number: true,
                    IsFloat2: true,
                    min: 0.000000000001
                },
                PropertyCompany: {
                    maxlength: 30
                },
                TrafficIntroduction: {
                    maxlength: 500
                },
                SupportingIntroduction: {
                    maxlength: 500
                },
                PremisesIntroduction: {
                    maxlength: 600
                },
                DId: {
                    IsWhere: true
                },
                BId: {
                    IsBId: true
                },
                Ring: {
                    IsRing: true
                }
            },
            messages: {
                PropertyName: {
                    required: "<span class=\"ie_valign_5 no\">请填写楼盘名称！</span>",
                    maxlength: "<span class=\"ie_valign_5 no\">楼盘名称最多可输入25个汉字！</span>"
                },
                ReferencePrice: {
                    required: "<span class=\"ie_valign_5 no\">请填写参考均价！</span>",
                    number: "<span class=\"ie_valign_5 no\">价格只能输入数字！</span>"
                },
                TelePhone: {
                    required: "<span class=\"ie_valign_5 no\">请填写咨询电话！</span>",
                    telephone: "<span class=\"ie_valign_5 no\">请输入正确的电话！</span>"
                },
                PremisesAddress: {
                    required: "<span class=\"ie_valign_5 no\">请填写项目地址！</span>",
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入150个汉字</span>"
                },
                salesAddress: {
                    required: "<span class=\"ie_valign_5 no\">请填写售楼地址！</span>",
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入150个汉字</span>"
                },
                Developer: {
                    required: "<span class=\"ie_valign_5 no\">请填写开发商！</span>",
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入30个汉字</span>"
                },
                Agent: {
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入30个汉字</span>"
                },
                PropertyRight: {
                    required: "<span class=\"ie_valign_5 no\">请填写产权！</span>",
                    range: "<span class=\"ie_valign_5 no\">请输入正确的产权</span>",
                    digits: "<span class=\"ie_valign_5 no\">请输入正确的产权</span>"
                },
                BuildingArea: {
                    required: "<span class=\"ie_valign_5 no\">请填写建筑面积！</span>",
                    max: "<span class=\"ie_valign_5 no\">最多可输入9位正数！</span>"
                },
                Area: {
                    required: "<span class=\"ie_valign_5 no\">请填写占地面积！</span>",
                    max: "<span class=\"ie_valign_5 no\">最多可输入9位正数！</span>"
                },
                UserCount: {
                    digits: "<span class=\"ie_valign_5 no\">请输入正确的总户数！</span>",
                    max: "<span class=\"ie_valign_5 no\">最多可输入9位正数！</span>"
                },
                AreaRatio: {
                    //number: "<span class=\"ie_valign_5 no\">请输入正数！</span>",
                    min: "<span class=\"ie_valign_5 no\">请输入正数！</span>"
                },
                RoomRate: {
                    //number: "<span class=\"ie_valign_5 no\">请输入正数！</span>",
                    min: "<span class=\"ie_valign_5 no\">请输入正数！</span>"
                },
                PropertyPrice: {
                    //number: "<span class=\"ie_valign_5 no\">请输入正数！</span>",
                    min: "<span class=\"ie_valign_5 no\">请输入正数！</span>"
                },
                ParkingLot: {
                    digits: "<span class=\"ie_valign_5 no\">请输入整数！</span>",
                    min: "<span class=\"ie_valign_5 no\">请输入整数！</span>"
                },
                GreeningRate: {
                    //number: "<span class=\"ie_valign_5 no\">请输入正数！</span>",
                    min: "<span class=\"ie_valign_5 no\">请输入正数！</span>"
                },
                PropertyCompany: {
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入30个汉字！</span>"
                },
                TrafficIntroduction: {
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入500个汉字！</span>"
                },
                SupportingIntroduction: {
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入500个汉字！</span>"
                },
                PremisesIntroduction: {
                    maxlength: "<span class=\"ie_valign_5 no\">最多可输入600个汉字！</span>"
                }
            },
            onkeyup: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                element.parent().find("span").remove();
                error.appendTo(element.parent());
            },
            submitHandler: function (form) {
                $("#ProvinceName").val($("#ProvinceId").find("option:selected").text());
                $("#CityName").val($("#CityId").find("option:selected").text());
                $("#DistrictName").val($("#DId").find("option:selected").text());
                $("#BusinessName").val($("#BId").find("option:selected").text());

                $("#IsShow400").val($("#chk400").prop("checked") ? "1" : "0");

                $("#Ring").val($("#Ring").is(":visible") ? $("#Ring").val() : "0");
                var f = false;
                var b = false;
                //判断是否上传楼盘沙盘图
                var sand = $("#SandboxCoordinate").val();
                if (sand != "" && sand != "[]") {
                    f = true;
                    $("#SandError").hide();
                }
                else {

                    $("#SandError").show();
                }


                //判断是否上传logo
                var src = $("#PremisesLogo").attr("src");
                if (src == undefined) {
                    $("#PreLogo").show();
                    $("#PropertyName").focus();
                    $("#PropertyName").blur();
                }
                else {
                    $("#PreLogo").hide();
                    b = true;
                }

                if (f && b) {
                    form.submit();
                }



            }
        });

    </script>
    <script type="text/javascript">

        var traffic_subway = {
            // 原数据库地铁站点
            new_stations: [], // {"id":id, "name":name}

            //选中的地铁站点json
            selected_stations: [],
            //当前地铁线地铁站 json
            line_stations: [],

            // 选中地铁站点
            selectedStation: function (id, name) {
                if (!traffic_subway.isExist_InSelectedStation(id)) {
                    if (10 <= traffic_subway.selected_stations.length) {
                        alert("请注意，同一楼盘最多可以添加10个站名。");
                        return;
                    }
                    traffic_subway.selected_stations.push({ "id": id, "name": name });
                    $("#station_" + id).addClass("orange bold");

                    traffic_subway.displaySelectedStation();
                }
            },

            // 当前id是否已经被选中
            isExist_InSelectedStation: function (id) {
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
            // 初始化 已选择的地铁站
            initSelectedStation: function () {
                $("a[id^=station_]").each(function () {
                    for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                        if ($(this).attr("id") == ("station_" + traffic_subway.selected_stations[i].id)) {
                            $(this).addClass("orange bold");
                        }
                    }
                });
            },

            // 删除已选择的地铁站
            delSelectedStation: function (id) {
                for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                    if (id == traffic_subway.selected_stations[i].id) {
                        traffic_subway.selected_stations.splice(i, 1);
                        $("#station_" + id).removeClass("orange bold");
                        break;
                    }
                }

                traffic_subway.displaySelectedStation();
            },

            // 显示已选中的站点
            displaySelectedStation: function () {
                $("#ul_traffic_info").html("");
                if (0 == traffic_subway.selected_stations.length) {
                    return;
                }

                var html = "";
                for (var i = 0; i < traffic_subway.selected_stations.length; i++) {
                    html += "<div class=\"colsebox\" data=\"" + traffic_subway.selected_stations[i].id + traffic_subway.selected_stations[i].name + "\">" + traffic_subway.selected_stations[i].name + "<a class=\"del_li cha\" onclick=\"traffic_subway.delSelectedStation('" + traffic_subway.selected_stations[i].id + "')\"> </a></div>";
                }
                $("#ul_traffic_info").html(html);
            },
            // 将选中的地铁站点显示在主页面上(点击确定)
            btn_Enter: function () {
                traffic_subway.new_stations = traffic_subway.cloneData(traffic_subway.selected_stations);
                traffic_subway.displaySelectedStation_InMainPage();
            },
            // 克隆数据
            cloneData: function (json) {
                var newArray = new Array();
                for (var i = 0; i < json.length; i++) {
                    newArray.push({ "id": json[i].id, "name": json[i].name });
                }
                return newArray;
            },
            // 将选中的地铁站点显示在主页面上
            displaySelectedStation_InMainPage: function () {
                if (0 == traffic_subway.new_stations.length) {
                    $("#Traffic").val("");
                    $("#show_traffic").html("");
                    return;
                }

                var html = "<ul class=\"tbUl\">";
                for (var i = 0; i < traffic_subway.new_stations.length; i++) {
                    html += "<div class=\"colsebox\" data=\"" + traffic_subway.new_stations[i].id + "\">";
                    html += "<span>" + traffic_subway.new_stations[i].name + "</span>";
                    html += "<a href=\"#\" class=\"del_li cha\" onclick=\"traffic_subway.delSelectedStation_InMainPage('" + traffic_subway.new_stations[i].id + "')\"> </a>";
                    html += "</div>";
                }
                html += "</ul>";
                $("#show_traffic").html(html);
                $("#Traffic").val(traffic_subway.getSubwayIds());
            },
            // 从主页面上删除地铁站
            delSelectedStation_InMainPage: function (id) {
                traffic_subway.delSelectedStation(id);
                traffic_subway.new_stations = traffic_subway.cloneData(traffic_subway.selected_stations);
                traffic_subway.displaySelectedStation_InMainPage();
            },
            // 获取地铁站编号集合(逗号分隔)
            getSubwayIds: function () {
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
            initSandBoxData: function () {
                var json = eval($("#SandboxCoordinate").val());

                jsnewsandbox.setSandBoxData(json);
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
            }
        };
    </script>
</asp:Content>
