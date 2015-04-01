<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-编辑模版
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <!-- InstanceBeginEditable name="EditRegion1" -->
        <form id="HouseForm" method="post">
        <div class="mt5">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="16%">
                        选择楼盘：
                    </th>
                    <td width="84%">
                        <%=Html.DropDownList("PremisesId", (IEnumerable<SelectListItem>)ViewData["PremisList"], new { @class = "selcss w200" })%>
                        <select class="selcss w100" id="BuildingId" name="BuildingId">
                            <option value="-1">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th width="16%">
                        选用已保存的房源模板：
                    </th>
                    <td width="84%">
                        <select class="selcss" id="HouseTemplate" name="HouseTemplate" style="width: 200px;">
                            <option value="-1">请选择房源模版</option>
                        </select><input id="SelectHouseTemplate" type="button" value="选用" class="btn_w80 ml15" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="btit clearFix mt15">
            <div class="tit_font fl">
                房源信息</div>
            <div class="font12 fr mr35">
                <span class="red">*</span>为必填项</div>
        </div>
        <div class="mt15 mb15">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="tab2">
                <tr>
                    <th width="16%">
                        <span class="mr2 red">*</span>房号：
                    </th>
                    <td width="84%">
                        <input type="text" class="txt22 w100" id="HouseName" name="HouseName" value="<%=Model.Name %>" maxlength="5" />
                        <span class="col999 ml20">（例如：1201;A302）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>单元：
                    </th>
                    <td>
                        <select class="selcss w200" id="UnitId" name="UnitId">
                            <option value="-1">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        <span class="mr2 red">*</span>所在楼层：
                    </th>
                    <td>
                        <div>
                            <input type="radio" checked="checked" id="TheFloor" name="TFloor" class="bnt_radio mr5" />
                            地上楼层
                            <input type="radio" id="Underloor" name="TFloor" class="bnt_radio mr5 ml20" />
                            地下楼层
                        </div>
                        <div class="mt15">
                            <select class="selcss w200" id="Floor" name="Floor">
                                <option value="0">请选择</option>
                            </select>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>户型：
                    </th>
                    <td>
                        <input type="text" class="txt22 w60 mr5" id="Room" name="Room" value="<%=Model.Room %>" />
                        室
                        <input type="text" class="txt22 w60 ml10 mr5" id="Hall" name="Hall" value="<%=Model.Hall %>" />
                        厅
                        <input type="text" class="txt22 w60 ml10 mr5" id="Toilet" name="Toilet" value="<%=Model.Toilet %>" />
                        卫
                        <input type="text" class="txt22 w60 ml10 mr5" id="Kitchen" name="Kitchen" value="<%=Model.Kitchen %>" />
                        厨<span class="ml15 col999">请填写数字（例如：3室2厅2卫1厨）</span> <span id="Apartment"></span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>建筑形式：
                    </th>
                    <td>
                        <select class="selcss w200" id="BuildingType" name="BuildingType">
                            <option value="-1">请选择</option>
                            <option value="1">开间</option>
                            <option value="2">错层</option>
                            <option value="3">跃层</option>
                            <option value="4">复式</option>
                            <option value="5">平层</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>采光情况：
                    </th>
                    <td>
                        <select class="selcss w200" id="Orientation" name="Orientation">
                            <option value="-1">无</option>
                            <option value="1">东</option>
                            <option value="2">南</option>
                            <option value="3">西</option>
                            <option value="4">北</option>
                            <option value="5">东南</option>
                            <option value="6">东北</option>
                            <option value="7">西南</option>
                            <option value="8">西北</option>
                            <option value="9">南北</option>
                            <option value="10">东西</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                    </th>
                    <td>
                        <input id="MarketPrice" type="radio" name="PriceType" class="bnt_radio mr5" value="0" />
                        市场价格
                        <input id="ReferencePrice" type="radio" name="PriceType" class="bnt_radio mr5 ml20"
                            value="1" />
                        参考价格
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>总价：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100 mr5" id="TotalPrice" name="TotalPrice" value="<%=Model.TotalPrice.ToString("#.##") %>" maxlength="9" />
                        万元/套 <span class="ml15 col999">请填写数字（例如：15000）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>单价：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100 mr5" id="SinglePrice" name="SinglePrice" value="<%=Model.SinglePrice.ToString("#.##")%>" maxlength="9" />
                        元/平方米<span class="ml15 col999">请填写数字（例如：15000）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>首期付款：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100 mr5" id="DownPayment" name="DownPayment" value="<%=Model.DownPayment.ToString("#.##")%>" maxlength="9" />
                        万元<span class="ml15 col999">请填写数字（例如：15000）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>销售状态：
                    </th>
                    <td>
                        <select class="selcss w200" id="SalesStatus" name="SalesStatus">
                            <option value="-1">无</option>
                            <option value="1">开发商保留</option>
                            <option value="2">可售</option>
                            <option value="3">已认购</option>
                            <option value="4">已签约</option>
                            <option value="5">已备案</option>
                            <option value="6">已办产权</option>
                            <option value="7">被限制</option>
                            <option value="8">拆迁安置</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>建筑面积：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="BuildingArea" name="BuildingArea" value="<%=Model.BuildingArea %>" />
                        平方米
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>使用面积：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="Area" name="Area" value="<%=Model.Area %>" />
                        平方米
                    </td>
                </tr>
                <tr>
                    <th valign="top">
                        <span class="mr2 red">*</span>户型图：
                    </th>
                    <td>
                        <select class="selcss w200" id="RId" name="RId">
                            <option value="-1">选择户型图</option>
                        </select>
                        <input id="UpdatePic" type="button" class="btn_w80 ml10" value="更新户型图" /><a id="UploadHousePic"
                            href="javascript:void(0);" class="ml10">上传户型图</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>房间位置标注：
                    </th>
                    <td>
                        <input id="RoomsMarked" type="button" value="点击标注" class="btn_w80 mr15" />
                    </td>
                </tr>
            </table>
            <div class="boxcent20 mt15">
                <input type="submit" class="bnt_w120" value="保存模版" />
            </div>
            </div>
            <input type="hidden" id="PremisesName" name="PremisesName" />
            <input type="hidden" id="BuildingName" name="BuildingName" />
            <input type="hidden" id="UnitName" name="UnitName" />
            <input type="hidden" id="IsRelease" value="true" name="IsRelease" />
            <input type="hidden" id="PicSrc" />
            <input type="hidden" id="CoordX" />
            <input type="hidden" id="CoordY" />
            <input type="hidden" id="HouseCoordinate" name="HouseCoordinate" />
        </form>
        <!-- InstanceEndEditable -->
    </div>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.min.js")%>" language="javascript" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/dialog.js")%>" language="javascript" type="text/javascript"></script>
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

            $("#RoomsMarked").bind("click", function () {
                show_dialog(mainUrl + "NHouse/RoomMarked?ram=" + Math.random());
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

        var msg = '<%= ViewData["msg"] %>';
        if (msg == "0") {
            alert("修改模版失败");
        }
        if (msg == "1") {
            alert("修改模版成功");
        }


        $(function () {
            //页面初始化信息
            $("#PremisesId").val('<%=Model.PremisesId %>');
            GetBuilding('<%=Model.PremisesId %>');
            GetUnit('<%=Model.PremisesId %>', '<%=Model.BuildingId %>');
            GetPremisesPic('<%=Model.PremisesId %>'); //获取户型图列表

            $("#BuildingType").val('<%=Model.BuildingType %>');
            $("#Orientation").val('<%=Model.Orientation %>');
            $("#SalesStatus").val('<%=Model.SalesStatus %>');

            if ('<%=Model.PriceType %>' == 0)
                $("#MarketPrice").attr("checked", "checked");
            else
                $("#ReferencePrice").attr("checked", "checked");
          
            var floor = '<%=Model.Floor %>';
            if (floor > 0) {
                $("#TheFloor").attr("checked", "checked");
                $("#Floor").empty();
                $("<option></option>").val("0").text("请选择").appendTo($("#Floor"));
                $.ajax({
                    type: "POST",
                    url: mainUrl + "/NHouse/TheFloor",
                    data: "BuildingId=" + '<%=Model.BuildingId %>',
                    success: function (data) {
                        for (var i = 1; i <= data; i++) {
                            var smtp = '<%=Model.Floor %>' == i ? "selected" : "";
                            $("<option " + smtp + "></option>").val(i).text(i + "F").appendTo($("#Floor"));
                        }
                    }
                });
            }
            else {
                $("#Underloor").attr("checked", "checked");
                $("#Floor").empty();
                $("<option></option>").val("0").text("请选择").appendTo($("#Floor"));
                $.ajax({
                    type: "POST",
                    url: mainUrl + "/NHouse/Underloor",
                    data: "BuildingId=" + '<%=Model.BuildingId %>',
                    success: function (data) {
                        for (var i = 1; i <= data; i++) {
                            var smtp = Math.abs('<%=Model.Floor %>') == i ? "selected" : "";
                            $("<option " + smtp + "></option>").val("-" + i).text("B" + i).appendTo($("#Floor"));
                        }

                    }
                });


            }
        });

        //地上楼层
        function GetTheFloor() {
            $("#Floor").empty();
            $("<option></option>").val("0").text("请选择").appendTo($("#Floor"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/TheFloor",
                data: "BuildingId=" + $("#BuildingId").val(),
                success: function (data) {
                    for (var i = 1; i <= data; i++) {
                        $("<option></option>").val(i).text("F" + i).appendTo($("#Floor"));
                    }

                }
            });
        }

        //获取地下楼层
        function GetUnderloor() {
            $("#Floor").empty();
            $("<option></option>").val("0").text("请选择").appendTo($("#Floor"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/Underloor",
                data: "BuildingId=" + $("#BuildingId").val(),
                success: function (data) {
                    for (var i = 1; i <= data; i++) {
                        $("<option></option>").val("-" + i).text("B" + i).appendTo($("#Floor"));
                    }

                }
            });
        }

        //获取模版
        function GetHouseTemplate() {
            $("#HouseTemplate").empty();
            $("<option></option>").val("-1").text("请选择房源模版").appendTo($("#HouseTemplate"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/GetHouseTemplate",
                success: function (data) {
                    $.each(data, function (i, item) {
                        $("<option></option>").val(item.Id).text(item.Title).appendTo($("#HouseTemplate"));
                    });
                }
            });
        }

        function GetBuilding(PremisesId) {
            $("#BuildingId").empty();
            $("<option></option>").val("-1").text("请选择").appendTo($("#BuildingId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NhBuilding/BuildingList",
                data: "PremisesId=" + PremisesId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var buildid = '<%=Model.BuildingId %>';
                        var stmp = buildid == item.Id ? "selected=\"selected\"" : "";
                        $("<option " + stmp + "></option>").val(item.Id).text(item.Name + item.NameType).appendTo($("#BuildingId"));
                    });
                }
            });
        }

        function GetUnit(PremisesId, BuildingId) {
            $("#UnitId").empty();
            $("<option></option>").val("-1").text("请选择").appendTo($("#UnitId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/GetUnitList",
                data: "PremisesId=" + PremisesId + "&BuildingId=" + BuildingId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var UnitId = '<%=Model.UnitId %>'
                        var stmp = UnitId == item.Id ? "selected=\"selected\"" : "";
                        $("<option " + stmp + "></option>").val(item.Num).text(item.Name).appendTo($("#UnitId"));
                    });
                      $("#UnitId").val(<%=Model.UnitId %>);
                }
            });

        }

        function GetUntitList(PremisesId, BuildingId, UnitId) {
            $("#UnitId").empty();
            $("<option></option>").val("-1").text("请选择").appendTo($("#UnitId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/GetUnitList",
                data: "PremisesId=" + PremisesId + "&BuildingId=" + BuildingId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var smtp = UnitId == item.Id ? "selected" : "";
                        $("<option " + smtp + "></option>").val(item.Id).text(item.Name).appendTo($("#UnitId"));
                    });
                }
            });
        }

        //获取户型图列表
        function GetPremisesPic(PremisesId) {
            $("#RId").empty();
            $("<option></option>").val("-1").text("请选择户型图").appendTo($("#RId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "NHouse/GetHousePic",
                data: "PremisesId=" + PremisesId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var temp = '<%=Model.RId %>' == item.ID ? "selected" : "";
                        $("<option  " + temp + "></option>").val(item.ID).text(item.Title).appendTo($("#RId"));
                    });
                }
            });
        }


        $(document).ready(function () {
            $("#SalesStatus").val('<%=Model.SalesStatus%>');
            GetHouseTemplate();

            //上传户型图
            $("#UploadHousePic").bind("click", function () {
                var id = $("#PremisesId").val();
                if (id == -1) {
                    alert("请先选择楼盘");
                    return false;
                }
                window.open(mainUrl + "/Premises/PremisesImage/" + id);
            });

            $("#UpdatePic").bind("click", function () {
                var id = $("#PremisesId").val();
                if (id == -1) {
                    alert("请先选择楼盘");
                    return false;
                }
                GetPremisesPic(id);
            });


            $("#SelectHouseTemplate").bind("click", function () {
                var Id = $("#HouseTemplate").val();
                if (Id == -1) {
                    alert("请选择一个房源模板");
                }

                $.ajax({
                    type: "POST",
                    url: mainUrl + "/NHouse/SelectHouseTemplate",
                    data: "Id=" + Id,
                    success: function (Data) {
                        var json = eval('(' + Data.Content + ')');
                        $("#PremisesId").val(json.PremisesId);
                        GetBuilding($("#PremisesId").val(), json.BuildingId);
                        GetUntitList(json.PremisesId, json.BuildingId, json.UnitId);

                        $("#HouseName").val(json.Name);

                        $("#Room").val(json.Room);
                        $("#Hall").val(json.Hall);
                        $("#Toilet").val(json.Toilet);
                        $("#Kitchen").val(json.Kitchen);
                        $("#TotalPrice").val(json.TotalPrice);
                        $("#SinglePrice").val(json.SinglePrice);
                        $("#DownPayment").val(json.DownPayment);
                        $("#BuildingArea").val(json.BuildingArea);
                        $("#Area").val(json.Area);


                        if (json.Floor > 0) {
                            $("#TheFloor").attr("checked", "checked");
                            $("#Floor").empty();
                            $("<option></option>").val("0").text("请选择").appendTo($("#Floor"));
                            $.ajax({
                                type: "POST",
                                url: mainUrl + "/NHouse/TheFloor",
                                data: "BuildingId=" + json.BuildingId,
                                success: function (data) {
                                    for (var i = 1; i <= data; i++) {
                                        var smtp = json.Floor == i ? "selected" : "";
                                        $("<option " + smtp + "></option>").val(i).text(i + "F").appendTo($("#Floor"));
                                    }
                                }
                            });
                        }
                        else {
                            $("#Underloor").attr("checked", "checked");
                            $("#Floor").empty();
                            $("<option></option>").val("0").text("请选择").appendTo($("#Floor"));
                            $.ajax({
                                type: "POST",
                                url: mainUrl + "/NHouse/Underloor",
                                data: "BuildingId=" + json.BuildingId,
                                success: function (data) {
                                    for (var i = 1; i <= data; i++) {
                                        var smtp = Math.abs(json.Floor) == i ? "selected" : "";
                                        $("<option " + smtp + "></option>").val("-" + i).text("B" + i).appendTo($("#Floor"));
                                    }

                                }
                            });


                        }
                        $("#UnitId").val(json.UnitId);
                        $("#BuildingType").val(json.BuildingType);
                        $("#SalesStatus").val(json.SalesStatus);
                        $("#Orientation").val(json.Orientation);
                        if (json.PriceType == 0) {
                            $("#MarketPrice").prop("checked", true);
                            $("#ReferencePrice").prop("checked", false);
                        }
                        else {
                            $("#MarketPrice").prop("checked", false);
                            $("#ReferencePrice").prop("checked", true);
                        }
                    }
                });
            });




            $("#PremisesId").change(function () {
                GetBuilding($("#PremisesId").val());
                GetPremisesPic($("#PremisesId").val());
            });

            //单元列表
            $("#BuildingId").change(function () {
                GetUnit($("#PremisesId").val(), $("#BuildingId").val());
                GetTheFloor();
            });

            $("#TheFloor").bind("click", function () {
                GetTheFloor();
            });

            $("#Underloor").bind("click", function () {
                GetUnderloor();
            });




            jQuery.validator.addMethod("IsBuildingId", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼盘楼栋！</span>");

            jQuery.validator.addMethod("IsUnitId", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择单元！</span>");

            jQuery.validator.addMethod("IsFloor", function (value, element) {
                return this.optional(element) || value == 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择楼层！</span>");


            jQuery.validator.addMethod("IsApartment", function (value, element) {
                var Count = 1;
                if ($("#Room").val() == "") {
                    Count = 0;
                }
                if ($("#Hall").val() == "") {
                    Count = 0;
                }
                if ($("#Toilet").val() == "") {
                    Count = 0;
                }
                if ($("#Kitchen").val() == "") {
                    Count = 0;
                }
                return this.optional(element) || Count <= 0 ? false : true;

            }, "<span class=\"ie_valign_5 no\">请填写户型！</span>");

            jQuery.validator.addMethod("IsBuildingType", function (value, element) {
                return this.optional(element) || value == -1 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择建筑形式！</span>");

            jQuery.validator.addMethod("IsOrientation", function (value, element) {
                return this.optional(element) || value == -1 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择采光情况！</span>");



            $("#HouseForm").validate({
                rules: {
                    BuildingId: {
                        IsBuildingId: true
                    },
                    HouseName: {
                        required: true
                    },
                    UnitId: {
                        IsUnitId: true
                    },
                    Floor: {
                        IsFloor: true
                    },
                    Kitchen: {
                        IsApartment: true
                    },
                    BuildingType: {
                        IsBuildingType: true
                    },
                    Orientation: {
                        IsOrientation: true
                    },
                    TotalPrice: {
                        required: true,
                        range: [0, 999999999]
                    },
                    SinglePrice: {
                        required: true,
                        range: [0, 999999999]
                    },
                    DownPayment: {
                        required: true,
                        range: [0, 999999999]
                    },
                    BuildingArea: {
                        required: true,
                        digits: true
                    },
                    Area: {
                        required: true,
                        digits: true
                    }
                },
                messages: {
                    HouseName: {
                        required: "<span class=\"ie_valign_5 no\">请填写房号！</span>"
                    },
                    TotalPrice: {
                        required: "<span class=\"ie_valign_5 no\">请填写总价！</span>",
                        range: "<span class=\"ie_valign_5 no\">请输入正确9位正数！</span>"
                    },
                    SinglePrice: {
                        required: "<span class=\"ie_valign_5 no\">请填写单价！</span>",
                        range: "<span class=\"ie_valign_5 no\">请输入正确9位正数！</span>"
                    },
                    DownPayment: {
                        required: "<span class=\"ie_valign_5 no\">请填写首期付款！</span>",
                        range: "<span class=\"ie_valign_5 no\">请输入正确9位正数！</span>"
                    },
                    BuildingArea: {
                        required: "<span class=\"ie_valign_5 no\">请填写建筑面积！</span>",
                        digits: "<span class=\"ie_valign_5 no\">格式有误！</span>"
                    },
                    Area: {
                        required: "<span class=\"ie_valign_5 no\">请填写使用面积！</span>",
                        digits: "<span class=\"ie_valign_5 no\">格式有误！</span>"
                    }

                },
                onkeyup: false,
                errorElement: "span",
                errorPlacement: function (error, element) {
                    element.parent().find("span").remove();
                    error.appendTo(element.parent());
                },
                submitHandler: function (form) {
                    $("#PremisesName").val($("#PremisesId").find("option:selected").text());
                    $("#BuildingName").val($("#BuildingId").find("option:selected").text());
                    $("#UnitName").val($("#UnitId").find("option:selected").text());
                    form.submit();
                }
            });
        });
    </script>
</asp:Content>
