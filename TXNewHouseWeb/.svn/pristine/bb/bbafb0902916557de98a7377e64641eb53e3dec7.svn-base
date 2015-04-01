<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新房后台-发布房源
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
                        <select class="selcss" id="HouseTemplate" name="HouseTemplate">
                            <option value="-1">请选择房源模版</option>
                        </select><input id="SelectHouseTemplate" type="button" value="选用" class="btn_w80 ml15">
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
                        <input type="text" class="txt22 w100" id="HouseName" name="HouseName" maxlength="5" />
                        <span class="col999 ml20">（例如：1201;A302）</span> <span id="err" class="ie_valign_5 no"
                            style="display: none;">房源名称已存在！</span>
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
                            地下楼层</div>
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
                        <input type="text" class="txt22 w60 mr5" id="Room" name="Room" maxlength="1" />
                        室
                        <input type="text" class="txt22 w60 ml10 mr5" id="Hall" name="Hall" maxlength="1" />
                        厅
                        <input type="text" class="txt22 w60 ml10 mr5" id="Toilet" name="Toilet" maxlength="1" />
                        卫
                        <input type="text" class="txt22 w60 ml10 mr5" id="Kitchen" name="Kitchen" maxlength="1" />
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
                        <input type="radio" id="MarketPrice" name="PriceType" class="bnt_radio mr5" value="0" />
                        市场价格
                        <input type="radio" id="ReferencePrice" name="PriceType" class="bnt_radio mr5 ml20"
                            value="1" />
                        参考价格
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>总价：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100 mr5" id="TotalPrice" name="TotalPrice" maxlength="9" />
                        万元/套 <span class="ml15 col999">请填写数字（例如：15000）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>单价：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100 mr5" id="SinglePrice" name="SinglePrice" maxlength="9" />
                        元/平方米<span class="ml15 col999">请填写数字（例如：15000）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>首期付款：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100 mr5" id="DownPayment" name="DownPayment" maxlength="9" />
                        万元<span class="ml15 col999">请填写数字（例如：15000）</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="mr2 red">*</span>销售状态：
                    </th>
                    <td>
                        <select class="selcss w200" id="SalesStatus" name="SalesStatus">
                            <%--   <option value="-1">请选择</option>
                            <option value="0">待售</option>
                            <option value="1">开发商保留</option>
                            <option value="2">在售</option>
                            <option value="3">已认购</option>
                            <option value="4">已签约</option>
                            <option value="5">已备案</option>
                            <option value="6">已办产权</option>
                            <option value="7">被限制</option>
                            <option value="8">拆迁安置</option>
                            <option value="9">售罄</option>--%>
                        </select>
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
                        <span class="mr2 red">*</span>使用面积：
                    </th>
                    <td>
                        <input type="text" class="txt22 w100" id="Area" name="Area" maxlength="9" />
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
                        <%--   <input id="RoomsMarked" name="RoomsMarked" type="button" value="点击标注" class="btn_w80 mr15"
                            onclick="Dialog.showDialog('url', '<%=TXCommons.GetConfig.BaseUrl%>NHouse/RoomMarked', '<%=TXCommons.GetConfig.ImgUrl%>images/loading.gif')" />--%>
                        <input id="RoomsMarked" name="RoomsMarked" type="button" value="点击标注" class="btn_w80 mr15" />
                    </td>
                </tr>
            </table>
            <div class="boxcent20 mt15">
                <input type="submit" class="bnt_w120" value="发布房源" /><input id="SaveHouse" type="button"
                    class="bnt_green_w120 ml15" value="保存房源" />
                <input id="SaveHouseTemplate" type="checkbox" name="SaveHouseTemplate" class="bnt_checkbox" /><span
                    class="col333 font12">保存为房源模板</span>
            </div>
        </div>
        <input type="hidden" id="TitleTemplate" name="TitleTemplate" />
        <input type="hidden" id="RemarkTemplate" name="RemarkTemplate" />
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
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jquery.validate.min.js")%>"
        language="javascript" type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/dialog_new.js")%>" language="javascript"
        type="text/javascript"></script>
    <script src="<%=TXCommons.GetConfig.GetFileUrl("js/commons/jqRegExpMothod.js")%>"
        type="text/javascript"></script>
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
                if ($("#BuildingId").val() == -1) {
                    alert("请先选择楼栋");
                    return false;
                }
                else {
                    show_dialog(mainUrl + "NHouse/RoomMarked?ram=" + Math.random());
                }

            });


            $("#SaveHouseTemplate").change(function () {
                if ($("#SaveHouseTemplate").prop("checked") == true) {
                    if ($("#HouseForm").valid()) {
                        show_dialog(mainUrl + "NHouse/AddTemplate?ram=" + Math.random());
                    }
                    else {
                        $(this).prop("checked", false);
                    }
                }
                else {
                    dialog.close();
                }
            });


        });

    </script>
    <script language="javascript" type="text/javascript">
        var mainUrl = "<%=TXCommons.GetConfig.BaseUrl%>";

        //保存房源
        $("#SaveHouse").bind("click", function () {
            if ($("#HouseForm").valid()) {
                $("#IsRelease").val("");
                $("#PremisesName").val($("#PremisesId").find("option:selected").text());
                $("#BuildingName").val($("#BuildingId").find("option:selected").text());
                $("#UnitName").val($("#UnitId").find("option:selected").text());
                $("#HouseForm").submit();
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
                        $("<option></option>").val(i).text(i + "F").appendTo($("#Floor"));
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
                        $("<option></option>").val(item.ID).text(item.Title).appendTo($("#RId"));
                    });
                }
            });
        }

        //获取房间位置标注图片
        function GetBuildPic(PremisesId, BuildingId) {
            $.ajax({
                type: "POST",
                url: mainUrl + "NHouse/SelectBuildingPic",
                data: "PremisesId=" + PremisesId + "&BuildingId=" + BuildingId,
                success: function (msg) {
                    $("#PicSrc").val(msg);
                }
            });
        }

        //获取楼栋列表
        function GetBuilding(PremisesId, BuildingId) {

            $("#BuildingId").empty();
            $("<option></option>").val("-1").text("请选择").appendTo($("#BuildingId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NhBuilding/BuildingList",
                data: "PremisesId=" + PremisesId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var smtp = BuildingId == item.Id ? "selected" : "";
                        $("<option " + smtp + "></option>").val(item.Id).text(item.Name).appendTo($("#BuildingId"));
                    });
                    GetTheFloor();
                }
            });
        }

        //获取单元列表
        function GetUntitList(PremisesId, BuildingId, UnitId) {
            $("#UnitId").empty();
            $("<option></option>").val("-1").text("请选择").appendTo($("#UnitId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "/NHouse/GetUnitList",
                data: "PremisesId=" + PremisesId + "&BuildingId=" + BuildingId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var smtp = UnitId == item.Num ? "selected" : "";
                        $("<option " + smtp + "></option>").val(item.Num).text(item.Name).appendTo($("#UnitId"));
                    });
                }
            });
        }

        //获取户型图列表
        function GetPremisesCheck(PremisesId, RId) {
            $("#RId").empty();
            $("<option></option>").val("-1").text("请选择户型图").appendTo($("#RId"));
            $.ajax({
                type: "POST",
                url: mainUrl + "NHouse/GetHousePic",
                data: "PremisesId=" + PremisesId,
                success: function (msg) {
                    $.each(msg, function (i, item) {
                        var smtp = RId == item.ID ? "selected" : "";
                        $("<option " + smtp + "></option>").val(item.ID).text(item.Title).appendTo($("#RId"));
                    });
                }
            });
        }

        //获取URl参数
        function getUrlParam(name) {

            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象

            var r = window.location.search.substr(1).match(reg);  //匹配目标参数

            if (r != null) return unescape(r[2]); return null; //返回参数值
        }


        var jsHouse = {
            //房源状态
            HouseSaleState: [{ id: -1, value: "请选择" }, { id: 0, value: "待售" }, { id: 1, value: "开发商保留" }, { id: 2, value: "在售" }, { id: 3, value: "已认购" }, { id: 4, value: "已签约" }, { id: 5, value: "已备案" }, { id: 6, value: "已办产权" }, { id: 7, value: "被限制" }, { id: 8, value: "拆迁安置" }, { id: 9, value: "售罄"}],

            //获取楼栋状态
            GetBuildingState: function (BuildingId) {
                var i = -1;
                $.ajax({
                    type: "POST",
                    async: false,
                    url: mainUrl + "/NHouse/GetState",
                    data: "BuildingId=" + BuildingId,
                    success: function (data) {
                        i = data;
                    }
                });
                return i;
            },

            ShowStateList: function (BuildingState) {

                $("#SalesStatus").empty();
                if (BuildingState == 4 || BuildingState == 5) {
                    alert("规划中、建设中状态中的楼栋不可发布房源。");
                    GetBuilding('<%=ViewData["pid"] %>',-1);
                    return false;
                }
                else {
                    if ('<%=ViewData["bid"] %>' != -1) {
                        GetBuilding('<%=ViewData["pid"] %>', '<%=ViewData["bid"] %>');
                    }
                }
                $.each(jsHouse.HouseSaleState, function (i, item) {
                    if (BuildingState == 1) {
                        //如果楼栋状态为待售 则房源也只能发待售
                        if (item.id == 0 || item.id == -1) {
                            $("<option value=" + item.id + "></option>").text(item.value).appendTo($("#SalesStatus"));
                        } else {
                            $("<option value=" + item.id + "  disabled=\"disabled\"></option>").text(item.value).appendTo($("#SalesStatus"));
                        }
                    }
                    if (BuildingState == 2) {
                        //如果楼栋状态为在售 则房源可以发布任意状态
                        $("<option value=" + item.id + "></option>").text(item.value).appendTo($("#SalesStatus"));

                    }
                    if (BuildingState == 3) {
                        //如果楼栋状态为售罄 则房源可以发布售罄房源
                        if (item.id == 9 || item.id == -1) {
                            $("<option value=" + item.id + "></option>").text(item.value).appendTo($("#SalesStatus"));
                        } else {
                            $("<option value=" + item.id + "  disabled=\"disabled\"></option>").text(item.value).appendTo($("#SalesStatus"));
                        }
                    }

                });
            }


        };


        //页面初始化
        $(document).ready(function () {

            var bid = getUrlParam('bid');
            if (bid != null) {
                var BuildingState = jsHouse.GetBuildingState(bid);
                jsHouse.ShowStateList(BuildingState);
            }


            $("#PremisesId").val('<%=ViewData["pid"] %>'); //默认加载


            GetUntitList('<%=ViewData["pid"] %>', '<%=ViewData["bid"] %>', -1);
            GetPremisesPic('<%=ViewData["pid"] %>');

            if ('<%=ViewData["pid"] %>' != -1 && '<%=ViewData["bid"] %>' != -1) {

                GetBuildPic('<%=ViewData["pid"] %>', '<%=ViewData["bid"] %>');
            }


            //未认证的开发商不能发布新房数据及营销活动
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
                        //$("#PremisesId").val(json.PremisesId);
                        //GetBuilding($("#PremisesId").val(), json.BuildingId);
                        //GetUntitList(json.PremisesId, json.BuildingId, json.UnitId);
                        //GetBuildPic(json.PremisesId, json);
                        //GetPremisesCheck(json.PremisesId, json.RId);

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
                            $("#MarketPrice").attr("checked", true);
                            $("#ReferencePrice").attr("checked", false);
                        }
                        else {
                            $("#MarketPrice").attr("checked", false);
                            $("#ReferencePrice").attr("checked", true);
                        }


                    }
                });
            })

            //楼盘选择
            $("#PremisesId").change(function () {
                GetBuilding($("#PremisesId").val(), -1);
                GetPremisesPic($("#PremisesId").val());
            });


            //楼栋选择
            $("#BuildingId").change(function () {
                //获取楼栋平面图
                GetBuildPic($("#PremisesId").val(), $("#BuildingId").val());

                //房源状态显示
                var BuildingState = jsHouse.GetBuildingState($("#BuildingId").val());
                jsHouse.ShowStateList(BuildingState);
            });

            //单元列表
            $("#BuildingId").change(function () {
                //楼栋 状态 规划中、建设中状态中的楼栋不可发布房源。
                $.ajax({
                    type: "POST",
                    url: mainUrl + "/NHouse/GetState",
                    data: "BuildingId=" + $("#BuildingId").val(),
                    success: function (data) {
                        if (data == 4 || data == 5) {
                            alert("规划中、建设中状态中的楼栋不可发布房源。");
                            $("#BuildingId").val(-1);
                        }
                    }
                });

                GetUntitList($("#PremisesId").val(), $("#BuildingId").val());
                $("input[name=TFloor]:checked").click();
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

            jQuery.validator.addMethod("IsRId", function (value, element) {
                return this.optional(element) || value <= 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择户型图！</span>");

            jQuery.validator.addMethod("IsSalesStatus", function (value, element) {
                return this.optional(element) || value < 0 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择销售状态！</span>");

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

            jQuery.validator.addMethod("IsRoom", function (value, element) {
                var r = /^\+?[1-9][0-9]*$/; //正整数 
                var f = r.test($("#Room").val());
                return this.optional(element) || f;
            }, "<span class=\"ie_valign_5 no\">请输入正确的正数！</span>");

            jQuery.validator.addMethod("IsHall", function (value, element) {
                var r = /^\+?[0-9][0-9]*$/; //正整数 
                var f = r.test($("#Hall").val());
                return this.optional(element) || f;
            }, "<span class=\"ie_valign_5 no\">请输入正确的数！</span>");

            jQuery.validator.addMethod("IsToilet", function (value, element) {
                var r = /^\+?[0-9][0-9]*$/; //正整数 
                var f = r.test($("#Toilet").val());
                return this.optional(element) || f;
            }, "<span class=\"ie_valign_5 no\">请输入正确的数！</span>");

            jQuery.validator.addMethod("IsKitchen", function (value, element) {
                var r = /^\+?[0-9][0-9]*$/; //正整数 
                var f = r.test($("#Kitchen").val());
                return this.optional(element) || f;
            }, "<span class=\"ie_valign_5 no\">请输入正确的数！</span>");


            jQuery.validator.addMethod("IsCompareArea", function (value, element) {
                if ($("#Area").val() - $("#BuildingArea").val() >= 0) {
                    value = -1;
                }
                return this.optional(element) || value == -1 ? false : true;
            }, "<span class=\"ie_valign_5 no\">使用面积应该小于建筑面积！</span>");


            jQuery.validator.addMethod("IsBuildingType", function (value, element) {
                return this.optional(element) || value == -1 ? false : true;
            }, "<span class=\"ie_valign_5 no\">请选择建筑形式！</span>");

            jQuery.validator.addMethod("IsRoomsMarked", function (value, element) {
                if ($("#CoordX").val() == "" && $("#CoordY").val() == "") {
                    value = -1;
                }
                return this.optional(element) || value == -1 ? false : true;

            }, "<span class=\"ie_valign_5 no\">请标注房间位置！</span>");

            jQuery.validator.addMethod('IsPriceType', function (value, element) {
                var checkedCount = 0;
                $("input[name='PriceType']:radio:checked").each(function () {
                    checkedCount = 1;
                });
                value = checkedCount;
                return this.optional(element) || value <= 0 ? false : true;

            }, "<span class=\"ie_valign_5 no\">请选择价格类型！</span>");


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
                        IsApartment: true,
                        IsRoom: true,
                        IsHall: true,
                        IsToilet: true,
                        IsKitchen: true
                    },
                    BuildingType: {
                        IsBuildingType: true
                    },
                    /*Orientation: {
                    IsOrientation: true
                    },*/
                    PriceType: {
                        IsPriceType: true
                    },
                    TotalPrice: {
                        required: true,
                        digits: true,
                        range: [0, 999999999]
                    },
                    SinglePrice: {
                        required: true,
                        digits: true,
                        range: [0, 999999999]
                    },
                    DownPayment: {
                        required: true,
                        digits: true,
                        range: [0, 999999999]
                    },
                    SalesStatus: {
                        IsSalesStatus: true
                    },
                    BuildingArea: {
                        required: true,
                        digits: true,
                        range: [1, 999999999]
                    },
                    Area: {
                        required: true,
                        digits: true,
                        range: [1, 999999999],
                        IsCompareArea: true
                    },
                    RId: {
                        IsRId: true
                    },
                    RoomsMarked: {
                        IsRoomsMarked: true
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
                        range: "<span class=\"ie_valign_5 no\">请输入正确的9位正数！</span>"
                    },
                    Area: {
                        required: "<span class=\"ie_valign_5 no\">请填写使用面积！</span>",
                        range: "<span class=\"ie_valign_5 no\">请输入正确的9位正数！</span>"
                    }
                },
                onkeyup: false,
                errorElement: "span",
                errorPlacement: function (error, element) {
                    element.parent().find("span").remove();
                    error.appendTo(element.parent());
                },
                submitHandler: function (form) {
                    $.ajax({
                        //异步验证必须post提交并且为json格式
                        url: mainUrl + "nhouse/checkhousename",   //后台处理程序    
                        data: { houseName: $("#HouseName").val(), buildingId: $("#BuildingId").val(), unitId: $("#UnitId").val() },
                        type: "post",                               //数据发送方式     
                        dataType: "json",                           //接受数据格式   
                        success: function (data) {
                            if (data) {
                                $("#PremisesName").val($("#PremisesId").find("option:selected").text());
                                $("#BuildingName").val($("#BuildingId").find("option:selected").text());
                                $("#UnitName").val($("#UnitId").find("option:selected").text());
                                form.submit();
                            } else {
                                $("#err").show();
                                $("#HouseName").focus();
                            }
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>
